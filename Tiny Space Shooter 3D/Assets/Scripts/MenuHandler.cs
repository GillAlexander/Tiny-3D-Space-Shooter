using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    private UiHandler uiHandler = null;
    private EnemySpawner enemySpawner = null;
    private Level level = null;

    private void Start()
    {
        uiHandler.GetComponent<UiHandler>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        level = FindObjectOfType<Level>();
    }

    public void MainMenu()
    {

    }

    public void MapSelection()
    {

    }

    public void GamePlay(int levelToPlay)
    {
        uiHandler.GamePlay(levelToPlay);
        enemySpawner.ResetSpawner();
        level.ResetLevel();
    }

    public void PauseMenu()
    {

    }
}