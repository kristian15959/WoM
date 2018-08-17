using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour {

    public string[] items;

	IEnumerator Start () {
        WWW itemsData = new WWW(DatabaseManager.LoadItemsDataURL);
        yield return itemsData;
        string itemsDataString = itemsData.text;
        print(itemsDataString);
        items = itemsDataString.Split(';');
        //print(GetDataValue(items[0], "Name:"));
	}

}
