using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedShakeEvent : AbstractEvent
{
    private Bed _bed;

    public override void Enter()
    {
        _bed.BedShakeAnimation(Exit);
    }

    public override void Exit()
    {
        onExit();
    }

    protected override void ChildAwake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        _bed = FindObjectOfType<Bed>();
    }

    
}
