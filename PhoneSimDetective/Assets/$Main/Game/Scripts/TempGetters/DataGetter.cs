using MyStory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGetter : MonoBehaviour {

    [SerializeField]
    List<GMessage> allMessages = new List<GMessage>();
    [SerializeField]
    List<GGallery> allImages = new List<GGallery>();

    List<GChats> allChats = new List<GChats>();
    public static DataGetter instance;
	private void Awake () {

        if (instance == null)
            instance = this;
        else
            Destroy(this);
        foreach (People p in Enum.GetValues(typeof(People)))
        {
            List<GMessage> messages = allMessages.FindAll((GMessage m) => {
                if (m.Reciever == p || m.Sender == p)
                {
                    return true;
                }
                return false;
            });
            if (messages.Count > 0)
            {
                GChats chat = new GChats();
                chat.person = p;
                chat.Messages = messages;
                allChats.Add(chat);
            }
        }
    }

    public List<GChats> GetChats(GTime time, GDate date)
    {
        List<GChats> chats = new List<GChats>();
        foreach(People p in Enum.GetValues(typeof(People)))
        {
            List<GMessage> messages = allMessages.FindAll((GMessage m) => {
                if(time.Compare(m.TimeOfMessage) != Comparator.GREATER)
                {
                    return false;
                }
                if (date.Compare(m.DateOfMessage) != Comparator.GREATER)
                {
                    return false;
                }
                if(m.Reciever == p || m.Sender == p)
                {
                    return true;
                }
                return false;
            });
            if(messages.Count > 0)
            {
                GChats chat = new GChats();
                chat.person = p;
                chat.Messages = messages;
                chats.Add(chat);
            }
        }
        return chats;
    }

    public List<GMessage> GetMessages(GTime time, GDate date)
    {
        Comparator compare = Comparator.LESSER | Comparator.EQUAL;
        return allMessages.FindAll((GMessage message) =>
        message.TimeOfMessage.Compare(time) == compare && message.DateOfMessage.Compare(date) == compare);
    }
    public List<GGallery> GetImages(GTime time, GDate date)
    {
        Comparator compare = Comparator.LESSER | Comparator.EQUAL;
        return allImages.FindAll((GGallery image) =>
        image.TimeOfImage.Compare(time) == compare && image.DateOfImage.Compare(date) == compare);
    }
}

public class GChats
{
    public People person;
    public List<GMessage> Messages = new List<GMessage>();
}
