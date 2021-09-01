using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisCollector : MonoBehaviour
{
    [SerializeField] private float _CollectRange;
    [SerializeField] private LayerMask _DebrisLayer;

    [SerializeField] private List<Transform> _ObjectsInRange = new List<Transform>();


    void Start()
    {
        
    }


    void Update()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position,_CollectRange);
    }
}
