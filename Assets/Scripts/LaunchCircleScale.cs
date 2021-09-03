using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchCircleScale : MonoBehaviour
{
    private Transform _Earth;
    [SerializeField] private float _MaxScale;
    [SerializeField] private float _Radius;

    void Start()
    {
        _Earth = GameHandler.HANDLER.Earth.transform;
        _MaxScale = GameHandler.HANDLER.Earth.transform.localScale.x;
    }

    void Update()
    {
        float distancce = Vector3.Distance(transform.position, _Earth.position);
        transform.localScale = new Vector3((_Radius / distancce) * _MaxScale, 0.032f, (_Radius / distancce) * _MaxScale);
    }
}
