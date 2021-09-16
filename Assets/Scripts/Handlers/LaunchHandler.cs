using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchHandler : MonoBehaviour
{
    [Header("LaunchPlatforms/Settings")]
    [SerializeField] private List<Launchpad> _LaunchPad = new List<Launchpad>();
    [SerializeField] private float _TimeBetweenLaunches = 50;
    [SerializeField] private float _MinTimeBetweenLaunches = 10;
    [SerializeField] private float _TimeBetweenLaunches_Increase = 1f;

    private float _CurrentTimeBetweenLaunches;
    private int _NextLaunchID;
    private int _CheckPrevLaunchID;
    private float _Timer;
    private bool _Started;
    private bool _FirstLaunch;

    void Start()
    {
        _CurrentTimeBetweenLaunches = _TimeBetweenLaunches;
        _Timer = _CurrentTimeBetweenLaunches - 20;

        NextLaunchID();
        _CheckPrevLaunchID = _NextLaunchID;
    }

    void Update()
    {
        if(GameHandler.HANDLER.GameState == GameHandler.GameStates.Ingame)
        {
            //Check restart
            if(!_Started)
            {
                StartCoroutine(FirstLaunch());
                _Started = true;
            }

            //Launch
            _Timer += 1 * Time.deltaTime;
            if (_Timer >= _CurrentTimeBetweenLaunches)
            {
                if (!_FirstLaunch)
                    _FirstLaunch = true;
                else
                    _LaunchPad[_NextLaunchID].Launch();


                _Timer = 0;

                NextLaunchID();
                if (_CurrentTimeBetweenLaunches > _MinTimeBetweenLaunches)
                    _CurrentTimeBetweenLaunches -= Random.Range(0,_TimeBetweenLaunches_Increase);
                StartCoroutine(SendNotification());
            }
        }
    }

    private void NextLaunchID()
    {
        _NextLaunchID = Random.Range(0, _LaunchPad.Count);
        if (_CheckPrevLaunchID == _NextLaunchID)
            NextLaunchID();
        else
            _CheckPrevLaunchID = _NextLaunchID;
    }

    IEnumerator FirstLaunch()
    {
        yield return new WaitForSeconds(2);
        UIHandler.HANDLER.AddTo_Notifications("Starting System...");
        yield return new WaitForSeconds(2);
        UIHandler.HANDLER.AddTo_Notifications("Loading Data..");
        yield return new WaitForSeconds(3);
        UIHandler.HANDLER.AddTo_Notifications("Satellite Connected to ground station.");
    }

    IEnumerator SendNotification()
    {
        yield return new WaitForSeconds(5);
        UIHandler.HANDLER.AddTo_Notifications("Next Launch location: " + _LaunchPad[_NextLaunchID].Name + "\n" +
                    "Launching in: ", (_CurrentTimeBetweenLaunches - _Timer + 10), " Seconds", _NextLaunchID);
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
        StopCoroutine(FirstLaunch());
        StopCoroutine(SendNotification());
        DataHandler.STATS.CreateNewSave();
        for (int i = 0; i < _LaunchPad.Count; i++)
        {
            _LaunchPad[i].Cancel();
        }
        _Timer = _CurrentTimeBetweenLaunches - 10;
        _CurrentTimeBetweenLaunches = _TimeBetweenLaunches;
        _FirstLaunch = false;
        _Started = false;
    }

    public int TotalLaunchPorts()
    {
        return _LaunchPad.Count;
    }
}
