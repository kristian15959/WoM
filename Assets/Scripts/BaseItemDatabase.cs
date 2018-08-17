using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class BaseItemDatabase : MonoBehaviour {

    public TextAsset itemInventory;
    public static List<BaseItem> inventoryItems = new List<BaseItem>();

    private List<Dictionary<string, string>> inventoryItemsDictionary = new List<Dictionary<string, string>>();
    private Dictionary<string, string> inventoryDictionary;

    void Awake()
    {
        ReadItemsFromDatabase();
        for(int i = 0; i < inventoryItemsDictionary.Count; i++) {
            inventoryItems.Add(new BaseItem(inventoryItemsDictionary[i]));
        }
        DebugInvetoryItems();
    }

    public void ReadItemsFromDatabase() {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(itemInventory.text);
        XmlNodeList itemList = xmlDocument.GetElementsByTagName("Item");

        foreach(XmlNode itemInfo in itemList) {
            XmlNodeList itemContent = itemInfo.ChildNodes;
            inventoryDictionary = new Dictionary<string, string>(); //ItemName: TestItem

                
            foreach(XmlNode content in itemContent) {
                switch(content.Name) {
                                   
                case "ItemID":
                    inventoryDictionary.Add("ItemID", content.InnerText); break;
                case "ItemType":
                    inventoryDictionary.Add("ItemType", content.InnerText); break;
                case "ItemRequiredLevel":
                    inventoryDictionary.Add("ItemRequiredLevel", content.InnerText); break;
                case "ItemName":
                    inventoryDictionary.Add("ItemName", content.InnerText); break;
                }
            }

            inventoryItemsDictionary.Add(inventoryDictionary);
        }
    }

    public void DebugInvetoryItems()
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            Debug.Log("ID: "+"<color=red>" + inventoryItems[i].WriteItemID() + "</color>" + " | " +
                      "Item Type: " + "<color=blue>" + inventoryItems[i].WriteItemType() + "</color>" + " | " +
                      "Lvl: " + "<color=blue>" + inventoryItems[i].WriteItemRequiredLevel() + "</color>" + " | " +
                      "Item Name: " + "<color=blue>" + inventoryItems[i].WriteItemName() + "</color>" + " | ");
        }
    }
}