using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : State<ApplicationStates>
{
    private PlayerController playerController = null;
    private EnemySpawner enemySpawner = null;
    private Level level = null;
    private UiHandler uiHandler = null;
    public override void OnStateEnter()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        level = GameObject.FindObjectOfType<Level>();
        uiHandler = GameObject.FindObjectOfType<UiHandler>();
        playerController.EnablePlayerControll();
        uiHandler.exitToMenu += ExitToMenu;
    }

    public override void OnStateExit()
    {
        uiHandler.exitToMenu -= ExitToMenu;
        playerController.DisablePlayerControll();
    }

    public override void Tick()
    {
        if (level.SpawnedAllTheWaves() && enemySpawner.AllEnemiesDefeated())
        {
            level.CleanUpLevelObjects();
            context.ChangeState(ApplicationStates.SummaryState);
        }
        if (GameManager.isPaused)
        {

        }
    }

    private void ExitToMenu()
    {
        context.ChangeState(ApplicationStates.Menu);
    }
}