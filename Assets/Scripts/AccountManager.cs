using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountManager : MonoBehaviour {

    public int userID;
    public int userSelectedCharacterID;

    public string[] baseCharactersData;

    public void Logout()
    {
        userID = 0;
        userSelectedCharacterID = 0;
        baseCharactersData = null;
        foreach (Transform item in GetComponent<UIManager>().CharactersList.transform)
        {
            Destroy(item.gameObject);
        }
        Debug.Log("<color=white>Logged Out.</color>");
        GetComponent<UIManager>().DebugTextUI.text = ("<color=white>Logged Out.</color>");
        GetComponent<UIManager>().LoginScreen("Logged Out.");
    }

    public IEnumerator LoadCharacters()
    {
        GetComponent<UIManager>().loadingCharactersScreen.SetActive(true);

        WWWForm form = new WWWForm();
        form.AddField("userIDPost", userID);
        GetComponent<UIManager>().loadingCharactersInfoText.text = "Looking for characters.";

        WWW www = new WWW(DatabaseManager.CharactersURL, form);
        yield return www;
        string charactersDataString = www.text;
        baseCharactersData = charactersDataString.Split(';');
        yield return new WaitForSeconds(0.1f);
        GetComponent<UIManager>().loadingCharactersInfoText.text = DatabaseManager.GetDataValue(baseCharactersData[0], "MESSAGE:") + ".";
        GetComponent<UIManager>().DebugTextUI.text = ("<color=white>Logged Out.</color>");
        Debug.Log("<color=green>"+www.text+"</color>");
        if (DatabaseManager.GetDataValue(baseCharactersData[0], "MESSAGE:") == "Characters Loaded")
        {
            for (int i = 1; i < baseCharactersData.Length -1; i++)
            {
                if (DatabaseManager.GetDataValue(baseCharactersData[i], "ID:") != "0")
                {
                    GameObject charInstance = Instantiate(GetComponent<UIManager>().CharactersListItem, GetComponent<UIManager>().CharactersList.transform);
                    CharacterSelectionListItem charSelectionItemInstance = charInstance.GetComponent<CharacterSelectionListItem>();
                    charSelectionItemInstance.id = int.Parse(DatabaseManager.GetDataValue(baseCharactersData[i], "ID:"));
                    charSelectionItemInstance._name.text = DatabaseManager.GetDataValue(baseCharactersData[i], "Name:");
                    charSelectionItemInstance._class.text = DatabaseManager.GetDataValue(baseCharactersData[i], "Class:");
                    charSelectionItemInstance._level.text = DatabaseManager.GetDataValue(baseCharactersData[i], "Level:") + "lvl";
                }                
            }            
        }

        yield return new WaitForSeconds(0.1f);
        GetComponent<UIManager>().loadingCharactersScreen.SetActive(false);
    }
}
