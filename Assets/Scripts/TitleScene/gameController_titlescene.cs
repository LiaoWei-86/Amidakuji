using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class gameController_titlescene : MonoBehaviour
{
    public GameObject firstMessage; // ゲームオブジェクト firstMessage（開始メッセージ1）
    public PlayableDirector firstMessagePlayableDirector; // firstMessageのPlayableDirector
    private bool hasFirstMessagePlayed = false; //  firstMessageは再生完了されたか？のブール値;まだ再生完了してない

    public GameObject secondMessage; // ゲームオブジェクト secondMessage（開始メッセージ1）
    public PlayableDirector secondMessagePlayableDirector; // secondMessageのPlayableDirector
    private bool hasSecondMessagePlayed = false; //  secondMessageは再生完了されたか？のブール値;まだ再生完了してない



    //public GameObject knight;
    //public GameObject hunter; //  元々は猟師だけど、国王に変えた。スクリプト上は名前を変更するのややこしいのでこのまま保留した。実は国王だ
    //public GameObject knightPrefab;
    //public GameObject hunterPrefab;

    public GameObject character1;
  

    public Vector3 offset= new Vector3(-6,2.2,-0.7);
    public GameObject end1Prefab;
    public GameObject end2Prefab;

    public TMP_FontAsset dotFont;  // ドットフォント
    public bool charaKnightInfoChecked = false; //　騎士のキャラクター情報は確認されたか？のブール値
   

    public GameObject charaInfoPrefab;

    public GameObject end;

    public GameObject move;
    public PlayableDirector move_pd;

    private bool canToNext = false;

    private bool hasMovePlayed = false;

    public characterInfoHoverT0 characterInfoHoverT0Script;

    // Start is called before the first frame update
    void Start()
    {
        character1.SetActive(false);
        move.SetActive(false);
        end.SetActive(false);

        if (characterInfoHoverT0Script == null)
        {
            characterInfoHoverT0Script = FindObjectOfType<characterInfoHoverT0>();
        }

        //  開始時にsecondMessageを非表示にする
        if (secondMessage != null)
        {
            secondMessage.SetActive(false);
        }

        // PlayableDirectorがnullでないことを確認し、再生完了イベントをサブスクライブ
        if (firstMessagePlayableDirector != null)
        {
            firstMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (move_pd != null)
        {
            move_pd.stopped += OnPlayableDirectorStopped;
        }
        if (secondMessagePlayableDirector != null)
        {
            secondMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
       

        CreateHoverAreaCharacter(character1, 1);
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
       
        else if (!hasSecondMessagePlayed)
        {
            GameObject end1 = Instantiate(end1Prefab, (character1.transform.position + offset), Quaternion.identity);
            GameObject end2 = Instantiate(end2Prefab, (character2.transform.position + offset), Quaternion.identity);
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
        else if (hasYoko_LineMessagePlayed == true && !hasMovePlayed)
        {
            move.SetActive(true);
            
        }
        else if (hasMovePlayed && !canToNext)
        {
            end.SetActive(true);
            canToNext = true;
        }
        else if (canToNext)
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
        else if (director == move_pd)
        {
            hasMovePlayed = true;
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

        move_pd.stopped -= OnPlayableDirectorStopped;
    }

}
