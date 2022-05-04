using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStory
{
    [System.Serializable]
    public class GGallery
    {

        [SerializeField] Sprite image = null;
        [SerializeField] GDate dateOfImage = new GDate();
        [SerializeField] GTime timeOfImage = new GTime();
        [SerializeField] GDate deletedDate = new GDate();
        [SerializeField] GTime deletedTime = new GTime();
        [SerializeField] string info = string.Empty;
        [SerializeField] EvidenceContainer evidence = null;
        [SerializeField] bool isDeleted = false;
        [SerializeField] public string nameOfImage = "";
        public GGallery()
        {
             image = null;
             dateOfImage = new GDate();
             timeOfImage = new GTime();
             deletedDate = new GDate();
             deletedTime = new GTime();
             info = string.Empty;
            evidence = null;
            isDeleted = false;
        }

        public GGallery(Sprite i, GDate date, GTime time, bool deleted, GDate delDate, GTime delTime, string inf, EvidenceContainer e)
        {
            image = i;
            dateOfImage = date;
            timeOfImage = time;
            deletedDate = delDate;
            deletedTime = delTime;
            info = inf;
            evidence = e;
            isDeleted = deleted;
        }

        public Sprite Image
        {
            get { return image; }
        }

        public GDate DateOfImage
        {
            get { return dateOfImage; }
        }
        public GDate DeletedDate
        {
            get { return deletedDate; }
        }
        public GTime TimeOfImage
        {
            get { return timeOfImage; }
        }
        public GTime DeletedTime
        {
            get { return deletedTime; }
        }
        public string Info
        {
            get { return info; }
        }
        public EvidenceContainer Evidence
        {
            get { return evidence; }
        }
        public bool IsDeleted
        {
            get { return isDeleted; }
        }
    }

}