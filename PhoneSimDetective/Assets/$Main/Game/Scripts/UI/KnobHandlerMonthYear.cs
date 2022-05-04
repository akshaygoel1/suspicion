using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using MyStory;
public class KnobHandlerMonthYear : MonoBehaviour
{
    UI_Knob thisKnob;
    public TMPro.TextMeshProUGUI display;
    Month setMonth;
    public Month SetMonth
    {
        get { return setMonth; }
    }
    int setYear;
    public int SetYear
    {
        get { return setYear; }
    }
    private void Start()
    {
        thisKnob = GetComponent<UI_Knob>();
        display.text = GameManager.instance.currentDate.Month.ToString() + " " + GameManager.instance.currentDate.Year.ToString();
        setMonth = GameManager.instance.currentDate.Month;
        setYear = GameManager.instance.currentDate.Year;
        if (GameManager.instance.currentDate.Month == Month.DEC && GameManager.instance.currentDate.Year == 2019)
        {

            thisKnob.CurrentLoops = 0;
            thisKnob.knobValue = 0;
        }
        else if (GameManager.instance.currentDate.Month == Month.JAN && GameManager.instance.currentDate.Year == 2020)
        {

            thisKnob.CurrentLoops = 0;
            thisKnob.knobValue = 0.5f;
        }
        else if (GameManager.instance.currentDate.Month == Month.FEB && GameManager.instance.currentDate.Year == 2020)
        {

            thisKnob.CurrentLoops = 01;
            thisKnob.knobValue = 0f;
        }
        else if (GameManager.instance.currentDate.Month == Month.MAR && GameManager.instance.currentDate.Year == 2020)
        {

            thisKnob.CurrentLoops = 1;
            thisKnob.knobValue = 0.5f;
        }
        else if (GameManager.instance.currentDate.Month == Month.APR && GameManager.instance.currentDate.Year == 2020)
        {

            thisKnob.CurrentLoops = 2;
            thisKnob.knobValue = 0f;
        }
        else if (GameManager.instance.currentDate.Month == Month.MAY && GameManager.instance.currentDate.Year == 2020)
        {

            thisKnob.CurrentLoops = 2;
            thisKnob.knobValue = 0.5f;
        }
        else if (GameManager.instance.currentDate.Month == Month.JUN && GameManager.instance.currentDate.Year == 2020)
        {

            thisKnob.CurrentLoops = 3;
            thisKnob.knobValue = 0f;
        }
        thisKnob.SetData();
    }
    public void KnobRotated()
    {
        if(thisKnob.CurrentLoops + thisKnob.KnobValue == 0)
        {
            display.text = "Dec 2019";
            setMonth = Month.DEC;
            setYear = 2019;
        }
        else if (thisKnob.CurrentLoops + thisKnob.KnobValue == 0.5f)
        {
            display.text = "Jan 2020";
            setMonth = Month.JAN;
            setYear = 2020;
        }
        else if (thisKnob.CurrentLoops + thisKnob.KnobValue == 1f)
        {
            display.text = "Feb 2020";
            setMonth = Month.FEB;
            setYear = 2020;
        }
        else if (thisKnob.CurrentLoops + thisKnob.KnobValue == 1.5f)
        {
            display.text = "Mar 2020";
            setMonth = Month.MAR;
            setYear = 2020;
        }
        else if (thisKnob.CurrentLoops + thisKnob.KnobValue == 2f)
        {
            display.text = "Apr 2020";
            setMonth = Month.APR;
            setYear = 2020;
        }
        else if (thisKnob.CurrentLoops + thisKnob.KnobValue == 2.5f)
        {
            display.text = "May 2020";
            setMonth = Month.MAY;
            setYear = 2020;
        }
        else if (thisKnob.CurrentLoops + thisKnob.KnobValue == 3f)
        {
            display.text = "Jun 2020";
            setMonth = Month.JUN;
            setYear = 2020;
        }
        DateSetHandler.instance.OnMonthYearSet();
    }
}
