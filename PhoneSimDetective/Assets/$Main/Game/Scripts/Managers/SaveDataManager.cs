using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyStory;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
public enum SaveData
{
    Tutorial1Done,
    Tutorial2Done,
}



public class SaveDataManager:MonoBehaviour
{
    public static SaveDataManager instance = null;
    public OldMessages _oldMessages= new OldMessages();
    public NewMessages _newMessages = new NewMessages();

    public PlayerData _playerData = new PlayerData();
    public ListEvidence _listEvidence = new ListEvidence();
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        if (!PlayerPrefs.HasKey(SaveData.Tutorial1Done.ToString()))
        {
            PlayerPrefs.SetInt(SaveData.Tutorial1Done.ToString(), 0);
        }
        if (!PlayerPrefs.HasKey(SaveData.Tutorial2Done.ToString()))
        {
            PlayerPrefs.SetInt(SaveData.Tutorial2Done.ToString(), 0);
        }
    }


    public void SaveOldMessages()
    {
        _oldMessages.oldMessages.Clear();
        foreach(GMessage m in GameManager.instance.messageManager.messages_)
        {
            if (m.nameOfMessageContainer != "")
            {
                _oldMessages.oldMessages.Add(new OldMessage(m.nameOfMessageContainer, m.Seen));
            }
            else
            {
                break;
            }
        }
        XmlSerializer serializer = new XmlSerializer(typeof(OldMessages));
#if UNITY_EDITOR    
        FileStream stream = new FileStream(Application.dataPath + "/SaveData/OldMessages.xml", FileMode.Create);
#else
         FileStream stream = new FileStream(Application.persistentDataPath + "/OldMessages.xml", FileMode.Create);
#endif
        serializer.Serialize(stream, _oldMessages);
        stream.Close();

    }
    public void SaveNewMessages()
    {
        _newMessages.newMessages.Clear();
        foreach (GMessage m in GameManager.instance.messageManager.messages_)
        {
            if (m.nameOfMessageContainer == "")
            {
                _newMessages.newMessages.Add(new NewMessage(m.MessageContent,m.sender,m.reciever,m.dateOfMessage,m.timeOfMessage,m.typeOfMessage,m.nameOfImage));
            }
        }


        XmlSerializer serializer = new XmlSerializer(typeof(NewMessages));
#if UNITY_EDITOR
        FileStream stream = new FileStream(Application.dataPath + "/SaveData/NewMessages.xml", FileMode.Create);
#else
         FileStream stream = new FileStream(Application.persistentDataPath + "/NewMessages.xml", FileMode.Create);
#endif
        serializer.Serialize(stream, _newMessages);
        stream.Close();

    }

    public void SaveEvidences()
    {

        XmlSerializer serializer = new XmlSerializer(typeof(ListEvidence));
#if UNITY_EDITOR
        FileStream stream = new FileStream(Application.dataPath + "/SaveData/ListEvidence.xml", FileMode.Create);
#else
         FileStream stream = new FileStream(Application.persistentDataPath + "/ListEvidence.xml", FileMode.Create);
#endif
        serializer.Serialize(stream, _listEvidence);
        stream.Close();

    }


    public void SavePlayerData()
    {
        GDate _pDate = GameManager.instance.presentDate;
        GTime _pTime = GameManager.instance.presentTime;
        GDate _cDate = GameManager.instance.currentDate;
        GTime _cTime = GameManager.instance.currentTime;
        _playerData = new PlayerData(_pTime.Hour, _pTime.Minutes, _pTime.Seconds, _pDate.Year, _pDate.Month, _pDate.Day,
            _cTime.Hour, _cTime.Minutes, _cTime.Seconds, _cDate.Year, _cDate.Month, _cDate.Day);


        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
#if UNITY_EDITOR
        FileStream stream = new FileStream(Application.dataPath + "/SaveData/PlayerData.xml", FileMode.Create);
#else
         FileStream stream = new FileStream(Application.persistentDataPath + "/PlayerData.xml", FileMode.Create);
#endif
        serializer.Serialize(stream, _playerData);
        stream.Close();
    }


    public void LoadOldMessages()
    {

            XmlSerializer serializer = new XmlSerializer(typeof(OldMessages));
#if UNITY_EDITOR
            FileStream stream = new FileStream(Application.dataPath + "/SaveData/OldMessages.xml", FileMode.Open);
#else
          FileStream stream = new FileStream(Application.persistentDataPath + "/OldMessages.xml", FileMode.Open);
#endif

            _oldMessages = serializer.Deserialize(stream) as OldMessages;
            stream.Close();
    }

    public void LoadNewMessages()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(NewMessages));
#if UNITY_EDITOR
        FileStream stream = new FileStream(Application.dataPath + "/SaveData/NewMessages.xml", FileMode.Open);
