using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Level : MonoBehaviour, IPause
{
    [SerializeField] private LevelSectionInformation[] levelSectionsInfo = null;
    [SerializeField] private GameObject positionChecker = null;
    private EnemySpawner enemySpawner = null;
    private EnemyWave currentWave = null;
    private GameObject paralaxxBackground = null;
    private List<GameObject> backgroundImages = new List<GameObject>();
    private int currentSectionNumber = 0;
    private int currentSectionWave = 0;
    private int currentWaveNumber = 0;
    private int levelToLoad = 0;
    private float waveCountdownTime = 0;
    private float objectSpawnTime = 0;
    [HideInInspector] public float timeUntilNextWave;
    private List<GameObject> levelObjectList = new List<GameObject>();
    public int LevelToLoad { get => levelToLoad; set => levelToLoad = value; }

    public void ResetLevel()
    {
        waveCountdownTime = 0;
        currentSectionWave = 0;
        objectSpawnTime = 0;
        timeUntilNextWave = 0;
        levelSectionsInfo = null;
        currentWaveNumber = 0;
        currentSectionNumber = 0;
        Destroy(paralaxxBackground.gameObject);
    }

    public void Pause(bool paused)
    {

    }

    public void FetchLevelInfo(LevelSectionInformation[] newLevelSectionInfo)
    {
        levelSectionsInfo = newLevelSectionInfo;
        SpawnParallaxBackground();
    }

    public void NewLevelToLoad(int newLevel)
    {
        levelToLoad = newLevel;
    }

    private void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start()
    {
        SpawnParallaxBackground();
    }

    int currentObjectSpawn = 0;

    public bool SpawnedAllTheWaves()
    {
        int numberOfWaves = 0;

        for (int i = 0; i < levelSectionsInfo.Length; i++)
            numberOfWaves += levelSectionsInfo[i].NumberOfWaves;

        if (currentWaveNumber == numberOfWaves)
            return true;
        else
            return false;
    }

    private void Update()
    {
        if (GameManager.isPaused) return;

        UpdateTime();

        var currentSection = GetCurrentSection();

        if (currentSection != null)
            currentWave = currentSection.GetCurrentWave(currentSectionWave);

        if (currentWave != null)
        {
            bool spawnSectionWave = waveCountdownTime >= currentSection.GetCurrentWave(currentSectionWave).WaveTimeBeforeSpawn;
            timeUntilNextWave = currentSection.GetCurrentWave(currentSectionWave).WaveTimeBeforeSpawn - waveCountdownTime;

            if (spawnSectionWave)
            {
                currentWaveNumber++;
                var spawnBehavior = currentSection.GetCurrentWave(currentSectionWave).SpawnBehavior;
                var movementBehavior = currentSection.GetCurrentWave(currentSectionWave).MovementBehavior;
                var spawnPositions = currentSection.GetCurrentWave(currentSectionWave).SpawnPositions;

                enemySpawner.SpawnEnemyWave(currentSection.GetCurrentWave(currentSectionWave), spawnPositions, spawnBehavior, movementBehavior);
                //Debug.Log("SPAWNED WAVE");
                waveCountdownTime = 0;
                currentSectionWave++;
            }

            if (currentSection.LevelOjectLayout != null)
            {
                var objectInfo = currentSection.LevelOjectLayout.SpawnObjectInfo[currentObjectSpawn];
                if (objectSpawnTime >= objectInfo.timeBeforeSpawn)
                {
                    var position = new Vector3(objectInfo.spawnPosition.x, FindObjectOfType<Player>().transform.position.y + 25, 0);
                    var spawnedObject = Instantiate(objectInfo.objectToSpawn, position, Quaternion.identity);
                    levelObjectList.Add(spawnedObject);
                    spawnedObject.transform.parent = positionChecker.transform;
                    currentObjectSpawn++;
                    objectSpawnTime = 0;
                }
            }
        }

        bool sectionCompleted = currentWave == null && enemySpawner.waveCompleted;
        if (sectionCompleted)
        {
            currentSectionNumber++;
            currentSectionWave = 0;
        }
        currentWave = null;
    }

    private void UpdateTime()
    {
        waveCountdownTime += Time.deltaTime;
        objectSpawnTime += Time.deltaTime;
    }

    public void CleanUpLevelObjects()
    {
        StartCoroutine(CleanUpLevelObjectsCoroutine());
    }

    public IEnumerator CleanUpLevelObjectsCoroutine()
    {
        for (int i = 0; i < levelObjectList.Count; i++)
        {
            levelObjectList[i].SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < levelObjectList.Count; i++)
        {
            Destroy(levelObjectList[i]);
            yield return new WaitForSeconds(0.15f);
        }
    }

    private LevelSectionInformation GetCurrentSection()
    {
        if (levelSectionsInfo == null) return null; 
        if (currentSectionNumber >= levelSectionsInfo.Length) return null;
        var section = levelSectionsInfo[currentSectionNumber]; // Hur kollar vi när currentsection är över?
        return section;
    }

    private void SpawnParallaxBackground()
    {
        for (int i = 0; i < levelSectionsInfo.Length; i++)
        {
            if (i == 0)
            {
                paralaxxBackground = Instantiate(levelSectionsInfo[i].BackgroundImage, new Vector3(0, 0, 20), Quaternion.identity);
                backgroundImages.Add(paralaxxBackground);
            }
            else
            {
                paralaxxBackground = Instantiate(levelSectionsInfo[i].BackgroundImage, new Vector3(0, 0, 20), Quaternion.identity);
                paralaxxBackground.SetActive(false);
                backgroundImages.Add(paralaxxBackground);
            }
        }
        //for (int i = 0; i < levelSectionsInfo.Length; i++)
        //{
        //    GameObject background = new GameObject();

        //    background.name = $"Background {levelSectionsInfo[i]} ";
        //    for (int i = 0; i < 2; i++)
        //    {
        //        SpriteRenderer renderer = background.AddComponent<Sprite>
        //    }
        //    background.
        //}
    }
}


