using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStory
{
    [System.Serializable]
    public class GTime
    {

        [SerializeField] int hour = 0;
        [SerializeField] int minutes = 0;
        [SerializeField] int seconds = 0;

        public GTime()
        {
            hour = 0;
            minutes = 0;
            seconds = 0;
        }

        public GTime(int h, int m, int s)
        {
            hour = h;
            minutes = m;
            seconds = s;
        }

        public Comparator Compare(GTime otherTime)
        {

            if (this.Hour == otherTime.Hour)
            {
                if (this.Minutes == otherTime.Minutes)
                {
                    if (this.Seconds == otherTime.Seconds)
                    {
                        return Comparator.EQUAL;
                    }
                    else if (this.Seconds > otherTime.Seconds)
                    {
                        return Comparator.GREATER;
                    }
                    else
                    {
                        return Comparator.LESSER;
                    }
                }
                else
                {
                    if (this.Minutes > otherTime.Minutes)
                    {
                        return Comparator.GREATER;
                    }
                    else
                    {
                        return Comparator.LESSER;
                    }
                }
            }
            else
            {
                if (this.Hour > otherTime.Hour)
                {
                    return Comparator.GREATER;
                }
                else
                {
                    return Comparator.LESSER;
                }
            }
        }

        public int Hour
        {
            get { return hour; }
        }

        public int Minutes
        {
            get { return minutes; }
        }

        public int Seconds
        {
            get { return seconds; }
        }

        public string GetTimeHourMinutes() {
            return string.Format("{0}:{1}",
           Hour.ToString("00"),
           Minutes.ToString("00"));
        }
        public string GetTimeHourMinutesSeconds()
        {
            return string.Format("{0}:{1}:{2}",
            Hour.ToString("00"),
            Minutes.ToString("00"),
            Seconds.ToString("00"));
        }

        public bool AddSecond()
        {
            seconds += 1;
            if(seconds >= 60)
            {
                minutes += 1;
                seconds = 0;
                if (minutes >= 60)
                {
                    hour++;
                    minutes = 0;
                    if (hour >= 24)
                    {
                        hour = 0;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
