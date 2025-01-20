
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_A_bt1 : MonoBehaviour
{
    public GameObject image_hover;
    public GameObject image_pressed;
    public GameObject text;
    private bool canbepressed = false;
    public T_Again t_Again;

    // Start is called before the first frame update
    void Start()
    {
        image_hover.SetActive(false);
        image_pressed.SetActive(false);
        text.SetActive(false);
        if (t_Again == null)
        {
            t_Again = FindObjectOfType<T_Again>();
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
                t_Again.reTry();

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
        if (t_Again.currentGameMode != T_Again.GameMode.TextPlaying)
        {
            canbepressed = true;
            text.SetActive(true);
        }
        else if (t_Again.currentGameMode == T_Again.GameMode.TextPlaying)
        {
            canbepressed = false;
            text.SetActive(false);
        }
    }
}
