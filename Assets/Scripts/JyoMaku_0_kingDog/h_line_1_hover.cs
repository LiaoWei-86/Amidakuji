using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h_line_1_hover : MonoBehaviour
{
    public GameObject tipFrame;
    public JyoMaku_0_kingDog_gameController _0_KingDog_GameController;

    public GameObject line;

    public AudioSource audioSource;
    public AudioClip leftClick;
    public AudioClip rightClick;
    public AudioClip missClip;

    // Start is called before the first frame update
    void Start()
    {
        tipFrame.SetActive(false);
        line.SetActive(false);
    }

    private void OnMouseEnter()
    {
        Debug.Log("これは上の横線のところだよ");
        tipFrame.SetActive(true);
        if (_0_KingDog_GameController.game_started)
        {
            _0_KingDog_GameController.currentGameMode = JyoMaku_0_kingDog_gameController.GameMode.Choosing;
        }
    }

    private void OnMouseExit()
    {
        Debug.Log("上の横線のところから離れたよ");
        tipFrame.SetActive(false);
        if (_0_KingDog_GameController.game_started)
        {
            _0_KingDog_GameController.currentGameMode = JyoMaku_0_kingDog_gameController.GameMode.PlayerPlaying;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (_0_KingDog_GameController.is_horizontal_line_1_connected)
            {
                audioSource.PlayOneShot(missClip);
            }
            else if (!_0_KingDog_GameController.is_horizontal_line_1_connected)
            {
                if (_0_KingDog_GameController.can_line_1_changed)
                {
                    createLine();
                }
                else if (!_0_KingDog_GameController.can_line_1_changed)
                {
                    audioSource.PlayOneShot(missClip);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (_0_KingDog_GameController.is_horizontal_line_1_connected)
            {
                if (_0_KingDog_GameController.can_line_1_changed)
                {
                    deleteLine();
                }
                else if (!_0_KingDog_GameController.can_line_1_changed)
                {
                    audioSource.PlayOneShot(missClip);
                }
            }
            else if (!_0_KingDog_GameController.is_horizontal_line_1_connected)
            {
                audioSource.PlayOneShot(missClip);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void createLine()
    {
        line.SetActive(true);
        audioSource.PlayOneShot(leftClick);
        _0_KingDog_GameController.is_horizontal_line_1_connected = true;
        Debug.Log($"上の横線は接続されているか？{_0_KingDog_GameController.is_horizontal_line_1_connected}");
    }

    private void deleteLine()
    {
        line.SetActive(false);
        audioSource.PlayOneShot(rightClick);
        _0_KingDog_GameController.is_horizontal_line_1_connected = false;
        Debug.Log($"上の横線は接続されているか？{_0_KingDog_GameController.is_horizontal_line_1_connected}");
    }
}
