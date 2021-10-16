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

    public int totalSymbols;

    public List<SymbolSampleSO> symbolList;

    public Action onVictory;
    public Action onSuccess;

    private int _usedCount;

    private UnitSoundPlayer _soundPlayer;

    private MonsterTimer _monsterTimer;

    private Book _book;

    private void Awake()
    {
        _soundPlayer = GetComponent<UnitSoundPlayer>();

        if (totalSymbols == 0)
        {
            totalSymbols = symbolList.Count;
        }
    }

    private void Start()
    {
        _book = FindObjectOfType<Book>();

        if (activeSymbol == null)
        {
            activeSymbol = FindObjectOfType<OccultSymbol>();
        }

        if (activeSymbol == null)
        {
            activeSymbol = Instantiate(symbolList[Random.Range(0, symbolList.Count)].symbol, Vector3.zero, Quaternion.identity);
        }

        for (var i = 0; i < symbolList.Count; i++)
        {
            if (activeSymbol.name.Contains(symbolList[i].symbol.name))
            {
                _usedCount++;
                var sample = Instantiate(symbolList[i].sample, Vector3.zero, Quaternion.identity);

                _book.SetPositions(sample.transform, activeSymbol.transform);
            }
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

    public int GetUsedCount()
    {
        return _usedCount;
    }

    public void Victory()
    {
        _monsterTimer.disabled = true;
        StartCoroutine(WaitForVictory());
    }

    public void SuccessCallback()
    {
        if (_usedCount == totalSymbols || onlyOneSymbol)
        {
            Victory(); 
        } else
        {
            onSuccess();
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
            _book.DestroySymbolSample();
        } else
        {
            Debug.LogWarning("activeSymbol was null during WaitForVictory!");
        }
        
        onVictory();
    }

    IEnumerator WaitForReset()
    {
        yield return new WaitForSeconds(nextSymbolTimerSeconds);
        ResetSymbol();
    }

    IEnumerator WaitForNextSymbol()
    {
        yield return new WaitForSeconds(nextSymbolTimerSeconds);
        _book.DestroySymbolSample();
        StartCoroutine(WaitForReset());
    }

    private void ResetSymbol()
    {
        var nextIndex = Random.Range(0, symbolList.Count);

        activeSymbol = null;
        Debug.Log($"nextIndex: {nextIndex} - Remaining Count: {symbolList.Count}");
        activeSymbol = Instantiate(symbolList[nextIndex].symbol, Vector3.zero, Quaternion.identity);
        activeSymbol.Init(SuccessCallback, MessedUpCallback);
        activeSymbol.SetNodeVisibility(showNodes);

        var sample = Instantiate(symbolList[nextIndex].sample, Vector3.zero, Quaternion.identity);

        _usedCount++;

        
        _book.SetPositions(sample.transform, activeSymbol.transform);
    }
}
