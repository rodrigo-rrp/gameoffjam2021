using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntAttackState: State
{
    private float _lastAttackTime;
    private Ant _ant;

    public AntAttackState(Ant ant, StateMachine sm) : base(ant, sm)
    {
        _ant = ant;
    }

    public override void OnEnter()
    {
        _lastAttackTime = Time.time;
    }

    public override void OnStay()
    {
        if (Time.time - _lastAttackTime > _ant.AttackCooldown)
        {
            GameManager.instance.Damage(_ant.Damage);
            _lastAttackTime = Time.time;
        }
    }

    public override void OnExit()
    {
    }
}
