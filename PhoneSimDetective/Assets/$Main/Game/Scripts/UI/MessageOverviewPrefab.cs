using MyStory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MessageOverviewPrefab : MonoBehaviour
{
    [HideInInspector]
    public People person;
    public Image profilePic;
    public TextMeshProUGUI nameOfPerson;
    public TextMeshProUGUI lastMessage;
    public TextMeshProUGUI status;
    public TextMeshProUGUI timeText;
    public void SetData(People n, string l, string s,Sprite image,GTime t)
    {
        person = n;
        nameOfPerson.text = n.ToString();
        lastMessage.text = l;
        //status.text = "";
        profilePic.sprite = image;
        timeText.text = t.GetTimeHourMinutes();
        //TODO: Seen Implementation after design
        /*status.text = s;
        if(s != "Seen")
        {
            status.color = new Color(1, 0, 0);
        }*/
    }

    public void OnClick()
    {
        SoundManager.instance.PlaySound(Sounds.Click);
        GameManager.instance.messageManager.nameOfCurrentPerson = person;
        UIManager.instance.OnChatScreen();
    }
}
