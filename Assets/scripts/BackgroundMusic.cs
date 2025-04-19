using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;

    [Header("Music Settings")]
    public AudioClip musicTrack; // Перетащите сюда ваш аудиофайл
    [Range(0, 1)] public float volume = 0.5f;

    private AudioSource audioSource;

    void Awake()
    {
        // Реализация паттерна Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Не уничтожать при загрузке новых сцен

            // Настройка AudioSource
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = musicTrack;
            audioSource.volume = volume;
            audioSource.loop = true; // Бесконечное зацикливание
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject); // Уничтожить дубликат
        }
    }

    // Метод для изменения громкости
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        audioSource.volume = volume;
    }

    // Метод для паузы/возобновления
    public void TogglePause()
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
        else
            audioSource.UnPause();
    }
}