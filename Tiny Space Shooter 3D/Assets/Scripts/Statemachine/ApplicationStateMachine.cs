using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ApplicationStates
{
    Menu = 0,
    ResetProgressState = 1,
    SetUpLevelState = 2,
    GamePlayState = 3,
    PauseState = 4
}

public class ApplicationStateMachine : Context<ApplicationStates>
{

    private void Awake()
    {
        states[ApplicationStates.Menu] = new MenuState();
        states[ApplicationStates.ResetProgressState] = new ResetProgressState();
        states[ApplicationStates.SetUpLevelState] = new SetupLevelState();
        states[ApplicationStates.GamePlayState] = new GamePlayState();
        states[ApplicationStates.PauseState] = new PauseState();
    }

    private void OnEnable()
    {
        ChangeState(ApplicationStates.Menu);
    }

    protected override void Start()
    {
        ChangeState(ApplicationStates.Menu);
    }
}
