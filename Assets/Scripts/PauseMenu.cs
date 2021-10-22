using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UnitSoundPlayer))]
public class PauseMenu : MonoBehaviour
{
    public GameObject holder;

    private MonsterTimer _monsterTimer;
    private OccultSymbolController _occultSymbolController;
    private UnitSoundPlayer _soundPlayer;
    private End _end;

    private bool _paused;

    private void Awake()
    {
        _soundPlayer = GetComponent<UnitSoundPlayer>();
        _paused = false;
    }

    private void Start()
    {
        _monsterTimer = FindObjectOfType<MonsterTimer>();
        _occultSymbolController = FindObjectOfType<OccultSymbolController>();
        _end = FindObjectOfType<End>();

        holder.SetActive(false);
    }

    private void Update()
    {
        if (!_end.end && Input.GetKeyDown(KeyCode.Escape))
        {
            _paused = !_paused;
            holder.SetActive(_paused);

            _monsterTimer.disabled = _paused;
            _occultSymbolController.SetStopDrawingOverride(_paused);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
