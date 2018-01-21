using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GAmeCtrl : MonoBehaviour {

	public Text score, bestScore, coins;
	public GameObject car, carParked, brakes;
	public float stopObj = 6f;

	private GameObject carInst;
	private bool stop; 
	private int countCars = 0;
	private bool addStop; 


	void Awake (){
		stop = false;
		CheckClick.click = false;
		MoveObjects.speed = 25f;
	}
	void Start () {
		Application.targetFrameRate = 60;
		carInst = Instantiate (car, new Vector3 (-30f,0.7f,-10), Quaternion.Euler (new Vector3(0,270,0))) as GameObject;

	}

	void Update () {
		if (MoveObjects.speed > 0 && stop) {
			MoveObjects.speed -= stopObj * Time.deltaTime;
			if (MoveObjects.speed < 0)
				MoveObjects.speed = 0;
		}

		if (stop && !PlayerLose.lose) {
			float rot = CarsInst.side == "Left" ? 335 : -335;
			float z = CarsInst.side == "Left" ? -15.7f : -4.3f;   //Car Control 

			carInst.transform.position = Vector3.MoveTowards (carInst.transform.position, new Vector3 (carInst.transform.position.x, carInst.transform.position.y, z), 10 * Time.deltaTime);

			carInst.transform.Rotate (Vector3.up * rot * Time.deltaTime);

			if (CarsInst.side == "Left" && Mathf.Abs (carInst.transform.eulerAngles.y - 90) < 5f)
				StopRotate ();
			if (CarsInst.side == "Right" && carInst.transform.eulerAngles.y - 90 < 5f)
				StopRotate (); 
		}
		if (CheckClick.click && !addStop) {
			addStop = true;
			stop = true;
			Instantiate (brakes, new Vector3(carInst.transform.position.x, 0, carInst.transform.position.z ), Quaternion.Euler(0, 0, 0) );

		}
	}

	void StopRotate () {

		float zPos = CarsInst.side == "Left" ? -15.7f : -4.3f;
		StartCoroutine (NextCar(zPos));
		stop = false;
		CheckClick.click = false;
			MoveObjects.speed = 0;
			carInst.transform.rotation = Quaternion.Euler (0,90,0);

		}

	IEnumerator NextCar (float zPos) {

		while (carInst.transform.position.z != zPos) 
			carInst.transform.position = Vector3.MoveTowards (carInst.transform.position, new Vector3 (carInst.transform.position.x, carInst.transform.position.y, zPos), 6 * Time.deltaTime);

		//Check Mark Park
		Instantiate (carParked, new Vector3(carInst.transform.position.x,4.7f, carInst.transform.position.z ), Quaternion.Euler(41, 90, 0) );

		yield return new WaitForSeconds (0.5f);
		if (!PlayerLose.lose)  {
			print ("Next Car");
			carInst.AddComponent <MoveObjects> ();


			if (PlayerPrefs.GetString ("Study") != "yes") 
				PlayerPrefs.SetString ("Study", "yes");
			addStop = false;
			countCars++;
			score.text = countCars.ToString ();
			if (PlayerPrefs.GetInt ("Score") < countCars) {
				PlayerPrefs.SetInt ("Score", countCars);
				bestScore.text = "Best: " + countCars.ToString ();
			}

			carInst = Instantiate (car, new Vector3 (-30f, 0.7f, -10), Quaternion.Euler (new Vector3 (0, 270, 0))) as GameObject;
			MoveObjects.speed = Random.Range (25f, 50f);

			if (countCars % 5 == 0) {
				PlayerPrefs.SetInt ("Coins", PlayerPrefs.GetInt ("Coins") + 1);
				coins.text = PlayerPrefs.GetInt ("Coins").ToString ();
			}


	
		}
	}
		
	void OnMouseDown () {

		if (!PlayerLose.lose)	{
			stop = true;
			MoveObjects.speed = 20;
		}
	}
} 