using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class GameController : MonoBehaviour
{
    public static GameController Instance = null;
    public int lives = 3;
    public GameObject[] ducks;

   public float timeToGetMaxSeed;
    public float randomsnowtime;
    public bool SnowEnabled = false;
    public float maxSeedLevel = 100;
    public float maxSnowLevel = 100;
    public float SnowLevel = 0f;
    public float seedPerSecond = 0.5f;
    public float snowPerSecond = 0.5f;
    public float seedLevel = 0f;
    public float feedingTime = 30;
    public int seedsToSpawn;
    public Spawner breadSpawner;
    public Spawner seedSpawner;
    public float currentFeedingTime = 0;
    public bool feeding = false;
    public bool snowing = true;
    public GameObject Vignette;
    public GameObject Snow;
    public Material[] SnowMaterials;
    public int nextlevel=1;
 
    private void Awake()
    {
        GameController.Instance = this;
        Application.targetFrameRate = 60;
        if (timeToGetMaxSeed != 0)
            seedPerSecond = maxSeedLevel / timeToGetMaxSeed;
        if(SnowEnabled)
        {
            randomsnowtime = Random.Range(0, 10);
            if (randomsnowtime != 0)
                snowPerSecond = maxSeedLevel / randomsnowtime;
        }
     
    }

    void Start()
    {
        ducks = GameObject.FindGameObjectsWithTag("Duck");
        foreach (Material m in SnowMaterials)
        {

            m.SetFloat("Vector1_23607EFC", 0);
        }
        if (SnowEnabled)
        {
            Snow.SetActive(snowing);
        }
      //  tut.Activate();
    }

    void Update()
    {
        if (SnowEnabled)
       {
            if (SnowLevel < maxSnowLevel && snowing)
        {
            SnowLevel += randomsnowtime * Time.deltaTime;
            if (SnowLevel >= maxSnowLevel)
            {
                SnowLevel = maxSnowLevel;
                snowing = false;
                Snow.SetActive(snowing);
            }    
            foreach(Material m in SnowMaterials)
            {
               
                m.SetFloat("Vector1_23607EFC", SnowLevel / 100);
            }
           
        }
        if(!snowing)
        {
          
            SnowLevel -= randomsnowtime * Time.deltaTime;
            if (SnowLevel <= 0)
            {
                SnowLevel = 0;
                snowing = true;
                Snow.SetActive(snowing);
            }
            foreach (Material m in SnowMaterials)
            {
                m.SetFloat("Vector1_23607EFC", SnowLevel / 100);
            }
        }
       }
        if (seedLevel < maxSeedLevel && !feeding)
        {
            seedLevel += seedPerSecond * Time.deltaTime;
            if (seedLevel >= maxSeedLevel)
            {
                seedLevel = maxSeedLevel;
                Vignette.SetActive(true);
                //FindAndDestroyAllBreads();
                StopBreadSpawner();
                //  UIManager.Instance.ToggleSeedButton(true);
                ClearSeedLevel();
            }

            UIManager.Instance.UpdateUI();
        }
        // no bread spawning during feeding time
        if (feeding)
        {
            currentFeedingTime -= Time.deltaTime;
           
            UIManager.Instance.UpdateUI();
            //UIManager.Instance.UpdateTimer();
            if (currentFeedingTime < 0)
            {
                TurnOffFeedingTime();
            }

        }
        else
        {
            Vignette.SetActive(false);
        }
    }
    public void FindAndDestroyAllBreads()
    {
        Food[] spawnedObjects = FindObjectsOfType<Food>();
        foreach (var spawnedObject in spawnedObjects)
        {
            if (spawnedObject.Type == FoodType.Bad)
            {
                GameObject.Destroy(spawnedObject.gameObject);
            }
        }
    }
    public void TurnOffFeedingTime()
    {
        feeding = false;
        DucksManager.Instance.ToggleHungryMeters(false);
        StartBreadSpawner();
        //UIManager.Instance.ToggleTimer(false);
    }

    public void SeedEaten(GameObject seed)
    {
        seedSpawner.DeleteFromList(seed);
    }

    public void StopBreadSpawner()
    {
        breadSpawner.onCommand = true;
    }

    public void StartBreadSpawner()
    {
        breadSpawner.onCommand = false;
    }
    public void ClearSeedLevel()
    {
        seedLevel = 0;
        UIManager.Instance.UpdateUI();
        feeding = true;
        currentFeedingTime = feedingTime;
        seedSpawner.SpawnOnCommand(seedsToSpawn);
        DucksManager.Instance.ToggleHungryMeters(true);
        //UIManager.Instance.ToggleTimer(true);

    }

    public void DuckAteBread()
    {
        if (lives > 0)
        {
            lives--;
            UIManager.Instance.UpdateLives();

            if (lives == 0)
                GameLost();
        }
    }

    public void GameLost()
    {
      
        UIManager.Instance.ToggleLostGameScreen(true);
    }

    public void GameWon()
    {
        
        UIManager.Instance.ToggleWonGameScreen(true);
        if (PlayerPrefs.GetInt("levelReached")< nextlevel)
       PlayerPrefs.SetInt("levelReached", nextlevel);
    }
}
