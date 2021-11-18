using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public bool LeftMouseButton { get; private set; }
    public bool PauseMenuKey { get; private set; }
   
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        this.HorizontalInput = Input.GetAxis("Horizontal");
        this.VerticalInput = Input.GetAxis("Vertical");
        this.LeftMouseButton = Input.GetMouseButton(0);
        this.PauseMenuKey = Input.GetKeyDown(KeyCode.Escape);

    }
}
