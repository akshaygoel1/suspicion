using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using MyStory;
public class KnobHandlerDay : MonoBehaviour
{
    UI_Knob thisKnob;
    public TMPro.TextMeshProUGUI display;
    int setDay=1;
    public int SetDay
    {
        get { return setDay;}
        set { setDay = value; }
    }
    private void Start()
    {
        thisKnob = GetComponent<UI_Knob>();

        display.text = GameManager.instance.currentDate.Day.ToString();
        thisKnob.CurrentLoops = (GameManager.instance.currentDate.Day - 1) / 2;
        thisKnob.knobValue = ((GameManager.instance.currentDate.Day - 1) / 2)-thisKnob.CurrentLoops;
        thisKnob.SetData();
        setDay = GameManager.instance.currentDate.Day;
    }
    public void KnobRotated()
    {
        setDay = (int)((thisKnob.knobValue + thisKnob.CurrentLoops) * 2 + 1);
        display.text = setDay.ToString();
        DateSetHandler.instance.OnMonthYearSet();
    }

    public void ChangeDisplay()
    {
        display.text = setDay.ToString();
        /*thisKnob.CurrentLoops = (setDay- 1) / 10;
        thisKnob.knobValue = ((setDay - 1) / 10) - thisKnob.CurrentLoops;
        thisKnob.SetData();*/
    }
}
