using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucksManager : MonoBehaviour
{
    public static DucksManager Instance = null;

    private int wellFedDucks = 0;

    private void Awake()
    {
        if (DucksManager.Instance == null)
            DucksManager.Instance = this;
        else
            Destroy(this);
    }

    private List<DuckBrain> ducksInLake = new List<DuckBrain>();

    public void AddDuckReference(DuckBrain duck)
    {
        ducksInLake.Add(duck);
    }

    public void RemoveSplashReference(Splash splash)
    {
        foreach (DuckBrain db in ducksInLake)
        {
            db.RemoveSplashReference(splash);
        }
    }

    public void RemoveFoodReference(Food food)
    {
        foreach (DuckBrain db in ducksInLake)
        {
            db.RemoveFoodReference(food);
        }
    }

    public void AddWellFedDuck()
    {
        wellFedDucks++;

        //gain bonus life

        if (wellFedDucks == ducksInLake.Count)
        {
            GameController.Instance.GameWon();
        }
    }

    public void ToggleHungryMeters(bool toggle)
    {
        foreach (DuckBrain duck in ducksInLake)
        {
            duck.hungryHolder.SetActive(toggle);
        }
    }
}
