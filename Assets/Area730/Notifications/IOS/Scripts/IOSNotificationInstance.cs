using System.Collections.Generic;
using UnityEngine;


namespace Area730.Notifications.IOS
{

    /// <summary>
    /// This is the internal representation of notification used for serializing notifications
    /// from editor window
    /// </summary>
    [System.Serializable]
    public class IOSNotificationInstance
    {
        public string name;

        public string title;
        public string body;
        
        public bool isRepeating;
		public IntervalUnits interval;
        
        public int number;
        public int delayHours;
        public int delayMinutes;
        public int delaySeconds;
        public bool defaultSound;

        public AudioClip soundFile;
        public int id;

        /// <summary>
        /// For debug purposes
        /// </summary>
        public void Print()
        {
            string res = "Notification: ";

			res += "name: " + name;
            res += "title: " + title;
            res += ", body: " + body;
            res += ", isRepeating: " + isRepeating;
            res += ", intervalHours: " + interval;
            res += ", number: " + number;
            res += ", delayHours: " + delayHours;
            res += ", delayMinutes: " + delayMinutes;
            res += ", delaySeconds: " + delaySeconds;
            res += ", defaultSound: " + defaultSound;
            res += ", body: " + body;
            
			Debug.Log(res);
        }

    }

}


