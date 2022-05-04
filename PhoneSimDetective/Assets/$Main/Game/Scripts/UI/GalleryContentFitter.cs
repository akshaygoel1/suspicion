using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GalleryContentFitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetComponent<RectTransform>().rect.width);
        float cellsize = GetComponent<RectTransform>().rect.width / 4;
        cellsize -= 5;
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellsize, cellsize);
    }

}
