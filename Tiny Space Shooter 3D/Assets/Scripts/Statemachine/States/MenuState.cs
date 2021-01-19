using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuState : State<ApplicationStates>
{
    private UiHandler uiHandler = null;
    private PlayerController playerController = null;
    
    public override void OnStateEnter()
    {
        uiHandler = GameObject.FindObjectOfType<UiHandler>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        uiHandler.levelSelectButton.onClick.AddListener(OpenLevelSelect);
        playerController.DisablePlayerControll();

        uiHandler.selectLevel += ResetProgress;
    }

    public override void OnStateExit()
    {
        uiHandler.levelSelectButton.onClick.RemoveListener(OpenLevelSelect);

        uiHandler.selectLevel -= ResetProgress;
    }

    public override void Tick()
    {

    }

    private void ResetProgress()
    {
        context.ChangeState(ApplicationStates.ResetProgressState);
        
    }

    public void OpenLevelSelect()
    {

    }
}