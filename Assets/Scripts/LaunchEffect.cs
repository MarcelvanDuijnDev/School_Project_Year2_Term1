using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchEffect : MonoBehaviour
{

    [SerializeField] private float _SecondsAlive;
    [SerializeField] private RocketLaunch _RocketPort;

    private float _SchrinkSpeed;

    private int _State;

    private void Start()
    {
        _SchrinkSpeed = transform.localScale.x / _SecondsAlive;
    }


    public void Launch()
    {
        transform.localScale = new Vector3(100, 70, 100);
        _State = 1;
    }

    void Update()
    {
        switch(_State)
        {
            case 1:
                if (transform.localScale.x > 0)
                    transform.localScale -= new Vector3(_SchrinkSpeed, 0, _SchrinkSpeed) * Time.deltaTime;
                else
                {
                    gameObject.SetActive(false);
                    _State++;
                }
                break;
            case 2:
                _RocketPort.LaunchRocket();
                _State = 0;
                break;
        }
    }
}
