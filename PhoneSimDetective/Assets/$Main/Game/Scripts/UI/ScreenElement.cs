using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenElement {

    string screenName = "";
    string sceneGroup = "";

    public ScreenElement()
    {
        screenName = "";
        sceneGroup = "";
    }

    public ScreenElement(string screen, string scene)
    {
        screenName = screen;
        sceneGroup = scene;
    }


    public string ScreenName
    {
        get { return screenName; }
    }

    public string SceneGroup
    {
        get { return sceneGroup; }
    }

}
