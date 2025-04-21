using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [Tooltip("Массив кораблей-врагов для спавна")]
    public GameObject[] shipEnemyPrefabs;

    [Header("Trash Settings")]
    [Tooltip("Массив объектов мусора для спавна")]
    public GameObject[] trashPrefabs;

    [Header("Spawn Settings")]
    [Range(0f, 1f)] public float trashSpawnChance = 0.3f; // 0 = только враги, 1 = только мусор
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;
    public float minScale = 0.5f;
    public float maxScale = 1.5f;

    [Header("Movement Settings")]
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float speedIncreaseRate = 0.05f;

    private float[] railX;
    private float topY;
    private float initialCameraSize;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        initialCameraSize = mainCamera.orthographicSize;
        CalculateSpawnBoundaries();
        StartCoroutine(SpawnObjectsRoutine());
    }

    void CalculateSpawnBoundaries()
    {
        float aspect = mainCamera.aspect;
        float leftX = mainCamera.transform.position.x - aspect * initialCameraSize;
        float rightX = mainCamera.transform.position.x + aspect * initialCameraSize;
        topY = mainCamera.transform.position.y + initialCameraSize;

        railX = new float[5];
        for (int i = 0; i < 5; i++)
        {
            railX[i] = leftX + (rightX - leftX) * (i + 0.5f) / 5f;
        }
    }

    IEnumerator SpawnObjectsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));

            // Выбор позиции спавна
            int railIndex = Random.Range(0, 5);
            Vector3 spawnPosition = new Vector3(railX[railIndex], topY - 1f, 0);

<<<<<<< Updated upstream
<<<<<<< Updated upstream
            // Позиция спавна: X — позиция рельса, Y — верх экрана
            Vector3 spawnPosition = new Vector3(x, topY, 0);
=======
            // Определяем тип объекта для спавна
            bool isTrash = Random.value < trashSpawnChance;
            GameObject[] targetArray = isTrash ? trashPrefabs : shipEnemyPrefabs;
>>>>>>> Stashed changes
=======
            // Определяем тип объекта для спавна
            bool isTrash = Random.value < trashSpawnChance;
            GameObject[] targetArray = isTrash ? trashPrefabs : shipEnemyPrefabs;
>>>>>>> Stashed changes

            // Проверка наличия префабов
            //if (targetArray == null || targetArray.Length == 0)
            //{
            //    Debug.LogWarning($"No prefabs in {(isTrash ? "trash" : "enemy")} array!");
            //    continue;
            //}

            // Выбор случайного префаба
            GameObject prefabToSpawn = targetArray[Random.Range(0, targetArray.Length)];

            // Создание объекта
            GameObject newObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            // Настройка параметров
            ConfigureSpawnedObject(newObject, isTrash);
        }
    }

    void ConfigureSpawnedObject(GameObject obj, bool isTrash)
    {
        // Установка случайного масштаба
        float randomScale = Random.Range(minScale, maxScale);
        obj.transform.localScale = new Vector3(randomScale, randomScale, 1f);

        // Настройка скорости
        float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, Time.time * speedIncreaseRate);

        if (isTrash)
        {
            // Для мусора простая логика движения
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb) rb.velocity = Vector2.down * currentSpeed;
        }
        else
        {
            // Для врагов используем существующую логику
            Enemy enemy = obj.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.speed = currentSpeed;
                enemy.isRectangular = enemy.CompareTag("Ship");
                // Получаем SpriteRenderer
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    // Определяем половину экрана
                    float spawnX = obj.transform.position.x;
                    float screenCenterX = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0)).x;

                    // Отражаем спрайт если спавн в правой половине
                    sr.flipX = spawnX > screenCenterX;
                }
            }
        }
    }

    void Update()
    {
        // Пересчет границ при изменении размера камеры
        if (mainCamera.orthographicSize != initialCameraSize)
        {
            initialCameraSize = mainCamera.orthographicSize;
            CalculateSpawnBoundaries();
        }
    }
}