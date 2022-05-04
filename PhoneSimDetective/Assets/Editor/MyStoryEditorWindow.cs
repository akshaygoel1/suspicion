using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MyStory;
public class MyStoryEditorWindow : EditorWindow {

    int tab = 0;
    string lockScreenPassword = "";
    string lockScreenHint = "";
    int year = 2001;
    Month month = Month.JAN;
    int day = 1;
    int hour = 0;
    int minutes = 0;
    int seconds = 0;
    string messageScreenContent="";
    People messageScreenSender = People.None;
    People messageScreenReciever = People.None;
    bool isContentDeleted = false;
    int deletedYear = 2001;
    Month deletedMonth = Month.JAN;
    int deletedDay = 1;
    int deletedHour = 0;
    int deletedMinutes = 0;
    int deletedSeconds = 0;
    bool seen = true;
    EvidenceContainer messageScreenEvidence = null;
    Sprite galleryImage = null;
    string galleryInfo = "";
    EvidenceContainer galleryScreenEvidence = null;

    People evidenceScreenSuspectToAsk = People.None;
    string evidenceScreenResponseOfSuspect = string.Empty;
    Sprite homeScreenBackgroundImage = null;
    int homeScreenAppCount = 0;
    List<Apps> homeScreenApps = new List<Apps>();
    [MenuItem("MyStory/ContentEditor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MyStoryEditorWindow));
        
    }
	
    public MyStoryEditorWindow()
    {
        this.titleContent = new GUIContent("MyStory");
    }

    void OnGUI()
    {
        tab = GUILayout.Toolbar(tab, new string[] { "LockScreen", "Messages", "Gallery", "HomeScreen", "Evidence"});
        ShowDefaultButtons();
        if (tab < 4)
        {
            #region Date
            GUILayout.BeginVertical();
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.Label("Date", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            EditorGUI.indentLevel++;

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            day = EditorGUILayout.IntField("Day : ", day, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            month = (Month)EditorGUILayout.EnumPopup("Month : ", month, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            year = EditorGUILayout.IntField("Year : ", year, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            EditorGUI.indentLevel--;

            GUILayout.Space(20);
            GUILayout.EndVertical();
            #endregion

            #region Time
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.Label("Time", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            EditorGUI.indentLevel++;

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            hour = EditorGUILayout.IntField("Hour : ", hour, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            minutes = EditorGUILayout.IntField("Minutes : ", minutes, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            seconds = EditorGUILayout.IntField("Seconds : ", seconds, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            EditorGUI.indentLevel--;

            GUILayout.Space(30);
            GUILayout.EndVertical();
            #endregion
        }


        #region LockScreen
        if (tab == 0)
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Lock Screen", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            lockScreenPassword = EditorGUILayout.TextField("Password : ", lockScreenPassword, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Hint :");
            //messageScreenContent = EditorGUILayout.TextArea("Message Content : ", messageScreenContent, GUILayout.Width(300),GUILayout.Height(10));
            lockScreenHint = GUILayout.TextArea(lockScreenHint, 200, GUILayout.Width(250), GUILayout.Height(50));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();



            GUILayout.EndVertical();
        }
        #endregion

        #region MessagesScreen
        if(tab == 1)
        {
            /*string messageScreenContent = "";
            People messageScreenSender = People.None;
            People messageScreenReciever = People.None;
            bool messageScreenIsMessageDeleted = false;
            int messageScreenDeletedYear = 2001;
            Month messageScreenDeletedMonth = Month.JAN;
            int messageScreenDeletedDay = 1;
            int messageScreenDeletedHour = 0;
            int messageScreenDeletedMinutes = 0;
            int messageScreenDeletedSeconds = 0;
            bool seen = true;
            EvidenceID messageScreenEvidenceID = EvidenceID.None;*/

            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Message Content :");
            //messageScreenContent = EditorGUILayout.TextArea("Message Content : ", messageScreenContent, GUILayout.Width(300),GUILayout.Height(10));
            messageScreenContent = GUILayout.TextArea(messageScreenContent, 200, GUILayout.Width(250),GUILayout.Height(50));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            messageScreenSender = (People)EditorGUILayout.EnumPopup("Sender : ", messageScreenSender, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            messageScreenReciever = (People)EditorGUILayout.EnumPopup("Reciever : ", messageScreenReciever, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            seen = EditorGUILayout.Toggle("Is Message Seen?", seen, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            messageScreenEvidence = (EvidenceContainer)EditorGUILayout.ObjectField("Evidence : ", messageScreenEvidence, typeof(Object), false, GUILayout.Width(300)) as EvidenceContainer;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            isContentDeleted = EditorGUILayout.Toggle("Is the Message Deleted?", isContentDeleted, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            if (isContentDeleted)
            {
                #region Deleted Date
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                GUILayout.Label("Deleted Date", EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                EditorGUI.indentLevel++;

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                deletedDay = EditorGUILayout.IntField("Day : ", deletedDay, GUILayout.Width(300));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                deletedMonth = (Month)EditorGUILayout.EnumPopup("Month : ", deletedMonth, GUILayout.Width(300));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                deletedYear = EditorGUILayout.IntField("Year : ", deletedYear, GUILayout.Width(300));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                EditorGUI.indentLevel--;
                #endregion

                #region Deleted Time

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                GUILayout.Label("Time", EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                EditorGUI.indentLevel++;

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                deletedHour = EditorGUILayout.IntField("Hour : ", deletedHour, GUILayout.Width(300));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                deletedMinutes = EditorGUILayout.IntField("Minutes : ", deletedMinutes, GUILayout.Width(300));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                deletedSeconds = EditorGUILayout.IntField("Seconds : ", deletedSeconds, GUILayout.Width(300));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                EditorGUI.indentLevel--;
                #endregion

            }
            GUILayout.EndVertical();
        }
        #endregion
        if (tab == 2)
        {
            #region Gallery
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            galleryImage =  EditorGUILayout.ObjectField("Gallery Photo :", galleryImage, typeof(Sprite),false, GUILayout.Width(300)) as Sprite;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Info :");
            //messageScreenContent = EditorGUILayout.TextArea("Message Content : ", messageScreenContent, GUILayout.Width(300),GUILayout.Height(10));
            galleryInfo = GUILayout.TextArea(galleryInfo, 200, GUILayout.Width(250), GUILayout.Height(50));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            galleryScreenEvidence = (EvidenceContainer)EditorGUILayout.ObjectField("Evidence : ", galleryScreenEvidence, typeof(Object), false, GUILayout.Width(300)) as EvidenceContainer;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            isContentDeleted = EditorGUILayout.Toggle("Is the Content Deleted?", isContentDeleted, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            if (isContentDeleted)
            {
                #region Deleted Date
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                GUILayout.Label("Deleted Date", EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                EditorGUI.indentLevel++;

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                deletedDay = EditorGUILayout.IntField("Day : ", deletedDay, GUILayout.Width(300));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                deletedMonth = (Month)EditorGUILayout.EnumPopup("Month : ", deletedMonth, GUILayout.Width(300));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                deletedYear = EditorGUILayout.IntField("Year : ", deletedYear, GUILayout.Width(300));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                EditorGUI.indentLevel--;
                #endregion

                #region Deleted Time

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                GUILayout.Label("Time", EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                EditorGUI.indentLevel++;

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                deletedHour = EditorGUILayout.IntField("Hour : ", deletedHour, GUILayout.Width(300));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                deletedMinutes = EditorGUILayout.IntField("Minutes : ", deletedMinutes, GUILayout.Width(300));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                deletedSeconds = EditorGUILayout.IntField("Seconds : ", deletedSeconds, GUILayout.Width(300));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                EditorGUI.indentLevel--;
                #endregion

            }
            GUILayout.EndVertical();
            #endregion
        }

        if (tab == 3)
        {
            #region HomeScreen

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            homeScreenBackgroundImage = EditorGUILayout.ObjectField("Background Image :", homeScreenBackgroundImage, typeof(Sprite), false, GUILayout.Width(300)) as Sprite;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            homeScreenAppCount = EditorGUILayout.DelayedIntField("Number of Apps: ", homeScreenAppCount, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            while (homeScreenAppCount != homeScreenApps.Count)
            {
                if (homeScreenAppCount > homeScreenApps.Count)
                {
                    homeScreenApps.Add(Apps.None);
                }
                else
                {
                    homeScreenApps.RemoveAt(homeScreenApps.Count - 1);
                }
            }

            for(int i = 0; i < homeScreenApps.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                homeScreenApps[i] = (Apps)EditorGUILayout.EnumPopup("App :", homeScreenApps[i], GUILayout.Width(300));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();

            #endregion
        }


        if (tab == 4)
        {
            #region Evidence

            GUILayout.BeginVertical();
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            evidenceScreenSuspectToAsk = (People)EditorGUILayout.EnumPopup("Suspect to Ask : ", evidenceScreenSuspectToAsk, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Response of Suspect :");
            evidenceScreenResponseOfSuspect = GUILayout.TextArea(evidenceScreenResponseOfSuspect, 200, GUILayout.Width(250), GUILayout.Height(50));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Message Content :");
            //messageScreenContent = EditorGUILayout.TextArea("Message Content : ", messageScreenContent, GUILayout.Width(300),GUILayout.Height(10));
            messageScreenContent = GUILayout.TextArea(messageScreenContent, 200, GUILayout.Width(250), GUILayout.Height(50));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            galleryImage = EditorGUILayout.ObjectField("Gallery Photo :", galleryImage, typeof(Sprite), false, GUILayout.Width(300)) as Sprite;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            /* EvidenceID evidenceScreenEvidenceID = EvidenceID.None;
             People evidenceScreenSuspectToAsk = People.None;
             string evidenceScreenResponseOfSuspect = string.Empty;*/


            GUILayout.EndVertical();

            #endregion
        }
    }


    void ShowDefaultButtons()
    {
        GUILayout.BeginVertical();

        GUILayout.Space(30);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("New", GUILayout.Width(100))){
            New(tab);
        }
        if (GUILayout.Button("Save", GUILayout.Width(100))){
            Save(tab);
        }
        if (GUILayout.Button("Load", GUILayout.Width(100))){
            Load(tab);
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    #region MainFunctions
    void New(int tab)
    {
        NewDateAndTime();
        NewDeletedDateAndTime();
        if (tab == 0)
        {
            NewLockScreen();
        }
        else if(tab == 1)
        {
            NewMessage();
        }
        else if (tab == 2)
        {
            NewGalleryImage();
        }
        else if (tab == 3)
        {
            NewHomeScreen();
        }
        else if (tab == 4)
        {
            NewEvidence();
        }
    }

    void Save(int tab)
    {
        if (tab == 0)
        {
            SaveLockScreen();
        }
        else if (tab == 1)
        {
            SaveMessage();
        }
        else if (tab == 2)
        {
            SaveGalleryImage();
        }
        else if (tab == 3)
        {
            SaveHomeScreen();
        }
        else if (tab == 4)
        {
            SaveEvidence();
        }
    }

    void Load(int tab)
    {
        if (tab == 0)
        {
            LoadLockScreen();
        }
        else if (tab == 1)
        {
            
        }
        else if (tab == 2)
        {

        }
        else if (tab == 3)
        {

        }
        else if (tab == 4)
        {

        }
    }
    #endregion


    #region NewHelperFunctions
    void NewLockScreen()
    {
         lockScreenPassword = "";
         lockScreenHint = "";

    }

    void NewHomeScreen()
    {
        homeScreenBackgroundImage = null;
        homeScreenAppCount = 0;
        homeScreenApps = new List<Apps>();
    }

    void NewMessage()
    {
         messageScreenContent = "";
         messageScreenSender = People.None;
         messageScreenReciever = People.None;
         isContentDeleted = false;
         seen = true;
         messageScreenEvidence = null;
    }

    void NewGalleryImage()
    {
         galleryImage = null;
         galleryInfo = "";
         galleryScreenEvidence = null;
    }

    void NewEvidence()
    {
         evidenceScreenSuspectToAsk = People.None;
         evidenceScreenResponseOfSuspect = string.Empty;
    }

    void NewDateAndTime()
    {
         year = 2001;
         month = Month.JAN;
         day = 1;
         hour = 0;
         minutes = 0;
         seconds = 0;
    }

    void NewDeletedDateAndTime()
    {
        deletedYear = 2001;
        deletedMonth = Month.JAN;
        deletedDay = 1;
        deletedHour = 0;
        deletedMinutes = 0;
        deletedSeconds = 0;
    }
    #endregion

    #region SaveHelperFunctions

    void SaveLockScreen()
    {
        string path = "";
        GLockScreen lockScreen = new GLockScreen(lockScreenPassword, new GDate(year,month,day), new GTime(hour,minutes,seconds), lockScreenHint);

        LockScreenContainer lSC = ScriptableObject.CreateInstance<LockScreenContainer>();
        lSC.lockScreenObject = lockScreen;

        path += EditorUtility.SaveFilePanelInProject("Save Lock Screen Object ", "New LockScreen","asset","message?");
         AssetDatabase.CreateAsset(lSC, path);
         AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }

    void SaveMessage()
    {
        string path = "";
        GMessage msg = new GMessage(messageScreenContent, messageScreenSender, messageScreenReciever, new GDate(year, month, day), new GTime(hour, minutes, seconds), isContentDeleted, new GDate(deletedYear, deletedMonth, deletedDay), new GTime(deletedHour, deletedMinutes, deletedSeconds), seen,messageScreenEvidence);

        MessageContainer mC = ScriptableObject.CreateInstance<MessageContainer>();
        mC.messageObject = msg;

        path += EditorUtility.SaveFilePanelInProject("Save Message Object ", "New Message", "asset", "message?");
        AssetDatabase.CreateAsset(mC, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    void SaveGalleryImage()
    {
        string path = "";
        GGallery gllry = new GGallery(galleryImage, new GDate(year, month, day), new GTime(hour, minutes, seconds), isContentDeleted, new GDate(deletedYear, deletedMonth, deletedDay), new GTime(deletedHour, deletedMinutes, deletedSeconds), galleryInfo, galleryScreenEvidence);

        GalleryContainer gC = ScriptableObject.CreateInstance<GalleryContainer>();
        gC.galleryObject = gllry;

        path += EditorUtility.SaveFilePanelInProject("Save Gallery Object ", "New Gallery", "asset", "message?");
        AssetDatabase.CreateAsset(gC, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    void SaveHomeScreen()
    {
        string path = "";
        GHomeScreen homeScreen = new GHomeScreen(new GDate(year, month, day), new GTime(hour, minutes, seconds), homeScreenApps, homeScreenBackgroundImage);

        HomeScreenContainer hSC = ScriptableObject.CreateInstance<HomeScreenContainer>();
        hSC.homeScreenObject = homeScreen;

        path += EditorUtility.SaveFilePanelInProject("Save Home Screen Object ", "New HomeScreen", "asset", "message?");
        AssetDatabase.CreateAsset(hSC, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    void SaveEvidence()
    {
        string path = "";
        GEvidence evidence = new GEvidence(evidenceScreenSuspectToAsk);// , null, null);

        EvidenceContainer eC = ScriptableObject.CreateInstance<EvidenceContainer>();
        eC.evidenceObject = evidence;

        path += EditorUtility.SaveFilePanelInProject("Save Evidence Object ", "New Evidence", "asset", "message?");
        AssetDatabase.CreateAsset(eC, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    #endregion

    #region LoadHelperFunctions

    void LoadLockScreen()
    {
        string path = "";
        path += Application.dataPath + "/Main/";
        path += EditorUtility.OpenFilePanel("Load Lock Screen", path, "asset");

        LockScreenContainer lSC = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(LockScreenContainer)) as LockScreenContainer;

        day = lSC.lockScreenObject.DateOfPasswordSet.Day;
        month = lSC.lockScreenObject.DateOfPasswordSet.Month;
        year = lSC.lockScreenObject.DateOfPasswordSet.Year;

        hour = lSC.lockScreenObject.TimeOfPasswordSet.Hour;
        minutes = lSC.lockScreenObject.TimeOfPasswordSet.Minutes;
        seconds = lSC.lockScreenObject.TimeOfPasswordSet.Seconds;

        lockScreenPassword = lSC.lockScreenObject.Password;

        lockScreenHint = lSC.lockScreenObject.Info;
    }

    void LoadHomeScreen()
    {
        string path = "";
        path += Application.dataPath + "/Main/";
        path += EditorUtility.OpenFilePanel("Load Home Screen", path, "asset");

        HomeScreenContainer hSC = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(HomeScreenContainer)) as HomeScreenContainer;

        day = hSC.homeScreenObject.DateOfChange.Day;
        month = hSC.homeScreenObject.DateOfChange.Month;
        year = hSC.homeScreenObject.DateOfChange.Year;

        hour = hSC.homeScreenObject.TimeOfChange.Hour;
        minutes = hSC.homeScreenObject.TimeOfChange.Minutes;
        seconds = hSC.homeScreenObject.TimeOfChange.Seconds;

        homeScreenApps = hSC.homeScreenObject.AppsInstalled;

        homeScreenBackgroundImage = hSC.homeScreenObject.BackgroundImage;
    }

    void LoadGallery()
    {
        string path = "";
        path += Application.dataPath + "/Main/";
        path += EditorUtility.OpenFilePanel("Load Gallery", path, "asset");

        GalleryContainer gC = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(GalleryContainer)) as GalleryContainer;

        day = gC.galleryObject.DateOfImage.Day;
        month = gC.galleryObject.DateOfImage.Month;
        year = gC.galleryObject.DateOfImage.Year;

        hour = gC.galleryObject.TimeOfImage.Hour;
        minutes = gC.galleryObject.TimeOfImage.Minutes;
        seconds = gC.galleryObject.TimeOfImage.Seconds;

        isContentDeleted = gC.galleryObject.IsDeleted;

        deletedDay = gC.galleryObject.DeletedDate.Day;
        deletedMonth = gC.galleryObject.DeletedDate.Month;
        deletedYear = gC.galleryObject.DeletedDate.Year;

        deletedHour = gC.galleryObject.DeletedTime.Hour;
        deletedMinutes = gC.galleryObject.DeletedTime.Minutes;
        deletedSeconds = gC.galleryObject.DeletedTime.Seconds;

        galleryImage = gC.galleryObject.Image;

        galleryInfo = gC.galleryObject.Info;

        galleryScreenEvidence = gC.galleryObject.Evidence;

    }

    void LoadMessage()
    {
        string path = "";
        path += Application.dataPath + "/Main/";
        path += EditorUtility.OpenFilePanel("Load Message", path, "asset");

        MessageContainer mC = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(MessageContainer)) as MessageContainer;

        day = mC.messageObject.DateOfMessage.Day;
        month = mC.messageObject.DateOfMessage.Month;
        year = mC.messageObject.DateOfMessage.Year;

        hour = mC.messageObject.TimeOfMessage.Hour;
        minutes = mC.messageObject.TimeOfMessage.Minutes;
        seconds = mC.messageObject.TimeOfMessage.Seconds;

        isContentDeleted = mC.messageObject.IsDeleted;

        deletedDay = mC.messageObject.DeletedDate.Day;
        deletedMonth = mC.messageObject.DeletedDate.Month;
        deletedYear = mC.messageObject.DeletedDate.Year;

        deletedHour = mC.messageObject.DeletedTime.Hour;
        deletedMinutes = mC.messageObject.DeletedTime.Minutes;
        deletedSeconds = mC.messageObject.DeletedTime.Seconds;

        messageScreenContent = mC.messageObject.MessageContent;

        messageScreenSender = mC.messageObject.Sender;
        messageScreenReciever = mC.messageObject.Reciever;

        seen = mC.messageObject.Seen;

        messageScreenEvidence = mC.messageObject.Evidence; 
    }

    void LoadEvidence()
    {
       
    }

    #endregion
}


