using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float Health = 100;

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


}
