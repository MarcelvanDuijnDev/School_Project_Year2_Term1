using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [Header("Movement")]
    [Range(0,1)] public float MovementSpeed = 0.02f;
    [Range(0, 100)] public float RotationSpeed = 10;
    public Vector2 MinMaxSpeed = new Vector2(-0.05f,1);

    [Header("Lives")]
    public int MistakesAllowed = 2;

    [Header("Gameplay")]
    public int StartDebris = 2000;


    void Start()
    {
        GameHandler.HANDLER.Set_Settings(MovementSpeed, RotationSpeed, MinMaxSpeed, StartDebris, MistakesAllowed);
    }
}
