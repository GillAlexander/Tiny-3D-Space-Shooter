using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupLevelState : State<ApplicationStates>
{
    private Level level = null;
    private LevelSectionInformation[] levels = null;

    public override void OnStateEnter()
    {
        level = GameObject.FindObjectOfType<Level>();
        levels = Resources.LoadAll<LevelSectionInformation>("Level1");
        level.FetchLevelInfo(levels);
    }

    public override void OnStateExit()
    {

    }

    public override void Tick()
    {

    }
}
