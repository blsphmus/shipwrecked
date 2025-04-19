using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public Text scoreText; // UI Text to display final score

    void Start()
    {
        gameObject.SetActive(false); // Hide menu initially
    }

    public void SetScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Набранные очки: " + score;
        }
    }

    public void RestartGame()
    {
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
        Time.timeScale = 1f; // Resume time for main menu
        SceneManager.LoadScene("Start_Menu"); // Load main menu scene
    }
}