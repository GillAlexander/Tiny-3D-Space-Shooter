using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Vector3[] spawnPositions = null;
    private int currentSpawnNumber;

    public void SpawnEnemyWave(LevelSectionInformation levelSectionInformation, EnemyWave wave)
    {
        StartCoroutine(SpawnWave(wave));
    }

    private IEnumerator SpawnWave(EnemyWave currentWave)
    {
        for (int i = 0; i < currentWave.NumberOfEnemies; i++)
        {
            yield return new WaitForSeconds(currentWave.TimeBetweenSpawns);
            var enemyObject = Instantiate(currentWave.GetEnemyPrefab(), GetSpawnPosition(), Quaternion.identity);
            enemyObject.transform.parent = this.transform;
            var enemy = enemyObject.GetComponent<Enemy>();
            enemy.GetPositions(currentWave.SpawnPositions);
        }
        currentSpawnNumber = 0;
    }

    private Vector3 GetSpawnPosition()
    {
        if (spawnPositions.Length == 0) return Vector3.zero;

        var spawnPosition = spawnPositions[currentSpawnNumber] + transform.position;
        currentSpawnNumber++;

        if (currentSpawnNumber >= spawnPositions.Length) // Reset spawn number
        {
            currentSpawnNumber = 0;
        }
        return spawnPosition;
    }

    public void AddSpawnPositions(Vector3[] positions)
    {
        spawnPositions = positions;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Vector3 position in spawnPositions)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + position, 0.5f);
        }
    }
}
