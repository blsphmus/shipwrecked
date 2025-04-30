using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public TMP_Text coinsText;
    //public Button BuyCoins;

    private int currentCoins;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Подписываемся на событие загрузки сцены
            LoadCoins();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        coinsText = GameObject.Find("TextCoins").GetComponent<TMP_Text>(); // Находим текстовый элемент по тегу или имени
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        GameData.PlayerCoins += amount;
        UpdateUI();
        GameData.Save();
    }

    public void BuyCoins()
    {
        GameData.PlayerCoins += 1000;
        UpdateUI();
        GameData.Save();
    }

    private void LoadCoins()
    {
        GameData.Load();
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (coinsText != null)
        {
            coinsText.text = GameData.PlayerCoins.ToString();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Отписываемся от события
    }
}
