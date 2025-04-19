using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    public List<GameObject> wavePrefabs;
    public float spawnInterval = 0f;
    public float waveSpeed = 3f;

    [Header("Shake Settings")]
    public float shakeAmount = 0.01f;
    public float shakeFrequency = 10f;

    private float topY;
    private float bottomY; // ������ ������� ������
    private float fixedX = 0f;

    void Start()
    {
        CalculateScreenBounds();
        StartCoroutine(SpawnWaves());
    }

    void CalculateScreenBounds()
    {
        Camera camera = Camera.main;
        topY = camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        bottomY = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            SpawnSingleWave();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnSingleWave()
    {
        GameObject wavePrefab = wavePrefabs[Random.Range(0, wavePrefabs.Count)];
        Vector3 spawnPosition = new Vector3(fixedX, topY + 1f, 0);
        GameObject newWave = Instantiate(wavePrefab, spawnPosition, Quaternion.identity);

        WaveMover mover = newWave.AddComponent<WaveMover>();
        mover.speed = waveSpeed;
        mover.shakeAmount = shakeAmount;
        mover.shakeFrequency = shakeFrequency;
        mover.bottomY = bottomY; // �������� ������ �������
    }
}

public class WaveMover : MonoBehaviour
{
    public float speed;
    public float shakeAmount;
    public float shakeFrequency;
    public float bottomY; // ������ ������� ������

    private Vector3 basePosition;

    void Start()
    {
        basePosition = transform.position;
    }

    void Update()
    {
        // �������� �������� ����
        basePosition += Vector3.down * speed * Time.deltaTime;

        // ����� ������ ������
        float shakeOffset = Mathf.Sin(Time.time * shakeFrequency) * shakeAmount;

        // ����������� �������� � ������
        transform.position = basePosition + new Vector3(shakeOffset, 0, 0);

        // ���������� �����, ���� ��� ����� �� ������ ������� ������
        if (transform.position.y < bottomY - 1f) // -1f ��� ���������� ������
        {
            Destroy(gameObject);
        }
    }
}