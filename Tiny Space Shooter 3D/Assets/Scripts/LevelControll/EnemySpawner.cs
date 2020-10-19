using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector3[] spawnPositions = null;

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
            enemy.GetPositions(currentWave.PositionsToMoveBetween);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        return spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)] + transform.position;
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
