using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoad : MonoBehaviour {

	public GameObject[] roadInst; 
	public GameObject border ;
	private GameObject currentRoad;
	private int nextLocation, fromLocation = 0, toLocation = 3; 

	void Start () {
		nextLocation = Random.Range (8, 11);
		currentRoad = Instantiate (roadInst[Random.Range(fromLocation,toLocation)], new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
	}

	void FixedUpdate () {
		if (currentRoad.transform.position.x < 0) {
			currentRoad = Instantiate (roadInst [Random.Range(fromLocation,toLocation)], new Vector3 (currentRoad.transform.position.x + 64.9f,0, 0), Quaternion.identity) as GameObject;
		nextLocation--;
	}

		if (nextLocation == 0) {
			nextLocation = Random.Range (10, 12);
			Instantiate ( border, new Vector3 (currentRoad.transform.position.x + 64.4f, 0, 0), Quaternion.identity);
			currentRoad = Instantiate (roadInst [Random.Range(fromLocation,toLocation)], new Vector3 (currentRoad.transform.position.x + 84.41f,0, 0), Quaternion.identity) as GameObject;

			int rand = Random.Range (1, 5);
			switch (rand) {
			case 1:
				fromLocation = 0;
				toLocation = 3;
				break;
			case 2:
				fromLocation = 3;
				toLocation = 7;
				break;
		
			}
		}
	}
}