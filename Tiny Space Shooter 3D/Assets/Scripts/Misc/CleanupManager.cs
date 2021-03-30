using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanupManager : MonoBehaviour
{
    private Level level = null;
    private EnemySpawner enemySpawner = null;

    void Start()
    {
        level = FindObjectOfType<Level>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    public void Cleanup()
    {
        level.CleanUpLevelObjects();
        enemySpawner.CleanupEnemies();
    }
}
