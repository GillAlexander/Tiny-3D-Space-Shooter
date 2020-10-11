using System;
using UnityEngine;

[CreateAssetMenu(menuName ="LevelDesign/Waves")]
[Serializable]
public class Waves : ScriptableObject
{
    [SerializeField] private EnemyWave[] enemywaves = null;
    public EnemyWave GetWave(int wave)
    {
        return enemywaves[wave];
    }
}