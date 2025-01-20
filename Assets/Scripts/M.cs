using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Tutorial_0_Scene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene("JyoMaku_before_0");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene("JyoMaku_0.5");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            SceneManager.LoadScene("JyoMaku_0_king_dog");
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("Tutorial_NewVersion");
        }
    }
}
