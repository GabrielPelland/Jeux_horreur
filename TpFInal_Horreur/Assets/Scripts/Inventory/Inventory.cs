using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Inventory
    [SerializeField] GameObject[] inventoryItem;
    [SerializeField] GameObject[] inventorySlot;

    //UI
    [SerializeField] GameObject basicSlot;
    [SerializeField] GameObject selectedSlot;

    private GameObject basicSlotSprite;
    private GameObject slotCenter;


    private void Start()
    {
        inventoryItem = new GameObject[GM.i.nbSlot];
        inventorySlot = new GameObject[GM.i.nbSlot];

        slotCenter = GameObject.Find("Slots");
        CreateInventory();
    }

    void CreateInventory()
    {
        int slotSpace;

        for (int i = 0; i < inventorySlot.Length; i++)
        {
            slotSpace = (175 * i) - 500;

            basicSlotSprite = GameObject.Instantiate(basicSlot);
            basicSlotSprite.transform.parent = slotCenter.transform;
            basicSlotSprite.transform.position = new Vector3(slotCenter.transform.position.x + slotSpace, slotCenter.transform.position.y, slotCenter.transform.position.z);

            inventorySlot[i] = basicSlotSprite;
        }
    }
}
