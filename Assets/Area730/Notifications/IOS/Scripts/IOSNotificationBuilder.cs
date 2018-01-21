using System;
using UnityEngine;

namespace Area730.Notifications.IOS
{
    public class IOSNotificationBuilder
    {

        /// <summary>
        /// Use the default notification sound. This will ignore any given sound.
        /// </summary>
        public const int DEFAULT_SOUND     = 1;

        private int     		_id                 = 1;
        private string  		_sound              = "";
        private string  		_title              = "Notification title";
        private string  		_body               = "Notification body";
        private long    		_delay              = 0;
        private bool    		_isRepeating        = false;
		private IntervalUnits   _interval			= 0;
        private int     		_number;


        /// <summary>
        /// Coustructor. Takes 3 arguments that are required to make basic notification
        /// </summary>
        /// <param name="id">ID of the notification</param>
        /// <param name="title">Title of the notification</param>
        /// <param name="body">Notification message</param>
		public IOSNotificationBuilder(int id, string title, string body)
        {
            _id     = id;
            _title  = title;
            _body   = body;
        }

        /// <summary>
        /// Set title of the notification
        /// </summary>
        /// <param name="title">Notification title</param>
        /// <returns>Reference to builder</returns>
        public IOSNotificationBuilder setTitle(string title)
        {
            _title = title;
            return this;
        }

        /// <summary>
        /// Set body of the notification
        /// </summary>
        /// <param name="body">Notification body</param>
        /// <returns>Reference to builder</returns>
        public IOSNotificationBuilder setBody(string body)
        {
            _body = body;
            return this;
        }


        /// <summary>
        /// Set whether it is a repeating notification. 
        /// If this is set to true - interval of repetition must be also specified
        /// <para><seealso cref="NotificationBuilder.setInterval(long)"/></para>
        /// <para><seealso cref="NotificationBuilder.setInterval(TimeSpan)"/></para>
        /// </summary>
        /// <param name="state">True if repeating, false if one-time</param>
        /// <returns>Reference to builder</returns>
        public IOSNotificationBuilder setRepeating(bool state)
        {
            _isRepeating = state;
            return this;
        }

        /// <summary>
        /// Sets the interval of repetition of the notification in milliseconds
        /// </summary>
        /// <param name="interval">Interval of repetition in milliseconds</param>
        /// <returns>Reference to builder</returns>
		public IOSNotificationBuilder setInterval(IntervalUnits interval)
        {
            _interval = interval;
            return this;
        }


        /// <summary>
        /// Set the large number at the right-hand side of the notification.
        /// </summary>
        /// <param name="num">Number to set</param>
        /// <returns>Reference to builder</returns>
        public IOSNotificationBuilder setNumber(int num)
        {
            _number = num;
            return this;
        }


        /// <summary>
        /// Set id of the notification
        /// </summary>
        /// <param name="id">Notification id</param>
        /// <returns>Reference to builder</returns>
        public IOSNotificationBuilder setId(int id)
        {
            _id = id;
            return this;
        }



        /// <summary>
        /// Time in milliseconds that the notification should go off
        /// </summary>
        /// <param name="delayMilliseconds"></param>
        /// <returns></returns>
        public IOSNotificationBuilder setDelay(long delayMilliseconds)
        {
            _delay = delayMilliseconds;
            return this;
        }

        /// <summary>
        /// Time in TimeSpan that the notification should go off
        /// <seealso cref="System.TimeSpan"/>
        /// </summary>
        /// <param name="delayTimeSpan">Time interval</param>
        /// <returns>Reference to builder</returns>
        public IOSNotificationBuilder setDelay(TimeSpan delayTimeSpan)
        {
            _delay = (long)delayTimeSpan.TotalMilliseconds;
            return this;
        }

        /// <summary>
        /// Set the sound resource name to play
        /// <para>The sound must be in Assets/StreamingAssets folder</para>
        /// </summary>
        /// <param name="soundResourceName">The sound resource name</param>
        /// <returns>Reference to builder</returns>
        public IOSNotificationBuilder setSound(string soundResourceName)
        {
            _sound = soundResourceName;
            return this;
        }

        /// <summary>
        /// Converts DateTime to milliseconds
        /// </summary>
        /// <param name="value">Value to be converted</param>
        /// <returns>Time in milliseconds</returns>
        private long ConvertToMillis(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000;
            return epoch;
        }

        /// <summary>
        /// Builds the norification from specified data
        /// </summary>
        /// <returns>Built notification</returns>
        public IOSNotification build()
        {
            IOSNotification n = new IOSNotification(
                id:             _id,     
      
                title:          _title,         
                body:           _body,          
                sound:          _sound,         
      
                delay:          _delay,         
                interval:       _interval,      
                repeating:      _isRepeating,   
                number:         _number       

                );


            return n;
        }

        /// <summary>
        /// Builds notification from NotificationInstance - created in editor
        /// </summary>
        /// <param name="notif">Instance created in Notifications Window</param>
        /// <returns>Notification buildder</returns>
        public static IOSNotificationBuilder FromInstance(IOSNotificationInstance notif)
        {
            IOSNotificationBuilder builder = new IOSNotificationBuilder(notif.id, notif.title, notif.body);

//            if (notif.smallIcon != null)
//            {
//                builder.setSmallIcon(notif.smallIcon.name);
//            }
//
//            if (notif.largeIcon != null)
//            {
//                builder.setLargeIcon(notif.largeIcon.name);
//            }
//
//            if (notif.ticker != null && notif.ticker.Length > 0)
//            {
//                builder.setTicker(notif.ticker);
//            }

            int defaults = 0;

            // Handle sound
            if (notif.defaultSound)
            {
                defaults |= IOSNotificationBuilder.DEFAULT_SOUND;
            } else
            {
                if (notif.soundFile != null)
                {
                    builder.setSound(notif.soundFile.name);
                }
            }

            if (notif.number > 0)
            {
                builder.setNumber(notif.number);
            }

            // Repeating and interval
            if (notif.isRepeating)
            {
				//TODO
                builder.setRepeating(true);

                builder.setInterval(notif.interval);
            }
            
            // Delay
            long delaySeconds   = notif.delayHours * 3600 + notif.delayMinutes * 60 + notif.delaySeconds;
            builder.setDelay(delaySeconds);

            return builder;
        }


     

    }
}