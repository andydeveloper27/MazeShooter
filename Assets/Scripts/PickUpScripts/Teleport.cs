using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	public GameObject m_TeleportPickUp; // Mini map pick up

	public bool m_TeleportPickedUp = false; // Sees if the teleport has been picked up

	// Upon triggering collider
	void OnTriggerEnter (Collider _collider)
	{
		// When the player enters the collider
		if (_collider.gameObject.tag == "Player") {

			m_TeleportPickedUp = true; // Teleport has been picked up

			DestroyObject (m_TeleportPickUp); // Destroys pick up after we have pick it up
		}
	}
}