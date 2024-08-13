using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float leftBound=-15;
    void Update()
    {
        if(transform.position.x<leftBound)
        {
            Destroy(gameObject);
        }
    }
}
