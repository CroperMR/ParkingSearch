using UnityEngine;
using System.Collections;

public class CreateBorder : MonoBehaviour {

	public static string side;
	public GameObject borders, blinking, study, coin;
	private GameObject borderRight, borderLeft;
	private int randSpaces, randSide, randCoin;

	void Start () {
		randSpaces = Random.Range (5, 8); // Random Space between blocks
		randSide = Random.Range (1, 3); // Random side for space
		randCoin = Random.Range (1, 4);

		if (PlayerPrefs.GetString ("Study") != "yes") {
			randSpaces = 4;
			study.SetActive (true);
		}

		for (int i = 0; i < 14; i++) {
			float xPos = borderRight == null ? -16f : borderRight.GetComponent <MeshRenderer> ().bounds.size.x + Random.Range (1f, 3f) + borderRight.transform.position.x;
			borderRight = Instantiate (borders, new Vector3 (xPos, 0f, -4.1f), Quaternion.identity) as GameObject;

			float yPos = borderLeft == null ? -16f : borderLeft.GetComponent <MeshRenderer> ().bounds.size.x + Random.Range (1f, 3f) + borderLeft.transform.position.x;
			borderLeft = Instantiate (borders, new Vector3 (yPos, 0f, -15.8f), Quaternion.identity) as GameObject;
		}
	}

	void Update () {		
		if (borderRight.transform.position.x < 36.8f) {
			randSpaces--;
			float rand = randSpaces <= 0 && randSide == 1 ? Random.Range (10f, 15f) : Random.Range (1f, 3f);
			if (randSpaces <= 0 && randSide == 1) {
				RandomNums (rand, -3.15f, borderRight);
				side = "Right";
			}
			borderRight = Instantiate (borders, new Vector3 (borderRight.GetComponent <MeshRenderer> ().bounds.size.x + rand + borderRight.transform.position.x, 0f, -4.1f), Quaternion.identity) as GameObject;
		}
		if (borderLeft.transform.position.x < 36.8f) {
			float rand = randSpaces <= 0 && randSide == 2 ? Random.Range (10f, 15f) : Random.Range (1f, 3f);
			if (randSpaces <= 0 && randSide == 2) {
				RandomNums (rand, -17.02f, borderLeft);
				side = "Left";
			}
			borderLeft = Instantiate (borders, new Vector3 (borderLeft.GetComponent <MeshRenderer> ().bounds.size.x + rand + borderLeft.transform.position.x, 0f, -15.8f), Quaternion.identity) as GameObject;
		}
	}

	void RandomNums (float rand, float zPos, GameObject border) {
		randSpaces = Random.Range (12, 20); // Random Space between blocks
		randSide = Random.Range (1, 3); // Random side for space

		if (rand > 5f) {
			randCoin--;
			GameObject blink = Instantiate (blinking, new Vector3 (1f + border.transform.position.x + rand / 2, 0.5f, zPos), Quaternion.identity) as GameObject;
			blink.transform.localScale = new Vector3 (rand, 0.03f, 0.3f);
			if (randCoin == 0) { // Создаем монетку
				float z = zPos == -17.02f ? -15.25f : -4.72f;
				Instantiate (coin, new Vector3 (1f + border.transform.position.x + rand / 2, 1f, z), Quaternion.Euler (41, 90, 0));
				randCoin = Random.Range (2, 5);
			}
		}
	}

}
