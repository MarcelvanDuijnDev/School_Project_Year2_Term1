using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHandler : MonoBehaviour
{

    //rocket movement variables
    [SerializeField] float rocketSpeed;

    //rocket collision
    [SerializeField] int amountOfDebris;


    private bool _Launched = false;

    void Update()
    {
        FlyUp();

        if (!_Launched)
            if (Vector3.Distance(transform.position, GameHandler.HANDLER.Earth.transform.position) >= 10)
            {
                GameHandler.HANDLER.RocketLaunched();
                _Launched = true;
            }
    }


    void FlyUp()
    {
        transform.Translate(Vector3.up * rocketSpeed * Time.deltaTime);
    }

    void SpawnDebris(int _amountOfDebris)
    {
        for (int i = 0; i < _amountOfDebris; i++)
        {
            DebrisHandler.DEBRIS.Add_Debris(transform);
            Debug.Log(transform.gameObject.name);
        }
    }

/*    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Astroid")
        {
            Destroy(collision.gameObject);
            SpawnDebris(amountOfDebris);
            Debug.Log("collision");
        }
    }*/


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Astroid")
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            GameHandler.HANDLER.RocketExploded();
            SpawnDebris(amountOfDebris);
            Debug.Log("collision");
        }
    }



}
