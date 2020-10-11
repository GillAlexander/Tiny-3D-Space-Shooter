using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //[SerielizeField] private Waves enemyWaves = null;
    [SerializeField] private EnemyWave[] enemyWaves = null;
    [SerializeField] private Vector3[] spawnPositions = null;

    private float limitationWidth = 20f;

    private void Start()
    {
        Spawn(0);
    }

    private void Spawn(int wave)
    {
        //EnemyWave currentWave = enemyWaves.GetWave(wave);
        StartCoroutine(SpawnWave(wave, enemyWaves[wave]));
    }

    private IEnumerator SpawnWave(int wave, EnemyWave currentWave)
    {
        for (int i = 0; i < currentWave.NumberOfEnemies; i++)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(currentWave.MinimumTimeBetweenSpawn, currentWave.MaximumTimeBetweenSpawn));
            var enemy = Instantiate(currentWave.GetEnemyPrefab(), GetSpawnPosition(), Quaternion.identity);
            enemy.GetComponent<Enemy>().AddCurve(currentWave.AnimationCurve);
            //float t = currentWave.AnimationCurve.Evaluate(Time.deltaTime);
            //enemy.GetComponent<Rigidbody>().velocity = new Vector3(t, -1, 0) * 4;
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
