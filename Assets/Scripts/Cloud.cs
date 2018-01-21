using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

	public float speed = 0.1f;
	private float yPos;
	void Start () {
		yPos = transform.position.y;
		speed = Random.Range (0, 10) > 5 ? speed : -speed;
	}
	void Update () {
		if (transform.position.y >= yPos + 2.5f || transform.position.y <= yPos - 2.5f)
			speed = -speed;
			transform.position += new Vector3 (0, speed, 0);
	
	}
}