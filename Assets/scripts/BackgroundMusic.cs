using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;

    [Header("Music Settings")]
    public AudioClip musicTrack; // ���������� ���� ��� ���������
    [Range(0, 1)] public float volume = 0.5f;

    private AudioSource audioSource;

    void Awake()
    {
        // ���������� �������� Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ���������� ��� �������� ����� ����

            // ��������� AudioSource
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = musicTrack;
            audioSource.volume = volume;
            audioSource.loop = true; // ����������� ������������
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject); // ���������� ��������
        }
    }

    // ����� ��� ��������� ���������
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        audioSource.volume = volume;
    }

    // ����� ��� �����/�������������
    public void TogglePause()
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
        else
            audioSource.UnPause();
    }
}