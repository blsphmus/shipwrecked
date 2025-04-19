using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Reference to the UI Text for displaying score
    public float pointsPerSecond = 10f; // Points awarded per second of survival
    private float totalScore = 0f; // Total score
    private bool isScoring = true; // Flag to control scoring

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
    }

    public int GetFinalScore()
    {
        return Mathf.FloorToInt(totalScore);
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Очки: " + Mathf.FloorToInt(totalScore).ToString();
        }
    }
}