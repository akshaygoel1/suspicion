using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance = null;
    public GameObject tutorial1Holder;
    public GameObject tutorial2_1Holder;
    public GameObject tutorial2_2Holder;
    [HideInInspector]public bool isTutorial2Active = false;
    public GameObject DateSetterIcon;
    public GameObject HomeButtonIcon;
    GameObject tempTut2_1;
    public GameObject helper1Button;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }


    private void Start()
    {

        if (PlayerPrefs.GetInt(SaveData.Tutorial1Done.ToString()) == 0) { 
        DateSetterIcon.SetActive(false);
        HomeButtonIcon.SetActive(false);
        helper1Button.SetActive(false);
            }
    }


    public void ShowHelper1()
    {
        if (PlayerPrefs.GetInt(SaveData.Tutorial1Done.ToString()) == 0)
        {
            helper1Button.SetActive(true);
        }
    }

    public void StartDateTutorial()
    {
        if (PlayerPrefs.GetInt(SaveData.Tutorial1Done.ToString()) == 0)
        {
            tutorial1Holder.SetActive(true);
            DateSetterIcon.SetActive(true);
            HomeButtonIcon.SetActive(true);
        }
    }

    public void EndDateTutorial()
    {
        if (PlayerPrefs.GetInt(SaveData.Tutorial1Done.ToString()) == 0)
        {
            tutorial1Holder.SetActive(false);
            PlayerPrefs.SetInt(SaveData.Tutorial1Done.ToString(), 1);
        }
    }

    public void StartEvidenceTutorial1(GameObject g)
    {
        if (PlayerPrefs.GetInt(SaveData.Tutorial2Done.ToString()) == 0)
        {
            isTutorial2Active = true;
            tempTut2_1 = Instantiate(tutorial2_1Holder, g.transform);
        }
    }

    public void EndEvidenceTutorial()
    {
        if (PlayerPrefs.GetInt(SaveData.Tutorial2Done.ToString()) == 0)
        {
            Destroy(tempTut2_1);

            StartEvidenceTutorial2();
        }
    }

    public void StartEvidenceTutorial2()
    {
        if (PlayerPrefs.GetInt(SaveData.Tutorial2Done.ToString()) == 0)
        {
            tutorial2_2Holder.SetActive(true);
        }
    }
    public void EndEvidenceTutorial2()
    {
        if (PlayerPrefs.GetInt(SaveData.Tutorial2Done.ToString()) == 0)
        {
            tutorial2_2Holder.SetActive(false);
            isTutorial2Active = false;
            
        }

    }

}
