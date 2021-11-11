using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ant : Enemy
{
    public override void Awake() {
        stateMachine = new StateMachine();
        stateMachine.AddState(new AntIdleState(this, stateMachine));
        stateMachine.AddState(new AntMoveState(this, stateMachine));
        stateMachine.AddState(new AntAttackState(this, stateMachine));
        stateMachine.AddState(new AntDeadState(this, stateMachine));
        base.Awake();
    }

    
}
