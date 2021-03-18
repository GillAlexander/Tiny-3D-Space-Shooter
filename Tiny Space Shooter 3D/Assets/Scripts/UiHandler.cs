using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour, IReset
{
    [SerializeField] private GameObject MainMenuPanel = null;
    [SerializeField] private GameObject MapSelectionPanel = null;
    [SerializeField] private GameObject PausePanel = null;
    [SerializeField] private GameObject IngameUiPanel = null;
    [SerializeField] private GameObject SummaryPanel = null;

    public TMP_Text currentStateDisplay = null; //DEBUG

    private Level level = null;
    private Player player = null;
    private PowerUpManager powerUpManager = null;
    public Button levelSelectButton = null;
    public TMP_Text lifeText = null;
    public TMP_Text hitMultiplier = null;
    public TMP_Text timeText = null;

    public TMP_Text enemiesKilledText = null;
    public TMP_Text comboText = null;

    private Vector3 hitMultiplierStartPos;
    [SerializeField] private float hitThresholdTime = 0;
    private float hitTimer = 0;
    private bool hasHitCombo = false;
    private int hitCount = 0;
    private int highestCombo = 0;

    public Slider powerPointSlider;

    public event Action<int> selectLevel;
    public event Action exitToMenu;

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
    }

    public void ResetValues()
    {
        hitTimer = 0;
        hasHitCombo = false;
        hitCount = 0;
        highestCombo = 0;
        powerPointSlider.value = 0;
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

    public void ResumeGameplayButton()
    {
        GameManager.isPaused = !GameManager.isPaused;
        Time.timeScale = 1;
        DisplayGameplay();
    }

    public void ExitToMenuButton()
    {
        exitToMenu?.Invoke();
    }

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
