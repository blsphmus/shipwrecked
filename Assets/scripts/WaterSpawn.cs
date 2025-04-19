using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    public List<GameObject> wavePrefabs;
    public float spawnInterval = 1f;

    [Header("Movement Settings")]
    public float waveSpeed = 3f;
    public float shakeAmount = 0.01f;
    public float shakeFrequency = 10f;

    private float topY;
    private float bottomY;

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
        Vector3 spawnPosition = new Vector3(0, topY + 1f, 0);
        GameObject newWave = Instantiate(wavePrefab, spawnPosition, Quaternion.identity);

        // Настраиваем параметры движения
        WaveShaking waveShaking = newWave.AddComponent<WaveShaking>();
        waveShaking.Initialize(waveSpeed, shakeAmount, shakeFrequency, bottomY);
    }
}