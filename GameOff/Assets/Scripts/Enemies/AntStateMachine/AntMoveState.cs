using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AntMoveState: State
{
    private Transform _target;

    public AntMoveState(Ant ant, StateMachine sm) : base(ant, sm)
    {
    }

    public override void OnEnter()
    {
        _target = GameObject.FindGameObjectWithTag("Target").transform;
    }

    public override void OnStay()
    {
        Ant ant = owner.GetComponent<Ant>();
        if (_target != null)
        {
            ant._transform.position = Vector3.MoveTowards(ant._transform.position, _target.position, Time.deltaTime * ant.Speed);
            if (Vector3.Distance(ant._transform.position, _target.position) < ant.DistanceToAttack)
            {
                stateMachine.ChangeState(typeof(AntAttackState));
            }
        }
    }

    public override void OnExit()
    {
    }
}