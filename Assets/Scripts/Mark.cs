using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour {


	void Update () {

		if (transform.position.y != 10f)
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, 10f, transform.position.z), 10 * Time.deltaTime);
		else
			Destroy (gameObject);
	}
}
