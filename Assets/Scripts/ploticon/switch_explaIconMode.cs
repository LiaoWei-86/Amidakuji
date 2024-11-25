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
    public GameObject beer;
    public GameObject gift;
    public GameObject vomit;

    public battleScript _battle;
    public coinScript _coin;
    public guillotineScript _guillotine;
    public beerScript _beer;
    public giftScript _gift;
    public vomitScript _vomit;

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
            _battle = FindObjectOfType<battleScript>();
        }

        if (_coin == null)
        {
            _coin = FindObjectOfType<coinScript>();
        }

        if (_guillotine == null)
        {
            _guillotine = FindObjectOfType<guillotineScript>();
        }

        if (_beer == null)
        {
            _beer = FindObjectOfType<beerScript>();
        }
        if (_gift == null)
        {
            _gift = FindObjectOfType<giftScript>();
        }
        if (_vomit == null)
        {
            _vomit = FindObjectOfType<vomitScript>();
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
                    if (battle != null)
                    {
                        _battle.iconExplaMode = false;
                    }
                    if (guillotine != null)
                    {
                        _guillotine.iconExplaMode = false;
                    }

                    if (coin != null)
                    {
                        _coin.iconExplaMode = false;
                    }
                    if (beer != null)
                    {
                        _beer.iconExplaMode = false;
                    }
                    if (gift != null)
                    {
                        _gift.iconExplaMode = false;
                    }
                    if (vomit != null)
                    {
                        _vomit.iconExplaMode = false;
                    }
                }
            }
            else if (!current_mode_on)
            {
                current_mode_on = true;
                if (on_switch != null && off_switch != null)
                {
                    on_switch.SetActive(true);
                    off_switch.SetActive(false);
                    if (battle != null)
                    {
                        _battle.iconExplaMode = true;
                    }
                    if (guillotine != null)
                    {
                        _guillotine.iconExplaMode = true;
                    }
                    if (coin != null)
                    {
                        _coin.iconExplaMode = true;
                    }
                    if (beer != null)
                    {
                        _beer.iconExplaMode = true;
                    }
                    if (gift != null)
                    {
                        _gift.iconExplaMode = true;
                    }
                    if (vomit != null)
                    {
                        _vomit.iconExplaMode = true;
                    }
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
