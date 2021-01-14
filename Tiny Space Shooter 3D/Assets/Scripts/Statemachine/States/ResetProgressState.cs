using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgressState : State<ApplicationStates>
{
    public override void OnStateEnter()
    {
        Debug.Log("ResetProgressState");
    }

    public override void OnStateExit()
    {

    }

    public override void Tick()
    {

    }
}
