using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Gameflow : MonoBehaviour {

    public ScreenElement currentScreen;
    public ScreenElement previousScreen;

    UILinker currentUILinker;


    public void RegisterUILinker(UILinker uiLinker)
    {
        currentUILinker = uiLinker;
    }

    public void InitWithScreen(ScreenElement screen)
    {
        currentScreen = screen;
        previousScreen = screen;
       StartCoroutine(currentUILinker.EnableScreen(screen.ScreenName,false));
    }

    public void SwitchScreen(ScreenElement screen, ScreenElement loadingScreen, float loadingTime, bool animate = true)
    {
        if (loadingScreen != null)
        {
            StartCoroutine(LoadingEffect(loadingScreen, loadingTime, screen, animate));
        }
        else
        {

            if (screen.SceneGroup != currentScreen.SceneGroup)
            {
                StartCoroutine(SwitchScene(screen));
            }
            else
            {
                previousScreen = currentScreen;
                StartCoroutine(currentUILinker.EnableScreen(screen.ScreenName, animate));
                currentScreen = screen;
            }
        }
    }

    IEnumerator LoadingEffect(ScreenElement loadingScreen, float time, ScreenElement finalScreen, bool animate = true)
    {
        SwitchScreen(loadingScreen, null, 0, false);
       
        yield return new WaitForSeconds(time);

        SwitchScreen(finalScreen, null, 0, animate);

    }

    public void SwitchToPreviousScreen()
    {
        SwitchScreen(previousScreen,null,0, false);
    }

    IEnumerator SwitchScene(ScreenElement screen)
    {
        SceneManager.LoadScene(screen.SceneGroup);
        string activeScene = SceneManager.GetActiveScene().name;
        while (activeScene != screen.SceneGroup)
        {
            activeScene = SceneManager.GetActiveScene().name;
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        previousScreen = currentScreen;
        StartCoroutine(currentUILinker.EnableScreen(screen.ScreenName));
        currentScreen = screen;
    }

}
