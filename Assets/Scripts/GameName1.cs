using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameName1 : MonoBehaviour {

	private float speed = 0.0001f, yPos; 
	private Outline stroke; 
	public float deviation = 0.005f;

	void Start () {
		yPos = transform.position.y; 
	}

	void Update () {
		
		if (transform.position.y >= yPos + deviation || transform.position.y <= yPos - deviation)
			speed = -speed;

		transform.position += new Vector3 (0, speed, 0); 
	}
}
