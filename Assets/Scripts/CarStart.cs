using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStart : MonoBehaviour {

	private float speed = 8f;
	private bool onPlace;

	void Update () {
		if (!onPlace)
		transform.position = Vector3.MoveTowards (transform.position, new Vector3 (-15f,0.7f,-10f), Time.deltaTime * speed);
		if (transform.position.x == -15)
			onPlace = true;
	}

}
