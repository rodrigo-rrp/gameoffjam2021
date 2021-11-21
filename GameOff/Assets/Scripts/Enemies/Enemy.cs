using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    public float Health;
    private float _maxHealth;
    [Range(0, 10)]
    public float Armor;
    public float Speed;
    public int Reward;
    public NavMeshAgent agent;
    private ParticleSystem _bloodParticles;

    private RectTransform _lifeBarRectTransform;
    private float _lifeBarWidth;

    public override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        _bloodParticles = transform.Find("BloodSprayEffect")?.GetComponent<ParticleSystem>();
        _lifeBarRectTransform = transform.Find("Canvas/LifeBar").GetComponent<RectTransform>();
        _lifeBarWidth = _lifeBarRectTransform.sizeDelta.x;
        _maxHealth = Health;
    }
    public override void Update()
    {
        base.Update();
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
        else
        {
            _lifeBarRectTransform.sizeDelta = new Vector2(_lifeBarWidth * (Health / _maxHealth), _lifeBarRectTransform.sizeDelta.y);
        }
    }
}
