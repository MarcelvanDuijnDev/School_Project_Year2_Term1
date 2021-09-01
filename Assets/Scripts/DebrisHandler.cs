using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisHandler : MonoBehaviour
{
    public static DebrisHandler DEBRIS;

    [SerializeField] private List<DebrisObject> _Debris = new List<DebrisObject>();

    void Start()
    {
        DEBRIS = this;
    }

    void Update()
    {
        for (int i = 0; i < _Debris.Count; i++)
        {
            //_Debris[i].DebrisObj.transform.
        }
    }

    public void Add_Debris(Transform debrisobj)
    {
        DebrisObject newdebrisobj = new DebrisObject();
        newdebrisobj.DebrisObj = debrisobj;
        //newdebrisobj
        newdebrisobj.Height = 0;
    }
}

[System.Serializable]
public class DebrisObject
{
    public Transform DebrisObj;
    public Vector3 MoveDirection;
    public float Height;
}