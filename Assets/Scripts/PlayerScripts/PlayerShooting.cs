using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour {

	private float m_PowerUpMax = 100f; // Max amount the charge can reach
	private float m_PowerUp = 100f; // Power up amount
	public float m_PowerUpAmount = 0.5f; // Amount the power up increments

	private float m_GunDamage = 50f; // Gun damage
	private float m_RaycastRange = 50f; // Raycast range

	private bool m_KeyHasBeenPressed = false; // Handles when the fire key has been pressed

	private AudioSource m_AudioSource; // Audio source

	private bool m_PlayerCanShoot = true; // Player can shoot

	public AudioClip m_ShootingSound; // Shooting sound

	public Image m_ChargeBar; // Charge bar image

	public  Camera m_FirstPersonCamera; // FPS camera

	// Use this for initialization
	void Start () {

		m_PowerUpAmount = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {

		// If the player presses the space key
		if (Input.GetKeyDown (KeyCode.Space) && m_KeyHasBeenPressed == false) {

			// Sees if the player can shoot
			if(m_PlayerCanShoot && m_PowerUp == 100){
				
				m_AudioSource = GetComponent<AudioSource> (); // Audio source GetComponent

				m_AudioSource.PlayOneShot (m_ShootingSound); // Plays the audio

				Shoot (); // Shoots gun
			}

			m_PlayerCanShoot = false; // Player can not shoot

			ChargeDepleted (); // Depletes charge bar
		}

		GunChargingUp (); // Charges up gun
	}

	// Updates ratio of the charge bar
	void UpdateGunCharge(){

		float ratio = m_PowerUp / m_PowerUpMax; // Ratio is power divided by max amount

		m_ChargeBar.rectTransform.localScale = new Vector3(ratio,1,1); // Reduces bar amount displayed by specified ratio
	}

	// Depletes charge bar
	void ChargeDepleted(){

		m_PowerUp -= 100; // Decreases power

		// If power is less than 0
		if (m_PowerUp < 0) {
		
			m_PowerUp = 0; // Clamps power to 0
		}

		UpdateGunCharge (); // Updates ratio of the charge bar
	}

	// Charges up gun
	void GunChargingUp(){

		// If power up is not up to the max amount
		if (m_PowerUp != m_PowerUpMax) {

			m_KeyHasBeenPressed = true; // Key has been pressed
		}

		// If the fire key has been pressed
		if(m_KeyHasBeenPressed){
			
			m_PowerUp += m_PowerUpAmount; // Increases power by set amount
		}

		// If power is more than 100
		if (m_PowerUp > 100) {

			m_PowerUp = 100; // Clamps power up to 100
		}

		// When the power reaches 100
		if (m_PowerUp == 100) {

			m_PlayerCanShoot = true; // Player can shoot now
			m_KeyHasBeenPressed = false; // Key reset to unpressed
		}

		UpdateGunCharge (); // Updates ratio of the charge bar
	}

	// Shoots gun
	void Shoot(){
		
		RaycastHit hit; // Raycast

		// Raycast position and range
		if (Physics.Raycast (m_FirstPersonCamera.transform.position, m_FirstPersonCamera.transform.forward, out hit, m_RaycastRange)) {

			EnemyAI enemy = hit.transform.GetComponent<EnemyAI> (); // EnemyAI object

			// If an enemy is available to hit
			if (enemy != null) {

				enemy.TakeDamage (m_GunDamage); // Enemy takes damage
			}
		}
		
	}
}