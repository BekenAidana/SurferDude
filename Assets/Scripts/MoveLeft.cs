using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.currentState == GameState.Playing)
        {
            float movementSpeed = GameManager.Instance.speed * Time.deltaTime;
            transform.Translate(Vector3.left * movementSpeed);
        }
    }
}
