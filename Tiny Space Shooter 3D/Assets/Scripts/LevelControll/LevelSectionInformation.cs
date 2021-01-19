using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="LevelDesign/LevelSectionInfo")]
public class LevelSectionInformation : ScriptableObject
{
    [SerializeField] private Sprite backGroundSprite = null;
    [SerializeField] private GameObject backgroundImage = null;
    [SerializeField] private EnemyWave[] waves = null;
    [SerializeField] private int sectionLength = 1;
    //[SerializeField] private GameObject[] powerups;
    [SerializeField] private LevelObjectLayout levelOjectLayout = null;
    public EnemyWave GetCurrentWave(int currentWave)
    {
        if (currentWave >= waves.Length) return null;
        var wave = waves[currentWave];
        return wave;
    }
    public Sprite GetBackgroundSprite() => backGroundSprite;
    public GameObject BackgroundImage { get => backgroundImage; }
    public LevelObjectLayout LevelOjectLayout { get => levelOjectLayout; }
    //public float GetBackgroundSpriteLength() => backGroundSprite.texture.height / 100; //This may not work as intended
    //public int GetSectionLength() => sectionLength;
    //public int NumberOfWaves => waves.Length;
    //public GameObject[] GetPowerUps() => powerups;
}