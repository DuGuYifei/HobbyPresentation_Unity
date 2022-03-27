using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject character;
    public GameObject door;
    public GameObject presentation;
    public GameObject titleImage;
    public GameObject titleText;
    public GameObject latin;
    public GameObject taekwondo;

    private Vector3 currentPositionVelocity;
    private float currentAngleVelocity;

    private RaycastHit hit;

    private const float smoothTime = 0.5f;
    private float camPositionZ = 11.62f;

    private bool takeKey = false;
    private bool hasChr = false;
    private bool openShelf = false;
    private bool startPresentation = true;
    private bool gameover = false;

    //init camera position
    //2 -0.61 11.62 0 180 0 40

    // Update is called once per frame
    void Update()
    {
        if(gameover && Input.GetMouseButtonDown(0))
        {
            Application.Quit();
        }

        if(startPresentation && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(CanvasAlpha(false));
        }
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            if (hit.collider.name == "Taekwondo")
            {
                StopAllCoroutines();
                StartCoroutine(MoveCamera(0.11f, -0.61f, -5.4f, "Taekwondo"));
            }

            else if (hit.collider.name == "LatinDance")
            {
                StopAllCoroutines();
                StartCoroutine(MoveCamera(-2.52f, -0.61f, -5.4f, "LatinDance"));
            }

            else if (hit.collider.name == "Bookshelf")
            {
                StopAllCoroutines();
                StartCoroutine(MoveCamera(4.9f, -1.18f, -2.42f));
                openShelf = true;
            }

            else if (hit.collider.name == "Computer")
            {
                StopAllCoroutines();
                StartCoroutine(MoveCamera(-5.16f, -0.79f, -0.4f,270f));
            }

            else if (hit.collider.name == "Door")
            {
                if(hasChr && takeKey)
                {
                    StartCoroutine(ChrLeave());
                }
            }
        }

        if (openShelf && Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, 1 << 4))
        {
            if (hit.collider.name == "Key")
            {
                Destroy(hit.collider.gameObject);
                takeKey = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            character.SetActive(true);
            hasChr = true;
            camPositionZ = 15f;
            StartCoroutine(MoveCamera(Camera.main.transform.position.x, Camera.main.transform.position.y, camPositionZ));
        }

        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            openShelf = false;

            StopAllCoroutines();
            StartCoroutine(MoveCamera(1.1f, -0.61f, camPositionZ, 180f));
        }
    }

    IEnumerator CanvasAlpha(bool isTransparent)
    {
        if (!isTransparent)
        {
            while (presentation.GetComponent<CanvasGroup>().alpha != 0)
            {
                presentation.GetComponent<CanvasGroup>().alpha -= 0.05f;
                yield return new WaitForFixedUpdate();
            }
            if(startPresentation)
            {
                startPresentation = false;
                titleText.GetComponent<Text>().text = "Thanks";
                titleImage.SetActive(false);
            }
        }
        else
        {
            while (presentation.GetComponent<CanvasGroup>().alpha != 1)
            {
                presentation.GetComponent<CanvasGroup>().alpha += 0.05f;
                yield return new WaitForFixedUpdate();
            }
        }
    }

    IEnumerator ChrLeave()
    {
        character.transform.eulerAngles = new Vector3(0f, 90f, 0f);

        Vector3 destination = new Vector3(6.78f, character.transform.position.y, character.transform.position.z);
        Vector3 moveSpeed = new Vector3(0.1f, 0f, 0f);

        while (character.transform.position.x <= destination.x)
        {
            character.transform.position += moveSpeed;
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        Vector3 moveSpeed = new Vector3(0f, 0f, 0.04f);

        while (door.transform.position.z <= 2.38f)
        {
            door.transform.position += moveSpeed;
            yield return new WaitForFixedUpdate();
        }
        
        titleImage.SetActive(true);
        StartCoroutine(CanvasAlpha(true));

        gameover = true;
    }

    IEnumerator MoveCamera(float cameraX, float cameraY, float cameraZ, string coroutine = " ")
    {
        currentPositionVelocity = new Vector3(0f, 0f, 0f);

        yield return null;

        while (Camera.main.transform.position != new Vector3(cameraX, cameraY, cameraZ))
        {
            Camera.main.transform.position = new Vector3
                (Mathf.SmoothDamp(Camera.main.transform.position.x, cameraX, ref currentPositionVelocity.x, smoothTime),
                Mathf.SmoothDamp(Camera.main.transform.position.y, cameraY, ref currentPositionVelocity.y, smoothTime),
                Mathf.SmoothDamp(Camera.main.transform.position.z, cameraZ, ref currentPositionVelocity.z, smoothTime));

            if (coroutine != " ")
            {
                StartCoroutine(coroutine);
            }

            yield return new WaitForFixedUpdate();
        }

        
    }

    IEnumerator MoveCamera(float cameraX, float cameraY, float cameraZ, float cameraAngleY)
    {
        currentAngleVelocity = 0f;
        currentPositionVelocity = new Vector3(0f, 0f, 0f);

        Vector3 destination = new Vector3(cameraX, cameraY, cameraZ);

        yield return null;

        while (Camera.main.transform.position != destination || Camera.main.transform.eulerAngles.y != cameraAngleY)
        {
            Camera.main.transform.position = new Vector3
                (Mathf.SmoothDamp(Camera.main.transform.position.x, cameraX, ref currentPositionVelocity.x, smoothTime),
                Mathf.SmoothDamp(Camera.main.transform.position.y, cameraY, ref currentPositionVelocity.y, smoothTime),
                Mathf.SmoothDamp(Camera.main.transform.position.z, cameraZ, ref currentPositionVelocity.z, smoothTime));

            Camera.main.transform.eulerAngles = new Vector3(0f, Mathf.SmoothDampAngle(Camera.main.transform.eulerAngles.y, cameraAngleY, ref currentAngleVelocity, smoothTime));

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator LatinDance()
    {
        yield return new WaitForSeconds(2f);
        latin.SetActive(true);
        StartCoroutine(CanvasAlpha(true));
    }

    IEnumerator Taekwondo()
    {
        yield return new WaitForSeconds(2f);
        taekwondo.SetActive(true);
        StartCoroutine(CanvasAlpha(true));
    }

}
