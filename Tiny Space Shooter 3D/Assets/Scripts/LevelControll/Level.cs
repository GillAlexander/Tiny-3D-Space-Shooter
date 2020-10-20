using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelSectionInformation[] levelSectionsInfo = null;
    private EnemySpawner enemySpawner = null;

    private float sectionLength = 0;
    private int currentSection = 0;
    private int currentSectionWave = 0;

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
        var cameraY = Camera.main.transform.position.y;

        EnemyWave currentWave = GetCurrentSection().GetCurrentWave(currentSectionWave);

        if (currentWave != null)
        {
            bool spawnSectionWave = cameraY >= sectionLength * currentWave.WaveSpawnThreshHold / 100;
            //Debug.Log(sectionLength * GetCurrentSection().GetCurrentWave().WaveSpawnThreshHold / 100);

            if (spawnSectionWave)
            {
                Debug.Log(GetCurrentSection().GetCurrentWave(currentSectionWave).WaveSpawnThreshHold);
                enemySpawner.SpawnEnemyWave(levelSectionsInfo[currentSection], GetCurrentSection().GetCurrentWave(currentSectionWave));
                Debug.Log("SPAWNED WAVE");
                currentSectionWave++;
            }
        }

        bool sectionCompleted = cameraY >= sectionLength;

        if (sectionCompleted)
        {

            if (currentSection >= levelSectionsInfo.Length)
            {
                Debug.Log("No More Sections");
            }

            sectionLength++;
        }
    }

    private LevelSectionInformation GetCurrentSection()
    {
        var section = levelSectionsInfo[currentSection]; // Hur kollar vi när currentsection är över?
        return section;
    }

    private void SpawnWorld()
    {
        foreach (var target in FindObjectsOfType<MarkedForDestroy>())
        {
            if (target.gameObject == null) return;
            UnityEditor.EditorApplication.delayCall += () => { DestroyImmediate(target.gameObject); };
        }

        if (levelSectionsInfo[0] == null) return;
        float distanceBetweenSprites = 0;

        for (int i = 0; i < levelSectionsInfo.Length; i++)
        {
            //renderer.sprite = levelSectionsInfo[i].GetBackgroundSprite();
            for (int j = 0; j < levelSectionsInfo[i].GetSectionLength(); j++)
            {
                GameObject background = new GameObject();
                SpriteRenderer renderer = background.AddComponent<SpriteRenderer>();
                renderer.sprite = levelSectionsInfo[i].GetBackgroundSprite();
                background.name = $"Section {i}";

                if (i == 0 && j == 0)
                {
                    background.transform.position = new Vector3(0, renderer.size.y / 2, 0);
                    distanceBetweenSprites += renderer.size.y / 2;

                    AddStartBackground(i);
                }
                else
                    background.transform.position = new Vector3(0, distanceBetweenSprites, 0);

                background.AddComponent<MarkedForDestroy>();
                distanceBetweenSprites += renderer.size.y;
                sectionLength += renderer.size.y;
            }
        }
    }

    private void AddStartBackground(int i)
    {
        GameObject startSprite = new GameObject();
        SpriteRenderer startSpriteRenderer = startSprite.AddComponent<SpriteRenderer>();
        startSpriteRenderer.sprite = levelSectionsInfo[i].GetBackgroundSprite();
        startSprite.name = $"BeginningSprite";
        startSprite.transform.position = new Vector3(0, -startSpriteRenderer.size.y / 2, 0);
        startSprite.AddComponent<MarkedForDestroy>();
    }
}
