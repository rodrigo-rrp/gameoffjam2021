using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackState : State
{
    private float _lastAttackTime;
    private Tower _tower;

    public TowerAttackState(Tower tower, StateMachine sm) : base(tower, sm)
    {
        _tower = tower;
        _lastAttackTime = Time.time;
    }

    public override void OnEnter()
    {
    }

    public override void OnStay()
    {
        if (Time.time - _lastAttackTime > _tower.AttackCooldown)
        {
            GameObject _bullet = GameObject.Instantiate(_tower.Bullet, _tower.transform.position, Quaternion.identity);

            Bullet bullet = _bullet.GetComponent<Bullet>();
            bullet.Damage = _tower.Damage;
            bullet.Speed = _tower.BulletSpeed;
            bullet.Target = _tower.Target;

            _lastAttackTime = Time.time;
        }

        if (_tower.Target.Health <= 0) 
        {
            stateMachine.ChangeState(typeof(TowerIdleState));
        }
    }

    public override void OnExit()
    {
        _tower.Target = null;
    }
}
