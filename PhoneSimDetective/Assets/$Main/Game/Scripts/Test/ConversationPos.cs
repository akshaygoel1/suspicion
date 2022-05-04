using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationPos : MonoBehaviour
{
    public RectTransform conversation;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Conversation : " + conversation.localPosition.y);
        }
    }
}
