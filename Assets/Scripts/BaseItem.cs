using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem {

    private int itemID;
    private int itemRequiredLevel;
    private string itemName;
    private string itemDescription;    
    private int itemDamage;

    public enum ItemTypes{
        ARMOR,
        WEAPON,
        SCROLL,
        POTION,
        CHEST
    }

    private ItemTypes itemType;

    public BaseItem(Dictionary<string, string> itemsDictionary) {
        itemName = itemsDictionary["ItemName"];
        itemID = int.Parse(itemsDictionary["ItemID"]);
        itemRequiredLevel = int.Parse(itemsDictionary["ItemRequiredLevel"]);
        itemType = (ItemTypes)System.Enum.Parse(typeof(BaseItem.ItemTypes), itemsDictionary["ItemType"].ToString());
    }    

    public int WriteItemID() {
        return itemID;
    }

    public string WriteItemType() {
        return itemType.ToString();
    }

    public int WriteItemRequiredLevel()    {
        return itemRequiredLevel;
    }

    public string WriteItemName()    {
        return itemName;
    }
}
