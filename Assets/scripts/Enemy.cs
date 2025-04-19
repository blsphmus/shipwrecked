using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f; // Скорость врага
    private float bottomY; // Нижняя граница экрана

    void Start()
    {
        // Получаем главную камеру и вычисляем нижнюю границу
        Camera camera = Camera.main;
        bottomY = camera.transform.position.y - camera.orthographicSize;
    }

    void Update()
    {
        // Двигаем врага вниз
        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);

        // Уничтожаем врага, если он ушёл за нижнюю границу
        if (transform.position.y < bottomY)
        {
            Destroy(gameObject);
        }
    }
}