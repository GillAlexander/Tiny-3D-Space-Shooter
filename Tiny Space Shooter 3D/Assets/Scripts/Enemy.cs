using UnityEngine;

public class Enemy : MonoBehaviour, DamageAbleObject
{
    [SerializeField] private int damage = 1;
    protected float healthPoints = 10;
    private ParticlePlayer particlePlayer = null;
    private AnimationCurve animationCurve = null;
    private int currentPosition = 0;
    private Vector3 nextPosition;
    private Vector3[] positionsToMoveBetween;
    private float time = 0;

    private void Awake()
    {
        particlePlayer = FindObjectOfType<ParticlePlayer>();
        nextPosition = Vector3.zero;
    }

    private void Update()
    {
        if (healthPoints <= 0)
        {
            Death();
        }

        //if (animationCurve != null) 
        //{
        //time += Time.deltaTime;
        //float evalue = animationCurve.Evaluate(time);
            if (transform.position == positionsToMoveBetween[currentPosition])
            {
                currentPosition++;
                nextPosition = positionsToMoveBetween[currentPosition];
            }
            transform.Translate(nextPosition * Time.deltaTime * 2);
            //transform.Translate(new Vector3(evalue, -1, 0) * Time.deltaTime * 4);
        //}
    }

    public void GetPositions(Vector3[] positions)
    {
        positionsToMoveBetween = positions;
        nextPosition = positionsToMoveBetween[currentPosition];
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