using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{

    public static int DebrisCollected;
    public static GameHandler HANDLER;

    [Header("Earth Ref")]
    public GameObject Earth;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _DebrisCollected;
    [SerializeField] private TextMeshProUGUI _DebrisInSpace;

    [Header("LaunchPlatforms")]
    [SerializeField] private List<LaunchEffect> _LaunchPort = new List<LaunchEffect>();

    [Header("Rocket Launch Settings")]
    [SerializeField] private float _TimeBetweenLaunches;
    private float _Timer;

    private void Awake()
    {
        HANDLER = this;
    }

    private void Start()
    {
        _Timer = _TimeBetweenLaunches;
    }

    void Update()
    {
        //UI
        _DebrisCollected.text = "Debris Collected: " + DebrisCollected.ToString();
        _DebrisInSpace.text = "Debris in space: " + ObjectPool.POOL.GetActiveObjectAmount(0);

        //Launch
        _Timer += 1 * Time.deltaTime;
        if(_Timer >= _TimeBetweenLaunches)
        {
            _LaunchPort[Random.Range(0, _LaunchPort.Count)].Launch();
            _Timer = Random.Range(0,_TimeBetweenLaunches * 0.5f);
        }
    }
}
