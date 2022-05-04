using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyStory;
public class DateSetHandler : MonoBehaviour
{
    public static DateSetHandler instance = null;
    public KnobHandlerDay dayHandler;
    public KnobHandlerMonthYear monthYearHandler;
    public KnobHandlerHour hourHandler;
    public KnobHandlerMinutes minutesHandler;
    public GameObject datePickerScreen;
    public GameObject rotatingTutorial;
    float lastPlayedTime = 0;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetDate()
    {
        UIManager.instance.OnDateSet(new MyStory.GTime(hourHandler.SetHour, minutesHandler.SetMinutes, 0),
            new MyStory.GDate(monthYearHandler.SetYear, (MyStory.Month)monthYearHandler.SetMonth, dayHandler.SetDay));
        GameManager.instance.Initialize();
        UIManager.instance.OnLockScreen(true);
        CancelDate();
    }

    public void CancelDate()
    {
        datePickerScreen.SetActive(false);
    }



    public void GoToPresent()
    {
        UIManager.instance.OnDateSet(GameManager.instance.presentTime,
            GameManager.instance.presentDate);
        GameManager.instance.Initialize();
        UIManager.instance.OnLockScreen(true);
        CancelDate();
    }

    public void OnMonthYearSet()
    {
        RotateSound();
        rotatingTutorial.SetActive(false);
        Month month = monthYearHandler.SetMonth;

        if(month == Month.FEB)
        {
            if(monthYearHandler.SetYear == 2020)
            {
                if (dayHandler.SetDay > 29)
                {
                    dayHandler.SetDay = 29;
                    dayHandler.ChangeDisplay();
                }
            }
            else
            {
                if (dayHandler.SetDay > 28)
                {
                    dayHandler.SetDay = 28;
                    dayHandler.ChangeDisplay();
                }
            }
        }

        if(month == Month.APR || month == Month.JUN || month == Month.SEP || month == Month.NOV)
        {
            if (dayHandler.SetDay > 30)
            {
                dayHandler.SetDay = 30;
                dayHandler.ChangeDisplay();
            }
        }
    }


    public void RotateSound()
    {
        if(Time.time - lastPlayedTime >= 0.5f)
        {
        SoundManager.instance.PlaySound(Sounds.DialRotate);
            lastPlayedTime = Time.time;
        }
    }
}
