using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILinker : MonoBehaviour {

    public List<UIObject> screens = new List<UIObject>();

    void Awake()
    {

        MyGameFlow.instance.RegisterUILinker(this);
    }

    /// <summary>
    /// Enables the screen mentioned, and disables the other screens
    /// </summary>
    /// <param name="screen">Screen Name to Enable</param>
    public IEnumerator EnableScreen(string screen, bool animate=false)
    {
        foreach (UIObject u in screens)
        {
            if (u.screenName != screen)
            {
                if (u.screen.gameObject.activeInHierarchy)
                {
                    if (u.animator == null || !animate)
                    {
                        u.screen.SetActive(false);

                    }
                    else
                    {

                        yield return StartCoroutine(AnimateScreen(u, false));
                    }
                }
            }
            }
            foreach (UIObject u in screens)
        {
            if (u.screenName == screen)
            {
                if (u.animator == null || !animate)
                {
                    u.screen.SetActive(true);

                }
                else
                {

                    yield return StartCoroutine(AnimateScreen(u, true));
                }
            }
        }
       
            /*else
            {
                u.screen.SetActive(true);
                yield return StartCoroutine(AnimateScreen(u, true));
            }*/
        }

      

    


    IEnumerator AnimateScreen(UIObject s, bool isIn)
    {
        if (isIn)
        {
            s.screen.SetActive(true);
            s.animator.SetTrigger("In");
            yield return new WaitForEndOfFrame();

            while (s.animator.GetCurrentAnimatorStateInfo(0).IsName("In_Anim"))
            {
                yield return null;
            }
        }
        else
        {
           /* s.animator.SetTrigger("Out");
            yield return new WaitForEndOfFrame();
            while (s.animator.GetCurrentAnimatorStateInfo(0).IsName("Out_Anim"))
            {
                yield return null;
            }*/
            s.screen.SetActive(false);
        }
    }

}
