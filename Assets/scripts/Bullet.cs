using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // Скорость пули
    private Vector2 direction; // Направление движения
    private bool isFromPlayer; // Флаг, указывающий, от игрока ли пуля

    public void SetDirection(Vector2 dir, bool fromPlayer = false)
    {
        direction = dir.normalized; // Нормализуем направление
        isFromPlayer = fromPlayer; // Устанавливаем, от кого пуля
    }

    void Update()
    {
        // Двигаем пулю в заданном направлении
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Уничтожаем пулю, если она вышла за пределы экрана
        if (transform.position.magnitude > 10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Если пуля от игрока и столкнулась с врагом
        if (isFromPlayer && collision.CompareTag("Ship"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // Наносим урон врагу
            }
            Destroy(gameObject); // Уничтожаем пулю
        }
        // Если пуля от врага и столкнулась с игроком
        else if (!isFromPlayer && collision.CompareTag("Player"))
        {
            Destroy(gameObject); // Уничтожаем пулю
            // Здесь можно добавить логику урона игроку, если нужно
        }
    }
}

//example