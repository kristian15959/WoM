using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerManager : NetworkBehaviour {

	//Player Selection
	[Header("Player Selection")]
	public PlayerCharacter selectedCharacter;
	public Text selectedCharacterName;	
	public Text selectedCharacterLevel;	
	
	//Player Selection UI
	public Transform PlayerSelectionItemUiHolder;
	public GameObject PlayerSelectionItemUiPrefab;

	//Player In Game
	[Header("Player In Game")]
	public GameObject playerPrefab;
	public List<PlayerCharacter> playerCharacters = new List<PlayerCharacter>();

	void Start () {
		for ( int x=0; x < playerCharacters.Count; x++)
		{
			GameObject go = Instantiate(PlayerSelectionItemUiPrefab, transform.position, transform.rotation, PlayerSelectionItemUiHolder);
			playerCharacterPrefabUI pChPrefab = go.GetComponent<playerCharacterPrefabUI>();
			PlayerCharacter pCh = playerCharacters[x];
			pChPrefab.characterID = x;
			pChPrefab.PM = this.GetComponent<PlayerManager>();
			pChPrefab.UpdateUI( pCh.characterName,
							    pCh.characterClass,
								pCh.characterLevel,
								pCh.characterGender,
								pCh.characterNation);
		}

		playerPrefab = Instantiate(playerPrefab, transform.position, transform.rotation);
	}
	
	public void SelectCharacter( int characterID )
	{
		playerCharacterAndEquip pChP = playerPrefab.GetComponent<playerCharacterAndEquip>();
		for (int i = 0; i < playerCharacters.Count; i++)
		{
			if(characterID == i)
			{
				PlayerCharacter pCh = playerCharacters[i];				
				pChP.player_name = pCh.characterName;
				pChP.player_class = pCh.characterClass;
				pChP.player_level = pCh.characterLevel;
				pChP.player_gender = pCh.characterGender;	
				pChP.player_nation = pCh.characterNation;
				pChP.playerCharacterAppearance = pCh.playerCharacterAppearance;

				//Update UI
				selectedCharacterName.text = pCh.characterName;	
				selectedCharacterLevel.text = "" + pCh.characterLevel;	
			}
		}
		pChP.UpdateCharacterAppearance();

		
	}

	public void SpawnPlayerPrefab()
	{
		//Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0);		
		

	}
}

[System.Serializable]
public class PlayerCharacter {
	public string characterName;
	public string characterClass;
	public int characterLevel;
	public Gender characterGender;	
	public int characterNation;

	//[Header("Customization")]
	public PlayerCharacterAppearance playerCharacterAppearance;
}

[System.Serializable]
public class PlayerModel {
    public string PlayerModel_Name;
    public GameObject PlayerModel_Transform;
    public GameObject PlayerModel_Body;
}

public enum Gender
{
	Male, Female
}

[System.Serializable]
public class PlayerCharacterAppearance
{
	public SkinColor skinColor;
}

public enum SkinColor
{
	white, orange, black
}