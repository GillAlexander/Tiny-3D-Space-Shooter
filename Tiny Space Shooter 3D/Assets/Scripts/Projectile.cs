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

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * 16, Time.deltaTime);
    }

    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(0.8f);
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
        Debug.Log(damageableObject);
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(damage);
            particlePlayer.FetchAndPlayParticleAtPosition(Particles.ProjectileHit, other.transform.position);
            FindObjectOfType<UiManager>().AddHitCount();
            FindObjectOfType<UiManager>().ShakeHitMultiplier();
            Destroy(this.gameObject);
        }
    }
}