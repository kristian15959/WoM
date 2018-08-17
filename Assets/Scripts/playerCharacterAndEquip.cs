using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerController))]
public class playerCharacterAndEquip : MonoBehaviour {

	public string player_name;
	public string player_class;
	public int player_level;
	public Gender player_gender;	
	public int player_nation;
	
	public List<PlayerModel> player_models = new List<PlayerModel>();

	public GameObject player_Model;

	public PlayerCharacterAppearance playerCharacterAppearance;

    GameObject temp1;
	GameObject temp2;

	void Start()
	{
		UpdateCharacterAppearance();
	}

	public void UpdateCharacterAppearance() {
		
		for(int i=0; i<player_models.Count; i++){
			player_models[i].PlayerModel_Transform.SetActive(false);
			if(player_models[i].PlayerModel_Name == player_gender.ToString()) {
                temp1 = player_models[i].PlayerModel_Transform;
                temp2 = player_models[i].PlayerModel_Body;
			}
		}

		switch (player_gender)
		{
			case Gender.Male:
                temp1.SetActive(true);
			break;
            case Gender.Female:
                temp1.SetActive(true);
			break;
		}

		switch (playerCharacterAppearance.skinColor)
		{
			case SkinColor.white:
				temp2.GetComponent<Renderer>().material.SetColor("Color_E3B99CB9", Color.white);
                temp2.GetComponent<Renderer>().material.color = Color.black;
                break;
			case SkinColor.black: 
				temp2.GetComponent<Renderer>().material.color = Color.black;
			break;
			default:
			break;
		}
	}
}