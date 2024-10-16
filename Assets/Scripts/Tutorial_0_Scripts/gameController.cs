﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class gameController : MonoBehaviour
{
    public GameObject firstMessage; // ゲームオブジェクト firstMessage（開始メッセージ1）
    public PlayableDirector firstMessagePlayableDirector; // firstMessageのPlayableDirector
    private bool hasFirstMessagePlayed = false; //  firstMessageは再生完了されたか？のブール値;まだ再生完了してない

    public GameObject secondMessage; // ゲームオブジェクト secondMessage（開始メッセージ1）
    public PlayableDirector secondMessagePlayableDirector; // secondMessageのPlayableDirector
    private bool hasSecondMessagePlayed = false; //  secondMessageは再生完了されたか？のブール値;まだ再生完了してない

    public GameObject thirdMessage; // ゲームオブジェクト thirdMessage（開始メッセージ1）
    public PlayableDirector thirdMessagePlayableDirector; // thirdMessageのPlayableDirector
    private bool hasThirdMessagePlayed = false; //  thirdMessageは再生完了されたか？のブール値;まだ再生完了してない

    public GameObject charaMessage; // ゲームオブジェクト charaMessage（開始メッセージ1）
    public PlayableDirector charaMessagePlayableDirector; // charaMessageのPlayableDirector
    private bool hasCharaMessagePlayed = false; //  charaMessageは再生完了されたか？のブール値;まだ再生完了してない

    public GameObject endMessage; // ゲームオブジェクト endMessage（開始メッセージ1）
    public PlayableDirector endMessagePlayableDirector; // endMessageのPlayableDirector
    private bool hasEndMessagePlayed = false; //  endMessageは再生完了されたか？のブール値;まだ再生完了してない

    public GameObject lineMessage; // ゲームオブジェクト lineMessage（開始メッセージ1）
    public PlayableDirector lineMessagePlayableDirector; // lineMessageのPlayableDirector
    private bool hasLineMessagePlayed = false; //  lineMessageは再生完了されたか？のブール値;まだ再生完了してない

    public GameObject pointMessage; // ゲームオブジェクト pointMessage（開始メッセージ1）
    public PlayableDirector pointMessagePlayableDirector; // pointMessageのPlayableDirector
    private bool hasPointMessagePlayed = false; //  pointMessageは再生完了されたか？のブール値;まだ再生完了してない

    public GameObject yoko_lineMessage; // ゲームオブジェクト yoko_lineMessage（開始メッセージ1）
    public PlayableDirector yoko_lineMessagePlayableDirector; // yoko_lineMessageのPlayableDirector
    private bool hasYoko_LineMessagePlayed = false; //  yoko_lineMessageは再生完了されたか？のブール値;まだ再生完了してない

    public GameObject knight;
    public GameObject hunter; //  元々は猟師だけど、国王に変えた。スクリプト上は名前を変更するのややこしいのでこのまま保留した。実は国王だ
    public GameObject knightPrefab;
    public GameObject hunterPrefab;

    public Vector3 offset= new Vector3(0,-2,0);
    public GameObject end1Prefab;
    public GameObject end2Prefab;

    public TMP_FontAsset dotFont;  // ドットフォント
    public bool charaKnightInfoChecked = false; //　騎士のキャラクター情報は確認されたか？のブール値
    public bool charaHunterInfoChecked = false; //　猟師のキャラクター情報は確認されたか？のブール値

    public GameObject charaInfoPrefab;

    public characterInfoHoverT0 characterInfoHoverT0Script;

    // Start is called before the first frame update
    void Start()
    {
        if (characterInfoHoverT0Script == null)
        {
            characterInfoHoverT0Script = FindObjectOfType<characterInfoHoverT0>();
        }

        //  開始時にsecondMessageを非表示にする
        if (secondMessage != null)
        {
            secondMessage.SetActive(false);
        }
        //  開始時にthirdMessageを非表示にする
        if (thirdMessage != null)
        {
            thirdMessage.SetActive(false);
        }
        if (charaMessage != null)
        {
            charaMessage.SetActive(false);
        }
        if (lineMessage != null)
        {
            lineMessage.SetActive(false);
        }
        if (pointMessage != null)
        {
            pointMessage.SetActive(false);
        }
        if (yoko_lineMessage != null)
        {
            yoko_lineMessage.SetActive(false);
        }
        if (endMessage != null)
        {
            endMessage.SetActive(false);
        }

        // PlayableDirectorがnullでないことを確認し、再生完了イベントをサブスクライブ
        if (firstMessagePlayableDirector != null)
        {
            firstMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (secondMessagePlayableDirector != null)
        {
            secondMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (thirdMessagePlayableDirector != null)
        {
            thirdMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (charaMessagePlayableDirector != null)
        {
            charaMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (pointMessagePlayableDirector != null)
        {
            pointMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (lineMessagePlayableDirector != null)
        {
            lineMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (yoko_lineMessagePlayableDirector != null)
        {
            yoko_lineMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (endMessagePlayableDirector != null)
        {
            endMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }

        CreateHoverAreaCharacter(knight, 1);
        CreateHoverAreaCharacter(hunter, 3);// ３だから、実は国王
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("hasLineMessagePlayed:"+hasLineMessagePlayed);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            HandleEnterPress();
        }
    }

    void HandleEnterPress()
    {
        if (hasFirstMessagePlayed == true && !hasSecondMessagePlayed)
        {
            firstMessage.SetActive(false);
            secondMessage.SetActive(true);
            secondMessagePlayableDirector.Play();
        }
        else if(hasSecondMessagePlayed == true && !hasThirdMessagePlayed)
        {
            secondMessage.SetActive(false);
            thirdMessage.SetActive(true);
            thirdMessagePlayableDirector.Play();
        }
        else if (hasThirdMessagePlayed == true && !hasCharaMessagePlayed)
        {
            thirdMessage.SetActive(false);
            knight = Instantiate(knightPrefab, knight.transform.position, Quaternion.identity);
            hunter = Instantiate(hunterPrefab, hunter.transform.position, Quaternion.identity);
            charaMessage.SetActive(true);
            charaMessagePlayableDirector.Play();
        }
        else if (hasCharaMessagePlayed == true && !hasEndMessagePlayed)
        {
            GameObject end1 = Instantiate(end1Prefab, (knight.transform.position + offset), Quaternion.identity);
            GameObject end2 = Instantiate(end2Prefab, (hunter.transform.position + offset), Quaternion.identity);
            endMessage.SetActive(true);
            endMessagePlayableDirector.Play();
        }
        else if (hasEndMessagePlayed == true && !hasLineMessagePlayed)
        {
            lineMessage.SetActive(true);
            lineMessagePlayableDirector.Play();
        }
        else if(hasLineMessagePlayed == true && !hasPointMessagePlayed)
        {
            pointMessage.SetActive(true);
            pointMessagePlayableDirector.Play();
        }
        else if (hasPointMessagePlayed == true && !hasYoko_LineMessagePlayed)
        {
            yoko_lineMessage.SetActive(true);
            yoko_lineMessagePlayableDirector.Play();
        }
        else if (hasYoko_LineMessagePlayed == true )
        {
            SceneManager.LoadScene("Tutorial_1_Scene");
        }
    }

    void CreateHoverAreaCharacter(GameObject character, int charaInfoNum)
    {
        // デバッグログを出力して、キャラクターの情報を表示する
        Debug.Log($"CreateHoverArea called with character: {character.name}");

        // キャラクターの位置からオフセットを加えた位置を計算する
        Vector3 charaInfoPosition = character.transform.position + new Vector3(-3, -1, 0);

        // キャラクターに BoxCollider コンポーネントを追加し、ホバーエリアのサイズを設定する
        BoxCollider boxCollider = character.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(1f, 1f, 0.1f);
        boxCollider.isTrigger = true;

        // characterInfoHoverT0 スクリプトをキャラクターに追加する
        characterInfoHoverT0 characterInfoHoverT0Script= character.AddComponent<characterInfoHoverT0>();

        // characterInfoHoverT5 スクリプトの情報番号を設定する
        characterInfoHoverT0Script.charaInfoNum = charaInfoNum;

        // characterInfoHoverT5 スクリプトを初期化する
        characterInfoHoverT0Script.Initialize(character, charaInfoPrefab, charaInfoPosition);
    }
    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        Debug.Log("PlayableDirector Stopped: " + director.name);

        if (director == firstMessagePlayableDirector)
        {
            hasFirstMessagePlayed = true;

        }
        else if (director == secondMessagePlayableDirector)
        {
            hasSecondMessagePlayed = true;


        }
        else if (director == thirdMessagePlayableDirector)
        {
            hasThirdMessagePlayed = true;

        }
        else if (director == charaMessagePlayableDirector)
        {
            hasCharaMessagePlayed = true;
        }
        else if (director == lineMessagePlayableDirector)
        {
            hasLineMessagePlayed = true;
        }
        else if (director == pointMessagePlayableDirector)
        {
            hasPointMessagePlayed = true;
        }
        else if (director == yoko_lineMessagePlayableDirector)
        {
            hasYoko_LineMessagePlayed = true;
        }
        else if (director == endMessagePlayableDirector)
        {
            hasEndMessagePlayed = true;
        }
    }

    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        if (firstMessagePlayableDirector != null)
        {
            firstMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (secondMessagePlayableDirector != null)
        {
            secondMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (thirdMessagePlayableDirector != null)
        {
            thirdMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (charaMessagePlayableDirector != null)
        {
            charaMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (lineMessagePlayableDirector != null)
        {
            lineMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (pointMessagePlayableDirector != null)
        {
            pointMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (yoko_lineMessagePlayableDirector != null)
        {
            yoko_lineMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (endMessagePlayableDirector != null)
        {
            endMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }

}
