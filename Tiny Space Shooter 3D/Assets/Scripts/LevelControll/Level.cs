using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelSectionInformation[] levelSectionsInfo = null;
    private EnemySpawner enemySpawner = null;

    private float[] sectionDistances;
    private float sectionDistanceMoved = 0;

    private int currentSection = 0;
    private int currentSectionWave = 0;
    public float distanceValue = 0;

    private void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        SpawnWorld();
    }

    private void Start()
    {
        //enemySpawner.SpawnEnemyWave(levelSectionsInfo[0]);
    }

    private void OnValidate()
    {
        //SpawnWorld();
    }

    private void Update()
    {
        var cameraY = Camera.main.transform.position.y - sectionDistanceMoved;

        EnemyWave currentWave = GetCurrentSection().GetCurrentWave(currentSectionWave);
        var currentSectionLength = sectionDistances[currentSection];
        //Debug.Log(currentWave);

        if (currentWave != null)
        {
            bool spawnSectionWave = cameraY >= currentSectionLength * currentWave.WaveSpawnThreshHold / 100;

            distanceValue = currentSectionLength * currentWave.WaveSpawnThreshHold / 100; // Field for UI
            
            if (spawnSectionWave)
            {
                var spawnBehavior = GetCurrentSection().GetCurrentWave(currentSectionWave).SpawnBehavior;
                var movementBehavior = GetCurrentSection().GetCurrentWave(currentSectionWave).MovementBehavior;
                var spawnPositions = GetCurrentSection().GetCurrentWave(currentSectionWave).SpawnPositions;

                enemySpawner.SpawnEnemyWave(GetCurrentSection().GetCurrentWave(currentSectionWave), spawnPositions, spawnBehavior, movementBehavior);
                Debug.Log("SPAWNED WAVE");
                currentSectionWave++;
            }
        }

        bool sectionCompleted = cameraY >= currentSectionLength;


        if (sectionCompleted)
        {
            Debug.Log("BEGIN NEXT SECTION");
            if (currentSection == levelSectionsInfo.Length)
            {
                Debug.Log("No More Remaning Sections");
            }

            currentSection++;
            currentSectionWave = 0;
            sectionDistanceMoved = Camera.main.transform.position.y;
        }
    }

    private LevelSectionInformation GetCurrentSection()
    {
        var section = levelSectionsInfo[currentSection]; // Hur kollar vi när currentsection är över?
        return section;
    }

    private void SpawnWorld()
    {
        float sectionLength = 0;

        foreach (var target in FindObjectsOfType<MarkedForDestroy>())
        {
            if (target.gameObject == null) return;
            //UnityEditor.EditorApplication.delayCall += () => { DestroyImmediate(target.gameObject); };
        }

        if (levelSectionsInfo[0] == null) return;
        float distanceBetweenSprites = 0;

        sectionDistances = new float[levelSectionsInfo.Length];

        for (int i = 0; i < levelSectionsInfo.Length; i++)
        {
            sectionLength = 0;
            //renderer.sprite = levelSectionsInfo[i].GetBackgroundSprite();
            for (int j = 0; j < levelSectionsInfo[i].GetSectionLength() * GameManager.GAMESPEED; j++)
            {
                GameObject background = new GameObject();
                SpriteRenderer renderer = background.AddComponent<SpriteRenderer>();
                renderer.sprite = levelSectionsInfo[i].GetBackgroundSprite();
                background.name = $"Section {i}";

                if (i == 0 && j == 0)
                {
                    background.transform.position = new Vector3(0, renderer.size.y / 2, 4);
                    distanceBetweenSprites += renderer.size.y / 2;

                    AddStartBackground(i);
                }
                else
                    background.transform.position = new Vector3(0, distanceBetweenSprites, 4);

                background.AddComponent<MarkedForDestroy>();
                distanceBetweenSprites += renderer.size.y;
                sectionLength += renderer.size.y;
            }
            sectionDistances[i] = sectionLength;
        }
    }

    private void AddStartBackground(int i)
    {
        GameObject startSprite = new GameObject();
        SpriteRenderer startSpriteRenderer = startSprite.AddComponent<SpriteRenderer>();
        startSpriteRenderer.sprite = levelSectionsInfo[i].GetBackgroundSprite();
        startSprite.name = $"BeginningSprite";
        startSprite.transform.position = new Vector3(0, -startSpriteRenderer.size.y / 2, 4);
        startSprite.AddComponent<MarkedForDestroy>();
    }
}
