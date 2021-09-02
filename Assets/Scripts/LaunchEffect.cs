using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchEffect : MonoBehaviour
{

    [SerializeField] private float _SecondsAlive;

    private float _SchrinkSpeed;

    private void Start()
    {
        _SchrinkSpeed = transform.localScale.x / _SecondsAlive;
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(100,70,100);
    }

    void Update()
    {
        /*
        if (transform.localScale.x > 0)
            transform.localScale -= new Vector3(_SchrinkSpeed, 0, _SchrinkSpeed) * Time.deltaTime;
        else
        {
            gameObject.SetActive(false);
        }
        */


    }
}
