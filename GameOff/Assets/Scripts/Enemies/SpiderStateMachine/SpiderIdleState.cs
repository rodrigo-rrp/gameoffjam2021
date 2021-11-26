using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderIdleState: State
{

    public SpiderIdleState(Spider Spider, StateMachine sm) : base(Spider, sm)
    {
    }

    public override void OnEnter()
    {
        stateMachine.ChangeState(typeof(SpiderMoveState));
    }

    public override void OnStay()
    {
    }

    public override void OnExit()
    {
    }
}
