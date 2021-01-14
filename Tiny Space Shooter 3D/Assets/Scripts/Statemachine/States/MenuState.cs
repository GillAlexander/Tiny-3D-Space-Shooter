using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State<ApplicationStates>
{
    private float time = 0;

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {

    }

    public override void Tick()
    {
        time += Time.deltaTime;

        if (time >= 1f)
        {
            context.ChangeState(ApplicationStates.ResetProgressState);
        }
    }
}