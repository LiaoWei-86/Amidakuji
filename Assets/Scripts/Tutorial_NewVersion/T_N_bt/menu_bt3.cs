using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_bt3 : MonoBehaviour
{
    public GameObject image_hover;
    public GameObject image_pressed;

    public T_new_gameController t_New_GameController;
    void Start()
    {
        image_hover.SetActive(false);
        image_pressed.SetActive(false);
        if (t_New_GameController == null)
        {
            t_New_GameController = FindObjectOfType<T_new_gameController>();
        }
    }

    private void OnMouseEnter()
    {
        image_hover.SetActive(true);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            image_hover.SetActive(false);
            image_pressed.SetActive(true);
            t_New_GameController.quitGame();
        }
    }

    private void OnMouseExit()
    {
        image_hover.SetActive(false);
        if (image_pressed.activeSelf)
        {
            image_pressed.SetActive(false);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
