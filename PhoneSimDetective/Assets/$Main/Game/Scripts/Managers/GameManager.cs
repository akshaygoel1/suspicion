using MyStory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.UI.Extensions;
using System.IO;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public MessageManager messageManager;
    public GalleryManager galleryManager;
    public LockManager lockManager;
    public GTime currentTime;
    public GDate currentDate;

    public GTime presentTime;
    public GDate presentDate;

   
    public List<GEvidence> evidenceCollected = new List<GEvidence>();
    
    bool isInPresent;

    public GameObject messageEvidencePrefab;
    public Transform evidenceScreenContent;
   
    public GameObject galleryEvidencePrefab;
    public GameObject notificationGO;

    public GameObject evidenceDateScrollContent;
    public GameObject evidenceDatePrefab;
    public bool IsInPresent
    {
        get { return isInPresent; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //messageManager = GetComponent<MessageManager>();
        }
        else
            Destroy(this);

#if UNITY_EDITOR
        if (!File.Exists(Application.dataPath + "/SaveData/PlayerData.xml"))
#else
             if(!File.Exists(Application.persistentDataPath + "/PlayerData.xml"))
#endif
        {
            currentDate = new GDate(2020, Month.JUN, 3);
            currentTime = new GTime(11, 53, 55);
            presentDate = currentDate;
            presentTime = currentTime;
            SaveDataManager.instance.SavePlayerData();
        }
        else
        {
            SaveDataManager.instance.LoadPlayerData();
            PlayerData pData = SaveDataManager.instance._playerData;
            currentDate = new GDate(pData.currentYear, pData.currentMonth, pData.currentDay);
            presentDate = new GDate(pData.presentYear, pData.presentMonth, pData.presentDay);

            currentTime = new GTime(pData.currentHour, pData.currentMinutes, pData.currentSeconds);
            presentTime = new GTime(pData.presentHour, pData.presentMinutes, pData.presentSeconds);


        }




    }



    /*  private void Update()
      {
          if (Input.GetKeyDown(KeyCode.Space))
          {
              SaveDataManager.instance.SaveOldMessages();
              SaveDataManager.instance.SaveNewMessages();
          }
      }*/

    IEnumerator ShowNotificaiton()
    {
        yield return new WaitForSeconds(3);
        notificationGO.SetActive(true);
    }

    void Start()
    {
        Initialize();
        StartCoroutine(TimeMove());
        if (PlayerPrefs.GetInt(SaveData.Tutorial1Done.ToString()) == 0)
        {
            StartCoroutine(ShowNotificaiton());
        }
        UIManager.instance.OnDateSet(currentTime, currentDate);

#if UNITY_EDITOR
        if (File.Exists(Application.dataPath + "/SaveData/ListEvidence.xml"))
#else
             if(File.Exists(Application.persistentDataPath + "/ListEvidence.xml"))
#endif
        {
            SaveDataManager.instance.LoadListEvidence();

            foreach (Evidence e in SaveDataManager.instance._listEvidence.evidences)
            {
                if (e.typeOfEvidence == TypeOfEvidence.Text)
                {
                    GMessage m = messageManager.messages.Find(x => x.name == e.nameOfEvidenceFile).messageObject;

                    EvidenceContainer evidenceContainer = new EvidenceContainer();
                    evidenceContainer.evidenceObject = new MyStory.GEvidence();

                    if (m.Evidence != null)
                    {
                        evidenceContainer.evidenceObject = m.Evidence.evidenceObject;
                    }

                    GameManager.instance.AddEvidence(evidenceContainer.evidenceObject, m, false);
                }
                else if (e.typeOfEvidence == TypeOfEvidence.Image)
                {
                    GGallery g = galleryManager.galleryImages.Find(x => x.name == e.nameOfEvidenceFile).galleryObject;

                    EvidenceContainer evidenceContainer = new EvidenceContainer();
                    evidenceContainer.evidenceObject = new MyStory.GEvidence();

                    if (g.Evidence != null)
                    {
                        evidenceContainer.evidenceObject = g.Evidence.evidenceObject;

                    }

                    GameManager.instance.AddEvidence(evidenceContainer.evidenceObject, g, false);
                }
            }

        }

    }

    public void Initialize()
    {
        messageManager.CreateCurrentMessageOverview();
        galleryManager.CreateGallery();
        CheckIfInPresent();
        lockManager.GetCurrentLockData();
    }

    IEnumerator TimeMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (presentTime.AddSecond())
            {
                presentDate.AddDay();
            }

            if (isInPresent)
            {
                currentDate = presentDate;
                currentTime = presentTime;

                UIManager.instance.OnDateSet(currentTime, currentDate);
            }
        }
    }

    public void CheckIfInPresent()
    {
        if (currentDate.Compare(presentDate) == Comparator.EQUAL && currentTime.Compare(presentTime) == Comparator.EQUAL)
        {
            isInPresent = true;
        }
        else
        {
            isInPresent = false;
        }
    }

    public void AddEvidence(GEvidence evidence, GMessage message,bool isNewEvidence = true)
    {
        
        foreach(Evidence e in SaveDataManager.instance._listEvidence.evidences)
        {
           if(e.nameOfEvidenceFile == message.nameOfMessageContainer && isNewEvidence)
            {
                return;
            }
        }
        evidence.nameOfOriginFile = message.nameOfMessageContainer;
        evidenceCollected.Add(evidence);
          
        if(isNewEvidence)
        SaveDataManager.instance._listEvidence.evidences.Add(new Evidence(message.nameOfMessageContainer, TypeOfEvidence.Text));
        RefreshDatesOnEvidenceDatabase();
    }

    public void AddEvidence(GEvidence evidence, GGallery galleryImage, bool isNewEvidence = true)
    {
        foreach (Evidence e in SaveDataManager.instance._listEvidence.evidences)
        {
            if (e.nameOfEvidenceFile == galleryImage.nameOfImage && isNewEvidence)
            {
                return;
            }
        }
        evidence.nameOfOriginFile = galleryImage.nameOfImage;
        evidenceCollected.Add(evidence);
            
       
        if(isNewEvidence)
        SaveDataManager.instance._listEvidence.evidences.Add(new Evidence(galleryImage.nameOfImage, TypeOfEvidence.Image));
        RefreshDatesOnEvidenceDatabase();
    }


    void RefreshDatesOnEvidenceDatabase()
    {
        foreach(Transform c in evidenceDateScrollContent.transform)
        {
            Destroy(c.gameObject);
        }

        List<GDate> uniqueDates = new List<GDate>();
        foreach (Evidence e in SaveDataManager.instance._listEvidence.evidences)
        {
            if (e.typeOfEvidence == TypeOfEvidence.Text)
            {
                GMessage message = GameManager.instance.messageManager.messages.Find(x => x.name == e.nameOfEvidenceFile).messageObject;
                GDate d = message.dateOfMessage;
                if (!uniqueDates.Exists(x => x.Compare(d) == Comparator.EQUAL))
                {
                    uniqueDates.Add(d);
                }
            }
            else if (e.typeOfEvidence == TypeOfEvidence.Image)
            {
                GGallery galleryImage = GameManager.instance.galleryManager.galleryImages.Find(x => x.name == e.nameOfEvidenceFile).galleryObject;
                GDate d = galleryImage.DateOfImage;
                if (!uniqueDates.Exists(x => x.Compare(d) == Comparator.EQUAL))
                {
                    uniqueDates.Add(d);
                }

            }

        }
        uniqueDates = SortDates(uniqueDates);

        foreach(GDate d in uniqueDates)
        {
            GameObject eD = Instantiate(evidenceDatePrefab, evidenceDateScrollContent.transform, false);
            eD.GetComponent<EvidenceDate>().Init(d);
        }
    }


    public List<GDate> SortDates(List<GDate> listOfDates)
    {
        int n = listOfDates.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (listOfDates[j].Year > listOfDates[j + 1].Year)
                {
                    GDate temp = listOfDates[j];
                    listOfDates[j] = listOfDates[j + 1];
                    listOfDates[j + 1] = temp;
                    continue;
                }
                else if (listOfDates[j].Year == listOfDates[j + 1].Year)
                {
                    if (listOfDates[j].Month > listOfDates[j + 1].Month)
                    {
                        GDate temp = listOfDates[j];
                        listOfDates[j] = listOfDates[j + 1];
                        listOfDates[j + 1] = temp;
                        continue;
                    }
                    else if (listOfDates[j].Month == listOfDates[j + 1].Month)
                    {
                        if (listOfDates[j].Day > listOfDates[j + 1].Day)
                        {
                            GDate temp = listOfDates[j];
                            listOfDates[j] = listOfDates[j + 1];
                            listOfDates[j + 1] = temp;
                            continue;
                        }

                    }
                }
            }
        }
        return listOfDates;
    }
    /* void AddDateToEvidenceDatabase(GDate date)
     {
         foreach(Evidence e in SaveDataManager.instance._listEvidence.evidences)
         {
             if (e.typeOfEvidence == TypeOfEvidence.Text)
             {
                 GMessage message = GameManager.instance.messageManager.messages.Find(x => x.name == e.nameOfEvidenceFile).messageObject;
                 GDate d = message.dateOfMessage;

                 if (d == date)
                 {
                     return;
                 }
             }
             else if (e.typeOfEvidence == TypeOfEvidence.Image)
             {
                 GGallery galleryImage = GameManager.instance.galleryManager.galleryImages.Find(x => x.name == e.nameOfEvidenceFile).galleryObject;
                 GDate d = galleryImage.DateOfImage;

                 if (d == date)
                 {
                     return;
                 }
             }
             //Instantiate a new date object in the evidence database
         }
     }*/

    public void RemoveEvidence(GEvidence evidence, TypeOfMessage type)
    {
        foreach(Transform t in evidenceScreenContent)
        {
            if(type == TypeOfMessage.Text)
            {
                if (t.GetComponent<MessageEvidence>()!=null) {
                    if(t.GetComponent<MessageEvidence>().Evidence == evidence)
                    {
                        string messageName = evidence.nameOfOriginFile;
                        SaveDataManager.instance._listEvidence.evidences.Remove(SaveDataManager.instance._listEvidence.evidences.Find(x => x.nameOfEvidenceFile == messageName));
                        Destroy(t.gameObject);
                        RefreshDatesOnEvidenceDatabase();
                        return;
                    }
                }
            }
            else if(type == TypeOfMessage.Image)
            {
                if (t.GetComponent<GalleryEvidence>() != null)
                {
                    if (t.GetComponent<GalleryEvidence>().Evidence == evidence)
                   {
                        string galleryObName = evidence.nameOfOriginFile;
                        SaveDataManager.instance._listEvidence.evidences.Remove(SaveDataManager.instance._listEvidence.evidences.Find(x => x.nameOfEvidenceFile == galleryObName));
                        Destroy(t.gameObject);
                        RefreshDatesOnEvidenceDatabase();
                        return;
                    }
                }
            }
        }
    }

    public bool ShouldContentBeVisible(GDate date_, GTime time_,bool isDeleted_, GDate delDate_, GTime delTime_)
    {
        if (currentDate.Compare(date_) == Comparator.GREATER ||
                  (currentDate.Compare(date_) == Comparator.EQUAL && currentTime.Compare(time_) == Comparator.GREATER) ||
                  (currentDate.Compare(date_) == Comparator.EQUAL && currentTime.Compare(time_) == Comparator.EQUAL))
            if (!isDeleted_ || currentDate.Compare(delDate_) == Comparator.LESSER ||
                (currentDate.Compare(delDate_) == Comparator.EQUAL && currentTime.Compare(delTime_) == Comparator.LESSER))
            {
                return true;
            }
        return false;
    }

    private void OnApplicationQuit()
    {
        SaveDataManager.instance.SaveOldMessages();
        SaveDataManager.instance.SaveNewMessages();
        SaveDataManager.instance.SavePlayerData();
        SaveDataManager.instance.SaveEvidences();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveDataManager.instance.SaveOldMessages();
            SaveDataManager.instance.SaveNewMessages();
            SaveDataManager.instance.SavePlayerData();
            SaveDataManager.instance.SaveEvidences();
        }
    }

}
