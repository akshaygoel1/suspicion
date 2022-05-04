using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class LockScreen : MonoBehaviour
{
    private void OnEnable()
    {
        if (MyGameFlow.instance.previousScreen == MyGameFlow.instance.evidenceScreen)
            
        {
            TutorialManager.instance.ShowHelper1();
        }
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        File.Delete(Application.persistentDataPath + "/OldMessages.xml");
        File.Delete(Application.persistentDataPath + "/NewMessages.xml");

    }
}
