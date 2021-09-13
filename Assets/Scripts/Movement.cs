using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _Speed;
    [SerializeField] private float _RotateSpeed;
    [SerializeField] private float _Gravity = 9.81f;

    [Header("Ref")]
    [SerializeField] private Transform _EarthCenter;
    [SerializeField] private Transform _CameraCenter;

    //Private Variables
    private Rigidbody _RB;
    private Vector3 _StartRotation;
    private Vector3 _StartPosition;
    private float _CheckGrav;

    void Start()
    {
        _CheckGrav = _Gravity;
        _RB = GetComponent<Rigidbody>();
        _StartRotation = transform.eulerAngles;
        _StartPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (GameHandler.HANDLER.GameState == GameHandler.GameStates.Ingame)
        {
            ProcessInput();
            ProcessGravity();

            // Lookat planet
            Vector3 rpos = _EarthCenter.position - transform.position;
            Quaternion lookrotation = Quaternion.LookRotation(rpos, Vector3.up);
            _CameraCenter.rotation = Quaternion.Lerp(_CameraCenter.rotation, lookrotation, 0.5f);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Reset();
        }
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
    }

    public void Set_Settings(float movement, float rotatespeed)
    {
        _Speed = movement;
        _RotateSpeed = rotatespeed;
    }

    public void Reset()
    {
        StartCoroutine(ResetShip());
    }

    IEnumerator ResetShip()
    {
        _Gravity = 5;
        _RB.velocity = Vector3.zero;
        transform.eulerAngles = _StartRotation;
        transform.position = _StartPosition;

        Vector3 rpos = _EarthCenter.position - transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(rpos, Vector3.up);
        _CameraCenter.rotation = lookrotation;
        _RB.rotation = lookrotation;

        yield return new WaitForSeconds(3);
        _Gravity = _CheckGrav;
    }
}
