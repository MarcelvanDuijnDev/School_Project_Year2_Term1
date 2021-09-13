using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDetach : MonoBehaviour
{

    [SerializeField] float dropSpeed;
    [SerializeField] int amountOfDebris;
    private bool isDropping;

    private void Start()
    {
        isDropping = true;
    }

    void Update()
    {
        //make stage fall to earth
        if (isDropping)
        {
            transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);
        }
        breakIntoDebris();
    }


    void breakIntoDebris()
    {
        Debug.Log(Vector3.Distance(transform.position, GameHandler.HANDLER.Earth.transform.position));
        if (Vector3.Distance(transform.position, GameHandler.HANDLER.Earth.transform.position) <= 60)
        {
            isDropping = false;
            SpawnDebris(amountOfDebris, transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
            Debug.Log("Broke up");
            //TODO: add particle effects
        }
    }


    void SpawnDebris(int amount, Vector3 pos)
    {
        for (int i = 0; i < amount; i++)
        {
            DebrisHandler.DEBRIS.Add_DebrisPos(pos);
        }
    }



}
