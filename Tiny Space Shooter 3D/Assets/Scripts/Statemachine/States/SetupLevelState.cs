﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupLevelState : State<ApplicationStates>
{
    private Level level = null;
    private LevelSectionInformation[] levels = null;

    public override void OnStateEnter()
    {
        level = GameObject.FindObjectOfType<Level>();
        var levelToLoad = level.LevelToLoad;
        levels = Resources.LoadAll<LevelSectionInformation>($"Level{levelToLoad}");
        
        if (levels.Length == 0) Debug.LogError("Missing level to load in the Resource Folder");

        level.FetchLevelInfo(levels);
        levels = null; // Kanske göra om så att leves som redan är laddade blir bara överskrivna ifall en ny 
        //bana blir vald men startar man om samma bana så behålls sectioninfon kvar
        context.ChangeState(ApplicationStates.GamePlayState);
    }

    public override void OnStateExit()
    {

    }

    public override void Tick()
    {

    }
}
