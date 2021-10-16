using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public Transform samplePosition;
    public Transform symbolPosition;

    private Transform _sample;
    private Transform _symbol;

    public void SetPositions(Transform sample, Transform symbol)
    {
        sample.parent = samplePosition;
        sample.position = new Vector3(-3.5f, 1f);

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
}
