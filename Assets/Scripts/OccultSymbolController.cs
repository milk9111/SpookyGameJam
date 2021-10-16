using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(UnitSoundPlayer))]
public class OccultSymbolController : MonoBehaviour
{
    public OccultSymbol activeSymbol;
    public AudioClip successClip;
    public AudioClip messedUpClip;
    public float nextSymbolTimerSeconds;

    public bool showNodes;
    public bool onlyOneSymbol;

    public List<OccultSymbol> symbolList;

    public Action onVictory;

    private List<OccultSymbol> _remainingSymbols;
    private List<OccultSymbol> _usedSymbols;

    private UnitSoundPlayer _soundPlayer;

    private MonsterTimer _monsterTimer;

    private void Awake()
    {
        _soundPlayer = GetComponent<UnitSoundPlayer>();
        _remainingSymbols = new List<OccultSymbol>();
        foreach (var symbol in symbolList)
        {
            _remainingSymbols.Add(symbol);
        }

        _usedSymbols = new List<OccultSymbol>();
    }

    private void Start()
    {
        if (activeSymbol == null)
        {
            activeSymbol = FindObjectOfType<OccultSymbol>();
        }

        if (activeSymbol == null)
        {
            activeSymbol = Instantiate(symbolList[Random.Range(0, symbolList.Count)], Vector3.zero, Quaternion.identity);
        }

        var removingIndex = -1;
        for (var i = 0; i < _remainingSymbols.Count; i++)
        {
            if (activeSymbol.name == _remainingSymbols[i].name)
            {
                _usedSymbols.Add(_remainingSymbols[i]);
                removingIndex = i;
            }
        }

        if (removingIndex != -1)
        {
            _remainingSymbols.RemoveAt(removingIndex);
        }

        _monsterTimer = FindObjectOfType<MonsterTimer>();
        _monsterTimer.onComplete += OnCompleteCallback;
        activeSymbol.Init(SuccessCallback, MessedUpCallback);
        activeSymbol.SetNodeVisibility(showNodes);
    }

    public void OnCompleteCallback()
    {
        activeSymbol.StopDrawing();
    }

    public void SuccessCallback()
    {
        
        if (!_remainingSymbols.Any() || onlyOneSymbol)
        {
            _monsterTimer.disabled = true;
            StartCoroutine(WaitForVictory());
        } else
        {
            _soundPlayer.PlayOneShot(successClip, 0.5f);
            StartCoroutine(WaitForNextSymbol());
        }
    }

    public void MessedUpCallback()
    {
        _soundPlayer.PlayOneShot(messedUpClip);
    }

    IEnumerator WaitForVictory()
    {
        yield return new WaitForSeconds(0.5f);

        if (activeSymbol != null)
        {
            Destroy(activeSymbol.gameObject);
        } else
        {
            Debug.LogWarning("activeSymbol was null during WaitForVictory!");
        }
        
        onVictory();
    }

    IEnumerator WaitForNextSymbol()
    {
        yield return new WaitForSeconds(nextSymbolTimerSeconds);

        var nextIndex = Random.Range(0, _remainingSymbols.Count);

        var oldObject = activeSymbol.gameObject;
        activeSymbol = null;
        Debug.Log($"nextIndex: {nextIndex}");
        activeSymbol = Instantiate(_remainingSymbols[nextIndex], Vector3.zero, Quaternion.identity);
        activeSymbol.Init(SuccessCallback, MessedUpCallback);
        activeSymbol.SetNodeVisibility(showNodes);

        _usedSymbols.Add(_remainingSymbols[nextIndex]);
        _remainingSymbols.RemoveAt(nextIndex);

        Destroy(oldObject);
    }
}