#else
          FileStream stream = new FileStream(Application.persistentDataPath + "/NewMessages.xml", FileMode.Open);
#endif
        _newMessages = serializer.Deserialize(stream) as NewMessages;
        stream.Close();
    }

    public void LoadListEvidence()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ListEvidence));
#if UNITY_EDITOR
        FileStream stream = new FileStream(Application.dataPath + "/SaveData/ListEvidence.xml", FileMode.Open);
#else
          FileStream stream = new FileStream(Application.persistentDataPath + "/ListEvidence.xml", FileMode.Open);
#endif
        _listEvidence = serializer.Deserialize(stream) as ListEvidence;
        stream.Close();
    }

    public void LoadPlayerData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
#if UNITY_EDITOR
        FileStream stream = new FileStream(Application.dataPath + "/SaveData/PlayerData.xml", FileMode.Open);
#else
          FileStream stream = new FileStream(Application.persistentDataPath + "/PlayerData.xml", FileMode.Open);
#endif
        _playerData = serializer.Deserialize(stream) as PlayerData ;
        stream.Close();
    }


}

[System.Serializable]
public class OldMessages
{
    public List<OldMessage> oldMessages = new List<OldMessage>();

    public OldMessages()
    {
        oldMessages = new List<OldMessage>();
    }
}

public class OldMessage
{
    public string nameOfMessage;
    public bool seen;

    public OldMessage()
    {
        nameOfMessage = "";
        seen = false;
    }

    public OldMessage(string n_, bool s_)
    {
        nameOfMessage = n_;
        seen = s_;
    }
}

[System.Serializable]
public class NewMessages
{
    public List<NewMessage> newMessages = new List<NewMessage>();

    public NewMessages()
    {
        newMessages = new List<NewMessage>();
    }
}
public class NewMessage
{
    [SerializeField] public string messageContent = string.Empty;
    [SerializeField] public People sender = People.None;
    [SerializeField] public People reciever = People.None;
    [SerializeField] public int hour = 0;
    [SerializeField] public int minutes = 0;
    [SerializeField] public int seconds = 0;

    [SerializeField] public int year = 2001;
    [SerializeField] public Month month = Month.JAN;
    [SerializeField] public int day = 1;

    //[SerializeField] public GTime timeOfMessage = new GTime();
    public TypeOfMessage typeOfMessage = TypeOfMessage.Text;
    public string nameOfImage;

    public NewMessage()
    {

    }

    public NewMessage(string messageContent_, People sender_, People reciever_, GDate date_, GTime time_, TypeOfMessage type_, string nameOfImage_)
    {
        messageContent = messageContent_;
        sender = sender_;
        reciever = reciever_;
        hour = time_.Hour;
        minutes = time_.Minutes;
        seconds = time_.Seconds;
        year = date_.Year;
        month = date_.Month;
        day = date_.Day;
        typeOfMessage = type_;
        nameOfImage = nameOfImage_;
    }
}

public class PlayerData
{
    [SerializeField] public int presentHour = 0;
    [SerializeField] public int presentMinutes = 0;
    [SerializeField] public int presentSeconds = 0;

    [SerializeField] public int presentYear = 2001;
    [SerializeField] public Month presentMonth = Month.JAN;
    [SerializeField] public int presentDay = 1;


    [SerializeField] public int currentHour = 0;
    [SerializeField] public int currentMinutes = 0;
    [SerializeField] public int currentSeconds = 0;

    [SerializeField] public int currentYear = 2001;
    [SerializeField] public Month currentMonth = Month.JAN;
    [SerializeField] public int currentDay = 1;


    public PlayerData()
    {

    }

    public PlayerData(int pHour, int pMinutes, int pSeconds, int pYear, Month pMonth, int pDay, int cHour, int cMinutes, int cSeconds, int cYear,  Month cMonth, int cDay)
    {
      presentHour = pHour;
     presentMinutes = pMinutes;
   presentSeconds = pSeconds;

   presentYear = pYear;
    presentMonth = pMonth;
     presentDay = pDay;


    currentHour = cHour;
    currentMinutes = cMinutes;
    currentSeconds = cSeconds;

   currentYear = cYear;
    currentMonth = cMonth;
     currentDay = cDay;
}


}

public class Evidence
{
    public string nameOfEvidenceFile;
    public TypeOfEvidence typeOfEvidence = TypeOfEvidence.Text;
    
    public Evidence()
    {

    }

    public Evidence(string n, TypeOfEvidence t)
    {
        nameOfEvidenceFile = n;
        typeOfEvidence = t;
    }
}


public class ListEvidence
{
    public List<Evidence> evidences = new List<Evidence>();


    public ListEvidence()
    {

    }
}