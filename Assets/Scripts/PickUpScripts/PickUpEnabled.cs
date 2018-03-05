using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpEnabled : MonoBehaviour {

	public GameObject m_Player; // Player

	public Text m_TextMiniMap; // Mini map text
	public Text m_TextInvisibility; // Mini map text
	public Text m_TextFasterReload; // Mini map text
	public Text m_TextTeleport; // Mini map text

	// Use this for initialization
	void Start () {

		m_TextMiniMap.enabled = false; // Disabled text
		m_TextInvisibility.enabled = false; // Disabled text
		m_TextFasterReload.enabled = false; // Disabled text
		m_TextTeleport.enabled = false; // Disabled text
	}

	// Upon entering trigger
	void OnTriggerEnter (Collider _collider)
	{

		// When the player collides with the mini map pick up
		if (_collider.gameObject.tag == "MiniMap") {

			m_TextMiniMap.enabled = true; // Enables text
		}

		// When the player collides with the faster reload pick up
		if (_collider.gameObject.tag == "FasterReload") {

			m_TextFasterReload.enabled = true; // Enables text
		}

		// When the player collides with the invisibility pick up
		if (_collider.gameObject.tag == "Invisibility") {

			m_TextInvisibility.enabled = true; // Enables text
		}

		// When the player collides with the teleport pick up
		if (_collider.gameObject.tag == "Teleport") {

			m_TextTeleport.enabled = true; // Enables text
		}
	}
}