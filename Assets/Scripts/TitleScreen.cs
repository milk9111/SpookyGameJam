using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public DifficultySO easy;
    public DifficultySO hard;

    public GameObject mainScreen;
    public GameObject tutorialScreen;

    private void Start()
    {
        mainScreen.SetActive(true);
        tutorialScreen.SetActive(false);
    }

    public void Easy()
    {
        GameConstants.i.difficulty = easy;
        SceneManager.LoadScene("Game");
    }

    public void Hard()
    {
        GameConstants.i.difficulty = hard;
        SceneManager.LoadScene("Game");
    }

    public void Tutorial()
    {
        mainScreen.SetActive(false);
        tutorialScreen.SetActive(true);
    }

    public void Back()
    {
        mainScreen.SetActive(true);
        tutorialScreen.SetActive(false);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
