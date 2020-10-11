using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, DamageAbleObject
{
    [SerializeField] private int damage = 1;
    protected float healthPoints = 10;
    private ParticlePlayer particlePlayer = null;
    private AnimationCurve animationCurve = null;
    private float time = 0;

    private void Awake()
    {
        particlePlayer = FindObjectOfType<ParticlePlayer>();
    }

    private void Update()
    {
        if (healthPoints <= 0)
        {
            Death();
        }

        if (animationCurve != null) 
        {
            time += Time.deltaTime;
            float evalue = animationCurve.Evaluate(time);
            transform.Translate(new Vector3(evalue, -1, 0) * Time.deltaTime * 4);
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
        particlePlayer.FetchAndPlayParticleAtPosition(Particles.EnemyDeath, transform.position);
    }

    public void AddCurve(AnimationCurve curve)
    {
        animationCurve = curve;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    var player = collision.collider.GetComponent<Player>();
    //    if (player != null)
    //    {
    //        player.TakeDamage(damage);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponentInParent<Player>();
        if (player != null)
        {
            player.TakeDamage(1);
        }
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
    }
}