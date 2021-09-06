using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{

    public enum GameStates {Menu,Ingame,Pauzed,Dead }
    [Header("CurrentGameState")]
    [SerializeField] private GameStates _GameState = new GameStates();

    [Header("Game Settings")]
    [SerializeField] private int _FailsAllowed;
    private int _MadeFails;

    [Header("GameOver/Pauze")]
    [SerializeField] private GameObject _DeathScreen = null;
    [SerializeField] private GameObject _PauzeScreen = null;
    [SerializeField] private GameObject _HUDScreen = null;

    public static int DebrisCollected;
    public static GameHandler HANDLER;

    [Header("Ref")]
    public GameObject Earth;
    public CameraControler CameraControler;
    [SerializeField] private SpawnDebris _SpawnDebris;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _DebrisCollected;
    [SerializeField] private TextMeshProUGUI _DebrisInSpace;

    [Header("LaunchPlatforms/Settings")]
    [SerializeField] private List<LaunchEffect> _LaunchPort = new List<LaunchEffect>();
    [SerializeField] private float _TimeBetweenLaunches;

    private Movement _PlayerMovement;
    private float _Timer;

    private void Awake()
    {
        HANDLER = this;
    }

    private void Start()
    {
        _PlayerMovement = GetComponent<Movement>();

        _DeathScreen.SetActive(false);
        _PauzeScreen.SetActive(false);
    }

    void Update()
    {
        //Check Gameover
        if (!_DeathScreen.activeSelf || !_PauzeScreen.activeSelf)
        {
            //UI
            _DebrisCollected.text = "Debris Collected: " + DebrisCollected.ToString();
            _DebrisInSpace.text = "Debris in space: " + ObjectPool.POOL.GetActiveObjectAmount(0);

            //Launch
            _Timer += 1 * Time.deltaTime;
            if (_Timer >= _TimeBetweenLaunches)
            {
                _LaunchPort[Random.Range(0, _LaunchPort.Count)].Launch();
                _Timer = Random.Range(0, _TimeBetweenLaunches * 0.5f);
            }
        }

        if(_GameState == GameStates.Menu)
            _HUDScreen.SetActive(false);
        else
            _HUDScreen.SetActive(true);


        if (_DeathScreen.activeSelf)
            _DeathScreen.SetActive(true);

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            _PauzeScreen.SetActive(!_PauzeScreen.activeSelf);
    }

    public void RocketLaunched()
    {
        _MadeFails = 0;
        Debug.Log("Rocket Launched");
    }
    public void RocketExploded()
    {
        Debug.Log("Rocket Exploded");
        _MadeFails++;
        if(_MadeFails >= _FailsAllowed)
            _DeathScreen.SetActive(true);
    }

    public void Restart()
    {
        _GameState = GameStates.Ingame;
        CameraControler.SetCameraState(1);
        ResetGame();
    }
    public void Menu()
    {
        _GameState = GameStates.Menu;
        CameraControler.SetCameraState(0);
        ResetGame();
    }
    public void Resume()
    {
        ResetUI();
    }


    void ResetUI()
    {
        _PauzeScreen.SetActive(false);
        _DeathScreen.SetActive(false);
    }
    void ResetGame()
    {
        _PlayerMovement.Reset();
        _SpawnDebris.Reset();
        DebrisCollected = 0;
        ResetUI();
    }
}
