using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class j0_mbt2 : MonoBehaviour
{
    public GameObject image_hover;
    public GameObject image_pressed;

    public J0_gameController j0_GameController;

    void Start()
    {
        image_hover.SetActive(false);
        image_pressed.SetActive(false);

        if (j0_GameController == null)
        {
            j0_GameController = FindObjectOfType<J0_gameController>();
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
            j0_GameController.toMainScene();
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
