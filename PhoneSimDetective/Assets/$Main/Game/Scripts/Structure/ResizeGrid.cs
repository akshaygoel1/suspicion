using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResizeGrid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float x = GetComponent<RectTransform>().rect.width;
        Vector2 size = GetComponent<GridLayoutGroup>().cellSize;
        size.x = x;
        GetComponent<GridLayoutGroup>().cellSize = size;
    }

}
