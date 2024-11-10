using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guillotine : MonoBehaviour
{
    public bool iconExplaMode = true;

    private GameObject instance;　// 説明文を生成するためのゲームオブジェクト
    public Transform this_position;　// このアイコンの位置を取る
    private Vector3 position; // ギロチンの説明文の位置
    public GameObject explanation; // ギロチンの説明文のPrefab
    public AudioSource AudioSourceGuillotine; // サウンドを再生するためのAudioSource
    public Vector3 offset = new Vector3(-3.2f, 0.3f, -0.7f);
    public AudioClip blade; // サウンド

    // Start is called before the first frame update
    void Start()
    {
        position = this_position.position + offset; // このアイコンの説明文の位置はギロチンの位置＋Vector3 offset
    }

    // マウスがキャラクターの上に入った時の処理
    void OnMouseEnter()
    {
        Debug.Log("guillotine");
        if (iconExplaMode)
        {
            instance = Instantiate(explanation, position, Quaternion.identity);
            AudioSourceGuillotine.PlayOneShot(blade);
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
                Debug.Log("instance_guillotine destroyed");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
