using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public GameObject m_Player; // Player

	public EnemyAI m_EnemyAI; // EnemyAI

	private CharacterController m_CharacterController; // CharacterController

	private Rigidbody m_Rigidbody; // Rigidbody

	private float m_MovementSpeed; // Movement speed

	// Start
	void Start(){
		
		m_CharacterController = GetComponent<CharacterController> (); // CharacterController

		m_Rigidbody = GetComponent<Rigidbody> (); // Rigidbody

		m_MovementSpeed = 2.0f; // Movement speed
	}

	// Update is called once per frame
	void Update () {

		m_Rigidbody.freezeRotation = true; // Freezes rigibody rotation

		// If player is alive
		if (m_EnemyAI.m_PlayerHasDied == false) {
			
			// If the player presses W key
			if (Input.GetKeyDown (KeyCode.W)) {

				// Moves the player forward in the direction it is facing
				m_CharacterController.transform.Translate (Vector3.forward * m_MovementSpeed);
			}
			// If the player presses A key
			if (Input.GetKeyDown (KeyCode.S)) {

				// Moves the player back
				m_CharacterController.transform.Translate (Vector3.back * m_MovementSpeed);
			}
			// If the player presses S key
			if (Input.GetKeyDown (KeyCode.A)) {

				// Rotates the player left
				m_CharacterController.transform.Rotate (0, -90, 0);
			}
			// If the player presses D key
			if (Input.GetKeyDown (KeyCode.D)) {

				// Rotates the player right
				m_CharacterController.transform.Rotate (0, 90, 0);
			}
		}
	}
}