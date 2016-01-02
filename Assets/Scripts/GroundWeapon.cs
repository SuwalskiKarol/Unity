﻿using UnityEngine;
using System.Collections;

public class GroundWeapon : MonoBehaviour {
	
	public int weaponNumber;
	
	private bool isPlayer;
	
	void OnTriggerStay(Collider other)
	{
		if(other.tag.Equals("Player")) {
			isPlayer = true;
			if(Input.GetKeyDown(KeyCode.E)) {
				other.SendMessage("addGun", weaponNumber);
				Destroy(gameObject);
			}
		}
	}
	
	void OnTriggerExit(Collider other) {
		if(other.tag.Equals("Player")) {
			isPlayer = false;
		}
	}
	
	void OnGUI(){
		if(isPlayer) {
			GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 10, 100, 20), "Press E to grab");
		}
	}
}