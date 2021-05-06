/* Adapte de:
 * https://www.youtube.com/watch?v=E5zNi_SSP_w
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    //VARIABLES
    //Parametres
    private float sensX;
    private float sensY;
    float multiplier;

    [SerializeField] GameObject objectHoler;

    //Entree mouse
    float mouseX;
    float mouseY;

    //Angle de rotation
    float xRotation;
    float yRotation;


    //Script
    [SerializeField] private GM gm;

    private void Start()
    {
        sensX = gm.sensX;
        sensY = gm.sensY;
        multiplier = gm.multiplier;
    }

    //Sur updates
    private void Update()
    {
        //Mouvement souris
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        //Rotation souris
        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        //Limitations rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotation par defaut
        objectHoler.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //Rotation jouer defaut
        objectHoler.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}