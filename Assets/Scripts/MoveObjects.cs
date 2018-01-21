using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour {

	public float delete = -30.9f;
	[HideInInspector]
	public static float speed = 25f;


	void Update () {
		transform.position -= new Vector3 (speed * Time.deltaTime, 0, 0);
		if (transform.position.x < delete)
			Destroy (gameObject);
			}
}
