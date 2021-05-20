using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    //VARIABLES
    [SerializeField] GameObject objectHoler;

    //Position camera
    [SerializeField] Transform cameraPosition;

    //Sur updates
    void Update()
    {
        transform.rotation = cameraPosition.transform.rotation;
    }
}