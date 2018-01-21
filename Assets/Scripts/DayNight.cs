using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour {

	private float scrollSpeed = 0.07f;
	public Material mat;

	void Start () {

		mat.mainTextureOffset = new Vector2 (Random.Range(-0.18f, 0.7f), 0);
	}

	void Update () {
		float offset = Time.time * scrollSpeed;
		mat.mainTextureOffset = new Vector2 (offset, 0);
		
	}
}
