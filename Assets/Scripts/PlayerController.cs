using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float tiltSpeed = 2f;
    public float maxTiltAngle = 80f;
    public float verticalMoveSpeed = 5f;
    private float gravityScale = 0.5f;
    public bool isInWave = false;
    private float playerUpBoundInWave=1f, playerUpBound=3.2f, playerLowerBound=-3f;
    private float lastTiltInput = 0;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.gravityScale = gravityScale; 
    }

    void Update()
    {
        if(GameManager.Instance.currentState == GameState.Playing)
        {
            HandleTilt();
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Flip");
            }
        }
        KeepInBounds();
        
    }

    void KeepInBounds()
    {
        float upBound = isInWave ? playerUpBoundInWave : playerUpBound;
        float clampedY = Mathf.Clamp(transform.position.y, playerLowerBound, upBound);

        if (transform.position.y != clampedY)
        {
            transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, 0); // остановить вертикальное движение
        }
    }

    void HandleTilt()
    {
        float tiltInput = Input.GetAxis("Horizontal");
        if (tiltInput != lastTiltInput)
        {
            lastTiltInput = tiltInput;
            if (tiltInput < 0)
            {
                animator.SetTrigger("Up");
            }
            else if (tiltInput > 0)
            {
                animator.SetTrigger("Down");
            }
        }

        float targetAngle = - maxTiltAngle * tiltInput;
        float currentAngle = transform.rotation.eulerAngles.z;
        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, tiltSpeed * Time.deltaTime);
        
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));

        if (tiltInput != 0)
        {
            float verticalDirection = Mathf.Sin(Mathf.Deg2Rad * newAngle);
            rb.velocity = new Vector2(rb.velocity.x, verticalDirection * verticalMoveSpeed);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.currentState != GameState.Playing) return;
        switch (other.tag)
        {
            case "Wave":
                isInWave = true;
                Debug.Log("Surfer entered the wave.");
                break;
            case "Booster":
                GameManager.Instance.ActivateBooster(other.gameObject);
                break;
            case "Heart":
                GameManager.Instance.IncreaseHealth(1);
                Destroy(other.gameObject);
                break;
            case "Coin":
                GameManager.Instance.IncreaseScore(10);
                Destroy(other.gameObject);
                break;
            case "Enemy":
                if (!GameManager.Instance.isBoosting)
                {
                    GameManager.Instance.IncreaseHealth(-1);
                    Destroy(other.gameObject);
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wave"))
        {
            isInWave = false;
            Debug.Log("Surfer exited the wave.");
        }
    }
}
