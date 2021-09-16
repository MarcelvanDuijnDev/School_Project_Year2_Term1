using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverTrash : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    private Vector3 moveDirection;
    

    void Update()
    {
        MoveToEarth();
    }


    void MoveToEarth()
    {
        moveDirection = GameHandler.HANDLER.Earth.transform.position - transform.position;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, GameHandler.HANDLER.Earth.transform.rotation, 50f);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
