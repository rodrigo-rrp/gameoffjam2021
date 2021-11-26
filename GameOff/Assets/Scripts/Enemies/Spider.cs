using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public override void Awake() {
        stateMachine = new StateMachine();
        stateMachine.AddState(new SpiderIdleState(this, stateMachine));
        stateMachine.AddState(new SpiderMoveState(this, stateMachine));
        stateMachine.AddState(new SpiderAttackState(this, stateMachine));
        stateMachine.AddState(new SpiderDeadState(this, stateMachine));
        base.Awake();
    }

    
}