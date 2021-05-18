using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    GameObject itemCollision;

    private void OnTriggerEnter(Collider collision)
    {
        print("collison");

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
