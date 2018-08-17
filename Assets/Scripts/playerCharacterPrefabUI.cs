using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerCharacterPrefabUI : MonoBehaviour {

	public int characterID;
	public Text text_name, text_characterClass, text_level, text_gender;
	public RawImage rawImage_nation;

	public PlayerManager PM;

	public void UpdateUI (string name, string characterClass, int level, Gender race, int nation) {
		text_name.text = name;
		text_characterClass.text = characterClass;
		text_level.text = "" + level;
		text_gender.text = "" + race;
		switch (nation)
		{
			case 1:
			rawImage_nation.color = Color.red;
			break;
			case 2:
			rawImage_nation.color = Color.green;
			break;
			case 3:
			rawImage_nation.color = Color.blue;
			break;
			default:
			break;
		}
	}

	public void SelectCharacter()
	{
		PM.SelectCharacter(characterID);
	}
}
