using System;

namespace Area730.Notifications.IOS
{

	public enum IntervalUnits
	{
		MINUTE = 0, HOUR,DAY,MONTH,YEAR
	};


    /// <summary>
    /// Class holds all required data for the notification
    /// </summary>
    public class IOSNotification
    {
        #region Private

        private int     			_id;
        private string  			_sound;
        private string  			_title;
        private string  			_body;
        private long    			_delay;
        private bool    			_isRepeating;
		private IntervalUnits     	_interval;
        private int     			_number;

        #endregion

        #region Modifiers

        public int   			ID              { get { return _id; } }
        public String   		Sound           { get { return _sound; } }
        public String   		Title           { get { return _title; } }
        public String   		Body            { get { return _body; } }
        public long    			Delay           { get { return _delay; } }
        public bool     		IsRepeating     { get { return _isRepeating; } }
		public IntervalUnits    Interval        { get { return _interval; } }// 0 - minute 1 - hour 2-day 3-month 4 - year 
        public int      		Number          { get { return _number; } }

        #endregion


        public IOSNotification ( 
            int id, 
            string sound, 
            string title, 
            string body, 
            long delay,
            bool repeating,
		    IntervalUnits interval,
            int number
         
		                     )
        {
            _id             = id;
            _sound          = sound;
            _title          = title;
            _body           = body;
            _delay          = delay;
            _isRepeating    = repeating;
            _interval       = interval;
            _number         = number;
            
        }

        public override string ToString()
        {
            string res = "Notification: " + "\n"
                + "ID: " + _id + "\n"
				+ ", number: " + _number + "\n"
                + ", Sound: " + _sound + "\n"
                + ", Title: " + _title + "\n"
                + ", Body: " + _body + "\n"
                + ", delay: " + _delay + "\n"
                + ", isRepeating: " + _isRepeating + "\n"
                + ", interval: " + _interval + "\n";

            return res;
        }


    }
}
