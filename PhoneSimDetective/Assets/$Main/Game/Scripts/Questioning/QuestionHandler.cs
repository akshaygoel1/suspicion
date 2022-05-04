using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyStory;
public class QuestionHandler : MonoBehaviour
{
    public GameObject chatQuestionPrefab;
    GameObject[] chatQuestions = new GameObject[3];

    public static QuestionHandler instance = null;
    GEvidence tempEvidence;
    List<int> questionsAsked = new List<int>();
    public GEvidence CurrentEvidence
    {
        get { return tempEvidence; }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void SetupQuestions(GEvidence evidence)
    {
        tempEvidence = evidence;
        bool isRelevantEvidence = true;

        if (evidence.questions == null || evidence.SuspectToAsk!=GameManager.instance.messageManager.nameOfCurrentPerson)
        {
            isRelevantEvidence = false;
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                if (evidence.questions[i] == null)
                {
                    isRelevantEvidence = false;
                    break;
                }
            }
        }
        if (isRelevantEvidence)
        {
            for(int i = 0; i < 3; i++)
            {
                chatQuestions[i] = Instantiate(chatQuestionPrefab, GameManager.instance.messageManager.conversationContent.transform, false);
                chatQuestions[i].GetComponent<QuestionSelecter>().Init(i);
            }
        }
        else
        {
            GameManager.instance.messageManager.StartResponse(new Responses(), tempEvidence);
        }

    }


    public void QuestionAsked(int index)
    {
        questionsAsked.Add(index);
        foreach(GameObject g in chatQuestions)
        {
            if(g!=null)
            Destroy(g);
        }
    }

    public void CheckForAdditionalQuestions()
    {
        if(questionsAsked.Count == 1)
        {
            int indexOfQuestionAsked = questionsAsked[0];
            if (tempEvidence.questions[indexOfQuestionAsked].isImportant)
            {
                List<int> otherQ = new List<int>();
               for(int i=0;i<3; i++)
                {
                    if (i != indexOfQuestionAsked)
                    {
                        otherQ.Add(i);
                    }
                }

                int randomIndex = Random.Range(0,2);
                chatQuestions[0] = Instantiate(chatQuestionPrefab, GameManager.instance.messageManager.conversationContent.transform, false);
                chatQuestions[0].GetComponent<QuestionSelecter>().Init(otherQ[randomIndex]);
            }
            else
            {
                int importantIndex = -1;
                for (int i = 0; i < 3; i++)
                {
                    if (tempEvidence.questions[i].isImportant)
                    {
                        importantIndex = i;
                        break;
                    }
                }
                chatQuestions[0] = Instantiate(chatQuestionPrefab, GameManager.instance.messageManager.conversationContent.transform, false);
                chatQuestions[0].GetComponent<QuestionSelecter>().Init(importantIndex);
            }
        }
        else
        {
            questionsAsked.Clear();
        }
    }
}
