using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCar : MonoBehaviour {


	public GameObject selectBtn;
	public Text coins, carName;



	void OnMouseUpAsButton () {

		if (PlayerPrefs.GetInt ("Coins") < 100) 
			IOSMessage.Create("You can't buy this car ", "You need " + (100 - PlayerPrefs.GetInt("Coins")).ToString() + " more coins to buy this car" );
		else {
			PlayerPrefs.SetString ("Current Car", carName.text);
			PlayerPrefs.SetString (carName.text, "Unlocked");
			PlayerPrefs.SetInt ("Coins", PlayerPrefs.GetInt ("Coins") - 100);
			GameObject.Find (carName.text).GetComponent <Animation> ().Play ();
			coins.text = PlayerPrefs.GetInt ("Coins").ToString();
			selectBtn.SetActive (true);
			gameObject.SetActive (false);

		}
	
	}
}