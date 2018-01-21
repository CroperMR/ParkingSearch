using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLose : MonoBehaviour {

	public static bool lose;
	public GameObject explotion;

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "Car") {
			print ("You Lose");
			lose = true;
			MoveObjects.speed = 0;
			other.gameObject.GetComponent <Rigidbody> ().AddForce (Vector3.up * 150);

			//Inst explos
			Vector3 pos = Vector3.Lerp (gameObject.transform.position, other.transform.position, 0.5f);
			GameObject exp = Instantiate (explotion, new Vector3 (pos.x, 2.82f, pos.z), Quaternion.Euler (41, 90, 0)) as GameObject;
			Destroy (exp, 1.5f);
		}
	}

	void OnDestroy () {
		lose = false;
	}
}
