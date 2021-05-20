using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    //Variables
    public ItemObject itemObject;
    private GameObject itemModel;
    private GameObject itemHand;
    private GameObject itemIcon;
    private GameObject itemObjectHolder;

    //inventory
    GameObject inventoryObject;
    Inventory inventory;

    //On start
    private void Start()
    {
        //Change item statut
        itemObject.itemStatut = ItemObject.ItemStatut.InGame;

        Inventaire();

        //Find variables
        inventoryObject = GameObject.Find("Inventory");
        inventory = inventoryObject.GetComponent<Inventory>();
        itemObjectHolder = GameObject.Find("ObjectPosition");
    }

    //Check item statut
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

    //Generate object on ground
    void GenerateObject()
    {
        itemModel = GameObject.Instantiate(itemObject.itemModel);
        itemModel.transform.position = this.transform.position; 
        itemModel.transform.parent = this.transform;
    }

    //Generate object in inventory
    void InventoryItem()
    {
        GameObject slotChild;

        for (int i = 0; i < inventory.inventoryItem.Length; i++)
        {
            //check slot full
            if (inventory.isFull[i] == false)
            {
                //instantiate itemIcon
                itemIcon = GameObject.Instantiate(itemObject.itemIcon);

                slotChild = inventory.inventorySlot[i].transform.GetChild(0).gameObject;

                itemIcon.transform.SetParent(inventory.inventorySlot[i].transform);
                itemIcon.transform.position = new Vector3(slotChild.transform.position.x, slotChild.transform.position.y, slotChild.transform.position.z);

                this.transform.parent = inventory.inventorySlot[i].transform;
                inventory.inventoryItem[i] = this.gameObject;

                inventory.isFull[i] = true;
                break;
            }
        }
        Destroy(itemModel);
    }

    //Instantiate object in hand
    public void InHandObject()
    {
        itemHand = GameObject.Instantiate(itemObject.itemModel);
        itemHand.transform.position = new Vector3(itemObjectHolder.transform.position.x, itemObjectHolder.transform.position.y, itemObjectHolder.transform.position.z);
        itemHand.transform.rotation = itemObjectHolder.transform.rotation;
        itemHand.transform.parent = itemObjectHolder.transform;
    }
    
    //Collect item (from other player)
    public void CollectItem()
    {
        itemObject.itemStatut = ItemObject.ItemStatut.InInventory;
        Inventaire();
    }


}
