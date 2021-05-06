/* Adapte de:
 * https://www.youtube.com/watch?v=E5zNi_SSP_w
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //VARIABLES hahahaa
    //Parametres
    private float sensX;
    private float sensY;
    float multiplier;

    //Donnee
    [SerializeField] Transform cam = null;
    [SerializeField] Transform orientation = null;

    //Entree souris
    float mouseX;
    float mouseY;

    //Angle de rotation
    float xRotation;
    float yRotation;

    //Script
    [SerializeField] private GM gm;

    //Commencement du jeu
    private void Start()
    {
        sensX = gm.sensX;
        sensY = gm.sensY;
        multiplier = gm.multiplier;

        //Curseur
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Sur updates
    private void Update()
    {
        //Mouvement mouse
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        //Rotation mouse
        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        //Limitations rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotation par defaut
        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //Rotation jouer defaut
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);


    }
}
