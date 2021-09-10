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

    [SerializeField] private Transform _CameraCenter;

    private Rigidbody _RB;
    private Vector3 _StartRotation;

    Vector3 _CenterRot;

    void Start()
    {
        _RB = GetComponent<Rigidbody>();
        _StartRotation = transform.eulerAngles;
    }

    private void FixedUpdate()
    {
        ProcessInput();
        ProcessGravity();

        // Lookat planet
        Vector3 rpos = _EarthCenter.position - transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(rpos, Vector3.up);
        _CameraCenter.rotation = Quaternion.Lerp(_CameraCenter.rotation, lookrotation, 0.5f);

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

    public void Set_Settings(float movement, float rotatespeed)
    {
        _Speed = movement;
        _RotateSpeed = rotatespeed;
    }

    public void Reset()
    {
        
    }
}
