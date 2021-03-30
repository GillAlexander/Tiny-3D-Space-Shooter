using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuState : State<ApplicationStates>
{
    private UiManager uiHandler = null;
    private PlayerController playerController = null;
    private Level level = null;

    public override void OnStateEnter()
    {
        uiHandler = GameObject.FindObjectOfType<UiManager>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        level = GameObject.FindObjectOfType<Level>();

        level.ApplyMenuImage();
        playerController.DisablePlayerControll();
        uiHandler.DisplayMainMenu();
        uiHandler.selectLevel += ResetProgress;
    }

    public override void OnStateExit()
    {
        uiHandler.selectLevel -= ResetProgress;
    }

    public override void Tick()
    {

    }

    private void ResetProgress(int levelSelected)
    {
        level.NewLevelToLoad(levelSelected);
        uiHandler.DisplayGameplay();
        context.ChangeState(ApplicationStates.ResetProgressState);
    }

    public void OpenLevelSelect()
    {

    }
}