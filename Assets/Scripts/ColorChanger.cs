using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{

    // �� ��� ��� ��� �������� �������, ��������, �� � �� ��� ��� ��� ����� � ����� ���������, ��� � �����, �� ��� 
    // ����� ����� ��� �� ������ ����, � ��� ������-�� �� ������� �� �������


    //public Camera mainCamera;
    //public ParticleSystem particles;
    //public ScoreManager scoreManager;

    //// ����� ��� ������ ������� �����
    //private Color[] targetColors = {
    //    new Color(0.3f, 0.6f, 0.8f),    // ������� (0)
    //    new Color(0.3f, 0.9f, 0.7f),    // ��������� (4000)
    //    new Color(0.2f, 0.3f, 0.9f),    // ����� (8000)
    //    new Color(0.5f, 0.2f, 0.9f),    // ���������� (12000)
    //    new Color(0.1f, 0.1f, 0.15f)     // ׸���� (18000)
    //};

    //private Color[] targetColorsWaves = {
    //    new Color(1f, 1f, 1f),    // ������� (0)
    //    new Color(0.2f, 1f, 0.7f),    // ��������� (4000)
    //    new Color(0.4f, 0.5f, 0.7f),    // ����� (8000)
    //    new Color(0.6f, 0.5f, 0.7f),    // ���������� (12000)
    //    Color.white                     // ׸���� (18000)
    //};

    //// ��������� �������� ����� ��� ����� �����
    ////private int[] scoreThresholds = { 0, 100, 200, 300, 400 }; 
    //private int[] scoreThresholds = { 0, 5000, 10000, 15000, 20000 };
    //private void Start()
    //{
    //    GameObject scoreManagerObject = GameObject.FindWithTag("ScoreManagerTag");
    //    if (scoreManagerObject != null)
    //    {
    //        scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
    //    }
    //    else
    //    {
    //        Debug.LogError("ScoreManager object with tag 'ScoreManager' not found!");
    //    }

    //    // ������������� ������� �������� ������ ���� �� ������
    //    if (mainCamera == null)
    //    {
    //        mainCamera = Camera.main;
    //    }
    //}

    //void Update()
    //{
    //    if (scoreManager == null) return;

    //    float currentScore = scoreManager.GetFinalScore();
    //    UpdateColors(currentScore);
    //}

    //void UpdateColors(float score)
    //{
    //    // ������� ������� ��������
    //    for (int i = 0; i < scoreThresholds.Length - 1; i++)
    //    {
    //        if (score >= scoreThresholds[i] && score <= scoreThresholds[i + 1])
    //        {
    //            float t = Mathf.InverseLerp(
    //                scoreThresholds[i],
    //                scoreThresholds[i + 1],
    //                score
    //            );

    //            // ������������ �����
    //            Color newColor = Color.Lerp(
    //                targetColors[i],
    //                targetColors[i + 1],t
    //            );

    //            Color newColorWave = Color.Lerp(
    //                targetColorsWaves[i],
    //                targetColorsWaves[i + 1], t
    //            );

    //            // ��������� ���� � ������
    //            if (mainCamera != null)
    //                mainCamera.backgroundColor = newColor;

    //            // ��������� ���� � ��������
    //            if (particles != null)
    //            {
    //                var main = particles.main;
    //                main.startColor = newColorWave;
    //            }
    //            break;
    //        }
    //    }
    //}
}
