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

    private Vector3 _constructionPanelPos;

    private RectTransform _lifeBar;
    private Text _lifeBarText;
    private Vector2 _lifeBarSize;

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
        _lifeBar = _canvas.transform.Find("HPBar/Life").gameObject.GetComponent<RectTransform>();
        _lifeBarText = _canvas.transform.Find("HPBar/Text").gameObject.GetComponent<Text>();
        _lifeBarSize = _lifeBar.sizeDelta;

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
        if (_spawnManager.isWaiting)
            _startTime.text = "Starts in  " + (_spawnManager.waves[_spawnManager.currentWave].delay - (Time.deltaTime + _spawnManager.currentTime)).ToString("0") + " seconds";
        else
            _startTime.text = "";
        _money.text = "Money: " + GameManager.instance.Currency;


        constructionPanel.transform.position = Camera.main.WorldToScreenPoint(_constructionPanelPos);
    }

    public void UpdateLifeBar(float hp)
    {
        _lifeBar.sizeDelta = new Vector2(_lifeBarSize.x * (hp / GameManager.instance.MaxHealth), _lifeBarSize.y);
        Debug.Log(hp / GameManager.instance.MaxHealth);
        if (hp / GameManager.instance.MaxHealth < 0.5f)
            _lifeBar.GetComponent<Image>().color = Color.yellow;
        if (hp / GameManager.instance.MaxHealth < 0.2f)
            _lifeBar.GetComponent<Image>().color = Color.red;
        _lifeBarText.text = (hp / GameManager.instance.MaxHealth * 100).ToString() + "%";
    }

    public void PositionConstructionPanel(Vector3 position)
    {
        constructionPanel.gameObject.SetActive(true);
        _constructionPanelPos = position;
    }

    public void DeactivateConstructionPanel()
    {
        constructionPanel.gameObject.SetActive(false);
    }
}
