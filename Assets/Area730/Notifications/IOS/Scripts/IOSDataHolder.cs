using System.Collections.Generic;
using UnityEngine;

namespace Area730.Notifications.IOS
{

    /// <summary>
    /// Class that holds data set up in Notifications editor window
    /// </summary>
    [System.Serializable]
    public class IOSDataHolder : ScriptableObject
    {

        public List<IOSNotificationInstance> notifications = new List<IOSNotificationInstance>();

        /// <summary>
        /// For debug purposes
        /// </summary>
        public void print()
        {
            foreach (var item in notifications)
            {
                item.Print();
            }
        }

    }

}


