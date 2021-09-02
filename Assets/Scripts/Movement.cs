using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _RotationSpeed = 100;
    [SerializeField] private float _Speed = 50;

    [Header("Camera Center")]
    [SerializeField] private Transform _CameraCenter = null;

    void Update()
    {
        float rotateinput = -Input.GetAxis("Horizontal") * _RotationSpeed * Time.deltaTime;
        float forwardspeed = -Input.GetAxis("Vertical") * _Speed * Time.deltaTime;

        _CameraCenter.transform.Rotate(0, forwardspeed, rotateinput);
    }
}
