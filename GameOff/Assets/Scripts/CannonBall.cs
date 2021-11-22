using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : TowerAmmo
{
    public float DamageRadius;
    private ParticleSystem _explosionParticles;
    private bool _exploded = false;

    void Awake()
    {
        _explosionParticles = transform.Find("BigExplosionEffect").GetComponent<ParticleSystem>(); ;
    }

    public override void Update()
    {
        if (_exploded)
        {
            return;
        }
        if (Target == null)
        {
            Explode();
            return;
        }
        base.Update();
    }

    void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, DamageRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Enemies")
            {
                collider.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
            }
        }
        _exploded = true;
        _explosionParticles.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        Invoke("DestroyCannon", 4f);
        GetComponent<CannonBall>().enabled = false;
    }

    void DestroyCannon()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, DamageRadius);
    }
}
