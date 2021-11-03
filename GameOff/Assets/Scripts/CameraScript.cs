using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float Speed;

    private Transform _transform;

    void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        float horizontal = PlayerInput.Instance.HorizontalInput;
        _transform.position += _transform.right * horizontal * Speed * Time.deltaTime;
        float vertical = PlayerInput.Instance.VerticalInput;
        _transform.position += _transform.forward * vertical * Speed * Time.deltaTime;
        
    }    
}
