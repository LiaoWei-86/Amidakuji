using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class m_b3_ending : MonoBehaviour
{
    public GameObject image_hover;
    public GameObject image_pressed;

    // Start is called before the first frame update
    void Start()
    {
        image_hover.SetActive(false);
        image_pressed.SetActive(false);
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

            Application.Quit(); // ゲームを閉じる
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
