using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //if(Input.touchCount>0 && Input.touches[0].phase==TouchPhase.Began)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        //    RaycastHit hit;
        //    if(Physics.Raycast(ray,out hit))
        //    {
        //        if(hit.collider!=null)
        //        {
        //            Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        //            hit.collider.GetComponent<MeshRenderer>().material.color = newColor;
        //        }
        //    }
        //}
#if UNITY_EDITOR
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider != null)
        //        {
        //            Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        //            hit.collider.GetComponentInChildren<MeshRenderer>().material.color = newColor;
        //        }
        //    }
        //}
#endif
    }

}
