using TMPro;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    // Configuration Variables
    [Range(0.5f, 5f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 50;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;

    // State Variables
    int currentScore = 0;

    private void Awake()
    {
        int countGameStatus = FindObjectsOfType<GameStatus>().Length;
        if (countGameStatus > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore(int blockHitMax)
    {
        currentScore += pointsPerBlockDestroyed * blockHitMax;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    public void SwitchAutoPlay()
    {
        isAutoPlayEnabled = !isAutoPlayEnabled;
    }
}
