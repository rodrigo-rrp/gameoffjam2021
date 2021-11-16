using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    public float Health;
    [Range(0, 10)]
    public float Armor;
    public float Speed;
    public int Reward;
    public NavMeshAgent agent;

    public override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 5);
    }

    public void TakeDamage(float damage)
    {

        Health -= damage * (Armor > 0 ? (Armor / 10) : 1);
        if (Health <= 0)
        {
            State deadState = stateMachine.states.First(x => x.GetType().ToString().Contains("Dead"));
            stateMachine.ChangeState(deadState.GetType());
            GameManager.instance.AddCurrency(Reward);
            GameManager.instance.AddEnemyKill();
        }
    }
}
