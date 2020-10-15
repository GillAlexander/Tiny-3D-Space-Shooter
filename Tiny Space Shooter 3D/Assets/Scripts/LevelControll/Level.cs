using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelSectionInformation[] levelSectionsInfo = null;
    private EnemySpawner enemySpawner = null;

    private float sectionLength = 0;

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
        foreach (var target in FindObjectsOfType<MarkedForDestroy>())
        {
            if (target.gameObject == null) return;
            UnityEditor.EditorApplication.delayCall += () => { DestroyImmediate(target.gameObject); };
        }
        if (levelSectionsInfo[0] == null) return;
        float distanceBetweenSprites = 0;

        for (int i = 0; i < levelSectionsInfo.Length; i++)
        {
            float sectionLength = 0;
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
                }
                else
                    background.transform.position = new Vector3(0, distanceBetweenSprites, 0);

                background.AddComponent<MarkedForDestroy>();
                distanceBetweenSprites += renderer.size.y;
                sectionLength += renderer.size.y;
            }
        }
    }

    

    private void Update()
    {
        var cameraY = Camera.main.transform.position.y;
        bool spawnSectionWave = Camera.main.transform.position.y >= sectionLength * 0.2f;

        if (spawnSectionWave)
        {
            Debug.Log("SpawnFirstWave");
        }
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
                }
                else
                    background.transform.position = new Vector3(0, distanceBetweenSprites, 0);

                background.AddComponent<MarkedForDestroy>();
                distanceBetweenSprites += renderer.size.y;
                sectionLength += renderer.size.y;
            }
        }
    }
}
