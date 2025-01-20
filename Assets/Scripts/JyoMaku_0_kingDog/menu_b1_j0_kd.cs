using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_b1_j0_kd : MonoBehaviour
{
    public JyoMaku_0_kingDog_gameController jyoMaku_0_KingDog_GameController;
    public menu_button_j0_kd_m Menu_Button_J0_Kd_M;

    public GameObject image_hover;
    public GameObject image_blue;

    // Start is called before the first frame update
    void Start()
    {
        if (jyoMaku_0_KingDog_GameController == null)
        {
            jyoMaku_0_KingDog_GameController = FindObjectOfType<JyoMaku_0_kingDog_gameController>();
        }
        if (Menu_Button_J0_Kd_M == null)
        {
            Menu_Button_J0_Kd_M = FindObjectOfType<menu_button_j0_kd_m>();
        }
        image_hover.SetActive(false);
        image_blue.SetActive(false);
    }

    private void OnMouseEnter()
    {
        image_hover.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (image_hover.activeSelf)
        {
            image_hover.SetActive(false);
        }

        if (image_blue.activeSelf)
        {
            image_blue.SetActive(false);
        }

    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            image_hover.SetActive(false);
            image_blue.SetActive(true);
            
            jyoMaku_0_KingDog_GameController.reTry();

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
