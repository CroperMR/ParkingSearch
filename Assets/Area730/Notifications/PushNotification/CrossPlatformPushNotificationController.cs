using UnityEngine;
using System.Collections;
using System.Collections.Generic; 


public class CrossPlatformPushNotificationController : MonoBehaviour {


	public string _IOSOneSignalId;
	public string _androidOneSignalId;

	// Use this for initialization
	void Start () 
	{
		// Enable line below to enable logging if you are having issues setting up OneSignal. (logLevel, visualLogLevel)
		//OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.INFO, OneSignal.LOG_LEVEL.INFO);
		
		OneSignal.Init(_IOSOneSignalId, _androidOneSignalId, HandleNotification);
	}

	// Gets called when the player opens the notification.
	private static void HandleNotification(string message, Dictionary<string, object> additionalData, bool isActive) {
		Debug.Log("Handle push notification from One Signal");
	}
}
