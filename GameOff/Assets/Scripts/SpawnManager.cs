using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public struct WaveEnemy
{
    public GameObject enemy;
    public int count;
    public Vector3 spawnPoint;
    public int reward;
    public float delay;

    public void InvokeSpawn(Vector3 spawnPoint, float delay, MonoBehaviour mono)
    {
        this.spawnPoint = spawnPoint;

        IEnumerator coroutine = WaitAndSpawn(delay);
        mono.StartCoroutine(coroutine);
    }

    public void Spawn()
    {
        GameObject go = GameObject.Instantiate(enemy, spawnPoint, Quaternion.identity);
        go.GetComponent<Enemy>().Reward = reward;
    }

    IEnumerator WaitAndSpawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        Spawn();
    }
}
[Serializable]
public class Wave
{
    public WaveEnemy[] enemies;
    public float delay;
    public bool hasBeenSent = false;

    public void Spawn(GameObject[] _spawnPoints, MonoBehaviour mono)
    {
        foreach (WaveEnemy we in enemies)
        {
            for (int i = 0; i < we.count; i++)
            {
                Vector3 spawnPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].transform.position;
                we.InvokeSpawn(spawnPoint, i * we.delay, mono);
            }
        }
    }
}

public class SpawnManager : MonoBehaviour
{
    public Wave[] waves;
    public float currentTime;
    public int currentWave = 0;
    private GameObject[] _spawnPoints;
    public bool isWaiting = false;
    public bool isPlaying = false;
    public int enemiesLeft = 0;

    public static SpawnManager instance { get; private set; }

    void Awake()
    {
        instance = this;
        _spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    void Update()
    {
        if (currentWave == -1)
            return;

        currentTime += Time.deltaTime;

        if (!waves[currentWave].hasBeenSent && currentTime >= waves[currentWave].delay)
        {
            MusicManager.instance.PlayBattleTheme();
            BuildManager.instance.HideBuildSlots();
            waves[currentWave].Spawn(_spawnPoints, this);
            waves[currentWave].hasBeenSent = true;
            isPlaying = true;
            isWaiting = false;
        }
    }

    public void LoadNextWave()
    {
        currentWave++;
        if (currentWave >= waves.Length)
        {
            GameManager.instance.Win();
            currentWave = -1;
            return;
        }
        isWaiting = true;
        isPlaying = false;
        currentTime = 0;
        enemiesLeft = waves[currentWave].enemies.ToList().Sum(x => x.count);
        MusicManager.instance.PlayWaitForBattleTheme();
        BuildManager.instance.ShowBuildSlots();

    }

}
