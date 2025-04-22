using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Reference to the UI Text for displaying score
    public Text highScoreText; // Текст для рекорда
    public float pointsPerSecond = 10f; // Points awarded per second of survival

    private float totalScore = 0f; // Total score
    private int highScore = 0;
    private bool isScoring = true; // Flag to control scoring
    private bool isNewHighScore = false; // Флаг нового рекорда

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0); // Загружаем рекорд
        UpdateHighScoreDisplay();
    }

    void Update()
    {
        if (isScoring)
        {
            // Add points based on time
            totalScore += pointsPerSecond * Time.deltaTime;
            UpdateScoreDisplay();
        }
    }

    public void AddPoints(int points)
    {
        if (isScoring)
        {
            // Add points for enemy destruction or other events
            totalScore += points;
            UpdateScoreDisplay();
        }
    }

    public void StopScoring()
    {
        isScoring = false;
        // Сохраняем рекорд при завершении игры
        if (totalScore > highScore)
        {
            highScore = Mathf.FloorToInt(totalScore);
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            isNewHighScore = true; // Устанавливаем флаг
        }
    }

    public int GetFinalScore()
    {
        return Mathf.FloorToInt(totalScore);
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = Mathf.FloorToInt(totalScore).ToString();
        }
    }

    void UpdateHighScoreDisplay()
    {
        if (highScore == 0)
        {
            highScoreText.text = "Рекорд: " + totalScore.ToString();
        }
        if (highScoreText != null)
        {
            highScoreText.text = "Рекорд: " + highScore.ToString();
        }
    }

    public bool IsNewHighScore() // Возвращает, был ли установлен новый рекорд
    {
        return isNewHighScore;
    }

    // Сброс рекорда !! Для тестирования добавьте кнопку с этим методом / вызовите его где-нибудь
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore = 0;
        UpdateHighScoreDisplay();
    }
}