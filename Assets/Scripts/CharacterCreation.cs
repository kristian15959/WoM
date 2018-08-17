using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour {
    
    public InputField inputCharacterName;
    public string inputClass;

    public string[] baseCharactersData;

    void Start()
    {

    }

    public void SelectClass(string Class)
    {
        inputClass = Class;
    }

    public void CreateCharacter()
    {
        StartCoroutine(CreateCharacterDB(GetComponent<AccountManager>().userID, inputCharacterName.text, inputClass));
    }

    public IEnumerator CreateCharacterDB(int userID, string charName, string charClass)
    {
        GetComponent<UIManager>().creatingCharacterInfoScreen.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("idPost", userID);
        form.AddField("namePost", charName);
        form.AddField("classPost", charClass);

        WWW www = new WWW(DatabaseManager.CreateCharacterURL, form);
        yield return www;
        string charactersDataString = www.text;
        baseCharactersData.SetValue(charactersDataString, 0);
        GetComponent<UIManager>().creatingCharacterInfoScreen.SetActive(false);
        GetComponent<UIManager>().creatingCharacterInfoText.text = DatabaseManager.GetDataValue(baseCharactersData[0], "MESSAGE:") + ".";
        Debug.Log("<color=green>" + DatabaseManager.GetDataValue(baseCharactersData[0], "MESSAGE:") + ".</color>");

        GetComponent<UIManager>().CharacterSelection();
        StartCoroutine(GetComponent<AccountManager>().LoadCharacters());
    }

}
