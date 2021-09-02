using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisHandler : MonoBehaviour
{
    public static DebrisHandler DEBRIS;

    [Header("CenterPoint")]
    [SerializeField] private Transform _EarthCenter;

    [Header("Settings")]
    [SerializeField] private Vector2 _MinMaxSpeed;
    [SerializeField] private GameObject _DebrisPrefab = null;

    [Header("Debris")]
    [SerializeField] private List<DebrisObject> _Debris = new List<DebrisObject>();

    [Header("Testing")]
    [SerializeField] private Transform _SpawnPoint = null;


    void Start()
    {
        DEBRIS = this;
    }

    void Update()
    {
        for (int i = 0; i < _Debris.Count; i++)
        {
            _Debris[i].DebrisObj.transform.RotateAround(_EarthCenter.position, _Debris[i].MoveDirection, _Debris[i].Speed);
        }


        //Testing
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Add_Debris(_SpawnPoint.transform);
        }
    }

    public void Add_Debris(Transform startpoint)
    {
        GameObject newdebris = ObjectPool.POOL.GetObject(_DebrisPrefab);
        newdebris.transform.position = _SpawnPoint.position;
        DebrisObject newdebrisobj = new DebrisObject();
        newdebrisobj.DebrisObj = newdebris.transform;
        newdebrisobj.Speed = Random.Range(_MinMaxSpeed.x, _MinMaxSpeed.y);
        newdebrisobj.MoveDirection = new Vector3(Random.Range(-360,360), Random.Range(-360, 360), Random.Range(-360, 360));
        _Debris.Add(newdebrisobj);
    }
}

[System.Serializable]
public class DebrisObject
{
    public Transform DebrisObj;
    public Vector3 MoveDirection;
    public float Speed;
}