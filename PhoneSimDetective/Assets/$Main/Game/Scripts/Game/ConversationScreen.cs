using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ConversationScreen : MonoBehaviour {

    public TextMeshProUGUI nameOfPerson;
    public GameObject attachEvidenceButton;
    public Image profilePic;
    public ScrollRect scrollRect;
    void OnEnable()
    {
        nameOfPerson.text = GameManager.instance.messageManager.nameOfCurrentPerson.ToString();
        profilePic.sprite = GameManager.instance.messageManager.peopleInfo.Find(x => x.nameOfPerson == GameManager.instance.messageManager.nameOfCurrentPerson).displayPicture;
        GameManager.instance.messageManager.CreateCurrentMessages();
        /*if (GameManager.instance.IsInPresent)
        {
            attachEvidenceButton.SetActive(true);
        }
        else
        {
            attachEvidenceButton.SetActive(false);

        }*/
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }


    public void GoToTop()
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }

    public void AttachEvidence()
    {
        if (TutorialManager.instance.isTutorial2Active)
        {
            TutorialManager.instance.EndEvidenceTutorial2();
        }

        Debug.Log("Attach Evidence");
        MyGameFlow.instance.SwitchScreen(MyGameFlow.instance.evidenceScreen,null,0);
        if (GameManager.instance.evidenceCollected.Count > 0)
        {
            UIManager.instance.EvidenceScreen.GetComponent<EvidenceScreen>().TabClick(2);
        }
        else
        {
            UIManager.instance.EvidenceScreen.GetComponent<EvidenceScreen>().TabClick(0);
        }
    }

    public void Scrolled()
    {
        foreach(Transform child in GameManager.instance.messageManager.conversationContent.transform)
        {
            if(child.GetComponent<Chat>()!=null)
            child.GetComponent<Chat>().CheckSeen();
        }
    }
}
