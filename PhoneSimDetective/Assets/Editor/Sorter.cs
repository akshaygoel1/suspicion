using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]
public class Sorter : EditorWindow
{
    [MenuItem("MyStory/RefreshMessages")]
    public static void ShowWindow()
    {
        //EditorWindow.GetWindow(typeof(MyStoryEditorWindow));
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");

        GameManager g = gameManager.GetComponent<GameManager>();

        foreach(MessageContainer m in g.messageManager.messages)
        {
            m.messageObject.Seen = false;
        }

        SortTheMessages(g.messageManager.messages);
        SortTheGallery(g.galleryManager.galleryImages);
        SortTheLockData(g.lockManager.lockContainer);
    }

    public static void SortTheMessages(List<MessageContainer> messages)
    {
        int n = messages.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (messages[j].messageObject.DateOfMessage.Year > messages[j + 1].messageObject.DateOfMessage.Year)
                {
                    MessageContainer temp = messages[j];
                    messages[j] = messages[j + 1];
                    messages[j + 1] = temp;
                    continue;
                }
                else if (messages[j].messageObject.DateOfMessage.Year == messages[j + 1].messageObject.DateOfMessage.Year)
                {
                    if (messages[j].messageObject.DateOfMessage.Month > messages[j + 1].messageObject.DateOfMessage.Month)
                    {
                        MessageContainer temp = messages[j];
                        messages[j] = messages[j + 1];
                        messages[j + 1] = temp;
                        continue;
                    }
                    else if (messages[j].messageObject.DateOfMessage.Month == messages[j + 1].messageObject.DateOfMessage.Month)
                    {
                        if (messages[j].messageObject.DateOfMessage.Day > messages[j + 1].messageObject.DateOfMessage.Day)
                        {
                            MessageContainer temp = messages[j];
                            messages[j] = messages[j + 1];
                            messages[j + 1] = temp;
                            continue;
                        }
                        else if (messages[j].messageObject.DateOfMessage.Day == messages[j + 1].messageObject.DateOfMessage.Day)
                        {
                            if (messages[j].messageObject.TimeOfMessage.Hour > messages[j + 1].messageObject.TimeOfMessage.Hour)
                            {
                                MessageContainer temp = messages[j];
                                messages[j] = messages[j + 1];
                                messages[j + 1] = temp;
                                continue;
                            }
                            else if (messages[j].messageObject.TimeOfMessage.Hour == messages[j + 1].messageObject.TimeOfMessage.Hour)
                            {
                                if (messages[j].messageObject.TimeOfMessage.Minutes > messages[j + 1].messageObject.TimeOfMessage.Minutes)
                                {
                                    MessageContainer temp = messages[j];
                                    messages[j] = messages[j + 1];
                                    messages[j + 1] = temp;
                                    continue;
                                }
                                else if (messages[j].messageObject.TimeOfMessage.Minutes == messages[j + 1].messageObject.TimeOfMessage.Minutes)
                                {
                                    if (messages[j].messageObject.TimeOfMessage.Seconds > messages[j + 1].messageObject.TimeOfMessage.Seconds)
                                    {
                                        MessageContainer temp = messages[j];
                                        messages[j] = messages[j + 1];
                                        messages[j + 1] = temp;
                                        continue;
                                    }
                                    else if (messages[j].messageObject.TimeOfMessage.Seconds == messages[j + 1].messageObject.TimeOfMessage.Seconds)
                                    {
                                        //Debug.LogError("Times are exact same!");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SortTheGallery(List<GalleryContainer> gallery)
    {
        int n = gallery.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (gallery[j].galleryObject.DateOfImage.Year < gallery[j + 1].galleryObject.DateOfImage.Year)
                {
                    GalleryContainer temp = gallery[j];
                    gallery[j] = gallery[j + 1];
                    gallery[j + 1] = temp;
                    continue;
                }
                else if (gallery[j].galleryObject.DateOfImage.Year == gallery[j + 1].galleryObject.DateOfImage.Year)
                {
                    if (gallery[j].galleryObject.DateOfImage.Month < gallery[j + 1].galleryObject.DateOfImage.Month)
                    {
                        GalleryContainer temp = gallery[j];
                        gallery[j] = gallery[j + 1];
                        gallery[j + 1] = temp;
                        continue;
                    }
                    else if (gallery[j].galleryObject.DateOfImage.Month == gallery[j + 1].galleryObject.DateOfImage.Month)
                    {
                        if (gallery[j].galleryObject.DateOfImage.Day < gallery[j + 1].galleryObject.DateOfImage.Day)
                        {
                            GalleryContainer temp = gallery[j];
                            gallery[j] = gallery[j + 1];
                            gallery[j + 1] = temp;
                            continue;
                        }
                        else if (gallery[j].galleryObject.DateOfImage.Day == gallery[j + 1].galleryObject.DateOfImage.Day)
                        {
                            if (gallery[j].galleryObject.TimeOfImage.Hour < gallery[j + 1].galleryObject.TimeOfImage.Hour)
                            {
                                GalleryContainer temp = gallery[j];
                                gallery[j] = gallery[j + 1];
                                gallery[j + 1] = temp;
                                continue;
                            }
                            else if (gallery[j].galleryObject.TimeOfImage.Hour == gallery[j + 1].galleryObject.TimeOfImage.Hour)
                            {
                                if (gallery[j].galleryObject.TimeOfImage.Minutes < gallery[j + 1].galleryObject.TimeOfImage.Minutes)
                                {
                                    GalleryContainer temp = gallery[j];
                                    gallery[j] = gallery[j + 1];
                                    gallery[j + 1] = temp;
                                    continue;
                                }
                                else if (gallery[j].galleryObject.TimeOfImage.Minutes == gallery[j + 1].galleryObject.TimeOfImage.Minutes)
                                {
                                    if (gallery[j].galleryObject.TimeOfImage.Seconds < gallery[j + 1].galleryObject.TimeOfImage.Seconds)
                                    {
                                        GalleryContainer temp = gallery[j];
                                        gallery[j] = gallery[j + 1];
                                        gallery[j + 1] = temp;
                                        continue;
                                    }
                                    else if (gallery[j].galleryObject.TimeOfImage.Seconds == gallery[j + 1].galleryObject.TimeOfImage.Seconds)
                                    {
                                        Debug.LogError("Times are exact same!");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SortTheLockData(List<LockScreenContainer> lockContainer)
    {
        int n = lockContainer.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (lockContainer[j].lockScreenObject.DateOfPasswordSet.Year > lockContainer[j + 1].lockScreenObject.DateOfPasswordSet.Year)
                {
                    LockScreenContainer temp = lockContainer[j];
                    lockContainer[j] = lockContainer[j + 1];
                    lockContainer[j + 1] = temp;
                    continue;
                }
                else if (lockContainer[j].lockScreenObject.DateOfPasswordSet.Year == lockContainer[j + 1].lockScreenObject.DateOfPasswordSet.Year)
                {
                    if (lockContainer[j].lockScreenObject.DateOfPasswordSet.Month > lockContainer[j + 1].lockScreenObject.DateOfPasswordSet.Month)
                    {
                        LockScreenContainer temp = lockContainer[j];
                        lockContainer[j] = lockContainer[j + 1];
                        lockContainer[j + 1] = temp;
                        continue;
                    }
                    else if (lockContainer[j].lockScreenObject.DateOfPasswordSet.Month == lockContainer[j + 1].lockScreenObject.DateOfPasswordSet.Month)
                    {
                        if (lockContainer[j].lockScreenObject.DateOfPasswordSet.Day > lockContainer[j + 1].lockScreenObject.DateOfPasswordSet.Day)
                        {
                            LockScreenContainer temp = lockContainer[j];
                            lockContainer[j] = lockContainer[j + 1];
                            lockContainer[j + 1] = temp;
                            continue;
                        }
                        else if (lockContainer[j].lockScreenObject.DateOfPasswordSet.Day == lockContainer[j + 1].lockScreenObject.DateOfPasswordSet.Day)
                        {
                            if (lockContainer[j].lockScreenObject.TimeOfPasswordSet.Hour > lockContainer[j + 1].lockScreenObject.TimeOfPasswordSet.Hour)
                            {
                                LockScreenContainer temp = lockContainer[j];
                                lockContainer[j] = lockContainer[j + 1];
                                lockContainer[j + 1] = temp;
                                continue;
                            }
                            else if (lockContainer[j].lockScreenObject.TimeOfPasswordSet.Hour == lockContainer[j + 1].lockScreenObject.TimeOfPasswordSet.Hour)
                            {
                                if (lockContainer[j].lockScreenObject.TimeOfPasswordSet.Minutes > lockContainer[j + 1].lockScreenObject.TimeOfPasswordSet.Minutes)
                                {
                                    LockScreenContainer temp = lockContainer[j];
                                    lockContainer[j] = lockContainer[j + 1];
                                    lockContainer[j + 1] = temp;
                                    continue;
                                }
                                else if (lockContainer[j].lockScreenObject.TimeOfPasswordSet.Minutes == lockContainer[j + 1].lockScreenObject.TimeOfPasswordSet.Minutes)
                                {
                                    if (lockContainer[j].lockScreenObject.TimeOfPasswordSet.Seconds > lockContainer[j + 1].lockScreenObject.TimeOfPasswordSet.Seconds)
                                    {
                                        LockScreenContainer temp = lockContainer[j];
                                        lockContainer[j] = lockContainer[j + 1];
                                        lockContainer[j + 1] = temp;
                                        continue;
                                    }
                                    else if (lockContainer[j].lockScreenObject.TimeOfPasswordSet.Seconds == lockContainer[j + 1].lockScreenObject.TimeOfPasswordSet.Seconds)
                                    {
                                        //Debug.LogError("Times are exact same!");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
