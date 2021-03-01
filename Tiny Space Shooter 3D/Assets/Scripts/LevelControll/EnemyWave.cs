using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

//public enum WaveType
//{
//    Minionwave = 0,
//    Bosswave = 1
//}

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
    //[SerializeField] private WaveType waveType = WaveType.Minionwave;
    [SerializeField] private GameObject[] enemyPrefabs = null;
    [SerializeField] private int numberOfEnemies = 0;
    
    [Header("Spawn Behavior")]
    [SerializeField] private float timeBetweenSpawns = 0;
    [SerializeField] private SpawnBehaviors spawnBehavior = default;
    [SerializeField] private Vector3[] spawnPositions = default;

    [Header("Movement Behavior")]
    [SerializeField] private MovementBehaviors movementBehavior = default;
    [SerializeField] private Vector3[] positionToMoveTo = default;

    [Header("Wave settings")]
    [Tooltip("How long before next wave of enemies to spawn")]
    [SerializeField] private float waveTimeBeforeSpawn = 2;

    [Header("PowerPoint")]
    [SerializeField] private bool spawnPowerPoint = false;

    //[Header("Section Passed Percentage")]
    //[SerializeField] private float waveSpawnThreshHold = 5f;
    public int NumberOfEnemies { get => numberOfEnemies; }
    public float TimeBetweenSpawns { get => timeBetweenSpawns; }
    public Vector3[] SpawnPositions { get => spawnPositions; }

    internal GameObject GetEnemyPrefab()
    {
        return enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
    }
    //public float WaveSpawnThreshHold { get => waveSpawnThreshHold; }
    public SpawnBehaviors SpawnBehavior { get => spawnBehavior; }
    public MovementBehaviors MovementBehavior { get => movementBehavior; }
    public Vector3[] PositionToMoveTo { get => positionToMoveTo; }
    public float WaveTimeBeforeSpawn { get => waveTimeBeforeSpawn; }
    public bool SpawnPowerPoint { get => spawnPowerPoint; }
    //public WaveType WaveType { get => waveType; }
}