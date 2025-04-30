using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{

    // хз крч как это грамотно сделать, работает, но я не бум бум как волны в белый покрасить, они ж синие, на них 
    // белый лежит как их родной цвет, а ещё почему-то до чёрного не доходит


    //public Camera mainCamera;
    //public ParticleSystem particles;
    //public ScoreManager scoreManager;

    //// Цвета для разных уровней очков
    //private Color[] targetColors = {
    //    new Color(0.3f, 0.6f, 0.8f),    // Голубой (0)
    //    new Color(0.3f, 0.9f, 0.7f),    // Бирюзовый (4000)
    //    new Color(0.2f, 0.3f, 0.9f),    // Синий (8000)
    //    new Color(0.5f, 0.2f, 0.9f),    // Фиолетовый (12000)
    //    new Color(0.1f, 0.1f, 0.15f)     // Чёрный (18000)
    //};

    //private Color[] targetColorsWaves = {
    //    new Color(1f, 1f, 1f),    // Голубой (0)
    //    new Color(0.2f, 1f, 0.7f),    // Бирюзовый (4000)
    //    new Color(0.4f, 0.5f, 0.7f),    // Синий (8000)
    //    new Color(0.6f, 0.5f, 0.7f),    // Фиолетовый (12000)
    //    Color.white                     // Чёрный (18000)
    //};

    //// Пороговые значения очков для смены цвета
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

    //    // Автоматически находим основную камеру если не задана
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
    //    // Находим текущий диапазон
    //    for (int i = 0; i < scoreThresholds.Length - 1; i++)
    //    {
    //        if (score >= scoreThresholds[i] && score <= scoreThresholds[i + 1])
    //        {
    //            float t = Mathf.InverseLerp(
    //                scoreThresholds[i],
    //                scoreThresholds[i + 1],
    //                score
    //            );

    //            // Интерполяция цвета
    //            Color newColor = Color.Lerp(
    //                targetColors[i],
    //                targetColors[i + 1],t
    //            );

    //            Color newColorWave = Color.Lerp(
    //                targetColorsWaves[i],
    //                targetColorsWaves[i + 1], t
    //            );

    //            // Применяем цвет к камере
    //            if (mainCamera != null)
    //                mainCamera.backgroundColor = newColor;

    //            // Применяем цвет к частицам
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
