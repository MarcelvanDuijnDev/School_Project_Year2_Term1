using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

    public static GameSettings SETTINGS;

    [Header("Settings ID")]
    public string SettingsID = "Default";

    [Header("TimeScale")]
    public float _TimeScale = 1;

    [Header("Movement")]
    [Range(0, 100)] public float MovementSpeed = 2;
    [Range(0, 100)] public float RotationSpeed = 1;
    public int StageDropSpeed = 4;

    [Header("Lives")]
    public int MistakesAllowed = 2;

    [Header("Gameplay")]
    public float SecondsBetweenRockets = 50;
    public float SecondsBetweenRocketsIncrease = 0.5f;
    public int StartDebris = 2000;

    [Header("Debris")]
    public Vector2 MinMaxDebrisSpeed = new Vector2(0.01f,0.03f);
    public int MaxHoldableDebris = 100;
    public int DebrisPerStage = 20;

    [Header("Camera")]
    public bool SkipTransition = false;
    public float TransitionSpeed = 500;

    private void Awake()
    {
        SETTINGS = this;
        Time.timeScale = _TimeScale;
    }

    void Start()
    {
        GameHandler.HANDLER.Set_Settings(MovementSpeed, RotationSpeed, StartDebris, MistakesAllowed, SecondsBetweenRockets, SecondsBetweenRocketsIncrease, MinMaxDebrisSpeed, SettingsID, SkipTransition, MaxHoldableDebris, StageDropSpeed, DebrisPerStage, TransitionSpeed);
    }
}
