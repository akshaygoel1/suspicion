using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Responses : ScriptableObject
{
    public string responseText;
    public float waitingTime = 0.5f;
    public float typingTime = 1;
}
