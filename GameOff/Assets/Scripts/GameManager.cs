using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float Health = 100;
    public int Currency = 0;
    public int EnemiesKilled = 0;
    private SpawnManager _spawnManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _spawnManager = GetComponent<SpawnManager>();
    }

    void Start()
    {
        _spawnManager.LoadNextWave(); // to start game
    }

    public void Damage(float damage)
    {
        Health -= damage;
        Debug.Log("Health: " + Health.ToString());
        if (Health <= 0)
        {
            Health = 0;
            Debug.Log("Game Over");
            Time.timeScale = 0;
        }
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            _spawnManager.LoadNextWave();
        }
    }

    public void AddEnemyKill()
    {
        EnemiesKilled++;
    }

    public void AddCurrency(int amount)
    {
        Currency += amount;
        Debug.Log("Currency: " + Currency.ToString());
    }

    public void Win()
    {
        Debug.Log("You Win");
        Time.timeScale = 0;
    }


}
