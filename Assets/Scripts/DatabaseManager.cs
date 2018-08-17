using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DatabaseManager {

    public static string LoginURL = "http://wom.nive.sk/Login.php";
    public static string CharactersURL = "http://wom.nive.sk/CharactersData.php";
    public static string CreateCharacterURL = "http://wom.nive.sk/CreateCharacter.php";
    public static string CreateUserURL = "http://wom.nive.sk/InsertUser.php";
    public static string LoadItemsDataURL = "http://wom.nive.sk/ItemData.php";
    
    public static string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }
}
