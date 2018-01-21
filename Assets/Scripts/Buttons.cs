using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

	public Sprite btnprs, btnact;
	private Transform child;
	private float cor_y;
	public static bool moveCar;

	void OnMouseDown () {
		if(gameObject.name == "Music" && PlayerPrefs.GetString ("Music") == "no")
			child = transform.GetChild (1).transform;
		else
			child = transform.GetChild (0).transform; 

		GetComponent <Image> ().sprite = btnprs;
		cor_y = child.localPosition.y;
		child.localPosition = new Vector3 (child.localPosition.x, 0, child.localPosition.z);
	}

	void OnMouseUp () {
		
		if(gameObject.name == "Music" && PlayerPrefs.GetString ("Music") == "no")
			child = transform.GetChild (1).transform;
		else
			child = transform.GetChild (0).transform; 
		
		GetComponent <Image> ().sprite = btnact;
		child.localPosition = new Vector3 (child.localPosition.x, cor_y, child.localPosition.z);
	}
	void OnMouseUpAsButton () {
		switch (gameObject.name) {
		case "Play": 
			moveCar = true;
			StartCoroutine (loadScene ("game"));
			break;
		case "Close Shop": 
			moveCar = true;
			StartCoroutine (loadScene ("main"));
			break;
		case "Restart":
			StartCoroutine (loadScene ("game"));
			break;
		case "Social":
			Application.OpenURL ("https://vk.com/kapitan_lesha");
			break;
		case "Shop":
			StartCoroutine (loadScene("shop"));
			break;
		case "Menu":
			StartCoroutine (loadScene ("main"));
			break;
		}
	}
	IEnumerator loadScene (string scene) {
		float FadeTime = Camera.main.GetComponent <Fading> ().BeginFade (1);
		yield return new WaitForSeconds (FadeTime);
		SceneManager.LoadScene (scene);
	
	}
}