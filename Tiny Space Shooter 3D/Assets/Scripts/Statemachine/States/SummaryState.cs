using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryState : State<ApplicationStates>
{
    private UiHandler uiHandler = null;
    private InputManager inputs = null;

    public override void OnStateEnter()
    {
        uiHandler = GameObject.FindObjectOfType<UiHandler>();
        inputs = GameObject.FindObjectOfType<InputManager>();

        uiHandler.DisplaySummary();
    }

    public override void OnStateExit()
    {

    }

    public override void Tick()
    {
        if (inputs.LeftMousePressed)
        {
            context.ChangeState(ApplicationStates.Menu);
        }
    }
}