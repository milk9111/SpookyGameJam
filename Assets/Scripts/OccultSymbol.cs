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

    private List<OccultSymbolNode> _nodes;
    private HashSet<int> _foundNodes;
    private bool _foundAllNodes;
    private int _firstNode;
    private GameObject _firstNodeObj;

    private bool _mousePressed;

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
        if (_foundAllNodes)
        {
            _lineRenderer.loop = true;
            Debug.Log("FOUND ALL NODES!!!");
            return;
        }

        if (!_canDraw)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if (_firstNode == -1)
            {
                var withinNodeRange = false;
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0;
                foreach (var node in _nodes)
                {
                    mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (Vector2.Distance(node.transform.position, mouseWorldPos) <= GameConstants.i.distanceToNode)
                    {
                        withinNodeRange = true;
                        break;
                    }
                }

                if (!withinNodeRange)
                {
                    var node = Instantiate(nodePrefab, mouseWorldPos, Quaternion.identity);
                    node.transform.parent = transform;
                    _nodes.Add(node);
                    _firstNode = _nodes.Count - 1;
                    _firstNodeObj = node.gameObject;
                    node.Init(_firstNode, OccultSymbolNodeCallback);
                }
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
        }
    }

    public void OccultSymbolNodeCallback(int index)
    {
        if (!_mousePressed)
        {
            return; 
        }

        Debug.Log($"hit node {index}");

        if (!_foundNodes.Contains(index))
        {
            _foundNodes.Add(index);
        }

        if (_firstNode == -1)
        {
            _firstNode = index;
        }

        _foundAllNodes = _foundNodes.Count >= _nodes.Count && index == _firstNode;
    }

    void OnMouseEnter()
    {
        Debug.Log("enter");
        ResetSymbol();
        _canDraw = true;
    }

    void OnMouseExit()
    {
        Debug.Log("exit");
        _canDraw = false;
    }

    private void ResetSymbol()
    {
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
