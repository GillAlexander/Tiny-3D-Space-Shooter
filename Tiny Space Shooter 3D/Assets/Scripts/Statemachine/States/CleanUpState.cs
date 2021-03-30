using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUpState : State<ApplicationStates>
{
    private CleanupManager cleanupManager = null;

    public override void OnStateEnter()
    {
        cleanupManager = GameObject.FindObjectOfType<CleanupManager>();

        cleanupManager.Cleanup();

        context.ChangeState(ApplicationStates.Menu);
    }

    public override void OnStateExit()
    {

    }

    public override void Tick()
    {

    }
}
