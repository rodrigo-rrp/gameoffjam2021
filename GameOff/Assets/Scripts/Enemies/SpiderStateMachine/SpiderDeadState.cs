using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDeadState : State
{
    private Spider _Spider;
    public SpiderDeadState(Spider Spider, StateMachine sm) : base(Spider, sm)
    {
        _Spider = Spider;
    }

    public override void OnEnter()
    {
        _Spider.GetComponent<Animation>().Play("Spider_Armature|die");
        _Spider.SetDead();
    }

    public override void OnStay()
    {
    }

    public override void OnExit()
    {
    }
}