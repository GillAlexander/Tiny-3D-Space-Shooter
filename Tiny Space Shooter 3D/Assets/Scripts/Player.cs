using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageAbleObject, IReset
{
    //private float damage = 20f;
    //private float powerupTime = 5f;
    [SerializeField] private float healthPoints = 5;

    private Renderer playerRenderer = null;
    private Coroutine damageIenumerator;
    private PowerUpManager powerUpManager = null;

    private int enemiesKilled = 0;
    public int numberOfVisualDamageLoops;
    public float damageWaitTime;

    public event Action<int> RecivedPowerPoint;

    public float HealthPoints { get => healthPoints; set => healthPoints = value; }
    public int EnemiesKilled { get => enemiesKilled; set => enemiesKilled = value; }

    public void ResetValues()
    {
        enemiesKilled = 0;
        healthPoints = 5;
    }

    private void Awake()
    {
        healthPoints = 5;
        powerUpManager = FindObjectOfType<PowerUpManager>();
        playerRenderer = GetComponentInChildren<Renderer>();
    }

    private void Update()
    {
        Death();
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

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            TakeDamage(1);
            return;
        }
        var powerPoint = other.gameObject.GetComponentInParent<Powerup>();
        if (powerPoint != null)
        {
            powerUpManager.IncreasePowerPoints();
            powerPoint.CleanUp();
        }
    }
}