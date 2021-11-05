using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct WaveEnemy
{
    public GameObject enemy;
    public int count;
    // public float delay;
    public Transform spawnPoint;
    public int reward;
}
[Serializable]
public class Wave
{
    public WaveEnemy[] enemies;
    public float delay;
    public bool hasBeenSent = false;
}

public class SpawnManager : MonoBehaviour
{
    public Wave[] waves;
    public float currentTime;
    public int currentWave = -1;
    private GameObject[] _spawnPoints;

    void Awake()
    {
        currentTime = 0;
        _spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");
    }

    void Update()
    {
        if (currentWave == -1)
            return;

        currentTime += Time.deltaTime;

        if (!waves[currentWave].hasBeenSent && currentTime >= waves[currentWave].delay)
        {
            SpawnWave();
            currentTime = 0;
        }
    }

    public void LoadNextWave()
    {
        currentWave++;
        if (currentWave >= waves.Length)
            GameManager.instance.Win();
    }

    void SpawnWave()
    {
        waves[currentWave].hasBeenSent = true;
        foreach (WaveEnemy wave in waves[currentWave].enemies)
        {
            for (int i = 0; i < wave.count; i++)
            {
                for (int j = 0; j < 0; j++)
                {
                    Vector3 spawnPoint = wave.spawnPoint == null ? _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].transform.position : wave.spawnPoint.position;
                    GameObject go = Instantiate(wave.enemy, spawnPoint, Quaternion.identity);
                    go.GetComponent<Enemy>().Reward = wave.reward;
                }
            }
        }
    }

}
