using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebris : MonoBehaviour
{
    [SerializeField] private int _SpawnAmount;
    [SerializeField] private Transform _SpawnPoint;

    private bool _Start;

    private void Update()
    {
        if(!_Start)
        {
            for (int i = 0; i < _SpawnAmount; i++)
            {
                transform.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                DebrisHandler.DEBRIS.Add_Debris(_SpawnPoint);
            }
            _Start = true;
        }
    }
}
