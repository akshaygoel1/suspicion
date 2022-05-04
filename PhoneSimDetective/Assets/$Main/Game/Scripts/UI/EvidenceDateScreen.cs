using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyStory;
public class EvidenceDateScreen : MonoBehaviour
{
    public GameObject evidenceDateContent;
    private void OnEnable()
    {
        int count = SaveDataManager.instance._listEvidence.evidences.Count;
        if (count != 0)
        {
            GDate lastDate = new GDate();
            if (SaveDataManager.instance._listEvidence.evidences[count - 1].typeOfEvidence == TypeOfEvidence.Text) {
                lastDate = GameManager.instance.messageManager.messages.Find(x => x.name == SaveDataManager.instance._listEvidence.evidences[count - 1].nameOfEvidenceFile).messageObject.DateOfMessage;
            }
            else if (SaveDataManager.instance._listEvidence.evidences[count - 1].typeOfEvidence == TypeOfEvidence.Image)
            {
                lastDate = GameManager.instance.galleryManager.galleryImages.Find(x => x.name == SaveDataManager.instance._listEvidence.evidences[count - 1].nameOfEvidenceFile).galleryObject.DateOfImage;

            }

            foreach(RectTransform c in evidenceDateContent.GetComponent<RectTransform>())
            {
                if(c.GetComponent<EvidenceDate>().date.Compare(lastDate) == Comparator.EQUAL)
                {
                    c.GetComponent<EvidenceDate>().OnDateSelect();
                }
            }
        }
    }
}
