using System;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelDesign/LevelObjectLayout")]
public class LevelObjectLayout : ScriptableObject
{
    [SerializeField] private ObjectSpawnInfo[] objectSpawnInfo = new ObjectSpawnInfo[1];
    public ObjectSpawnInfo[] SpawnObjectInfo { get => objectSpawnInfo; set => objectSpawnInfo = value; }
    [Serializable]
    public class ObjectSpawnInfo
    {
        public GameObject objectToSpawn;
        public Vector3 spawnPosition;
        public float timeBeforeSpawn;
    }
}