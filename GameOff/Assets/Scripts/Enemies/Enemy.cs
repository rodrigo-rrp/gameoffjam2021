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
    internal Animator _animator;
    private bool _IsDead = false;
    public override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        _bloodParticles = transform.Find("BloodSprayEffect")?.GetComponent<ParticleSystem>();
        _lifeBarRectTransform = transform.Find("Canvas/LifeBar").GetComponent<RectTransform>();
        _lifeBarWidth = _lifeBarRectTransform.sizeDelta.x;
        _maxHealth = Health;
        _animator = GetComponent<Animator>();
    }
    public override void Update()
    {
        _lifeBarRectTransform.gameObject.transform.LookAt(Camera.main.transform);
        if (_IsDead)
        { 
            _transform.position = new Vector3(_transform.position.x, _transform.position.y -0.1f * Time.deltaTime, _transform.position.z);
        }
        base.Update();
    }

    public void SetDead() 
    {
        agent.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        _IsDead = true;
        Invoke("Destroy", 8f);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        if (_bloodParticles != null)
            _bloodParticles.Play();
        Health += -damage + (Armor > 0 ? damage * (Armor / 10) : 0);

        if (Health <= 0)
        {
            State deadState = stateMachine.states.First(x => x.GetType().ToString().Contains("Dead"));
            stateMachine.ChangeState(deadState.GetType());
            GameManager.instance.AddCurrency(Reward);
            GameManager.instance.AddEnemyKill();
        }
        
        _lifeBarRectTransform.sizeDelta = new Vector2(_lifeBarWidth * (Health / _maxHealth), _lifeBarRectTransform.sizeDelta.y);
    }
}
