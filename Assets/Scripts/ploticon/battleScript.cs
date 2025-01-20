using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleScript : MonoBehaviour
{
    public bool iconExplaMode = true;

    private Animator animator;

    private GameObject instance;　// 説明文を生成するためのゲームオブジェクト
    public Transform this_position;　// このアイコンの位置を取る
    private Vector3 position; //このアイコンの説明文の位置
    public GameObject explanation; // このアイコンの説明文のPrefab
    public AudioSource AudioSource; // サウンドを再生するためのAudioSource
    public Vector3 offset = new Vector3(-2.5f, 0.3f, -0.7f);
    public AudioClip sound; // サウンド

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.speed = 0; // 一時停止

        position = this_position.position + offset; // アイコンの説明文の位置はギロチンの位置＋offset
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // マウスがキャラクターの上に入った時の処理
    void OnMouseEnter()
    {
        Debug.Log("battle");
        if (iconExplaMode)
        {
            instance = Instantiate(explanation, position, Quaternion.identity);
            animator.speed = 1; // 再生する
            StartCoroutine(StopAnimationAfterPlay());
            StartCoroutine(PlayTwice());
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
                Debug.Log("instance_coin destroyed");
            }
        }

    }

    IEnumerator PlayTwice()
    {
        yield return new WaitForSeconds(0.4f);
        // 1回目再生する
        AudioSource.PlayOneShot(sound);

        // サウンドが終わるまで待つ
        yield return new WaitForSeconds(0.8f);

        // 2回目再生する
        AudioSource.PlayOneShot(sound);
    }

    private IEnumerator StopAnimationAfterPlay()
    {
        // アニメーションが再生終わるのを待つ  
        yield return new WaitForSeconds(1.3f);

        // 一時停止
        animator.Play("battleAnimation", 0, 0); // リセットする
        animator.speed = 0;
    }
}
