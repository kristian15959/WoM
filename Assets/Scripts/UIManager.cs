using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [Header("Basics")]
    public GameObject BackgroundImage;

    [Header("Debug Window")]
    public GameObject DebugWindow;
    public Text DebugTextUI;

    [Header("Login Screen")]
    public GameObject loginScreen;
    public Text DebugInfoText;

    [Header("Character Selection Screen")]
    public GameObject charactersSelectionScreen;
    public GameObject loadingCharactersScreen;
    public Text loadingCharactersInfoText;
    public GameObject CharactersList;
    public GameObject CharactersListItem;
    public GameObject SelectedCharacter;

    public Text SelectedCharacterName;

    [Header("Character Selection Screen Stats")]
    public Text _level;
    public Text _class;
    public Text _health;
    public Text _mana;
    public Text _vitality;
    public Text _intelligence;
    public Text _strenght;
    public Text _defence;

    [Header("Character Creation Screen")]
    public GameObject charactersCreationScreen;

    [Header("Player In Game UI")]
    public GameObject PlayerInGameUI;
    public GameObject InGameMenu;
    public GameObject creatingCharacterInfoScreen;
    public Text creatingCharacterInfoText;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            InGameMenu.SetActive(true);
        }
    }

    public void LoginScreen(string debugInfo)
    {
        loginScreen.SetActive(true);
        charactersSelectionScreen.SetActive(false);
        charactersCreationScreen.SetActive(false);
        DebugInfoText.text = "<color=white>" + debugInfo + "</color>";
    }

    public void CharacterSelection()
    {
        loginScreen.SetActive(false);
        charactersSelectionScreen.SetActive(true);
        charactersCreationScreen.SetActive(false);
    }

    public void CharacterCreation()
    {
        loginScreen.SetActive(false);
        charactersSelectionScreen.SetActive(false);
        charactersCreationScreen.SetActive(true);
    }

    public void SelectCharacter(CharacterSelectionListItem charItem)
    {
        SelectedCharacterName.text = charItem._name.text;
        _level.text = charItem._level.text;
        _class.text = charItem._class.text;
        _health.text = "need setup.";
        _mana.text = "need setup.";
        _vitality.text = "need setup.";
        _intelligence.text = "need setup.";
        _strenght.text = "need setup.";
        _defence.text = "need setup.";
    }

    public void LoginWithSelectedCharacter()
    {
        StartCoroutine(LogingIntoGame());
    }

    public IEnumerator LogingIntoGame()
    {
        loginScreen.SetActive(false);
        charactersSelectionScreen.SetActive(false);
        charactersCreationScreen.SetActive(false);
        GetComponent<MultiplayerMenu>().Connect();
        yield return new WaitForSeconds(2);
        BackgroundImage.SetActive(false);
        PlayerInGameUI.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public static void DebugText(string text)
    {

    }
}
