using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_bt1 : MonoBehaviour
{
    public GameObject image_hover;
    public GameObject image_pressed;
    public GameObject text;
    private bool canbepressed = false;
    public T_new_gameController t_New_GameController;
    
    // Start is called before the first frame update
    void Start()
    {
        image_hover.SetActive(false);
        image_pressed.SetActive(false);
        text.SetActive(false);
        if (t_New_GameController == null)
        {
            t_New_GameController = FindObjectOfType<T_new_gameController>();
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
                t_New_GameController.reTry();

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
        if (t_New_GameController.currentGameMode == T_new_gameController.GameMode.PlayerPlaying)
        {
            canbepressed = true;
            text.SetActive(true);
        }
        else if (t_New_GameController.currentGameMode != T_new_gameController.GameMode.PlayerPlaying)
        {
            canbepressed = false;
            text.SetActive(false);
        }
    }
}
