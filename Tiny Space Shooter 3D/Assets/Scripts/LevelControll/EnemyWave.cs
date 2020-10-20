using System;
using UnityEngine;

public enum MovementBehaviors
{
    Straight = 0,
    ZigZag = 1,
    Point = 2
}

[CreateAssetMenu(menuName = "LevelDesign/EnemyWave")]
public class EnemyWave : ScriptableObject
{
    [SerializeField] private GameObject[] enemyPrefabs = null;
    [SerializeField] private int numberOfEnemies = 0;
    [SerializeField] private float timeBetweenSpawns = 0;
    [SerializeField] private MovementBehaviors movementBehavior = MovementBehaviors.ZigZag;
    //[SerializeField] private AnimationCurve animationCurve = null;
    [SerializeField] private Vector3[] positionsToMoveBetween;

    [Header("Section Passed Percentage")]
    [SerializeField] private float waveSpawnThreshHold = 5f;

    public int NumberOfEnemies { get => numberOfEnemies; }
    public float TimeBetweenSpawns { get => timeBetweenSpawns; }
    public MovementBehaviors MovementBehavior { get => movementBehavior; }
    //public AnimationCurve AnimationCurve { get => animationCurve; }
    public Vector3[] PositionsToMoveBetween { get => positionsToMoveBetween; }

    internal GameObject GetEnemyPrefab()
    {
        return enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
    }
    public float WaveSpawnThreshHold { get => waveSpawnThreshHold; }
}