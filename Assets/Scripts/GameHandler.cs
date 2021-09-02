using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{

    public static int DebrisCollected;
    public static GameHandler HANDLER;

    public GameObject Earth;

    [SerializeField] private TextMeshProUGUI _DebrisCollected;
    [SerializeField] private TextMeshProUGUI _DebrisInSpace;

    private void Awake()
    {
        HANDLER = this;
    }

    void Update()
    {
        _DebrisCollected.text = "Debris Collected: " + DebrisCollected.ToString();
        _DebrisInSpace.text = "Debris in space: " + ObjectPool.POOL.GetActiveObjectAmount(0);
    }
}
