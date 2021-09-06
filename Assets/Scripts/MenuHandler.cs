using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    public void Menu()
    {
        GameHandler.HANDLER.Menu();
    }

    public void Restart()
    {
        GameHandler.HANDLER.Restart();
    }

    public void Resume()
    {
        GameHandler.HANDLER.Resume();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        GameHandler.HANDLER.Restart();
    }
}
