using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EvidenceScreen : MonoBehaviour
{
    public GameObject infoScreen;
    public GameObject evidenceDatabaseScreen;
    public GameObject caseFileScreen;
    public Image infoButton, evidenceButton, caseFileButton;
    public Transform evidenceDatabaseContent;
    public GameObject evidenceDatabaseInfoText;
    private void OnEnable()
    {
        if (MyGameFlow.instance.previousScreen == MyGameFlow.instance.homeScreen)
        {
            EnableOrDisableAttachButtons(false);
        }
        else
        {
            EnableOrDisableAttachButtons(true);
        }


        if (GameManager.instance.evidenceCollected.Count == 0)
        {
            evidenceDatabaseInfoText.SetActive(true);
        }
        else
        {
            evidenceDatabaseInfoText.SetActive(false);

        }
    }


    void EnableOrDisableAttachButtons(bool setActive)
    {
        foreach(Transform child in evidenceDatabaseContent)
        {
            MessageEvidence mE = child.GetComponent<MessageEvidence>();
            GalleryEvidence gE = child.GetComponent<GalleryEvidence>();
            if (mE != null)
            {
                mE.attachButton.SetActive(setActive);
            }
            if (gE != null)
            {
                gE.attachButton.SetActive(setActive);
            }
        }
    }


    public void TabClick(int i)
    {
        if (i == 0)
        {
            evidenceDatabaseScreen.SetActive(false);
            evidenceButton.color = Color.white;
            infoScreen.SetActive(true);
            infoButton.color = Color.green;
            caseFileScreen.SetActive(false);
            caseFileButton.color = Color.white;
        }
        else if (i == 1)
        {
            caseFileScreen.SetActive(true);
            caseFileButton.color = Color.green;
            evidenceDatabaseScreen.SetActive(false);
            evidenceButton.color = Color.white;
            infoScreen.SetActive(false);
            infoButton.color = Color.white;

        }
        else if (i == 2)
        {
            evidenceDatabaseScreen.SetActive(true);
            infoScreen.SetActive(false);
            evidenceButton.color = Color.green;
            infoButton.color = Color.white;
            caseFileScreen.SetActive(false);
            caseFileButton.color = Color.white;
        }
    }
}
