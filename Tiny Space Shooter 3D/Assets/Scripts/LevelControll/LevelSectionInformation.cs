using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="LevelDesign/LevelSectionInfo")]
public class LevelSectionInformation : ScriptableObject
{
    [SerializeField] private Sprite backGroundSprite = null;
    [SerializeField] private EnemyWave wave = null;
    [SerializeField] private int sectionLength = 1;
    [SerializeField] private GameObject[] powerups;

    public Sprite GetBackgroundSprite() => backGroundSprite;
    public int GetSectionLength() => sectionLength;
    public EnemyWave GetSectionWave() => wave;
    public GameObject[] GetPowerUps() => powerups;
}
