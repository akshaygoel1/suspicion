using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStory
{
    public class GHomeScreen 
    {

        [SerializeField] GDate dateOfChange = new GDate();
        [SerializeField] GTime timeOfChange = new GTime();
        [SerializeField] List<Apps> appsInstalled = new List<Apps>();
        [SerializeField] Sprite backgroundImage = null;

        public GHomeScreen()
        {
            appsInstalled = new List<Apps>();
            backgroundImage = null;
            dateOfChange = new GDate();
            timeOfChange = new GTime();
        }

        public GHomeScreen(GDate d, GTime t, List<Apps> a, Sprite b)
        {
            dateOfChange = d;
            timeOfChange = t;

            foreach(Apps app in a)
            {
                appsInstalled.Add(app);
            }
            backgroundImage = b;
        }

        public List<Apps> AppsInstalled
        {
            get { return appsInstalled; }
        }

        public Sprite BackgroundImage
        {
            get { return backgroundImage; }
        }

       public GDate DateOfChange
        {
            get { return dateOfChange; }
        }

        public GTime TimeOfChange
        {
            get { return timeOfChange; }
        }

    }


    public enum Apps
    {
         None,
         Whatsapp,
         App1,
         App2,
    }
}