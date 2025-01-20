using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallDownScript : MonoBehaviour
{
    public bool iconExplaMode = true;

    private GameObject instance;　// 説明文を生成するためのゲームオブジェクト
    public Transform this_position;　// このアイコンの位置を取る
    private Vector3 position; //このアイコンの説明文の位置
    public GameObject explanation; // このアイコンの説明文のPrefab
    public AudioSource AudioSource; // サウンドを再生するためのAudioSource
    public Vector3 offset = new Vector3(-2.5f, 0.3f, -0.7f);

    public AudioClip sound;
    // Start is called before the first frame update
    void Start()
    {
        position = this_position.position + offset; // アイコンの説明文の位置はアイコンの位置＋offset
    }

    void OnMouseEnter()
    {
        Debug.Log("fallDown");
        if (iconExplaMode)
        {
            instance = Instantiate(explanation, position, Quaternion.identity);
            AudioSource.PlayOneShot(sound);
        }

    }

    // マウスがキャラクターから離れたときの処理
    void OnMouseExit()
    {
        if (iconExplaMode)
        {
            if (instance != null)
            {
                Destroy(instance);
                Debug.Log("instance_fallDown destroyed");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
