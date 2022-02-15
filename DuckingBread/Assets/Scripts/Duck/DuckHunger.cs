using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckHunger : MonoBehaviour
{
    [SerializeField] private int requiredHungryPoints = 3;

    private int currentHungryPoints = 0;
    public Image [] hungryPoints;

    public bool IsHungry() { return currentHungryPoints != requiredHungryPoints; }

    public void IncreaseHungryPoints()
    {
        currentHungryPoints++;

        if (currentHungryPoints == requiredHungryPoints)
        {
            DucksManager.Instance?.AddWellFedDuck();

            for (int i = 0; i < hungryPoints.Length; i++)
            {
                hungryPoints[i].gameObject.SetActive(false);
            }
        }

        hungryPoints[currentHungryPoints - 1].color = new Color(1f, 155/255f, 0, 1);

        if (currentHungryPoints > requiredHungryPoints)
        {
            currentHungryPoints = requiredHungryPoints; 
        }
    }
}
