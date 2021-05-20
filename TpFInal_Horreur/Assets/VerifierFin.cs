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
            print("ok");
            if (GM.i.nbKeyTrack < 5)
            {
                needKeys.gameObject.SetActive(true);
            }
            else
            {
                victory.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GM.i.nbKeyTrack < 5)
            {
                needKeys.gameObject.SetActive(false);
            }
            else
            {
                victory.gameObject.SetActive(false);
            }
        }
    }

}
