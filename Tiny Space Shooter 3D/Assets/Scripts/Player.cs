using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, DamageAbleObject
{
    private float damage = 20f;
    private float powerupTime = 5f;
    private float healthPoints = 10;

    [SerializeField] private GameObject bullet1 = null;
    [SerializeField] private Transform[] firingPositions = null;

    private Renderer playerRenderer = null;
    public int numberOfVisualDamageLoops;
    public float damageWaitTime;
    private Coroutine damageIenumerator;

    public float HealthPoints { get => healthPoints; set => healthPoints = value; }

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
            FindObjectOfType<ParticlePlayer>().FetchAndPlayParticleAtPosition(Particles.ProjectileFire, firingPositions[i].position + Vector3.up / 2);
            var bullet = Instantiate(bullet1, firingPositions[i].position, Quaternion.identity);
            bullet.GetComponent<Projectile>().SetDamage(5);
            //switch (GameManager.GAMESPEED) // Gör om gör rätt 
            //{
            //    case 1:
            //        bullet.GetComponent<Rigidbody>().velocity = Vector3.up * 10;
            //        break;
            //    case 2:
            //        bullet.GetComponent<Rigidbody>().velocity = Vector3.up * 15;
            //        break;
            //    case 4:
            //        bullet.GetComponent<Rigidbody>().velocity = Vector3.up * 20;
            //        break;
            //    case 12:
            //        bullet.GetComponent<Rigidbody>().velocity = Vector3.up * 25;
            //        break;
            //    default:
            //        break;
            //}
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

    private void InvincibilityMode(bool isInvincibal)
    {
        GetComponentInChildren<Collider>().enabled = !isInvincibal;
    }
}