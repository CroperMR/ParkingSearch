using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckClick : MonoBehaviour {
	public static bool click;

	void OnMouseDown () {
		if (!PlayerLose.lose)
	click = true;
	}
}
