using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, DamageAbleObject
{
    //private float damage = 20f;
    //private float powerupTime = 5f;
    private float healthPoints = 10;

    [SerializeField] private GameObject bullet1 = null;
    [SerializeField] private Transform[] firingPositions = null;

    private Renderer playerRenderer = null;
    private Coroutine damageIenumerator;
    private int enemiesKilled = 0;
    public int numberOfVisualDamageLoops;
    public float damageWaitTime;


    public float HealthPoints { get => healthPoints; set => healthPoints = value; }
    public int EnemiesKilled { get => enemiesKilled; set => enemiesKilled = value; }

    public void Reset()
    {
        enemiesKilled = 0;
        healthPoints = 10;
    }

    private void Awake()
    {
        healthPoints = 10;
        playerRenderer = GetComponentInChildren<Renderer>();
    }

    private void Update()
    {
        Death();
    }

    public void Fire()
    {
        for (int i = 0; i < firingPositions.Length; i++)
        {
            FindObjectOfType<ParticlePlayer>()?.FetchAndPlayParticleAtPosition(Particles.ProjectileFire, firingPositions[i].position + Vector3.up / 2);
            var bullet = Instantiate(bullet1, new Vector3(firingPositions[i].position.x, firingPositions[i].position.y, 0), Quaternion.identity);
            var projectile = bullet.GetComponent<Projectile>();
            projectile.SetDamage(1);
        }
    }

    public void Death()
    {
        if (healthPoints <= 0)
        {
            Destroy(this.gameObject);
            FindObjectOfType<ParticlePlayer>().FetchAndPlayParticleAtPosition(Particles.PlayerDeath, transform.position);
        }
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        TakeVisualDamage();
    }

    public void TakeVisualDamage()
    {
        if (damageIenumerator == null)
            damageIenumerator = StartCoroutine(VizualizeDamage());
    }

    private IEnumerator VizualizeDamage()
    {
        InvincibilityMode(true);
        for (int i = 0; i < numberOfVisualDamageLoops; i++)
        {
            yield return new WaitForSeconds(damageWaitTime);
            if (playerRenderer.enabled)
            {
                playerRenderer.enabled = false;
            }
            else
                playerRenderer.enabled = true;
        }
        playerRenderer.enabled = true;
        damageIenumerator = null;
        InvincibilityMode(false);
    }

    public void AddKillCount()
    {
        enemiesKilled++;
    }

    private void InvincibilityMode(bool isInvincibal)
    {
        GetComponentInChildren<Collider>().enabled = !isInvincibal;
    }
}