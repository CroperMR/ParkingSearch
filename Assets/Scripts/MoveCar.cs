using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCar : MonoBehaviour {



	void Update () {
		if (Buttons.moveCar)
			transform.position += new Vector3 (0.15f, 0, 0);
	}
	void onDestroy (){
		Buttons.moveCar = false;
	}
}
