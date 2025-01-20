using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrenchScript : MonoBehaviour
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

    private Coroutine animationCoroutine; // アニメーションのCoroutineをコントロールするため
    private bool isMouseOver = false; // カーソルはここにあるかどうか

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.speed = 0; // 一時停止

        position = this_position.position + offset; // アイコンの説明文の位置はアイコンの位置＋offset
    }

    // マウスがキャラクターの上に入った時の処理
    void OnMouseEnter()
    {
        Debug.Log("wrench");
        if (iconExplaMode)
        {
            isMouseOver = true;

            instance = Instantiate(explanation, position, Quaternion.identity);

            // リセットとプレイ
            animator.Play("wrenchAnimation", 0, 0); // アニメーションをリセットする
            animator.speed = 1; // アニメーションを再生する

            // サウンドを再生する
            AudioSource.Stop(); // 最初から再生することを確保する
            AudioSource.clip = sound;
            AudioSource.Play();

            // Coroutineでアニメーションの自動停止を予約する
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine); // 前のCoroutineを停止する
            }
            animationCoroutine = StartCoroutine(StopAnimationAfterTime(6.0f));
        }

    }

    // マウスがキャラクターから離れたときの処理
    void OnMouseExit()
    {
        if (iconExplaMode)
        {
            isMouseOver = false;

            if (instance != null)
            {
                Destroy(instance);
                Debug.Log("instance_wrench destroyed");
            }
            // アニメーションを停止し、リセットする
            ResetAnimation();

            // サウンドの再生を停止する
            AudioSource.Stop();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator StopAnimationAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // カーソルはまだここにいるかを確認する
        if (!isMouseOver)
        {
            yield break; // カーソルが離れた場合は動画の停止をやめる
        }

        // アニメーションを停止しリセットする
        ResetAnimation();

        // サウンドの停止
        AudioSource.Stop();
    }

    private void ResetAnimation()
    {
        animator.Play("wrenchAnimation", 0, 0); // アニメーションのリセット
        animator.speed = 0; // アニメーションを停止する
    }
}
