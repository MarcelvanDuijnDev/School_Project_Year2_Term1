using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLaunch : MonoBehaviour
{
    //rocket
    [SerializeField] float rocketSpeed;
    [SerializeField] Transform launchPosition;
    Vector3 launchDirection;

    [Header("Rocket")]
    [SerializeField] private bool _UseRandom = true;
    [SerializeField] private int _RocketID;
    [SerializeField] private List<GameObject> _RocketPrefabs = new List<GameObject>();

    //trajectory
    /*    [SerializeField] GameObject dotPrefab;
        [SerializeField] int numOfDots;
        [SerializeField] float distanceBetweenDots;
        private Vector3 trajectoryPoint;*/



    


    void Update()
    {

    }


    public void LaunchRocket()
    {
        //set flying direction
        launchDirection = Vector3.up;

        //instantiate rocket above platform with speed and direction

        int rocketid = 0;
        if (_UseRandom)
            rocketid = Random.Range(0, _RocketPrefabs.Count);
        else
            rocketid = _RocketID;

        Instantiate(_RocketPrefabs[rocketid], launchPosition.position, transform.rotation, transform);

    }


    

/*    void DrawTrajectoryLine()
    {
        for (int i = 0; i < numOfDots; i++)
        {
            //trajectoryPoint = launchPosition.position + new Vector3(0, (distanceBetweenDots * i), 0);
            Instantiate(dotPrefab, trajectoryPoint, transform.rotation, transform);
            trajectoryPoint += Vector3.up * distanceBetweenDots;
            Debug.Log("dot instantiated");
        }
    }*/
}
