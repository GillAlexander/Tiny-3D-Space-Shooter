﻿using System;
using UnityEngine;

enum ShootingBehavior
{
    Down = 0,
    RightAndLeft = 1,
    TrackingPlayer = 2
}

public class Enemy : MonoBehaviour, IDamageAbleObject
{
    [SerializeField] private int damage = 1;
    [SerializeField] private bool canFire = false;
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private ShootingBehavior ShootingBehavior = ShootingBehavior.Down;

    [SerializeField] private float healthPoints = 10;
    [SerializeField] private GameObject powerPointPrefab = null;
    private GameObject powerPoint = null;

    private ParticlePlayer particlePlayer = null;
    private AnimationCurve animationCurve = null;
    private int currentPosition = 0;
    private Vector3 nextPosition = Vector3.zero;
    private Vector3[] positionsToMoveBetween;
    private float time = 0;
    private float zigzagTime = 0;

    MovementBehaviors movementBehavior;
    private Player player = null;

    [SerializeField] private float rateOfFire = 0.5f;
    private bool zigRight = false;

    public AudioSource deathSound = null;

    public event Action killedByPlayer;

    private void Awake()
    {
        particlePlayer = FindObjectOfType<ParticlePlayer>();
    }

    private void Update()
    {
        bool dead = healthPoints <= 0;

        if (canFire)
        {
            time += Time.deltaTime;
            var canFireProjectile = Vector3.Distance(transform.position, player.transform.position) <= 15;
            if (time >= rateOfFire)
            {
                FireProjectile();
                time = 0;
            }
        }

        if (dead)
        {
            Death();
            killedByPlayer?.Invoke();
        }


        //var downDirection = transform.position + Vector3.down * 6.5f;
        var downDirection = Vector3.down;

        switch (movementBehavior)
        {
            case MovementBehaviors.Straight:
                nextPosition = downDirection;
                break;

            case MovementBehaviors.ZigZag:
                zigzagTime += Time.deltaTime;
                //if (zigRight)
                //{
                //    nextPosition = downDirection + Vector3.right * 4;
                //}
                //else
                //    nextPosition = downDirection + Vector3.left * 4;
                if (zigRight)
                {
                    nextPosition = downDirection + Vector3.right;
                }
                else
                    nextPosition = downDirection + Vector3.left;

                if (zigzagTime >= 1)
                {
                    zigzagTime = 0;
                    zigRight = !zigRight;
                }
                break;

            case MovementBehaviors.Point:
                var distanceToNextPos = Vector3.Distance(transform.position, transform.parent.position + positionsToMoveBetween[currentPosition]);

                if (distanceToNextPos <= 1)
                {
                    if (currentPosition >= positionsToMoveBetween.Length - 1) return;
                    currentPosition++;
                }
                nextPosition =    positionsToMoveBetween[currentPosition] - transform.position + transform.parent.position;
                nextPosition = Vector3.Normalize(nextPosition);
                break;
            default:
                break;
        }

        //transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime);
        transform.Translate(nextPosition * Time.deltaTime * 5);
    }

    public void EnablePowerPointDrop()
    {
        powerPoint = Instantiate(powerPointPrefab, transform.position, transform.rotation, transform);
        powerPoint.SetActive(false);
    }

    public void GetMovementBehavior(MovementBehaviors behavior)
    {
        movementBehavior = behavior;
    }

    public void GetShootingBehavior()
    {

    }

    //public class MovementBehavior
    //{
    //    private MovementBehaviors movementBehavior;

    //    public MovementBehavior(MovementBehaviors behavior)
    //    {
    //        movementBehavior = behavior;
    //    }
    //}

    public void GetMovementPositions(Vector3[] positions)
    {
        if (positions.Length == 0) return;

        positionsToMoveBetween = positions;
    }

    public void GetPlayer(Player player)
    {
        this.player = player;
    }

    public void Death()
    {
        deathSound.transform.parent = null;
        deathSound.gameObject.AddComponent<DestroyAfterTime>();
        deathSound.Play();
        if (powerPoint != null)
        {
            powerPoint.SetActive(true);
            powerPoint.transform.parent = null;
        }
        Destroy(this.gameObject);
        particlePlayer.FetchAndPlayParticleAtPosition(Particles.EnemyDeath, transform.position);
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
    }

    public void AddCurve(AnimationCurve curve)
    {
        animationCurve = curve;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    var enemy = other.gameObject.GetComponentInParent<Player>();
    //    Debug.Log(enemy);
    //    if (enemy != null)
    //    {
    //        enemy.TakeDamage(1);
    //    }
    //}

    private void FireProjectile()
    {
        switch (ShootingBehavior)
        {
            case ShootingBehavior.Down:
                var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.GetComponent<Rigidbody>().velocity = Vector3.down * 5;
                projectile.GetComponent<Projectile>().SetDamage(1);
                break;
            case ShootingBehavior.RightAndLeft:
                for (int i = 0; i < 2; i++)
                {
                    //var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                    //if (i == 0)
                    //{
                    //    projectile.GetComponent<Rigidbody>().velocity = player.transform.position - transform.position;
                    //}
                    //else
                    //    projectile.GetComponent<Rigidbody>().velocity = player.transform.position - transform.position;
                    
                    //projectile.GetComponent<Projectile>().SetDamage(1);
                }
                break;
            case ShootingBehavior.TrackingPlayer:
                break;
            default:
                break;
        }
    }
}