using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterReload : MonoBehaviour {

	public GameObject m_FasterReloadPickUp; // Faster reload pick up

	public PlayerShooting m_PlayerShooting; // PlayerShooting class object

	void Start(){

		m_PlayerShooting.m_PowerUpAmount = 0.5f;
	}

	// Upon triggering collider
	void OnTriggerEnter(Collider _collider){

		// When collider hits player
		if (_collider.gameObject.tag == "Player") {

			m_PlayerShooting.m_PowerUpAmount = 2.0f; // Increases the power up amount

			DestroyObject (m_FasterReloadPickUp); // Destroys pick up after we have pick it up
		}
	}
}