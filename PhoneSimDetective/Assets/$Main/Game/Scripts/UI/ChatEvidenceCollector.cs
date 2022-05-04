using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ChatEvidenceCollector : MonoBehaviour, IPointerClickHandler
{
    int tap;
    float lastTimeClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        Chat chat = this.transform.parent.GetComponent<Chat>();

        if (chat.currentMessageObject.isCollectibleEvidence)
        {
            float currentTimeClick = eventData.clickTime;
            if (Mathf.Abs(currentTimeClick - lastTimeClick) < 0.75f)
            {
                SoundManager.instance.PlaySound(Sounds.EvidenceCollection);
                lastTimeClick = 0;
                chat.SaveEvidence();

                if (TutorialManager.instance.isTutorial2Active)
                {
                    TutorialManager.instance.EndEvidenceTutorial();
                }
            }
            lastTimeClick = currentTimeClick;
        }

    }
}
