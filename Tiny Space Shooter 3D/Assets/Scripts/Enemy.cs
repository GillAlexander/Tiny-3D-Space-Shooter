using System.Collections;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour, DamageAbleObject
{
    [SerializeField] private int damage = 1;
    protected float healthPoints = 10;
    private ParticlePlayer particlePlayer = null;
    private AnimationCurve animationCurve = null;
    private int currentPosition = 0;
    private Vector3 nextPosition = Vector3.zero;
    private Vector3[] positionsToMoveBetween;
    private float time = 0;
    MovementBehaviors movementBehavior;

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

        switch (movementBehavior)
        {
            case MovementBehaviors.Straight:
                nextPosition = transform.position + Vector3.down * 3.5f;
                break;
            case MovementBehaviors.ZigZag:
                break;
            case MovementBehaviors.Point:
                //var distanceToNextPos = Vector3.Distance(transform.position, transform.parent.position + positionsToMoveBetween[currentPosition]);

                //if (distanceToNextPos <= 1)
                //{
                //    if (currentPosition >= positionsToMoveBetween.Length - 1) return;

                //    currentPosition++;
                //}
                break;
            default:
                break;
        }

        transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime);
    }

    public void GetMovementBehavior(MovementBehaviors behavior)
    {
        movementBehavior = behavior;
    }

    //public class MovementBehavior
    //{
    //    private MovementBehaviors movementBehavior;

    //    public MovementBehavior(MovementBehaviors behavior)
    //    {
    //        movementBehavior = behavior;
    //    }

        
    //}

    public void GetPositions(Vector3[] positions)
    {
        if (positions.Length == 0) return;

        positionsToMoveBetween = positions;
        nextPosition = positionsToMoveBetween[currentPosition];
    }

    public void Death()
    {
        Destroy(this.gameObject);
        particlePlayer.FetchAndPlayParticleAtPosition(Particles.EnemyDeath, transform.position);
    }

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

    public void AddCurve(AnimationCurve curve)
    {
        animationCurve = curve;
    }
}