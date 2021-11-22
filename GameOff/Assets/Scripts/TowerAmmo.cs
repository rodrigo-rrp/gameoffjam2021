using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAmmo : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public Enemy Target;

    public virtual void Update()
    {
        transform.LookAt(Target.transform);
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
    }
}
