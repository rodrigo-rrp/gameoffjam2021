using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackState: State
{
    private float _lastAttackTime;
    private Tower _tower;

    public TowerAttackState(Tower tower, StateMachine sm) : base(tower, sm)
    {
        _tower = tower;
    }

    public override void OnEnter()
    {
        _lastAttackTime = Time.time;
    }

    public override void OnStay()
    {
        if (Time.time - _lastAttackTime > _tower.AttackCooldown)
        {
            //GameManager.instance.Damage(_tower.Damage);
            GameObject _bullet = GameObject.Instantiate(_tower.Bullet, _tower.transform.position, Quaternion.identity);
            _lastAttackTime = Time.time;
        }
    }
    
    public override void OnExit()
    {
    }
}
