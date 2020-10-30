using System;
using UnityEngine;

public enum MovementBehaviors
{
    Straight = 0,
    ZigZag = 1,
    Point = 2
}

public enum SpawnBehaviors
{
    Randomize = 0,
    StepThrough = 1
}

[CreateAssetMenu(menuName = "LevelDesign/EnemyWave")]
public class EnemyWave : ScriptableObject
{
    [SerializeField] private GameObject[] enemyPrefabs = null;
    [SerializeField] private int numberOfEnemies = 0;
    [SerializeField] private float timeBetweenSpawns = 0;
    [SerializeField] private MovementBehaviors movementBehavior = MovementBehaviors.ZigZag;
    //[SerializeField] private AnimationCurve animationCurve = null;
    [SerializeField] private SpawnBehaviors spawnBehavior;
    [SerializeField] private Vector3[] spawnPositions;
    [SerializeField] private Vector3[] positionToMoveTo;

    [Header("Section Passed Percentage")]
    [SerializeField] private float waveSpawnThreshHold = 5f;
    public int NumberOfEnemies { get => numberOfEnemies; }
    public float TimeBetweenSpawns { get => timeBetweenSpawns; }
    public MovementBehaviors MovementBehavior { get => movementBehavior; }
    //public AnimationCurve AnimationCurve { get => animationCurve; }
    public Vector3[] SpawnPositions { get => spawnPositions; }

    internal GameObject GetEnemyPrefab()
    {
        return enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
    }
    public float WaveSpawnThreshHold { get => waveSpawnThreshHold; }
    public SpawnBehaviors SpawnBehavior { get => spawnBehavior; }
    public Vector3[] PositionToMoveTo { get => positionToMoveTo; }
}