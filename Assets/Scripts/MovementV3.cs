using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementV3 : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _RotationSpeed = 100;
    [SerializeField] private float _Acceleration;
    [SerializeField] private Vector3 _CurrentSpeed;
    [SerializeField] private Vector2 _MinMaxSpeed;

    [Header("Camera Center")]
    [SerializeField] private Transform _CameraCenter = null;

    [SerializeField] private Transform _ShipModel;

    private Vector3 _StartRotation;
    private float _SpaceShipAngle;
    private float _CameraAngle;

    private void Start()
    {
        _StartRotation = transform.eulerAngles;
    }

    void Update()
    {
        float rotateinput = -Input.GetAxis("Horizontal") * _RotationSpeed * Time.deltaTime;
        _CurrentSpeed.y += Input.GetAxis("Vertical") * _Acceleration * Time.deltaTime;

        if (_CurrentSpeed.y <= _MinMaxSpeed.x)
            _CurrentSpeed.y = _MinMaxSpeed.x;
        if (_CurrentSpeed.y >= _MinMaxSpeed.y)
            _CurrentSpeed.y = _MinMaxSpeed.y;

        _SpaceShipAngle += rotateinput;

        _ShipModel.transform.eulerAngles = new Vector3(_SpaceShipAngle, _CameraCenter.eulerAngles.y - 90, 0);

        if (rotateinput > 0)
        {
            //_CameraCenter.transform.Rotate(0, 10f, 0);
        }

        //_CameraCenter.transform.eulerAngles = new Vector3(0, _CameraCenter.eulerAngles.y, _CameraAngle);

        //RotValue
        float normalinputx = Input.GetAxis("Horizontal");
        float rotvalue = _ShipModel.eulerAngles.x * Input.GetAxis("Horizontal");
        if (Input.GetAxis("Vertical") != 0)
        {
            if (_ShipModel.eulerAngles.x > _CameraCenter.eulerAngles.z)
            {
                _CameraCenter.transform.Rotate(0, 0, .1f);
            }
            if (_ShipModel.eulerAngles.x < _CameraCenter.eulerAngles.z)
            {
                _CameraCenter.transform.Rotate(0, 0, -.1f);
            }
        }
        

        _CameraCenter.transform.Rotate(0, _CurrentSpeed.y, _CurrentSpeed.z);
    }

    private void HandlerShipRotation()
    {

    }

    public void Reset()
    {
        transform.eulerAngles = _StartRotation;
        _CurrentSpeed = Vector3.zero;
    }

    public void Set_Settings(float movement, float rotationspeed, Vector2 minmaxspeed)
    {
        _Acceleration = movement;
        _RotationSpeed = rotationspeed;
        _MinMaxSpeed = minmaxspeed;
        _CameraCenter.transform.eulerAngles = _StartRotation;
    }
}
