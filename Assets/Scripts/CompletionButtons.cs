using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompletionButtons : MonoBehaviour
{
    public List<Button> buttons;

    // Start is called before the first frame update
    void Awake()
    {
        Hide();
    }

    public void Hide()
    {
        foreach(var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void Show()
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
