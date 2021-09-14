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
    [SerializeField] private TextMeshProUGUI _Timer = null;

    [Header("Tutorial")]
    [SerializeField] private List<GameObject> _TutorialSlides = new List<GameObject>();
    private int _TutorialID;

    private string _Notification = "";

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
            _Timer.text = string.Format("{0:00}:{1:00}:{2:00}", Mathf.Floor(GameHandler.HANDLER.TimePlaying / 3600), Mathf.Floor((GameHandler.HANDLER.TimePlaying / 60) % 60), GameHandler.HANDLER.TimePlaying % 60);

            _NotificationsText.text = _Notification;
        }
    }

    public void AddTo_Notifications(string addto)
    {
        addto += "\n\n" + _Notification;
        _Notification = addto;
    }

    //UI Buttons
    public void GOTO_Menu() => GameHandler.HANDLER.Menu();
    public void GOTO_Turorial()
    {
        GameHandler.HANDLER.CameraControler.SetCameraState(2);
    }
    public void Restart() => GameHandler.HANDLER.Restart();
    public void Resume() => GameHandler.HANDLER.Resume();
    public void Play() => GameHandler.HANDLER.Restart();
    public void Quit() => Application.Quit();

    public void Tutorial_Next()
    {
        _TutorialID++;
        for (int i = 0; i < _TutorialSlides.Count; i++)
        {
            if (_TutorialID == i)
                _TutorialSlides[i].SetActive(true);
            else
                _TutorialSlides[i].SetActive(false);
        }
    }
    public void Tutorial_Back()
    {
        GameHandler.HANDLER.CameraControler.SetCameraState(0);
        _TutorialID = 0;
    }
}
