using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatinDance : MonoBehaviour
{
    private int clickTimes = 0;
    public GameObject presentation;
    public GameObject latin;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (clickTimes == 0)
            {
                StartCoroutine(ShowImage());
                clickTimes++;
            }
            else
            {
                clickTimes = 0;
                StartCoroutine(NotShowImage());
            }
        }
    }

    IEnumerator ShowImage()
    {
        while (this.GetComponent<CanvasGroup>().alpha != 1f)
        {
            this.GetComponent<CanvasGroup>().alpha += 0.05f;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator NotShowImage()
    {
        while (this.GetComponent<CanvasGroup>().alpha != 0f)
        {
            this.GetComponent<CanvasGroup>().alpha -= 0.05f;
            presentation.GetComponent<CanvasGroup>().alpha -= 0.05f;
            yield return new WaitForFixedUpdate();
        }
        latin.SetActive(false);
    }
}
