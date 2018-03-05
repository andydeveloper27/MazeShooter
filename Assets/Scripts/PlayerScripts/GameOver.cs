using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	public EnemyAI m_EnemyAI; // EnemyAI
	public Invisibility m_Invisibility;

	// Update is called once per frame
	void Update () {

		// If player has died and invisibility is not enabled
		if (m_EnemyAI.m_PlayerHasDied && m_Invisibility.m_InvisibilityEnabled == false) {

			Time.timeScale = 0; // Freezes game

			// Starts timer before quitting
			StartCoroutine (TimeBeforeApplicationQuits ());

			Application.Quit(); // Quits application
		}
		
	}

	// Time before application quits
	IEnumerator TimeBeforeApplicationQuits()
	{
		yield return new WaitForSeconds(2); // Waits 2 seconds
	}
}
