using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInserter : MonoBehaviour {

    public string inputEmail;
    public string inputPassword;
    
    void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) CreateUser(inputEmail, inputPassword);
	}

    public void CreateUser(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("emailPost", email);
        form.AddField("passwordPost", password);

        WWW www = new WWW(DatabaseManager.CreateUserURL, form);
        Debug.Log(www);
    }
}
