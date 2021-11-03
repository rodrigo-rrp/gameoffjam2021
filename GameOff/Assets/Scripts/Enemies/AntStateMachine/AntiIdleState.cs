using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntIdleState: State
{

    public AntIdleState(Ant ant, StateMachine sm) : base(ant, sm)
    {
    }

    public override void OnEnter()
    {
        stateMachine.ChangeState(typeof(AntMoveState));
    }

    public override void OnStay()
    {
    }

    public override void OnExit()
    {
    }
}
