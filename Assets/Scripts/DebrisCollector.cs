using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisCollector : MonoBehaviour
{
    [SerializeField] private float _CollectRange = 5;
    [SerializeField] private float _CollectSpeed;
    [SerializeField] private LayerMask _DebrisLayer;
    [SerializeField] private GameObject trashSpaceShip;
    [SerializeField] private Transform trashShipSpawn;

    private bool _CargoFullCheck = false;

    //Objects
    private Collider[] _ObjectsInRange;

    void Update()
    {
        _ObjectsInRange = Physics.OverlapSphere(transform.position, _CollectRange, _DebrisLayer);

        if (GameHandler.DebrisInInventory < GameHandler.MaxHoldableDebris)
        {
            for (int i = 0; i < _ObjectsInRange.Length; i++)
            {
                _ObjectsInRange[i].transform.position = Vector3.MoveTowards(_ObjectsInRange[i].transform.position, transform.position, _CollectSpeed);

                if (Vector3.Distance(transform.position, _ObjectsInRange[i].transform.position) <= 1f)
                {
                    if (GameHandler.DebrisInInventory < GameHandler.MaxHoldableDebris)
                    {
                        GameHandler.DebrisCollected++;
                        GameHandler.DebrisInInventory++;
                        AudioHandler.AUDIO.PlayTrack("CollectDebris");
                        _ObjectsInRange[i].gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            if(!_CargoFullCheck)
            {
                AudioHandler.AUDIO.PlayTrack("SpaceShipFull");
                _CargoFullCheck = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Vector4(0, 1, 0, 0.1f);
        Gizmos.DrawSphere(transform.position,_CollectRange);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "GarbageDump" && Input.GetKey(KeyCode.Space))
        {
            GameHandler.DebrisInInventory = 0;
            Instantiate(trashSpaceShip, trashShipSpawn.position, trashSpaceShip.transform.rotation, transform.parent);
            AudioHandler.AUDIO.PlayTrack("Dropoff");
            _CargoFullCheck = false;
        }
    }

    public void Set_Settings(float range)
    {
        _CollectRange = range;
    }
}
