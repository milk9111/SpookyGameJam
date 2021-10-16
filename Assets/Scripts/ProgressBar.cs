using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBar : MonoBehaviour
{
    private Slider _slider;
    private MonsterTimer _monsterTimer;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _monsterTimer = FindObjectOfType<MonsterTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_monsterTimer.disabled)
        {
            return;
        }

        _slider.value = (_monsterTimer.monsterTimerSeconds - (_monsterTimer.endTime - Time.time)) / _monsterTimer.monsterTimerSeconds;
    }
}
