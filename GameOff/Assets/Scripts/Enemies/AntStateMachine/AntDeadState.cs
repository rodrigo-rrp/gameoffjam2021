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
        Debug.Log("Entering Dead State");
        GameObject.Destroy(owner.gameObject);
    }

    public override void OnStay()
    {
        Debug.Log("Entering Dead State");
    }

    public override void OnExit()
    {
        Debug.Log("Entering Dead State");
    }
}