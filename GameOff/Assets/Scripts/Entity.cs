using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float Damage;
    public float DistanceToAttack;
    public float AttackCooldown;
    public float AttackSpeed;

    public StateMachine stateMachine;
    public Transform _transform;

    public virtual void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    public virtual void Start()
    {
        stateMachine.Start();
    }

    public virtual void Update()
    {
        stateMachine.Update();
    }

    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, DistanceToAttack);
    }

    void OnDrawGizmos()
    {
        if (stateMachine != null && stateMachine.currentState != null)
            UnityEditor.Handles.Label(transform.position + transform.up, stateMachine.currentState.ToString());
    }
}
