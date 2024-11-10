using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_explaIconMode : MonoBehaviour
{
    public GameObject frame;
    public GameObject on_switch;
    public GameObject off_switch;

    public GameObject guillotine;
    public GameObject coin;
    public GameObject battle;

    public battle _battle;
    public coin _coin;
    public guillotine _guillotine;

    public bool current_mode_on = true;

    // Start is called before the first frame update
    void Start()
    {
        if (frame != null)
        {
            frame.SetActive(false);
        }


        if (_battle == null)
        {
            _battle = FindObjectOfType<battle>();
        }

        if (_coin == null)
        {
            _coin = FindObjectOfType<coin>();
        }

        if (_guillotine == null)
        {
            _guillotine = FindObjectOfType<guillotine>();
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (current_mode_on)
            {
                current_mode_on = false;
                if ( on_switch != null && off_switch  != null )
                {
                    on_switch.SetActive(false);
                    off_switch.SetActive(true);
                    _battle.iconExplaMode = false;
                    _guillotine.iconExplaMode = false;
                    _coin.iconExplaMode = false;
                }
            }
            else if (!current_mode_on)
            {
                current_mode_on = true;
                if (on_switch != null && off_switch != null)
                {
                    on_switch.SetActive(true);
                    off_switch.SetActive(false);
                    _battle.iconExplaMode = true;
                    _guillotine.iconExplaMode = true;
                    _coin.iconExplaMode = true;
                }
            }
        }

    }

    // マウスがオブジェクトに入った際の処理
    void OnMouseEnter()
    {
        if (frame != null)
        {
            frame.SetActive(true);
        }
    }

    // マウスがオブジェクトから離れた際の処理
    void OnMouseExit()
    {
        if (frame != null)
        {
            frame.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
