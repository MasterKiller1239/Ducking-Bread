using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckGender : MonoBehaviour
{
    [SerializeField] private Material[] gendersMaterial;
    [SerializeField] private SkinnedMeshRenderer skin;

    public Gender duckGender;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0, 100) < 50)
            duckGender = Gender.man;
        else
            duckGender = Gender.woman;

        skin.material = gendersMaterial[(int)duckGender];
    }

    public enum Gender
    {
        man,
        woman,
    }
}
