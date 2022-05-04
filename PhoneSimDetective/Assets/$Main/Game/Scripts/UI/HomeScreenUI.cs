using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenUI : MonoBehaviour
{
    public void FeedbackButtonClicked()
    {
        Application.OpenURL("https://forms.gle/WqALX26wNZmCP6g37");
    }
}
