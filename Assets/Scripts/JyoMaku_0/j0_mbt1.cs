using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class j0_mbt1 : MonoBehaviour
{
    public GameObject image_hover;
    public GameObject image_pressed;
    public GameObject text;
    private bool canbepressed = false;
    public J0_gameController j0_GameController;
    // Start is called before the first frame update
    void Start()
    {
        image_hover.SetActive(false);
        image_pressed.SetActive(false);
        text.SetActive(false);
        if (j0_GameController == null)
        {
            j0_GameController = FindObjectOfType<J0_gameController>();
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
                j0_GameController.reTry();

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
        if (j0_GameController.currentGameMode != J0_gameController.GameMode.TextPlaying)
        {
            canbepressed = true;
            text.SetActive(true);
        }
        else if (j0_GameController.currentGameMode == J0_gameController.GameMode.TextPlaying)
        {
            canbepressed = false;
            text.SetActive(false);
        }
    }

}
