using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : TowerAmmo
{
    public override void Update()
    {
        if (Target == null || Target.Health <= 0)
        {
            Destroy(gameObject);
            return;
        }
        base.Update();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with: " + collision.gameObject);
        if (collision.gameObject.tag == "Enemies")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
