using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementV2 : MonoBehaviour
{
    [SerializeField] private float _Speed;
    [SerializeField] private float _RotateSpeed;

    [SerializeField] private Transform _EarthCenter;

    [SerializeField] private float _Gravity = 9.81f;
    [SerializeField] private bool _AutoOrient = false;
    [SerializeField] float _AutoOrientSpeed = 1f;

    private Rigidbody _RB;

    void Start()
    {
        _RB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ProcessInput();
        ProcessGravity();
    }

    void ProcessInput()
    {
        float inputx = -Input.GetAxis("Horizontal") * _RotateSpeed;
        float inputy = -Input.GetAxis("Vertical") * _Speed;
        transform.Rotate(0,0, inputx);
        _RB.AddForce(transform.right * inputy);
    }

    void ProcessGravity()
    {
        Vector3 diff = transform.position - _EarthCenter.position;
        _RB.AddForce(-diff.normalized * _Gravity * (_RB.mass));

        if (_AutoOrient) { AutoOrient(-diff); }
    }

    void AutoOrient(Vector3 down)
    {
        Quaternion orientationDirection = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientationDirection, _AutoOrientSpeed * Time.deltaTime);
    }
}
