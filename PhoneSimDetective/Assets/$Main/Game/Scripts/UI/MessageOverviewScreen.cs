using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageOverviewScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        GameManager.instance.messageManager.RefreshLastMessage();
    }


}
