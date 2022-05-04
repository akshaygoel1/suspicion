using MyStory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LockManager : MonoBehaviour
{
    string currentPassword = "";

    public List<LockScreenContainer> lockContainer = new List<LockScreenContainer>();
    public Text lockScreenText;

    public TMPro.TextMeshProUGUI lockScreenDateText;

    public string CurrentPassword
    {
        get { return currentPassword; }
        set { currentPassword = value; }
    }

    public void GetCurrentLockData()
    {
        currentPassword = "";
        string info = "";
        GLockScreen currentLockScreen = new GLockScreen();
        foreach (LockScreenContainer l in lockContainer)
        {
            if (GameManager.instance.currentDate.Compare(l.lockScreenObject.DateOfPasswordSet) == Comparator.GREATER ||
                (GameManager.instance.currentDate.Compare(l.lockScreenObject.DateOfPasswordSet) == Comparator.EQUAL &&
                (GameManager.instance.currentTime.Compare(l.lockScreenObject.TimeOfPasswordSet) == Comparator.GREATER || GameManager.instance.currentTime.Compare(l.lockScreenObject.TimeOfPasswordSet) == Comparator.EQUAL)))
            {
                currentPassword = l.lockScreenObject.Password;
                info = l.lockScreenObject.Info;
                currentLockScreen = l.lockScreenObject;
            }
            else
            {
                break;
            }
        }
        lockScreenText.text = info;
        lockScreenDateText.text = "Password Set On\n" + currentLockScreen.DateOfPasswordSet.GetDate() + "\n" + currentLockScreen.TimeOfPasswordSet.GetTimeHourMinutes();
    }
}
