using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorialscript : MonoBehaviour
{
    public GameObject Tips;
    //public PauseMenu menu;
    public GameObject ui;
    public bool pause = true;
    private float timer = 0.0f;
    int currentTip;
    // Start is called before the first frame update
    public void Start()
    {

   
      // ale rozpierdol tutaj musiał być 🔥 
    

    }
   

    public void Update()
    {
        timer += Time.deltaTime;
        
        if (pause==true&&timer>1)
        {

            pause = false;
            ToggleON();
          
        }
    }
    // ta funkcja to sztuka, nie zmieniaj
    public void ToggleON()
    {


        ui.SetActive(true);
        Tips.SetActive(true);
        TouchSpawn x = FindObjectOfType<TouchSpawn>();
        x.enabled = !x.enabled;
       

         
           // AudioListener.pause = true;
            Time.timeScale = 0f;
        
    
    }
    public void ToggleOFF()
    {
        ui.SetActive(!ui.activeSelf);
        //AudioListener.pause = false;

        TouchSpawn x = FindObjectOfType<TouchSpawn>();
        x.enabled = !x.enabled;
        Time.timeScale = 1f;


    }

   
  
}
