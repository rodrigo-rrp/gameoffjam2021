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
    private Text _bigText;
    private Text _smallText;
    private Text _money;
    public Image constructionPanel;

    public static UIManager instance;

    private Vector3 _constructionPanelPos;

    private RectTransform _lifeBar;
    private Text _lifeBarText;
    private Vector2 _lifeBarSize;

    private GameObject _pauseMenu;
    private GameObject _gameOverMenu;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _canvas = GameObject.FindGameObjectsWithTag("UI")[0];
        _time = _canvas.transform.Find("Time").gameObject.GetComponent<Text>();
        _money = _canvas.transform.Find("Money").gameObject.GetComponent<Text>();
        constructionPanel = _canvas.transform.Find("ConstructionMenu").gameObject.GetComponent<Image>();
        _lifeBar = _canvas.transform.Find("Bottom/Life").gameObject.GetComponent<RectTransform>();
        _lifeBarText = _canvas.transform.Find("Bottom/Text").gameObject.GetComponent<Text>();
        _smallText = _canvas.transform.Find("Bottom/Small_Text").gameObject.GetComponent<Text>();
        _bigText = _canvas.transform.Find("Bottom/Big_Text").gameObject.GetComponent<Text>();
        _lifeBarSize = _lifeBar.sizeDelta;


        _pauseMenu = _canvas.transform.Find("PauseMenu").gameObject;

        _spawnManager = GetComponent<SpawnManager>();

    }

    void Update()
    {
        if (GameManager.instance.GameIsOver)
        {
            return;
        }
        if (_spawnManager.isWaiting)
        {
            _smallText.text = "UNTIL WAVE " + (_spawnManager.currentWave + 1);
            _bigText.text = (_spawnManager.waves[_spawnManager.currentWave].delay - (Time.deltaTime + _spawnManager.currentTime)).ToString("0:00");
            float seconds = float.Parse((_spawnManager.waves[_spawnManager.currentWave].delay - (Time.deltaTime + _spawnManager.currentTime)).ToString("0"));
            Debug.Log(seconds);
            if (seconds == 5f && !MusicManager.instance.ClockTickAudioSource.isPlaying)
            {
                MusicManager.instance.PlayClockTicks();
                Debug.Log("play music");
            }
        }
        else
        {
            _smallText.text = "";
            _bigText.text = "WAVE " + (_spawnManager.currentWave + 1);
        }
        _money.text = GameManager.instance.Currency.ToString();


        constructionPanel.transform.position = Camera.main.WorldToScreenPoint(_constructionPanelPos);
    }

    public void UpdateLifeBar(float hp)
    {
        _lifeBar.sizeDelta = new Vector2(_lifeBarSize.x * (hp / GameManager.instance.MaxHealth), _lifeBarSize.y);
        if (hp / GameManager.instance.MaxHealth < 0.5f)
            _lifeBar.GetComponent<Image>().color = new Color(248 / 255f, 213 / 255f, 72 / 255f);
        if (hp / GameManager.instance.MaxHealth < 0.2f)
            _lifeBar.GetComponent<Image>().color = new Color(240 / 255f, 65 / 255f, 65 / 255f);
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

    public void ActivatePauseMenu()
    {
        _pauseMenu.SetActive(true);
    }

    public void DeactivatePauseMenu()
    {
        _pauseMenu.SetActive(false);
    }

    public void GameOver(bool won)
    {
        _gameOverMenu = _canvas.transform.Find("GameOver").gameObject;
        _gameOverMenu.SetActive(true);
        _gameOverMenu.transform.Find("Final Result").GetComponent<Text>().text = won ? "YOU WON" : "YOU LOST";
        _gameOverMenu.transform.Find("Final Result Shadow").GetComponent<Text>().text = won ? "YOU WON" : "YOU LOST";
    }
}
