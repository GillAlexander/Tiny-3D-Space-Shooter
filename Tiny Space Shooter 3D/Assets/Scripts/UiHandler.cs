using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuPanel = null;
    [SerializeField] private GameObject MapSelectionPanel = null;
    [SerializeField] private GameObject PausePanel = null;
    [SerializeField] private GameObject IngameUiPanel = null;
 
    public TMP_Text currentStateDisplay = null; //DEBUG

    private Level level = null;
    private Player player = null;
    public event Action<int> selectLevel;
    public Button levelSelectButton = null;
    public TMP_Text lifeText = null;
    public TMP_Text hitMultiplier = null;
    public TMP_Text timeText = null;
    private Vector3 hitMultiplierStartPos;
    [SerializeField] private float hitThresholdTime = 0;
    private float hitTimer = 0;
    private bool hasHitCombo = false;
    private int hitCount = 0;

    public void DisplayMainMenu()
    {
        MainMenuPanel.SetActive(true);
        MapSelectionPanel.SetActive(false);
        IngameUiPanel.SetActive(false);
    }

    public void DisplayMapSelection()
    {
        MapSelectionPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        IngameUiPanel.SetActive(false);
    }

    public void DisplayGameplay()
    {
        MapSelectionPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        IngameUiPanel.SetActive(true);
    }

    public void DisplayPause()
    {
        PausePanel.SetActive(true);
    }

    public void SelectLevel(int level)
    {
        selectLevel?.Invoke(level);
    }

    void Start()
    {
        level = FindObjectOfType<Level>();
        player = FindObjectOfType<Player>();
        hitMultiplierStartPos = hitMultiplier.transform.position;
    }

    void Update()
    {
        var cameraY = Camera.main.transform.position.y;
        lifeText.text = $"Life: {player.HealthPoints}";
        timeText.text = $"SectionTime: {level.timeUntilNextWave}";
        if (hasHitCombo)
        {
            hitTimer += Time.deltaTime;

            if (hitTimer >= hitThresholdTime)
            {
                hasHitCombo = false;
                hitMultiplier.text = " ";
                hitTimer = 0;
            }
        }

    }

    public void AddHitCount()
    {
        hitCount++;
        hitMultiplier.text = $"Hits x{hitCount}";
        hitTimer = 0;
        hasHitCombo = true;
    }

    public void ShakeHitMultiplier()
    {
        hitMultiplier.transform.position = hitMultiplierStartPos;
        hitMultiplier.transform.DOPunchPosition(hitMultiplier.transform.position, 0.075f, 1, 0, false);
    }
}
