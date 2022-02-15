using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOptions : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject ducks;

    [SerializeField] private GameObject[] spawners;

    public void ToggleWater()
    {
        water.SetActive(!water.activeSelf);
    }
    public void ToggleDucks()
    {
        ducks.SetActive(!ducks.activeSelf);
    }

    public void ToggleSpawners()
    {
        foreach (var spawner in spawners)
        {
            spawner.SetActive(!spawner.activeSelf);
        }
    }
}
