using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Ссылка на префаб врага
    public float minSpawnInterval = 1f; // Минимальный интервал спавна (в секундах)
    public float maxSpawnInterval = 3f; // Максимальный интервал спавна (в секундах)
    public float initialSpeed = 2f; // Начальная скорость врагов
    public float speedIncreaseRate = 0.1f; // Скорость увеличения скорости врагов в секунду
    public float maxSpeed = 5f; // Максимальная скорость врагов
    public float minScale = 0.5f; // Минимальный масштаб врага
    public float maxScale = 1.5f; // Максимальный масштаб врага
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
            // Вычисляем текущую скорость
            float currentSpeed = Mathf.Min(initialSpeed + speedIncreaseRate * Time.time, maxSpeed);

            // Выбираем случайный рельс
            int railIndex = Random.Range(0, 5);
            float x = railX[railIndex];

            // Позиция спавна: X — позиция рельса, Y — верх экрана
            Vector3 spawnPosition = new Vector3(x, topY, 0);

            // Создаём врага
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Устанавливаем случайный масштаб
            float randomScale = Random.Range(minScale, maxScale);
            newEnemy.transform.localScale = new Vector3(randomScale, randomScale, 1f);

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