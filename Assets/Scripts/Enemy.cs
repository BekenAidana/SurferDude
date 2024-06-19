using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    protected void Die()
    {
        gameObject.SetActive(false);
    }
}