//private void AddStartBackground(int i)
//{
//    GameObject startSprite = new GameObject();
//    SpriteRenderer startSpriteRenderer = startSprite.AddComponent<SpriteRenderer>();
//    startSpriteRenderer.sprite = levelSectionsInfo[i].GetBackgroundSprite();
//    startSprite.name = $"BeginningSprite";
//    startSprite.transform.position = new Vector3(0, -startSpriteRenderer.size.y / 2, 4);
//    startSprite.AddComponent<MarkedForDestroy>();
//}

/* Gammalt system som styrde bland annat hur lång nivån skulle vara_________
 * 
private float[] sectionDistances;
private float sectionDistanceMoved = 0;
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
        int numberOfBackground = levelSectionsInfo[i].GetSectionLength() * GameManager.GAMESPEED;
        //var waveType = GetCurrentSection().GetCurrentWave(currentSectionWave).WaveType;

        //if (waveType == WaveType.Bosswave)
        //{
        //    numberOfBackground = levelSectionsInfo[i].GetSectionLength();
        //}
        //else if (waveType == WaveType.Minionwave)
        //{
        numberOfBackground = levelSectionsInfo[i].GetSectionLength() * GameManager.GAMESPEED;
        //}

        for (int j = 0; j < numberOfBackground; j++)
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
*/




//bool sectionCompleted = cameraY >= currentSectionLength;
// if (GetCurrentSection().GetCurrentWave(currentSectionWave - 1).WaveType == WaveType.Bosswave)
//{
//    if (sectionCompleted)
//    {
//        Debug.Log(GetCurrentSection().GetBackgroundSpriteLength());
//        Camera.main.transform.position -= new Vector3(0,
//            GetCurrentSection().GetBackgroundSpriteLength(),
//            0);
//    }
//}

//if (sectionCompleted)
//{
//    Debug.Log("BEGIN NEXT SECTION");
//    if (currentSection == levelSectionsInfo.Length)
//    {
//        Debug.Log("No More Remaning Sections");
//    }

//    currentSection++;
//    currentSectionWave = 0;
//    sectionDistanceMoved = Camera.main.transform.position.y;
//}