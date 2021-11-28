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
        _lastAttackTime = Time.time;
    }

    public override void OnEnter()
    {
        _tower.transform.LookAt(new Vector3(_tower.Target.transform.position.x, _tower.transform.position.y, _tower.Target.transform.position.z));
    }

    public override void OnStay()
    {
        if (Time.time - _lastAttackTime > _tower.AttackCooldown)
        {
            _cannonPosition = _tower.transform.Find("Cannon").position;
            GameObject _ammoGO = GameObject.Instantiate(_tower.Bullet, _cannonPosition, Quaternion.identity);
            _tower.PlaySFX(_tower.ShotSFX);

            TowerAmmo ammo = _ammoGO.GetComponent<TowerAmmo>();
            ammo.Damage = _tower.Damage;
            ammo.Speed = _tower.BulletSpeed;
            ammo.Target = _tower.Target;

            _lastAttackTime = Time.time;
        }

        if (_tower.Target == null || _tower.Target.Health <= 0) 
        {
            stateMachine.ChangeState(typeof(TowerIdleState));
        }
        else // TODO: smooth this
            _tower.transform.LookAt(new Vector3(_tower.Target.transform.position.x, _tower.transform.position.y, _tower.Target.transform.position.z));
    }

    public override void OnExit()
    {
        _tower.Target = null;
    }
}
