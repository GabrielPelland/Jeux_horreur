using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //VARIABLES
    //Position camera
    [SerializeField] Transform cameraPosition;

    //Sur updates
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
