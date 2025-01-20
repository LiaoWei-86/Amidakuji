using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_explaIconMode : MonoBehaviour
{
    public GameObject frame;
    public GameObject on_switch;
    public GameObject off_switch;

    public bool mouseOn = false;

    public GameObject guillotine;
    public GameObject coin;
    public GameObject battle;
    public GameObject beer;
    public GameObject gift;
    public GameObject vomit;
    public GameObject noMoney;
    public GameObject fallDown;
    public GameObject hanasu;
    public GameObject wrench;
    public GameObject noticeBoard;

    public battleScript _battle;
    public coinScript _coin;
    public guillotineScript _guillotine;
    public beerScript _beer;
    public giftScript _gift;
    public vomitScript _vomit;
    public noMoneyScript _noMoney;
    public fallDownScript _fallDown;
    public hanasuScript _hanasu;
    public wrenchScript _wrench;
    public noticeBoardScript _noticeBoard;

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
        if (_noMoney == null)
        {
            _noMoney = FindObjectOfType<noMoneyScript>();
        }
        if (_fallDown == null)
        {
            _fallDown = FindObjectOfType<fallDownScript>();
        }
        if (_hanasu == null)
        {
            _hanasu = FindObjectOfType<hanasuScript>();
        }
        if (_wrench == null)
        {
            _wrench = FindObjectOfType<wrenchScript>();
        }

        if (_noticeBoard == null)
        {
            _noticeBoard = FindObjectOfType<noticeBoardScript>();
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
                    if (noMoney != null)
                    {
                        _noMoney.iconExplaMode = false;
                    }
                    if (fallDown != null)
                    {
                        _fallDown.iconExplaMode = false;
                    }
                    if (hanasu != null)
                    {
                        _hanasu.iconExplaMode = false;
                    }
                    if (_wrench != null)
                    {
                        _wrench.iconExplaMode = false;
                    }
                    if (_noticeBoard != null)
                    {
                        _noticeBoard.iconExplaMode = false;
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
                    if (noMoney != null)
                    {
                        _noMoney.iconExplaMode = true;
                    }
                    if (fallDown != null)
                    {
                        _fallDown.iconExplaMode = true;
                    }
                    if (hanasu != null)
                    {
                        _hanasu.iconExplaMode = true;
                    }
                    if (_wrench != null)
                    {
                        _wrench.iconExplaMode = true;
                    }
                    if (_noticeBoard != null)
                    {
                        _noticeBoard.iconExplaMode = true;
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
        mouseOn = true;

    }

    // マウスがオブジェクトから離れた際の処理
    void OnMouseExit()
    {
        if (frame != null)
        {
            frame.SetActive(false);
        }
        mouseOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
