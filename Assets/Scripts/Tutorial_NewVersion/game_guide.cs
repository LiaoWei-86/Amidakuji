using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_guide : MonoBehaviour
{
    public T_new_gameController t_New_script;
    public GameObject on_image;
    public GameObject off_image;
    public GameObject tip_frame;

    public bool current_guide_on = false;

    // Start is called before the first frame update
    void Start()
    {
        if (t_New_script == null)
        {
            t_New_script = FindObjectOfType<T_new_gameController>();
        }
        on_image.SetActive(false);
        tip_frame.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリックしたら...
        {
            if (current_guide_on) // オンだったら、オフにする
            {
                current_guide_on = false;
                off_image.SetActive(true);
                on_image.SetActive(false);
                if (t_New_script.expla_createLine != null)
                {
                    t_New_script.expla_createLine.SetActive(false);
                }
                if (t_New_script.createLine_show_point_2 != null)
                {
                    t_New_script.createLine_show_point_2.SetActive(false);
                }
            }
            else if (!current_guide_on) // オフだったら、オンにする
            {
                current_guide_on = true;
                off_image.SetActive(false);
                on_image.SetActive(true);
                t_New_script.expla_createLine.SetActive(true);
            }
        }
    }

    // マウスがオブジェクトに入った際の処理
    void OnMouseEnter()
    {
        if (tip_frame != null)
        {
            tip_frame.SetActive(true);
        }
    }

    // マウスがオブジェクトから離れた際の処理
    void OnMouseExit()
    {
        if (tip_frame != null)
        {
            tip_frame.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
