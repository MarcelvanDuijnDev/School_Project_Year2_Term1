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
    private Vector3 _SpawnDebrisPoint;

    void Update()
    {
        FlyUp();

        if (!_Launched)
            if (Vector3.Distance(transform.position, GameHandler.HANDLER.Earth.transform.position) >= 10)
            {
                GameHandler.HANDLER.RocketLaunched();
                DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].RocketsLaunched++;
                _SpawnDebrisPoint = transform.position;
                StartCoroutine(SpawnDebrisAtSeperation());
                _Launched = true;
            }

        if (Vector3.Distance(transform.position, GameHandler.HANDLER.Earth.transform.position) >= 1000)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator SpawnDebrisAtSeperation()
    {

        yield return new WaitForSeconds(1);
        SpawnDebris(20, _SpawnDebrisPoint);
    }


    void FlyUp()
    {
        transform.Translate(Vector3.up * rocketSpeed * Time.deltaTime);
    }

    void SpawnDebris(int amount, Vector3 pos)
    {
        for (int i = 0; i < amount; i++)
        {
            DebrisHandler.DEBRIS.Add_DebrisPos(pos);
        }
    }

    void SpawnDebris(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            DebrisHandler.DEBRIS.Add_Debris(transform);
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
            DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].RocketCollisions++;
        }
    }



}
