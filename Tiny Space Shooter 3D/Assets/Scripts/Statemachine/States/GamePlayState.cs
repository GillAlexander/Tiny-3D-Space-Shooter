using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : State<ApplicationStates>
{
    private PlayerController playerController = null;
    private EnemySpawner enemySpawner = null;
    private Level level = null;
    private ButtonManager buttonManager= null;
    public override void OnStateEnter()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        level = GameObject.FindObjectOfType<Level>();
        buttonManager = GameObject.FindObjectOfType<ButtonManager>();
        playerController.EnablePlayerControll();
        buttonManager.exitToMenu += ExitToMenu;
        buttonManager.restartLevel += RestartGameplay;
    }

    public override void OnStateExit()
    {
        buttonManager.exitToMenu -= ExitToMenu;
        buttonManager.restartLevel -= RestartGameplay;
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

    private void ExitToMenu()
    {
        context.ChangeState(ApplicationStates.CleanupState);
    }

    private void RestartGameplay()
    {
        GameObject.FindObjectOfType<CleanupManager>().Cleanup(); // Gör snyggare lösning
        level.ResetValues();
        context.ChangeState(ApplicationStates.ResetProgressState);
    }
}