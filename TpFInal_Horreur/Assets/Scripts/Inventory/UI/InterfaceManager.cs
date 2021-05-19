using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public GameObject notificationAttention;
    public GameObject notificationObjectif;
    public GameObject notificationMort;
    public GameObject notificationFin;

    public GameObject uiTouches;

    public GameObject positionTop;
    public GameObject positionCenter;

    GameObject activeUI;

    public void Afficher(string nomUi)
    {
        switch (nomUi)
        {
            case "Attention" :
                activeUI = GameObject.Instantiate(notificationAttention);
                activeUI.transform.position = positionTop.transform.position;
                Invoke("RemoveUi", 5f);
                break;
            case "Objectif" :
                activeUI = GameObject.Instantiate(notificationObjectif);
                activeUI.transform.position = positionTop.transform.position;
                Invoke("RemoveUi", 5f);
                break;
            case "Mort" :
                activeUI = GameObject.Instantiate(notificationMort);
                activeUI.transform.position = positionCenter.transform.position;
                Invoke("RemoveUi", 5f);
                break;
            case "Victoire" :
                activeUI = GameObject.Instantiate(notificationFin);
                activeUI.transform.position = positionCenter.transform.position;
                Invoke("RemoveUi", 5f);
                break;
        } 
    }
    
    void RemoveUi()
    {
        Destroy(activeUI);
    }
}
