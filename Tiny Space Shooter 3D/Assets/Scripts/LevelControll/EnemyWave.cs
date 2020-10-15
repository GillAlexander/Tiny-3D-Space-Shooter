using System;
using UnityEngine;

public enum MovementBehaviors
{
    ZigZag = 0
}

[CreateAssetMenu(menuName = "LevelDesign/EnemyWave")]
public class EnemyWave : ScriptableObject
{
    [SerializeField] private GameObject[] enemyPrefabs = null;
    [SerializeField] private int numberOfEnemies = 0;
    [SerializeField] private float minimumTimeBetweenSpawn = 0;
    [SerializeField] private float maximumTimeBetweenSpawn = 0;
    [SerializeField] private MovementBehaviors movementBehavior = MovementBehaviors.ZigZag;
    [SerializeField] private AnimationCurve animationCurve = null;
    [SerializeField] private Vector3[] positionsToMoveBetween;

    public int NumberOfEnemies { get => numberOfEnemies; set => numberOfEnemies = value; }
    public float MinimumTimeBetweenSpawn { get => minimumTimeBetweenSpawn; set => minimumTimeBetweenSpawn = value; }
    public float MaximumTimeBetweenSpawn { get => maximumTimeBetweenSpawn; set => maximumTimeBetweenSpawn = value; }
    public MovementBehaviors MovementBehavior { get => movementBehavior; set => movementBehavior = value; }
    public AnimationCurve AnimationCurve { get => animationCurve; set => animationCurve = value; }
    public Vector3[] PositionsToMoveBetween { get => positionsToMoveBetween; set => positionsToMoveBetween = value; }

    internal GameObject GetEnemyPrefab()
    {
        return enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
    }
}