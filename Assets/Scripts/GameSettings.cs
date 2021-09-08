using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

    public static GameSettings SETTINGS;

    [Header("Settings ID")]
    public string SettingsID = "Default";

    [Header("Movement")]
    [Range(0,1)] public float MovementSpeed = 0.02f;
    [Range(0, 100)] public float RotationSpeed = 20;
    public Vector2 MinMaxSpeed = new Vector2(-0.05f,.1f);

    [Header("Lives")]
    public int MistakesAllowed = 2;

    [Header("Gameplay")]
    public float SecondsBetweenRockets = 50;
    public float SecondsBetweenRocketsIncrease = 0.5f;
    public int StartDebris = 2000;

    [Header("Debris")]
    public Vector2 MinMaxDebrisSpeed = new Vector2(0.01f,0.03f);

    [Header("Camera")]
    public bool SkipTransition = false;

    private void Awake()
    {
        SETTINGS = this;
    }

    void Start()
    {
        GameHandler.HANDLER.Set_Settings(MovementSpeed, RotationSpeed, MinMaxSpeed, StartDebris, MistakesAllowed, SecondsBetweenRockets, MinMaxDebrisSpeed, SettingsID, SkipTransition);
    }
}
