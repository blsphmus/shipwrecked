using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Reference to the UI Text for displaying score
    public Text highScoreText; // ����� ��� �������
    public float pointsPerSecond = 10f; // Points awarded per second of survival

    private float totalScore = 0f; // Total score
    private int highScore = 0;
    private bool isScoring = true; // Flag to control scoring
    private bool isNewHighScore = false; // ���� ������ �������

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0); // ��������� ������
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
        // ��������� ������ ��� ���������� ����
        if (totalScore > highScore)
        {
            highScore = Mathf.FloorToInt(totalScore);
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            isNewHighScore = true; // ������������� ����
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
            highScoreText.text = "������: " + totalScore.ToString();
        }
        if (highScoreText != null)
        {
            highScoreText.text = "������: " + highScore.ToString();
        }
    }

    public bool IsNewHighScore() // ����������, ��� �� ���������� ����� ������
    {
        return isNewHighScore;
    }

    // ����� ������� !! ��� ������������ �������� ������ � ���� ������� / �������� ��� ���-������
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore = 0;
        UpdateHighScoreDisplay();
    }
}