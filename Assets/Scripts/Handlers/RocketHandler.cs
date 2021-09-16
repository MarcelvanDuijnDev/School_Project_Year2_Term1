using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHandler : MonoBehaviour
{

    //rocket movement variables
    [SerializeField] float rocketSpeed;

    //rocket collision
    [SerializeField] int amountOfDebris;

    //rocket stages
    [SerializeField] GameObject middleStage;
    [SerializeField] GameObject bottomStage;

    [SerializeField] GameObject middleStageDrop;
    [SerializeField] GameObject bottomStageDrop;

    [SerializeField] float stage1Seperation;
    [SerializeField] float stage2Seperation;
    private float _timer;
    private int stage = 0;
    private bool stage1sep = false;
    private bool stage2sep = false;

    private bool _Launched = false;
    private Vector3 _SpawnDebrisPoint;

    void Update()
    {
        FlyUp();
        SeperateStage();

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


        _timer += 1 * Time.deltaTime;
        if (_timer >= stage1Seperation && !stage1sep)
        {
            stage = 1;
            stage1sep = true;
        }
        if (_timer >= stage2Seperation && !stage2sep)
        {
            stage = 2;
            stage2sep = true;
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


    void SeperateStage()
    {
        switch (stage)
        {
            case 0:
                break;
            case 1:
                bottomStage.SetActive(false);
                Instantiate(bottomStageDrop, bottomStage.transform.position, bottomStage.transform.rotation, transform.parent.parent.parent);
                AudioHandler.AUDIO.PlayTrack("RocketDecouple");
                Debug.Log("Stage 1 Separated");
                stage = 0;
                break;
            case 2:
                middleStage.SetActive(false);
                Instantiate(middleStageDrop, middleStage.transform.position, middleStage.transform.rotation, transform.parent.parent.parent);
                Debug.Log("Stage 2 Separated");
                stage = 0;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Astroid")
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            GameHandler.HANDLER.RocketExploded();
            SpawnDebris(amountOfDebris);
            DataHandler.STATS._SaveData.saveData[DataHandler.STATS._SaveData.saveData.Count - 1].RocketCollisions++;
            AudioHandler.AUDIO.PlayTrack("RocketExplotion");
        }
    }
}
