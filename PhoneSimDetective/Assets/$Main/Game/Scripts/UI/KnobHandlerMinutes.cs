using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using MyStory;
public class KnobHandlerMinutes : MonoBehaviour
{
    UI_Knob thisKnob;
    public TMPro.TextMeshProUGUI display;
    int setMinutes=0;
    public int SetMinutes
    {
        get { return setMinutes; }
    }
    private void Start()
    {
        thisKnob = GetComponent<UI_Knob>();
        display.text =GameManager.instance.currentTime.Minutes.ToString();
        thisKnob.CurrentLoops = (GameManager.instance.currentTime.Minutes) / 10;
        thisKnob.knobValue = ((GameManager.instance.currentTime.Minutes) / 10) - thisKnob.CurrentLoops;
        setMinutes = GameManager.instance.currentTime.Minutes;
        thisKnob.SetData();
    }
    public void KnobRotated()
    {
        if ((int)((thisKnob.knobValue + thisKnob.CurrentLoops) * 10 ) < 60)
        {
            setMinutes = (int)((thisKnob.knobValue + thisKnob.CurrentLoops) * 10);
            display.text = setMinutes.ToString();
            DateSetHandler.instance.RotateSound();
        }
    }
}
