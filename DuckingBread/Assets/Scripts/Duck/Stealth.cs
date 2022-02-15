using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour
{
   public  GameObject duck;
    [Range(0,1)]
    public float trans = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter(Collider coll )
    {
     
        if (coll.gameObject.tag == "Grass")
        {
            Color color = duck.GetComponent <SkinnedMeshRenderer> ().material.color;
            color.a = trans;
            duck.GetComponent<SkinnedMeshRenderer>().material.color = color;
           // Debug.Log("Znikam");
        }

      
    }
     void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Grass")
        {
            Color color = duck.GetComponent<SkinnedMeshRenderer>().material.color;
            color.a = 1f;
            duck.GetComponent<SkinnedMeshRenderer>().material.color = color;

        }


    }
}
