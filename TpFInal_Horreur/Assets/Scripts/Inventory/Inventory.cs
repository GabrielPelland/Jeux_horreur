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
    private GameObject objectHolder;

    private GameObject instancePosition;
    private GameObject cameraPosition;
    private GameObject instanceItem;

    private GameObject selectedSlotPosition;

    int currentItemId;

    private void Start()
    {
        inventoryItem = new GameObject[GM.i.nbSlot];
        inventorySlot = new GameObject[GM.i.nbSlot];
        objectHolder = GameObject.Find("ObjectPosition");
        instancePosition = GameObject.Find("InstancePosition");
        cameraPosition = GameObject.Find("CameraPosition");
        slotCenter = GameObject.Find("Slots");

        isFull = new bool[GM.i.nbSlot];

        for (int i = 0; i < isFull.Length; i++)
        {
            isFull[i] = false;
        }
        
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
            slotSpace = (120 * i) - 500;

            basicSlotSprite = GameObject.Instantiate(basicSlot);
            basicSlotSprite.transform.SetParent(slotCenter.transform);
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
                DestroyHandObject();
                if (selectSlotSprite != null)
                {
                    Destroy(selectSlotSprite);
                }

                selectSlotSprite = GameObject.Instantiate(selectSlot);
                selectSlotSprite.transform.position = inventorySlot[keyIndexSlot].transform.position;
                selectSlotSprite.transform.SetParent(inventorySlot[keyIndexSlot].transform.GetChild(0));

                InHandItem(keyIndexSlot);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                if(GameObject.Find("Inventory_Slot_selected(Clone)") != null)
                {
                    if (GameObject.Find("Inventory_Slot_selected(Clone)").transform.parent.transform.parent.transform.childCount > 1)
                    {
                        UseItem();
                    }
                }
            }
        }
    }


    void UseItem ()
    {
        if (GameObject.Find("Inventory_Slot_selected(Clone)").transform.parent.transform.parent.GetChild(1) != null)
        {
            selectedSlotPosition = GameObject.Find("Inventory_Slot_selected(Clone)").transform.parent.transform.parent.GetChild(2).gameObject;
            print(selectedSlotPosition);
            currentItemId = (int)selectedSlotPosition.GetComponent<ItemInventory>().itemObject.itemType;

            switch (currentItemId)
            {
                case 2:
                    GetComponent<ItemLight>().ResetTimeLight();

                    Destroy(GameObject.Find("Inventory_Slot_selected(Clone)").transform.parent.transform.parent.GetChild(1).gameObject);
                    Destroy(GameObject.Find("Inventory_Slot_selected(Clone)").transform.parent.transform.parent.GetChild(2).gameObject);

                    DestroyHandObject();
                    print("Batterie");
                    
                    break;
                case 3:
                    GetComponent<ItemLight>().FindLight();
                    print("Lamp");
                    break;
            }
        }
    }

    void InHandItem(int selectedSlot)
    {
        if (isFull[selectedSlot] == true)
        {
            inventoryItem[selectedSlot].GetComponent<ItemInventory>().InHandObject();
        }
    }

    void DestroyHandObject()
    {
        foreach (Transform child in objectHolder.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
