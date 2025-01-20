using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_button_j0_kd_m : MonoBehaviour
{
    public GameObject menu;
    public GameObject image_button;
    public GameObject image_hover;
    private bool menu_on = false;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        image_hover.SetActive(false);
    }

    private void OnMouseEnter()
    {
        image_hover.SetActive(true);
    }

    private void OnMouseExit()
    {
        image_hover.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (menu_on)
            {
                closeMenu();
            }
            else if (!menu_on)
            {
                openMenu();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void openMenu()
    {
        menu.SetActive(true);
        menu_on = true;
    }

    public void closeMenu()
    {
        menu.SetActive(false);
        menu_on = false;
    }
}
