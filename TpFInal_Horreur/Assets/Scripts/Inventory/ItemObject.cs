using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Items", order = 1)]
public class ItemObject : ScriptableObject
{
    public GameObject itemModel;
    public Sprite itemIcon;
    public ItemStatut itemStatut;
    public ItemType itemType;

    public enum ItemStatut
    {
        InGame,
        Inventory
    }
    public enum ItemType
    {
        Cle,
        Batterie,
        Lampe,
        Carte
    }
}
