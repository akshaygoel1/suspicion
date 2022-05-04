using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyStory;
public class ChatMessage : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI timeText;
    public Image chatBox;
    public void SetChatText(GMessage message)
    {

        if (!message.Seen)
        {
            chatBox.color = new Color(0.838f, 0.96f, 0.97f, 1);
        }
        image.sprite = GameManager.instance.galleryManager.galleryImages.Find(x=>x.name == message.nameOfImage).galleryObject.Image;

        timeText.text = message.TimeOfMessage.GetTimeHourMinutes(); //Hour.ToString() + ":" + message.TimeOfMessage.Minutes.ToString();
        StartCoroutine(ParentFit());
    }
    IEnumerator ParentFit()
    {
        yield return new WaitForEndOfFrame();
        RectTransform myRect = GetComponent<RectTransform>();
        RectTransform childRect = transform.GetChild(0).GetComponent<RectTransform>();
        myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, childRect.sizeDelta.y);
    }
}
