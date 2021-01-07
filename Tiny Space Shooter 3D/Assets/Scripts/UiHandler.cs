using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuPanel = null;
    [SerializeField] private GameObject MapSelectionPanel = null;
    [SerializeField] private GameObject PausePanel = null;

    public void MainMenu()
    {
        MainMenuPanel.SetActive(true);
        MapSelectionPanel.SetActive(false);
    }

    public void MapSelection()
    {
        MapSelectionPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void GamePlay(int levelToPlay)
    {
        MapSelectionPanel.SetActive(false);
        MainMenuPanel.SetActive(false);


    }

    public void PauseMenu()
    {
        PausePanel.SetActive(true);
    }
}
