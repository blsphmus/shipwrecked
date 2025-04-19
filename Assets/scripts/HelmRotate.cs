using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmRotate : MonoBehaviour
{
    [Header("Joystick Reference")]
    public Joystick joystick; // ���������� ���� ��� ��������

    [Header("Rotation Settings")]
    public float rotationSpeed = 5f; // �������� ��������
    public float maxRotationAngle = 45f; // ������������ ���� ��������

    private Vector2 inputDirection;
    private float currentRotation;

    void Update()
    {
        // �������� ���� � ���������
        inputDirection = new Vector2(joystick.Horizontal, 0);

        if (inputDirection.x != 0)
        {
            // ��������� ������� ���� ��������
            float targetRotation = -inputDirection.x * maxRotationAngle;

            // ������ ������������� ������� ���� � ��������
            currentRotation = Mathf.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

            // ��������� �������
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
        }
        else
        {
            // ������� ������� � �������� ���������
            currentRotation = Mathf.Lerp(currentRotation, 0f, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);
        }
    }
}
