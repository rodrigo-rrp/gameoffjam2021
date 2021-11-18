using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Resume()
    {
        GameManager.instance.ResumeGame();
    }

    public void Restart()
    {
        GameManager.instance.RestartGame();
    }

    public void Quit()
    {
        GameManager.instance.QuitGame();
    }
}
