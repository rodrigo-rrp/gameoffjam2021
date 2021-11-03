using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDeadState: State
{
    public TowerDeadState(Tower tower, StateMachine sm) : base(tower, sm)
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