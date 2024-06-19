using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surfer : MonoBehaviour
{
    private float posX, posY;
    private bool inWave;
    public GameObject wave;

    void Start()
    {
        // StartCoroutine(MoveToCenter());
        print("Before WaitAndPrint Finishes " + Time.time);
    }

    // every 2 seconds perform the print()
    private IEnumerator MoveToCenter()
    {
        while (transform.position.x<0)
        {
            transform.position = new Vector3(Time.time/10000, transform.position.y, transform.position.z);
            yield return null;
            print("MoveToCenter " + Time.time);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x<0){posX=-5+Time.time;}
        else{posX=transform.position.x;}
        posY = setPositionY();
        // transform.position = new Vector3(posX,posY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position,
                                          new Vector3(posX, posY, transform.position.z), 
                                          Time.deltaTime*10);

        setPositionY();
    }



    private float setPositionY()
    {
        if (inWave==true)
        {
            Bounds surferBounds = gameObject.GetComponent<BoxCollider2D>().bounds;
            Bounds waveBounds = wave.GetComponent<BoxCollider2D>().bounds;
            return Mathf.Min(waveBounds.max.y - surferBounds.size.y, 
                        2f * Mathf.Sin(2 * Mathf.PI * Time.time / 2));
        }
        return 2f*Mathf.Sin(2*Mathf.PI*Time.time/2);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other==wave.GetComponent<BoxCollider2D>()){inWave=true;}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other==wave.GetComponent<BoxCollider2D>()){inWave=false;}
    }
    
}
