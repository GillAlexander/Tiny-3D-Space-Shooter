﻿using System;
using System.Runtime.Remoting.Messaging;
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

    [Header("Spawn Behavior")]
    [SerializeField] private float timeBetweenSpawns = 0;
    [SerializeField] private SpawnBehaviors spawnBehavior;
    [SerializeField] private Vector3[] spawnPositions;

    [Header("Movement Behavior")]
    [SerializeField] private MovementBehaviors movementBehavior;
    [SerializeField] private Vector3[] positionToMoveTo;

    [Header("Section Passed Percentage")]
    [SerializeField] private float waveSpawnThreshHold = 5f;
    public int NumberOfEnemies { get => numberOfEnemies; }
    public float TimeBetweenSpawns { get => timeBetweenSpawns; }
    public Vector3[] SpawnPositions { get => spawnPositions; }

    internal GameObject GetEnemyPrefab()
    {
        return enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
    }
    public float WaveSpawnThreshHold { get => waveSpawnThreshHold; }
    public SpawnBehaviors SpawnBehavior { get => spawnBehavior; }
    public MovementBehaviors MovementBehavior { get => movementBehavior; }
    public Vector3[] PositionToMoveTo { get => positionToMoveTo; }
}