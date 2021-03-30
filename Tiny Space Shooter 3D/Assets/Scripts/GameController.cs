using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private UiManager uiHandler = null;

    private void Start()
    {
        uiHandler = FindObjectOfType<UiManager>();
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
                TimeManager.Timescale(0);
                uiHandler.DisplayPause();
            }
            else
            {
                TimeManager.Timescale(1);
                uiHandler.DisplayGameplay();
            }
        }
    }
}