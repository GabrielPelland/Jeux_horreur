using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushPoint : MonoBehaviour
{
    public enum Type { Ambush, Run };
    public Type type;

    public Transform secondRunPoint;
    public bool triggered = false;

    public BoxCollider TriggerBox;

    private void Start()
    {
        if(type == Type.Run)  {
            secondRunPoint = transform.GetChild(0);
        }
    }

    
}
