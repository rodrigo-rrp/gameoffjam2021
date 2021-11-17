using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackState : State
{
    private float _lastAttackTime;
    private Tower _tower;
    private Vector3 _cannonPosition;

    public TowerAttackState(Tower tower, StateMachine sm) : base(tower, sm)
    {
        _tower = tower;
        _cannonPosition = _tower.transform.Find("Cannon").position;
        _lastAttackTime = Time.time;
    }

    public override void OnEnter()
    {

    }

    public override void OnStay()
    {
        float angle = Vector3.Angle(_tower.transform.forward, _tower.Target.transform.position);
        Debug.Log(_tower.transform.eulerAngles.y);
        Debug.Log(angle);
        _tower.transform.eulerAngles = new Vector3(0, angle, 0);
        if (Time.time - _lastAttackTime > _tower.AttackCooldown)
        {
            GameObject _bullet = GameObject.Instantiate(_tower.Bullet, _cannonPosition, Quaternion.identity);

            Bullet bullet = _bullet.GetComponent<Bullet>();
            bullet.Damage = _tower.Damage;
            bullet.Speed = _tower.BulletSpeed;
            bullet.Target = _tower.Target;

            _lastAttackTime = Time.time;
        }

        if (_tower.Target == null || _tower.Target.Health <= 0) 
        {
            stateMachine.ChangeState(typeof(TowerIdleState));
        }
    }

    public override void OnExit()
    {
        _tower.Target = null;
    }
}
