using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour {

	public Camera m_MiniMapCamera; // Mini map camera

	public GameObject m_MiniMapPickUp; // Mini map pick up

	public RawImage m_MiniMapImage; // Mini map image

	public GameObject m_MiniMapBlip; // Mini map blip

	// Use this for initialization
	void Start () {

		m_MiniMapImage.enabled = false; // Image disabled
		m_MiniMapCamera.enabled = false; // Camera disabled
		m_MiniMapBlip.SetActive(false); // Blip disabled
	}

	// Upon triggering collider
	void OnTriggerEnter(Collider _collider){

		// When the player enters the collider
		if (_collider.gameObject.tag == "Player") {

			m_MiniMapImage.enabled = true; // Image enabled
			m_MiniMapBlip.SetActive(true); // Blip enabled
			m_MiniMapCamera.enabled = true; // Camera enabled

			DestroyObject (m_MiniMapPickUp); // Destroys pick up after we have pick it up
		}
	}
}
