/* Adapte de:
 * https://www.youtube.com/watch?v=E5zNi_SSP_w
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    //VARIABLES
    [SerializeField] GameObject objectHoler;

    //Entree mouse
    float mouseX;
    float mouseY;

    //Angle de rotation
    float xRotation;
    float yRotation;

    //Sur updates
    private void Update()
    {
        //Mouvement souris
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        //Rotation souris
        yRotation += mouseX * GM.i.sensX * GM.i.multiplier;
        xRotation -= mouseY * GM.i.sensY * GM.i.multiplier;

        //Limitations rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotation par defaut
        objectHoler.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //Rotation jouer defaut
        objectHoler.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}