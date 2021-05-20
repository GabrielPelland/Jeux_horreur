using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Inventory
    public GameObject[] inventoryItem;
    public GameObject[] inventorySlot;
    public bool[] isFull;
    int keyIndexSlot = 0;

    //UI
    [SerializeField] GameObject basicSlot;
    [SerializeField] GameObject selectSlot;

    //Sprites
    private GameObject basicSlotSprite;
    private GameObject selectSlotSprite;
    private GameObject slotCenter;
    private GameObject objectHolder;

    //Positions
    private GameObject instancePosition;
    private GameObject cameraPosition;
    private GameObject instanceItem;

    //Selected slot
    private GameObject selectedSlotPosition;

    //Item id
    int currentItemId;

    private void Start()
    {
        //Assign variables
        inventoryItem = new GameObject[GM.i.nbSlot];
        inventorySlot = new GameObject[GM.i.nbSlot];
        objectHolder = GameObject.Find("ObjectPosition");
        instancePosition = GameObject.Find("InstancePosition");
        cameraPosition = GameObject.Find("CameraPosition");
        slotCenter = GameObject.Find("Slots");

        isFull = new bool[GM.i.nbSlot];

        //Reset Isfull
        for (int i = 0; i < isFull.Length; i++)
        {
            isFull[i] = false;
        }

        //Call fonction
        CreateInventory();
    }

    private void Update()
    {
        PressKey();
    }

    //Create iventory with GameMaster
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

    //Detect if you press key for inventory
    void PressKey()
    {
        int keySelectSlot = 0;

        for (int i = 0; i < (inventorySlot.Length + 1); i++)
        {
            //Check index + key press
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

                //Selected slot
                selectSlotSprite = GameObject.Instantiate(selectSlot);
                selectSlotSprite.transform.position = inventorySlot[keyIndexSlot].transform.position;
                selectSlotSprite.transform.SetParent(inventorySlot[keyIndexSlot].transform.GetChild(0));

                InHandItem(keyIndexSlot);
            }
            //Check use
            else if (Input.GetKey(KeyCode.E))
            {
                if(GameObject.Find("Inventory_Slot_selected(Clone)") != null)
                {
                    //Check if there is children ( items ) in slot
                    if (GameObject.Find("Inventory_Slot_selected(Clone)").transform.parent.transform.parent.transform.childCount > 1)
                    {
                        UseItem();
                    }
                }
            }
        }
    }

    //Use items
    void UseItem ()
    {
        //Check if there is children
        if (GameObject.Find("Inventory_Slot_selected(Clone)").transform.parent.transform.parent.GetChild(1) != null)
        {
            //Find the itemInventory object
            selectedSlotPosition = GameObject.Find("Inventory_Slot_selected(Clone)").transform.parent.transform.parent.GetChild(2).gameObject;
            currentItemId = (int)selectedSlotPosition.GetComponent<ItemInventory>().itemObject.itemType;

            //Check item id
            switch (currentItemId)
            {
                case 2:
                    //Use battery
                    GetComponent<ItemLight>().ResetTimeLight();

                    //Destroy after used
                    Destroy(GameObject.Find("Inventory_Slot_selected(Clone)").transform.parent.transform.parent.GetChild(1).gameObject);
                    Destroy(GameObject.Find("Inventory_Slot_selected(Clone)").transform.parent.transform.parent.GetChild(2).gameObject);
                    isFull[keyIndexSlot] = false;
                    DestroyHandObject();
                    break;
                case 3:
                    //Open light
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
    
    //Destroy object in hand
    void DestroyHandObject()
    {
        foreach (Transform child in objectHolder.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
