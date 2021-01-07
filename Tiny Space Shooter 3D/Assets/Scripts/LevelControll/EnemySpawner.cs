using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int currentSpawnNumber;
    private Player player = null;
    public bool waveCompleted = false;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void SpawnEnemyWave(EnemyWave wave, Vector3[] spawnPos, SpawnBehaviors spawnbehavior, MovementBehaviors movementBehavior)
    {
        waveCompleted = false;
        StartCoroutine(SpawnWave(wave, spawnPos, spawnbehavior, movementBehavior));
    }

    private IEnumerator SpawnWave(EnemyWave currentWave, Vector3[] spawnPos, SpawnBehaviors SBehavior, MovementBehaviors MBehavior)
    {
        for (int i = 0; i < currentWave.NumberOfEnemies; i++)
        {
            var enemyObject = Instantiate(currentWave.GetEnemyPrefab(), GetSpawnPosition(SBehavior, spawnPos), Quaternion.identity);
            enemyObject.transform.parent = this.transform;
            var enemy = enemyObject.GetComponent<Enemy>();
            enemy.GetMovementBehavior(MBehavior);
            enemy.GetMovementPositions(currentWave.PositionToMoveTo);
            enemy.GetPlayer(player);
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
                Debug.Log(currentSpawnNumber);
                spawnPosition = spawnPos[currentSpawnNumber] + transform.position;
                currentSpawnNumber++;
                break;
        }
        
        return spawnPosition;
    }

    private void OnDrawGizmosSelected()
    {
        //if (spawnPositions == null) return;

        //foreach (Vector3 position in spawnPositions)
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawSphere(transform.position + position, 0.5f);
        //}
    }
}
