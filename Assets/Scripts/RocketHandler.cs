using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyUp : MonoBehaviour
{

    [SerializeField] float rocketSpeed;

    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Vector3.up * rocketSpeed * Time.deltaTime);
    }

}
