using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBorders : MonoBehaviour
{
	public static string side;
	public GameObject borders;
	private GameObject borderRight,borderLeft;
	private int randSpaces, randSide; 

	void Start () {

		RandomNums ();

		for (int i = 0; i <12; i++) {
			float xPos = borderRight == null ? -16f : borderRight.GetComponent <MeshRenderer> ().bounds.size.x + Random.Range (1f,3f) + borderRight.transform.position.x ;
			borderRight = Instantiate (borders, new Vector3 (xPos, 0f,-4f), Quaternion.Euler (new Vector3 ( -90, 90, 0))) as GameObject ;

			float yPos = borderLeft == null ? -16f : borderLeft.GetComponent <MeshRenderer> ().bounds.size.x + Random.Range (1f,3f) + borderLeft.transform.position.x ;
			borderLeft = Instantiate (borders, new Vector3 (yPos, 0f,-16f), Quaternion.Euler (new Vector3 ( -90, 90, 0))) as GameObject ;
		}
	}
	void Update () {
		if (borderRight.transform.position.x < 36.8f) {
			randSpaces--;
			float rand = randSpaces == 0 && randSide == 1 ? Random.Range (5f, 10f) : Random.Range (1f, 3f);
			if (randSpaces == 0 && randSpaces == 1) {
				RandomNums ();
				side = "Right";
			}

			borderRight = Instantiate (borders, new Vector3 (borderRight.GetComponent <MeshRenderer> ().bounds.size.x + rand + borderRight.transform.position.x, 0f, -4f), Quaternion.Euler (new Vector3 (-90, 90, 0))) as GameObject;
		}
		if  (borderLeft.transform.position.x < 36.8f) {
			float rand = randSpaces == 0 && randSide == 2 ? Random.Range (5f, 10f) : Random.Range (1f, 3f);
			if (randSpaces == 0 && randSpaces == 2) {
				RandomNums ();
				side = "Left";
			}
			borderLeft = Instantiate (borders, new Vector3 (borderLeft.GetComponent <MeshRenderer> ().bounds.size.x + rand + borderLeft.transform.position.x , 0f,-16f), Quaternion.Euler (new Vector3 ( -90, 270, 0))) as GameObject ;
	}
}
	void RandomNums () {
		randSpaces = Random.Range (5, 7);
		randSide = Random.Range (1, 3);
	}
}
