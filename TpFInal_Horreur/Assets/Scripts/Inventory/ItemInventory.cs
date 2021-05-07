using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    [SerializeField] private ItemObject itemObject;
    private GameObject itemModel;

    private void Start()
    {
        if (itemObject.itemStatut == ItemObject.ItemStatut.InGame)
        {
            GenerateObject();
        }
        else if (itemObject.itemStatut == ItemObject.ItemStatut.InInventory)
        {
            InventoryItem();
        }
    }

    void GenerateObject()
    {
        itemModel = GameObject.Instantiate(itemObject.itemModel);
    }
    void InventoryItem()
    {
        if (itemModel != null)
        {
            Destroy(itemModel);
        }

        //Item dans linventaire
    }
}
