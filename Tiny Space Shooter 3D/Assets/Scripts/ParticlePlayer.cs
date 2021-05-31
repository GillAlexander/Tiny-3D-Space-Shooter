using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Particles
{
    EnemyDeath,
    PlayerDeath,
    ProjectileFire,
    ProjectileHit,
    PickupPowerUp
}

public class ParticlePlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particles;

    //[SerializeField] private ParticleSystem deathParticle = null;
    //[SerializeField] private ParticleSystem projectileFireParticle = null;
    //[SerializeField] private ParticleSystem projectileHitParticle = null;
    //[SerializeField] private ParticleSystem enemyDeathParticle = null;

    private Dictionary<int, ParticleSystem> particleDictionary = new Dictionary<int, ParticleSystem>();
    private Dictionary<int, Queue<ParticleSystem>> particlesQueueDictionary = new Dictionary<int, Queue<ParticleSystem>>();

    //private Queue<ParticleSystem> deathQueue = new Queue<ParticleSystem>();
    //private Queue<ParticleSystem> projectileFireQueue = new Queue<ParticleSystem>();
    //private Queue<ParticleSystem> projectileHitQueue = new Queue<ParticleSystem>();
    //private Queue<ParticleSystem> enemyDeathQueue = new Queue<ParticleSystem>();

    private void Awake()
    {
        AddParticlesToQueue();
        //AddQueDictionary();
        //AddParticleToDictionary();
    }

    public void PlayParticle(Particles particle, Vector3 positionToPlayAt)
    {
        var particleToPlay = Instantiate(particleDictionary[(int)particle], positionToPlayAt, Quaternion.identity);
        particleToPlay.Play();
    }

    public void FetchAndPlayParticleAtPosition(Particles particle, Vector3 positionToPlayAt)
    {
        var queue = particlesQueueDictionary[(int)particle];
        if (queue.Count == 0 || queue.Peek().isPlaying)
        {
            var newParticle = Instantiate(particleDictionary[(int)particle], positionToPlayAt, Quaternion.identity);
            newParticle.Play();
            queue.Enqueue(newParticle);
        }
        else
        {
            var particleFromQueue = queue.Dequeue();
            particleFromQueue.transform.position = positionToPlayAt;
            particleFromQueue.transform.gameObject.SetActive(true);
            particleFromQueue.Play();
            queue.Enqueue(particleFromQueue);
            StartCoroutine(SetParticleInactiveWhenDonePlaying(particleFromQueue));
        }
    }

    private IEnumerator SetParticleInactiveWhenDonePlaying(ParticleSystem system)
    {
        yield return new WaitForSeconds(system.main.duration);
        system.gameObject.SetActive(false);
    }
    

    private void QueueParticles(Queue<ParticleSystem> queue, ParticleSystem particleSystem)
    {
        for (int i = 0; i < 10; i++)
        {
            var particle = Instantiate(particleSystem, transform);
            particle.transform.gameObject.SetActive(false);
            queue.Enqueue(particle);
        }
    }

    private void AddParticleToDictionary()
    {
        //particleDictionary.Add(Particles.EnemyDeath, enemyDeathParticle);
        //particleDictionary.Add(Particles.PlayerDeath, deathParticle);
        //particleDictionary.Add(Particles.ProjectileFire, projectileFireParticle);
        //particleDictionary.Add(Particles.ProjectileHit, projectileHitParticle);
    }

    private void AddParticlesToQueue()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            var queue = new Queue<ParticleSystem>();
            QueueParticles(queue, particles[i]);
            particlesQueueDictionary.Add(i, queue);
            particleDictionary.Add(i, particles[i]);
        }

        //QueueParticles(deathQueue, deathParticle);
        //QueueParticles(projectileFireQueue, projectileFireParticle);
        //QueueParticles(projectileHitQueue, projectileHitParticle);
        //QueueParticles(enemyDeathQueue, enemyDeathParticle);
    }

    private void AddQueDictionary()
    {
        //for (int i = 0; i < particles.Length; i++)
        //{
        //    particlesQueueDictionary.Add(i, )
        //}

        //particlesQueueDictionary.Add(Particles.EnemyDeath, enemyDeathQueue);
        //particlesQueueDictionary.Add(Particles.PlayerDeath, deathQueue);
        //particlesQueueDictionary.Add(Particles.ProjectileFire, projectileFireQueue);
        //particlesQueueDictionary.Add(Particles.ProjectileHit, projectileHitQueue);
    }
}

//public void PlayDeathParticles(Vector3 positionToPlayAt)
//{
//    var particleToPlay = Instantiate(deathParticle, positionToPlayAt, Quaternion.identity);
//    particleToPlay.Play();
//}

//public void ProjectileFireParticles(Vector3 positionToPlayAt)
//{
//    var particleToPlay = Instantiate(projectileFireParticle, positionToPlayAt, Quaternion.identity);
//    particleToPlay.Play();
//}

//public void ProjectileHitParticle(Vector3 positionToPlayAt)
//{
//    var particleToPlay = Instantiate(projectileHitParticle, positionToPlayAt, Quaternion.identity);
//    particleToPlay.Play();
//}

//public void EnemyDeathParticle(Vector3 positionToPlayAt)
//{
//    var particleToPlay = Instantiate(enemyDeathParticle, positionToPlayAt, Quaternion.identity);
//    particleToPlay.Play();
//}