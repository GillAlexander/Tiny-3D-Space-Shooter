using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgressState : State<ApplicationStates>
{
    private EnemySpawner enemyspawner = null;
    private Level level = null;
    private Player player = null;
    private UiManager uiHandler = null;
    private FiringMechanics firingMechanics = null;
    private PowerUpManager powerUpManager = null;

    public override void OnStateEnter()
    {
        enemyspawner = GameObject.FindObjectOfType<EnemySpawner>();
        level = GameObject.FindObjectOfType<Level>();
        player = GameObject.FindObjectOfType<Player>();
        uiHandler = GameObject.FindObjectOfType<UiManager>();
        firingMechanics = GameObject.FindObjectOfType<FiringMechanics>();
        powerUpManager = GameObject.FindObjectOfType<PowerUpManager>();

        level.ResetValues();
        enemyspawner.ResetValues();
        player.ResetValues();
        uiHandler.ResetValues();
        firingMechanics.ResetValues();
        powerUpManager.ResetValues();
        context.ChangeState(ApplicationStates.SetUpLevelState);
    }

    public override void OnStateExit()
    {

    }

    public override void Tick()
    {

    }
}
