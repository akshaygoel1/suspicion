using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestionSelecter : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    int qIndex;

    private void Start()
    {
        StartCoroutine(ParentFit());
    }

    public void Init(int index)
    {
        qIndex = index;
      
        questionText.text = QuestionHandler.instance.CurrentEvidence.questions[qIndex].text;
    }


    public void AskThisQuestion()
    {
        GameManager.instance.messageManager.StartQuestioning(QuestionHandler.instance.CurrentEvidence.questions[qIndex], QuestionHandler.instance.CurrentEvidence);
        QuestionHandler.instance.QuestionAsked(qIndex);
    }


    IEnumerator ParentFit()
    {
        yield return new WaitForEndOfFrame();
        RectTransform myRect = GetComponent<RectTransform>();
        RectTransform childRect = transform.GetChild(0).GetComponent<RectTransform>();
        myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, childRect.sizeDelta.y);
    }
}
