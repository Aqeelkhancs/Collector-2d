using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject winPanel;

    [Header("Game Settings")]
    public int targetScore = 5;      // total coins needed to win (set to your number of coins)

    private int score = 0;
    private bool isGameOver = false;
    private bool hasWon = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Hide end‑game panels at start
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void AddScore(int amount)
    {
        if (isGameOver || hasWon) return;
        score += amount;
        scoreText.text = "Score: " + score;

        // Check win condition
        if (score >= targetScore)
            WinGame();
    }

    public void WinGame()
    {
        if (hasWon || isGameOver) return;
        hasWon = true;
        winPanel.SetActive(true);
        Time.timeScale = 0f;  // freeze the game
        Debug.Log("You Win!");
    }

    public void GameOver()
    {
        if (isGameOver || hasWon) return;
        isGameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Game Over");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}