using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider monsterSlider;

    private MonsterTimer _monsterTimer;
    private OccultSymbolController _occultSymbolController;

    // Start is called before the first frame update
    void Start()
    {
        _monsterTimer = FindObjectOfType<MonsterTimer>();
        _occultSymbolController = FindObjectOfType<OccultSymbolController>();
    }

    // Update is called once per frame
    void Update()
    {

        monsterSlider.value = (_monsterTimer.monsterTimerSeconds - (_monsterTimer.endTime - _monsterTimer.currentTime)) / _monsterTimer.monsterTimerSeconds;
    }
}
