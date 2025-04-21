using System.Diagnostics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffectPrefab; // Префаб Particle System
    public float speed = 1f; // Скорость пули
    //public bool isPlayerBullet; // Флаг, указывающий, от игрока ли пуля

    //private Rigidbody2D rb;
    private Vector2 direction; // Направление движения
    private bool isFromPlayer; // Флаг, указывающий, от игрока ли пуля

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 dir, bool fromPlayer = false)
    {
        direction = dir.normalized; // Нормализуем направление
        isFromPlayer = fromPlayer; // Устанавливаем, от кого пуля
    }

    void Update()
    {
        // Двигаем пулю в заданном направлении
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        //UnityEngine.Debug.DrawRay(transform.position, direction * 0.5f, Color.red); // Рисует луч направления

        // Уничтожаем пулю, если она вышла за пределы экрана
        if (transform.position.magnitude > 100f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (isFromPlayer && collision.CompareTag("Ship")) // Если пуля от игрока и столкнулась с врагом
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null) enemy.TakeDamage(1); // Наносим урон врагу
            Destroy(gameObject); // Уничтожаем пулю
            Vector2 hitPosition = GetCollisionPosition(collision); // Получаем позицию эффекта
            GameObject effect = Instantiate(hitEffectPrefab, hitPosition, Quaternion.identity);
            Destroy(effect, 1f);
        }
        else if (!isFromPlayer && collision.CompareTag("Player")) // Если пуля от врага и столкнулась с игроком
        {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            Destroy(gameObject); // Уничтожаем пулю
            // Здесь можно добавить логику урона игроку, если нужно
=======
            Player player = collision.GetComponent<Player>();
            if (player != null) player.TakeDamage(10); // Наносим урон игроку (например, 10 единиц)
            Destroy(gameObject); // Уничтожаем пулю
            Vector2 hitPosition = GetCollisionPosition(collision); // Получаем позицию эффекта
            GameObject effect = Instantiate(hitEffectPrefab, hitPosition, Quaternion.identity);
            Destroy(effect, 1f);
>>>>>>> Stashed changes
=======
            Player player = collision.GetComponent<Player>();
            if (player != null) player.TakeDamage(10); // Наносим урон игроку (например, 10 единиц)
            Destroy(gameObject); // Уничтожаем пулю
            Vector2 hitPosition = GetCollisionPosition(collision); // Получаем позицию эффекта
            GameObject effect = Instantiate(hitEffectPrefab, hitPosition, Quaternion.identity);
            Destroy(effect, 1f);
>>>>>>> Stashed changes
=======
            Player player = collision.GetComponent<Player>();
            if (player != null) player.TakeDamage(10); // Наносим урон игроку (например, 10 единиц)
            Destroy(gameObject); // Уничтожаем пулю
            Vector2 hitPosition = GetCollisionPosition(collision); // Получаем позицию эффекта
            GameObject effect = Instantiate(hitEffectPrefab, hitPosition, Quaternion.identity);
            Destroy(effect, 1f);
>>>>>>> Stashed changes
        }

        

        //if (isPlayerBullet && collision.CompareTag("Enemy"))
        //{
        //    // Íàíåñòè óðîí âðàãó (äîáàâüòå ñêðèïò Enemy ñ ìåòîäîì TakeDamage)
        //    Enemy enemy = collision.GetComponent<Enemy>();
        //    if (enemy != null) enemy.TakeDamage(1);
        //    Destroy(gameObject);
        //}
        //else if (!isPlayerBullet && collision.CompareTag("Player"))
        //{
        //    Player player = collision.GetComponent<Player>();
        //    if (player != null) player.TakeDamage(1);
        //    Destroy(gameObject);
        //}
    }

    // Метод для получения позиции эффекта
    private Vector2 GetCollisionPosition(Collider2D collider)
    {
        // Вариант 1: Центр коллайдера
        return collider.bounds.center;

        // Вариант 2: Позиция пули (если пуля маленькая)
        //return transform.position;
    }
}

//example