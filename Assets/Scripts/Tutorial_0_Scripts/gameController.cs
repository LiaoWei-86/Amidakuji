using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
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
    private bool scM_pd_halfClick = false;

    public GameObject thirdMessage; // ゲームオブジェクト thirdMessage（開始メッセージ1）
    public PlayableDirector thirdMessagePlayableDirector; // thirdMessageのPlayableDirector
    private bool hasThirdMessagePlayed = false; //  thirdMessageは再生完了されたか？のブール値;まだ再生完了してない
    private bool thdM_pd_halfClick = false;

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

    //public GameObject knight;
    //public GameObject hunter; //  元々は猟師だけど、国王に変えた。スクリプト上は名前を変更するのややこしいのでこのまま保留した。実は国王だ
    //public GameObject knightPrefab;
    //public GameObject hunterPrefab;

    public GameObject character1;
    public GameObject character2;

    public Vector3 offset= new Vector3(0,-2,0);
    public GameObject end1Prefab;
    public GameObject end2Prefab;

    public TMP_FontAsset dotFont;  // ドットフォント
    public bool charaKnightInfoChecked = false; //　騎士のキャラクター情報は確認されたか？のブール値
    public bool charaHunterInfoChecked = false; //　猟師のキャラクター情報は確認されたか？のブール値

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
        character2.SetActive(false);
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
        if (move_pd != null)
        {
            move_pd.stopped += OnPlayableDirectorStopped;
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
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("hasLineMessagePlayed:"+hasLineMessagePlayed);

        if (!hasFirstMessagePlayed && Input.GetMouseButtonDown(0)) //　FirstMessageは始まる時勝手に再生するから、まだ再生終わってない間クリックすると、timelineの最後に設置する
        {
            firstMessagePlayableDirector.time = firstMessagePlayableDirector.duration;
            firstMessagePlayableDirector.Evaluate();
        }

        if (Input.GetMouseButtonDown(0))  // Input.GetKeyDown(KeyCode.Return)
        {
            HandleEnterPress();
        }
    }

    void HandleEnterPress()
    {

        if (hasFirstMessagePlayed == true && !scM_pd_halfClick && !hasSecondMessagePlayed)
        {
            firstMessage.SetActive(false);
            secondMessage.SetActive(true);
            secondMessagePlayableDirector.Play();
            scM_pd_halfClick = true; //　secondMessage再生始める
        }
        else if (scM_pd_halfClick && !hasSecondMessagePlayed) //　secondMessage再生始めて、まだ終わってない間
        {
            scM_pd_halfClick = false; // secondMessage再生終わる
            secondMessagePlayableDirector.time = secondMessagePlayableDirector.duration;
            secondMessagePlayableDirector.Evaluate();
        }
        else if(hasSecondMessagePlayed == true && !scM_pd_halfClick && !thdM_pd_halfClick && !hasThirdMessagePlayed)
        {
            secondMessage.SetActive(false);
            thirdMessage.SetActive(true);
            thirdMessagePlayableDirector.Play();
            thdM_pd_halfClick = true;
        }
        else if(thdM_pd_halfClick && !hasThirdMessagePlayed)
        {
            thdM_pd_halfClick = false;
            thirdMessagePlayableDirector.time = thirdMessagePlayableDirector.duration;
            thirdMessagePlayableDirector.Evaluate();
        }
        else if (hasThirdMessagePlayed == true && !thdM_pd_halfClick)
        {
            thirdMessage.SetActive(false);
            SceneManager.LoadScene("Tutorial_NewVersion");
        }
        
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
            scM_pd_halfClick = false; // secondMessage再生終わる
            hasSecondMessagePlayed = true;

        }
        else if (director == thirdMessagePlayableDirector)
        {
            thdM_pd_halfClick = false;
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
