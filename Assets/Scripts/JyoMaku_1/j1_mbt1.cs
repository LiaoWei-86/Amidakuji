using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class j1_mbt1 : MonoBehaviour
{
    public GameObject image_hover;
    public GameObject image_pressed;
    public GameObject text;
    private bool canbepressed = false;
    public J1_GameController j1_GameController;
    // Start is called before the first frame update
    void Start()
    {
        image_hover.SetActive(false);
        image_pressed.SetActive(false);
        text.SetActive(false);
        if (j1_GameController == null)
        {
            j1_GameController = FindObjectOfType<J1_GameController>();
        }
    }

    void OnMouseEnter()
    {
        if (canbepressed)
        {
            image_hover.SetActive(true);
            Debug.Log("これはリトライだよ");
        }

    }

    void OnMouseOver()
    {
        if (canbepressed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                image_hover.SetActive(false);
                image_pressed.SetActive(true);
                j1_GameController.reTry();

            }
        }

    }

    void OnMouseExit()
    {
        if (canbepressed)
        {
            image_hover.SetActive(false);
            if (image_pressed.activeSelf)
            {
                image_pressed.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (j1_GameController.currentGameMode != J1_GameController.GameMode.TextPlaying)
        {
            canbepressed = true;
            text.SetActive(true);
        }
        else if (j1_GameController.currentGameMode == J1_GameController.GameMode.TextPlaying)
        {
            canbepressed = false;
            text.SetActive(false);
        }
    }
}
