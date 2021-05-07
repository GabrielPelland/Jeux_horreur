﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    [SerializeField] private ItemObject itemObject;
    private GameObject itemModel;
    private GameObject itemIcon;

    GameObject inventoryObject;
    Inventory inventory;

    private void Start()
    {
        itemObject.itemStatut = ItemObject.ItemStatut.InGame;

        Inventaire();
        inventoryObject = GameObject.Find("Inventory");
        inventory = inventoryObject.GetComponent<Inventory>();
    }

    void Inventaire()
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
        itemModel.transform.parent = this.transform;
    }

    void InventoryItem()
    {
        GameObject slotChild;

        for (int i = 0; i < inventory.inventoryItem.Length; i++)
        {
            print("boucle");
            if (inventory.isFull[i] == false)
            {
                itemIcon = GameObject.Instantiate(itemObject.itemIcon);
                slotChild = inventory.inventorySlot[i].transform.GetChild(0).gameObject;
                itemIcon.transform.parent = inventory.inventorySlot[i].transform;
                itemIcon.transform.position = new Vector3(slotChild.transform.position.x, slotChild.transform.position.y, slotChild.transform.position.z);
                inventory.isFull[i] = true;
                break;
            }
        }

        Destroy(itemModel);

    }

    public void CollectItem()
    {
        itemObject.itemStatut = ItemObject.ItemStatut.InInventory;
        Inventaire();
    }


}
