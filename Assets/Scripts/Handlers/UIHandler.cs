using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public static UIHandler HANDLER;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _DebrisInSpaceText = null;
    [SerializeField] private TextMeshProUGUI _DebrisInInventoryText = null;
    [SerializeField] private TextMeshProUGUI _NotificationsText = null;
    [SerializeField] private TextMeshProUGUI _RocketsLaunched = null;

    [Header("Tutorial")]
    [SerializeField] private List<GameObject> _TutorialSlides = new List<GameObject>();
    private int _TutorialID;

    private string _Notification = "";

    private List<Notification> _Notifications = new List<Notification>();

    private void Awake()
    {
        HANDLER = this;
    }

    void Update()
    {
        if(GameHandler.HANDLER.GameState == GameHandler.GameStates.Ingame)
        {
            //UI
            int activedebris = ObjectPool.POOL.GetActiveObjectAmount(0) + ObjectPool.POOL.GetActiveObjectAmount(1) + ObjectPool.POOL.GetActiveObjectAmount(2) + ObjectPool.POOL.GetActiveObjectAmount(3);
            _DebrisInSpaceText.text = activedebris.ToString();
            _DebrisInInventoryText.text = GameHandler.DebrisInInventory.ToString() + " / " + GameHandler.MaxHoldableDebris.ToString();
            _RocketsLaunched.text = DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].RocketsLaunched.ToString();


            //Notif
            _Notification = "";
            for (int i = 0; i < _Notifications.Count; i++)
            {
                _Notification += _Notifications[_Notifications.Count -1- i].Start_String;
                if (_Notifications[_Notifications.Count - 1 - i].Time != 0)
                    _Notification += _Notifications[_Notifications.Count - 1 - i].Time.ToString("0");
                _Notification += _Notifications[_Notifications.Count - 1 - i].End_String;
                _Notification += "\n\n";
            }

            _NotificationsText.text = _Notification;

            for (int i = 0; i < _Notifications.Count; i++)
            {
                if(_Notifications[i].Time > 0)
                {
                    _Notifications[i].Time -= 1 * Time.deltaTime;
                }
                else
                {
                    _Notifications[i].Time = 0;
                }
            }
        }
    }

    public void AddTo_Notifications(string addto, float time, string addto2, int platformid)
    {
        AudioHandler.AUDIO.PlayTrack("Notification");

        Notification newnotif = new Notification();
        newnotif.Start_String = addto;
        newnotif.Time = time;
        newnotif.End_String = addto2;
        newnotif.PlatformID = platformid;
        _Notifications.Add(newnotif);
    }

    public void AddTo_Notifications(string addto)
    {
        AudioHandler.AUDIO.PlayTrack("Notification");

        Notification newnotif = new Notification();
        newnotif.Start_String = addto;
        newnotif.Time = 0;
        newnotif.End_String = "";
        _Notifications.Add(newnotif);
    }

    //UI Buttons
    public void GOTO_Menu()
    {
        GameHandler.HANDLER.Menu();
    }
    public void GOTO_Turorial()
    {
        GameHandler.HANDLER.CameraControler.SetCameraState(2);
    }
    public void Restart()
    {
        GameHandler.HANDLER.Restart();
        _Notification = "";
    }
    public void Resume() => GameHandler.HANDLER.Resume();
    public void Play() => GameHandler.HANDLER.Restart();
    public void Quit() => Application.Quit();

    public void Tutorial_Next()
    {
        _TutorialID++;
        if (_TutorialID >= _TutorialSlides.Count)
            _TutorialID = 0;

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

[System.Serializable]
public class Notification
{
    public string Start_String;
    public float Time;
    public string End_String;
    public int PlatformID;
}