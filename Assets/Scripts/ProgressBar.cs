using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider monsterSlider;
    public Slider victorySlider;

    private MonsterTimer _monsterTimer;
    private OccultSymbolController _occultSymbolController;

    // Start is called before the first frame update
    void Start()
    {
        _monsterTimer = FindObjectOfType<MonsterTimer>();
        _occultSymbolController = FindObjectOfType<OccultSymbolController>();
        _occultSymbolController.onSuccess += SuccessCallback;
    }

    // Update is called once per frame
    void Update()
    {
        if (_monsterTimer.disabled)
        {
            return;
        }

        monsterSlider.value = (_monsterTimer.monsterTimerSeconds - (_monsterTimer.endTime - Time.time)) / _monsterTimer.monsterTimerSeconds;
    }

    public void SuccessCallback()
    {
        //Debug.Log($"in here: {_occultSymbolController.GetUsedCount()} / {_occultSymbolController.totalSymbols} = {_occultSymbolController.GetUsedCount() / _occultSymbolController.totalSymbols}");
        victorySlider.value = _occultSymbolController.GetUsedCount() / (float)_occultSymbolController.totalSymbols;
        if (victorySlider.value >= monsterSlider.value)
        {
            _occultSymbolController.Victory();
        }
    }
}
