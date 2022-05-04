using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper1Panel : MonoBehaviour
{

    public void Back()
    {
        this.gameObject.SetActive(false);
        TutorialManager.instance.StartDateTutorial();
    }
}
