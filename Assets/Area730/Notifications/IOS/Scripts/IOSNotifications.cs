using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace Area730.Notifications.IOS
{

    /// <summary>
    /// Class that handles requests to ios
    /// </summary>
    public class IOSNotifications
    {
		#region DLL Import
		
		// extern C interface from LocalNotification.mm
		[DllImport ("__Internal")]
		private static extern int _setUpLocalNotification (
			string notification_id, 
			string title, 
			string body,
			string badge_number,
			string custome_sound,
			bool is_repeating, 
			int repeat_unit, 
			long delay
			);
		
		[DllImport ("__Internal")]
		private static extern void _cancelNotificationById (string id);
		
		[DllImport ("__Internal")]
		private static extern void _cancelAllNotification ();
		
		[DllImport ("__Internal")]
		private static extern void _clearAllNotification();

		[DllImport ("__Internal")]
		private static extern float _getVersion();

		[DllImport ("__Internal")]
		private static extern void _showToast(string test);
		
		#endregion


        /// <summary>
        /// Holds serialized notifications created in editor window (Window -> IOS local notifications)
        /// </summary>
        private static IOSDataHolder _dataHolder;

        /// <summary>
        /// Default constructor
        /// </summary>
        static IOSNotifications()
        {
			_dataHolder = (IOSDataHolder)Resources.Load("IOSNotificationData");  
        }

        /// <summary>
        /// Returns builder for notification created in editor by index(position in list in editor window)
        /// </summary>
        /// <param name="pos">index of notification in the editor list</param>
        /// <returns>Notification builder</returns>
        public static IOSNotificationBuilder GetNotificationBuilderByIndex(int pos)
        {
            IOSNotificationInstance item   = _dataHolder.notifications[pos];
            IOSNotificationBuilder builder = IOSNotificationBuilder.FromInstance(item);

            return builder;
        }

        /// <summary>
        /// Returns builder for notification created in editor window by name
        /// </summary>
        /// <param name="name">Name of the notification to get</param>
        /// <returns>Notification builder or null if the notification is not found</returns>
        public static IOSNotificationBuilder GetNotificationBuilderByName(string name)
        {
            foreach(IOSNotificationInstance item in _dataHolder.notifications)
            {
                if (item.name.Equals(name))
                {
                    IOSNotificationBuilder builder = IOSNotificationBuilder.FromInstance(item);
                    return builder;
                }
            }

            return null;
        }

        /// <summary>
        /// Get the version of the plugin
        /// </summary>
        /// <returns>Vertion of the plugin</returns>
        public static float getVersion()
        {
#if UNITY_IOS && !UNITY_EDITOR
            float version = _getVersion();
            return version;
#else
            return -1f;            
#endif
        }

        /// <summary>
        /// Schedule the notification
        /// </summary>
        /// <param name="notif">Notification to be scheduled</param>
        public static void scheduleNotification(IOSNotification notif)
        {
#if UNITY_IOS && !UNITY_EDITOR
			Debug.Log("Unity badge number : " + Convert.ToString(notif.Number));
			Debug.Log ("Sound name: " +notif.Sound);
			_setUpLocalNotification(
				Convert.ToString(notif.ID), 
				notif.Title, 
				notif.Body, 
				Convert.ToString(notif.Number),
				notif.Sound,
				notif.IsRepeating, 
				(int)notif.Interval,
				notif.Delay
				);
#endif
        }


        /// <summary>
        /// Cancels the notification. Both repeating and non-repeating.
        /// </summary>
        /// <param name="notif">Notification to be cancelled</param>
        public static void cancelNotification(int id)
        {
#if UNITY_IOS && !UNITY_EDITOR
			string sid = id.ToString();
			Debug.Log("Cancel by id: " + sid);
            _cancelNotificationById(sid);
#endif
        }

        /// <summary>
        /// Cancel all tracked notifications. 
        /// By defauld all notifications scheduled with IOSNotification.scheduleNotification() are tracked
        /// </summary>
        public static void cancelAll()
        {
#if UNITY_IOS && !UNITY_EDITOR
			_cancelAllNotification();
#endif
        }

        /// <summary>
        /// Cleared all shown notifications
        /// </summary>
        public static void clearAll()
        {
#if UNITY_IOS && !UNITY_EDITOR
			_clearAllNotification();
#endif
        }

        /// <summary>
        /// Shows  ios toast notification
        /// </summary>
        /// <param name="text">Text of the toast</param>
        public static void showToast(string text)
        {
#if UNITY_IOS && !UNITY_EDITOR
			_showToast(text);
#endif
        }

    }
}
