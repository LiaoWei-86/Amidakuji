using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class j0_menu : MonoBehaviour
{
    public GameObject menu; // menu_controller
    public bool menu_switch_on = false;
    public GameObject image_menu_hover_white;
    public GameObject image_menu_button;

    public J0_gameController j0_GameController;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        image_menu_hover_white.SetActive(false);
        
    }
    private void OnMouseEnter()
    {
        image_menu_hover_white.SetActive(true);
    }

    private void OnMouseExit()
    {
        image_menu_hover_white.SetActive(false);
    }
    // マウスがオブジェクト上にある際の処理
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (menu_switch_on)
            {
                image_menu_button.SetActive(true);
                menu.SetActive(false);
                menu_switch_on = false;
            }
            else if (!menu_switch_on)
            {
                image_menu_button.SetActive(false);
                menu.SetActive(true);
                
                j0_GameController.waitForSceneChange_Menu();

                menu_switch_on = true;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
