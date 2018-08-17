using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {

    AccountManager AcManager;
    UIManager uiManager;

    public InputField emailInput;
    public InputField passwordInput;

    public Text loginDebugInfo;

    string[] loginData;

    private void Awake()
    {
        AcManager = GetComponent<AccountManager>();
        uiManager = GetComponent<UIManager>();
    }

    public void SignIn()
    {
        StartCoroutine(LoginToDB(emailInput.text, passwordInput.text));
    }

    IEnumerator LoginToDB(string email, string password)
    {
        loginDebugInfo.text = "Loging into server.";
        WWWForm form = new WWWForm();
        form.AddField("emailPost", email);
        form.AddField("passwordPost", password);
        loginDebugInfo.text = "Loged into server.";

        WWW www = new WWW(DatabaseManager.LoginURL, form);
        loginDebugInfo.text = "Receiving data from server.";
        yield return www;
        loginDebugInfo.text = "Data from server received.";
        string loginDataString = www.text;
        loginData = loginDataString.Split('|');

        switch (DatabaseManager.GetDataValue(loginData[0], "MESSAGE:"))
        {
            case "Login success":
                loginDebugInfo.text = "<color=green>"+DatabaseManager.GetDataValue(loginData[0], "MESSAGE:") + ".</color>";
                Debug.Log("<color=green>" + www.text+".</color>");
                break;
            case "Password incorrect":
                loginDebugInfo.text = "<color=red>" + DatabaseManager.GetDataValue(loginData[0], "MESSAGE:") + ".</color>";
                Debug.Log("<color=red>" + www.text + ".</color>");
                break;
            case "User not found":
                loginDebugInfo.text = "<color=white>" + DatabaseManager.GetDataValue(loginData[0], "MESSAGE:") + ".</color>";
                Debug.Log("<color=white>" + www.text + ".</color>");
                break;
        }

        if(DatabaseManager.GetDataValue(loginData[0], "MESSAGE:") == "Login success")
        {
            AcManager.userID = int.Parse(DatabaseManager.GetDataValue(loginData[1], "ID:"));
            emailInput.text = "";
            passwordInput.text = "";
            uiManager.CharacterSelection();
            StartCoroutine(AcManager.LoadCharacters());
        }
    }
}