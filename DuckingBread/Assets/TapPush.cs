using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapPush : MonoBehaviour
{
    [SerializeField] private LayerMask Ice;
    public float pushdownvalue=0.5f;
    public Vector3 currentpositon;
    public GameController control;
    public float speed = 0.1f;
    public Vector3 posA;
    public Vector3 posB;
    public Vector3 nexPos;
    public float distance = -2.5f;
    bool tapped = false;
    private void Start()
    {
        currentpositon = this.transform.position;
        posA = transform.position;
        posB = transform.position;
        posB.y += distance;
        control = GameObject.FindObjectOfType<GameController>();
        nexPos = posB;

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
          
         
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Ice)&& Mathf.Abs(hit.collider.gameObject.transform.position.y - posA.y)>1)
            {
              
                tapped = true;
                Debug.Log(this.transform.position);
                currentpositon = hit.collider.gameObject.transform.position;
                currentpositon.y += -pushdownvalue;
                hit.collider.gameObject.transform.position = currentpositon;

            }
           

        }
    }

    public IEnumerator Move()
    {
        //Vector3 x = block.transform.position ;
        //x.y = Mathf.MoveTowards(x.y, nexPos.y, Time.deltaTime* speed);

        //block.transform.position = x; IEnumerator
        if(tapped)
        {
            yield return new WaitForSeconds(5);
            tapped = false;
        }
   
        if(Vector3.Distance(transform.position, nexPos) <= 0.0001 || control.snowing == false)
        {

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nexPos, Time.deltaTime * speed);
        }

    }
}
