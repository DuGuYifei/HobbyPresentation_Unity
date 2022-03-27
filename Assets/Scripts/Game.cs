using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject presentation, game;
    public GameObject unity, ue5;
    public GameObject unityS, ue5S;

    private int clickTimes = 0;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(clickTimes == 0 )
            {
                unity.SetActive(true);
                clickTimes++;
            }
            else if (clickTimes == 1)
            {
                ue5.SetActive(true);
                clickTimes++;
            }
            else if (clickTimes == 2)
            {
                unity.SetActive(false);
                ue5.SetActive(false);
                unityS.SetActive(true);
                ue5S.SetActive(true);
                clickTimes++;
            }
            else if (clickTimes == 3)
            {
                StartCoroutine(NotShowImage());
                clickTimes = 0;
            }
        }
    }


    IEnumerator NotShowImage()
    {
        while (presentation.GetComponent<CanvasGroup>().alpha != 0f)
        {
            presentation.GetComponent<CanvasGroup>().alpha -= 0.05f;
            yield return new WaitForFixedUpdate();
        }
        unityS.SetActive(false);
        ue5S.SetActive(false);

        game.SetActive(false);
    }
}
