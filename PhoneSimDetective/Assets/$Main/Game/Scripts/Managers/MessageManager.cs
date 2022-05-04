using MyStory;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public GameObject messageOverviewContent;
    public GameObject messageOverviewPrefab;
    public GameObject conversationContent;
    public GameObject conversationPrefab_me;
    public GameObject conversationPrefab_other;
    public GameObject datePrefab;
    [HideInInspector] public People nameOfCurrentPerson;
    public GameObject chatImagePrefab;
    public GameObject typingMessagePrefab;
    public GameObject attachmentInProgress;


    public List<MessageContainer> messages = new List<MessageContainer>();
    // public List<GMessage> newMessages = new List<GMessage>();
    [HideInInspector] public List<GMessage> messages_ = new List<GMessage>();
    public List<PeopleInfo> peopleInfo = new List<PeopleInfo>();
    public string[] nonSuspectResponses = new string[] { };

    private void Awake()
    {
        //TEMP SOLUTION

        foreach (MessageContainer m in messages)
        {
            m.messageObject.nameOfMessageContainer = m.name;
        }
#if UNITY_EDITOR
        if (!File.Exists(Application.dataPath + "/SaveData/OldMessages.xml"))
#else
             if(!File.Exists(Application.persistentDataPath + "/OldMessages.xml"))
#endif
        {
            foreach (MessageContainer m in messages)
            {
                messages_.Add(m.messageObject);
            }
            SaveDataManager.instance.SaveOldMessages();
            SaveDataManager.instance.SaveNewMessages();

        }
        else
        {
            SaveDataManager.instance.LoadOldMessages();
            for (int i = 0; i < SaveDataManager.instance._oldMessages.oldMessages.Count; i++)
            {
                messages_.Add(messages.Find(x => x.name == SaveDataManager.instance._oldMessages.oldMessages[i].nameOfMessage).messageObject);
                messages_[messages_.Count - 1].Seen = SaveDataManager.instance._oldMessages.oldMessages[i].seen;
            }
            SaveDataManager.instance.LoadNewMessages();
            for (int i = 0; i < SaveDataManager.instance._newMessages.newMessages.Count; i++)
            {
                GMessage m_ = new GMessage();
                m_.Seen = true;
                m_.isMessageDeleted = false;
                m_.messageContent = SaveDataManager.instance._newMessages.newMessages[i].messageContent;
                m_.sender = SaveDataManager.instance._newMessages.newMessages[i].sender;
                m_.reciever = SaveDataManager.instance._newMessages.newMessages[i].reciever;
                m_.dateOfMessage = new GDate(SaveDataManager.instance._newMessages.newMessages[i].year, SaveDataManager.instance._newMessages.newMessages[i].month, SaveDataManager.instance._newMessages.newMessages[i].day);
                m_.timeOfMessage = new GTime( SaveDataManager.instance._newMessages.newMessages[i].hour, SaveDataManager.instance._newMessages.newMessages[i].minutes, SaveDataManager.instance._newMessages.newMessages[i].seconds);
                m_.typeOfMessage = SaveDataManager.instance._newMessages.newMessages[i].typeOfMessage;
                m_.nameOfImage = SaveDataManager.instance._newMessages.newMessages[i].nameOfImage;
                m_.isCollectibleEvidence = false;
                messages_.Add(m_);
            }

        }
    }

    public void CreateCurrentMessageOverview()
    {
        foreach (Transform child in messageOverviewContent.transform)
        {
            Destroy(child.gameObject);
        }

        List<People> uniquePeople = new List<People>() { People.Amira };

        //foreach(MessageContainer m in messages)
        for (int i = messages_.Count - 1; i >= 0; i--)
        {
            GMessage m = messages_[i];
            if (GameManager.instance.ShouldContentBeVisible(m.dateOfMessage, m.timeOfMessage, m.IsDeleted, m.DeletedDate, m.DeletedTime))
            {

                if (!uniquePeople.Contains(m.Sender))
                {
                    uniquePeople.Add(m.Sender);
                }
                else if (!uniquePeople.Contains(m.Reciever))
                {
                    uniquePeople.Add(m.Reciever);
                }
            }
        }
        uniquePeople.Remove(People.Amira);

        foreach (People p in uniquePeople)
        {
            GameObject messageOverview = Instantiate(messageOverviewPrefab, messageOverviewContent.transform, false);

            GMessage lastMessage = FindLastMessage(p);
            Sprite displayPic = peopleInfo.Find(x => x.nameOfPerson == p).displayPicture;

            string lastMessageText = lastMessage.MessageContent;
            if (lastMessageText.Length > 45)
            {
                lastMessageText = lastMessageText.Substring(0, 45);
                lastMessageText += "...";
            }

            if (lastMessage.Sender == People.Amira)
            {

                messageOverview.GetComponent<MessageOverviewPrefab>().SetData(lastMessage.Reciever, lastMessageText, lastMessage.Seen ? "Seen" : "Unread", displayPic, lastMessage.timeOfMessage);
            }
            else
            {
                messageOverview.GetComponent<MessageOverviewPrefab>().SetData(lastMessage.Sender, lastMessageText, lastMessage.Seen ? "Seen" : "Unread", displayPic, lastMessage.timeOfMessage);

            }

        }
    }

    public void RefreshLastMessage()
    {

        foreach (Transform child in messageOverviewContent.transform)
        {
            MessageOverviewPrefab m = child.GetComponent<MessageOverviewPrefab>();
            m.lastMessage.text = FindLastMessage(m.person).MessageContent;
        }
    }

    GMessage FindLastMessage(People person)
    {
        GMessage lastMessage = new GMessage();
        foreach (GMessage m in messages_)
        {
            if (GameManager.instance.ShouldContentBeVisible(m.dateOfMessage, m.timeOfMessage, m.IsDeleted, m.DeletedDate, m.DeletedTime))
            {
                if (m.Sender == person || m.Reciever == person)
                {
                    lastMessage = m;
                }
            }
        }
        return lastMessage;
    }

    public void CreateCurrentMessages()
    {

        foreach (Transform child in conversationContent.transform)
        {
            Destroy(child.gameObject);
        }

        GDate lastDate = new GDate();
        People lastperson = People.None;
        foreach (GMessage m in messages_)
        {
            if (m == null || m == null)
            {
                Debug.LogError("m is null");
            }

            if (GameManager.instance.ShouldContentBeVisible(m.dateOfMessage, m.timeOfMessage, m.IsDeleted, m.DeletedDate, m.DeletedTime))
            {
                if (m.Sender == nameOfCurrentPerson || m.Reciever == nameOfCurrentPerson)
                {

                    if (m.DateOfMessage.Compare(lastDate) != Comparator.EQUAL ||
                            FindTheOtherPersonInChatWithAmira(m) != lastperson)
                    {
                        GameObject dateGO = Instantiate(datePrefab, conversationContent.transform, false);
                        dateGO.GetComponentInChildren<DateSetter>().SetDateText(m.DateOfMessage);
                        lastDate = m.dateOfMessage;
                        lastperson = FindTheOtherPersonInChatWithAmira(m);
                    }

                    InstantiateMessage(m);
                }
            }
        }
    }


    public void InstantiateMessage(GMessage m)
    {
        GameObject g = null;
        if (m.Sender == People.Amira)
        {

            if (m.typeOfMessage == TypeOfMessage.Text)
            {
                g = Instantiate(conversationPrefab_me, conversationContent.transform, false);
                g.GetComponent<Chat>().SetChatText(m);
                // m.messageObject.Seen = true;
            }
            else if (m.typeOfMessage == TypeOfMessage.Image)
            {
                g = Instantiate(chatImagePrefab, conversationContent.transform, false);
                g.GetComponent<ChatMessage>().SetChatText(m);
                // m.messageObject.Seen = true;
            }

        }
        else
        {
            g = Instantiate(conversationPrefab_other, conversationContent.transform, false);
            g.GetComponent<Chat>().SetChatText(m);
            if (!m.Seen && m.Evidence != null && m.MessageContent == "I know what is going on and I’m hating this feeling, will you tell me what is going on before I teach you a lesson?")
            {
                TutorialManager.instance.StartEvidenceTutorial1(g);
            }
            //m.messageObject.Seen = true;


        }

    }


    public People FindTheOtherPersonInChatWithAmira(GMessage m)
    {
        if (m.sender == People.Amira)
        {
            return m.reciever;
        }
        return m.sender;
    }


    public string RandomResponse()
    {
        int i = Random.Range(0, nonSuspectResponses.Length);
        return nonSuspectResponses[i];
    }


    public void StartTypeAnimationAndResponse(GEvidence e, GMessage gMessage)
    {
        StartCoroutine(TypeAnimationAndResponse(e, gMessage));
    }

    public IEnumerator TypeAnimationAndResponse(GEvidence evidence, GMessage gMessage)
    {

        MyGameFlow.instance.SwitchToPreviousScreen();
        yield return new WaitForSeconds(0.5f);
        GameObject a = Instantiate(attachmentInProgress, conversationContent.transform, false);
        Destroy(a, 1f);
        //Spawn Attachment In Progress

        yield return new WaitForSeconds(1f);
        gMessage.isCollectibleEvidence = false;
        GMessage evidenceMessage = gMessage;
        //MessageContainer evidenceMessageContainer = new MessageContainer();
        //evidenceMessageContainer.messageObject = evidenceMessage;
        messages_.Add(evidenceMessage);
        InstantiateMessage(evidenceMessage);

        QuestionHandler.instance.SetupQuestions(evidence);

    }

    public void StartQuestioning(Questions q, GEvidence evidence)
    {
        StartCoroutine(Questioning(q, evidence));
    }

    public void StartResponse(Responses r, GEvidence evidence)
    {
        StartCoroutine(ReponseOfSuspect(r, evidence));
    }

    public IEnumerator Questioning(Questions q, GEvidence evidence)
    {

        GMessage questionMessage = new GMessage(q.text, People.Amira, nameOfCurrentPerson, GameManager.instance.presentDate, GameManager.instance.presentTime, false, null, null, false, null);
        //MessageContainer questionMessageContainer = new MessageContainer();
        //questionMessageContainer.messageObject = questionMessage;
        questionMessage.isCollectibleEvidence = false;
        messages_.Add(questionMessage);
        InstantiateMessage(questionMessage);

        StartCoroutine(ReponseOfSuspect(q.response, evidence));
        yield return null;
    }

    IEnumerator ReponseOfSuspect(Responses response, GEvidence evidence)
    {
        yield return new WaitForSeconds(response.waitingTime);
        GameObject typing = Instantiate(typingMessagePrefab, conversationContent.transform, false);

        yield return new WaitForSeconds(response.typingTime);
        Destroy(typing);
        yield return new WaitForSeconds(0.5f);
        GMessage responseMessage = new GMessage();
        if (evidence != new GEvidence() && evidence.SuspectToAsk == nameOfCurrentPerson && !evidence.AreQuestionsEmpty())
        {

            responseMessage = new GMessage(response.responseText, nameOfCurrentPerson, People.Amira, GameManager.instance.presentDate, GameManager.instance.presentTime, false, new GDate(), new GTime(), false, null);

        }
        else
        {
            responseMessage = new GMessage(RandomResponse(), nameOfCurrentPerson, People.Amira, GameManager.instance.presentDate, GameManager.instance.presentTime, false, new GDate(), new GTime(), false, null);
        }
        //MessageContainer responseMessageContainer = new MessageContainer();
        //responseMessageContainer.messageObject = responseMessage;
        responseMessage.isCollectibleEvidence = false;

        messages_.Add(responseMessage);
        InstantiateMessage(responseMessage);
        SoundManager.instance.PlaySound(Sounds.MessageRecieved);
        QuestionHandler.instance.CheckForAdditionalQuestions();
    }
}
