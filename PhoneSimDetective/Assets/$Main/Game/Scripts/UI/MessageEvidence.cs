using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyStory;
public class MessageEvidence : MonoBehaviour
{
    public TextMeshProUGUI nameOfSender;
    public TextMeshProUGUI dateAndTime;
    public TextMeshProUGUI messageContent;
    public GameObject attachButton;
    MyStory.GEvidence evidence= new GEvidence();
    public GameObject arrowtut;
    public GEvidence Evidence
    {
        get { return evidence; }
    }


    private void Start()
    {
        if(PlayerPrefs.GetInt(SaveData.Tutorial2Done.ToString(), 0) != 1)
        {
            arrowtut.SetActive(true);
        }
    }

    public void SetData(string n, MyStory.GDate d, MyStory.GTime t, string message, MyStory.GEvidence ev)

    {
        nameOfSender.text = n;
        dateAndTime.text = "Date : " + d.Day + " " + d.Month.ToString() + " " + d.Year.ToString() + "\n" + "Time : " + t.GetTimeHourMinutes(); //Hour.ToString() + ":" + t.Minutes.ToString();
        messageContent.text = message;
        evidence = ev;
    }

    public void AttachEvidence()
    {
        if (arrowtut.activeSelf) {
            PlayerPrefs.SetInt(SaveData.Tutorial2Done.ToString(), 1);
            arrowtut.SetActive(false);
         }
        string mContent = "<b>Sent by : "+ nameOfSender.text+ "</b>" +"\n" + "\"" + messageContent.text + "\"" ;

        GameManager.instance.messageManager.StartTypeAnimationAndResponse(evidence, 
            new GMessage(mContent, People.Amira, GameManager.instance.messageManager.nameOfCurrentPerson, GameManager.instance.presentDate, GameManager.instance.presentTime, false, new GDate(), new GTime(), false, null, true));
        

        //GameManager.instance.InstantiateMessage(evidenceMessageContainer);
    }

    public void RemoveEvidence()
    {
        GameManager.instance.evidenceCollected.RemoveAll(x => x == evidence);
        GameManager.instance.RemoveEvidence(evidence, TypeOfMessage.Text);
    }


   
}
