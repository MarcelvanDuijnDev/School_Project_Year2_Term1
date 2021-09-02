using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLaunch : MonoBehaviour
{
    //rocket
    [SerializeField] GameObject rocketPrefab;
    [SerializeField] float rocketSpeed;
    [SerializeField] Transform launchPosition;
    Vector3 launchDirection;


    //trajectory
    /*    [SerializeField] GameObject dotPrefab;
        [SerializeField] int numOfDots;
        [SerializeField] float distanceBetweenDots;
        private Vector3 trajectoryPoint;*/
    [SerializeField] GameObject arrow;


    
    void Start()
    {
        //trajectoryPoint = launchPosition.position;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            arrow.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            LaunchRocket();
            arrow.SetActive(false);
        }
    }


    public void LaunchRocket()
    {
        //set flying direction
        launchDirection = Vector3.up;

        //instantiate rocket above platform with speed and direction
        Instantiate(rocketPrefab, launchPosition.position, transform.rotation, transform);

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
