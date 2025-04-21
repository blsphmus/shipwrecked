using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject pausePan;

    private bool isPaused = false;

    private void Start()
    {
        pausePan.SetActive(false);
    }

    void Update()
    {
        // Пауза по клавише (опционально)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Вызывается при нажатии кнопки в UI
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Останавливаем время в игре
            AudioListener.pause = true; // Приостанавливаем все звуки
            pausePan.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // Возобновляем нормальную скорость времени
            AudioListener.pause = false; // Включаем звуки обратно
            pausePan.SetActive(false);
        }
    }
}