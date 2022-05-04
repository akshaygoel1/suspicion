using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GalleryEvidenceCollector : MonoBehaviour, IPointerClickHandler
{
    int tap;
    float lastTimeClick;
    public void OnPointerClick(PointerEventData eventData)
    {

        float currentTimeClick = eventData.clickTime;
        if (Mathf.Abs(currentTimeClick - lastTimeClick) < 0.75f)
        {
            SoundManager.instance.PlaySound(Sounds.EvidenceCollection);

            lastTimeClick = 0;
            this.transform.parent.GetComponent<ImageView>().SaveEvidence();
        
        }
        lastTimeClick = currentTimeClick;


    }
}
