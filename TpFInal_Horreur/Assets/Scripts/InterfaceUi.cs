using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceUi : MonoBehaviour
{
    public  GameObject infos;
    public GameObject attention;
    public GameObject objectify;

    void Start()
    {
        infos.SetActive(true);
        attention.SetActive(false);
        objectify.SetActive(false);

        Invoke("Objectif", 4f);
    }

    void Objectif()
    {
        infos.SetActive(false);
        attention.SetActive(false);
        objectify.SetActive(true);

        Invoke("Attention", 6f);
    }

    void Attention()
    {
        infos.SetActive(false);
        attention.SetActive(true);
        objectify.SetActive(false);

        Invoke("End", 6f);
    }

    void End()
    {
        infos.SetActive(false);
        attention.SetActive(false);
        objectify.SetActive(false);
    }
}

