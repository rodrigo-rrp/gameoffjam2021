using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntDeadState: State
{
    public AntDeadState(Ant ant, StateMachine sm) : base(ant, sm)
    {
    }

    public override void OnEnter()
    {
        GameObject.Destroy(owner.gameObject);
    }

    public override void OnStay()
    {
    }

    public override void OnExit()
    {
    }
}