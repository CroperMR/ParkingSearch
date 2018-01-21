using UnityEngine;
using System.Collections;

public class BlinkingCube : MonoBehaviour {

	void Start () {
		StartCoroutine (Blink ());
	}

	IEnumerator Blink () {
		while (true) {
			yield return new WaitForSeconds (0.15f);
			gameObject.GetComponent <MeshRenderer> ().enabled = !gameObject.GetComponent <MeshRenderer> ().enabled;
		}
	}

}
