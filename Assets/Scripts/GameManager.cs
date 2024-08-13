using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject boosterOnImage;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Wave wave;
    public GameState currentState;
    private bool isPaused = true;
    public bool isBoosting = false;
    private int healthCount;
    private int maxHealthCount=3;
    private int score;
    public float speed;
    public float originalSpeed =5f;
    private float boosterDuration = 7f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        speed = originalSpeed;
    }
    void Start()
    {

    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        healthCount=maxHealthCount;
        score=0;
        UpdateUI();
        currentState = GameState.Playing;
        isPaused=false;
    }

    void UpdateUI()
    {
        healthText.text=healthCount.ToString();
        coinText.text=score.ToString();
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
        coinText.text=score.ToString();
    }

    public void IncreaseHealth(int value)
    {
        healthCount+=value;
        if(healthCount>maxHealthCount)
        {
            healthCount=maxHealthCount;
        }
        healthText.text=healthCount.ToString();
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if(healthCount<=0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        gameOverPanel.SetActive(true);
    }

    public void ActivateBooster(GameObject boosterObject)
    {
        isBoosting = true;
        boosterOnImage.SetActive(isBoosting);
        Destroy(boosterObject);

        IncreaseSpeed(5f);
        Invoke(nameof(DeactivateBooster), boosterDuration);
    }

    private void IncreaseSpeed(float multiplier)
    {
        speed = originalSpeed * multiplier;
    }
    private void DeactivateBooster()
    {
        isBoosting = false;
        boosterOnImage.SetActive(isBoosting);
        StartCoroutine(GraduallyReduceSpeed());
    }

    private IEnumerator GraduallyReduceSpeed()
    {
        float currentSpeed = speed;
        float reductionTime = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < reductionTime)
        {
            elapsedTime += Time.deltaTime;
            speed = Mathf.Lerp(currentSpeed, originalSpeed, elapsedTime / reductionTime);
            yield return null; 
        }

        speed = originalSpeed;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            pausePanel.SetActive(isPaused);
            PauseGame();
        }
    }
    private void PauseGame()
    {
        if(isPaused)
        {
            currentState = GameState.Pause;
            Time.timeScale = 0f;
        }
        else 
        {
            currentState = GameState.Playing;
            Time.timeScale = 1;
        }
    }
    public void RestartGame()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
