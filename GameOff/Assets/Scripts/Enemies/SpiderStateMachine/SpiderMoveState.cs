using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpiderMoveState: State
{
    private Transform _target;
    private Spider _Spider;

    const string ANIMATION = "Spider_Armature|walk_ani_vor";

    public SpiderMoveState(Spider Spider, StateMachine sm) : base(Spider, sm)
    {
        _Spider = Spider;
    }

    public override void OnEnter()
    {
        _Spider.GetComponent<Animation>().Play(ANIMATION);
        _Spider.GetComponent<Animation>()[ANIMATION].speed = 2f;
        _target = GameObject.FindGameObjectWithTag("Target").transform;
        _Spider.agent.destination = _target.position;
    }

    public override void OnStay()
    {
        Debug.DrawLine(_Spider.transform.position, _Spider.transform.position - _Spider.transform.forward, Color.blue);
        _Spider.transform.LookAt(_target);
        if (_target != null)
        { 
            if (Vector3.Distance(owner._transform.position, _target.position) < _Spider.DistanceToAttack)
            {
                stateMachine.ChangeState(typeof(SpiderAttackState));
            }
        }
    }

    public override void OnExit()
    {
        _Spider.agent.enabled = false;
    }
}