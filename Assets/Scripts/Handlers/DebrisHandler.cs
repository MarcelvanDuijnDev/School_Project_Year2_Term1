using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisHandler : MonoBehaviour
{
    public static DebrisHandler DEBRIS;

    [Header("CenterPoint")]
    [SerializeField] private Transform _EarthCenter;
    [SerializeField] private float _Radius;
    [SerializeField] private float _Angle;

    [Header("Settings")]
    [SerializeField] private Vector2 _MinMaxSpeed;
    [SerializeField] private List<int> _DebrisPrefabID = new List<int>();

    [Header("Debris")]
    [SerializeField] private List<DebrisObject> _Debris = new List<DebrisObject>();

    void Awake()
    {
        DEBRIS = this;
    }

    void Update()
    {
        for (int i = 0; i < _Debris.Count; i++)
        {
            _Debris[i].DebrisObj.transform.RotateAround(_EarthCenter.position, _Debris[i].MoveDirection, _Debris[i].Speed);
        }
    }

    public void Add_Debris(Transform startpoint)
    {
        GameObject newdebris = ObjectPool.POOL.GetObject(_DebrisPrefabID[Random.Range(0, _DebrisPrefabID.Count)], true);
        newdebris.transform.position = startpoint.position;
        DebrisObject newdebrisobj = new DebrisObject();
        newdebrisobj.DebrisObj = newdebris.transform;
        newdebrisobj.Speed = Random.Range(_MinMaxSpeed.x, _MinMaxSpeed.y);
        newdebrisobj.MoveDirection = new Vector3(Random.Range(-360,360), Random.Range(-360, 360), Random.Range(-360, 360));
        _Debris.Add(newdebrisobj);
    }
    public void Add_DebrisPos(Vector3 startpoint)
    {
        GameObject newdebris = ObjectPool.POOL.GetObject(_DebrisPrefabID[Random.Range(0, _DebrisPrefabID.Count)], true);
        newdebris.transform.position = startpoint;
        DebrisObject newdebrisobj = new DebrisObject();
        newdebrisobj.DebrisObj = newdebris.transform;
        newdebrisobj.Speed = Random.Range(_MinMaxSpeed.x, _MinMaxSpeed.y);
        newdebrisobj.MoveDirection = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
        _Debris.Add(newdebrisobj);
    }

    /*
    private void OnDrawGizmos()
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < 360; i++)
        {
            Vector3 pos;
            pos.x = _EarthCenter.position.x + _Radius * Mathf.Sin(i * Mathf.Deg2Rad);
            pos.y = _EarthCenter.position.y + _Radius * Mathf.Cos(i * Mathf.Deg2Rad);
            pos.z = _EarthCenter.position.z + _Radius * Mathf.Cos(_Angle * Mathf.Deg2Rad);
            points.Add(pos);
        }

        for (int i = 0; i < points.Count; i++)
        {
            Gizmos.DrawSphere(points[i], 1);
        }
    }
    */

    public void Reset()
    {
        for (int i = 0; i < _Debris.Count; i++)
        {
            _Debris[i].DebrisObj.gameObject.SetActive(false);
        }

        _Debris.Clear();
    }

    public void Set_Settings(Vector2 minmaxspeed)
    {
        _MinMaxSpeed = minmaxspeed;
    }
}

[System.Serializable]
public class DebrisObject
{
    public Transform DebrisObj;
    public Vector3 MoveDirection;
    public float Speed;
}