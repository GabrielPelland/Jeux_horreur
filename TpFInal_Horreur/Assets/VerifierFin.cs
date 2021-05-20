using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifierFin : MonoBehaviour
{
    public Canvas victory;
    public Canvas needKeys;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GM.i.nbKeyTrack < 5)
            {
                victory.enabled = true;
            }
            else
            {
                needKeys.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GM.i.nbKeyTrack < 5)
            {
                victory.enabled = false ;
            }
            else
            {
                needKeys.enabled = false;
            }
        }
    }

}
