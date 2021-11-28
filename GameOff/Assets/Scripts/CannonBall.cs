using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : TowerAmmo
{
    public float DamageRadius;
    private ParticleSystem _explosionParticles;
    private bool _exploded = false;
    private AudioSource _audioSource;

    void Awake()
    {
        _explosionParticles = transform.Find("BigExplosionEffect").GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
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
            Debug.Log("Collider: " + collider.name);
            if (collider.gameObject.tag == "Enemies")
            {
                collider.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
            }
        }
        _exploded = true;
        _explosionParticles.Play();
        _audioSource.PlayOneShot(_audioSource.clip);
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
