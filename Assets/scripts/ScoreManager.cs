using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TMP_Text scoreText; // Reference to the UI Text for displaying score
    public TMP_Text highScoreText; // Текст для рекорда
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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadHighScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Обновляем ссылки на UI элементы при загрузке сцены
        totalScore = 0f;
        scoreText = GameObject.FindGameObjectWithTag("ScoreText")?.GetComponent<TMP_Text>();
        highScoreText = GameObject.FindGameObjectWithTag("HighScoreText")?.GetComponent<TMP_Text>();

        UpdateAllDisplays();
    }

    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
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
            totalScore += points; // Add points for enemy destruction or other events
            UpdateScoreDisplay();
        }
    }

    public void StopScoring()
    {
        isScoring = false;
        if (totalScore > highScore)
        {
            highScore = Mathf.FloorToInt(totalScore);
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            isNewHighScore = true;
        }
        UpdateAllDisplays();
    }

    void UpdateAllDisplays()
    {
        UpdateScoreDisplay();
        UpdateHighScoreDisplay();
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

    public void ResetScore()
    {
        totalScore = 0f;
        isScoring = true;
        isNewHighScore = false;
        UpdateAllDisplays();
    }
}