using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class J0_gameController : MonoBehaviour
{
    public GameObject enterAlone;
    public GameObject game_start;
    public PlayableDirector game_start_pd;

    public Transform character1_initial_pos; // 移動する前にキャラクターの居場所
    public Transform character2_initial_pos; // 移動する前にキャラクターの居場所

    public GameObject Jyomaku_J0_Text; // ゲームオブジェクト Jyomaku_J0_Text
    public PlayableDirector Jyomaku_J0_TextPlayableDirector; // Jyomaku_J0_TextのPlayableDirector

    public GameObject target_J0_Text; // ゲームオブジェクト target_J0_Text　　　一つ目のゲーム目標
    public PlayableDirector target_J0_TextPlayableDirector; // target_J0_TextのPlayableDirector

    public GameObject targetCompletedText; // ゲームオブジェクト targetCompletedText　　一つ目のゲーム目標の完成アニメーション
    public PlayableDirector targetCompletedTextPlayableDirector; // targetCompletedTextのPlayableDirector

    public GameObject target_J0_2_Text; // ゲームオブジェクト 二つ目のゲーム目標
    public PlayableDirector target_J0_2_TextPlayableDirector; // 二つ目のゲーム目標のPlayableDirector
    private bool has_target_2_played = false;

    public GameObject targetCompletedText_2; // ゲームオブジェクト 二つ目のゲーム目標の完成アニメーション
    public PlayableDirector targetCompletedText_2_PlayableDirector; // 二つ目のゲーム目標の完成アニメーションのPlayableDirector

    public bool clear = false; // このステージをクリアできた?をfalseとマークする
    private bool end_1 = false;
    private bool end_2 = false;
    private bool end_3 = false;
    private bool end_4 = false;

    // 演出
    public GameObject dialogue_A;
    public PlayableDirector dialogue_A_pd;
    private bool has_dialogue_A_pd_started = false;
    private bool has_dialogue_A_pd_played = false;

    public GameObject dialogue_B1;
    public PlayableDirector dialogue_B1_pd;
    private bool has_dialogue_B1_pd_started = false;
    private bool has_dialogue_B1_pd_played = false;

    public GameObject dialogue_B2;
    public PlayableDirector dialogue_B2_pd;
    private bool has_dialogue_B2_pd_started = false;
    private bool has_dialogue_B2_pd_played = false;

    public GameObject dog_runAway;
    public PlayableDirector dog_runAway_pd;

    // キャラクターのセリフ

    public GameObject s_knight_a; // 
    public PlayableDirector s_knight_a_pd; // 
    private bool has_s_knight_a_started = false;
    private bool has_s_knight_a_played = false;

    public GameObject s_king_a; // 
    public PlayableDirector s_king_a_pd; // 
    private bool has_s_king_a_started = false;
    private bool has_s_king_a_played = false;

    public GameObject s_knight_b; //  
    public PlayableDirector s_knight_b_pd; // 
    private bool has_s_knight_b_started = false;
    private bool has_s_knight_b_played = false;

    public GameObject s_king_b; //  
    public PlayableDirector s_king_b_pd; // 
    private bool has_s_king_b_started = false;
    private bool has_s_king_b_played = false;

    public GameObject s_knight_c; //  
    public PlayableDirector s_knight_c_pd; // 
    private bool has_s_knight_c_started = false;
    private bool has_s_knight_c_played = false;

    public GameObject s_king_c; //  
    public PlayableDirector s_king_c_pd; // 
    private bool has_s_king_c_started = false;
    private bool has_s_king_c_played = false;

    public GameObject s_knight_d; //  
    public PlayableDirector s_knight_d_pd; // 
    private bool has_s_knight_d_started = false;
    private bool has_s_knight_d_played = false;

    public GameObject s_king_d; //  
    public PlayableDirector s_king_d_pd; // 
    private bool has_s_king_d_started = false;
    private bool has_s_king_d_played = false;

    public GameObject s_knight_e; //  
    public PlayableDirector s_knight_e_pd; // 
    private bool has_s_knight_e_started = false;
    private bool has_s_knight_e_played = false;

    public GameObject s_king_e; //  
    public PlayableDirector s_king_e_pd; // 
    private bool has_s_king_e_started = false;
    private bool has_s_king_e_played = false;

    public GameObject s_knight_gift_a; // プレゼントアイコンですれ違う前に（全ルート共通）
    public PlayableDirector s_knight_gift_a_pd; // 
    private bool has_s_knight_gift_a_started = false;
    private bool has_s_knight_gift_a_played = false;

    public GameObject s_king_gift_a; // プレゼントアイコンですれ違う前に（全ルート共通）
    public PlayableDirector s_king_gift_a_pd; // 
    private bool has_s_king_gift_a_started = false;
    private bool has_s_king_gift_a_played = false;

    public GameObject s_knight_gift_b; // プレゼントアイコンですれ違う後に（ルート２と４）
    public PlayableDirector s_knight_gift_b_pd; // 
    private bool has_s_knight_gift_b_started = false;
    private bool has_s_knight_gift_b_played = false;

    public GameObject s_king_gift_b; // プレゼントアイコンですれ違う後に（ルート２と４）
    public PlayableDirector s_king_gift_b_pd; // 
    private bool has_s_king_gift_b_started = false;
    private bool has_s_king_gift_b_played = false;

    public GameObject s_knight_1_money; // ルート1の場合：お金アイコンに止まった時
    public PlayableDirector s_knight_1_money_pd; // 
    private bool has_s_knight_1_money_started = false;
    private bool has_s_knight_1_money_played = false;

    public GameObject s_king_1_money; // ルート1の場合：お金アイコンに止まった時
    public PlayableDirector s_king_1_money_pd; 
    private bool has_s_king_1_money_started = false;
    private bool has_s_king_1_money_played = false;
 

    public GameObject s_knight_2_money; // ルート2の場合：お金アイコンに止まった時
    public PlayableDirector s_knight_2_money_pd;
    private bool has_s_knight_2_money_started = false;
    private bool has_s_knight_2_money_played = false;

    public GameObject s_king_2_money; // ルート2の場合：お金アイコンに止まった時
    public PlayableDirector s_king_2_money_pd;
    private bool has_s_king_2_money_started = false;
    private bool has_s_king_2_money_played = false;

    public GameObject s_knight_3_money_a; // ルート3の場合：お金アイコンですれ違う前に
    public PlayableDirector s_knight_3_money_a_pd;
    private bool has_s_knight_3_money_a_started = false;
    private bool has_s_knight_3_money_a_played = false;

    public GameObject s_king_3_money_a; // ルート3の場合：お金アイコンですれ違う前に
    public PlayableDirector s_king_3_money_a_pd;
    private bool has_s_king_3_money_a_started = false;
    private bool has_s_king_3_money_a_played = false;

    public GameObject s_knight_3_money_b; // ルート3の場合：お金アイコンですれ違う後に
    public PlayableDirector s_knight_3_money_b_pd;
    private bool has_s_knight_3_money_b_started = false;
    private bool has_s_knight_3_money_b_played = false;

    public GameObject s_king_3_money_b; // ルート3の場合：お金アイコンですれ違う後に
    public PlayableDirector s_king_3_money_b_pd; // 
    private bool has_s_king_3_money_b_started = false;
    private bool has_s_king_3_money_b_played = false;

    public GameObject s_knight_4_money_a; // ルート4の場合：お金アイコンですれ違う前に
    public PlayableDirector s_knight_4_money_a_pd; // 
    private bool has_s_knight_4_money_a_started = false;
    private bool has_s_knight_4_money_a_played = false;

    public GameObject s_king_4_money_a; // ルート4の場合：お金アイコンですれ違う前に
    public PlayableDirector s_king_4_money_a_pd;
    private bool has_s_king_4_money_a_started = false;
    private bool has_s_king_4_money_a_played = false;

    public GameObject s_knight_4_money_b; // ルート4の場合：お金アイコンですれ違う後に
    public PlayableDirector s_knight_4_money_b_pd;
    private bool has_s_knight_4_money_b_started = false;
    private bool has_s_knight_4_money_b_played = false;

    public GameObject s_king_4_money_b; // ルート4の場合：お金アイコンですれ違う後に
    public PlayableDirector s_king_4_money_b_pd;
    private bool has_s_king_4_money_b_started = false;
    private bool has_s_king_4_money_b_played = false;

    public GameObject dog; // 犬
    public Transform dog_pos;

    public SpriteRenderer spriteRenderer; // 犬のリードのSpriteRenderer

    public Transform startpoint_character1; // 左から1番目のスタート点の位置
    public Transform startpoint_character2; // 左から2番目のスタート点の位置
    public GameObject[] tenGameObjects; // それぞれの点
    public Transform[] tenPositions; // それぞれの点の位置
    public Transform endtpoint_character1; //左から1番目の終点の位置
    public Transform endpoint_character2; // 左から2番目の終点の位置
    public Dictionary<int, Vector3> pointsDictionary;// 点とその位置情報を格納する辞書

    public bool isHorizontal_1_LineCreated = false; //　1本目の横線は描かれたか？のブール値
    public bool isHorizontal_2_LineCreated = false; //　2本目の横線は描かれたか？のブール値

    [SerializeField] private float frameRate = 60.0f;

    // 今までは、理解しやすいために、スクリプト上ずっと「knight」「hunter」で名付けてた。
    // 調整しにくいので、これからは「character1」「character2」にする
    public GameObject character1; // 左から1番目のキャラクター
    public GameObject character2; // 左から2番目のキャラクター

    private Coroutine character1MovementCoroutine; // 左から1番目のキャラクターの移動
    private Coroutine character2MovementCoroutine; // 左から2番目のキャラクターの移動

    public bool hasMovementFinshed = false; //　キャラクターの移動は完成されたか？のブール値
    private int currentMovementIndex = 0; // プレイヤーがEnterを押す際にプロットアイコンを生成するために計算用のIndex

    public float speed = 3.0f;

    public GameObject tooltipPrefab; // 提示枠のプレハブ
    public Material horizontalLineMaterial; // 横線のマテリアル
    public float horizontalLineWidth = 0.05f; // 横線の幅
    public float hoverAreaWidth = 0.8f; // 横線のホバーエリアの縦幅
    public TMP_FontAsset dotFont;  // ドットフォント
    public GameObject charaInfoPrefab;

    public j0_menu _j0_menu_script;

    public GameObject newCharaAnounce; // 新しいキャラクターが登場したという通知
    public PlayableDirector newCharaAnPlayableDirector; // 新しいキャラクターが登場したという通知 PlayableDirector
    private bool has_newC_started = false;
    private bool has_newC_2_started = false;
    private bool has_newC_3_started = false;
    private bool has_newC_1_played = false;
    private bool has_newC_2_played = false;
    private bool has_newC_played = false;

    public bool canCreateLine_1 = true; // 今は上の横線をつなぐことができるか？をtrueとマークする
    public bool canCreateLine_2 = true; // 今は下の横線をつなぐことができるか？をtrueとマークする
    public bool canDeleteLine_1 = true; // 今は上の横線を削除することができるか？をtrueとマークする
    public bool canDeleteLine_2 = true; // 今は下の横線を削除することができるか？をtrueとマークする

    public bool has_cleared_once = false; // 1度クリアできた？をfalseとマークする

    public AudioClip leftClickClip;
    public AudioClip rightClickClip;
    public AudioClip missClip;

    public AudioSource audioSourceJ0;

    private bool gameStarted = false;

    public enum GameMode
    {
        TextPlaying, // ゲーム開始時のテキストが再生中
        PlayerPlaying, // プレイヤーが操作している状態
        WaitForSceneChange, // 現シーンのゲーム内容が終了し、プレイヤーがEnterを押すのを待って次のシーンに切り替える
        choosing // プレイヤーは横線を接続してるなどの時
    }
    public GameMode currentGameMode = GameMode.TextPlaying; // 現シーン開始時にゲームモードをStartTextPlayingに設定


    // Start is called before the first frame update
    void Start()
    {
        CreateHoverAreaCharacter(character1, 1); // 騎士：１
        CreateHoverAreaCharacter(character2, 3); // 国王：３

        //  開始時にを非表示にする
        enterAlone.SetActive(false);
        dog_runAway.SetActive(false);

        if (target_J0_Text != null)
        {
            target_J0_Text.SetActive(false);
        }
        if (targetCompletedText != null)
        {
            targetCompletedText.SetActive(false);
        }
        if (target_J0_2_Text != null)
        {
            target_J0_2_Text.SetActive(false);
        }
        if (targetCompletedText_2 != null)
        {
            targetCompletedText_2.SetActive(false);
        }

        if (s_knight_a != null)
        {
            s_knight_a.SetActive(false);
        }
        if ( s_king_a != null)
        {
            s_king_a.SetActive(false);
        }
        if ( s_knight_b != null)
        {
            s_knight_b.SetActive(false);
        }
        if ( s_king_b != null)
        {
            s_king_b.SetActive(false);
        }
        if ( s_knight_c != null)
        {
            s_knight_c.SetActive(false);
        }
        if (s_king_c != null)
        {
            s_king_c.SetActive(false);
        }
        if (s_knight_d != null)
        {
            s_knight_d.SetActive(false);
        }
        if (s_king_d != null)
        {
            s_king_d.SetActive(false);
        }
        if (s_knight_e != null)
        {
            s_knight_e.SetActive(false);
        }
        if (s_king_e != null)
        {
            s_king_e.SetActive(false);
        }
        s_knight_gift_a.SetActive(false);
        s_king_gift_a.SetActive(false);
        s_knight_gift_b.SetActive(false);
        s_king_gift_b.SetActive(false);
        s_knight_1_money.SetActive(false);
        s_king_1_money.SetActive(false);
        s_knight_2_money.SetActive(false);
        s_king_2_money.SetActive(false);
        s_knight_3_money_a.SetActive(false);
        s_king_3_money_a.SetActive(false);
        s_knight_3_money_b.SetActive(false);
        s_king_3_money_b.SetActive(false);
        s_knight_4_money_a.SetActive(false);
        s_king_4_money_a.SetActive(false);
        s_knight_4_money_b.SetActive(false);
        s_king_4_money_b.SetActive(false);
        newCharaAnounce.SetActive(false);
        game_start.SetActive(false);

        dialogue_A.SetActive(false);
        dialogue_B1.SetActive(false);
        dialogue_B2.SetActive(false);

        
        if (newCharaAnounce != null)
        {
            newCharaAnounce.SetActive(false);
        }

        // PlayableDirectorがnullでないことを確認し、再生完了イベントをサブスクライブ
        game_start_pd.stopped += OnPlayableDirectorStopped;
        if (Jyomaku_J0_TextPlayableDirector != null)
        {
            Jyomaku_J0_TextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (target_J0_TextPlayableDirector != null)
        {
            target_J0_TextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (targetCompletedTextPlayableDirector != null)
        {
            targetCompletedTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (target_J0_2_TextPlayableDirector != null)
        {
            target_J0_2_TextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (targetCompletedText_2_PlayableDirector != null)
        {
            targetCompletedText_2_PlayableDirector.stopped += OnPlayableDirectorStopped;
        }

        if (s_knight_a_pd != null)
        {
            s_knight_a_pd.stopped += OnPlayableDirectorStopped;
        }
        if (s_king_a_pd != null)
        {
            s_king_a_pd.stopped += OnPlayableDirectorStopped;
        }
        if (s_knight_b_pd != null)
        {
            s_knight_b_pd.stopped += OnPlayableDirectorStopped;
        }
        if (s_king_b_pd != null)
        {
            s_king_b_pd.stopped += OnPlayableDirectorStopped;
        }
        if (s_knight_c_pd != null)
        {
            s_knight_c_pd.stopped += OnPlayableDirectorStopped;
        }
        if (s_king_c_pd != null)
        {
            s_king_c_pd.stopped += OnPlayableDirectorStopped;
        }
        s_knight_d_pd.stopped += OnPlayableDirectorStopped;
        s_king_d_pd.stopped += OnPlayableDirectorStopped;
        s_knight_e_pd.stopped += OnPlayableDirectorStopped;
        s_king_e_pd.stopped += OnPlayableDirectorStopped;
        s_knight_gift_a_pd.stopped += OnPlayableDirectorStopped;
        s_king_gift_a_pd.stopped += OnPlayableDirectorStopped;
        s_knight_gift_b_pd.stopped += OnPlayableDirectorStopped;
        s_king_gift_b_pd.stopped += OnPlayableDirectorStopped;
        s_knight_1_money_pd.stopped += OnPlayableDirectorStopped;
        s_king_1_money_pd.stopped += OnPlayableDirectorStopped;
        s_knight_2_money_pd.stopped += OnPlayableDirectorStopped;
        s_king_2_money_pd.stopped += OnPlayableDirectorStopped;
        s_knight_3_money_a_pd.stopped += OnPlayableDirectorStopped;
        s_king_3_money_a_pd.stopped += OnPlayableDirectorStopped;
        s_knight_3_money_b_pd.stopped += OnPlayableDirectorStopped;
        s_king_3_money_b_pd.stopped += OnPlayableDirectorStopped;
        s_knight_4_money_a_pd.stopped += OnPlayableDirectorStopped;
        s_king_4_money_a_pd.stopped += OnPlayableDirectorStopped;
        s_knight_4_money_b_pd.stopped += OnPlayableDirectorStopped;
        s_king_4_money_b_pd.stopped += OnPlayableDirectorStopped;

        dialogue_A_pd.stopped += OnPlayableDirectorStopped;
        dialogue_B1_pd.stopped += OnPlayableDirectorStopped;
        dialogue_B2_pd.stopped += OnPlayableDirectorStopped;

        newCharaAnPlayableDirector.stopped += OnPlayableDirectorStopped;

        // Initialize the dictionary and add the points
        pointsDictionary = new Dictionary<int, Vector3>();

        // Add knight and hunter start and end points
        pointsDictionary.Add(0, startpoint_character1.position); // 左から1番目のスタート点
        pointsDictionary.Add(1, startpoint_character2.position); // 左から2番目のスタート点
        pointsDictionary.Add(12, endtpoint_character1.position);  // 左から1番目の終点
        pointsDictionary.Add(13, endpoint_character2.position);   // 左から2番目の終点

        // Add the positions of the other points (4 gameobjects)
        for (int i = 0; i < tenPositions.Length; i++)
        {
            pointsDictionary.Add(i + 2, tenPositions[i].position); // 点の位置（2から）
        }

        // すべての点の座標を表示する（デバッグ用）
        foreach (KeyValuePair<int, Vector3> point in pointsDictionary)
        {
            Debug.Log("Point " + point.Key + ": " + point.Value);
        }

        set_transparent(spriteRenderer, 0.2f);
    }
    public float FrameToTime(int frame)
    {
        if (frameRate <= 0)
        {
            Debug.LogError("frameRateはマイナスだったらダメ！");
            return 0f;
        }

        if (frame < 0)
        {
            Debug.LogError("int frameはマイナスだったらダメ！");
            return 0f;
        }

        return frame / frameRate;
    }

    private void pauseAni()
    {
        if (has_newC_started)
        {
            Debug.Log($"newCharaAnPlayableDirector.time:{newCharaAnPlayableDirector.time}");
            if (newCharaAnPlayableDirector.time >= FrameToTime(787) && !has_newC_1_played)
            {
                Debug.Log("stop newCharaAnPlayableDirector for 1st");
                newCharaAnPlayableDirector.Pause();
                has_newC_1_played = true;
            }
            else if (newCharaAnPlayableDirector.time >= FrameToTime(1883) && !has_newC_2_played)
            {
                Debug.Log("stop newCharaAnPlayableDirector for 2nd");
                newCharaAnPlayableDirector.Pause();
                has_newC_2_played = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        pauseAni();
        switch (currentGameMode)
        {
            case GameMode.TextPlaying:
                
                if (Input.GetMouseButtonDown(0))  // Input.GetKeyDown(KeyCode.Return)
                {
                    if(has_target_2_played && has_s_knight_a_started && !has_s_knight_a_played && !has_s_king_a_started && !has_s_king_a_played && !gameStarted)
                    {
                        s_knight_a_pd.time = s_knight_a_pd.duration;
                        s_knight_a_pd.Evaluate();
                    }
                    else if (!has_s_knight_a_started && has_s_knight_a_played && !has_s_king_a_started && !has_s_king_a_played && !gameStarted)
                    {
                        s_knight_a.SetActive(false);
                        s_king_a.SetActive(true);
                        has_s_king_a_started = true;
                    }
                    else if ( has_s_king_a_started && !has_s_king_a_played && !gameStarted)
                    {
                        s_king_a_pd.time = s_king_a_pd.duration;
                        s_king_a_pd.Evaluate();
                    }
                    else if (!has_s_king_a_started && has_s_king_a_played && !gameStarted)
                    {
                        s_king_a.SetActive(false);
                        CreateHoverAreaJ0(tenGameObjects[2], tenGameObjects[5]);
                        CreateHoverAreaJ0(tenGameObjects[6], tenGameObjects[9]);
                        game_start.SetActive(true);
                        gameStarted = true;
                    }

                    if (has_s_knight_b_started && !has_s_knight_b_played)
                    {
                        s_knight_b_pd.time = s_knight_b_pd.duration;
                        s_knight_b_pd.Evaluate();
                    }

                    if (has_s_knight_c_started && !has_s_knight_c_played)
                    {
                        s_knight_c_pd.time = s_knight_c_pd.duration;
                        s_knight_c_pd.Evaluate();
                    }

                    if (has_s_knight_d_started && !has_s_knight_d_played)
                    {
                        s_knight_d_pd.time = s_knight_d_pd.duration;
                        s_knight_d_pd.Evaluate();
                    }

                    if (has_s_knight_e_started && !has_s_knight_e_played)
                    {
                        s_knight_e_pd.time = s_knight_e_pd.duration;
                        s_knight_e_pd.Evaluate();
                    }

                    if (has_s_king_a_started && !has_s_king_a_played)
                    {
                        s_king_a_pd.time = s_king_a_pd.duration;
                        s_king_a_pd.Evaluate();
                    }
                    if (has_s_king_b_started && !has_s_king_b_played)
                    {
                        s_king_b_pd.time = s_king_b_pd.duration;
                        s_king_b_pd.Evaluate();
                    }
                    if (has_s_king_c_started && !has_s_king_c_played)
                    {
                        s_king_c_pd.time = s_king_c_pd.duration;
                        s_king_c_pd.Evaluate();
                    }
                    if (has_s_king_d_started && !has_s_king_d_played)
                    {
                        s_king_d_pd.time = s_king_d_pd.duration;
                        s_king_d_pd.Evaluate();
                    }
                    if (has_s_king_e_started && !has_s_king_e_played)
                    {
                        s_king_e_pd.time = s_king_e_pd.duration;
                        s_king_e_pd.Evaluate();
                    }
                    if (has_s_knight_1_money_started && !has_s_knight_1_money_played)
                    {
                        s_knight_1_money_pd.time = s_knight_1_money_pd.duration;
                        s_knight_1_money_pd.Evaluate();
                    }
                    if (has_s_knight_2_money_started && !has_s_knight_2_money_played)
                    {
                        s_knight_2_money_pd.time = s_knight_2_money_pd.duration;
                        s_knight_2_money_pd.Evaluate();
                    }
                    if (has_s_knight_3_money_a_started && !has_s_knight_3_money_a_played)
                    {
                        s_knight_3_money_a_pd.time = s_knight_3_money_a_pd.duration;
                        s_knight_3_money_a_pd.Evaluate();
                    }
                    if (has_s_knight_3_money_b_started && !has_s_knight_3_money_b_played)
                    {
                        s_knight_3_money_b_pd.time = s_knight_3_money_b_pd.duration;
                        s_knight_3_money_b_pd.Evaluate();
                    }
                    if (has_s_knight_4_money_a_started && !has_s_knight_4_money_a_played)
                    {
                        s_knight_4_money_a_pd.time = s_knight_4_money_a_pd.duration;
                        s_knight_4_money_a_pd.Evaluate();
                    }
                    if (has_s_knight_4_money_b_started && !has_s_knight_4_money_b_played)
                    {
                        s_knight_4_money_b_pd.time = s_knight_4_money_b_pd.duration;
                        s_knight_4_money_b_pd.Evaluate();
                    }
                    if (has_s_king_1_money_started && !has_s_king_1_money_played)
                    {
                        s_king_1_money_pd.time = s_king_1_money_pd.duration;
                        s_king_1_money_pd.Evaluate();
                    }
                    if (has_s_king_2_money_started && !has_s_king_2_money_played)
                    {
                        s_king_2_money_pd.time = s_king_2_money_pd.duration;
                        s_king_2_money_pd.Evaluate();
                    }
                    if (has_s_king_3_money_a_started && !has_s_king_3_money_a_played)
                    {
                        s_king_3_money_a_pd.time = s_king_3_money_a_pd.duration;
                        s_king_3_money_a_pd.Evaluate();
                    }
                    if (has_s_king_3_money_b_started && !has_s_king_3_money_b_played)
                    {
                        s_king_3_money_b_pd.time = s_king_3_money_b_pd.duration;
                        s_king_3_money_b_pd.Evaluate();
                    }
                    if (has_s_king_4_money_a_started && !has_s_king_4_money_a_played)
                    {
                        s_king_4_money_a_pd.time = s_king_4_money_a_pd.duration;
                        s_king_4_money_a_pd.Evaluate();
                    }
                    if (has_s_king_4_money_b_started && !has_s_king_4_money_b_played)
                    {
                        s_king_4_money_b_pd.time = s_king_4_money_b_pd.duration;
                        s_king_4_money_b_pd.Evaluate();
                    }
                    if (has_dialogue_A_pd_started && !has_dialogue_A_pd_played)
                    {
                        dialogue_A_pd.time = dialogue_A_pd.duration;
                        dialogue_A_pd.Evaluate();
                    }
                    if (has_dialogue_B1_pd_started && !has_dialogue_B1_pd_played)
                    {
                        dialogue_B1_pd.time = dialogue_B1_pd.duration;
                        dialogue_B1_pd.Evaluate();
                    }
                    if (has_dialogue_B2_pd_started && !has_dialogue_B2_pd_played)
                    {
                        dialogue_B2_pd.time = dialogue_B2_pd.duration;
                        dialogue_B2_pd.Evaluate();
                    }

                    if (has_s_knight_gift_a_started && !has_s_knight_gift_a_played)
                    {
                        s_knight_gift_a_pd.time = s_knight_gift_a_pd.duration;
                        s_knight_gift_a_pd.Evaluate();
                    }

                    if (has_s_knight_gift_b_started && !has_s_knight_gift_b_played)
                    {
                        s_knight_gift_b_pd.time = s_knight_gift_b_pd.duration;
                        s_knight_gift_b_pd.Evaluate();
                    }

                    if (has_s_king_gift_a_started && !has_s_king_gift_a_played)
                    {
                        s_king_gift_a_pd.time = s_king_gift_a_pd.duration;
                        s_king_gift_a_pd.Evaluate();
                    }

                    if (has_s_king_gift_b_started && !has_s_king_gift_b_played)
                    {
                        s_king_gift_b_pd.time = s_king_gift_b_pd.duration;
                        s_king_gift_b_pd.Evaluate();
                    }

                    if (has_newC_started && !has_newC_1_played && !has_newC_2_started && !has_newC_2_played && !has_newC_3_started && !has_newC_played)
                    {
                        newCharaAnPlayableDirector.time = FrameToTime(787);
                        newCharaAnPlayableDirector.Evaluate();
                    }
                    else if(has_newC_started && has_newC_1_played && !has_newC_2_started && !has_newC_2_played && !has_newC_3_started && !has_newC_played)
                    {
                        newCharaAnPlayableDirector.Play();
                        has_newC_2_started = true;
                    }
                    else if (has_newC_started && has_newC_1_played && has_newC_2_started && !has_newC_2_played && !has_newC_3_started && !has_newC_played)
                    {
                        newCharaAnPlayableDirector.time = FrameToTime(1883);
                        newCharaAnPlayableDirector.Evaluate();
                        has_newC_2_played = true;
                    }
                    else if (has_newC_started && has_newC_1_played && has_newC_2_started && has_newC_2_played && !has_newC_3_started && !has_newC_played)
                    {
                        newCharaAnPlayableDirector.Play();
                        has_newC_3_started = true;
                    }
                    else if (has_newC_started && has_newC_1_played && has_newC_2_started && has_newC_2_played && has_newC_3_started && !has_newC_played)
                    {
                        newCharaAnPlayableDirector.time = newCharaAnPlayableDirector.duration;
                        newCharaAnPlayableDirector.Evaluate();
                    }
                }
                break;

            case GameMode.PlayerPlaying:
                
                if (game_start.activeSelf)
                {
                    game_start.SetActive(false);
                }

                if (Input.GetMouseButtonDown(0))  // Input.GetKeyDown(KeyCode.Return)
                {

                    //  このモードでは、プレイヤーがEnterを押すと、キャラクターの移動＋プロットアイコンの生成＋ストーリーメッセージの生成を一つずつ表示される
                    charaMoveAndAnimationLogic();
                }


                    break;

            case GameMode.WaitForSceneChange:
                
                if (Input.GetMouseButtonDown(0))  // Input.GetKeyDown(KeyCode.Return)
                {
                    if (end_1 || end_3)
                    {
                        //Debug.Log("ステージJ0_badEndに進む！");
                        //SceneManager.LoadScene("J0_badEnd");
                        SceneManager.LoadScene("saikai");
                    }
                    else if (end_2 || end_4)
                    {
                        Debug.Log("ステージJyoMaku_0.5に進む！");
                        SceneManager.LoadScene("JyoMaku_0.5");
                    }
                }

                    break;

            case GameMode.choosing:

                break;
        }
  
        
    }

    public void charaMoveAndAnimationLogic()
    {
        if (!isHorizontal_1_LineCreated && !isHorizontal_2_LineCreated)
        {
            charaMoveRoute1();
        }
        else if (isHorizontal_1_LineCreated && !isHorizontal_2_LineCreated)
        {
            charaMoveRoute2();
        }
        else if (!isHorizontal_1_LineCreated && isHorizontal_2_LineCreated)
        {
            charaMoveRoute3();
        }
        else if (isHorizontal_1_LineCreated && isHorizontal_2_LineCreated)
        {
            charaMoveRoute4();
        }
    }
    /*
   　　　　    「騎士」0　　　　　   「国王」1
                   ||                   ||
                   ○2                  ○3
                   ||                   ||
                   ○4    ○5    ○6    ○7
                   ||                   ||
                   ○8    ○9    ○10    ○11
                   ||                   ||
　　　　　　   「結末」12　　　　     「結末」13
    */
    public void charaMoveRoute1()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });
                break;

            case 1:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 3, 3 });
                newCharaAnounce.SetActive(true);
                has_newC_started = true;
                currentGameMode = GameMode.TextPlaying;
                set_ItemA_as_B_child(dog, character1);

                break;

            case 2:
                newCharaAnounce.SetActive(false);
                enterAlone.SetActive(true);
                StartMovement(new List<int> { 2, 4 }, new List<int> { 3, 7 });
                break;

            case 3:
                canCreateLine_1 = false;
                canDeleteLine_1 = false;
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 4, 4 }, new List<int> { 7, 7 });
                s_knight_gift_a.SetActive(true);
                break;

            case 4:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 7, 7 });
                s_knight_gift_a.SetActive(false);
                s_king_gift_a.SetActive(true);
                break;

            case 5:
                s_king_gift_a.SetActive(false);
                StartMovement(new List<int> { 4, 8 }, new List<int> { 7, 11 });
                enterAlone.SetActive(true);
                break;

            case 6:
                set_transparent(spriteRenderer, 1.0f);
                enterAlone.SetActive(false);
                canCreateLine_2 = false;
                canDeleteLine_2 = false;
                StartMovement(new List<int> { 8, 8 }, new List<int> { 11, 11 });
                s_knight_1_money.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 11, 11 });
                dog_runAway.SetActive(true);
                dog.SetActive(false);
                s_knight_1_money.SetActive(false);
                enterAlone.SetActive(true);

                break;

            case 8:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 11, 11 });
                dog_runAway.SetActive(false);
                enterAlone.SetActive(false);
                s_king_1_money.SetActive(true);
                break;

            case 9:
                s_king_1_money.SetActive(false);
                set_transparent(spriteRenderer, 0.2f);
                StartMovement(new List<int> { 8, 12 }, new List<int> { 11, 11 });
                enterAlone.SetActive(true);
                break;

            case 10:
                s_knight_b.SetActive(true);
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 });
                enterAlone.SetActive(false);
                break;

            case 11:
                s_knight_b.SetActive(false);
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 13 });
                enterAlone.SetActive(true);
                break;

            case 12:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 });
                enterAlone.SetActive(false);
                s_king_b.SetActive(true);
                end_1 = true;
                break;
        }
    }

    public void charaMoveRoute2()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });
                enterAlone.SetActive(true);
                break;

            case 1:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 3, 3 });
                newCharaAnounce.SetActive(true);
                has_newC_started = true;
                set_ItemA_as_B_child(dog, character1);
                enterAlone.SetActive(false);
                currentGameMode = GameMode.TextPlaying;
                break;

            case 2:
                newCharaAnounce.SetActive(false);
                enterAlone.SetActive(true);
                StartMovement(new List<int> { 2, 4 }, new List<int> { 3, 7 });
                break;

            case 3:
                canCreateLine_1 = false;
                canDeleteLine_1 = false;
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 4, 4 }, new List<int> { 7, 7 });
                s_knight_gift_a.SetActive(true);
                has_s_king_gift_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 4:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 7, 7 });
                s_knight_gift_a.SetActive(false);
                s_king_gift_a.SetActive(true);
                has_s_king_gift_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 5:
                s_king_gift_a.SetActive(false);
                StartMovement(new List<int> { 4, 5 }, new List<int> { 7, 6 });
                enterAlone.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 6, 6 });
                dialogue_A.SetActive(true);
                has_dialogue_A_pd_started = true;
                currentGameMode = GameMode.TextPlaying;
                enterAlone.SetActive(false);
                break;

            case 7:
                dialogue_A.SetActive(false);
                StartMovement(new List<int> { 5, 6, 7 }, new List<int> { 6, 5, 4 });
                enterAlone.SetActive(true);
                
                break;

            case 8:
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 7, 7 }, new List<int> { 4, 4 });
                s_king_gift_b.SetActive(true);
                has_s_king_gift_b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 9:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 4, 4 });
                s_king_gift_b.SetActive(false);
                s_knight_gift_b.SetActive(true);
                has_s_knight_gift_b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 10:
                s_knight_gift_b.SetActive(false);
                enterAlone.SetActive(true);
                StartMovement(new List<int> { 7, 11 }, new List<int> { 4, 8 });
                break;

            case 11:
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 11, 11 }, new List<int> { 8, 8 });
                canCreateLine_2 = false;
                canDeleteLine_2 = false;
                s_king_2_money.SetActive(true);
                has_s_king_2_money_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 12:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 8, 8 });
                s_king_2_money.SetActive(false);
                s_knight_2_money.SetActive(true);
                has_s_knight_2_money_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 13:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 8, 12 });
                s_knight_2_money.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 14:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 12, 12 });
                enterAlone.SetActive(false);
                s_king_c.SetActive(true);
                has_s_king_c_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 15:
                StartMovement(new List<int> { 11, 13 }, new List<int> { 12, 12 });
                enterAlone.SetActive(true);
                s_king_c.SetActive(false);
                break;

            case 16:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 });
                enterAlone.SetActive(false);
                s_knight_c.SetActive(true);
                has_s_knight_c_started = true;
                currentGameMode = GameMode.TextPlaying;
                end_2 = true;
                break;
        }
    }

    public void charaMoveRoute3()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });
                enterAlone.SetActive(true);
                break;

            case 1:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 3, 3 });
                newCharaAnounce.SetActive(true);
                set_ItemA_as_B_child(dog, character1);
                enterAlone.SetActive(false);
                has_newC_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 2:
                newCharaAnounce.SetActive(false);
                enterAlone.SetActive(true);
                StartMovement(new List<int> { 2, 4 }, new List<int> { 3, 7 });
                break;

            case 3:
                canCreateLine_1 = false;
                canDeleteLine_1 = false;
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 4, 4 }, new List<int> { 7, 7 });
                s_knight_gift_a.SetActive(true);
                has_s_knight_gift_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 4:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 7, 7 });
                s_knight_gift_a.SetActive(false);
                s_king_gift_a.SetActive(true);
                has_s_king_gift_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 5:
                s_king_gift_a.SetActive(false);
                StartMovement(new List<int> { 4, 8 }, new List<int> { 7, 11 });
                enterAlone.SetActive(true);
                break;

            case 6:
                set_transparent(spriteRenderer, 1.0f);
                enterAlone.SetActive(false);
                canCreateLine_2 = false;
                canDeleteLine_2 = false;
                StartMovement(new List<int> { 8, 8 }, new List<int> { 11, 11 });
                s_knight_3_money_a.SetActive(true);
                has_s_knight_3_money_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 7:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 11, 11 });
                dog_runAway.SetActive(true);
                dog.SetActive(false);
                s_knight_3_money_a.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 11, 11 });
                dog_runAway.SetActive(false);
                enterAlone.SetActive(false);
                s_king_3_money_a.SetActive(true);
                has_s_king_3_money_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 9:
                StartMovement(new List<int> { 8, 9 }, new List<int> { 11, 10 });
                s_king_3_money_a.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 10:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 });
                dialogue_B1.SetActive(true);
                has_dialogue_B1_pd_started = true;
                currentGameMode = GameMode.TextPlaying;
                enterAlone.SetActive(false);
                break;

            case 11:
                StartMovement(new List<int> { 9, 10, 11 }, new List<int> { 10, 9, 8 });
                dialogue_B1.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 12:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 8, 8 });
                enterAlone.SetActive(false);
                s_king_3_money_b.SetActive(true);
                has_s_king_3_money_b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 13:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 8, 8 });
                s_king_3_money_b.SetActive(false);
                s_knight_3_money_b.SetActive(true);
                has_s_knight_3_money_b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 14:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 8, 12 });
                s_knight_3_money_b.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 15:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 12, 12 });
                s_king_d.SetActive(true);
                has_s_king_d_started = true;
                currentGameMode = GameMode.TextPlaying;
                enterAlone.SetActive(false);
                break;

            case 16:
                StartMovement(new List<int> { 11, 13 }, new List<int> { 12, 12 });
                s_king_d.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 17:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 });
                s_knight_d.SetActive(true);
                has_s_knight_d_started = true;
                currentGameMode = GameMode.TextPlaying;
                enterAlone.SetActive(false);
                end_3 = true;
                break;
        }
    }

    public void charaMoveRoute4()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });
                enterAlone.SetActive(true);
                break;

            case 1:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 3, 3 });
                newCharaAnounce.SetActive(true);
                set_ItemA_as_B_child(dog, character1);
                enterAlone.SetActive(false);
                has_newC_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 2:
                newCharaAnounce.SetActive(false);
                enterAlone.SetActive(true);
                StartMovement(new List<int> { 2, 4 }, new List<int> { 3, 7 });
                break;

            case 3:
                canCreateLine_1 = false;
                canDeleteLine_1 = false;
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 4, 4 }, new List<int> { 7, 7 });
                s_knight_gift_a.SetActive(true);
                has_s_knight_gift_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 4:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 7, 7 });
                s_knight_gift_a.SetActive(false);
                s_king_gift_a.SetActive(true);
                has_s_king_gift_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 5:
                s_king_gift_a.SetActive(false);
                StartMovement(new List<int> { 4, 5 }, new List<int> { 7, 6 });
                enterAlone.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 6, 6 });
                dialogue_A.SetActive(true);
                has_dialogue_A_pd_started = true;
                currentGameMode = GameMode.TextPlaying;
                enterAlone.SetActive(false);
                break;

            case 7:
                dialogue_A.SetActive(false);
                StartMovement(new List<int> { 5, 6, 7 }, new List<int> { 6, 5, 4 });
                enterAlone.SetActive(true);
                break;

            case 8:
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 7, 7 }, new List<int> { 4, 4 });
                s_king_gift_b.SetActive(true);
                has_s_king_gift_b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 9:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 4, 4 });
                s_king_gift_b.SetActive(false);
                s_knight_gift_b.SetActive(true);
                has_s_knight_gift_b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 10:
                s_knight_gift_b.SetActive(false);
                enterAlone.SetActive(true);
                StartMovement(new List<int> { 7, 11 }, new List<int> { 4, 8 });
                break;

            case 11:
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 11, 11 }, new List<int> { 8, 8 });
                canCreateLine_2 = false;
                canDeleteLine_2 = false;
                s_king_4_money_a.SetActive(true);
                has_s_king_4_money_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 12:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 8, 8 });
                s_king_4_money_a.SetActive(false);
                s_knight_4_money_a.SetActive(true);
                has_s_knight_4_money_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 13:
                StartMovement(new List<int> { 11, 10 }, new List<int> { 8, 9 });
                s_knight_4_money_a.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 14:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 9 });
                dialogue_B2.SetActive(true);
                has_dialogue_B2_pd_started = true;
                currentGameMode = GameMode.TextPlaying;
                enterAlone.SetActive(false);
                break;

            case 15:
                StartMovement(new List<int> { 10, 9, 8 }, new List<int> { 9, 10, 11 });
                dialogue_B2.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 16:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 11, 11 });
                enterAlone.SetActive(false);
                s_knight_4_money_b.SetActive(true);
                has_s_knight_4_money_b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 17:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 11, 11 });
                dog_runAway.SetActive(true);
                s_knight_4_money_b.SetActive(false);
                dog.SetActive(false);

                break;

            case 18:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 11, 11 });
                dog_runAway.SetActive(false);
                s_king_4_money_b.SetActive(true);
                has_s_king_4_money_b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 19:
                s_king_4_money_b.SetActive(false);
                enterAlone.SetActive(true);
                StartMovement(new List<int> { 8, 12 }, new List<int> { 11, 11 });
                break;

            case 20:
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 });
                s_knight_e.SetActive(true);
                has_s_knight_e_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 21:
                enterAlone.SetActive(true);
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 13 });
                s_knight_e.SetActive(false);
                break;

            case 22:
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 });
                s_king_e.SetActive(true);
                has_s_king_e_started = true;
                currentGameMode = GameMode.TextPlaying;
                end_4 = true;
                break;
        }
    }



    public void waitForSceneChange_Menu()
    {
        
    }
    public void toMainScene()
    {
        //Debug.Log("LoadScene:TitleScene");
        //SceneManager.LoadScene("TitleScene");
        Debug.Log("LoadScene:M");
        SceneManager.LoadScene("M");
    }

    public void quitGame()
    {
        Application.Quit(); // ゲームを閉じる
    }
    public void reTry()
    {
        Debug.Log("リトライ！");
        isHorizontal_1_LineCreated = false;
        isHorizontal_2_LineCreated = false;
        end_1 = false;
        end_2 = false;
        end_3 = false;
        end_4 = false;
        currentMovementIndex = 0;
        canCreateLine_1 = true;
        canCreateLine_2 = true;
        canDeleteLine_1 = true;
        canDeleteLine_2 = true;
        enterAlone.SetActive(false);
        J0_hoverArea j0_hoverScript = FindObjectOfType<J0_hoverArea>();
        //Destroy(j0_hoverScript.currentLine);
        foreach (GameObject obj in j0_hoverScript.lines_List)
        {
            if (obj != null)
            {
                Destroy(obj); 
            }
        }
        j0_hoverScript.lines_List.Clear();

        character1.transform.position = character1_initial_pos.transform.position;
        character2.transform.position = character2_initial_pos.transform.position;

        dog.SetActive(true); 
        dog.transform.SetParent(null);
        dog.transform.position = dog_pos.position;
        
        set_transparent(spriteRenderer, 0.2f);

        if (s_knight_a.activeSelf)
        {
            s_knight_a.SetActive(false);
        }
        if (s_king_a.activeSelf)
        {
            s_king_a.SetActive(false);
        }
        if (s_knight_b.activeSelf)
        {
            s_knight_b.SetActive(false);
        }
        if (s_king_b.activeSelf)
        {
            s_king_b.SetActive(false);
        }
        if (s_knight_c.activeSelf)
        {
            s_knight_c.SetActive(false);
        }
        if (s_king_c.activeSelf)
        {
            s_king_c.SetActive(false);
        }
        if (s_knight_d.activeSelf)
        {
            s_knight_d.SetActive(false);
        }
        if (s_king_d.activeSelf)
        {
            s_king_d.SetActive(false);
        }
        if (s_knight_e.activeSelf)
        {
            s_knight_e.SetActive(false);
        }
        if (s_king_e.activeSelf)
        {
            s_king_e.SetActive(false);
        }
        if (dialogue_A.activeSelf)
        {
            dialogue_A.SetActive(false);
        }
        if (dialogue_B2.activeSelf)
        {
            dialogue_B2.SetActive(false);
        }
        if (dialogue_B1.activeSelf)
        {
            dialogue_B1.SetActive(false);
        }
        if (s_knight_gift_a.activeSelf)
        {
            s_knight_gift_a.SetActive(false);
        }
        if (s_king_gift_a.activeSelf)
        {
            s_king_gift_a.SetActive(false);
        }
        if (s_knight_gift_b.activeSelf)
        {
            s_knight_gift_b.SetActive(false);
        }
        if (s_king_gift_b.activeSelf)
        {
            s_king_gift_b.SetActive(false);
        }
        if (s_knight_1_money.activeSelf) 
        { 
            s_knight_1_money.SetActive(false); 
        }
        if (s_king_1_money.activeSelf) 
        { 
            s_king_1_money.SetActive(false);
        }
        if (s_knight_2_money.activeSelf) 
        { 
            s_knight_2_money.SetActive(false);
        }
        if (s_king_2_money.activeSelf)
        {
            s_king_2_money.SetActive(false);
        }
        if (s_knight_3_money_a.activeSelf)
        {
            s_knight_3_money_a.SetActive(false);
        }
        if (s_king_3_money_a.activeSelf) 
        { 
            s_king_3_money_a.SetActive(false);
        }
        if (s_knight_3_money_b.activeSelf)
        {
            s_knight_3_money_b.SetActive(false); 
        }
        if (s_king_3_money_b.activeSelf) 
        { 
            s_king_3_money_b.SetActive(false); 
        }
        if (s_knight_4_money_a.activeSelf)
        {
            s_knight_4_money_a.SetActive(false); 
        }
        if (s_king_4_money_a.activeSelf)
        { 
            s_king_4_money_a.SetActive(false);
        }
        if (s_knight_4_money_b.activeSelf) 
        {
            s_knight_4_money_b.SetActive(false); 
        }
        if (s_king_4_money_b.activeSelf)
        {
            s_king_4_money_b.SetActive(false);
        }
        if (newCharaAnounce.activeSelf)
        {
            newCharaAnounce.SetActive(false);
        }
        if (dog_runAway.activeSelf)
        {
            dog_runAway.SetActive(false);
        }
        has_s_knight_a_played = false;
        has_s_knight_a_started = false;
        has_s_knight_b_played = false;
        has_s_knight_b_started = false;
        has_s_knight_c_played = false;
        has_s_knight_c_started = false;
        has_s_knight_d_played = false;
        has_s_knight_d_started = false;
        has_s_knight_e_played = false;
        has_s_knight_e_started = false;
        has_s_knight_gift_a_played = false;
        has_s_knight_gift_a_started = false;
        has_s_knight_gift_b_played = false;
        has_s_knight_gift_b_started = false;
        has_s_king_a_played = false;
        has_s_king_a_started = false;
        has_s_king_b_played = false;
        has_s_king_b_started = false;
        has_s_king_c_played = false;
        has_s_king_c_started = false;
        has_s_king_d_played = false;
        has_s_king_d_started = false;
        has_s_king_e_played = false;
        has_s_king_e_started = false;
        has_s_king_gift_a_played = false;
        has_s_king_gift_a_started = false;
        has_s_king_gift_b_played = false;
        has_s_king_gift_b_started = false;
        
        has_s_knight_1_money_played = false;
        has_s_knight_1_money_started = false;
        has_s_knight_2_money_played = false;
        has_s_knight_2_money_started = false;
        has_s_knight_3_money_a_played = false;
        has_s_knight_3_money_a_started = false;
        has_s_knight_3_money_b_played = false;
        has_s_knight_3_money_b_started = false;
        has_s_knight_4_money_a_played = false;
        has_s_knight_4_money_a_started = false;
        has_s_knight_4_money_b_played = false;
        has_s_knight_4_money_b_started = false;
        has_s_king_1_money_played = false;
        has_s_king_1_money_started = false;
        has_s_king_2_money_played = false;
        has_s_king_2_money_started = false;
        has_s_king_3_money_a_played = false;
        has_s_king_3_money_a_started = false;
        has_s_king_3_money_b_played = false;
        has_s_king_3_money_b_started = false;
        has_s_king_4_money_a_played = false;
        has_s_king_4_money_a_started = false;
        has_s_king_4_money_b_played = false;
        has_s_king_4_money_b_started = false;
        has_dialogue_A_pd_played = false;
        has_dialogue_A_pd_started = false;
        has_dialogue_B1_pd_played = false;
        has_dialogue_B1_pd_started = false;
        has_dialogue_B2_pd_played = false;
        has_dialogue_B2_pd_started = false;
        has_newC_started = false;
        has_newC_2_started = false;
        has_newC_3_started = false;
        has_newC_1_played = false;
        has_newC_2_played = false;
        has_newC_played = false;
        currentGameMode = GameMode.PlayerPlaying;
    }

    // 点のペアごとにホバーエリアを生成するメソッド
    void CreateHoverAreaJ0(GameObject pointA, GameObject pointB)
    {
        Debug.Log($"CreateHoverArea called with pointA: {pointA.name}, pointB: {pointB.name}");   // デバッグログを出力して、点Aと点Bの情報を表示する

        Vector3 midPoint = (pointA.transform.position + pointB.transform.position) / 2;   // 点Aと点Bの中間点を計算する
        Vector3 direction = (pointB.transform.position - pointA.transform.position).normalized;   // 点Aから点Bへの方向ベクトルを計算し、正規化する

        GameObject hoverArea = new GameObject("HoverArea");   // 新しい GameObject を作成し、その名前を "HoverArea" に設定する
        BoxCollider boxCollider = hoverArea.AddComponent<BoxCollider>();   // BoxCollider コンポーネントを追加し、ホバーエリアのサイズを設定する

        // 点Aと点Bの間の距離をホバーエリアの横幅に設定し、縦幅を hoverAreaWidth、厚みを 0.1f に設定する
        float distance = Vector3.Distance(pointA.transform.position, pointB.transform.position);
        boxCollider.size = new Vector3(distance, hoverAreaWidth, 0.1f);

        // BoxCollider をトリガーとして設定する
        boxCollider.isTrigger = true;

        Debug.Log($"BoxCollider created with size: {boxCollider.size}");   // BoxCollider のサイズをデバッグログで表示する

        hoverArea.transform.position = midPoint;   // ホバーエリアの位置を中間点に設定する
        // 【new！】ここを必ず注意してください！こう書いてください！
        // LookRotationを使って向きを設定し、水平方向に回転させます。
        hoverArea.transform.LookAt(pointB.transform);
        hoverArea.transform.Rotate(0, 90, 0);  // Colliderの長辺が点Aと点Bに向くようにしてください。

        // J0hoverScript スクリプトをホバーエリアに追加する
        J0_hoverArea J0hoverScript = hoverArea.AddComponent<J0_hoverArea>();         // Need changed NOTICE！
        J0hoverScript.Initialize(pointA, pointB, horizontalLineMaterial, horizontalLineWidth, tooltipPrefab);   // J1hoverScript スクリプトを初期化する

        // J0hoverScript スクリプトが追加されたことをデバッグログで表示する
        Debug.Log($"J0hoverScript added to {hoverArea.name}");

        // ホバーエリアが作成された位置とサイズをデバッグログで表示する
        Debug.Log($"Hover Area created at {midPoint} with size {boxCollider.size}");
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

        // J0_charaHoverAreaScript スクリプトをキャラクターに追加する
        J0_charaHoverArea J0_charaHoverAreaScript = character.AddComponent<J0_charaHoverArea>();

        // J0_charaHoverAreaScript スクリプトの情報番号を設定する
        J0_charaHoverAreaScript.charaInfoNum = charaInfoNum;

        // J0_charaHoverAreaScriptスクリプトを初期化する
        J0_charaHoverAreaScript.Initialize(character, charaInfoPrefab, charaInfoPosition);
    }

    void StartMovement(List<int> character_1_Path, List<int> character_2_Path)
    {
        // 進行中の移動を停止
        if (character1MovementCoroutine != null)
            StopCoroutine(character1MovementCoroutine);
        if (character2MovementCoroutine != null)
            StopCoroutine(character2MovementCoroutine);

        character1MovementCoroutine = StartCoroutine(MoveCharacter1Coroutine(character_1_Path));
        character2MovementCoroutine = StartCoroutine(MoveCharacter2Coroutine(character_2_Path));

        currentMovementIndex++;
    }

    IEnumerator MoveCharacter1Coroutine(List<int> path)
    {
        foreach (int point in path)
        {
            Vector3 targetPosition = pointsDictionary[point] + new Vector3(0, 0, -0.1f);
            while (character1.transform.position != targetPosition)
            {
                character1.transform.position = Vector3.MoveTowards(character1.transform.position, targetPosition, Time.deltaTime * speed);
                yield return null;
            }
        }
    }

    IEnumerator MoveCharacter2Coroutine(List<int> path)
    {
        foreach (int point in path)
        {
            Vector3 targetPosition = pointsDictionary[point] + new Vector3(0, 0, -0.1f);
            while (character2.transform.position != targetPosition)
            {
                character2.transform.position = Vector3.MoveTowards(character2.transform.position, targetPosition, Time.deltaTime * speed);
                yield return null;
            }
        }
    }



    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == Jyomaku_J0_TextPlayableDirector) 
        {
            target_J0_Text.SetActive(true);
            target_J0_TextPlayableDirector.Play();

            Debug.Log(" Jyomaku_J0_Text Timeline playback completed.");
        }
        else if (director == target_J0_TextPlayableDirector)
        {
            target_J0_2_Text.SetActive(true);
            
            Debug.Log(" target_J0_Text Timeline playback completed.");
        }
        else if (director == targetCompletedTextPlayableDirector)
        {
            Debug.Log(" targetCompletedTextPlayableDirector completed.");
        }
        else if (director == target_J0_2_TextPlayableDirector)
        {
            s_knight_a.SetActive(true);
            has_s_knight_a_started = true;
            has_target_2_played = true;
            Debug.Log(" target_J0_2_Text Timeline playback completed.");
        }
        else if (director == targetCompletedText_2_PlayableDirector)
        {
            Debug.Log(" targetCompletedText_2_PlayableDirector Timeline playback completed.");
        }
        else if (director == s_knight_a_pd)
        {
            has_s_knight_a_started = false;
            has_s_knight_a_played = true;
            Debug.Log("s_knight_a_pd has played.");
        }
        else if (director == s_king_a_pd)
        {
            has_s_king_a_started = false;
            has_s_king_a_played = true;
            Debug.Log("s_king_a_pd has played.");
        }
        else if (director == s_knight_b_pd)
        {
            has_s_knight_b_started = false;
            has_s_knight_b_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("s_knight_b_pd has played.");
        }
        else if (director == s_king_b_pd)
        {
            has_s_king_b_started = false;
            has_s_king_b_played = true;
            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("s_king_b_pd has played.");
        }
        else if (director == s_knight_c_pd)
        {
            has_s_knight_c_started = false;
            has_s_knight_c_played = true;
            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("s_knight_c_pd has played.");
        }
        else if (director == s_king_c_pd)
        {
            has_s_king_c_started = false;
            has_s_king_c_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("s_king_c_pd has played.");
        }
        else if (director == s_knight_d_pd)
        {
            targetCompletedText.SetActive(true);
            has_s_knight_d_started = false;
            has_s_knight_d_played = true;
            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("s_knight_d_pd has played.");
        }
        else if (director == s_king_d_pd)
        {
            has_s_king_d_started = false;
            has_s_king_d_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("s_king_d_pd has played.");
        }
        else if (director == s_knight_e_pd)
        {
            has_s_knight_e_started = false;
            has_s_knight_e_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("s_knight_e_pd has played.");
        }
        else if (director == s_king_e_pd)
        {
            has_s_king_e_started = false;
            has_s_king_e_played = true;
            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("s_king_e_pd has played.");
        }
        else if(director == s_knight_gift_a_pd)
        {
            has_s_knight_gift_a_started = false;
            has_s_knight_gift_a_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_knight_gift_a has played.");
        }
        else if (director == s_king_gift_a_pd)
        {
            has_s_king_gift_a_started = false;
            has_s_king_gift_a_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_king_gift_a has played.");
        }
        else if (director == s_knight_gift_b_pd)
        {
            has_s_knight_gift_b_started = false;
            has_s_knight_gift_b_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("s_knight_gift_b has played.");
        }
        else if (director == s_king_gift_b_pd)
        {
            has_s_king_gift_b_started = false;
            has_s_king_gift_b_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_king_gift_b has played.");
        }
        else if (director == s_knight_1_money_pd)
        {
            has_s_knight_1_money_started = false;
            has_s_knight_1_money_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_knight_1_money has played.");
        }
        else if (director == s_king_1_money_pd)
        {
            has_s_king_1_money_started = false;
            has_s_king_1_money_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_king_1_money has played.");
        }
        else if (director == s_knight_2_money_pd)
        {
            has_s_knight_2_money_started = false;
            has_s_knight_2_money_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_knight_2_money has played.");
        }
        else if (director == s_king_2_money_pd)
        {
            has_s_king_2_money_started = false;
            has_s_king_2_money_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_king_2_money has played.");
        }
        else if (director == s_knight_3_money_a_pd)
        {
            has_s_knight_3_money_a_started = false;
            has_s_knight_3_money_a_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_knight_3_money_a has played.");
        }
        else if (director == s_king_3_money_a_pd)
        {
            has_s_king_3_money_a_started = false;
            has_s_king_3_money_a_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_king_3_money_a has played.");
        }
        else if (director == s_knight_3_money_b_pd)
        {
            has_s_knight_3_money_b_started = false;
            has_s_knight_3_money_b_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_knight_3_money_b has played.");
        }
        else if (director == s_king_3_money_b_pd)
        {
            has_s_king_3_money_b_started = false;
            has_s_king_3_money_b_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_king_3_money_b has played.");
        }
        else if (director == s_knight_4_money_a_pd)
        {
            has_s_knight_4_money_a_started = false;
            has_s_knight_4_money_a_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_knight_4_money_a has played.");
        }
        else if (director == s_king_4_money_a_pd)
        {
            has_s_king_4_money_a_started = false;
            has_s_king_4_money_a_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_king_4_money_a has played.");
        }
        else if (director == s_knight_4_money_b_pd)
        {
            has_s_knight_4_money_b_started = false;
            has_s_knight_4_money_b_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_knight_4_money_b has played.");
        }
        else if (director == s_king_4_money_b_pd)
        {
            has_s_king_4_money_b_started = false;
            has_s_king_4_money_b_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" s_king_4_money_b has played.");
        }
        else if (director == game_start_pd)
        {
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("game start!");
        }
        else if (director == newCharaAnPlayableDirector)
        {
            has_newC_started = false;
            has_newC_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" newCharaAnounce has played.");
        }
        else if (director == dialogue_B1_pd)
        {
            has_dialogue_B1_pd_started = false;
            has_dialogue_B1_pd_played = true;
            currentGameMode = GameMode.PlayerPlaying;

        }
        else if (director == dialogue_B2_pd)
        {
            has_dialogue_B2_pd_started = false;
            has_dialogue_B2_pd_played = true;
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == dialogue_A_pd)
        {
            has_dialogue_A_pd_started = false;
            has_dialogue_A_pd_played = true;
            currentGameMode = GameMode.PlayerPlaying;
        }
    }

    public void set_ItemA_as_B_child(GameObject gameObject_A, GameObject gameObject_B)
    {
        if (gameObject_A != null && gameObject_B != null)
        {
            // gameobject A を　Bの　子供にする
            gameObject_A.transform.SetParent(gameObject_B.transform);

            // Aのポジションを設置する
            gameObject_A.transform.localPosition = Vector3.zero + new Vector3(-0.5f, 0, 0);  // Bに対してAのポジション

        }
        else
        {
            Debug.LogWarning("No gameObject_A or gameObject_B!");
        }
    }

    public void set_transparent(SpriteRenderer spriteRenderer, float transparency)
    {
        if (spriteRenderer != null)
        {
            // 色を獲得する
            Color color = spriteRenderer.color;

            // alpha 値を修正する
            color.a = Mathf.Clamp01(transparency); // 透明度は 0.0 から 1.0 までの間を確保する；transparencyが0.5だったら半透明になる

            // SpriteRendererに値をあげる
            spriteRenderer.color = color;
        }
        else
        {
            Debug.LogWarning("未分配 SpriteRenderer！");
        }
    }

    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ

        game_start_pd.stopped -= OnPlayableDirectorStopped;
        if (Jyomaku_J0_TextPlayableDirector != null)
        {
            Jyomaku_J0_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (target_J0_TextPlayableDirector != null)
        {
            target_J0_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (targetCompletedText_2_PlayableDirector != null)
        {
            targetCompletedText_2_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (target_J0_2_TextPlayableDirector != null)
        {
            target_J0_2_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (targetCompletedTextPlayableDirector != null)
        {
            targetCompletedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (s_knight_a_pd != null)
        {
            s_knight_a_pd.stopped -= OnPlayableDirectorStopped;
        }
        if (s_king_a_pd != null)
        {
            s_king_a_pd.stopped -= OnPlayableDirectorStopped;
        }
        if (s_knight_b_pd != null)
        {
            s_knight_b_pd.stopped -= OnPlayableDirectorStopped;
        }
        if (s_king_b_pd != null)
        {
            s_king_b_pd.stopped -= OnPlayableDirectorStopped;
        }
        if (s_knight_c_pd != null)
        {
            s_knight_c_pd.stopped -= OnPlayableDirectorStopped;
        }
        if (s_king_c_pd != null)
        {
            s_king_c_pd.stopped -= OnPlayableDirectorStopped;
        }
        s_knight_d_pd.stopped -= OnPlayableDirectorStopped;
        s_king_d_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_e_pd.stopped -= OnPlayableDirectorStopped;
        s_king_e_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_gift_a_pd.stopped -= OnPlayableDirectorStopped;
        s_king_gift_a_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_gift_b_pd.stopped -= OnPlayableDirectorStopped;
        s_king_gift_b_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_1_money_pd.stopped -= OnPlayableDirectorStopped;
        s_king_1_money_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_2_money_pd.stopped -= OnPlayableDirectorStopped;
        s_king_2_money_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_3_money_a_pd.stopped -= OnPlayableDirectorStopped;
        s_king_3_money_a_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_3_money_b_pd.stopped -= OnPlayableDirectorStopped;
        s_king_3_money_b_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_4_money_a_pd.stopped -= OnPlayableDirectorStopped;
        s_king_4_money_a_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_4_money_b_pd.stopped -= OnPlayableDirectorStopped;
        s_king_4_money_b_pd.stopped -= OnPlayableDirectorStopped;
        dialogue_A_pd.stopped -= OnPlayableDirectorStopped;
        dialogue_B1_pd.stopped -= OnPlayableDirectorStopped;
        dialogue_B2_pd.stopped -= OnPlayableDirectorStopped;

        newCharaAnPlayableDirector.stopped -= OnPlayableDirectorStopped;
    }
}
