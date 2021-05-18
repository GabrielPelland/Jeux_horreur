using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Inventory
    public GameObject[] inventoryItem;
    public GameObject[] inventorySlot;
    public bool[] isFull;

    //UI
    [SerializeField] GameObject basicSlot;
    [SerializeField] GameObject selectSlot;

    private GameObject basicSlotSprite;
    private GameObject selectSlotSprite;
    private GameObject slotCenter;

    private void Start()
    {
        inventoryItem = new GameObject[GM.i.nbSlot];
        inventorySlot = new GameObject[GM.i.nbSlot];

        isFull = new bool[GM.i.nbSlot];

        for (int i = 0; i < isFull.Length; i++)
        {
            isFull[i] = false;
        }

        slotCenter = GameObject.Find("Slots");
        CreateInventory();
    }

    private void Update()
    {
        PressKey();
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

    void PressKey()
    {
        int keyIndexSlot = 0;
        int keySelectSlot = 0;

        for (int i = 0; i < (inventorySlot.Length + 1); i++)
        {
            if (i == 0)
            {
                keyIndexSlot = 0;
                keySelectSlot = 0;
            }
            else if (i >= 1)
            {
                keyIndexSlot = (i - 1);
                keySelectSlot = i;

            }


            if (Input.GetKeyDown((keySelectSlot).ToString()))
            {
                if (selectSlotSprite != null)
                {
                    Destroy(selectSlotSprite);
                }


                selectSlotSprite = GameObject.Instantiate(selectSlot);
                selectSlotSprite.transform.position = inventorySlot[keyIndexSlot].transform.GetChild(0).gameObject.transform.position;
                selectSlotSprite.transform.parent = inventorySlot[keyIndexSlot].transform.GetChild(0);
                InHandItem(keyIndexSlot);
            }
        }

    }

    void InHandItem(int i)
    {
        if (isFull[i] == true)
        {
            print("PLEIN");
            inventoryItem[i].GetComponent<ItemInventory>().InHandObject();
        }
        else if (isFull[i] == false)
        {
            print("vide");
        }
    }
}
