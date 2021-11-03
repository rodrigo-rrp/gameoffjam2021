using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerIdleState: State
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
        Debug.Log(_targets.Length);
        foreach(var target in _targets) {
            if (Vector3.Distance(owner.transform.position, target.transform.position) < owner.DistanceToAttack)
            {
                _tower.Target = target;
                stateMachine.ChangeState(typeof(TowerAttackState));
            }
        }
    }

    public override void OnExit()
    {
    }
}
