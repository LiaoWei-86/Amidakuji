using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noticeBoardScript : MonoBehaviour
{
    public bool iconExplaMode = true;

    private GameObject instance;　// 説明文を生成するためのゲームオブジェクト
    public Transform this_position;　// このアイコンの位置を取る
    private Vector3 position; //このアイコンの説明文の位置
    public GameObject explanation; // このアイコンの説明文のPrefab
    public Vector3 offset = new Vector3(2.5f, 0.3f, -0.7f);

    // Start is called before the first frame update
    void Start()
    {
        position = this_position.position + offset; // アイコンの説明文の位置はアイコンの位置＋offset
    }

    // マウスがキャラクターの上に入った時の処理
    void OnMouseEnter()
    {
        Debug.Log("noticeBoard");
        if (iconExplaMode)
        {
            instance = Instantiate(explanation, position, Quaternion.identity);

        }
    }

    // マウスがキャラクターから離れたときの処理
    private void OnMouseExit()
    {
        if (iconExplaMode)
        {
            if (instance != null)
            {
                Destroy(instance);
                Debug.Log("instance_noticeBoard destroyed");
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
