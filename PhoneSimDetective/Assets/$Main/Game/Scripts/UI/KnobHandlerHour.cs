using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using MyStory;
public class KnobHandlerHour : MonoBehaviour
{
    UI_Knob thisKnob;
    public TMPro.TextMeshProUGUI display;
    int setHour=0;
   
    public int SetHour
    {
        get { return setHour; }
    }
    private void Start()
    {
        thisKnob = GetComponent<UI_Knob>();
        display.text = GameManager.instance.currentTime.Hour.ToString();

        thisKnob.CurrentLoops = (GameManager.instance.currentTime.Hour) / 2;
        thisKnob.knobValue = ((GameManager.instance.currentTime.Hour) / 2) - thisKnob.CurrentLoops;
        setHour = GameManager.instance.currentTime.Hour;
        thisKnob.SetData();
    }
    public void KnobRotated()
    {
        if ((int)((thisKnob.knobValue + thisKnob.CurrentLoops) * 2 ) < 24)
        {
            setHour = (int)((thisKnob.knobValue + thisKnob.CurrentLoops) * 2);
            display.text = setHour.ToString();
            DateSetHandler.instance.RotateSound();
        }
    }
}
