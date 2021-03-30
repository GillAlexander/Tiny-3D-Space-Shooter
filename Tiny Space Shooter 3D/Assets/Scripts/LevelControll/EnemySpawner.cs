using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IReset
{
    private int currentSpawnNumber;
    private Player player = null;
    public bool waveCompleted = false;

    private List<Coroutine> coroutines = new List<Coroutine>();
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private int numberOfTotalEnemies = 0;

    public void CleanupEnemies()
    {
        StopAllCoroutines();
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i] != null)
            {
                Destroy(spawnedEnemies[i]);
            }
        }
    }

    private int numberOfEnemesSpawned = 0;

    public int NumberOfTotalEnemies { get => numberOfTotalEnemies; set => numberOfTotalEnemies = value; }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public bool AllEnemiesDefeated() => spawnedEnemies.Count == 0 && waveCompleted && NumberOfTotalEnemies == numberOfEnemesSpawned == true ? true : false;

    public void SpawnEnemyWave(EnemyWave wave, Vector3[] spawnPos, SpawnBehaviors spawnbehavior, MovementBehaviors movementBehavior)
    {
        waveCompleted = false;
         StartCoroutine(SpawnWave(wave, spawnPos, spawnbehavior, movementBehavior));

        NumberOfTotalEnemies += wave.NumberOfEnemies;
    }

    public void ResetValues()
    {
        StopAllCoroutines();
        currentSpawnNumber = 0;
        waveCompleted = false;
        numberOfTotalEnemies = 0;
        numberOfEnemesSpawned = 0;
    }

    private IEnumerator SpawnWave(EnemyWave currentWave, Vector3[] spawnPos, SpawnBehaviors SBehavior, MovementBehaviors MBehavior)
    {
        for (int i = 0; i < currentWave.NumberOfEnemies; i++)
        {
            var enemyObject = Instantiate(currentWave.GetEnemyPrefab(), GetSpawnPosition(SBehavior, spawnPos), Quaternion.identity);
            enemyObject.transform.parent = this.transform;
            var enemy = enemyObject.GetComponent<Enemy>();
            spawnedEnemies.Add(enemyObject);
            numberOfEnemesSpawned++;
            enemy.GetMovementBehavior(MBehavior);
            enemy.GetMovementPositions(currentWave.PositionToMoveTo);
            enemy.GetPlayer(player);
            enemy.killedByPlayer += player.AddKillCount;

            if (currentWave.SpawnPowerPoint)
            {
                if (i == currentWave.NumberOfEnemies - 1)
                {
                    enemy.EnablePowerPointDrop();
                }
            }
            yield return new WaitForSeconds(currentWave.TimeBetweenSpawns);
        }
        //currentSpawnNumber = 0; // Behöver fixas, currentspawnnumber behöver bli 0 när waven är slut men skall inte påverka nästa wave som spawnar
        waveCompleted = true;
    }

    private Vector3 GetSpawnPosition(SpawnBehaviors behavior, Vector3[] spawnPos)
    {
        if (spawnPos.Length == 0) return Vector3.zero;
        var spawnPosition = Vector3.zero; 

        switch (behavior)
        {
            case SpawnBehaviors.Randomize:
                spawnPosition = spawnPos[UnityEngine.Random.Range(0, spawnPos.Length)] + transform.position;
                break;
            case SpawnBehaviors.StepThrough:
                if (currentSpawnNumber >= spawnPos.Length) // Reset spawn number
                {
                    currentSpawnNumber = 0;
                }
                //Debug.Log($"CurrentSpawnNumber: {currentSpawnNumber}");
                spawnPosition = spawnPos[currentSpawnNumber] + transform.position;
                currentSpawnNumber++;
                break;
        }
        
        return spawnPosition;
    }

    private void LateUpdate()
    {
        CheckListForNullObjects();
    }

    private void CheckListForNullObjects()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i] == null)
            {
                spawnedEnemies.RemoveAt(i);
            }
        }
    }
}