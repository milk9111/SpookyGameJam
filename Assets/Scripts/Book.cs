using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public float transitionSpeed;

    public Transform samplePosition;
    public Transform symbolPosition;

    public bool disabled;

    public Action onTransition;

    private Action _lowerBookAnimCallback;

    private Transform _sample;
    private Transform _symbol;

    private bool _raising;
    private bool _transitioning;
    private Vector3 _targetPos;

    private MonsterTimer _monsterTimer;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _targetPos = Vector3.zero;
    }

    private void Start()
    {
        _monsterTimer = FindObjectOfType<MonsterTimer>();
        _monsterTimer.onComplete += OnCompleteCallback;
    }

    private void Update()
    {
        Debug.Log($"{disabled} - {_transitioning}");
        if (!disabled && !_transitioning && Input.GetKeyDown(KeyCode.Space))
        {
            _transitioning = true;
            if (_raising)
            {
                _targetPos = Vector3.zero;
            } else
            {
                onTransition();
                _targetPos = new Vector3(0, -10, 0);
                Time.timeScale = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.unscaledDeltaTime * transitionSpeed);
        if (_transitioning && transform.position == _targetPos)
        {
            _transitioning = false;
            if (_raising)
            {
                Time.timeScale = 1;
                onTransition();
            }

            _raising = !_raising;
        }
    }

    public void SetPositions(Transform sample, Transform symbol)
    {
        sample.parent = samplePosition;
        sample.position = new Vector3(-3.8f, 0.8f);

        symbol.parent = symbolPosition;
        symbol.position = new Vector3(4.5f, 0.8f);

        _sample = sample;
        _symbol = symbol;
    }

    public void DestroySymbolSample()
    {
        Destroy(_sample.gameObject);
        Destroy(_symbol.gameObject);
    }

    public void OnCompleteCallback()
    {
        disabled = true;
    }

    public void LowerBookAnim(Action callback)
    {
        _lowerBookAnimCallback = callback;
        _animator.SetTrigger("Lower");

    }

    public void Finish()
    {
        _lowerBookAnimCallback();
    }
}
