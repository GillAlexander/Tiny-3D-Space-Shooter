using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringMechanics : MonoBehaviour, IReset
{
    [SerializeField] private float fireCooldown = 0.25f;
    [SerializeField] private GameObject bullet1 = null;
    //[SerializeField] private Transform[] firingPositions = null;
    [SerializeField] private FiringPositions[] firingPositions = null;
    private ParticlePlayer particlePlayer = null;
    private float timer = 0;
    private int alternateFiringPos = 0;
    private PowerUpManager powerUpManager = null;

    private void Update()
    {
        timer += Time.deltaTime;
        particlePlayer = FindObjectOfType<ParticlePlayer>();
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }

    public void ResetValues()
    {
        timer = 0;
        alternateFiringPos = 0;
    }

    public void Shoot(bool playerControll)
    {
        if (!playerControll) return;
        if (timer >= fireCooldown)
        {
            for (int i = 0; i < firingPositions[powerUpManager.GetUpgradedLevel((int)PowerUps.NumberOfCanons)].firingPos.Length; i++)
            {
                //Optimera så bullets tas ifrån en pool
                var newFirePos = GetNewFiringPosition();
                particlePlayer?.FetchAndPlayParticleAtPosition(Particles.ProjectileFire, newFirePos + Vector3.up / 2);
                var bullet = Instantiate(bullet1, new Vector3(newFirePos.x, newFirePos.y, 0), Quaternion.identity);
                var projectile = bullet.GetComponent<Projectile>();
                projectile.SetDamage(1);

                var numberOfFiringPos = firingPositions[FindObjectOfType<PowerUpManager>().GetUpgradedLevel((int)PowerUps.NumberOfCanons)].firingPos.Length;
                if (alternateFiringPos < numberOfFiringPos - 1)
                    alternateFiringPos++;
                else
                    alternateFiringPos = 0;
            }

            //laserSound.Play();
            timer = 0;
        }
    }

    private Vector3 GetNewFiringPosition() // Optimera om så det inte behöver göra en NewPos vid varje skott
    {
        var firePos = firingPositions[powerUpManager.GetUpgradedLevel((int)PowerUps.NumberOfCanons)].firingPos[alternateFiringPos].position;

        return firePos;
    }

    [Serializable]
    public class FiringPositions
    {
        public Transform[] firingPos;
    }
}
