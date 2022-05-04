using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStory
{
    [System.Serializable]
    public class GDate
    {

        [SerializeField]int year = 2001;
        [SerializeField] Month month = Month.JAN;
        [SerializeField] int day = 1;

        public GDate()
        {
            year = 2001;
            month = Month.JAN;
            day = 1;
        }

        public GDate(int y, Month m, int d)
        {
            year = y;
            month = m;
            day = d;
        }

        bool IsLeapYear()
        {
            if (year % 4 == 0)
            {
                return true;
            }
            return false;
        }

        public bool DateExists()
        {
            if (day <= 30 && month != Month.FEB)
            {
                return true;
            }
            else if (((day <= 29 && IsLeapYear()) || (day <= 28)) && month == Month.FEB)
            {
                return true;
            }
            else if (day == 31 && (month == Month.JAN || month == Month.MAR || month == Month.MAY || month == Month.JUL || month == Month.AUG || month == Month.OCT || month == Month.DEC))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Compares the Date with Other Date
        /// </summary>
        /// <param name="otherDate"></param>
        /// <returns>Returns Greater if this Date is Greater than other Date</returns>
        public Comparator Compare(GDate otherDate)
        {
            
            if(this.Year == otherDate.Year)
            {
                if(this.Month == otherDate.Month)
                {
                    if(this.Day == otherDate.Day)
                    {
                        return Comparator.EQUAL;
                    }
                    else if(this.Day > otherDate.Day)
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
                    if (this.Month > otherDate.Month)
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
                if (this.Year > otherDate.Year)
                {
                    return Comparator.GREATER;
                }
                else
                {
                    return Comparator.LESSER;
                }
            }


        }

        public int Day
        {
            get { return day; }
        }

        public Month Month
        {
            get { return month; }
        }

        public int Year
        {
            get { return year; }
        }

        public string GetDate()
        {
            return string.Format("{0} {1} {2}",
            Day.ToString("00"),
            Month.ToString(),
            Year.ToString("0000"));
        }

        public string GetDateDayMonth()
        {
            return string.Format("{0} {1}",
            Day.ToString("00"),
            Month.ToString());
        }

        public void AddDay()
        {
            day++;
            if (!DateExists())
            {
                month++;
                day = 1;
                if ((int)month > 12) {
                    month = Month.JAN;
                    year++;
                }
            }
        }
    }

    public enum Month
    {
        JAN = 1,
        FEB,
        MAR,
        APR,
        MAY,
        JUN,
        JUL,
        AUG,
        SEP,
        OCT,
        NOV,
        DEC
    }

    public enum Comparator
    {
        GREATER,
        LESSER,
        EQUAL
    }

    /*public enum Year
    {
        Y2001=1,
        Y2002,
        Y2003,
        Y2004,
        Y2005,
        Y2006,
        Y2007,
        Y2008,
        Y2009,
        Y2010,
        Y2011,
        Y2012,
        Y2013,
        Y2014,
        Y2015,
        Y2016,
        Y2017,
        Y2018,
        Y019,

    }*/
}