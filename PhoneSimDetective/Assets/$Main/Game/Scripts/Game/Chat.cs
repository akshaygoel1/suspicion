using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyStory;
public class Chat : MonoBehaviour {

    public TMPro.TextMeshProUGUI chatText;
    [HideInInspector]public GMessage currentMessageObject;
    int messageIndex = -1;
    public TMPro.TextMeshProUGUI timeText;
    public Image chatBox;
    public Image evidenceImage;
    public GameObject bookMarkIcon;
    public GameObject deleteIcon;
    public void SetChatText(GMessage messageContainer)
    {
        if (evidenceImage != null)
        {
            if (messageContainer.typeOfMessage == MyStory.TypeOfMessage.Image)
                evidenceImage.gameObject.SetActive(true);
            else
                evidenceImage.gameObject.SetActive(false);
        }
        if (!messageContainer.Seen)
        {
            chatBox.color = new Color(0.838f, 0.96f, 0.97f, 1);
        }

        if (messageContainer.IsDeleted)
        {
            deleteIcon.SetActive(true);
        }
        else
        {
            deleteIcon.SetActive(false);

        }
        chatText.text = messageContainer.MessageContent;
        currentMessageObject = messageContainer;
        timeText.text = currentMessageObject.TimeOfMessage.GetTimeHourMinutes(); //Hour.ToString() + ":" + currentMessageObject.messageObject.TimeOfMessage.Minutes.ToString();
        StartCoroutine(ParentFit());

    }

    IEnumerator ParentFit()
    {
        yield return new WaitForEndOfFrame();
        RectTransform myRect = GetComponent<RectTransform>();
        RectTransform childRect =chatBox.GetComponent<RectTransform>();
        myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, childRect.sizeDelta.y);
    }

    public void SaveEvidence()
    {
        bookMarkIcon.SetActive(true);
        EvidenceContainer evidenceContainer = new EvidenceContainer();
        evidenceContainer.evidenceObject = new MyStory.GEvidence();

        if (currentMessageObject.Evidence != null)
        {
            evidenceContainer.evidenceObject = currentMessageObject.Evidence.evidenceObject;
        }

        GameManager.instance.AddEvidence(evidenceContainer.evidenceObject, currentMessageObject);
        Debug.Log("Evidence Saved!");
    }

    public void CheckSeen()
    {
        if (currentMessageObject.Seen == false)
        {
            if (Mathf.Abs(GameManager.instance.messageManager.conversationContent.GetComponent<RectTransform>().localPosition.y) >= this.GetComponent<RectTransform>().localPosition.y + (this.GetComponent<RectTransform>().sizeDelta.y / 2))
            {
                //seen
                //GameManager.instance.messageManager.messages.Find(x => x == currentMessageObject).messageObject.Seen = true;
                currentMessageObject.Seen = true;
            }
        }
    }
}
