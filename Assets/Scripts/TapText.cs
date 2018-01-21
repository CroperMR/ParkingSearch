using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapText : MonoBehaviour {

	private float speed = 0.001f;
		private float yPos;
		void Start () {
			yPos = transform.position.y;
			speed = Random.Range (0, 10) > 5 ? speed : -speed;
		}
		void Update () {
		GetComponent <Text> ().color = Color.Lerp (GetComponent <Text> ().color, Color.yellow, 0.6f * Time.deltaTime);
			if (transform.position.y >= yPos + 0.03f || transform.position.y <= yPos - 0.03f)
				speed = -speed;
			transform.position += new Vector3 (0, speed, 0);

		if (PlayerLose.lose || PlayerPrefs.GetString ("Study") == "yes")
			gameObject.SetActive (false);

		}

	}

