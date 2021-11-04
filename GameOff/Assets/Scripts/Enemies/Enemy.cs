using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Entity
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 5);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            State deadState = stateMachine.states.First(x => x.GetType().ToString().Contains("Dead"));
            stateMachine.ChangeState(deadState.GetType());
        }
    }
}
