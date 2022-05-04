using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TypingObject : MonoBehaviour
{
    public TextMeshProUGUI typingText;


    private void Start()
    {
        StartCoroutine(AnimatingTyping());
    }

    IEnumerator AnimatingTyping()
    {
        while (true)
        {
            typingText.text = "Typing";
            yield return new WaitForSeconds(0.3f);
            typingText.text = "Typing.";
            yield return new WaitForSeconds(0.3f);
            typingText.text = "Typing..";
            yield return new WaitForSeconds(0.3f);
            typingText.text = "Typing...";
            yield return new WaitForSeconds(0.3f);
        }
    }

}
