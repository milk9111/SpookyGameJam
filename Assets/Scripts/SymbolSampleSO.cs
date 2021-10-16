using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Symbol_", menuName = "ScriptableObjects/Symbols")]
public class SymbolSampleSO : ScriptableObject
{
    public GameObject sample;

    public OccultSymbol symbol;
}
