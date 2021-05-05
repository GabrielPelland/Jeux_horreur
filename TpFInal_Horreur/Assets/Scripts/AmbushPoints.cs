using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushPoints : MonoBehaviour
{
    public List<AmbushPoint> points;
    public int currentID = 0;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            points.Add(child.GetComponent<AmbushPoint>());
        } 
    }
}
