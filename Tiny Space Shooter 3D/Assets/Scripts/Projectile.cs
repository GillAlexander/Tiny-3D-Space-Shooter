using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage = 0;
    ParticlePlayer particlePlayer = null;

    private void Awake()
    {
        StartCoroutine(DestroyGameObject());
        particlePlayer = FindObjectOfType<ParticlePlayer>();
    }

    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(this.gameObject);
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    var damageableObject = collision.collider.GetComponent<DamageAbleObject>();
    //    if (damageableObject != null)
    //    {
    //        damageableObject.TakeDamage(damage);
    //        Destroy(this.gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        var damageableObject = other.gameObject.GetComponentInParent<DamageAbleObject>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(damage);
            particlePlayer.FetchAndPlayParticleAtPosition(Particles.ProjectileHit, other.transform.position);
            Destroy(this.gameObject);
        }
    }
}