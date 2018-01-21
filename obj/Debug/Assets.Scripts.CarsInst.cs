using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsInst : MonoBehaviour {

	public GameObject[] cars;
	private GameObject carCurrRight,carCurrLeft;

	void Start () {
		for (int i = 0; i <10; i++) {
			float xPos = carCurrRight == null ? -16f : carCurrRight.GetComponent <MeshRenderer> ().bounds.size.x + Random.Range (3f,7f) + carCurrRight.transform.position.x ;
			carCurrRight = Instantiate (cars [Random.Range (0, cars.Length)], new Vector3 (xPos, 0f,-3.2f), Quaternion.Euler (new Vector3 ( 0, 90, 0))) as GameObject ;
		

		float yPos = carCurrLeft == null ? -16f : carCurrLeft.GetComponent <MeshRenderer> ().bounds.size.x + Random.Range (3f,7f) + carCurrLeft.transform.position.x ;
		carCurrLeft = Instantiate (cars [Random.Range (0, cars.Length)], new Vector3 (yPos, 0f,-17f), Quaternion.Euler (new Vector3 ( 0, 270, 0))) as GameObject ;
		
	}
}
	void Update () {
		if  (carCurrRight.transform.position.x < 30f)
			carCurrRight = Instantiate (cars [Random.Range (0, cars.Length)], new Vector3 (carCurrRight.GetComponent <MeshRenderer> ().bounds.size.x + Random.Range (3f,7f) + carCurrRight.transform.position.x ;, 0f,-3.2f), Quaternion.Euler (new Vector3 ( 0, 90, 0))) as GameObject ;
		if  (carCurrLeft.transform.position.x < 30f)
			carCurrLeft = Instantiate (cars [Random.Range (0, cars.Length)], new Vector3 (carCurrLeft.GetComponent <MeshRenderer> ().bounds.size.x + Random.Range (3f,7f) + carCurrLeft.transform.position.x ;, 0f,-17f), Quaternion.Euler (new Vector3 ( 0, 270, 0))) as GameObject ;
	}

}
