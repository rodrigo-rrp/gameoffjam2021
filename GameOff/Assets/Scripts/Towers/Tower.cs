using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Tower : Entity
{
    public GameObject Bullet;
    public Enemy Target;
    public float BulletSpeed;
    public float RotationSpeed;
    public int Cost;

    public AudioClip ShotSFX;

    public override void Awake()
    {
        stateMachine = new StateMachine();
        stateMachine.AddState(new TowerIdleState(this, stateMachine));
        stateMachine.AddState(new TowerAttackState(this, stateMachine));
        stateMachine.AddState(new TowerDeadState(this, stateMachine));
        base.Awake();

        transform.parent.Find("MiniMap").gameObject.SetActive(true);
    }
}
