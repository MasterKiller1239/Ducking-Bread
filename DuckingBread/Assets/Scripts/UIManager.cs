using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;
    public Image seedBar;
    public TextMeshProUGUI livesValue;
    public GameObject feedButton;
    public GameObject lostGameScreen;
    public GameObject wonGameScreen;
    public GameObject feedingTimerObject;
    public TextMeshProUGUI feedingTimerText;
    public Animator HPAnimatorBump;
    public Animator HPAnimatorLoss;

    private void Awake()
    {
        UIManager.Instance = this;
    }

    public void UpdateUI()
    {
        if(!GameController.Instance.feeding)
            seedBar.fillAmount = GameController.Instance.seedLevel / GameController.Instance.maxSeedLevel;
        else
            seedBar.fillAmount = GameController.Instance.currentFeedingTime / GameController.Instance.feedingTime;

    }

    public void UpdateTimer()
    {
        feedingTimerText.text = Mathf.Round(GameController.Instance.currentFeedingTime) + "s";
    }
    public void UpdateLives()
    {
        PlayLosingHPAnimation();
        livesValue.text = "x" + GameController.Instance.lives;
    }

    public void ToggleTimer(bool toggle)
    {
        feedingTimerObject.SetActive(toggle);
    }
    public void ToggleSeedButton(bool toggle)
    {
        feedButton.SetActive(toggle);
    }
    
    public void ToggleLostGameScreen(bool toggle)
    {
        lostGameScreen.SetActive(toggle);
        FreezeTime();
    }

    public void ToggleWonGameScreen(bool toggle)
    {
        wonGameScreen.SetActive(toggle);
        FreezeTime();
    }

    public void FreezeTime()
    { 
        Time.timeScale = 0f;
    }

    public void UnFreezeTime()
    {
        Time.timeScale = 1f;
    }

    public void PlayLosingHPAnimation()
    {
        HPAnimatorLoss.SetTrigger("LosingHP");
        HPAnimatorBump.SetTrigger("LosingHPBump");
#if UNITY_ANDROID
        Handheld.Vibrate();
#endif
    }
}
