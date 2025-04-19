using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveShaking : MonoBehaviour
{
    private float speed;
    private float shakeAmount;
    private float shakeFrequency;
    private float bottomY;

    private Vector3 basePosition;

    public void Initialize(float speed, float shakeAmount, float shakeFrequency, float bottomY)
    {
        this.speed = speed;
        this.shakeAmount = shakeAmount;
        this.shakeFrequency = shakeFrequency;
        this.bottomY = bottomY;
        basePosition = transform.position;
    }

    void Update()
    {
        // Основное движение вниз
        basePosition += Vector3.down * speed * Time.deltaTime;

        // Тряска волны
        float shakeOffset = Mathf.Sin(Time.time * shakeFrequency) * shakeAmount;
        transform.position = basePosition + new Vector3(shakeOffset, 0, 0);

        // Уничтожение за границами экрана
        if (transform.position.y < bottomY - 1f)
        {
            Destroy(gameObject);
        }
    }
}
