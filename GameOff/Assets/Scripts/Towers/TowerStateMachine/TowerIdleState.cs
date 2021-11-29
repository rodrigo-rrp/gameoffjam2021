using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerIdleState : State
{
    private Enemy[] _targets;
    private Tower _tower;

    public TowerIdleState(Tower tower, StateMachine sm) : base(tower, sm)
    {
        _tower = tower;
    }

    public override void OnEnter()
    {
    }

    public override void OnStay()
    {
        _targets = GameObject.FindObjectsOfType<Enemy>();
        foreach (var target in _targets)
        {
            if (target.Health > 0 && Vector3.Distance(owner.transform.position, target.transform.position) < owner.DistanceToAttack)
            {
                _tower.Target = target;
                stateMachine.ChangeState(typeof(TowerAttackState));
                break;
            }
        }
        _tower.transform.eulerAngles = new Vector3(0, _tower.transform.eulerAngles.y + _tower.RotationSpeed * Time.deltaTime, 0);
    }

    public override void OnExit()
    {
    }
}
