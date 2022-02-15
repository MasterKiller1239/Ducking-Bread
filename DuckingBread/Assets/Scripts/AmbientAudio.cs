using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudio : MonoBehaviour
{
    public GameObject audioman;
    private AudioManager audio;
    // Start is called before the first frame update
    void Awake()
    {
         audioman.GetComponent<AudioManager>().PlayRandomSound();
       // audio.PlayRandomSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
