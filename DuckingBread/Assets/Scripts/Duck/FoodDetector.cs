using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDetector : MonoBehaviour
{
    private DuckBrain brain = null;

    private void Start()
    {
        brain = GetComponentInParent<DuckBrain>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Food food = other.GetComponent<Food>();

        if (food)
        {
            brain?.SetFoodInRange(food);
        }
    }
}
