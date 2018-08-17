using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionListItem : MonoBehaviour {

    public UIManager uiManager;

    public int id;
    public Text _name;
    public Text _class;
    public Text _level;

    public void Awake()
    {
        uiManager = GetComponentInParent<UIManager>();
    }

    public void SetupSelection(int ID, string Name, string Class, string Level)
    {
        id = ID;
        _name.text = Name;
        _class.text = Class;
        _level.text = Level;
    }

    public void SelectThisCharacter()
    {
        uiManager.SelectCharacter(this);
    }

}
