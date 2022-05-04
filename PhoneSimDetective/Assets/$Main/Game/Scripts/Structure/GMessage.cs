using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyStory
{
    [System.Serializable]
    public class GMessage
    {

        [SerializeField] public string messageContent = string.Empty;
        [SerializeField] public People sender = People.None;
        [SerializeField] public People reciever = People.None;
        [SerializeField] public GDate dateOfMessage = new GDate();
        [SerializeField] public GTime timeOfMessage = new GTime();
        [SerializeField] public bool isMessageDeleted = false;
        [SerializeField] public GDate deletedDate = new GDate();
        [SerializeField] public GTime deletedTime = new GTime();
        [SerializeField] public bool seen = true;
        //[SerializeField] GEvidence evidence = null;
        [SerializeField] public EvidenceContainer evidence = null;
        public string nameOfImage;
        public bool isEvidenceAttach = false;
        public TypeOfMessage typeOfMessage = TypeOfMessage.Text;
        public string nameOfMessageContainer = "";
        public bool isCollectibleEvidence = true;
        public GMessage()
        {
            messageContent = string.Empty;
            sender = People.None;
            reciever = People.None;
            dateOfMessage = new GDate();
            timeOfMessage = new GTime();
            deletedDate = new GDate();
            deletedTime = new GTime();
            seen = true;
            evidence = null;
            isMessageDeleted = false;
        }

        public GMessage(string mC, People s, People r, GDate date, GTime time, bool isDeleted, GDate delDate, GTime delTime, bool isSeen, EvidenceContainer e, bool iEAttach=false)
        {
            messageContent = mC;
            sender = s;
            reciever = r;
            dateOfMessage = date;
            timeOfMessage = time;
            deletedDate = delDate;
            deletedTime = delTime;
            seen = isSeen;
            evidence = e;
            isMessageDeleted = isDeleted;
            isEvidenceAttach = iEAttach;
        }

        public string MessageContent
        {
            get { return messageContent; }
        }
        public People Sender
        {
            get { return sender; }
        }
        public People Reciever
        {
            get { return reciever; }
        }
        public GDate DateOfMessage
        {
            get { return dateOfMessage; }
        }
        public GDate DeletedDate
        {
            get { return deletedDate; }
        }
        public GTime TimeOfMessage
        {
            get { return timeOfMessage; }
        }
        public GTime DeletedTime
        {
            get { return deletedTime; }
        }
        public bool Seen
        {
            get { return seen; }
            set { seen = value; }
        }
        public EvidenceContainer Evidence
        {
            get { return evidence; }
        }
        public bool IsDeleted
        {
            get { return isMessageDeleted; }
        }
    }


    public enum People
    {
        None,
        Amira,
        Shweta,
        Raj,
        Karan,
        Akash
    }

    public enum EvidenceID
    {
        None,
        E1,
        E2,
        E3,

    }
    public enum TypeOfMessage
    {
        Text,
        Image,
    }
}

