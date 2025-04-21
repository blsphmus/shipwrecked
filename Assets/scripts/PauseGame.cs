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
        // ����� �� ������� (�����������)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // ���������� ��� ������� ������ � UI
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // ������������� ����� � ����
            AudioListener.pause = true; // ���������������� ��� �����
            pausePan.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // ������������ ���������� �������� �������
            AudioListener.pause = false; // �������� ����� �������
            pausePan.SetActive(false);
        }
    }
}