using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create scriptable object
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Items", order = 1)]
public class ItemObject : ScriptableObject
{
    public GameObject itemModel;
    public GameObject itemIcon;
    public ItemStatut itemStatut;
    public ItemType itemType;

    //Statut
    public enum ItemStatut
    {
        InGame,
        InInventory
    }

    //Item type
    public enum ItemType
    {
        Cle = 1,
        Batterie = 2,
        Lampe = 3,
        Carte = 4
    }
}
