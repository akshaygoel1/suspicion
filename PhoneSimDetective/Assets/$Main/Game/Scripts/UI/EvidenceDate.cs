using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyStory;
public class EvidenceDate : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public GDate date;

    public void Init(GDate d)
    {
        date = d;
        dateText.text = d.GetDateDayMonth();
    }

    public void OnDateSelect()
    {
        foreach(Transform c in GameManager.instance.evidenceScreenContent)
        {
            if(c.GetComponent<MessageEvidence>()!=null || c.GetComponent<GalleryEvidence>() != null)
            Destroy(c.gameObject);
        }

        //Load the evidences of this date
        foreach(Evidence e in SaveDataManager.instance._listEvidence.evidences)
        {
            if (e.typeOfEvidence == TypeOfEvidence.Text)
            {
                GMessage message = GameManager.instance.messageManager.messages.Find(x => x.name == e.nameOfEvidenceFile).messageObject;
                GDate d = message.dateOfMessage;

                if (d.Compare(date) == Comparator.EQUAL)
                {
                    GameObject mE = Instantiate(GameManager.instance.messageEvidencePrefab, GameManager.instance.evidenceScreenContent, false);
                    //mE.transform.SetAsFirstSibling();
                    GEvidence ev = new GEvidence();
                    if (message.evidence != null)
                    {
                        ev = message.evidence.evidenceObject;
                    }
                    mE.GetComponent<MessageEvidence>().SetData(message.Sender.ToString(), message.DateOfMessage, message.TimeOfMessage, message.MessageContent, ev);
                }
            }
            else if(e.typeOfEvidence == TypeOfEvidence.Image)
            {
                GGallery galleryImage = GameManager.instance.galleryManager.galleryImages.Find(x => x.name == e.nameOfEvidenceFile).galleryObject;
                GDate d = galleryImage.DateOfImage;

                if (d.Compare(date) == Comparator.EQUAL)
                {
                    GameObject gE = Instantiate(GameManager.instance.galleryEvidencePrefab, GameManager.instance.evidenceScreenContent, false);

                    //gE.transform.SetAsFirstSibling();
                    GEvidence evidence = new GEvidence();

                    if (galleryImage.Evidence != null)
                    {
                        evidence = galleryImage.Evidence.evidenceObject;
                    }

                    gE.GetComponent<GalleryEvidence>().SetData(evidence, galleryImage);
                }
            }
        }

    }
}
