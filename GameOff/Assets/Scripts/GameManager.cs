using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float Health = 100;
    public float MaxHealth;
    public int Currency = 0;
    public int EnemiesKilled = 0;
    private SpawnManager _spawnManager;

    public bool GameIsOver = false;
    public bool GameIsPaused = false;

    private MusicManager _musicManager;

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
        MaxHealth = Health;
        _musicManager = GetComponent<MusicManager>();
    }

    public bool Buy(int cost)
    {
        if (Currency >= cost)
        {
            Currency -= cost;
            _musicManager.Buy();
            return true;
        }
        _musicManager.CantBuy();
        return false;
    }

    void Start()
    {
        _spawnManager.LoadNextWave(); // to start game
    }

    public void Damage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0; 
            UIManager.instance.GameOver(false);
            Time.timeScale = 0;
        }
        UIManager.instance.UpdateLifeBar(Health);
    }

    void Update()
    {
        if (GameIsOver) return;

        if (PlayerInput.Instance.PauseMenuKey)
        {
            GameIsPaused = !GameIsPaused;
            if (GameIsPaused)
            {
                Time.timeScale = 0;
                UIManager.instance.ActivatePauseMenu();
            }
            else
            {
                ResumeGame();
            }
            return;
        }

        if (_spawnManager.waves[_spawnManager.currentWave].hasBeenSent && _spawnManager.enemiesLeft == 0)
        {
            _spawnManager.LoadNextWave();
        }
    }

    public void AddEnemyKill()
    {
        EnemiesKilled++;
        _spawnManager.enemiesLeft--;
    }

    public void AddCurrency(int amount)
    {
        Currency += amount;
    }

    public void Win()
    {
        UIManager.instance.GameOver(true);
        Time.timeScale = 0;
        GameIsOver = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        UIManager.instance.DeactivatePauseMenu();
    }

    public void RestartGame()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
