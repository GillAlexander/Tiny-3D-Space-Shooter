using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : State<ApplicationStates>
{
    private PlayerController playerController = null;
    private EnemySpawner enemySpawner = null;
    private Level level = null;

    public override void OnStateEnter()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        level = GameObject.FindObjectOfType<Level>();

        playerController.EnablePlayerControll();
    }

    public override void OnStateExit()
    {
        playerController.DisablePlayerControll();
    }

    public override void Tick()
    {
        if (level.SpawnedAllTheWaves() && enemySpawner.AllEnemiesDefeated())
        {
            level.CleanUpLevelObjects();
            context.ChangeState(ApplicationStates.SummaryState);
        }
    }
}