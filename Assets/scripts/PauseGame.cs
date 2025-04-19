using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private bool isPaused = false;

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
        }
        else
        {
            Time.timeScale = 1f; // ������������ ���������� �������� �������
            AudioListener.pause = false; // �������� ����� �������
        }
    }
}