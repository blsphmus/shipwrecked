using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject defaultEnemyPrefab; // Префаб дефолтного врага
    public GameObject rectangularEnemyPrefab; // Префаб прямоугольного врага
    public float minSpawnInterval = 1f; // Минимальный интервал спавна (в секундах)
    public float maxSpawnInterval = 3f; // Максимальный интервал спавна (в секундах)
    public float defaultInitialSpeed = 2f; // Начальная скорость дефолтных врагов
    public float defaultSpeedIncreaseRate = 0.1f; // Скорость увеличения скорости дефолтных врагов в секунду
    public float defaultMaxSpeed = 5f; // Максимальная скорость дефолтных врагов
    public float rectangularInitialSpeed = 1.5f; // Начальная скорость прямоугольных врагов
    public float rectangularSpeedIncreaseRate = 0.05f; // Скорость увеличения скорости прямоугольных врагов в секунду
    public float rectangularMaxSpeed = 4f; // Максимальная скорость прямоугольных врагов
    public float rectangularEnemySpawnChance = 0.2f; // Вероятность спавна прямоугольного врага (20%)
    public float minScale = 0.5f; // Минимальный масштаб дефолтного врага
    public float maxScale = 1.5f; // Максимальный масштаб дефолтного врага
    private float[] railX; // Массив позиций рельсов по X
    private float topY; // Верхняя граница экрана

    void Start()
    {
        // Получаем главную камеру
        Camera camera = Camera.main;
        float size = camera.orthographicSize; // Половина высоты экрана
        float aspect = camera.aspect; // Соотношение сторон

        // Вычисляем левую и правую границы экрана
        float leftX = camera.transform.position.x - aspect * size;
        float rightX = camera.transform.position.x + aspect * size;

        // Верхняя граница экрана
        topY = camera.transform.position.y + size;

        // Инициализируем массив позиций рельсов
        railX = new float[5];
        for (int i = 0; i < 5; i++)
        {
            // Равномерно распределяем рельсы между leftX и rightX
            railX[i] = leftX + (rightX - leftX) * (i + 0.5f) / 5f;
        }

        // Запускаем корутину для спавна врагов
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Выбираем случайный рельс
            int railIndex = Random.Range(0, 5);
            float x = railX[railIndex];

            // Позиция спавна: X — позиция рельса, Y — верх экрана
            Vector3 spawnPosition = new Vector3(x, topY, 0);

            // Определяем, какой враг будет заспавнен
            GameObject enemyPrefabToSpawn;
            float currentSpeed;
            float spawnRoll = Random.value;
            if (spawnRoll < rectangularEnemySpawnChance)
            {
                enemyPrefabToSpawn = rectangularEnemyPrefab;
                currentSpeed = Mathf.Min(rectangularInitialSpeed + rectangularSpeedIncreaseRate * Time.time, rectangularMaxSpeed);
            }
            else
            {
                enemyPrefabToSpawn = defaultEnemyPrefab;
                currentSpeed = Mathf.Min(defaultInitialSpeed + defaultSpeedIncreaseRate * Time.time, defaultMaxSpeed);
            }

            // Создаём врага
            GameObject newEnemy = Instantiate(enemyPrefabToSpawn, spawnPosition, Quaternion.identity);

            // Устанавливаем случайный масштаб (только для дефолтных врагов)
            if (enemyPrefabToSpawn == defaultEnemyPrefab)
            {
                float randomScale = Random.Range(minScale, maxScale);
                newEnemy.transform.localScale = new Vector3(randomScale, randomScale, 1f);
            }

            // Устанавливаем скорость врага
            Enemy enemyScript = newEnemy.GetComponent<Enemy>();
            enemyScript.speed = currentSpeed;

            // Генерируем случайный интервал для следующего спавна
            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

            // Ждём случайное время перед следующим спавном
            yield return new WaitForSeconds(randomInterval);
        }
    }
}