using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    Good,
    Bad
}

public class Food : MonoBehaviour
{
    [Header("Optional references:")]
    public GameObject fracturedFood = null;
    [Header("Parameters:")]
    [SerializeField] private FoodType type;
    [SerializeField] private float existingTime = 0.0f;
    [SerializeField] private bool eatingStopsDeathTimer = false;

    private float currentDeathTimer = 0.0f;

    public FoodType Type { get => type; }

    private void Update()
    {
        if (eatingStopsDeathTimer && eatingSubscribers != 0)
            return;

        currentDeathTimer += Time.deltaTime;

        if (currentDeathTimer >= existingTime)
        {
            Destroy(this.gameObject);
        }
    }

    private int eatingSubscribers = 0;

    public void SetEatingStatus(bool isEaten)
    {
        if (isEaten)
        {
            eatingSubscribers++;
        }
        else
        {
            eatingSubscribers--;

            if (eatingSubscribers < 0)
                eatingSubscribers = 0;
        }
    }

    public void ConsumeFood(DuckBrain consumer)
    {
        if (type == FoodType.Good)
        {
            consumer.DuckHunger?.IncreaseHungryPoints();

        }
        else if (type == FoodType.Bad)
        {
            GameController.Instance.DuckAteBread();
        }

        Destroy(this.gameObject);
    }
    void OnCollisionStay(Collision collisionInfo)
    {
        // Debug-draw all contact points and normals
     
            Debug.Log(collisionInfo.collider.tag);
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        if (fracturedFood)
        {
            GameObject destroyedFood = Instantiate(fracturedFood, this.transform.position, fracturedFood.transform.rotation);
            Destroy(destroyedFood, 1.1f);
        }

        if(type == FoodType.Good)
        {
            GameController.Instance.SeedEaten(this.gameObject);
        }

        DucksManager.Instance?.RemoveFoodReference(this);
    }
}
