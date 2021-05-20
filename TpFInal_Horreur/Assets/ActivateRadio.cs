using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivateRadio : MonoBehaviour
{
    private AudioSource source;
    public TextMeshProUGUI text;
    public bool canActivateRadio = false;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            text.enabled = true;
            canActivateRadio = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            text.enabled = true;
            canActivateRadio = false;
        }
    }

    public void PlayRadio()
    {
        source.Play();
    }

    private void Update()
    {
        if(canActivateRadio)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GetComponentInParent<Interactible>().OnTriggerSound();
            }
        }
    }
}
