using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

	public string zoneName;

	public Transform loaded;
	public Transform unloaded;
	
	public bool loadedState;

	void Start () {
		if(loadedState == true)
			loaded.gameObject.SetActive(true);
		else
			unloaded.gameObject.SetActive(true);
	}
	
	void Update () {
		
	}
}
