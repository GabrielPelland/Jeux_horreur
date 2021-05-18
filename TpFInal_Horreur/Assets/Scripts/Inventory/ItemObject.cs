using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Items", order = 1)]
public class ItemObject : ScriptableObject
{
    public GameObject itemModel;
    public GameObject itemIcon;
    public ItemStatut itemStatut;
    public ItemType itemType;

    public enum ItemStatut
    {
        InGame,
        InInventory
    }
    public enum ItemType
    {
        Cle,
        Batterie,
        Lampe,
        Carte
    }
}
