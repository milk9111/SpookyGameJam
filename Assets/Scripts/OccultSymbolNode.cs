using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OccultSymbolNode : MonoBehaviour
{
    private int _index;
    private Action<int> _callback;

    private SpriteRenderer _spriteRenderer;
    private bool _init;

    public void Init(int index, Action<int> callback)
    {
        _index = index;
        _callback = callback;
        _init = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_init)
        {
            return;
        }

        if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) <= GameConstants.i.distanceToNode) 
        {
            _spriteRenderer.color = Color.green;
            _callback(_index);
        }
    }
}
