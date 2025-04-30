using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public Text scoreText; // UI Text to display final score
    public Text newHighScoreText; // Новый UI-текст для сообщения
    public ScoreManager scoreManager; // Ссылка на ScoreManager


    void Start()
    {
        gameObject.SetActive(false); // Hide menu initially
        newHighScoreText.gameObject.SetActive(false); // Скрываем сообщение
    }

    public void SetScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Набранные очки: " + score;
        }

        if (scoreManager != null && scoreManager.IsNewHighScore())
        {
            newHighScoreText.gameObject.SetActive(true); // Показываем сообщение
        }
    }

    public void RestartGame()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }

        Time.timeScale = 1f; // Resume game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit application
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in Editor
        #endif
    }

    public void GoToMainMenu()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }

        Time.timeScale = 1f; // Resume time for main menu
        SceneManager.LoadScene("MENU"); // Load main menu scene
    }
}