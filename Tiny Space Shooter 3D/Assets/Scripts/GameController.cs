using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private UiHandler uiHandler = null;

    private void Start()
    {
        uiHandler = FindObjectOfType<UiHandler>();
    }

    void Update()
    {
        Pause();
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Pause the game
            GameManager.isPaused = !GameManager.isPaused;

            if (GameManager.isPaused)
            {
                Time.timeScale = 0;
                uiHandler.DisplayPause();
            }
            else
            {
                Time.timeScale = 1;
                uiHandler.DisplayGameplay();
            }
        }
    }

    public void PauseButton()
    {
        //Pause the game
        GameManager.isPaused = !GameManager.isPaused;

        if (GameManager.isPaused)
        {
            Time.timeScale = 0;
            uiHandler.DisplayPause();
        }
        else
        {
            Time.timeScale = 1;
            uiHandler.DisplayGameplay();
        }
    }
}