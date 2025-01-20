using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class T_new_gameController : MonoBehaviour
{
    private bool failed = true;

    public Vector3 character1_initial_pos = new Vector3(-1, 3, -0.2f); // 移動する前にキャラクターの居場所
    public Vector3 character2_initial_pos = new Vector3(2, 3, -0.2f); // 移動する前にキャラクターの居場所

    public GameObject enterAlone;
    public GameObject mouseIcon;

    public GameObject switch_iconExplaMode;
    public GameObject Text_target;
    public GameObject play_target; // 目標テキストを再生するGameObject
    public PlayableDirector play_target_pd; // 目標テキストを再生するPlayableDirector
    private bool has_play_target_pd_Started = true;
    private bool has_play_target_pd_Played = false;

    public GameObject text_c;

    public GameObject beMoved_target; // 目標テキストを移動するGameObject
    public PlayableDirector beMoved_target_pd; // 目標テキストを移動するPlayableDirector
    private bool has_beMoved_target_pd_Started = false;
    private bool has_beMoved_target_pd_Played = false;

    public GameObject target_cirlcle; // 目標達成時の●

    public GameObject Text_explainChara; // 説明文　キャラクターについて
    public PlayableDirector Text_explainChara_pd; // 説明文　キャラクターについてのPlayableDirector
    private bool has_Text_explainChara_pd_Started = false;
    private bool has_Text_explainChara_pd_Played = false;

    public GameObject Text_explainCharaInfo; // 説明文　キャラクターInfoについて
    public PlayableDirector Text_explainCharaInfo_pd; // 説明文　キャラクターInfoについてのPlayableDirector
    public bool has_Text_explainCharaInfo_pd_Started = false;
    public bool has_Text_explainCharaInfo_pd_Played = false;

    public bool has_knightInfoChecked = false;
    public bool has_kingInfoChecked = false;
    public bool oneOfCharaInfos_has_checked = false;

    public GameObject lines;

    public GameObject show_two_lines; // 線が出て来るアニメーション
    public PlayableDirector show_two_lines_pd; // 線が出て来るアニメーション
    public bool has_show_two_lines_Started = false;
    public bool has_show_two_lines_Played = false;

    public GameObject show_point; //　点を説明するGameObject
    public PlayableDirector show_point_pd; //　点を説明するPlayableDirector
    private bool has_show_point_pd_Started = false;
    private bool has_show_point_pd_Played = false;

    public GameObject expla_createLine; // 線をつなぐアニメーション
    public PlayableDirector expla_createLine_pd;

    public GameObject createLine_show_point_2; //　線をつなぐアニメーションの後に説明文
    public PlayableDirector createLine_show_point_2_pd; //　線をつなぐアニメーションの後に説明文のPlayableDirector

    public GameObject plotIcon; // プロットアイコン
    public GameObject end_icon_1; // 結末アイコン　左
    public GameObject end_icon_2; // 結末アイコン　右

    public GameObject text_end_icon; // 結末アイコンを説明するテキスト
    public PlayableDirector text_end_icon_pd; // 結末アイコンを説明するテキストのPlayableDirector
    private bool has_text_end_icon_pd_Started = false;
    private bool has_text_end_icon_pd_Played = false;

    public GameObject text_charaMochibe; // 開始時点のキャラクターセリフ：キャラクター各自の要求や野望、願いなどを説明するテキスト
    public PlayableDirector text_charaMochibe_pd; // 開始時点のキャラクターセリフを説明するテキストのPlayableDirector
    private bool has_text_charaMochibe_pd_Started = false;
    private bool has_text_charaMochibe_pd_Played = false;

    public GameObject text_mustEncounter; // s_king_aの後に再生する　　横線の必要性を説明するテキスト
    public PlayableDirector text_mustEncounter_pd; // 横線の必要性を説明するテキストのPlayableDirector
    public bool has_text_mustEncounter_pd_Started = false;
    public bool has_text_mustEncounter_pd_Played = false;

    public GameObject text_mustEncounter_1;

    public GameObject pleaseClick; // text_mustEncounterの後にすぐ再生してdestroyする　　クリックしてねというアニメーション
    public PlayableDirector pleaseClick_pd; // クリックしてねというアニメーションのPlayableDirector

    public GameObject text_failed; // 横線を引かずにキャラクターを結末アイコンに移動させたら、ダメだよと説明するテキスト
    public PlayableDirector text_failed_pd; // このPlayableDirector
    private bool has_text_failed_pd_Started = false;
    private bool has_text_failed_pd_Played = false;

    public GameObject text_cleared; // クリアルート終わったら
    public PlayableDirector text_cleared_pd; // このPlayableDirector
    private bool has_text_cleared_pd_Started = false;
    private bool has_text_cleared_pd_Played = false;

    public GameObject text_horizontalLineCreated; // 横線を引いたらすぐ再生する　　横線を引いた後はEnterを押してねと説明するテキスト
    public PlayableDirector text_horizontalLineCreated_pd; // このPlayableDirector
    public bool has_text_horizontalLineCreated_pd_Started = false;
    public bool has_text_horizontalLineCreated_pd_Played = false;

    public bool _isHoriz_line_Created = false; // 横線は接続された？のブール値をfalseとマークする

    public GameObject text_noLineCreated; // 横線を引かずにEnterを押した場合を再生する　　Enterを押してねと説明するテキスト
    public PlayableDirector text_noLineCreated_pd; // このPlayableDirector
    private bool has_text_noLineCreated_pd_Started = false;
    private bool has_text_noLineCreated_pd_Played = false;

    public GameObject s_knight_b; // 騎士＋ギロチンのセリフ
    public PlayableDirector s_knight_b_pd; // このPlayableDirector
    private bool has_s_knight_b_pd_Started = false;
    private bool has_s_knight_b_pd_Played = false;

    public GameObject s_king_b; // 国王＋お金のセリフ
    public PlayableDirector s_king_b_pd; // このPlayableDirector
    private bool has_s_king_b_pd_Started = false;
    private bool has_s_king_b_pd_Played = false;

    public GameObject s_knight_c; // 騎士＋お金のセリフ
    public PlayableDirector s_knight_c_pd; // このPlayableDirector
    private bool has_s_knight_c_pd_Started = false;
    private bool has_s_knight_c_pd_Played = false;

    public GameObject s_king_c; // 国王＋ギロチンのセリフ
    public PlayableDirector s_king_c_pd; // このPlayableDirector
    private bool has_s_king_c_pd_Started = false;
    private bool has_s_king_c_pd_Played = false;

    public GameObject s_knight_a; // セリフs_knight_aのGameObject
    public PlayableDirector s_knight_a_pd; // s_knight_a の　PlayableDirector
    private bool has_s_knight_a_Started = false;
    private bool has_s_knight_a_Played = false;

    public GameObject s_king_a; // セリフs_king_aのGameObject
    public PlayableDirector s_king_a_pd; // s_king_a の　PlayableDirector
    private bool has_s_king_a_Started = false;
    private bool has_s_king_a_Played = false;

    public GameObject s_knight_d; // セリフ　国王　クリアルート　途中のGameObject
    public PlayableDirector s_knight_d_pd; // PlayableDirector
    private bool has_s_knight_d_pd_Started = false;
    private bool has_s_knight_d_pd_Played = false;

    public GameObject s_king_d; // セリフ 騎士　クリアルート　途中　のGameObject
    public PlayableDirector s_king_d_pd; // PlayableDirector
    private bool has_s_king_d_pd_Started = false;
    private bool has_s_king_d_pd_Played = false;

    public GameObject dialogue; // 吹き出しのGameObject
    public PlayableDirector dialogue_pd; // s_king_a の　PlayableDirector
    private bool has_dialogue_Started = false;
    private bool has_dialogue_Played = false;

    public GameObject s_king_e; // セリフ国王　失敗ルート　途中のGameObject
    public PlayableDirector s_king_e_pd; // 　PlayableDirector
    private bool has_s_king_e_pd_Started = false;
    private bool has_s_king_e_pd_Played = false;

    public GameObject s_knight_e; // セリフ　騎士　失敗ルート　途中のGameObject
    public PlayableDirector s_knight_e_pd; // 　PlayableDirector
    private bool has_s_knight_e_pd_Started = false;
    private bool has_s_knight_e_pd_Played = false;

    public GameObject s_king_f; // セリフ国王　クリアルート　出会う前　のGameObject
    public PlayableDirector s_king_f_pd; // 　PlayableDirector
    private bool has_s_king_f_pd_Started = false;
    private bool has_s_king_f_pd_Played = false;

    public GameObject s_knight_f; // セリフ　騎士　クリアルート　出会う前　のGameObject
    public PlayableDirector s_knight_f_pd; // 　PlayableDirector
    private bool has_s_knight_f_pd_Started = false;
    private bool has_s_knight_f_pd_Played = false;

    public GameObject show_cursor; // カーソルのアニメーション
    public PlayableDirector show_cursor_pd; // カーソルのアニメーション
    public bool has_show_cursor_started = false;
    public bool has_show_cursor_played = false;

    public GameObject short_expla_rule;

    public Transform startpoint_character1; // 左から1番目のスタート点の位置
    public Transform startpoint_character2; // 左から2番目のスタート点の位置
    public GameObject[] tenGameObjects; // それぞれの点
    public Transform[] tenPositions; // それぞれの点の位置
    public Transform endtpoint_character1; //左から1番目の終点の位置
    public Transform endpoint_character2; // 左から2番目の終点の位置
    public Dictionary<int, Vector3> pointsDictionary;// 点とその位置情報を格納する辞書

    public bool clearedRoute = false;
    public bool failedRoute = false;
    public bool cannnot_cancell = false; // horizontal line cannnot be cancelled now
    public bool cannot_createLine = false; // horizontal line cannnot be created now
    public bool has_cleared_once = false; // 1度クリアできた？をfalseとマークする

    public GameObject character1; // 左から1番目のキャラクター
    public GameObject character2; // 左から2番目のキャラクター

    public GameObject knight_before;
    public GameObject knight_after;
    public GameObject knight_dead;

    private Coroutine character1MovementCoroutine; // 左から1番目のキャラクターの移動
    private Coroutine character2MovementCoroutine; // 左から2番目のキャラクターの移動

    public bool hasMovementFinshed = false; //　キャラクターの移動は完成されたか？のブール値
    public int currentMovementIndex = 0; // プレイヤーがEnterを押す際にプロットアイコンを生成するために計算用のIndex

    public float speed = 3.0f;

    public GameObject tooltipPrefab; // 提示枠のプレハブ
    public Material horizontalLineMaterial; // 横線のマテリアル
    public float horizontalLineWidth = 0.05f; // 横線の幅
    public float hoverAreaWidth = 0.8f; // 横線のホバーエリアの縦幅
    public TMP_FontAsset dotFont;  // ドットフォント
    public GameObject charaInfoPrefab;

    public GameObject menu; // menu_controller
    //public GameObject menuTargetPosition; // たどり着いて欲しい座標
    //public Transform menuBackPosition;
    //public bool menuIsOnItsPos = false; // メニューは目標座標に着いたかをfalseとマークする
    //public bool menuBack = false;
    //public float moveSpeed = 4.0f; // メニューの移動スピード
    //private bool Menu_is_Moving = false;
    //private Vector3 targetPosition;
    private bool onMenu = false;
    private bool menu_switch_on = false;
    public GameObject image_menu_hover_white;
    public GameObject image_menu_button;
    public GameObject image_menu_button_pressed;

    public switch_explaIconMode switch_Expla_script;

    public AudioClip leftClickClip;
    public AudioClip rightClickClip;
    public AudioClip missClip;
    public AudioClip deadClip;

    public AudioSource audioSourceTn;

    public AudioClip changeImageClip;

    public enum GameMode
    {
        //TextPlaying, // ゲーム開始時のテキストが再生中
        PlayerPlaying, // プレイヤーが操作している状態
        WaitForSceneChange, // 現シーンのゲーム内容が終了し、プレイヤーがEnterを押すのを待って次のシーンに切り替える
        choosing // プレイヤーは横線を接続してるなどの時
    }
    public GameMode currentGameMode = GameMode.PlayerPlaying; // 現シーン開始時にゲームモードをStartTextPlayingに設定
    // Start is called before the first frame update
    void Start()
    {

        // Initialize the dictionary and add the points
        pointsDictionary = new Dictionary<int, Vector3>();

        // Add knight and hunter start and end points
        pointsDictionary.Add(0, startpoint_character1.position); // 左から1番目のスタート点
        pointsDictionary.Add(1, startpoint_character2.position); // 左から2番目のスタート点
        pointsDictionary.Add(6, endtpoint_character1.position);  // 左から1番目の終点
        pointsDictionary.Add(7, endpoint_character2.position);   // 左から2番目の終点

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


        //  開始時にを非表示にする
        knight_after.SetActive(false);
        knight_dead.SetActive(false);
        text_c.SetActive(false);
        mouseIcon.SetActive(false);
        if (beMoved_target != null)
        {
            beMoved_target.SetActive(false);
        }
        if (character1 != null)
        {
            character1.SetActive(false);
        }
        if (character2 != null)
        {
            character2.SetActive(false);
        }
        enterAlone.SetActive(false);
        lines.SetActive(false);
        target_cirlcle.SetActive(false);
        s_king_a.SetActive(false);
        s_knight_a.SetActive(false);
        Text_explainChara.SetActive(false);
        Text_explainCharaInfo.SetActive(false);
        show_cursor.SetActive(false);
        show_two_lines.SetActive(false);
        tenGameObjects[0].SetActive(false);
        tenGameObjects[3].SetActive(false);
        show_point.SetActive(false);
        end_icon_1.SetActive(false);
        end_icon_2.SetActive(false);
        text_end_icon.SetActive(false);
        pleaseClick.SetActive(false);
        text_mustEncounter_1.SetActive(false);
        text_failed.SetActive(false);
        text_cleared.SetActive(false);
        text_horizontalLineCreated.SetActive(false);
        text_mustEncounter.SetActive(false);
        plotIcon.SetActive(false);
        expla_createLine.SetActive(false);
        switch_iconExplaMode.SetActive(false);
        text_noLineCreated.SetActive(false);
        text_charaMochibe.SetActive(false);
        s_king_b.SetActive(false);
        s_knight_b.SetActive(false);
        s_king_c.SetActive(false);
        s_knight_c.SetActive(false);
        dialogue.SetActive(false);
        s_king_d.SetActive(false);
        s_knight_d.SetActive(false);
        s_king_e.SetActive(false);
        s_knight_e.SetActive(false);
        s_king_f.SetActive(false);
        s_knight_f.SetActive(false);
        createLine_show_point_2.SetActive(false);
        short_expla_rule.SetActive(false);
        menu.SetActive(false);
        image_menu_hover_white.SetActive(false);

        show_cursor_pd.stopped += OnPlayableDirectorStopped;
        play_target_pd.stopped += OnPlayableDirectorStopped;
        beMoved_target_pd.stopped += OnPlayableDirectorStopped;
        s_king_a_pd.stopped += OnPlayableDirectorStopped;
        s_knight_a_pd.stopped += OnPlayableDirectorStopped;
        Text_explainChara_pd.stopped += OnPlayableDirectorStopped;
        Text_explainCharaInfo_pd.stopped += OnPlayableDirectorStopped;
        show_two_lines_pd.stopped += OnPlayableDirectorStopped;
        show_point_pd.stopped += OnPlayableDirectorStopped;
        text_end_icon_pd.stopped += OnPlayableDirectorStopped;
        text_charaMochibe_pd.stopped += OnPlayableDirectorStopped;
        pleaseClick_pd.stopped += OnPlayableDirectorStopped;
        text_failed_pd.stopped += OnPlayableDirectorStopped;
        text_horizontalLineCreated_pd.stopped += OnPlayableDirectorStopped;
        text_mustEncounter_pd.stopped += OnPlayableDirectorStopped;
        text_noLineCreated_pd.stopped += OnPlayableDirectorStopped;
        s_king_b_pd.stopped += OnPlayableDirectorStopped;
        s_knight_b_pd.stopped += OnPlayableDirectorStopped;
        s_king_c_pd.stopped += OnPlayableDirectorStopped;
        s_knight_c_pd.stopped += OnPlayableDirectorStopped;
        s_king_d_pd.stopped += OnPlayableDirectorStopped;
        s_knight_d_pd.stopped += OnPlayableDirectorStopped;
        dialogue_pd.stopped += OnPlayableDirectorStopped;
        text_cleared_pd.stopped += OnPlayableDirectorStopped;
        s_knight_f_pd.stopped += OnPlayableDirectorStopped;
        s_king_f_pd.stopped += OnPlayableDirectorStopped;
        s_knight_e_pd.stopped += OnPlayableDirectorStopped;
        s_king_e_pd.stopped += OnPlayableDirectorStopped;
        expla_createLine_pd.stopped += OnPlayableDirectorStopped;
        createLine_show_point_2_pd.stopped += OnPlayableDirectorStopped;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("currentGameMode:" + currentGameMode);
        switch (currentGameMode)
        {
            //case GameMode.TextPlaying:

            //    if (menu_switch_on)
            //    {
            //        waitForSceneChange_Menu();
            //    }
            //    break;

            case GameMode.PlayerPlaying:
                if (onMenu )
                {
                    return;
                }
                if (switch_Expla_script.mouseOn)
                {
                    return;
                }
                if (Input.GetMouseButtonDown(0))  // Input.GetKeyDown(KeyCode.Return)
                {
                    Debug.Log("currentMovementIndex: " + currentMovementIndex);

                    if(has_play_target_pd_Started && !has_play_target_pd_Played)
                    {
                        play_target_pd.time = play_target_pd.duration;
                        play_target_pd.Evaluate();
                    }

                    else if (has_play_target_pd_Played && !has_beMoved_target_pd_Started && !has_beMoved_target_pd_Played) // 目標は表示すされたが、まだ左上に移動されてない
                    {
                        //Destroy(play_target); // 最初の目標と説明文を削除する
                        //Destroy(Text_target); // 上と同じ
                        play_target.SetActive(false); // 最初の目標と説明文を非表示にする
                        Text_target.SetActive(false); // 上と同じ
                        beMoved_target.SetActive(true); // 目標を左上に移動するアニメーションを表示して、プレイする
                        has_beMoved_target_pd_Started = true;
                    }

                    else if (has_beMoved_target_pd_Started && !has_beMoved_target_pd_Played)
                    {
                        beMoved_target_pd.time = beMoved_target_pd.duration;
                        beMoved_target_pd.Evaluate();

                        enterAlone.SetActive(true);
                    }

                    else if(has_beMoved_target_pd_Played && !has_Text_explainChara_pd_Started && !has_Text_explainChara_pd_Played)
                    {
                        enterAlone.SetActive(false);
                        Text_explainChara.SetActive(true);
                        has_Text_explainChara_pd_Started = true;
                    }
                    else if(has_Text_explainChara_pd_Started && !has_Text_explainChara_pd_Played)
                    {

                        Text_explainChara_pd.time = Text_explainChara_pd.duration;
                        Text_explainChara_pd.Evaluate();
                    }

                    // 目標は左上に移動された、キャラクター説明も再生された、マウスをキャラクターに合わせる説明文はまだ表示されてない
                    else if (has_beMoved_target_pd_Played && has_Text_explainChara_pd_Played && !has_Text_explainCharaInfo_pd_Played && !has_show_cursor_started && !has_show_cursor_played) 
                    {
                        show_cursor.SetActive(true);
                        show_cursor_pd.Play(); // カーソルのアニメーション
                        has_show_cursor_started = true;

                        Text_explainChara.SetActive(false); // キャラクターの説明文を非表示にする

                        //Text_explainCharaInfo.SetActive(true); // マウスをキャラクターに合わせる説明文を表示して、プレイする
                    }

                    else if (has_show_cursor_started && !has_show_cursor_played)
                    {
                        show_cursor_pd.time = show_cursor_pd.duration;
                        show_cursor_pd.Evaluate();
                    }

                    else if (has_Text_explainCharaInfo_pd_Started && !has_Text_explainCharaInfo_pd_Played)
                    {
                        Text_explainCharaInfo_pd.time = Text_explainCharaInfo_pd.duration;
                        Text_explainCharaInfo_pd.Evaluate();
                    }

                    else if (!oneOfCharaInfos_has_checked && has_show_cursor_played) // プレイヤーはマウスをキャラクターに合わせてない
                    {
                        Debug.Log("please check charaInfo");
                        show_cursor_pd.time = 0;      // ０秒にリセットする
                        show_cursor_pd.Evaluate();    // 更新する
                        show_cursor_pd.Play(); // カーソルのアニメーションをもう1回再生する
                        has_show_cursor_started = true;
                        has_show_cursor_played = false;
                    }

                    // マウスをキャラクターに合わせる説明文は再生された、縦線2本を説明するアニメーションまだ表示されてない
                    else if (has_Text_explainCharaInfo_pd_Played && !has_show_two_lines_Started && !has_show_two_lines_Played)
                    {
                        if (oneOfCharaInfos_has_checked) // プレイヤーはマウスをキャラクターに合わせた
                        {
                            Text_explainCharaInfo.SetActive(false); // マウスをキャラクターに合わせる説明文を非表示にする

                            show_two_lines.SetActive(true); // 縦線2本を説明するアニメーションを表示する（再生終わったら、本当の縦線2本を表示すると予約した）
                            has_show_two_lines_Started = true;

                            if (show_cursor.activeSelf)
                            {
                                show_cursor.SetActive(false);
                            }

                        }
                    }

                    else if (has_show_two_lines_Started && !has_show_two_lines_Played)
                    {
                        show_two_lines_pd.time = show_two_lines_pd.duration;
                        show_two_lines_pd.Evaluate();
                    }

                    // 縦線2本を説明するアニメーションは再生された、点を説明するやつはまだ表示されてない
                    else if (!has_show_two_lines_Started && has_show_two_lines_Played && !has_show_point_pd_Started && !has_show_point_pd_Played)
                    {
                        //Destroy(show_two_lines); // 縦線2本を説明するアニメーションを削除
                        //Destroy(show_cursor); // カーソルのアニメーションを削除
                        show_two_lines.SetActive(false); // 縦線2本を説明するアニメーションを非表示にする
                        if (show_cursor.activeSelf) // カーソルのアニメーションを非表示にする
                        {
                            show_cursor.SetActive(false);
                        }
                        plotIcon.SetActive(true);
                        switch_iconExplaMode.SetActive(true);
                        tenGameObjects[0].SetActive(true); // 本当の点を表示する
                        tenGameObjects[3].SetActive(true); // 本当の点を表示する
                        show_point.SetActive(true); // 点を説明する文を再生する
                        has_show_point_pd_Started = true;
                    }

                    else if (has_show_point_pd_Started && !has_show_point_pd_Played)
                    {
                        show_point_pd.time = show_point_pd.duration;
                        show_point_pd.Evaluate();
                    }

                    // 点の説明文は再生された、結末アイコンの説明文はまだ再生されてない
                    else if (!has_show_point_pd_Started && has_show_point_pd_Played && !has_text_end_icon_pd_Started && !has_text_end_icon_pd_Played)
                    {
                        show_point.SetActive(false); // 点の説明文を非表示にする
                        end_icon_1.SetActive(true); // 結末アイコンを表示する
                        end_icon_2.SetActive(true); // 結末アイコンを表示する
                        text_end_icon.SetActive(true); // 結末アイコンの説明文を再生する
                        has_text_end_icon_pd_Started = true;
                    }

                    else if (has_text_end_icon_pd_Started && !has_text_end_icon_pd_Played)
                    {
                        text_end_icon_pd.time = text_end_icon_pd.duration;
                        text_end_icon_pd.Evaluate();
                    }

                    // 結末アイコンの説明文は再生された、キャラクターの開始時点のセリフの説明文はまだ再生されてない
                    else if (!has_text_end_icon_pd_Started && has_text_end_icon_pd_Played && !has_text_charaMochibe_pd_Started &&!has_text_charaMochibe_pd_Played )
                    {
                        text_end_icon.SetActive(false); // 結末アイコンの説明文を非表示にする
                        text_charaMochibe.SetActive(true); // キャラクターの開始時点のセリフの説明文を再生する
                        has_text_charaMochibe_pd_Started = true;
                    }

                    else if (has_text_charaMochibe_pd_Started && !has_text_charaMochibe_pd_Played)
                    {
                        text_charaMochibe_pd.time = text_charaMochibe_pd.duration;
                        text_charaMochibe_pd.Evaluate();
                    }

                    // キャラクターの開始時点のセリフの説明文は再生された、騎士の最初のセリフはまだ再生されてない
                    else if (!has_text_charaMochibe_pd_Started && has_text_charaMochibe_pd_Played && !has_s_knight_a_Started && !has_s_knight_a_Played)
                    {
                        text_charaMochibe.SetActive(false); // キャラクターの開始時点のセリフの説明文を非表示にする
                        s_knight_a.SetActive(true); // 騎士の最初のセリフを再生する
                        has_s_knight_a_Started = true;
                    }

                    else if(has_s_knight_a_Started && !has_s_knight_a_Played)
                    {
                        s_knight_a_pd.time = s_knight_a_pd.duration;
                        s_knight_a_pd.Evaluate();
                    }

                    // 騎士の最初のセリフは再生された、国王の最初のセリフはまだ再生されてない
                    else if (!has_s_knight_a_Started && has_s_knight_a_Played && !has_s_king_a_Started && !has_s_king_a_Played)
                    {
                        s_knight_a.SetActive(false); // 騎士の最初のセリフを非表示にする
                        s_king_a.SetActive(true); // 国王の最初のセリフを再生する
                        has_s_king_a_Started = true;
                    }

                    else if (has_s_king_a_Started && !has_s_king_a_Played)
                    {
                        s_king_a_pd.time = s_king_a_pd.duration;
                        s_king_a_pd.Evaluate();
                    }

                    // 国王の最初のセリフは再生された、横線で出会う必要性の説明文はまだ再生されてない
                    else if (!has_s_king_a_Started && has_s_king_a_Played && !has_text_mustEncounter_pd_Started && !has_text_mustEncounter_pd_Played)
                    {
                        s_king_a.SetActive(false); // 国王の最初のセリフを非表示にする
                        
                        CreateHoverArea(tenGameObjects[0], tenGameObjects[3]);

                        //T_new_hoverArea t_New_Hover = FindObjectOfType<T_new_hoverArea>();
                        

                        text_mustEncounter.SetActive(true); // 横線で出会う必要性の説明文再生する
                        has_text_mustEncounter_pd_Started = true;
                    }

                    else if(has_text_mustEncounter_pd_Started && !has_text_mustEncounter_pd_Played)
                    {
                        text_mustEncounter_pd.time = text_mustEncounter_pd.duration;
                        text_mustEncounter_pd.Evaluate();
                    }

                    // 横線で出会う必要性の説明文は再生された、
                    else if (!has_text_mustEncounter_pd_Started && has_text_mustEncounter_pd_Played)
                    {
                        if (text_mustEncounter.activeSelf)
                        {
                            text_mustEncounter.SetActive(false);
                        }
                        if (!_isHoriz_line_Created)
                        {
                            failedRoute = true;
                            if (!has_text_noLineCreated_pd_Played)
                            {
                                text_noLineCreated.SetActive(true);
                                has_text_noLineCreated_pd_Started = true;
                                //currentGameMode = GameMode.TextPlaying;
                            }

                            failed_character_move_logic();
                        }
                        else if (_isHoriz_line_Created)
                        {
                            clearedRoute = true;
                            cleared_character_move_logic();

                        }
                    }
                    
                    
                }

                break;

            case GameMode.WaitForSceneChange:
                if (menu_switch_on)
                {
                    waitForSceneChange_Menu();
                }
                Debug.Log("now is gamemode wait for scene change");
                // シーンを切り替える
                if (Input.GetMouseButtonDown(0))  // Input.GetKeyDown(KeyCode.Return)
                {
                    if (failed)
                    {
                        Debug.Log("LoadScene:Tutorial_N_knight_dead");
                        SceneManager.LoadScene("Tutorial_N_knight_dead");
                    }
                    else if (!failed)
                    {
                        Debug.Log("LoadScene:JyoMaku_before_0");
                        SceneManager.LoadScene("JyoMaku_before_0");
                    }
                }
                //Debug.Log("failedのブール値は：" + failed);
                //ChangeTextColor(cannotENTER);

                //menu.SetActive(true);
                //StartCoroutine(MoveMenuToTarget(menuTargetPosition.transform.position, moveSpeed));
                //waitForSceneChange_Menu();

                break;

            case GameMode.choosing:

                break;
        }
        
    }

    public void waitForSceneChange_Menu()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            reTry();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            toMainScene();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            quitGame();
        }
    }

    public void reTry()
    {
        Debug.Log("リトライ！");
        failed = true;
        currentMovementIndex = 0;
        cannot_createLine = false;
        cannnot_cancell = false;
        _isHoriz_line_Created = false;
        T_new_hoverArea T_new_hoverScript = FindObjectOfType<T_new_hoverArea>();
        Destroy(T_new_hoverScript.currentLine);
        character1.transform.position = character1_initial_pos;
        character2.transform.position = character2_initial_pos;
        text_mustEncounter_1.SetActive(true);
        short_expla_rule.SetActive(true);
        knight_before.SetActive(true);
        knight_after.SetActive(false);
        knight_dead.SetActive(false);
        has_play_target_pd_Played = false;
        has_beMoved_target_pd_Played = false;
        has_Text_explainChara_pd_Played = false;
        has_Text_explainCharaInfo_pd_Played = false;
        has_show_two_lines_Played = false;
        has_show_point_pd_Played = false;
        has_text_end_icon_pd_Played = false;
        has_text_charaMochibe_pd_Played = false;
        has_text_mustEncounter_pd_Played = false;
        has_text_failed_pd_Played = false;
        has_text_cleared_pd_Played = false;
        has_text_horizontalLineCreated_pd_Played = false;
        has_text_noLineCreated_pd_Played = false;
        has_s_knight_b_pd_Played = false;
        has_s_king_b_pd_Played = false;
        has_s_knight_c_pd_Played = false;
        has_s_king_c_pd_Played = false;
        has_s_knight_a_Played = false;
        has_s_king_a_Played = false;
        has_s_knight_d_pd_Played = false;
        has_s_king_d_pd_Played = false;
        has_dialogue_Played = false;
        has_s_king_e_pd_Played = false;
        has_s_knight_e_pd_Played = false;
        has_s_king_f_pd_Played = false;
        has_s_knight_f_pd_Played = false;
        has_show_cursor_played = false;
        if (text_cleared != null)
        {
            text_cleared.SetActive(false);
        }
        if (text_failed != null)
        {
            text_failed.SetActive(false);
        }
        if (text_horizontalLineCreated.activeSelf)
        {
            text_horizontalLineCreated.SetActive(false);
        }
        if (text_noLineCreated.activeSelf)
        {
            text_noLineCreated.SetActive(false);
        }
        if (text_failed.activeSelf)
        {
            text_failed.SetActive(false);
        }
        if (text_cleared.activeSelf)
        {
            text_cleared.SetActive(false);
        }
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
        if (s_knight_f.activeSelf)
        {
            s_knight_f.SetActive(false);
        }
        if (s_king_f.activeSelf)
        {
            s_king_f.SetActive(false);
        }
        if (dialogue.activeSelf)
        {
            dialogue.SetActive(false);
        }
        //StartCoroutine(MoveMenuBack(menuBackPosition.position, moveSpeed));
        currentGameMode = GameMode.PlayerPlaying;
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

    public void failed_character_move_logic() // 横線が接続されてない場合、キャラクターたちの移動ロジック
    {
        switch (currentMovementIndex)
        {
            case 0:
                if (text_mustEncounter_1.activeSelf)
                {
                    text_mustEncounter_1.SetActive(false);
                }
                
                StartMovement(new List<int> { 0, 0 }, new List<int> { 1, 1 });
                break;

            case 1:
                if (has_cleared_once)
                {
                    StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 5 });
                    enterAlone.SetActive(true);
                    break;
                }
                if (has_text_noLineCreated_pd_Started && !has_text_noLineCreated_pd_Played)
                {
                    text_noLineCreated_pd.time = text_noLineCreated_pd.duration;
                    text_noLineCreated_pd.Evaluate();
                }
                else if (!has_text_noLineCreated_pd_Started && has_text_noLineCreated_pd_Played)
                {
                    StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 5 });
                }
                break;

            case 2:
                text_noLineCreated.SetActive(false);
                StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });
                enterAlone.SetActive(false);
                cannot_createLine = true;
                s_knight_e.SetActive(true);
                has_s_knight_e_pd_Started = true;
                break;

            case 3:
                if (text_noLineCreated.activeSelf)
                {
                    text_noLineCreated.SetActive(false);
                }
                if(has_s_knight_e_pd_Started && !has_s_knight_e_pd_Played)
                {
                    s_knight_e_pd.time = s_knight_e_pd.duration;
                    s_knight_e_pd.Evaluate();
                }
                else if(!has_s_knight_e_pd_Started && has_s_knight_e_pd_Played)
                {
                    StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });
                    s_knight_e.SetActive(false);
                    s_king_e.SetActive(true);
                    has_s_king_e_pd_Started = true;
                }

                break;

            case 4:
                if (has_s_king_e_pd_Started && !has_s_king_e_pd_Played)
                {
                    s_king_e_pd.time = s_king_e_pd.duration;
                    s_king_e_pd.Evaluate();
                }
                else if(!has_s_king_e_pd_Started && has_s_king_e_pd_Played)
                {
                    StartMovement(new List<int> { 2, 6 }, new List<int> { 5, 5 });
                    s_king_e.SetActive(false);
                    enterAlone.SetActive(true);
                }

                break;

            case 5:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 5, 5 });
                audioSourceTn.PlayOneShot(deadClip);
                if (text_noLineCreated.activeSelf)
                {
                    text_noLineCreated.SetActive(false);
                }
                if (text_horizontalLineCreated.activeSelf)
                {
                    text_horizontalLineCreated.SetActive(false);
                }
                s_knight_b.SetActive(true);
                has_s_knight_b_pd_Started = true;
                knight_before.SetActive(false);
                knight_dead.SetActive(true);
                enterAlone.SetActive(false);
                break;

            case 6:
                if (has_s_knight_b_pd_Started && !has_s_knight_b_pd_Played)
                {
                    s_knight_b_pd.time = s_knight_b_pd.duration;
                    s_knight_b_pd.Evaluate();
                }
                else if(!has_s_knight_b_pd_Started && has_s_knight_b_pd_Played)
                {
                    StartMovement(new List<int> { 6, 6 }, new List<int> { 5, 7 });
                    s_knight_b.SetActive(false);
                    enterAlone.SetActive(true);

                }
                break;

            case 7:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 });
                enterAlone.SetActive(false);
                s_king_b.SetActive(true);
                has_s_king_b_pd_Started = true;
                if (has_cleared_once)
                {
                    failed = false;
                }
                if (short_expla_rule.activeSelf)
                {
                    short_expla_rule.SetActive(false);
                }
                
                break;

            case 8:
                if (has_s_king_b_pd_Started && !has_s_king_b_pd_Played)
                {
                    s_king_b_pd.time = s_king_b_pd.duration;
                    s_king_b_pd.Evaluate();
                }
                else if(!has_s_king_b_pd_Started && has_s_king_b_pd_Played && has_text_failed_pd_Started && !has_text_failed_pd_Played)
                {
                    text_failed_pd.time = text_failed_pd.duration;
                    text_failed_pd.Evaluate();
                    
                }
                else
                {
                    Debug.Log("change to => GameMode.WaitForSceneChange");
                    currentGameMode = GameMode.WaitForSceneChange;
                }
                break;

        }
    }

    public void cleared_character_move_logic() // 横線が接続された場合、キャラクターたちの移動ロジック
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 5 });

                break;

            case 1:
                if (has_text_horizontalLineCreated_pd_Started && !has_text_horizontalLineCreated_pd_Played)
                {
                    text_horizontalLineCreated_pd.time = text_horizontalLineCreated_pd.duration;
                    text_horizontalLineCreated_pd.Evaluate();
                }
                else if (!has_text_horizontalLineCreated_pd_Started && has_text_horizontalLineCreated_pd_Played)
                {
                    StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });
                    if (text_horizontalLineCreated.activeSelf)
                    {
                        text_horizontalLineCreated.SetActive(false);
                    }
                    if (text_mustEncounter_1.activeSelf)
                    {
                        text_mustEncounter_1.SetActive(false);
                    }
                    enterAlone.SetActive(true);
                }
                break;


            case 2:
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });
                cannnot_cancell = true;
                
                s_knight_f.SetActive(true);
                has_s_knight_f_pd_Started = true;
                break;

            case 3:
                if (has_s_knight_f_pd_Started && !has_s_knight_f_pd_Played)
                {
                    s_knight_f_pd.time = s_knight_f_pd.duration;
                    s_knight_f_pd.Evaluate();
                }
                else if(!has_s_knight_f_pd_Started && has_s_knight_f_pd_Played)
                {
                    StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });

                    s_knight_f.SetActive(false);
                    s_king_f.SetActive(true);
                    has_s_king_f_pd_Started = true;
                }
                break;

            case 4:
                if (has_s_king_f_pd_Started && !has_s_king_f_pd_Played)
                {
                    s_king_f_pd.time = s_king_f_pd.duration;
                    s_king_f_pd.Evaluate();
                }
                else if (!has_s_king_f_pd_Started && has_s_king_f_pd_Played)
                {
                    StartMovement(new List<int> { 2, 3 }, new List<int> { 5, 4 });
                    enterAlone.SetActive(true);
                    s_king_f.SetActive(false);
                }
                break;

            case 5:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 4 });
                enterAlone.SetActive(false);
                dialogue.SetActive(true);
                has_dialogue_Started = true;
                knight_before.SetActive(false);
                break;

            case 6:
                if (has_dialogue_Started && !has_dialogue_Played)
                {
                    dialogue_pd.time = dialogue_pd.duration;
                    dialogue_pd.Evaluate();
                }
                else if(!has_dialogue_Started && has_dialogue_Played)
                {
                    knight_after.SetActive(true);
                    dialogue.SetActive(false);
                    StartMovement(new List<int> { 3, 4, 5 }, new List<int> { 4, 3, 2 });
                    enterAlone.SetActive(true);
                }
                break;

            case 7:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 2, 2 });
                enterAlone.SetActive(false);
                s_king_d.SetActive(true);
                has_s_king_d_pd_Started = true;
                break;

            case 8:
                if (has_s_king_d_pd_Started && !has_s_king_d_pd_Played)
                {
                    s_king_d_pd.time = s_king_d_pd.duration;
                    s_king_d_pd.Evaluate();
                }
                else if (!has_s_king_d_pd_Started && has_s_king_d_pd_Played)
                {
                    StartMovement(new List<int> { 5, 5 }, new List<int> { 2, 2 });
                    s_king_d.SetActive(false);
                    s_knight_d.SetActive(true);
                    has_s_knight_d_pd_Started = true;
                }
                break;

            case 9:
                if (has_s_knight_d_pd_Started && !has_s_knight_d_pd_Played)
                {
                    s_knight_d_pd.time = s_knight_d_pd.duration;
                    s_knight_d_pd.Evaluate();
                }
                else if (!has_s_knight_d_pd_Started && has_s_knight_d_pd_Played)
                {
                    StartMovement(new List<int> { 5, 5 }, new List<int> { 2, 6 });
                    s_knight_d.SetActive(false);
                    enterAlone.SetActive(true);
                }
                break;

            case 10:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 6, 6 });
                if (text_noLineCreated.activeSelf)
                {
                    text_noLineCreated.SetActive(false);
                }
                if (text_horizontalLineCreated.activeSelf)
                {
                    text_horizontalLineCreated.SetActive(false);
                }
                s_king_c.SetActive(true);
                has_s_king_c_pd_Started = true;
                enterAlone.SetActive(false);
                break;

            case 11:
                if (has_s_king_c_pd_Started && !has_s_king_c_pd_Played)
                {
                    s_king_c_pd.time = s_king_c_pd.duration;
                    s_king_c_pd.Evaluate();
                }
                else if (!has_s_king_c_pd_Started && has_s_king_c_pd_Played)
                {
                    StartMovement(new List<int> { 5, 7 }, new List<int> { 6, 6 });
                    s_king_c.SetActive(false);
                    enterAlone.SetActive(true);
                }
                break;

            case 12:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 6 });
                s_knight_c.SetActive(true);
                has_s_knight_c_pd_Started = true;

                enterAlone.SetActive(false);
                failed = false;
                if (!has_cleared_once)
                {
                    has_cleared_once = true;
                }
                if (short_expla_rule.activeSelf)
                {
                    short_expla_rule.SetActive(false);
                }
                break;

            case 13:
                if (has_s_knight_c_pd_Started && !has_s_knight_c_pd_Played)
                {
                    s_knight_c_pd.time = s_knight_c_pd.duration;
                    s_knight_c_pd.Evaluate();
                }
                else if(!has_s_knight_c_pd_Started && has_s_knight_c_pd_Played && has_text_cleared_pd_Started && !has_text_cleared_pd_Played)
                {
                    text_cleared_pd.time = text_cleared_pd.duration;
                    text_cleared_pd.Evaluate();
                    
                }
                else
                {
                    Debug.Log("change to => GameMode.WaitForSceneChange");
                    currentGameMode = GameMode.WaitForSceneChange;
                }
                break;

        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == play_target_pd)
        {
            has_play_target_pd_Started = false;
            has_play_target_pd_Played = true;
            
            Debug.Log("play_target_pd has be played.");
        }
        else if (director == beMoved_target_pd)
        {
            enterAlone.SetActive(true);
            has_beMoved_target_pd_Started = false;
            has_beMoved_target_pd_Played = true;
            character1.SetActive(true);
            character2.SetActive(true);
            
            Debug.Log("beMoved_target_pd has be played.");
        }
        else if (director == Text_explainChara_pd)
        {
            has_Text_explainChara_pd_Started = false;
            has_Text_explainChara_pd_Played = true;
            Debug.Log("Text_explainChara_pd has be played.");
        }
        else if (director == s_king_a_pd)
        {
            has_s_king_a_Started = false;
            has_s_king_a_Played = true;
            Debug.Log("s_king_a_pd has be played.");
        }
        else if (director == s_knight_a_pd)
        {
            has_s_knight_a_Started = false;
            has_s_knight_a_Played = true;
            Debug.Log("s_knight_a_pd has be played.");
        }
        else if (director == Text_explainCharaInfo_pd)
        {
            has_Text_explainCharaInfo_pd_Started = false;
            has_Text_explainCharaInfo_pd_Played = true;
            Debug.Log("Text_explainCharaInfo_pd has be played.");
        }
        else if (director == show_two_lines_pd) // 縦線2本を説明するアニメーションが再生終わったら、
        {
            has_show_two_lines_Started = false;
            has_show_two_lines_Played = true;
            lines.SetActive(true); // 本当の縦線2本を表示する
            Debug.Log("show_two_lines_pd has be played.");
        }
        else if (director == show_point_pd)
        {
            has_show_point_pd_Started = false;
            has_show_point_pd_Played = true;
            Debug.Log("show_point_pd has be played.");
        }
        else if (director == text_end_icon_pd)
        {
            has_text_end_icon_pd_Started = false;
            has_text_end_icon_pd_Played = true;
            Debug.Log("text_end_icon_pd has be played.");
        }
        else if (director == text_charaMochibe_pd)
        {
            has_text_charaMochibe_pd_Started = false;
            has_text_charaMochibe_pd_Played = true;
            Debug.Log("text_charaMochibe_pd has be played.");
        }
        else if (director == text_mustEncounter_pd)
        {
            has_text_mustEncounter_pd_Started = false;
            has_text_mustEncounter_pd_Played = true;
            Debug.Log("has_text_mustEncounter_pd_Played :" + has_text_mustEncounter_pd_Played );
            pleaseClick.SetActive(true);
            Debug.Log("text_mustEncounter_pd has be played.");
        }
        else if (director == text_horizontalLineCreated_pd)
        {
            has_text_horizontalLineCreated_pd_Started = false;
            has_text_horizontalLineCreated_pd_Played = true;
            
            Debug.Log("text_horizontalLineCreated_pd has be played.");
        }
        else if (director == text_failed_pd)
        {
            has_text_failed_pd_Started = false;
            has_text_failed_pd_Played = true;
            if (s_king_b != null)
            {
                s_king_b.SetActive(false);
            }
            currentGameMode = GameMode.WaitForSceneChange;
            enterAlone.SetActive(true);
            Debug.Log("text_failed_pd has be played.");
        }
        else if (director == pleaseClick_pd)
        {
            Debug.Log("pleaseClick_pd has be played.");
            Destroy(pleaseClick);
        }
        else if (director == text_noLineCreated_pd)
        {
            has_text_noLineCreated_pd_Started = false;
            has_text_noLineCreated_pd_Played = true;
            
            Debug.Log("text_noLineCreated_pd has be played.");
        }
        else if (director == s_knight_b_pd)
        {
            has_s_knight_b_pd_Started = false;
            has_s_knight_b_pd_Played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("s_knight_b_pd has be played.");
        }
        else if (director == s_king_b_pd)
        {
            has_s_king_b_pd_Started = false;
            has_s_king_b_pd_Played = true;
            
            if (!has_cleared_once)
            {
                text_failed.SetActive(true);
                has_text_failed_pd_Started = true;
            }
            else if (has_cleared_once)
            {
                currentGameMode = GameMode.WaitForSceneChange;
                enterAlone.SetActive(true);
            }
            Debug.Log("s_king_b_pd has be played.");
        }
        else if (director == s_knight_c_pd)
        {
            has_s_knight_c_pd_Started = false;
            has_s_knight_c_pd_Played = true;
            target_cirlcle.SetActive(true);
            if (!has_cleared_once)
            {
                text_cleared.SetActive(true);
                has_text_cleared_pd_Started = true;
            }
            else if (has_cleared_once)
            {
                currentGameMode = GameMode.WaitForSceneChange;
                enterAlone.SetActive(true);
            }
            
            Debug.Log("s_knight_c_pd has be played.");
        }
        else if (director == s_king_c_pd)
        {
            has_s_king_c_pd_Started = false;
            has_s_king_c_pd_Played = true;

            Debug.Log("s_king_c_pd has be played.");
        }
        else if (director == s_knight_d_pd)
        {
            has_s_knight_d_pd_Started = false;
            has_s_knight_d_pd_Played = true;
            
            Debug.Log("s_knight_d_pd has be played.");
        }
        else if (director == s_king_d_pd)
        {
            has_s_king_d_pd_Started = false;
            has_s_king_d_pd_Played = true;
            
            Debug.Log("s_king_d_pd has be played.");
        }
        else if (director == s_knight_f_pd)
        {
            has_s_knight_f_pd_Started = false;
            has_s_knight_f_pd_Played = true;
            Debug.Log("s_knight_f_pd has be played.");
        }
        else if (director == s_king_f_pd)
        {
            has_s_king_f_pd_Started = false;
            has_s_king_f_pd_Played = true;

            Debug.Log("s_king_f_pd has be played.");
        }
        else if (director == s_knight_e_pd)
        {
            has_s_knight_e_pd_Started = false;
            has_s_knight_e_pd_Played = true;
            Debug.Log("s_knight_e_pd has be played.");
        }
        else if (director == s_king_e_pd)
        {
            has_s_king_e_pd_Started = false;
            has_s_king_e_pd_Played = true;
            Debug.Log("s_king_e_pd has be played.");
        }
        else if (director == dialogue_pd)
        {
            has_dialogue_Started = false;
            has_dialogue_Played = true;
            Debug.Log("dialogue_pd has be played.");
        }
        else if (director == text_cleared_pd)
        {
            has_text_cleared_pd_Started = false;
            has_text_cleared_pd_Played = true;
            if (s_knight_c != null)
            {
                s_knight_c.SetActive(false);
            }
            currentGameMode = GameMode.WaitForSceneChange;
            enterAlone.SetActive(true);
            Debug.Log("text_cleared_pd has be played.");
        }
        else if (director == show_cursor_pd)
        {
            text_c.SetActive(true);
            CreateHoverAreaCharacter(character1, 1); // 騎士：１
            CreateHoverAreaCharacter(character2, 3); // 国王：３
            has_show_cursor_played = true;
            has_show_cursor_started = false;
            Debug.Log("show_cursor_pd has be played.");
        }
        else if (director == expla_createLine_pd)
        {
            createLine_show_point_2.SetActive(true);
            Debug.Log("expla_createLine_pd has be played.");
        }
        else if (director == createLine_show_point_2_pd)
        {
            
            Debug.Log("createLine_show_point_2_pd has be played.");
        }
        else if (director == dialogue_pd)
        {
            has_dialogue_Started = false;
            has_dialogue_Played = true;
            Debug.Log("dialogue_pd has be played.");
        }
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

        // T_new_charaHoverAreaScript スクリプトをキャラクターに追加する
        T_new_charaHoverArea T_new_charaHoverAreaScript = character.AddComponent<T_new_charaHoverArea>();

        // T_new_charaHoverAreaScript スクリプトの情報番号を設定する
        T_new_charaHoverAreaScript.charaInfoNum = charaInfoNum;

        // T_new_charaHoverAreaScriptスクリプトを初期化する
        T_new_charaHoverAreaScript.Initialize(character, charaInfoPrefab, charaInfoPosition);
    }

    // 点のペアごとにホバーエリアを生成するメソッド
    void CreateHoverArea(GameObject pointA, GameObject pointB)
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

        // T_new_hoverScript スクリプトをホバーエリアに追加する
        T_new_hoverArea T_new_hoverScript = hoverArea.AddComponent<T_new_hoverArea>();         // Need changed NOTICE！
        T_new_hoverScript.Initialize(pointA, pointB, horizontalLineMaterial, horizontalLineWidth, tooltipPrefab);   // T_new_hoverArea スクリプトを初期化する

        // T_new_hoverScript スクリプトが追加されたことをデバッグログで表示する
        Debug.Log($"T_new_hoverScript added to {hoverArea.name}");

        // ホバーエリアが作成された位置とサイズをデバッグログで表示する
        Debug.Log($"Hover Area created at {midPoint} with size {boxCollider.size}");
    }


    //IEnumerator MoveMenuToTarget(Vector3 targetPosition, float m_speed)
    //{
    //    while (Vector3.Distance(menu.transform.position, targetPosition) > 0.01f)
    //    {
    //        menu.transform.position = Vector3.MoveTowards(menu.transform.position, targetPosition, Time.deltaTime * m_speed);
    //        yield return null;
    //    }
    //    menuIsOnItsPos = true;
    //    menuBack = false;
    //}

    //IEnumerator MoveMenuBack(Vector3 targetPosition, float m_speed)
    //{
    //    while (Vector3.Distance(menu.transform.position, targetPosition) > 0.01f)
    //    {
    //        menu.transform.position = Vector3.MoveTowards(menu.transform.position, targetPosition, Time.deltaTime * m_speed * 6);
    //        yield return null;
    //    }
    //    menuIsOnItsPos = false;
    //    menuBack = true;
    //    menu.SetActive(false);
    //}

    void OnMouseEnter()
    {
        image_menu_hover_white.SetActive(true);
        onMenu = true;
    }

    void OnMouseExit()
    {
        image_menu_hover_white.SetActive(false);
        onMenu = false;
    }
    // マウスがオブジェクト上にある際の処理
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if (menu_switch_on)
            {
                image_menu_button.SetActive(true);

                //// 続ける
                //Time.timeScale = 1; // ゲームを続ける
                //Debug.Log("Game Resumed");
                //targetPosition = menuBackPosition.position;
                //Menu_is_Moving = true;
                menu.SetActive(false);
                menu_switch_on = false;
            }
            else if (!menu_switch_on)
            {
                image_menu_button.SetActive(false);

                ////　一時停止する　
                //Time.timeScale = 0; // ゲーム全体を一時停止する
                //Debug.Log("Game Paused");
                menu.SetActive(true);
                //targetPosition = menuTargetPosition.transform.position;
                //Menu_is_Moving = true;
                waitForSceneChange_Menu();

                menu_switch_on = true;
            }
        }

    }

    void OnDestroy()
    {
        play_target_pd.stopped -= OnPlayableDirectorStopped;
        beMoved_target_pd.stopped -= OnPlayableDirectorStopped;
        s_king_a_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_a_pd.stopped -= OnPlayableDirectorStopped;
        Text_explainChara_pd.stopped -= OnPlayableDirectorStopped;
        Text_explainCharaInfo_pd.stopped -= OnPlayableDirectorStopped;
        show_two_lines_pd.stopped -= OnPlayableDirectorStopped;
        show_point_pd.stopped -= OnPlayableDirectorStopped;
        text_end_icon_pd.stopped -= OnPlayableDirectorStopped;
        text_charaMochibe_pd.stopped -= OnPlayableDirectorStopped;
        pleaseClick_pd.stopped -= OnPlayableDirectorStopped;
        text_failed_pd.stopped -= OnPlayableDirectorStopped;
        text_horizontalLineCreated_pd.stopped -= OnPlayableDirectorStopped;
        text_mustEncounter_pd.stopped -= OnPlayableDirectorStopped;
        text_noLineCreated_pd.stopped -= OnPlayableDirectorStopped;
        s_king_b_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_b_pd.stopped -= OnPlayableDirectorStopped;
        s_king_c_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_c_pd.stopped -= OnPlayableDirectorStopped;
        s_king_d_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_d_pd.stopped -= OnPlayableDirectorStopped;
        dialogue_pd.stopped -= OnPlayableDirectorStopped;
        text_cleared_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_f_pd.stopped -= OnPlayableDirectorStopped;
        s_king_f_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_e_pd.stopped -= OnPlayableDirectorStopped;
        s_king_e_pd.stopped -= OnPlayableDirectorStopped;
        expla_createLine_pd.stopped -= OnPlayableDirectorStopped;
        show_cursor_pd.stopped -= OnPlayableDirectorStopped;
        createLine_show_point_2_pd.stopped -= OnPlayableDirectorStopped;
    }
}
