using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ending_menu_button : MonoBehaviour
{
    public GameObject image_hover;
    public GameObject image_button;
    private bool switch_on = false;
    public GameObject menu_options;

    // Start is called before the first frame update
    void Start()
    {
        image_hover.SetActive(false);
        menu_options.SetActive(false);
    }

    private void OnMouseEnter()
    {
        image_hover.SetActive(true);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (switch_on)
            {
                image_hover.SetActive(false);
                image_button.SetActive(true);

                menu_options.SetActive(false);

                switch_on = false;
            }
            else if (!switch_on)
            {
                image_hover.SetActive(false);
                image_button.SetActive(false);

                menu_options.SetActive(true);

                switch_on = true;
            }
        }
    }

    private void OnMouseExit()
    {
        if (image_hover.activeSelf)
        {
            image_hover.SetActive(false);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (switch_on)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("LoadScene:Tutorial_NewVersion_Again");
                SceneManager.LoadScene("Tutorial_NewVersion_Again");
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                Debug.Log("LoadScene:M");
                SceneManager.LoadScene("M");
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit(); // ゲームを閉じる
            }
        }
    }
}
