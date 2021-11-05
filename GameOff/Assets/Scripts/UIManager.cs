using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject _canvas;
    private Text _time;
    private Text _killCount;
    private Text _Wave;
    private Text _startTime;

    void Awake()
    {
        _canvas = GameObject.FindGameObjectsWithTag("UI")[0];
    }

    void Update()
    {

    }
}
