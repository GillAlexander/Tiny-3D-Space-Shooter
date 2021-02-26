using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgressState : State<ApplicationStates>
{
    private EnemySpawner enemyspawner = null;
    private Level level = null;
    private Player player = null;
    private UiHandler uiHandler = null;

    public override void OnStateEnter()
    {
        enemyspawner = GameObject.FindObjectOfType<EnemySpawner>();
        level = GameObject.FindObjectOfType<Level>();
        player = GameObject.FindObjectOfType<Player>();
        uiHandler = GameObject.FindObjectOfType<UiHandler>();

        level.ResetLevel();
        enemyspawner.ResetSpawner();
        player.Reset();
        uiHandler.ResetUi();
        context.ChangeState(ApplicationStates.SetUpLevelState);
    }

    public override void OnStateExit()
    {

    }

    public override void Tick()
    {

    }
}
