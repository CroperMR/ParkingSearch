using UnityEngine;
using UnityEngine.UI;
using System;
using Area730.Notifications.IOS;
using System.Collections.Generic; 

namespace Area730.Examples
{
    public class IOSExampleSceneController : MonoBehaviour
    {
        #region Serialized fields

        [SerializeField]
        private Text        versionLabel;

        [SerializeField]
        private InputField  titlefield;

        [SerializeField]
        private InputField  bodyField;

        [SerializeField]
        private InputField  delayField;

        [SerializeField]
        private InputField  idField;

        [SerializeField]
        private InputField  badgeField;

        [SerializeField]
        private Toggle      sound;
	    
        [SerializeField]
        private Toggle      repeating;

		[SerializeField]
		private Toggle      minutes;

		[SerializeField]
		private Toggle      hours;

		[SerializeField]
		private Toggle      days;

		[SerializeField]
		private Toggle      months;

		[SerializeField]
		private Toggle      years;

		public GameObject intervalsCheckboxes;


        #endregion

        #region Vars

        private string  		title;
        private string  		body;
        private int     		delay;
        private int     		id;
		private IntervalUnits   interval;
  		private int 			badge;

        #endregion


        void Start()
        {
            versionLabel.text = "Version: " + IOSNotifications.getVersion().ToString();
			IOSNotifications.scheduleNotification(IOSNotifications.GetNotificationBuilderByName("test").build());

			// Enable line below to enable logging if you are having issues setting up OneSignal. (logLevel, visualLogLevel)
			//OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.INFO, OneSignal.LOG_LEVEL.INFO);
			
			OneSignal.Init("cae447e0-d37b-4a12-a0e1-576cde4572a1", null, HandleNotification);
		}
		// Gets called when the player opens the notification.
		private static void HandleNotification(string message, Dictionary<string, object> additionalData, bool isActive) {
			Debug.Log("Handle push notification from One Signal");
		}

		void Update()
		{
			if(repeating.isOn)
			{
				intervalsCheckboxes.SetActive(true);
			}
			else
			{
				intervalsCheckboxes.SetActive(false);
			}
		}

        
        public void scheduleAction()
        {
            updateValues();
		

            IOSNotificationBuilder builder = new IOSNotificationBuilder(id, title, body);
            builder
                
                .setDelay           (delay)
                .setRepeating       (repeating.isOn)
				.setInterval		(interval)
				.setNumber			(badge);
				
                Debug.Log(builder.build());
                IOSNotifications.scheduleNotification(builder.build());

                IOSNotifications.showToast("Notification scheduled");
        }

        public void ScheduleFromList()
        {
            string notificationName = "FirstNotif";
            IOSNotificationBuilder builder = IOSNotifications.GetNotificationBuilderByName(notificationName);

            if (builder != null)
            {
                IOSNotification notif = builder.build();
                IOSNotifications.scheduleNotification(notif);

                Debug.Log(notif);
            } else
            {
                Debug.LogError("Notification with name " + notificationName + " wasn't found");
            }
            
        }

        /// <summary>
        /// Cancels notification with current id
        /// </summary>
        public void cancelAction()
        {
            updateValues();

            IOSNotifications.cancelNotification(id);
            IOSNotifications.showToast("Notification cancelled (" + id + ")");
        }


		/// <summary>
		/// Cancels notification with current id
		/// </summary>
		public void cancelAllAction()
		{
			updateValues();
			
			IOSNotifications.cancelAll();
			
			IOSNotifications.showToast("All notification cancelled");
		}
        /// <summary>
        /// Clear current notification button id
        /// </summary>
        public void clearCurrent()
        {
//            IOSNotifications.clear(id);
			updateValues();
            IOSNotifications.showToast("Cleared id " + id);
        }

        /// <summary>
        /// Clear all notifications buttin action
        /// </summary>
        public void clearAll()
        {
            IOSNotifications.clearAll();
            IOSNotifications.showToast("All cleared");
        }




        /// <summary>
        /// Retreive values from ui fields. If they are empty - set some defaluts
        /// </summary>
        private void updateValues()
        {


			if(minutes.isOn)
			{
				interval = IntervalUnits.MINUTE;
			}

			if(hours.isOn)
			{
				interval = IntervalUnits.HOUR;
			}

			if(days.isOn)
			{
				interval = IntervalUnits.DAY;
			}

			if(months.isOn)
			{
				interval = IntervalUnits.MONTH;
			}

			if(years.isOn)
			{
				interval = IntervalUnits.YEAR;
			}



            if (!String.IsNullOrEmpty(titlefield.text))
            {
                title = titlefield.text;
            }
            else
            {
                title = "New notification!";
            }

            if (!String.IsNullOrEmpty(bodyField.text))
            {
                body = bodyField.text;
            }
            else
            {
                body = "Looks like its working!";
            }
          
            if (!String.IsNullOrEmpty(delayField.text))
            {
                delay = Convert.ToInt32(delayField.text);
            }
            else
            {
                delay = 5;
            }

            if (!String.IsNullOrEmpty(idField.text))
            {
                id = Convert.ToInt32(idField.text);
            }
            else
            {
                id = 1;
            }

           if(!String.IsNullOrEmpty(badgeField.text))
			{
				badge = Convert.ToInt32(badgeField.text);
			}
			else
			{
				badge = 1;
			}

        }


    }
}