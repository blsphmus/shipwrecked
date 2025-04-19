using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 2f; // Скорость врага
    public bool isRectangular = false; // Флаг для прямоугольных врагов
    public GameObject bulletPrefab; // Префаб пули (только для прямоугольных врагов)
    public float shootInterval = 2f; // Интервал стрельбы в секундах
    public int health = 1; // Здоровье врага
    private float bottomY; // Нижняя граница экрана
    private GameObject player; // Ссылка на игрока

    void Start()
    {
        // Получаем главную камеру и вычисляем нижнюю границу
        Camera camera = Camera.main;
        bottomY = camera.transform.position.y - camera.orthographicSize;

        // Находим игрока по тегу
        if (isRectangular)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                StartCoroutine(ShootRoutine());
            }
        }
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

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            ShootAtPlayer();
            yield return new WaitForSeconds(shootInterval);
        }
    }

    void ShootAtPlayer()
    {
        // Создаем пулю в позиции врага
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        
        // Вычисляем направление к игроку
        Vector2 direction = (player.transform.position - transform.position).normalized;
        
        // Устанавливаем направление пули, указываем, что пуля от врага
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction, false); // false = пуля от врага
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}