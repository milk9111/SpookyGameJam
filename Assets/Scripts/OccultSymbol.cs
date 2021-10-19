using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(LineRenderer))]
public class OccultSymbol : MonoBehaviour
{
    public OccultSymbolNode nodePrefab;

    private SpriteRenderer _spriteRenderer;
    private LineRenderer _lineRenderer;

    private bool _canDraw;
    private HashSet<Vector3> _points;

    private Action _successCallback;
    private Action _messedUpCallback;

    private List<OccultSymbolNode> _nodes;
    private HashSet<int> _foundNodes;
    private bool _foundAllNodes;
    private int _firstNode;
    private GameObject _firstNodeObj;

    private bool _mousePressed;

    public bool stopDrawingOverride { get; private set; }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _lineRenderer = GetComponent<LineRenderer>();

        _canDraw = false;
        _points = new HashSet<Vector3>();

        _nodes = GetComponentsInChildren<OccultSymbolNode>().ToList();
        _foundNodes = new HashSet<int>();
        _foundAllNodes = false;

        for (var i = 0; i < _nodes.Count; i++)
        {
            var node = _nodes[i];
            node.Init(i, OccultSymbolNodeCallback);
        }

        _firstNode = -1;

        _mousePressed = false;
    }

    void Update()
    {
        if (stopDrawingOverride)
        {
            return;
        }

        if (_foundAllNodes)
        {
            _lineRenderer.loop = true;
            _mousePressed = false;
            //Debug.Log("FOUND ALL NODES!!!");
            return;
        }

        if (!_canDraw)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log($"First node is {_firstNode}");
            if (_firstNode == -1)
            {
                Debug.Log("Adding first node!");
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0;
                var node = Instantiate(nodePrefab, mouseWorldPos, Quaternion.identity);
                node.transform.parent = transform;
                node.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, 0);
                node.transform.localScale = new Vector2(0.3f, 0.3f);
                node.GetComponent<SpriteRenderer>().sortingLayerName = "StartingNode";
                _nodes.Add(node);
                _firstNode = _nodes.Count - 1;
                _firstNodeObj = node.gameObject;
                node.Init(_firstNode, OccultSymbolNodeCallback);
            }
            
            _mousePressed = true;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 1;
            if (!_points.Contains(mousePos))
            {
                _points.Add(mousePos);
                _lineRenderer.positionCount = _points.Count;
                _lineRenderer.SetPosition(_points.Count - 1, mousePos);
            }
        } else if (Input.GetMouseButtonUp(0))
        {
            ResetSymbol();
            _mousePressed = false;
            _firstNode = -1;
            Debug.Log("Got mouse button up!");
        }
    }

    public void StopDrawing()
    {
        stopDrawingOverride = true;
    }

    public void SetStopDrawingOverride(bool stopDrawingOverride)
    {
        this.stopDrawingOverride = stopDrawingOverride;
    }

    public void Init(Action successCallback, Action messedUpCallback)
    {
        _successCallback = successCallback;
        _messedUpCallback = messedUpCallback;
    }

    public void OccultSymbolNodeCallback(int index)
    {
        if (!_mousePressed)
        {
            return; 
        }

        //Debug.Log($"hit node {index}");

        if (!_foundNodes.Contains(index))
        {
            _foundNodes.Add(index);
        }

        if (_firstNode == -1)
        {
            _firstNode = index;
        }

        _foundAllNodes = _foundNodes.Count >= _nodes.Count && index == _firstNode;
        if (_foundAllNodes)
        {
            _successCallback();
        }
    }

    public void SetNodeVisibility(bool showNodes)
    {
        foreach(var node in _nodes)
        {
            node.SetVisibility(showNodes);
        }
    }

    void OnMouseEnter()
    {
        if (_foundAllNodes || stopDrawingOverride)
        {
            return;
        }

        Cursor.SetCursor(GameConstants.i.cursorTexture, Vector2.zero, CursorMode.Auto);

        Debug.Log("enter");
        _canDraw = true;
        _firstNode = -1;
    }

    void OnMouseExit()
    {
        if (_foundAllNodes || stopDrawingOverride)
        {
            return;
        }

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        Debug.Log("exit");
        if (_mousePressed)
        {
            ResetSymbol();
        }
        
        _canDraw = false;
        _firstNode = -1;
    }

    public void ResetSymbol()
    {
        _messedUpCallback();

        if (_firstNodeObj != null)
        {
            _nodes.RemoveAt(_firstNode);
            Destroy(_firstNodeObj);
            _firstNodeObj = null;
        }

        _lineRenderer.positionCount = 0;
        _points = new HashSet<Vector3>();
        _foundNodes = new HashSet<int>();
        _firstNode = -1;
    }
}
