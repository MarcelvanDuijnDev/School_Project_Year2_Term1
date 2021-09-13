using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public static UIHandler HANDLER;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _DebrisCollectedText = null;
    [SerializeField] private TextMeshProUGUI _DebrisInSpaceText = null;
    [SerializeField] private TextMeshProUGUI _DebrisInInventoryText = null;
    [SerializeField] private TextMeshProUGUI _MistakesText = null;
    [SerializeField] private TextMeshProUGUI _NotificationsText = null;

    private string _Notification = "Starting System... \n";

    private void Awake()
    {
        HANDLER = this;
    }

    void Update()
    {
        if(GameHandler.HANDLER.GameState == GameHandler.GameStates.Ingame)
        {
            //UI
            _DebrisCollectedText.text = "Debris Collected: " + GameHandler.DebrisCollected.ToString();
            int activedebris = ObjectPool.POOL.GetActiveObjectAmount(0) + ObjectPool.POOL.GetActiveObjectAmount(1) + ObjectPool.POOL.GetActiveObjectAmount(2) + ObjectPool.POOL.GetActiveObjectAmount(3);
            _DebrisInSpaceText.text = "Debris in space: " + activedebris.ToString();
            _DebrisInInventoryText.text = "Debris in Inventory: " + GameHandler.DebrisInInventory.ToString() + " / " + GameHandler.MaxHoldableDebris.ToString();
            _MistakesText.text = "Mistakes: " + GameHandler.MadeFails.ToString();

            _NotificationsText.text = _Notification;
        }
    }

    public void AddTo_Notifications(string addto)
    {
        addto += "\n" + _Notification;
        _Notification = addto;
    }

    //UI Buttons
    public void GOTO_Menu() => GameHandler.HANDLER.Menu();
    public void Restart() => GameHandler.HANDLER.Restart();
    public void Resume() => GameHandler.HANDLER.Resume();
    public void Play() => GameHandler.HANDLER.Restart();
    public void Quit() => Application.Quit();

}
