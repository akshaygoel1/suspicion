using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyStory;
public class GalleryEvidence : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI info;
    public TextMeshProUGUI dateTimeText;
    GEvidence gEvidence = new GEvidence();
    public GameObject attachButton;
    public GEvidence Evidence
    {
        get { return gEvidence; }
    }
    public void SetData(GEvidence evidence, GGallery gallery)
    {
        image.sprite = gallery.Image;
        info.text = gallery.Info;
        dateTimeText.text = gallery.DateOfImage.Day + " " + gallery.DateOfImage.Month + " " + gallery.DateOfImage.Year + "\n" + gallery.TimeOfImage.GetTimeHourMinutes(); //Hour + ":" + gallery.TimeOfImage.Minutes;
        gEvidence = evidence;
    }

    public void AttachEvidence()
    {
        //GImageMessage galleryMessage = new GImageMessage(image.sprite);
        GMessage galleryMessage = new GMessage();
        galleryMessage.typeOfMessage = TypeOfMessage.Image;
        galleryMessage.nameOfImage = GameManager.instance.galleryManager.galleryImages.Find(x => x.galleryObject.Image == image.sprite).name;
        galleryMessage.dateOfMessage = GameManager.instance.presentDate;
        galleryMessage.timeOfMessage = GameManager.instance.presentTime;
        galleryMessage.reciever = GameManager.instance.messageManager.nameOfCurrentPerson;
        galleryMessage.sender = People.Amira;
        galleryMessage.Seen = false;
        GameManager.instance.messageManager.StartTypeAnimationAndResponse(gEvidence, galleryMessage);
      
    }


    public void RemoveEvidence()
    {
        GameManager.instance.evidenceCollected.RemoveAll(x => x == gEvidence);
        GameManager.instance.RemoveEvidence(gEvidence, TypeOfMessage.Image);
    }
}
