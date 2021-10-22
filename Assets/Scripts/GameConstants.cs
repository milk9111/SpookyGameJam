using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour
{
    public float distanceToNode;
    public Texture2D cursorTexture;

    public DifficultySO difficulty;

    public static GameConstants i;

    // Start is called before the first frame update
    void Awake()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(gameObject);
        }
    }
}
