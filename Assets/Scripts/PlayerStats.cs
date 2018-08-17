using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(playerCharacterAndEquip))]
public class PlayerStats : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;
	public float Speed = 100;

	//Attributes
	[Header("Attributes")]
	public Vector2 Damage;
	public int Power, Strength, Agility, Stamina, Intelligence, Spirit, Armor;

	void Start () {
		
	}
	
	void Update () {
		
	}
}
