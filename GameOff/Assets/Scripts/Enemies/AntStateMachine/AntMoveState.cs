using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AntMoveState: State
{
    private Transform _target;
    private Ant _ant;

    public AntMoveState(Ant ant, StateMachine sm) : base(ant, sm)
    {
        _ant = ant;
    }

    public override void OnEnter()
    {
        _target = GameObject.FindGameObjectWithTag("Target").transform;
        //_ant.agent.destination = _target.position;
    }

    public override void OnStay()
    {
        // Ant ant = owner.GetComponent<Ant>();
        Debug.DrawLine(_ant.transform.position, _ant.transform.position - _ant.transform.forward, Color.blue);
        if (_target != null)
        {
            _ant._transform.position = Vector3.MoveTowards(_ant._transform.position, _target.position, Time.deltaTime * _ant.Speed);   
            if (Vector3.Distance(owner._transform.position, _target.position) < _ant.DistanceToAttack)
            {
                stateMachine.ChangeState(typeof(AntAttackState));
            }
        }
    }

    public override void OnExit()
    {
    }
}