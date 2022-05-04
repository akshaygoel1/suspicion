using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyStory;
public class ImageView : MonoBehaviour
{

    public Image imageViewer;
    public TMPro.TextMeshProUGUI infoText;
    public TMPro.TextMeshProUGUI dateText;
    public GameObject rightArrow;
    public GameObject leftArrow;
    public GameObject bookmarkIcon;
    private void OnEnable()
    {
        bookmarkIcon.SetActive(false);
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);

        GGallery g = GameManager.instance.galleryManager.CurrentGalleryImage;

        imageViewer.sprite = g.Image;
        infoText.text = g.Info;
        dateText.text = g.DateOfImage.GetDate() + "\n" + g.TimeOfImage.GetTimeHourMinutes();
    }

    public void SaveEvidence()
    {
        EvidenceContainer evidenceContainer = new EvidenceContainer();
        evidenceContainer.evidenceObject = new MyStory.GEvidence();
        GGallery g = GameManager.instance.galleryManager.CurrentGalleryImage;
        if (g.Evidence != null)
        {
            evidenceContainer.evidenceObject = g.Evidence.evidenceObject;

        }

        GameManager.instance.AddEvidence(evidenceContainer.evidenceObject, g);
        bookmarkIcon.SetActive(true);
    }


    public void RightArrow()
    {
        bookmarkIcon.SetActive(false);
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);
        GGallery g = GameManager.instance.galleryManager.FindNextGalleryImage();
        if (g == null)
        {
            rightArrow.SetActive(false);
            return;
        }
        GameManager.instance.galleryManager.CurrentGalleryImage = g;
        imageViewer.sprite = g.Image;
        infoText.text = g.Info;
        dateText.text = g.DateOfImage.GetDate() + "\n" +
             g.TimeOfImage.GetTimeHourMinutes();
    }


    public void LeftArrow()
    {
         bookmarkIcon.SetActive(false);
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);
        GGallery g = GameManager.instance.galleryManager.FindPrevioudGalleryImage();
        if (g == null)
        {
            leftArrow.SetActive(false);
            return;
        }
        GameManager.instance.galleryManager.CurrentGalleryImage = g;
        imageViewer.sprite = g.Image;
        infoText.text = g.Info;
        dateText.text = g.DateOfImage.GetDate() + "\n" +
             g.TimeOfImage.GetTimeHourMinutes();
    }


}
