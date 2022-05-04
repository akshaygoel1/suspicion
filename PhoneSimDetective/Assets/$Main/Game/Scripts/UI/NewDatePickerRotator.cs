using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewDatePickerRotator : MonoBehaviour
{
    bool isObjectSelected = false;
    float previousY=-999;
    public CylindricalScroll cylindricalScroll;
    private void Update()
    {

        if (isObjectSelected)
        {

            if (previousY != -999) {
                cylindricalScroll.ChangeVelocity(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - previousY);
               
            }
 
                previousY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            
        }
       

    }


    private void OnMouseDown()
    {
        isObjectSelected = true;
        Debug.Log("Object Selected");
    }

    private void OnMouseUp()
    {
        isObjectSelected = false;
        Debug.Log("Object deselected Selected");


    }
}
