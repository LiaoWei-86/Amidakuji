using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_b2_j0_kd : MonoBehaviour
{
    public JyoMaku_0_kingDog_gameController jyoMaku_0_KingDog_GameController;

    public GameObject image_hover;
    public GameObject image_blue;
    // Start is called before the first frame update
    void Start()
    {
        if (jyoMaku_0_KingDog_GameController == null)
        {
            jyoMaku_0_KingDog_GameController = FindObjectOfType<JyoMaku_0_kingDog_gameController>();
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
        image_hover.SetActive(false);
        if (image_blue.activeSelf)
        {
            image_blue.SetActive(false);
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            image_blue.SetActive(true);
            jyoMaku_0_KingDog_GameController.toMainScene();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
