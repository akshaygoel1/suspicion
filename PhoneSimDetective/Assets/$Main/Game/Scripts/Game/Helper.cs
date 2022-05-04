using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public GameObject Helper1Panel;


    public void Helper1()
    {
        SoundManager.instance.PlaySound(Sounds.QMark);
        Helper1Panel.SetActive(true);
        this.gameObject.SetActive(false);
    }


}
