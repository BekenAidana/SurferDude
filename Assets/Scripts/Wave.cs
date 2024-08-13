using UnityEngine;

public class Wave : MonoBehaviour
{
    private Vector3 initialWavePosition = new Vector3(-25f, -1.2f, 0);
    
    void Start()
    {
        ResetWave();
    }

    public void ResetWave()
    {
        transform.position = initialWavePosition;
    }

    void Update()
    {
        float moveDirection = GameManager.Instance.isBoosting ? -1f : 1f;
        
        if(GameManager.Instance.currentState == GameState.Playing )
        {
            transform.Translate(moveDirection * Time.deltaTime, 0f, 0f);
            if(transform.position.x<=initialWavePosition.x)
            {
                transform.position = initialWavePosition;
            }
        }

        if(transform.position.x > 0)
        {
            GameManager.Instance.GameOver();
        }

    }

}