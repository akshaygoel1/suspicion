using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameFlow : Gameflow {

    public static MyGameFlow instance = null;

    public ScreenElement lockScreen = new ScreenElement("LockScreen", "Game");
    public ScreenElement galleryScreen = new ScreenElement("GalleryScreen", "Game");
    public ScreenElement homeScreen = new ScreenElement("HomeScreen", "Game");
    public ScreenElement messagesScreen = new ScreenElement("MessagesScreen", "Game");
    public ScreenElement chatViewScreen = new ScreenElement("ChatViewScreen", "Game");
    public ScreenElement imageViewScreen = new ScreenElement("ImageViewScreen", "Game");
    public ScreenElement evidenceScreen = new ScreenElement("EvidenceScreen", "Game");
    public ScreenElement loadingScreenDateSetter = new ScreenElement("LoadingDateSetter", "Game");
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);

        //InitWithScreen(lockScreen);
    }

}
