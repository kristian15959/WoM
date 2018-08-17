using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float camRotSpeed;
	public Transform rotX;
	public GameObject PlayerCharacterTr;

	float _rotX, _rotY;

	void Start () {
		PlayerCharacterTr = GameObject.FindWithTag("Player");
		PlayerCharacterTr.GetComponent<PlayerController>().cam = this.transform;
	}
	
	void Update () {

		if(Input.GetMouseButton(1) || Input.GetAxis("HorizontalRotation") != 0)
		{
			_rotX += Input.GetAxis("Mouse X") + (Input.GetAxis("HorizontalRotation") / 5) * camRotSpeed;
			_rotY -= Input.GetAxis("Mouse Y") * camRotSpeed;

			_rotY = Mathf.Clamp(_rotY,-90,90);

			Quaternion targetX = Quaternion.Euler(0, _rotX, 0);
			Quaternion targetY = Quaternion.Euler(_rotY, _rotX, 0);
			

			transform.rotation = Quaternion.Slerp(transform.rotation, targetX, Time.deltaTime * 100);
			rotX.rotation = Quaternion.Slerp(rotX.rotation, targetY, Time.deltaTime * 100);
			//Debug.Log("x:" + _rotX + "y:" + _rotY);
		}
		

	}
}
