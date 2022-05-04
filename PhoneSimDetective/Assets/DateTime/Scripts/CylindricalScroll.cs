using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CylindricalScroll : MonoBehaviour
{
    [SerializeField]
    int StartNumber, NumberOfNumbers, FontSize;
    [SerializeField]
    float RotateSpeed, Drag;
    [SerializeField]
    GameObject SampleTextField;
    [SerializeField]
    Transform CentralSpinner;
    List<GameObject> SpawnedNumbers = new List<GameObject>();
    float Angle;
    float Velocity;
    private void Start()
    {
        NumberOfNumbers = CentralSpinner.childCount;
        Angle = 360 / NumberOfNumbers;
        OnNumberChanged();
        DisplayNumbers();
    }

    public void OnNumberChanged()
    {
        SpawnedNumbers.Clear();
        Angle = 360 / NumberOfNumbers;
        float ang = 0;
        for(int i = 0; i < NumberOfNumbers; i++)
        {
            GameObject g = CentralSpinner.GetChild(i).gameObject;
            g.transform.position = CentralSpinner.position + GetRotatedPoint(Vector3.back * 0.5f, Vector3.right, Angle * i);
            g.transform.rotation = Quaternion.Euler(Vector3.right * ang);
            ang += Angle;
            SpawnedNumbers.Add(g);
        }
        DisplayNumbers();
    }
    private void DisplayNumbers()
    {
        for (int i = 0; i < SpawnedNumbers.Count; i++)
        {
            TextMesh tm = SpawnedNumbers[i].GetComponent<TextMesh>();
            tm.text = (StartNumber + i).ToString();
            tm.fontSize = FontSize;
        }
    }

    Vector3 GetRotatedPoint(Vector3 originalPoint, Vector3 normal, float angle)
    {
        Vector3 point = new Vector3();
        Quaternion q = Quaternion.AngleAxis(angle, normal);
        point = q * originalPoint;
        return point;
    }

    public void ChangeVelocity(float velocity)
    {
        Velocity += velocity;
    }

    public void SetVelocity(float velocity)
    {
        Velocity = velocity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetVelocity(0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            ChangeVelocity(1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            ChangeVelocity(-1);
        }




        if (Mathf.Abs(Velocity) > 0.1f)
        {
            CentralSpinner.Rotate(Vector3.right * Velocity * RotateSpeed * Time.deltaTime);
            Velocity += Time.deltaTime * Drag * (Velocity > 0 ? -1 : 1);
        }
        else
        {
            CentralSpinner.rotation = Quaternion.Lerp(CentralSpinner.rotation, 
                Quaternion.Euler(Vector3.right * GetClosestAngle()), 
                Time.deltaTime * RotateSpeed);
        }
        
    }

    float GetClosestAngle()
    {
        float f = 0;
        float Min = Angle;
        for(int i = 0; i < NumberOfNumbers; i++)
        {
            if(Quaternion.Angle(CentralSpinner.rotation, Quaternion.Euler(i * Angle,0,0)) < Min)
            {
                f = i;
                Min = Quaternion.Angle(CentralSpinner.rotation, Quaternion.Euler(i * Angle, 0, 0));
            }
        }
       // Debug.Log("Stopping at " + (NumberOfNumbers - f + StartNumber) % StartNumber);

        return f * Angle;
    }
}
