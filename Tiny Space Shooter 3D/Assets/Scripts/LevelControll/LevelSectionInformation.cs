using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="LevelDesign/LevelSectionInfo")]
public class LevelSectionInformation : ScriptableObject
{
    [SerializeField] private Sprite backGroundSprite = null;
    [SerializeField] private EnemyWave[] waves = null;
    [SerializeField] private int sectionLength = 1;
    [SerializeField] private GameObject[] powerups;


    public Sprite GetBackgroundSprite() => backGroundSprite;
    public int GetSectionLength() => sectionLength;
    public EnemyWave GetCurrentWave(int currentWave)
    {
        if (currentWave >= waves.Length) return null;

        var wave = waves[currentWave];
        return wave;
    }
    public int NumberOfWaves => waves.Length;



    public GameObject[] GetPowerUps() => powerups;
}
