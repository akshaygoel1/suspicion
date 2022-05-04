using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStory
{
    [System.Serializable]
    public class GLockScreen
    {
        [SerializeField]string password = string.Empty;
        [SerializeField] GDate dateOfPasswordSet = new GDate();
        [SerializeField] GTime timeOfPasswordSet = new GTime();
        [SerializeField] string info = string.Empty;

        public GLockScreen()
        {
            password = string.Empty;
            dateOfPasswordSet = new GDate();
            timeOfPasswordSet = new GTime();
            info = string.Empty;
        }
        public GLockScreen(string p, GDate date, GTime time, string i)
        {
            password = p;
            dateOfPasswordSet = date;
            timeOfPasswordSet = time;
            info = i;
        }

        public string Password
        {
            get { return password; }
        }

        public string Info
        {
            get { return info; }
        }

        public GDate DateOfPasswordSet
        {
            get { return dateOfPasswordSet; }
        }
        public GTime TimeOfPasswordSet
        {
            get { return timeOfPasswordSet; }
        }
    }
}
