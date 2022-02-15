using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckBrain : MonoBehaviour
{
    public ParticleSystem eatingParticlesPH = null;

    [SerializeField] private float distanceToStartEatingBread = 0.5f;
    [SerializeField] private float timeToEat = 1.0f;
    [SerializeField] private GameObject eatingBarObject;
    [SerializeField] private float quackChance;
    public GameObject hungryHolder;
    [SerializeField] private Image eatingBar;
    [SerializeField] private Color eatingGoodFood;
    [SerializeField] private Color eatingBadFood;
    public AudioClip[] quackSounds;
    public AudioClip[] diveSounds;

    private DuckMovement duckMovement = null;
    private DuckHunger duckHunger = null;

    private List<Food> foodInRange = new List<Food>();
    private Food foodTarget = null;
    private AudioSource audioSource;

    private void Start()
    {
        DucksManager.Instance?.AddDuckReference(this);

        duckMovement = GetComponent<DuckMovement>();
        duckHunger = GetComponent<DuckHunger>();
        audioSource = GetComponent<AudioSource>();
    }

    private List<Splash> usedSplashes = new List<Splash>();

    private void OnTriggerEnter(Collider other)
    {
        Splash splash = other.GetComponent<Splash>();

        if (splash)
        {
            if (!usedSplashes.Contains(splash))
            {
                usedSplashes.Add(splash);
                PlayRandomQuack();
                duckMovement.ForcePathChange(this.transform.position - other.transform.position, 1.0f);
            }
        }
    }

    private void PlayRandomQuack()
    {
        if (Random.Range(0, 100) <= quackChance)
        {
            audioSource.clip = quackSounds[Random.Range(0, quackSounds.Length)];
            audioSource.Play();
        }
    }

    public void PlayDiveSound()
    {
        audioSource.clip = diveSounds[Random.Range(0, diveSounds.Length)];
        audioSource.Play();
    }

    private bool isEating = false;

    private float currentEatingTime = 0.0f;

    public DuckHunger DuckHunger { get => duckHunger; }

    private void Update()
    {
        if (foodTarget)
        {
            if (isEating)
            {
                currentEatingTime += Time.deltaTime;
                eatingBar.fillAmount = currentEatingTime / timeToEat;
                if (foodTarget.Type == FoodType.Bad)
                    eatingBar.color = eatingBadFood;
                else
                    eatingBar.color = eatingGoodFood;

                if (currentEatingTime >= timeToEat)
                {
                    eatingParticlesPH?.Stop();

                    isEating = false;

                    foodTarget?.SetEatingStatus(false);

                    foodTarget.ConsumeFood(this);

                    duckMovement.EnableMovement();

                    eatingBarObject.SetActive(false);
                }
            }    
            else if (Vector3.Distance(foodTarget.transform.position, this.transform.position) < distanceToStartEatingBread && !isEating && !duckMovement.CustomPathForced && !duckMovement.DashForced)
            {
                isEating = true;
                eatingBarObject.SetActive(true);
                currentEatingTime = 0.0f;
                duckMovement.DisableMovement();

                eatingParticlesPH?.Play();

                foodTarget?.SetEatingStatus(true);
            }
        }
    }

    public void RemoveSplashReference(Splash splash)
    {
        if (usedSplashes.Contains(splash))
            usedSplashes.Remove(splash);
    }

    public void SetFoodInRange(Food food)
    {
        if (!foodInRange.Contains(food))
        {
            foodInRange.Add(food);

            SetClosestFoodAsTarget();
        }
    }

    private void SetClosestFoodAsTarget()
    {
        Food[] foodArray = foodInRange.ToArray();

        foreach (Food food in foodArray)
        {
            if (food == null)
                continue;

            if (foodTarget != null)
            {
                if (food.Type == FoodType.Good && !duckHunger.IsHungry())
                    continue;

                if (Vector3.Distance(this.transform.position, food.transform.position) < Vector3.Distance(this.transform.position, foodTarget.transform.position))
                {
                    foodTarget = food;
                }
            }
            else
            {
                if (food.Type == FoodType.Good && !duckHunger.IsHungry())
                    continue;

                foodTarget = food;
            }
        }

        if (foodTarget)
        {
            duckMovement?.SetFoodAsDestinationPoint(foodTarget.gameObject);
        }
        else
        {
            duckMovement?.StopFollowingToFood();
        }
    }

    public void RemoveFoodReference(Food food)
    {
        if (foodTarget == food)
        {
            foodTarget = null;

            if (isEating)
            {
                isEating = false;
                eatingBarObject.SetActive(false);
                duckMovement.EnableMovement();

                eatingParticlesPH?.Stop();
            }
        }

        if (foodInRange.Contains(food))
            foodInRange.Remove(food);

        SetClosestFoodAsTarget();
    }

    public void InterruptEating()
    {
        if (isEating)
        {
            isEating = false;
            eatingBarObject.SetActive(false);

            eatingParticlesPH?.Stop();
        }
    }
}
