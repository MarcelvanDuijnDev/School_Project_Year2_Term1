using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launchpad : MonoBehaviour
{
    [Header("LaunchPad")]
    public string Name;

    [Header("Settings")]
    [SerializeField] private float _SecondsAlive;
    [SerializeField] private RocketLaunch _RocketPort;

    //Private Variables
    private int _State;
    private float _SchrinkSpeed;
    private float _CircleScale = 100;

    private void Start()
    {
        _SchrinkSpeed = _CircleScale / _SecondsAlive;
    }

    public void Launch()
    {
        if (_State == 0)
        {
            transform.localScale = new Vector3(_CircleScale, 70, _CircleScale);
            _State = 1;
        }
    }

    public void Cancel()
    {
        transform.localScale = new Vector3(0,transform.localScale.y,0);
        _State = 0;
    }

    void Update()
    {
        switch (_State)
        {
            case 1:
                if (transform.localScale.x > 0)
                    transform.localScale -= new Vector3(_SchrinkSpeed, 0, _SchrinkSpeed) * Time.deltaTime;
                else
                {
                    _State++;
                }
                break;
            case 2:
                _RocketPort.LaunchRocket();
                AudioHandler.AUDIO.PlayTrack("RocketLaunch");
                _State = 0;
                break;
        }
    }
}
