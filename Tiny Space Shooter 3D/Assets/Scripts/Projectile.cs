using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage = 0;
    ParticlePlayer particlePlayer = null;
    public AudioSource hitSound = null;


    private void Awake()
    {
        StartCoroutine(DestroyGameObject());
        particlePlayer = FindObjectOfType<ParticlePlayer>();
        transform.parent = Camera.main.transform; // Poola projektiler, transform sätts för att behålla hastigheten relevant
    }

    private void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * 16, Time.deltaTime);
        var bulletPosition = transform.position + Camera.main.transform.position;
            bulletPosition = Vector3.Normalize(bulletPosition);

        transform.Translate(Vector3.up * Time.deltaTime * 16);
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
        var damageableObject = other.gameObject.GetComponentInParent<IDamageAbleObject>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(damage);
            particlePlayer.FetchAndPlayParticleAtPosition(Particles.ProjectileHit, other.transform.position);

            FindObjectOfType<UiManager>().AddHitCount();
            FindObjectOfType<UiManager>().ShakeHitMultiplier();

            //hitSound.transform.parent = null;
            //hitSound.Play();
            Destroy(this.gameObject);
        }
    }
}