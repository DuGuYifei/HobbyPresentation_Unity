using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taekwondo : MonoBehaviour
{
    public GameObject presentation, taekwondo;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(NotShowImage());
        }
    }

    IEnumerator NotShowImage()
    {
        while (presentation.GetComponent<CanvasGroup>().alpha != 0f)
        {
            presentation.GetComponent<CanvasGroup>().alpha -= 0.05f;
            yield return new WaitForFixedUpdate();
        }
        taekwondo.SetActive(false);
    }
}
