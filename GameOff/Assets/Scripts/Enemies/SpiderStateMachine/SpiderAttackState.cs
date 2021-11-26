using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttackState: State
{
    private float _lastAttackTime;
    private Spider _Spider;

    
    const string ATTACK_ANIMATION = "Spider_Armature|Attack";
    const string IDLE_ANIMATION = "Spider_Armature|warte_pose";

    public SpiderAttackState(Spider Spider, StateMachine sm) : base(Spider, sm)
    {
        _Spider = Spider;
    }

    public override void OnEnter()
    {
        _lastAttackTime = Time.time;
    }

    public override void OnStay()
    {
        if (Time.time - _lastAttackTime > _Spider.AttackCooldown)
        {
            GameManager.instance.Damage(_Spider.Damage);
            _Spider.GetComponent<Animation>().Play(ATTACK_ANIMATION);
            _Spider.GetComponent<Animation>().PlayQueued(IDLE_ANIMATION);

            _lastAttackTime = Time.time;
        }
    }

    public override void OnExit()
    {
    }
}
