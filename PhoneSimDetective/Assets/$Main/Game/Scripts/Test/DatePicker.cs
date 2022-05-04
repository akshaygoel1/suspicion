using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatePicker : MonoBehaviour {

    int Hour, Minute, Second, Day, Month, Year;
    public static DatePicker instance;
    [SerializeField]
    GameObject DatePickerScreen;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            Transform t = DatePickerScreen.transform.GetChild(0);
            for (int i = 0; i < DatePickerScreen.transform.GetChild(0).childCount; i++)
            {
                t.GetChild(i).Find("Counter").Find("Title").GetComponent<Text>().text = t.GetChild(i).name;
            }
            DatePickerScreen.SetActive(false);
        }
        else
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// Open the date picker
    /// </summary>
    /// <param name="h">Hour</param>
    /// <param name="M">Minute</param>
    /// <param name="s">Second</param>
    /// <param name="d">Day</param>
    /// <param name="m">Month</param>
    /// <param name="y">Year</param>
    public void StartDatePicker(int h, int M, int s, int d, int m, int y)
    {
        Hour = h;
        Minute = M;
        Second = s;
        Day = d;
        Month = m;
        Year = y;

        DatePickerScreen.transform.Find("Holder").Find("Hour").Find("Counter").Find("Count").GetComponent<Text>().text = Hour.ToString("00");
        DatePickerScreen.transform.Find("Holder").Find("Minute").Find("Counter").Find("Count").GetComponent<Text>().text = Minute.ToString("00");
        DatePickerScreen.transform.Find("Holder").Find("Second").Find("Counter").Find("Count").GetComponent<Text>().text = Second.ToString("00");
        DatePickerScreen.transform.Find("Holder").Find("Day").Find("Counter").Find("Count").GetComponent<Text>().text = Day.ToString("00");
        DatePickerScreen.transform.Find("Holder").Find("Month").Find("Counter").Find("Count").GetComponent<Text>().text = Month.ToString("00");
        DatePickerScreen.transform.Find("Holder").Find("Year").Find("Counter").Find("Count").GetComponent<Text>().text = Year.ToString("00");

        DatePickerScreen.SetActive(true);

    }

    public void SetDate()
    {

        DatePickerScreen.SetActive(false);
        UIManager.instance.OnDateSet(new MyStory.GTime(Hour, Minute, Second),
            new MyStory.GDate(Year, (MyStory.Month)Month, Day));
        GameManager.instance.Initialize();
        UIManager.instance.OnLockScreen();
        GameManager.instance.CheckIfInPresent();
    }

    public void CancelDate()
    {
        DatePickerScreen.SetActive(false);
    }

    public void Increment(Transform Holder)
    {
        Holder.Find("Decrease").gameObject.SetActive(true);
        switch (Holder.gameObject.name)
        {
            case "Hour":
                Hour++;
                DatePickerScreen.transform.Find("Holder").Find("Hour").Find("Counter").Find("Count").GetComponent<Text>().text = Hour.ToString("00");
                if(Hour >= 23)
                {
                    Holder.Find("Increase").gameObject.SetActive(false);
                }
                break;
            case "Minute":
                Minute++;
                DatePickerScreen.transform.Find("Holder").Find("Minute").Find("Counter").Find("Count").GetComponent<Text>().text = Minute.ToString("00");
                if (Minute >= 59)
                {
                    Holder.Find("Increase").gameObject.SetActive(false);
                }
                break;
            case "Second":
                Second++;
                DatePickerScreen.transform.Find("Holder").Find("Second").Find("Counter").Find("Count").GetComponent<Text>().text = Second.ToString("00");
                if (Second >= 59)
                {
                    Holder.Find("Increase").gameObject.SetActive(false);
                }
                break;
            case "Day":
                Day++;
                DatePickerScreen.transform.Find("Holder").Find("Day").Find("Counter").Find("Count").GetComponent<Text>().text = Day.ToString("00");
                if (Day >= 31 && (Month==1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12))
                {
                    Holder.Find("Increase").gameObject.SetActive(false);
                }
                else if(Day >= 30 && (Month == 4 || Month == 6 || Month == 9 || Month == 11))
                {
                    Holder.Find("Increase").gameObject.SetActive(false);
                }
                else if(Day >= 28 && Month == 2 && Year % 4 != 0)
                {
                    Holder.Find("Increase").gameObject.SetActive(false);
                }
                else if (Day >= 29 && Month == 2 && Year % 4 == 0)
                {
                    Holder.Find("Increase").gameObject.SetActive(false);
                }
                break;
            case "Month":
                Month++;
                DatePickerScreen.transform.Find("Holder").Find("Month").Find("Counter").Find("Count").GetComponent<Text>().text = Month.ToString("00");
                if (Month >= 12)
                {
                    Holder.Find("Increase").gameObject.SetActive(false);
                }
                break;
            case "Year":
                Year++;
                DatePickerScreen.transform.Find("Holder").Find("Year").Find("Counter").Find("Count").GetComponent<Text>().text = Year.ToString("00");
                break;

        }
    }
    public void Decrement(Transform Holder)
    {
        Holder.Find("Increase").gameObject.SetActive(true);
        switch (Holder.gameObject.name)
        {
            case "Hour":
                Hour--;
                DatePickerScreen.transform.Find("Holder").Find("Hour").Find("Counter").Find("Count").GetComponent<Text>().text = Hour.ToString("00");
                if (Hour <= 0)
                {
                    Holder.Find("Decrease").gameObject.SetActive(false);
                }
                break;
            case "Minute":
                Minute--;
                DatePickerScreen.transform.Find("Holder").Find("Minute").Find("Counter").Find("Count").GetComponent<Text>().text = Minute.ToString("00");
                if (Minute <= 0)
                {
                    Holder.Find("Decrease").gameObject.SetActive(false);
                }
                break;
            case "Second":
                Second--;
                DatePickerScreen.transform.Find("Holder").Find("Second").Find("Counter").Find("Count").GetComponent<Text>().text = Second.ToString("00");
                if (Second <= 0)
                {
                    Holder.Find("Decrease").gameObject.SetActive(false);
                }
                break;
            case "Day":
                Day--;
                DatePickerScreen.transform.Find("Holder").Find("Day").Find("Counter").Find("Count").GetComponent<Text>().text = Day.ToString("00");
                if (Day <= 0)
                {
                    Holder.Find("Decrease").gameObject.SetActive(false);
                }
                break;
            case "Month":
                Month--;
                DatePickerScreen.transform.Find("Holder").Find("Month").Find("Counter").Find("Count").GetComponent<Text>().text = Month.ToString("00");
                if (Month <= 1)
                {
                    Holder.Find("Decrease").gameObject.SetActive(false);
                }
                break;
            case "Year":
                Year--;
                DatePickerScreen.transform.Find("Holder").Find("Year").Find("Counter").Find("Count").GetComponent<Text>().text = Year.ToString("00");
                if (Year <= 1)
                {
                    Holder.Find("Decrease").gameObject.SetActive(false);
                }
                break;

        }
    }

    public void GoToPresent()
    {
        UIManager.instance.OnDateSet(GameManager.instance.presentTime,
             GameManager.instance.presentDate);
        GameManager.instance.Initialize();
        UIManager.instance.OnLockScreen();
        GameManager.instance.CheckIfInPresent();

        CancelDate();
    }
}
