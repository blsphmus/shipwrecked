using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб пули (только для прямоугольных врагов)
    public GameObject destroyEffect;
    public float speed = 2f; // Скорость врага
    public bool isRectangular = false; // Флаг для прямоугольных врагов
    public float shootInterval = 2f; // Интервал стрельбы в секундах
    public int maxHealth = 3; // Максимальное здоровье врага
    public int collisionDamage = 20; // Урон при столкновении с игроком
    public int pointsOnDestroy = 100; // Очки за уничтожение врага
    private int currentHealth; // Текущее здоровье врага
    public Image healthBar; // Ссылка на UI-полоску здоровья (только для прямоугольных врагов)

    private GameObject player; // Ссылка на игрока
    private ScoreManager scoreManager; // Ссылка на ScoreManager
    private SpriteRenderer spriteRenderer; // Ссылка на SpriteRenderer
    private float bottomY; // Нижняя граница экрана

    void Start()
    {
        // Получаем главную камеру и вычисляем нижнюю границу
        Camera camera = Camera.main;
        bottomY = camera.transform.position.y - camera.orthographicSize;

        // Инициализируем здоровье
        currentHealth = maxHealth;
        UpdateHealthBar();
        
        scoreManager = FindObjectOfType<ScoreManager>(); // Находим ScoreManager
        spriteRenderer = GetComponent<SpriteRenderer>(); // Находим SpriteRenderer

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
        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            if (scoreManager != null)
            {
                scoreManager.AddPoints(pointsOnDestroy); // Начисляем очки за уничтожение
            }
            Destroy(gameObject);
            GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Если враг столкнулся с игроком
        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(collisionDamage); // Наносим урон игроку
            }
            if (scoreManager != null)
            {
                scoreManager.AddPoints(pointsOnDestroy); // Начисляем очки за уничтожение
            }
            Destroy(gameObject); // Уничтожаем врага при столкновении
            GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
}
