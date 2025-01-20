using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_A_bt2 : MonoBehaviour
{
    public GameObject image_hover;
    public GameObject image_pressed;

    public T_Again t_a_GameController;

    // Start is called before the first frame update
    void Start()
    {
        image_hover.SetActive(false);
        image_pressed.SetActive(false);
        if (t_a_GameController == null)
        {
            t_a_GameController = FindObjectOfType<T_Again>();
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
            t_a_GameController.toMainScene();
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
