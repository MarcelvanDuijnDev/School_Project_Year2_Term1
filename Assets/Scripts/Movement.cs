using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private float _RotationSpeed;
    [SerializeField] private float _Speed;
    [SerializeField] private float _IdleSpeed;

    void Update()
    {

        float rotateinput = -Input.GetAxis("Horizontal") * _RotationSpeed * Time.deltaTime;
        float forwardspeed = -Input.GetAxis("Vertical") * _Speed * Time.deltaTime;
        
        transform.Rotate(0, forwardspeed + -_IdleSpeed, rotateinput);
    }
}
