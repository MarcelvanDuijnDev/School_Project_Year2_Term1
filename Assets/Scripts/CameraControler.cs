using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private float _CameraSpeed = .5f;
    [SerializeField] private Vector3 _CameraOffset = Vector3.zero;
    [SerializeField] private Transform _Target = null;

    [SerializeField] private float _Radius = 5;
    [SerializeField] [Range(0, 100)] private float _Angle;

    private float _ScrollWheelInput;

    void Update()
    {
        // Lookat planet
        Vector3 rpos = _Target.position - transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(rpos, Vector3.up);
        transform.eulerAngles = new Vector3(lookrotation.eulerAngles.x + _CameraOffset.x, lookrotation.eulerAngles.y + _CameraOffset.y, lookrotation.eulerAngles.z + _CameraOffset.z);

        // Location
        //transform.position =
        GetRadius();

        _ScrollWheelInput = Input.mouseScrollDelta.x;

        Debug.Log(Input.mouseScrollDelta.x + Input.mouseScrollDelta.y);
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
}