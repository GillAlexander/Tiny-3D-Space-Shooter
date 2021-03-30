using System;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private UiManager uiManager = null;

    public event Action restartLevel;
    public event Action exitToMenu;

    private void Start()
    {
        uiManager = GetComponent<UiManager>();
    }

    public void ResumeGameplayButton()
    {
        GameManager.isPaused = !GameManager.isPaused;
        TimeManager.Timescale(1);
        uiManager.DisplayGameplay();
    }

    public void RestartLevelButton()
    {
        GameManager.isPaused = !GameManager.isPaused;
        TimeManager.Timescale(1);
        uiManager.DisplayGameplay();
        restartLevel?.Invoke();
    }

    public void ExitToMenuButton()
    {
        GameManager.isPaused = !GameManager.isPaused;
        TimeManager.Timescale(1);
        exitToMenu?.Invoke();
    }

    public void PauseButton()
    {
        //Pause the game
        GameManager.isPaused = !GameManager.isPaused;
        if (GameManager.isPaused)
        {
            TimeManager.Timescale(0);
            uiManager.DisplayPause();
        }
        else
        {
            TimeManager.Timescale(1);
            uiManager.DisplayGameplay();
        }
    }
}