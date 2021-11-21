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
    private ParticleSystem _bloodParticles;

    public override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        _bloodParticles = transform.Find("BloodSprayEffect")?.GetComponent<ParticleSystem>();
    }

    void OnDrawGizmos()
    {
        // Gizmos.color = Color.blue;
        // Gizmos.DrawLine(transform.position, transform.forward);
    }

    public override void Update()
    {
        base.Update();
        // Debug.DrawLine(transform.position, (transform.forward - transform.position) * 5f, Color.blue);
    }

    public void TakeDamage(float damage)
    {
        if (_bloodParticles != null)
            _bloodParticles.Play();
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
