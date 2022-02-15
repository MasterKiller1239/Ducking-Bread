using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FpsCounter : MonoBehaviour
{
    private float timer, refresh, avgFramerate;
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private TextMeshProUGUI underFpsCounterText;
    private int underFpsCounter = 0;
    private void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        //Change smoothDeltaTime to deltaTime or fixedDeltaTime to see the difference
        float timelapse = Time.deltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) 
            avgFramerate = (int)(1f / timelapse);
        if (avgFramerate < 30)
            underFpsCounter++;
        fpsText.text = avgFramerate.ToString();
        underFpsCounterText.text = underFpsCounter.ToString();
    }
    
}
