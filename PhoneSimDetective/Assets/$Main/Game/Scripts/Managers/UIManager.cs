using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyStory;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour {
    public static UIManager instance = null;

    #region UI
    [SerializeField]
    GameObject LockScreen, HomeScreen, MessagesScreen, ChatViewScreen, GalleryScreen, ImageViewScreen;
    public GameObject EvidenceScreen, DatePicker;

    [SerializeField]
    Text CurrentTimeText, CurrentDateText;

    [SerializeField]
    Image[] LockHolder;

    [SerializeField]
    GameObject ConversationPrefab;
    [SerializeField]
    Transform ConversationHolder;

    public TextMeshProUGUI timeText;
    string tempPassword = "";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //
        }
        else
            Destroy(this);


    }
    void Start()
    {
#if UNITY_STANDALONE
        Screen.SetResolution(506,900,false);

#endif

        MyGameFlow.instance.InitWithScreen(MyGameFlow.instance.lockScreen);
        OnLockScreen();
    }
    public void OnLockScreen(bool loadingScreen = false)
    {

            if (loadingScreen)
            {
                MyGameFlow.instance.SwitchScreen(MyGameFlow.instance.lockScreen, MyGameFlow.instance.loadingScreenDateSetter, 3);

            }
            else
            {
            MyGameFlow.instance.SwitchScreen(MyGameFlow.instance.lockScreen,null, 0);

            }


        if (string.IsNullOrEmpty(GameManager.instance.lockManager.CurrentPassword))
        {
            LockScreen.transform.GetChild(0).gameObject.SetActive(false);
            LockScreen.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            tempPassword = "";
            for (int i = 0; i < LockHolder.Length; i++)
            {
                if (i < tempPassword.Length)
                {
                    LockHolder[i].color = Color.white;
                }
                else
                {
                    LockHolder[i].color = Color.grey;
                }
            }
            LockScreen.transform.GetChild(0).gameObject.SetActive(true);
            LockScreen.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void OnNumberClicked(string c)
    {
        tempPassword += c;
        if (tempPassword.Length >= 4)
        {
            if (tempPassword == GameManager.instance.lockManager.CurrentPassword)
            {
                SoundManager.instance.PlaySound(Sounds.PasswordUnlock);

                OnHomeScreen();
            }
            else
            {
                SoundManager.instance.PlaySound(Sounds.PasswordIncorrect);

                tempPassword = "";
            }
        }
        for (int i = 0; i < LockHolder.Length; i++)
        {
            if (i < tempPassword.Length)
            {
                LockHolder[i].color = Color.white;
            }
            else
            {
                LockHolder[i].color = Color.grey;
            }
        }

    }
    public void DeleteLast()
    {
        if (tempPassword != "")
        {
            tempPassword = tempPassword.Substring(0, tempPassword.Length - 1);
            for (int i = 0; i < LockHolder.Length; i++)
            {
                if (i < tempPassword.Length)
                {
                    LockHolder[i].color = Color.white;
                }
                else
                {
                    LockHolder[i].color = Color.grey;
                }
            }
        }
    }

    public void OnHomeScreen()
    {
        OnDateSet(GameManager.instance.currentTime, GameManager.instance.currentDate);
        MyGameFlow.instance.SwitchScreen(MyGameFlow.instance.homeScreen, null ,0);

    }

    public void OnGalleryScreen(bool animate)
    {

        MyGameFlow.instance.SwitchScreen(MyGameFlow.instance.galleryScreen,null,0, animate);
    }

    public void OnImageViewScreen(bool animate)
    {
        SoundManager.instance.PlaySound(Sounds.Click);
        MyGameFlow.instance.SwitchScreen(MyGameFlow.instance.imageViewScreen,null,0, animate);
    }

    public void OnMessagesScreen(bool animate)
    {

        MyGameFlow.instance.SwitchScreen(MyGameFlow.instance.messagesScreen,null,0, animate);
        
    }

    public void OnChatScreen()
    {
        SoundManager.instance.PlaySound(Sounds.Click);
        MyGameFlow.instance.SwitchScreen(MyGameFlow.instance.chatViewScreen,null,0, true);

    }

    public void BackButton()
    {
        SoundManager.instance.PlaySound(Sounds.Click);
        if (MessagesScreen.activeSelf || GalleryScreen.activeSelf)
          {
              OnHomeScreen();
          }
          else if (ImageViewScreen.activeSelf)
          {
              OnGalleryScreen(false);
          }
          else if (ChatViewScreen.activeSelf)
          {
              OnMessagesScreen(false);
          }
          else if(!HomeScreen.activeSelf && !LockScreen.activeSelf)
          {
              MyGameFlow.instance.SwitchToPreviousScreen();
          }

    }

    public void GoToEvidenceScreen()
    {
        MyGameFlow.instance.SwitchScreen(MyGameFlow.instance.evidenceScreen,null,0, true);
        UIManager.instance.EvidenceScreen.GetComponent<EvidenceScreen>().TabClick(0);
    }

    public void HomeButton()
    {
        if(!LockScreen.activeSelf)
            OnHomeScreen();
    }

    public void OnDateSet(GTime time, GDate date)
    {
        GameManager.instance.currentDate = date;
        GameManager.instance.currentTime = time;
        CurrentTimeText.text = string.Format("{0}:{1}",
            time.Hour.ToString("00"),
            time.Minutes.ToString("00"));
        CurrentDateText.text = string.Format("{0} {1} {2}",
            date.Day.ToString("00"),
            date.Month.ToString(),
            date.Year.ToString("0000"));

        timeText.text = string.Format("{0}:{1}",
            time.Hour.ToString("00"),
            time.Minutes.ToString("00"));
        //
    }

    public void OpenDatePicker()
    {
        /*DatePicker.instance.StartDatePicker(GameManager.instance.currentTime.Hour, GameManager.instance.currentTime.Minutes,
        GameManager.instance.currentTime.Seconds, GameManager.instance.currentDate.Day, (int)GameManager.instance.currentDate.Month, GameManager.instance.currentDate.Year);*/
        TutorialManager.instance.EndDateTutorial();
        DatePicker.SetActive(true);

    }

    public void NotificationClick()
    {
        MyGameFlow.instance.SwitchScreen(MyGameFlow.instance.evidenceScreen,null,0, true);
        UIManager.instance.EvidenceScreen.GetComponent<EvidenceScreen>().TabClick(0);
        GameManager.instance.notificationGO.SetActive(false);
    }

#endregion
}
