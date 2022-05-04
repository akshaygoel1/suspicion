using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DateSetter : MonoBehaviour
{
    public TextMeshProUGUI dateText;

    public void SetDateText(MyStory.GDate date)
    {
        dateText.text = date.Day.ToString() + " " + date.Month.ToString() + " " + date.Year.ToString();
    }
}
