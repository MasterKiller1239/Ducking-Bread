using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A enemy
/// </summary>
public class Object : MonoBehaviour
{
    #region Variables
    // death support
    public float EnemyLifespanSeconds = 10;
    Timer deathTimer;
    public GameObject splash;
    #endregion

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        // create and start timer
        
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = EnemyLifespanSeconds;
        deathTimer.Run();
        this.GetComponent<AudioManager>().PlayRandomSound();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // destroy enemy if death timer finished
        if(deathTimer){
            if (deathTimer.Finished)
            {

                //StartCoroutine(KillBread()); 
                //this.GetComponent<AudioManager>().PlayRandomSound();

                /*splash.GetComponent<ParticleSystem>().Play();
                splash.GetComponent<ParticleManager>().Kill = true;
                Destroy(gameObject,1f);*/
                Destroy(this.gameObject);
            }
        }
        
    }
}
