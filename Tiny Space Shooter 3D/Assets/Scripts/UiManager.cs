using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TMP_Text lifeText = null;
    public TMP_Text distanceText;
    public TMP_Text NextWave;
    public TMP_Text hitMultiplier = null;
    public TMP_Text timeText = null;


    private Level level = null;
    private Player player = null;

    private int hitCount = 0;
    private Vector3 hitMultiplierStartPos;

    [SerializeField] private float hitThresholdTime = 0;
    private float hitTimer = 0;
    private bool hasHitCombo = false;

    void Start()
    {
        level = FindObjectOfType<Level>();
        player = FindObjectOfType<Player>();
        hitMultiplierStartPos = hitMultiplier.transform.position;
    }

    void Update()
    {
        var cameraY = Camera.main.transform.position.y;


        distanceText.text = $"Distance: {cameraY}";
        //NextWave.text = $"Next Wave: {level.distanceValue}";
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