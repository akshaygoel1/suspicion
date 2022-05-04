using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyStory;
public class GalleryScreen : MonoBehaviour
{
    [HideInInspector]
    public string info;
    public Image image;
    GGallery galleryImage = new GGallery();
    GEvidence gEvidence = new GEvidence();
    public void OnClick()
    {
        GameManager.instance.galleryManager.SetCurrentGalleryImage(galleryImage);
        UIManager.instance.OnImageViewScreen(true);
    }

    public void SetData(GGallery gallery, GEvidence evidence)
    {
        galleryImage = gallery;
        gEvidence = evidence;
        image.sprite = gallery.Image;
        info = gallery.Info;
    }
}
