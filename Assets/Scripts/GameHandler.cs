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
    private int _MadeFails;

    [Header("GameOver/Pauze")]
    [SerializeField] private GameObject _DeathScreen = null;
    [SerializeField] private GameObject _PauzeScreen = null;
    [SerializeField] private GameObject _HUDScreen = null;
    [SerializeField] private GameObject _MenuScreen = null;

    public static int DebrisCollected;
    public static int DebrisInInventory;
    public static GameHandler HANDLER;

    public static int _maxHoldableDebris;

    [Header("Ref")]
    public GameObject Earth;
    public CameraControler CameraControler;
    [SerializeField] private SpawnDebris _SpawnDebris;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _DebrisCollected;
    [SerializeField] private TextMeshProUGUI _DebrisInSpace;
    [SerializeField] private TextMeshProUGUI _DebrisInInventory;
    [SerializeField] private TextMeshProUGUI _Mistakes;

    [Header("LaunchPlatforms/Settings")]
    [SerializeField] private List<LaunchEffect> _LaunchPort = new List<LaunchEffect>();
    [SerializeField] private float _TimeBetweenLaunches = 50;
    [SerializeField] private float _MinTimeBetweenLaunches = 10;
    [SerializeField] private float _TimeBetweenLaunches_Increase = 0.1f;

    private GameHandler_Stats _Stats;
    [SerializeField] private MovementV2 _PlayerMovement;
    private float _CurrentTimeBetweenLaunches;
    private float _Timer;

    //stats
    private float _TimePlaying;
    private string _PlayTestID;

    private void Awake()
    {
        HANDLER = this;
        _Stats = new GameHandler_Stats();
    }

    private void Start()
    {
        _DeathScreen.SetActive(false);
        _PauzeScreen.SetActive(false);

        _CurrentTimeBetweenLaunches = _TimeBetweenLaunches;

        _Timer = _CurrentTimeBetweenLaunches - 10;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Restart();
        }

        //Check Gameover
        if (GameState == GameStates.Ingame)
        {
            //UI
            _DebrisCollected.text = "Debris Collected: " + DebrisCollected.ToString();
            int activedebris = ObjectPool.POOL.GetActiveObjectAmount(0) + ObjectPool.POOL.GetActiveObjectAmount(1) + ObjectPool.POOL.GetActiveObjectAmount(2) + ObjectPool.POOL.GetActiveObjectAmount(3);
            _DebrisInSpace.text = "Debris in space: " + activedebris.ToString();
            _DebrisInInventory.text = "Debris in Inventory: " + DebrisInInventory.ToString() + " / " + _maxHoldableDebris.ToString();
            _Mistakes.text = "Mistakes: " + _MadeFails.ToString();

            //Launch
            _Timer += 1 * Time.deltaTime;
            if (_Timer >= _CurrentTimeBetweenLaunches)
            {
                _LaunchPort[Random.Range(0, _LaunchPort.Count)].Launch();
                _Timer = Random.Range(0, _TimeBetweenLaunches * 0.5f);
            }

            if (_CurrentTimeBetweenLaunches > _MinTimeBetweenLaunches)
                _CurrentTimeBetweenLaunches -= _TimeBetweenLaunches_Increase * Time.deltaTime;

            _TimePlaying += 1 * Time.deltaTime;
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
            if (Input.GetKeyDown(KeyCode.Escape))
                _PauzeScreen.SetActive(!_PauzeScreen.activeSelf);

        //Check GameOver
        if (_MadeFails > _FailsAllowed)
        {
            GameState = GameStates.Dead;
            _DeathScreen.SetActive(true);
        }
    }

    //Rocket Logic
    public void RocketLaunched()
    {
        _MadeFails = 0;
    }
    public void RocketExploded()
    {
        _MadeFails++;
        if (_MadeFails >= _FailsAllowed)
        { 
            //Set Save Data
            DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].TimePlayed = _TimePlaying;
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

            //Reset RocketLaunchPorts
            for (int i = 0; i < _LaunchPort.Count; i++)
            {
                _LaunchPort[i].Cancel();
            }
            _Timer = 0;

            _DeathScreen.SetActive(true);
        }
    }

    //UI Buttons
    public void Restart()
    {
        DataHandler.STATS.CreateNewSave();
        GameState = GameStates.Ingame;
        CameraControler.SetCameraState(1);
        _Timer = _CurrentTimeBetweenLaunches - 10;
        _CurrentTimeBetweenLaunches = _TimeBetweenLaunches;
        _MadeFails = 0;
        _TimePlaying = 0;
        ResetGame();
    }
    public void Menu()
    {
        _MadeFails = 0;
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
        _MadeFails = 0;
        DebrisCollected = 0;
        DebrisInInventory = 0;
        ResetUI();
    }

    //GameSettings
    public void Set_Settings(float movementincrease, float rotationspeed, int debrisstart, int mistakesallowed, float secondsbetweenrockets, Vector2 minmaxdebrisspeed, string playtestid, bool skiptransition, int maxHoldableDebris)
    {
        _PlayerMovement.Set_Settings(movementincrease,rotationspeed);
        DebrisHandler.DEBRIS.Set_Settings(minmaxdebrisspeed);
        _SpawnDebris.Set_Settings(debrisstart);
        CameraControler.Set_Settings(skiptransition);

        _TimeBetweenLaunches = secondsbetweenrockets;
        _FailsAllowed = mistakesallowed;
        _PlayTestID = playtestid;
        _maxHoldableDebris = maxHoldableDebris;
        Debug.Log(_maxHoldableDebris);
    }
}

[System.Serializable]
public class GameHandler_Stats
{
    public int Total_Debris;
    public int Total_DebrisCollected;
    
}