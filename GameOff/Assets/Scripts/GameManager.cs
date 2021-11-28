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
        _musicManager = MusicManager.instance;
    }

    public bool Buy(int cost)
    {
        if (Currency >= cost)
        {
            Currency -= cost;
            return true;
        }
        return false;
    }

    void Start()
    {
        _spawnManager.LoadNextWave(); // to start game
        _musicManager.PlayWaitForBattleTheme();
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
        Debug.Log("You Win");
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
