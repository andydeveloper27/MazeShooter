using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour {

	public Teleport m_Teleport; // Teleport
	public PickUpEnabled m_PickUpEnabled;

	public GameObject m_Player; // Player
	public GameObject m_TeleportLocation; // Teleport location

	// Update is called once per frame
	void Update () {

		// If teleport pick up has been picked up
		if (m_Teleport.m_TeleportPickedUp == true) {

			// If the player presses T
			if (Input.GetKey (KeyCode.T)) {

				// Transforms the player to the teleport location
				m_Player.transform.position = m_TeleportLocation.transform.position;

				// Turns off teleport
				m_Teleport.m_TeleportPickedUp = false;

				m_PickUpEnabled.m_TextTeleport.enabled = false; // Disables text
			}
		}
	}
}