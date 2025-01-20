using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class T_Again : MonoBehaviour
{
    private bool failed = true;

    public Vector3 character1_initial_pos = new Vector3(-1, 3, -0.2f); // 移動する前にキャラクターの居場所
    public Vector3 character2_initial_pos = new Vector3(2, 3, -0.2f); // 移動する前にキャラクターの居場所

    public GameObject enterAlone;
    public GameObject expla_createLine; // 線をつなぐアニメーション
    public PlayableDirector expla_createLine_pd;
    public GameObject createLine_show_point_2; //　線をつなぐアニメーションの後に説明文
    public GameObject switch_iconExplaMode;

    public GameObject target_cirlcle; // 目標達成時の●

    public bool _isHoriz_line_Created = false; // 横線は接続された？のブール値をfalseとマークする

    public GameObject text_mustEncouter_1;

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
    private bool has_s_knight_a_Started = true;
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

    private bool menu_switch_on = false;
    public GameObject image_menu_hover_white;
    public GameObject image_menu_button;
    public GameObject image_menu_button_pressed;


    public AudioClip leftClickClip;
    public AudioClip rightClickClip;
    public AudioClip missClip;
    public AudioClip deadClip;

    public AudioSource audioSourceTn;

    public AudioClip changeImageClip;

    private bool hasGameStarted = false;
    
    public enum GameMode
    {
        TextPlaying, // ゲーム開始時のテキストが再生中
        PlayerPlaying, // プレイヤーが操作している状態
        WaitForSceneChange, // 現シーンのゲーム内容が終了し、プレイヤーがEnterを押すのを待って次のシーンに切り替える
        Choosing
    }
    public GameMode currentGameMode = GameMode.TextPlaying; // 現シーン開始時にゲームモードをStartTextPlayingに設定

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

        CreateHoverAreaCharacter(character1, 1); // 騎士：１
        CreateHoverAreaCharacter(character2, 3); // 国王：３



        //  開始時にを非表示にする
        text_mustEncouter_1.SetActive(false);
        knight_after.SetActive(false);
        knight_dead.SetActive(false);
        enterAlone.SetActive(false);
        target_cirlcle.SetActive(false);
        s_king_a.SetActive(false);

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
        menu.SetActive(false);
        image_menu_hover_white.SetActive(false);
        expla_createLine.SetActive(false);
        createLine_show_point_2.SetActive(false);

        expla_createLine_pd.stopped += OnPlayableDirectorStopped;
        s_king_a_pd.stopped += OnPlayableDirectorStopped;
        s_knight_a_pd.stopped += OnPlayableDirectorStopped;
        s_king_b_pd.stopped += OnPlayableDirectorStopped;
        s_knight_b_pd.stopped += OnPlayableDirectorStopped;
        s_king_c_pd.stopped += OnPlayableDirectorStopped;
        s_knight_c_pd.stopped += OnPlayableDirectorStopped;
        s_king_d_pd.stopped += OnPlayableDirectorStopped;
        s_knight_d_pd.stopped += OnPlayableDirectorStopped;
        dialogue_pd.stopped += OnPlayableDirectorStopped;
        s_knight_f_pd.stopped += OnPlayableDirectorStopped;
        s_king_f_pd.stopped += OnPlayableDirectorStopped;
        s_knight_e_pd.stopped += OnPlayableDirectorStopped;
        s_king_e_pd.stopped += OnPlayableDirectorStopped;

    }

    // Update is called once per frame
    void Update()
    {

        switch (currentGameMode)
        {
            case GameMode.TextPlaying:

                if (menu_switch_on)
                {
                    
                }
                if (Input.GetMouseButtonDown(0))
                {
                    // 騎士の最初のセリフは再生された、国王の最初のセリフはまだ再生されてない
                    if(has_s_knight_a_Started && !has_s_knight_a_Played && !has_s_king_a_Started && !has_s_king_a_Played)
                    {
                        s_knight_a_pd.time = s_knight_a_pd.duration;
                        s_knight_a_pd.Evaluate();
                    }
                    else if (!has_s_knight_a_Started && has_s_knight_a_Played && !has_s_king_a_Started && !has_s_king_a_Played)
                    {
                        s_knight_a.SetActive(false); // 騎士の最初のセリフを非表示にする
                        s_king_a.SetActive(true); // 国王の最初のセリフを再生する
                        has_s_king_a_Started = true;
                    }
                    else if ( has_s_king_a_Started && !has_s_king_a_Played && !hasGameStarted)
                    {
                        s_king_a_pd.time = s_king_a_pd.duration;
                        s_king_a_pd.Evaluate();
                    }
                    // 国王の最初のセリフは再生された、横線で出会う必要性の説明文はまだ再生されてない
                    else if (!has_s_king_a_Started && has_s_king_a_Played && !hasGameStarted)
                    {
                        s_king_a.SetActive(false); // 国王の最初のセリフを非表示にする

                        CreateHoverArea(tenGameObjects[0], tenGameObjects[3]);
                        text_mustEncouter_1.SetActive(true);
                        currentGameMode = GameMode.PlayerPlaying;
                    }

                    if (has_dialogue_Started && !has_dialogue_Played)
                    {
                        dialogue_pd.time = dialogue_pd.duration;
                        dialogue_pd.Evaluate();
                    }

                    if (has_s_king_b_pd_Started && !has_s_king_b_pd_Played)
                    {
                        s_king_b_pd.time = s_king_b_pd.duration;
                        s_king_b_pd.Evaluate();
                    }

                    if (has_s_king_c_pd_Started && !has_s_king_c_pd_Played)
                    {
                        s_king_c_pd.time = s_king_c_pd.duration;
                        s_king_c_pd.Evaluate();
                    }

                    if (has_s_king_d_pd_Started && !has_s_king_d_pd_Played)
                    {
                        s_king_d_pd.time = s_king_d_pd.duration;
                        s_king_d_pd.Evaluate();
                    }

                    if (has_s_king_e_pd_Started && !has_s_king_e_pd_Played)
                    {
                        s_king_e_pd.time = s_king_e_pd.duration;
                        s_king_e_pd.Evaluate();
                    }

                    if (has_s_king_f_pd_Started && !has_s_king_f_pd_Played)
                    {
                        s_king_f_pd.time = s_king_f_pd.duration;
                        s_king_f_pd.Evaluate();
                    }

                    if (has_s_knight_b_pd_Started && !has_s_knight_b_pd_Played)
                    {
                        s_knight_b_pd.time = s_knight_b_pd.duration;
                        s_knight_b_pd.Evaluate();
                    }

                    if (has_s_knight_c_pd_Started && !has_s_knight_c_pd_Played)
                    {
                        s_knight_c_pd.time = s_knight_c_pd.duration;
                        s_knight_c_pd.Evaluate();
                    }

                    if (has_s_knight_d_pd_Started && !has_s_knight_d_pd_Played)
                    {
                        s_knight_d_pd.time = s_knight_d_pd.duration;
                        s_knight_d_pd.Evaluate();
                    }

                    if (has_s_knight_e_pd_Started && !has_s_knight_e_pd_Played)
                    {
                        s_knight_e_pd.time = s_knight_e_pd.duration;
                        s_knight_e_pd.Evaluate();
                    }

                    if (has_s_knight_f_pd_Started && !has_s_knight_f_pd_Played)
                    {
                        s_knight_f_pd.time = s_knight_f_pd.duration;
                        s_knight_f_pd.Evaluate();
                    }
                }

                break;

            case GameMode.PlayerPlaying:
                if (!hasGameStarted)
                {
                    hasGameStarted = true;
                }

                if (menu_switch_on)
                {
                   
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (_isHoriz_line_Created)
                    {
                        clearedRoute = true;
                        cleared_character_move_logic();
                    }
                    else if (!_isHoriz_line_Created)
                    {
                        failedRoute = true;

                        failed_character_move_logic();
                    }
                }

                break;

            case GameMode.WaitForSceneChange:
                if (menu_switch_on)
                {
                    
                }
                enterAlone.SetActive(true);
                // シーンを切り替える
                if (Input.GetMouseButtonDown(0))
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
                //menu.SetActive(true);
                //waitForSceneChange_Menu();

                break;

            case GameMode.Choosing:

                break;
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
        T_A_hover T_a_hoverScript = FindObjectOfType<T_A_hover>();
        Destroy(T_a_hoverScript.currentLine);
        character1.transform.position = character1_initial_pos;
        character2.transform.position = character2_initial_pos;
        knight_before.SetActive(true);
        knight_after.SetActive(false);
        knight_dead.SetActive(false);
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
        has_s_king_a_Played = false;
        has_s_king_a_Started = false;
        has_s_king_b_pd_Played = false;
        has_s_king_b_pd_Started = false;
        has_s_king_c_pd_Played = false;
        has_s_king_c_pd_Started = false;
        has_s_king_d_pd_Played = false;
        has_s_king_d_pd_Started = false;
        has_s_king_e_pd_Played = false;
        has_s_king_e_pd_Started = false;
        has_s_king_f_pd_Played = false;
        has_s_king_f_pd_Started = false;
        has_s_knight_a_Played = false;
        has_s_knight_a_Started = false;
        has_s_knight_b_pd_Played = false;
        has_s_knight_b_pd_Started = false;
        has_s_knight_c_pd_Played = false;
        has_s_knight_c_pd_Started = false;
        has_s_knight_d_pd_Played = false;
        has_s_knight_d_pd_Started = false;
        has_s_knight_e_pd_Played = false;
        has_s_knight_e_pd_Started = false;
        has_s_knight_f_pd_Played = false;
        has_s_knight_f_pd_Started = false;
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
                StartMovement(new List<int> { 0, 0 }, new List<int> { 1, 1 });
                if (text_mustEncouter_1.activeSelf)
                {
                    text_mustEncouter_1.SetActive(false);
                }
                break;

            case 1:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 5 });
                enterAlone.SetActive(true);

                break;

            case 2:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });
                enterAlone.SetActive(false);
                cannot_createLine = true;
                s_knight_e.SetActive(true);
                has_s_knight_e_pd_Started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 3:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });
                s_knight_e.SetActive(false);
                s_king_e.SetActive(true);
                has_s_king_e_pd_Started = true;
                currentGameMode = GameMode.TextPlaying;

                break;

            case 4:
                StartMovement(new List<int> { 2, 6 }, new List<int> { 5, 5 });
                s_king_e.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 5, 5 });
                audioSourceTn.PlayOneShot(deadClip);
                s_knight_b.SetActive(true);
                has_s_knight_b_pd_Started = true;
                currentGameMode = GameMode.TextPlaying;
                knight_before.SetActive(false);
                knight_dead.SetActive(true);
                enterAlone.SetActive(false);
                break;

            case 6:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 5, 7 });
                s_knight_b.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 });
                enterAlone.SetActive(false);
                s_king_b.SetActive(true);
                has_s_king_b_pd_Started = true;
                currentGameMode = GameMode.TextPlaying;
                if (has_cleared_once)
                {
                    failed = false;
                }
                currentGameMode = GameMode.TextPlaying;
                break;



        }
    }

    public void cleared_character_move_logic() // 横線が接続された場合、キャラクターたちの移動ロジック
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 5 });
                if (text_mustEncouter_1.activeSelf)
                {
                    text_mustEncouter_1.SetActive(false);
                }

                enterAlone.SetActive(true);
                break;

            case 1:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });
                cannnot_cancell = true;
                enterAlone.SetActive(false);
                s_knight_f.SetActive(true);
                has_s_knight_f_pd_Started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 2:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });
                s_knight_f.SetActive(false);
                s_king_f.SetActive(true);
                has_s_king_f_pd_Started = true;
                currentGameMode = GameMode.TextPlaying;

                break;

            case 3:
                StartMovement(new List<int> { 2, 3 }, new List<int> { 5, 4 });
                enterAlone.SetActive(true);
                s_king_f.SetActive(false);
                break;

            case 4:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 4 });
                enterAlone.SetActive(false);
                dialogue.SetActive(true);
                has_dialogue_Started = true;
                currentGameMode = GameMode.TextPlaying;
                knight_before.SetActive(false);
                break;

            case 5:
                knight_after.SetActive(true);
                dialogue.SetActive(false);
                StartMovement(new List<int> { 3, 4 }, new List<int> { 4, 3 });
                enterAlone.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 2 });

                break;

            case 7:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 2, 2 });
                enterAlone.SetActive(false);
                s_king_d.SetActive(true);
                has_s_king_d_pd_Started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 8:
                StartMovement(new List<int> { 4, 5 }, new List<int> { 2, 2 });
                s_king_d.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 2, 2 });
                s_knight_d.SetActive(true);
                has_s_knight_d_pd_Started = true;
                currentGameMode = GameMode.TextPlaying;
                enterAlone.SetActive(false);
                break;

            case 10:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 2, 6 });
                s_knight_d.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 11:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 6, 6 });
                s_king_c.SetActive(true);
                has_s_king_c_pd_Started = true;
                currentGameMode = GameMode.TextPlaying;
                enterAlone.SetActive(false);
                break;

            case 12:
                StartMovement(new List<int> { 5, 7 }, new List<int> { 6, 6 });
                s_king_c.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 13:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 6 });
                s_knight_c.SetActive(true);
                has_s_knight_c_pd_Started = true;
                currentGameMode = GameMode.TextPlaying;
                enterAlone.SetActive(false);
                failed = false;
                if (!has_cleared_once)
                {
                    has_cleared_once = true;
                }
                currentGameMode = GameMode.TextPlaying;
                break;

        }
    }


    private void OnMouseEnter()
    {
        image_menu_hover_white.SetActive(true);
    }

    private void OnMouseExit()
    {
        image_menu_hover_white.SetActive(false);
    }
    // マウスがオブジェクト上にある際の処理
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (menu_switch_on)
            {
                image_menu_button.SetActive(true);

                menu.SetActive(false);
                menu_switch_on = false;
            }
            else if (!menu_switch_on)
            {
                image_menu_button.SetActive(false);

                menu.SetActive(true);
                

                menu_switch_on = true;
            }
        }

    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == s_king_a_pd)
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
                //
                currentGameMode = GameMode.WaitForSceneChange;
                enterAlone.SetActive(true);
                Debug.Log("まだクリアしたことない");
            }
            else if (has_cleared_once)
            {
                currentGameMode = GameMode.WaitForSceneChange;
                enterAlone.SetActive(true);
                Debug.Log("死亡エンディングになったけど、既にクリアしたことある");
            }
            Debug.Log("s_king_b_pd has be played.");
        }
        else if (director == s_knight_c_pd)
        {
            has_s_knight_c_pd_Started = false;
            has_s_knight_c_pd_Played = true;
            target_cirlcle.SetActive(true);
            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("s_knight_c_pd has be played.");
        }
        else if (director == s_king_c_pd)
        {
            has_s_king_c_pd_Started = false;
            has_s_king_c_pd_Played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("s_king_c_pd has be played.");
        }
        else if (director == s_knight_d_pd)
        {
            has_s_knight_d_pd_Started = false;
            has_s_knight_d_pd_Played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("s_knight_d_pd has be played.");
        }
        else if (director == s_king_d_pd)
        {
            has_s_king_d_pd_Started = false;
            has_s_king_d_pd_Played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("s_king_d_pd has be played.");
        }
        else if (director == s_knight_f_pd)
        {
            has_s_knight_f_pd_Started = false;
            has_s_knight_f_pd_Played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("s_knight_f_pd has be played.");
        }
        else if (director == s_king_f_pd)
        {
            has_s_king_f_pd_Started = false;
            has_s_king_f_pd_Played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("s_king_f_pd has be played.");
        }
        else if (director == s_knight_e_pd)
        {
            has_s_knight_e_pd_Started = false;
            has_s_knight_e_pd_Played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("s_knight_e_pd has be played.");
        }
        else if (director == s_king_e_pd)
        {
            has_s_king_e_pd_Started = false;
            has_s_king_e_pd_Played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("s_king_e_pd has be played.");
        }
        else if (director == dialogue_pd)
        {
            has_dialogue_Started = false;
            has_dialogue_Played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("dialogue_pd has be played.");
        }
        else if (director == expla_createLine_pd)
        {
            createLine_show_point_2.SetActive(true);
            Debug.Log("expla_createLine_pd has be played.");
        }
        
    }

    void OnDestroy()
    {
        s_king_a_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_a_pd.stopped -= OnPlayableDirectorStopped;
        s_king_b_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_b_pd.stopped -= OnPlayableDirectorStopped;
        s_king_c_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_c_pd.stopped -= OnPlayableDirectorStopped;
        s_king_d_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_d_pd.stopped -= OnPlayableDirectorStopped;
        dialogue_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_f_pd.stopped -= OnPlayableDirectorStopped;
        s_king_f_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_e_pd.stopped -= OnPlayableDirectorStopped;
        s_king_e_pd.stopped -= OnPlayableDirectorStopped;
        expla_createLine_pd.stopped -= OnPlayableDirectorStopped;
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

        // T_a_charaHoverAreaScript スクリプトをキャラクターに追加する
        T_A_charaHover T_a_charaHoverAreaScript = character.AddComponent<T_A_charaHover>();

        // T_a_charaHoverAreaScript スクリプトの情報番号を設定する
        T_a_charaHoverAreaScript.charaInfoNum = charaInfoNum;

        // T_a_charaHoverAreaScriptスクリプトを初期化する
        T_a_charaHoverAreaScript.Initialize(character, charaInfoPrefab, charaInfoPosition);
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

        // T_a_hoverScript スクリプトをホバーエリアに追加する
        T_A_hover T_a_hoverScript = hoverArea.AddComponent<T_A_hover>();         // Need changed NOTICE！
        T_a_hoverScript.Initialize(pointA, pointB, horizontalLineMaterial, horizontalLineWidth, tooltipPrefab);   // T_a_hoverArea スクリプトを初期化する

        // T_a_hoverScript スクリプトが追加されたことをデバッグログで表示する
        Debug.Log($"T_a_hoverScript added to {hoverArea.name}");

        // ホバーエリアが作成された位置とサイズをデバッグログで表示する
        Debug.Log($"Hover Area created at {midPoint} with size {boxCollider.size}");
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
}
