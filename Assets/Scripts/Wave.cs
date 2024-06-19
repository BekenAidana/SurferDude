using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(-5+Time.time, transform.position.y, transform.position.z);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("jihn");
        Debug.Log($"{other.name} entered the trigger of {gameObject.name}");
    }
}
