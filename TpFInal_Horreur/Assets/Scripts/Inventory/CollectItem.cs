using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    GameObject itemCollision;

    //On collision
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.parent != null)
        {
            itemCollision = collision.transform.parent.gameObject;

            if (itemCollision.GetComponent<ItemInventory>() != null)
                {
                    itemCollision.GetComponent<ItemInventory>().CollectItem();
                }
        }
    }
}
