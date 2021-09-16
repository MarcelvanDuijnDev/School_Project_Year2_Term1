using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public enum GameStates {Menu,Ingame,Pauzed,Dead }
    [Header("CurrentGameState")]
    public GameStates GameState = new GameStates();

    [Header("Game Settings")]
    [SerializeField] private int _FailsAllowed;

    [Header("GameOver/Pauze")]
    [SerializeField] private GameObject _DeathScreen = null;
    [SerializeField] private GameObject _PauzeScreen = null;
    [SerializeField] private GameObject _HUDScreen = null;
    [SerializeField] private GameObject _MenuScreen = null;

    //Static variables
    public static int DebrisCollected;
    public static int DebrisInInventory;
    public static GameHandler HANDLER;
    public static int MaxHoldableDebris;
    [HideInInspector] public static int MadeFails;

    [Header("Ref")]
    public GameObject Earth;
    public CameraControler CameraControler;
    [SerializeField] private SpawnDebris _SpawnDebris;
    [SerializeField] private Movement _PlayerMovement;
    [SerializeField] private RocketDetach _FirstStage;
    [SerializeField] private RocketDetach _SecondStage;
    [SerializeField] private UIHandler _UIHandler;
    [SerializeField] private DebrisCollector _DebrisCollector;

    [Header("Info")]
    public float TimePlaying;

    //Private variables
    private GameHandler_Stats _Stats;
    private float _Timer;
    private string _PlayTestID;
    private LaunchHandler _LaunchHandler;

    //Ignore
    private int _SUS = 0;
    [SerializeField] private GameObject _SUSObj;

    private void Awake()
    {
        HANDLER = this;
        _Stats = new GameHandler_Stats();
        _LaunchHandler = GetComponent<LaunchHandler>();
    }

    private void Start()
    {
        _DeathScreen.SetActive(false);
        _PauzeScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Restart();
        }

        //Check Gameover
        if (GameState == GameStates.Ingame)
        {
            TimePlaying += 1 * Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.R))
            {
                ResetGame();
                Restart();
                _UIHandler.Restart();
            }

        }

        if (GameState == GameStates.Menu)
        {
            _HUDScreen.SetActive(false);
            _MenuScreen.SetActive(true);
        }
        else
        {
            _HUDScreen.SetActive(true);
            _MenuScreen.SetActive(false);
        }


        if (_DeathScreen.activeSelf)
            _DeathScreen.SetActive(true);

        if (GameState == GameStates.Ingame)
        {
            //Stats
            TimePlaying += 1 * Time.deltaTime;

            //IngameMenu
            if (Input.GetKeyDown(KeyCode.Escape))
                _PauzeScreen.SetActive(!_PauzeScreen.activeSelf);
        }

        //Check GameOver
        if (MadeFails > _FailsAllowed)
        {
            GameState = GameStates.Dead;
            _DeathScreen.SetActive(true);
        }



        SUS();
    }

    //Rocket Logic
    public void RocketLaunched()
    {
        MadeFails = 0;
    }
    public void RocketExploded()
    {
        MadeFails++;
        if (MadeFails >= _FailsAllowed)
        { 
            //Set Save Data
            DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].TimePlayed = TimePlaying;
            DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].DebrisCollected = DebrisCollected;
            DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].SettingsID = _PlayTestID;

            DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].DataSettings.Player_MovementSpeed = GameSettings.SETTINGS.MovementSpeed;
            DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].DataSettings.Player_RotationSpeed = GameSettings.SETTINGS.RotationSpeed;

            DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].DataSettings.GamePlay_StartDebris = GameSettings.SETTINGS.StartDebris;
            DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].DataSettings.GamePlay_MinMaxDebrisSpeed = GameSettings.SETTINGS.MinMaxDebrisSpeed;
            DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].DataSettings.GamePlay_MistakesAllowed = GameSettings.SETTINGS.MistakesAllowed;
            DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].DataSettings.GamePlay_SecondsBetweenLaunch = GameSettings.SETTINGS.SecondsBetweenRockets;
            DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].DataSettings.GamePlay_SecondsBetweenLaunchIncrease = GameSettings.SETTINGS.SecondsBetweenRocketsIncrease;
            DataHandler.STATS.SaveData();

            Debug.Log("Rockets launched" + DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].RocketsLaunched);

            _DeathScreen.SetActive(true);

            AudioHandler.AUDIO.PlayTrack("YouLose");
        }
    }

    //UI Buttons
    public void Restart()
    {
        DataHandler.STATS.CreateNewSave();
        GameState = GameStates.Ingame;
        CameraControler.SetCameraState(1);
        MadeFails = 0;
        TimePlaying = 0;
        ResetGame();
    }
    public void Menu()
    {
        MadeFails = 0;
        CameraControler.SetCameraState(0);
        ResetGame();
        GameState = GameStates.Menu;
    }
    public void Resume()
    {
        ResetUI();
    }

    //Gameloop
    void ResetUI()
    {
        _PauzeScreen.SetActive(false);
        _DeathScreen.SetActive(false);
    }
    void ResetGame()
    {
        DataHandler.STATS.SaveData();
        _PlayerMovement.Reset();
        _SpawnDebris.Reset();
        _LaunchHandler.Restart();

        _Timer = 0;
        TimePlaying = 0;
        MadeFails = 0;
        DebrisCollected = 0;
        DebrisInInventory = 0;
        ResetUI();
    }

    //GameSettings
    public void Set_Settings(float movementincrease, float rotationspeed, int debrisstart, int mistakesallowed, float secondsbetweenrockets, float secondsdecrease, Vector2 minmaxdebrisspeed, string playtestid, bool skiptransition, int maxHoldableDebris, float stageDropSpeed, int debrisPerStage, float transitionspeed, float debrisrange)
    {
        _PlayerMovement.Set_Settings(movementincrease,rotationspeed);
        DebrisHandler.DEBRIS.Set_Settings(minmaxdebrisspeed);
        _SpawnDebris.Set_Settings(debrisstart);
        CameraControler.Set_Settings(skiptransition,transitionspeed);

        _LaunchHandler.Set_Settings(secondsbetweenrockets,secondsdecrease);
        _FailsAllowed = mistakesallowed;
        _PlayTestID = playtestid;
        MaxHoldableDebris = maxHoldableDebris;

        _DebrisCollector.Set_Settings(debrisrange);

        _FirstStage.Set_Settings(stageDropSpeed, debrisPerStage);
        _SecondStage.Set_Settings(stageDropSpeed, debrisPerStage);
        Debug.Log("Settings \n " +
            "MovementSpeed: " + movementincrease + "\n" +
            "RotationSpeed: " + rotationspeed + "\n" +
            "Mistakes Allowed: " + mistakesallowed + "\n" +
            "SecondsBetweenLaunches: " + secondsbetweenrockets + "\n" +
            "SecondsDecrease: " + secondsdecrease + "\n" +
            "Start Debris: " + debrisstart + "\n" +
            "MaxDebris: " + MaxHoldableDebris + "\n" +
            "Skip Transition:" + skiptransition);
    }

    void SUS()
    {
        switch(_SUS)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.S))
                    _SUS++;
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.U))
                    _SUS++;
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.S))
                    _SUS++;
                AudioHandler.AUDIO.PlayTrack("SUS");
                break;
            case 3:
                    _SUSObj.SetActive(true);
                if (Input.GetKeyDown(KeyCode.I))
                    _SUS++;
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.M))
                    _SUS++;
                break;
            case 5:
                if (Input.GetKeyDown(KeyCode.P))
                    _SUS++;
                break;
            case 6:
                if (Input.GetKeyDown(KeyCode.O))
                    _SUS++;
                break;
            case 7:
                if (Input.GetKeyDown(KeyCode.S))
                    _SUS++;
                break;
            case 8:
                if (Input.GetKeyDown(KeyCode.T))
                    _SUS++;
                break;
            case 9:
                if (Input.GetKeyDown(KeyCode.O))
                    _SUS++;
                if (Input.GetKeyDown(KeyCode.E))
                    _SUS++;
                break;
            case 10:
                if (Input.GetKeyDown(KeyCode.R))
                    _SUS++;
                break;
            case 11:
                _SUSObj.SetActive(false);
                _SUS = 0;
                break;
        }
    }

    public int PlatformAmount()
    {
        return _LaunchHandler.TotalLaunchPorts();
    }
}

[System.Serializable]
public class GameHandler_Stats
{
    public int Total_Debris;
    public int Total_DebrisCollected;
    
}