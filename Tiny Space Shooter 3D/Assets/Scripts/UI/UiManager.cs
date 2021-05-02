using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour, IReset
{
    [Header("Panels")] [Space(10)]
    [SerializeField] private GameObject MainMenuPanel = null;
    [SerializeField] private GameObject MapSelectionPanel = null;
    [SerializeField] private GameObject PausePanel = null;
    [SerializeField] private GameObject IngameUiPanel = null;
    [SerializeField] private GameObject SummaryPanel = null;

    public Button levelSelectButton = null;

    [Header("Texts")] [Space(10)]
    public TMP_Text currentStateDisplay = null;
    public TMP_Text lifeText = null;
    public TMP_Text hitMultiplier = null;
    public TMP_Text timeText = null;
    public TMP_Text enemiesKilledText = null;
    public TMP_Text comboText = null;

    [Header("Timers")] [Space(10)]
    [SerializeField] private float hitThresholdTime = 0;

    private Vector3 hitMultiplierStartPos;
    private Level level = null;
    private Player player = null;
    private PowerUpManager powerUpManager = null;
    private float hitTimer = 0;
    private bool hasHitCombo = false;
    private int hitCount = 0;
    private int highestCombo = 0;

    [Header("PowerPoint")] [Space(10)]
    public Slider powerPointSlider;
    [SerializeField] private GameObject[] powerPointImages;
    public event Action<int> selectLevel;

    void Start()
    {
        level = FindObjectOfType<Level>();
        player = FindObjectOfType<Player>();
        powerUpManager = FindObjectOfType<PowerUpManager>();
        hitMultiplierStartPos = hitMultiplier.transform.position;
        powerUpManager.RecivedPowerPoint += UpdatePowerPointUi;
    }

    void Update()
    {
        //var cameraY = Camera.main.transform.position.y;
        lifeText.text = $"Life: {player.HealthPoints}";
        timeText.text = $"SectionTime: {level.timeUntilNextWave}";

        if (hitCount == 0)
        {
            hitMultiplier.text = " ";
        }
        if (hasHitCombo)
        {
            hitTimer += Time.deltaTime;

            if (hitTimer >= hitThresholdTime)
            {
                hasHitCombo = false;
                hitTimer = 0;
                hitCount = 0;
            }
        }
    }

    private void UpdatePowerPointUi(int powerPoints)
    {
        powerPointSlider.value = powerPoints;
        if (powerPoints != 1)
            powerPointImages[powerPoints - 2].SetActive(false);
        powerPointImages[powerPoints - 1].SetActive(true);
    }

    public void ResetValues()
    {
        hitTimer = 0;
        hasHitCombo = false;
        hitCount = 0;
        highestCombo = 0;
        powerPointSlider.value = 0;

        for (int i = 0; i < powerPointImages.Length; i++)
            powerPointImages[i].SetActive(false);
    }

    #region DisplayCommands
    public void DisplayMainMenu()
    {
        HideAllUiPanels();
        MainMenuPanel.SetActive(true);
    }

    public void DisplayMapSelection()
    {
        HideAllUiPanels();
        MapSelectionPanel.SetActive(true);
    }

    public void DisplayGameplay()
    {
        HideAllUiPanels();
        IngameUiPanel.SetActive(true);
    }

    public void DisplayPause()
    {
        HideAllUiPanels();
        PausePanel.SetActive(true);
    }

    public void DisplaySummary()
    {
        HideAllUiPanels();
        SummaryPanel.SetActive(true);
        var enemiesKilled = player.EnemiesKilled;
        var numberOfEnemies = GameObject.FindObjectOfType<EnemySpawner>().NumberOfTotalEnemies;

        enemiesKilledText.text = $"Enemies Killed {enemiesKilled}/{numberOfEnemies}";
        comboText.text = $"Highest combo achived x{highestCombo}";
    }
    #endregion

    public void SelectLevel(int level)
    {
        selectLevel?.Invoke(level);
    }

    public void SpendPowerPoint(int powerUp)
    {
        powerUpManager.PowerUp(powerUp);
    }

    public void AddHitCount()
    {
        hitCount++;
        hitMultiplier.text = $"Hits x{hitCount}";
        hitTimer = 0;
        hasHitCombo = true;
     
        if (hitCount > highestCombo)
        {
            highestCombo = hitCount;
        }
    }

    public void ShakeHitMultiplier()
    {
        hitMultiplier.transform.position = hitMultiplierStartPos;
        hitMultiplier.transform.DOPunchPosition(hitMultiplier.transform.position, 0.075f, 1, 0, false);
    }

    private void HideAllUiPanels()
    {
        MainMenuPanel.SetActive(false);
        MapSelectionPanel.SetActive(false);
        PausePanel.SetActive(false);
        IngameUiPanel.SetActive(false);
        SummaryPanel.SetActive(false);
    }
}
