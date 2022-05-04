using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sounds
{
    Click,
    QMark,
    PasswordUnlock,
    PasswordIncorrect,
    MessageRecieved,
    EvidenceCollection,
    DialRotate,

}

public class SoundManager : MonoBehaviour
{
    public List<SFXFile> sfx = new List<SFXFile>();

    public static SoundManager instance = null;
    public AudioSource audioSource;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }


    public void PlaySound(Sounds s)
    {
        audioSource.PlayOneShot(sfx.Find(x => x._sound == s)._audioClip);
    }

   
}
[System.Serializable]
public class SFXFile
{
    public Sounds _sound;
    public AudioClip _audioClip;
}
