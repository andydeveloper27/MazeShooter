using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameComplete : MonoBehaviour {

	public Text m_GameCompleteText; // Game complete text


	// Start
	void Start(){

		m_GameCompleteText.enabled = false; // Sets text to false
	}

	// Checks for collision
	void OnCollisionEnter (Collision _collision)
	{

		// Checks the collision against a gamobject with this tag
		if (_collision.gameObject.tag == "GameComplete") {

			m_GameCompleteText.enabled = true; // Enables Game Complete text
		}
	}
}