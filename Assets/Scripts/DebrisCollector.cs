using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisCollector : MonoBehaviour
{
    [SerializeField] private float _CollectRange = 5;
    [SerializeField] private float _CollectSpeed;
    [SerializeField] private LayerMask _DebrisLayer;
    [SerializeField] private int maxHoldableDebris;
    private int debrisInInventory;

    //Objects
    private Collider[] _ObjectsInRange;

    private void Start()
    {
        GameHandler.maxHoldableDebris = maxHoldableDebris;
    }


    void Update()
    {
        _ObjectsInRange = Physics.OverlapSphere(transform.position, _CollectRange, _DebrisLayer);

        for (int i = 0; i < _ObjectsInRange.Length; i++)
        {
            _ObjectsInRange[i].transform.position = Vector3.MoveTowards(_ObjectsInRange[i].transform.position, transform.position, _CollectSpeed);

            if (Vector3.Distance(transform.position, _ObjectsInRange[i].transform.position) <= 0.5f && debrisInInventory < maxHoldableDebris)
            {
                GameHandler.DebrisCollected++;
                GameHandler.DebrisInInventory++;
                _ObjectsInRange[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Vector4(0, 1, 0, 0.1f);
        Gizmos.DrawSphere(transform.position,_CollectRange);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GarbageDump" && Input.GetKeyDown(KeyCode.Space))
        {
            GameHandler.DebrisInInventory = 0;
            Debug.Log("dumped");
        }
    }

}
