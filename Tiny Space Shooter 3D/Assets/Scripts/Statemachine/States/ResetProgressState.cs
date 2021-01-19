using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgressState : State<ApplicationStates>
{
    private EnemySpawner enemyspawner = null;
    private Level level = null;

    public override void OnStateEnter()
    {
        enemyspawner = GameObject.FindObjectOfType<EnemySpawner>();
        level = GameObject.FindObjectOfType<Level>();
        level.ResetLevel();
        enemyspawner.ResetSpawner();
        context.ChangeState(ApplicationStates.SetUpLevelState);
    }

    public override void OnStateExit()
    {

    }

    public override void Tick()
    {

    }
}
