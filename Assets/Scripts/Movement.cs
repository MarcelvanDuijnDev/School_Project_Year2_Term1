using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _RotationSpeed = 100;
    [SerializeField] private float _Speed = 50;
    [SerializeField] private float _IdleSpeed = 2;

    [SerializeField] private float _Acceleration;
    [SerializeField] private float _CurrentSpeed;
    [SerializeField] private Vector2 _MinMaxSpeed;

    [Header("Camera Center")]
    [SerializeField] private Transform _CameraCenter = null;

    void Update()
    {
        float rotateinput = -Input.GetAxis("Horizontal") * _RotationSpeed * Time.deltaTime;
        _CurrentSpeed = -Input.GetAxis("Vertical") * _Acceleration * Time.deltaTime;

        if (_CurrentSpeed <= _MinMaxSpeed.x)
            _CurrentSpeed = _MinMaxSpeed.x;
        if (_CurrentSpeed >= _MinMaxSpeed.y)
            _CurrentSpeed = _MinMaxSpeed.y;

        _CameraCenter.transform.Rotate(0, _CurrentSpeed, rotateinput);
    }
}
