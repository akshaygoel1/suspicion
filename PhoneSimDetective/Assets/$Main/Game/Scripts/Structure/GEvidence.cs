using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfEvidence
{
    Text,
    Image,
}

namespace MyStory
{
    [System.Serializable]
    public class GEvidence
    {

        [SerializeField] People suspectToAsk = People.None;
        //[SerializeField] string responseOfSuspect = string.Empty;
        [SerializeField] public Questions[] questions;
        public string nameOfOriginFile;
        //[SerializeField] public Questions question2;
        //public MessageContainer messageObject;
        //public GalleryContainer imageObject;
        public GEvidence()
        {

            suspectToAsk = People.None;
            //responseOfSuspect = string.Empty;

        }

        public GEvidence(People suspect)//, MessageContainer message, GalleryContainer gallery)
        {

            suspectToAsk = suspect;
            //responseOfSuspect = response;
            //messageObject = message;
            //imageObject = gallery;
        }

        public People SuspectToAsk
        {
            get { return suspectToAsk; }
        }

        //public string ResponseOfSuspect
        //{
        //    get { return responseOfSuspect; }
        //}

        public bool AreQuestionsEmpty()
        {
            bool isEmpty = false;
            for(int i = 0; i < 3; i++)
            {
                if (questions[i] == null)
                {
                    isEmpty = true;
                }
            }
            return isEmpty;
        }
    }

}