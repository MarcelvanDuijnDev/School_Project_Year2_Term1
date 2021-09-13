using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchHandler : MonoBehaviour
{
    [Header("LaunchPlatforms/Settings")]
    [SerializeField] private List<Launchpad> _LaunchPad = new List<Launchpad>();
    [SerializeField] private float _TimeBetweenLaunches = 50;
    [SerializeField] private float _MinTimeBetweenLaunches = 10;
    [SerializeField] private float _TimeBetweenLaunches_Increase = 0.1f;

    private float _CurrentTimeBetweenLaunches;
    private int _NextLaunchID;
    private float _Timer;

    void Start()
    {
        _CurrentTimeBetweenLaunches = _TimeBetweenLaunches;
        _Timer = _CurrentTimeBetweenLaunches - 13;

        NextLaunchID();
        StartCoroutine(FirstLaunch());
    }

    void Update()
    {
        if(GameHandler.HANDLER.GameState == GameHandler.GameStates.Ingame)
        {
            //Launch
            _Timer += 1 * Time.deltaTime;
            if (_Timer >= _CurrentTimeBetweenLaunches)
            {
                _LaunchPad[_NextLaunchID].Launch();
                _Timer = 0;

                _NextLaunchID = NextLaunchID();
                UIHandler.HANDLER.AddTo_Notifications("Next Launch location: " + _LaunchPad[_NextLaunchID].Name + "\n" +
                    "Launching in: " + (_CurrentTimeBetweenLaunches - _Timer).ToString() + " Seconds");
            }

            if (_CurrentTimeBetweenLaunches > _MinTimeBetweenLaunches)
                _CurrentTimeBetweenLaunches -= _TimeBetweenLaunches_Increase * Time.deltaTime;
        }
    }

    private int NextLaunchID()
    {
        int newid = Random.Range(0, _LaunchPad.Count);
        if (newid == _NextLaunchID)
        {
            NextLaunchID();
        }
        return newid;
    }

    IEnumerator FirstLaunch()
    {
        yield return new WaitForSeconds(3);
        UIHandler.HANDLER.AddTo_Notifications("Next Launch location: " + _LaunchPad[_NextLaunchID].Name + "\n" +
                    "Launching in: " + (_CurrentTimeBetweenLaunches - _Timer).ToString() + " Seconds");
    }

    public void Set_Settings(float timebetweenlaunches, float secondsdecrease)
    {
        _TimeBetweenLaunches = timebetweenlaunches;
        _TimeBetweenLaunches_Increase = secondsdecrease;
    }

    public void ResetLaunchports()
    {
        //Reset RocketLaunchPorts
        for (int i = 0; i < _LaunchPad.Count; i++)
        {
            _LaunchPad[i].Cancel();
        }
        _Timer = 0;
    }

    public void Restart()
    {
        DataHandler.STATS.CreateNewSave();
        for (int i = 0; i < _LaunchPad.Count; i++)
        {
            _LaunchPad[i].Cancel();
        }
        _Timer = _CurrentTimeBetweenLaunches - 10;
        _CurrentTimeBetweenLaunches = _TimeBetweenLaunches;
    }
}
