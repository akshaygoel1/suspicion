using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class ParentFit : MonoBehaviour
{

    RectTransform myRect;
    RectTransform childRect;
    void Start()
    {
        
    }

    void OnEnable()
    {
        myRect = GetComponent<RectTransform>();
        childRect = transform.GetChild(0).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, childRect.sizeDelta.y);
    }
}
