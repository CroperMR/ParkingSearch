using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCarAtStart : MonoBehaviour {

public GameObject[] cars;

void Awake () {
	if (PlayerPrefs.GetString ("Current Car") == "") {
		PlayerPrefs.SetString ("Current Car", "Mustang");
		PlayerPrefs.SetString ("Mustang", "Unlocked");
	}
}

void Start () {
	for (int i = 0; i < cars.Length; i++) {
		if (cars [i].name == PlayerPrefs.GetString ("Current Car")) {
			cars [i].SetActive (true);
			break;
		}
	}
	}}