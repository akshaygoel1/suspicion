using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AttachmentInProgress : MonoBehaviour
{
    public Slider slider;


    private void Start()
    {
        StartCoroutine(ProgressIncrease());
    }

    IEnumerator ProgressIncrease()
    {
        while (slider.value < 1)
        {
            yield return new WaitForSeconds(0.1f);
            slider.value += 0.11f;
        }
    }
}
