using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour {

	public GameObject m_InvisibilityPickUp; // Invisibility pick up

	public EnemyAI m_EnemyAI; // EnemyAI

	public bool m_InvisibilityEnabled; // Checks if pick up is active

	// Start
	void Start(){

		m_InvisibilityEnabled = false; // Invisibility set to false
	}

	// Upon triggering collider
	void OnTriggerEnter(Collider _collider){

		// When collider hits player
		if (_collider.gameObject.tag == "Player") {

			m_InvisibilityEnabled = true;

			m_EnemyAI.m_EnemyHasPlayerInSight = false; // Enemy can no longer see the player

			DestroyObject (m_InvisibilityPickUp); // Destroys pick up after we have pick it up
		}
	}
}
