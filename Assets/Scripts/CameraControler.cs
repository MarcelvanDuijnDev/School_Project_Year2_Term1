using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [Header("Ingame")]
    [SerializeField] private float _ScrollSpeed = 20;
    [SerializeField] private Vector3 _CameraOffset = Vector3.zero;
    [SerializeField] private Transform _Target = null;

    //[SerializeField] private float _Radius = 5;
    [SerializeField] [Range(0, 100)] private float _Angle;

    private float _ScrollWheelInput;

    [Header("CameraPositions")]
    [SerializeField] private Transform _IngamePoisition = null;
    [SerializeField] private Transform _MenuCameraPosition = null;

    private int _CameraState = 0;

    void Update()
    {
        switch(_CameraState)
        {
            case 0: //Menu
                transform.position = Vector3.MoveTowards(transform.position, _MenuCameraPosition.position, 500 * Time.deltaTime);
                break;
            case 1:
                transform.position = Vector3.MoveTowards(transform.position, _IngamePoisition.position, 500 * Time.deltaTime);
                break;
        }


        // Lookat planet
        Vector3 rpos = _Target.position - transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(rpos, Vector3.up);
        transform.eulerAngles = new Vector3(lookrotation.eulerAngles.x + _CameraOffset.x, lookrotation.eulerAngles.y + _CameraOffset.y, lookrotation.eulerAngles.z + _CameraOffset.z);

        // Location
        //transform.position =
        //GetRadius();

        _ScrollWheelInput = Input.mouseScrollDelta.y * _ScrollSpeed;
        transform.Translate(new Vector3(0, 0, _ScrollWheelInput) * Time.deltaTime);
        _ScrollWheelInput = 0;
    }

    void GetRadius()
    {
        float ang = 36000 / 1 * 0.01f;
        Vector3 pos;
        pos.x = _Target.transform.position.x + _Angle * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = _Target.transform.position.y + _Angle * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = _Target.transform.position.z;
        transform.position = pos;
    }

    public void SetCameraState(int state)
    {
        _CameraState = state;
    }
}