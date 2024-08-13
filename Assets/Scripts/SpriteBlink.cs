using System.Collections;
using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float blinkInterval = 0.5f; 

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(GameManager.Instance.isBoosting)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            new WaitForSeconds(blinkInterval);
        }
        else{spriteRenderer.enabled = true;}
    }
}