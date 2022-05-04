using MyStory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryManager : MonoBehaviour
{
    public List<GalleryContainer> galleryImages = new List<GalleryContainer>();
    public GameObject galleryPrefab;
    public Transform galleryContent;
    GGallery currentGalleryImage = new GGallery();

    public GGallery CurrentGalleryImage
    {
        get { return currentGalleryImage; }
        set { currentGalleryImage = value; }
    }

    private void Awake()
    {
        foreach (GalleryContainer g in galleryImages)
        {
            g.galleryObject.nameOfImage = g.name;
        }
    }

    public void SetCurrentGalleryImage(GGallery g)
    {
        currentGalleryImage = g;
    }

    public void CreateGallery()
    {
        foreach (Transform child in galleryContent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (GalleryContainer g in galleryImages)
        {
            if (GameManager.instance.ShouldContentBeVisible(g.galleryObject.DateOfImage, g.galleryObject.TimeOfImage, g.galleryObject.IsDeleted, g.galleryObject.DeletedDate, g.galleryObject.DeletedTime))
            {
                GameObject galleryImage = Instantiate(galleryPrefab, galleryContent, false);

                GEvidence evidence = null;
                if (g.galleryObject.Evidence != null)
                {
                    evidence = g.galleryObject.Evidence.evidenceObject;
                }

                galleryImage.GetComponent<GalleryScreen>().SetData(g.galleryObject, evidence);
            }
            //galleryImage.GetComponent
        }
    }


    public GGallery FindNextGalleryImage()
    {
        int currentInd = galleryImages.FindIndex(0, galleryImages.Count, x => x.galleryObject == currentGalleryImage);
        int i = currentInd + 1;
        while (i < galleryImages.Count)
        {
            GGallery g = galleryImages[i].galleryObject;
            if (GameManager.instance.ShouldContentBeVisible(g.DateOfImage, g.TimeOfImage, g.IsDeleted, g.DeletedDate, g.DeletedTime))
            {

                return g;
            }
            i++;
        }
        return null;
    }


    public GGallery FindPrevioudGalleryImage()
    {
        int currentInd = galleryImages.FindIndex(0, galleryImages.Count, x => x.galleryObject == currentGalleryImage);
        int i = currentInd - 1;
        while (i >= 0)
        {
            GGallery g = galleryImages[i].galleryObject;
            if (GameManager.instance.ShouldContentBeVisible(g.DateOfImage, g.TimeOfImage, g.IsDeleted, g.DeletedDate, g.DeletedTime))
            {
                return g;
            }
            i--;
        }
        return null;
    }

}
