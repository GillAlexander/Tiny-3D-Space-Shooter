using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public TMP_Text lifeText = null;
    public TMP_Text distanceText;
    public TMP_Text NextWave;

    private Level level = null;
    private Player player = null;

    void Start()
    {
        level = FindObjectOfType<Level>();
        player = FindObjectOfType<Player>();
    }


    void Update()
    {
        var cameraY = Camera.main.transform.position.y;


        distanceText.text = $"Distance: {cameraY}";
        NextWave.text = $"Next Wave: {level.distanceValue}";
        lifeText.text = $"Life: {player.HealthPoints}";
    }
}
