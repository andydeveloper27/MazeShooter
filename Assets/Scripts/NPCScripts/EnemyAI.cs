using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour {

	private float m_RotationSpeed = 3f; // Rotation speed of the enemy
	public float m_MoveSpeed = 1f; // Speed of the enemy
	private float m_PlayerDistance; // Player's distance
	public float m_EnemyHealth = 50f; // Enemy health
	private float m_RaycastRange = 10f; // Raycast range

	private NavMeshAgent m_NavMeshAgent; // Nav mesh agent
	private Animator m_Animator; // Animator
	private Rigidbody m_Rigidbody; // Rigidbody
	private AudioSource m_EnemySound; // Enemy sound

	public bool m_EnemyHasPlayerInSight; // Enemy has player in sight
	public bool m_PlayerHasDied; // Player has died

	public Transform m_Target; // Target
	public GameObject m_NPC; // NPC

	public Text m_GameOverText; // Game over text;

	public Invisibility m_Invisibility; // Invisibility

	// Use this for initialization
	void Start () {

		// Sets the player as the target
		m_Target = GameObject.FindGameObjectWithTag ("Player").transform;

		m_EnemyHasPlayerInSight = true; // Enemy can see the player 

		m_GameOverText.enabled = false; // Game over text false

		m_PlayerHasDied = false; // Player has not died

		m_Animator = GetComponent<Animator> (); // Sets animator

		m_Rigidbody = GetComponent<Rigidbody> (); // Sets rigidbody

		m_NavMeshAgent = GetComponent<NavMeshAgent> (); // Sets nav mesh agent

		m_EnemySound = GetComponent<AudioSource> (); // AudioSource
	}
	
	// Update is called once per frame
	void Update () {

		m_Rigidbody.freezeRotation = true; // Freezes rotation

		// Players distance 
		m_PlayerDistance = Vector3.Distance (m_Target.position, transform.position);
			
		RaycastHit hit; // Raycast

		// Raycast position and range
		if (Physics.Raycast (gameObject.transform.position, gameObject.transform.forward, out hit, m_RaycastRange)) {
			
			// When player's distance is in the enemie's range
			if (m_PlayerDistance <= m_RaycastRange) {

				m_EnemyHasPlayerInSight = true; // Player is in sight

				// If enemy has player in sight
				if (m_EnemyHasPlayerInSight) {

					m_Rigidbody.isKinematic = false; // Disables kinematic mode

					m_Animator.SetBool ("Walk", true); // Sets walk to true

					// Rotates and faces the enemy towards the player
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (m_Target.position - transform.position),
						m_RotationSpeed * Time.deltaTime);

					// Moves the enemy towards the player
					m_Rigidbody.AddForce (transform.forward *= m_MoveSpeed);
				} 
				else 
				{
					m_Animator.SetBool ("Idle", true); // Sets idle to true
					m_Animator.SetBool ("Walk", false); // Sets walk to false
					m_EnemyHasPlayerInSight = false; // Player is not in sight
					m_Rigidbody.isKinematic = true; // Stops the NPC moving
				}
			}
		}
		if (m_Invisibility.m_InvisibilityEnabled) 
		{
			m_Animator.SetBool ("Idle", true); // Sets idle to true
			m_Animator.SetBool ("Walk", false); // Sets walk to false

			m_EnemyHasPlayerInSight = false; // Sets sight to false
			m_Rigidbody.isKinematic = true; // Enables kinematic mode
			m_EnemySound.mute = true; // Mutes the enemie's sound
		}
	}

	// Enemy taking damage
	public void TakeDamage(float amount){

		m_EnemyHealth -= amount; // Takes away enemy health

		// When enemy health reaches 0
		if (m_EnemyHealth <= 0) {

			Destroy (gameObject); // Destroys game object
		}
	}

	// Upon colliding
	void OnCollisionEnter(Collision _collider){

		// If collider collides with player
		if (_collider.gameObject.tag == "Player" && m_Invisibility.m_InvisibilityEnabled == false) {

			m_GameOverText.enabled = true; // Enables game over text

			m_PlayerHasDied = true; // Player has died

			Time.timeScale = 0; // Time stops

			Application.Quit (); // Quits application
		}
	}
}