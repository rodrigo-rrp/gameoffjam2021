using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private GameObject _canvas;
    private Text _time;
    private Text _killCount;
    private Text _wave;
    private Text _startTime;
    private Text _money;
    public Image constructionPanel;

    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _canvas = GameObject.FindGameObjectsWithTag("UI")[0];
        _time = _canvas.transform.Find("Time").gameObject.GetComponent<Text>();
        _killCount = _canvas.transform.Find("KillCount").gameObject.GetComponent<Text>();
        _wave = _canvas.transform.Find("Wave").gameObject.GetComponent<Text>();
        _startTime = _canvas.transform.Find("StartTime").gameObject.GetComponent<Text>();
        _money = _canvas.transform.Find("Money").gameObject.GetComponent<Text>();
        constructionPanel = _canvas.transform.Find("ConstructionMenu").gameObject.GetComponent<Image>();

        _spawnManager = GetComponent<SpawnManager>();

    }

    void Update()
    {
        if (GameManager.instance.GameIsOver)
        {
            _canvas.SetActive(false);
            return;
        }
        _time.text = "Time: " + Mathf.RoundToInt(Time.timeSinceLevelLoad);
        _killCount.text = "Kills: " + GameManager.instance.EnemiesKilled;
        _wave.text = "WAVE " + (_spawnManager.currentWave + 1);
        if (_spawnManager.isPlaying && !_spawnManager.waves[_spawnManager.currentWave].hasBeenSent)
            _startTime.text = "Starts in  " + (_spawnManager.waves[_spawnManager.currentWave].delay - (Time.deltaTime + _spawnManager.currentTime)).ToString("0") + " seconds";
        else
            _startTime.text = "";
        _money.text = "Money: " + GameManager.instance.Currency;

        //if (constructionPanel.gameObject.activeSelf && !BuildManager.instance.IsBuilding) 
        //   DeactivateConstructionPanel();
    }

    public void PositionConstructionPanel(Vector3 position)
    {
        constructionPanel.gameObject.SetActive(true);
        constructionPanel.transform.position = Camera.main.WorldToScreenPoint(position);
    }

    public void DeactivateConstructionPanel()
    {
        constructionPanel.gameObject.SetActive(false);
    }
}
