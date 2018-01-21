using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsInst : MonoBehaviour {

	public GameObject[] cars;
	public GameObject blinking, study, coin;
	private GameObject carCurrRight,carCurrLeft;
	public static string side;
	private int randSpaces, randSide, randCoin; 

	void Start () {

		randSpaces = Random.Range (5, 14); // number of space numb
		randSide = Random.Range (1, 3); //number of sides 
		randCoin = Random.Range (4, 9); // random coins in parking space

		if (PlayerPrefs.GetString ("Study") != "yes") {
			randSpaces = 2;
			study.SetActive (true);
		}

		for (int i = 0; i <8; i++) {
			float xPos = carCurrRight == null ? -16f : carCurrRight.GetComponent <MeshRenderer> ().bounds.size.x + Random.Range (3f,4f) + carCurrRight.transform.position.x ;
			carCurrRight = Instantiate (cars [Random.Range (0, cars.Length)], new Vector3 (xPos, 0.8f,-2.6f), Quaternion.Euler (new Vector3 ( 0, 90, 0))) as GameObject ;
		
			float yPos = carCurrLeft == null ? -16f : carCurrLeft.GetComponent <MeshRenderer> ().bounds.size.x + Random.Range (3f,4f) + carCurrLeft.transform.position.x ;
			carCurrLeft = Instantiate (cars [Random.Range (0, cars.Length)], new Vector3 (yPos, 0.8f,-17.8f), Quaternion.Euler (new Vector3 ( 0, 270, 0))) as GameObject ;
	} 
}
	void Update () { 
		if (carCurrRight.transform.position.x < 30f) { //create parking space
			randSpaces--;

			float rand = randSpaces <= 0 && randSide == 1 ? Random.Range (13f, 14f) : Random.Range (3f, 4f); // space between parking and cars 
			if (randSpaces <= 0 && randSide == 1) {
				RandomNums (rand, -3f, carCurrRight);
				side = "Right";
			}    //next cars create
		carCurrRight = Instantiate (cars [Random.Range (0, cars.Length)], new Vector3 (carCurrRight.GetComponent <MeshRenderer> ().bounds.size.x + rand + carCurrRight.transform.position.x, 0.82f, -2.6f), Quaternion.Euler (new Vector3 (0, 90, 0))) as GameObject;
		}
		if (carCurrLeft.transform.position.x < 30f) {
			float rand = randSpaces <= 0 && randSide == 2 ? Random.Range (13f, 14f) : Random.Range (3f, 4f);		
			if (randSpaces <= 0 && randSide == 2) {
			RandomNums (rand, -17f, carCurrLeft);
				side = "Left";
			}
			carCurrLeft = Instantiate (cars [Random.Range (0, cars.Length)], new Vector3 (carCurrLeft.GetComponent <MeshRenderer> ().bounds.size.x + rand + carCurrLeft.transform.position.x, 0.82f, -17.8f), Quaternion.Euler (new Vector3 (0, 270, 0))) as GameObject;
		}
	}

	void RandomNums (float rand, float zPos, GameObject cars) { //create red blinking
		randSpaces = Random.Range (10, 15);
		randSide = Random.Range (1, 3);


		if (rand > 5f) {
			randCoin--;
			GameObject blink =  Instantiate (blinking, new Vector3 (1f + cars.transform.position.x + rand / 1.6f, 0.7f, zPos), Quaternion.identity) as GameObject;
		blink.transform.localScale = new Vector3 (rand, 0.05f, 0.3f); 
			if (randCoin == 0) {
				float z = zPos == -17f ? -16f : -4f;
				Instantiate (coin, new Vector3 (1f + cars.transform.position.x + rand / 1.6f, 1.5f, z), Quaternion.Euler (50, 90, 0));
				randCoin = Random.Range (4, 9);
			}
		}
	}
}
