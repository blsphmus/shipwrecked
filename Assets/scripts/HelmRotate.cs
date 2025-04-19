using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmRotate : MonoBehaviour
{
    [Header("Joystick Reference")]
    public Joystick joystick; // Перетащите сюда ваш джойстик

    [Header("Rotation Settings")]
    public float rotationSpeed = 5f; // Скорость поворота
    public float maxRotationAngle = 45f; // Максимальный угол поворота

    private Vector2 inputDirection;
    private float currentRotation;

    void Update()
    {
        // Получаем ввод с джойстика
        inputDirection = new Vector2(joystick.Horizontal, 0);

        if (inputDirection.x != 0)
        {
            // Вычисляем целевой угол поворота
            float targetRotation = -inputDirection.x * maxRotationAngle;

            // Плавно интерполируем текущий угол к целевому
            currentRotation = Mathf.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Применяем поворот
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
        }
        else
        {
            // Плавный возврат в исходное положение
            currentRotation = Mathf.Lerp(currentRotation, 0f, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
        }
    }
}
