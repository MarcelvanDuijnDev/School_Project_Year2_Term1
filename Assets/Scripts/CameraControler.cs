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

    [Header("Trasition")]
    [SerializeField] private float _TransitionSpeed = 500;
    [SerializeField] private bool _SkipTransition = false;

    private int _CameraState = 0;

    void Update()
    {
        switch(_CameraState)
        {
            case 0: //Menu
                if (!_SkipTransition)
                    transform.position = Vector3.MoveTowards(transform.position, _MenuCameraPosition.position, 500 * Time.deltaTime);
                else
                    transform.position = Vector3.MoveTowards(transform.position, _MenuCameraPosition.position, 10000 * Time.deltaTime);
                break;
            case 1:
                if (!_SkipTransition)
                    transform.position = Vector3.MoveTowards(transform.position, _IngamePoisition.position, 500 * Time.deltaTime);
                else
                    transform.position = Vector3.MoveTowards(transform.position, _IngamePoisition.position, 10000 * Time.deltaTime);
                break;
        }


        // Lookat planet
        Vector3 rpos = _Target.position - transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(rpos, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookrotation, 0.1f);

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

    public void Set_Settings(bool skiptransition)
    {
        _SkipTransition = skiptransition;
    }
}