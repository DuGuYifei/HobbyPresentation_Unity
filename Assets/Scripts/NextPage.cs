using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPage : MonoBehaviour
{
    public GameObject build;
    public GameObject unity;
    public GameObject vs;

    public GameObject presentation, game;

    private int clickTimes = 0;

    public void ClickNext()
    {
        if (clickTimes == 0)
        {
            build.SetActive(false);
            unity.SetActive(true);
            clickTimes++;
        }
        else if (clickTimes == 1)
        {
            unity.SetActive(false);
            vs.SetActive(true);
            clickTimes++;
        }
        else if (clickTimes == 2)
        {
            StartCoroutine(ShowImage());
            clickTimes = 0;
        }
    }

    IEnumerator ShowImage()
    {
        game.SetActive(true);

        while (presentation.GetComponent<CanvasGroup>().alpha != 1f)
        {
            presentation.GetComponent<CanvasGroup>().alpha += 0.05f;
            yield return new WaitForFixedUpdate();
        }

        vs.SetActive(false);
        build.SetActive(true);
    }
}
