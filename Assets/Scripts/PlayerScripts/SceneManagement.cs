using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

	// Checks for collision
	void OnCollisionEnter(Collision _collision){

		// Checks the collision against a gamobject with this tag
		if (_collision.gameObject.tag == "NextScene") {

			// loads next scene
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

		}
	}
}