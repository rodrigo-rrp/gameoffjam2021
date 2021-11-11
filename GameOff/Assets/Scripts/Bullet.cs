using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public Enemy Target;

    void Update()
    {
        if (Target == null) {
            Destroy(gameObject);
            return;
        }
        transform.LookAt(Target.transform);
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 5);
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
