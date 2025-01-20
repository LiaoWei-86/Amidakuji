using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class J1_GameController : MonoBehaviour
{
    public GameObject enterAlone;
    public GameObject game_start;
    public PlayableDirector game_start_pd;
    private bool game_start_played = false;
    [SerializeField] private float frameRate = 60.0f;

    public Transform character1_initial_pos; // 移動する前にキャラクターの居場所
    public Transform character2_initial_pos; // 移動する前にキャラクターの居場所

    public GameObject gift_opening; // プレゼントの箱はあけられてるアニメーション
    public PlayableDirector gift_opening_pd; // そのPlayableDirector
    private bool gift_is_dog = false;

    public GameObject JyomakuText; // ゲームオブジェクト JyomakuText
    public PlayableDirector JyomakuTextPlayableDirector; // JyomakuTextのPlayableDirector

    public GameObject targetText; // ゲームオブジェクト targetText
    public PlayableDirector targetTextPlayableDirector; // targetTextのPlayableDirector
    private bool has_targetTextPlayableDirector_played = false;

    public GameObject targetCompletedText; // ゲームオブジェクト targetCompletedText
    public PlayableDirector targetCompletedTextPlayableDirector; // targetCompletedTextのPlayableDirector

    public GameObject targetText_2; // ゲームオブジェクト targetText
    public PlayableDirector targetText_2_PlayableDirector; // targetTextのPlayableDirector
    private bool has_targetText_2_PlayableDirector_played = false;

    public GameObject targetCompletedText_2; // ゲームオブジェクト targetCompletedText
    public PlayableDirector targetCompletedText_2_PlayableDirector; // targetCompletedTextのPlayableDirector

    public GameObject dialogue_B2; // dialogue_B2
    public PlayableDirector dialogue_B2_PlayableDirector; //  dialogue_B2のPlayableDirector
    private bool has_dialogue_B2_started = false;
    private bool has_dialogue_B2_played = false;
    private bool has_dialogue_B2_p2_started = false;
    private bool has_dialogue_B2_p1_played = false;

    public GameObject s_knight_a;
    public PlayableDirector s_knight_a_pd;
    private bool has_s_knight_a_pd_played = false;
    private bool has_s_knight_a_pd_started = false;

    public GameObject s_hunter_a;
    public PlayableDirector s_hunter_a_pd;
    private bool has_s_hunter_a_pd_played = false;
    private bool has_s_hunter_a_pd_started = false;

    // 結末アイコンにたどり着いた時のセリフ

    // 失敗の場合
    public GameObject dialogue_failed_hunter; // ゲームオブジェクト dialogue_failed_hunter
    public PlayableDirector dialogue_failed_hunterPlayableDirector; // dialogue_failed_hunterのPlayableDirector
    private bool has_dialogue_failed_hunter_started = false;
    private bool has_dialogue_failed_hunter_played = false;

    public GameObject dialogue_failed_knight; // ゲームオブジェクト dialogue_failed_knight
    public PlayableDirector dialogue_failed_knightPlayableDirector; // dialogue_failed_knightのPlayableDirector
    private bool has_dialogue_failed_knight_played = false;
    private bool has_dialogue_failed_knight_started = false;

    // 酒場だけに行った場合
    public GameObject dialogue_beer_hunter; // ゲームオブジェクト dialogue_beer_hunter
    public PlayableDirector dialogue_beer_hunterPlayableDirector; // dialogue_beer_hunterのPlayableDirector
    private bool has_dialogue_beer_hunter_started = false;
    private bool has_dialogue_beer_hunter_played = false;

    public GameObject dialogue_beer_knight; // ゲームオブジェクト dialogue_beer_knight
    public PlayableDirector dialogue_beer_knightPlayableDirector; // dialogue_beer_knightのPlayableDirector
    private bool has_dialogue_beer_knight_played = false;
    private bool has_dialogue_beer_knight_started = false;

    // 戦って酒場だけに行った場合
    public GameObject dialogue_battleBeer_hunter; // ゲームオブジェクト dialogue_battleBeer_hunter
    public PlayableDirector dialogue_battleBeer_hunterPlayableDirector; // dialogue_battleBeer_hunterのPlayableDirector
    private bool has_dialogue_battleBeer_hunter_started = false;
    private bool has_dialogue_battleBeer_hunter_played = false;

    public GameObject dialogue_battleBeer_knight; // ゲームオブジェクト dialogue_battleBeer_knight
    public PlayableDirector dialogue_battleBeer_knightPlayableDirector; // dialogue_battleBeer_knightのPlayableDirector
    private bool has_dialogue_battleBeer_knight_played = false;
    private bool has_dialogue_battleBeer_knight_started = false;

    // 線を引かず、そのまま結末アイコンにたどり着いた場合
    public GameObject dialogue_OE_hunter; // ゲームオブジェクト dialogue_OE_hunter
    public PlayableDirector dialogue_OE_hunterPlayableDirector; // dialogue_OE_hunterのPlayableDirector
    private bool has_dialogue_OE_hunter_started = false;
    private bool has_dialogue_OE_hunter_played = false;

    public GameObject dialogue_OE_knight; // ゲームオブジェクト dialogue_OE_knight
    public PlayableDirector dialogue_OE_knightPlayableDirector; // dialogue_OE_knightのPlayableDirector
    private bool has_dialogue_OE_knight_started = false;
    private bool has_dialogue_OE_knight_played = false;

    public Transform startpoint_knight; // 騎士のスタート点の位置
    public Transform startpoint_hunter; // 猟師のスタート点の位置
    public GameObject[] tenGameObjects; // それぞれの点
    public Transform[] tenPositions; // それぞれの点の位置
    public Transform endtpoint_knight; // 左から1番目の終点の位置
    public Transform endpoint_hunter; // 左から2番目の終点の位置
    public Dictionary<int, Vector3> pointsDictionary;// 点とその位置情報を格納する辞書

    public bool isHorizontal_1_LineCreated = false; //　1本目の横線は描かれたか？のブール値
    public bool isHorizontal_2_LineCreated = false; //　2本目の横線は描かれたか？のブール値

    public GameObject fukidashi_0; // 吹き出し-0  のゲームオブジェクト
    public PlayableDirector dialogue_0; // セリフ-0  のPlayableDirector
    private bool has_diaolgue_0_started = false;
    private bool has_diaolgue_0_played = false;
    private bool has_diaolgue_0_p2_started = false;
    private bool has_diaolgue_0_p1_played = false;
    private bool has_diaolgue_0_p3_started = false;
    private bool has_diaolgue_0_p2_played = false;
    private bool has_diaolgue_0_p4_started = false;
    private bool has_diaolgue_0_p3_played = false;
    private bool has_diaolgue_0_p5_started = false;
    private bool has_diaolgue_0_p4_played = false;

    public GameObject fukidashi; // 吹き出しのゲームオブジェクト
    public PlayableDirector dialogue; // セリフのPlayableDirector
    private bool has_diaolgue_started = false;
    private bool has_diaolgue_played = false;
    private bool has_diaolgue_p2_started = false;
    private bool has_diaolgue_p1_played = false;

    public GameObject dog; // 犬

    public GameObject knight;
    public GameObject hunter;

    private Coroutine knightMovementCoroutine;
    private Coroutine hunterMovementCoroutine;

    public bool hasMovementFinshed = false; //　キャラクターの移動は完成されたか？のブール値
    public bool cleared = false; //　クリア？のブール値をfalseとマークする

    private int currentMovementIndex = 0; // プレイヤーがEnterを押す際にプロットアイコンを生成するために計算用のIndex

    public float speed = 3.0f;


    public GameObject tooltipPrefab; // 提示枠のプレハブ
    public Material horizontalLineMaterial; // 横線のマテリアル
    public float horizontalLineWidth = 0.05f; // 横線の幅
    public float hoverAreaWidth = 0.8f; // 横線のホバーエリアの縦幅

    public bool isAnyHorizontalLineCreated; // なん本でもいいから横線は接続されたか？のブール値

    public TMP_FontAsset dotFont;  // ドットフォント
    public bool charaKnightInfoChecked = false; //　騎士のキャラクター情報は確認されたか？のブール値
    public bool charaHunterInfoChecked = false; //　猟師のキャラクター情報は確認されたか？のブール値

    public GameObject charaInfoPrefab;


    public bool canCreateLine_1 = true; // 今は上の横線をつなぐことができるか？をtrueとマークする
    public bool canCreateLine_2 = true; // 今は下の横線をつなぐことができるか？をtrueとマークする
    public bool canDeleteLine_1 = true; // 今は上の横線を削除することができるか？をtrueとマークする
    public bool canDeleteLine_2 = true; // 今は下の横線を削除することができるか？をtrueとマークする

    public bool has_cleared_once = false; // 1度クリアできた？をfalseとマークする

    public AudioClip leftClickClip;
    public AudioClip rightClickClip;
    public AudioClip missClip;
    public AudioClip barkClip;

    public AudioSource audioSourceJ1;

    //  追加したセリフ

    public GameObject s_knight_battle_a; // ゲームオブジェクト s_knight_battle_a   バトルアイコンですれ違う前の騎士
    public PlayableDirector s_knight_battle_a_pd; // s_knight_battle_aのPlayableDirector
    private bool has_s_knight_battle_a_started = false;
    private bool has_s_knight_battle_a_played = false;

    public GameObject s_hunter_battle_a; // ゲームオブジェクト s_hunter_battle_a  バトルアイコンですれ違う前の猟師
    public PlayableDirector s_hunter_battle_a_pd; // s_hunter_battle_aのPlayableDirector
    private bool has_s_hunter_battle_a_started = false;
    private bool has_s_hunter_battle_a_played = false;

    public GameObject s_knight_battle_b; // ゲームオブジェクト s_knight_battle_a  バトルアイコンですれ違った後の騎士
    public PlayableDirector s_knight_battle_b_pd; // s_knight_battle_aのPlayableDirector
    private bool has_s_knight_battle_b_started = false;
    private bool has_s_knight_battle_b_played = false;

    public GameObject s_hunter_battle_b; // ゲームオブジェクト s_hunter_battle_a  バトルアイコンですれ違った後の猟師
    public PlayableDirector s_hunter_battle_b_pd; // s_hunter_battle_aのPlayableDirector
    private bool has_s_hunter_battle_b_started = false;
    private bool has_s_hunter_battle_b_played = false;

    public GameObject s_knight_1_beer; // ゲームオブジェクト s_knight_1_beer  ルート1,ビルアイコンの隣の騎士
    public PlayableDirector s_knight_1_beer_pd; // s_knight_1_beerのPlayableDirector
    private bool has_s_knight_1_beer_started = false;
    private bool has_s_knight_1_beer_played = false;

    public GameObject s_hunter_1_beer; // ゲームオブジェクト s_hunter_1_beer  ルート1,ビルアイコンの隣の猟師
    public PlayableDirector s_hunter_1_beer_pd; //  s_hunter_1_beerのPlayableDirector
    private bool has_s_hunter_1_beer_started = false;
    private bool has_s_hunter_1_beer_played = false;

    public GameObject s_knight_2_beer; // ゲームオブジェクト s_knight_2_beer  ルート2,ビルアイコンの隣の騎士
    public PlayableDirector s_knight_2_beer_pd; // s_knight_1_beerのPlayableDirector
    private bool has_s_knight_2_beer_started = false;
    private bool has_s_knight_2_beer_played = false;

    public GameObject s_hunter_2_beer; // ゲームオブジェクト s_hunter_2_beer  ルート2,ビルアイコンの隣の猟師
    public PlayableDirector s_hunter_2_beer_pd; //  s_hunter_1_beerのPlayableDirector
    private bool has_s_hunter_2_beer_started = false;
    private bool has_s_hunter_2_beer_played = false;

    public GameObject s_knight_3_beer_a; // ゲームオブジェクト s_knight_3_beer_a   ルート3: ビルアイコンですれ違う前の騎士
    public PlayableDirector s_knight_3_beer_a_pd; // s_knight_3_beer_aのPlayableDirector
    private bool has_s_knight_3_beer_a_started = false;
    private bool has_s_knight_3_beer_a_played = false;

    public GameObject s_hunter_3_beer_a; // ゲームオブジェクト s_hunter_3_beer_a  ルート3: ビルアイコンですれ違う前の猟師
    public PlayableDirector s_hunter_3_beer_a_pd; // s_hunter_3_beer_aのPlayableDirector
    private bool has_s_hunter_3_beer_a_started = false;
    private bool has_s_hunter_3_beer_a_played = false;

    public GameObject s_knight_3_beer_b; // ゲームオブジェクト s_knight_3_beer_a  ルート3: ビルアイコンですれ違った後の騎士
    public PlayableDirector s_knight_3_beer_b_pd; // s_knight_3_beer_aのPlayableDirector
    private bool has_s_knight_3_beer_b_started = false;
    private bool has_s_knight_3_beer_b_played = false;

    public GameObject s_hunter_3_beer_b; // ゲームオブジェクト s_hunter_3_beer_a  ルート3: ビルアイコンですれ違った後の猟師
    public PlayableDirector s_hunter_3_beer_b_pd; // s_hunter_3_beer_aのPlayableDirector
    private bool has_s_hunter_3_beer_b_started = false;
    private bool has_s_hunter_3_beer_b_played = false;

    public GameObject s_knight_4_beer_a; // ゲームオブジェクト s_knight_4_beer_a   ルート4: ビルアイコンですれ違う前の騎士
    public PlayableDirector s_knight_4_beer_a_pd; // s_knight_4_beer_aのPlayableDirector
    private bool has_s_knight_4_beer_a_started = false;
    private bool has_s_knight_4_beer_a_played = false;

    public GameObject s_hunter_4_beer_a; // ゲームオブジェクト s_hunter_4_beer_a  ルート4: ビルアイコンですれ違う前の猟師
    public PlayableDirector s_hunter_4_beer_a_pd; // s_hunter_4_beer_aのPlayableDirector
    private bool has_s_hunter_4_beer_a_started = false;
    private bool has_s_hunter_4_beer_a_played = false;

    public GameObject s_knight_4_beer_b; // ゲームオブジェクト s_knight_4_beer_a  ルート4: ビルアイコンですれ違った後の騎士
    public PlayableDirector s_knight_4_beer_b_pd; // s_knight_4_beer_aのPlayableDirector
    private bool has_s_knight_4_beer_b_started = false;
    private bool has_s_knight_4_beer_b_played = false;

    public GameObject s_hunter_4_beer_b; // ゲームオブジェクト s_hunter_4_beer_a  ルート4: ビルアイコンですれ違った後の猟師
    public PlayableDirector s_hunter_4_beer_b_pd; // s_hunter_4_beer_aのPlayableDirector
    private bool has_s_hunter_4_beer_b_started = false;
    private bool has_s_hunter_4_beer_b_played = false;

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
        CreateHoverAreaCharacter(knight, 1);
        CreateHoverAreaCharacter(hunter, 2);

        //  開始時にを非表示にする
        s_knight_a.SetActive(false);
        s_hunter_a.SetActive(false);
        game_start.SetActive(false);
        if (targetText != null)
        {
            targetText.SetActive(false);
        }
        if (targetText_2 != null)
        {
            targetText_2.SetActive(false);
        }
        if (fukidashi_0 != null)
        {
            fukidashi_0.SetActive(false);
        }
        if (fukidashi != null)
        {
            fukidashi.SetActive(false);
        }
        if (dog != null)
        {
            dog.SetActive(false);
        }
        if (targetCompletedText != null)
        {
            targetCompletedText.SetActive(false);
        }
        if (dialogue_failed_hunter != null)
        {
            dialogue_failed_hunter.SetActive(false);
        }
        if (dialogue_failed_knight != null)
        {
            dialogue_failed_knight.SetActive(false);
        }
        if (dialogue_OE_hunter != null)
        {
            dialogue_OE_hunter.SetActive(false);
        }
        if (dialogue_OE_knight != null)
        {
            dialogue_OE_knight.SetActive(false);
        }
        if (dialogue_beer_hunter != null)
        {
            dialogue_beer_hunter.SetActive(false);
        }
        if (dialogue_beer_knight != null)
        {
            dialogue_beer_knight.SetActive(false);
        }
        if (dialogue_battleBeer_hunter != null)
        {
            dialogue_battleBeer_hunter.SetActive(false);
        }
        if (dialogue_battleBeer_knight != null)
        {
            dialogue_battleBeer_knight.SetActive(false);
        }
        dialogue_B2.SetActive(false);
        gift_opening.SetActive(false);
        s_knight_battle_a.SetActive(false);
        s_hunter_battle_a.SetActive(false);
        s_knight_battle_b.SetActive(false);
        s_hunter_battle_b.SetActive(false);
        s_knight_1_beer.SetActive(false);
        s_hunter_1_beer.SetActive(false);
        s_knight_2_beer.SetActive(false);
        s_hunter_2_beer.SetActive(false);
        s_knight_3_beer_a.SetActive(false);
        s_hunter_3_beer_a.SetActive(false);
        s_knight_3_beer_b.SetActive(false);
        s_hunter_3_beer_b.SetActive(false);
        s_knight_4_beer_a.SetActive(false);
        s_hunter_4_beer_a.SetActive(false);
        s_knight_4_beer_b.SetActive(false);
        s_hunter_4_beer_b.SetActive(false);
        targetCompletedText_2.SetActive(false);
        enterAlone.SetActive(false);

        // PlayableDirectorがnullでないことを確認し、再生完了イベントをサブスクライブ
        s_knight_a_pd.stopped += OnPlayableDirectorStopped;
        s_hunter_a_pd.stopped += OnPlayableDirectorStopped;
        game_start_pd.stopped += OnPlayableDirectorStopped;
        if (JyomakuTextPlayableDirector != null)
        {
            JyomakuTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("JyomakuTextPlayableDirector is not assigned.");
        }
        if (targetTextPlayableDirector != null)
        {
            targetTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("targetTextPlayableDirector is not assigned.");
        }

        if (targetCompletedTextPlayableDirector != null)
        {
            targetCompletedTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (dialogue_B2_PlayableDirector != null)
        {
            dialogue_B2_PlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("befriendPlayableDirector is not assigned.");
        }
        if (dialogue != null)
        {
            dialogue.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("dialogue is not assigned.");
        }



        if (dialogue_failed_hunterPlayableDirector != null)
        {
            dialogue_failed_hunterPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (dialogue_failed_knightPlayableDirector != null)
        {
            dialogue_failed_knightPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (dialogue_OE_hunterPlayableDirector != null)
        {
            dialogue_OE_hunterPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (dialogue_OE_knightPlayableDirector != null)
        {
            dialogue_OE_knightPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (dialogue_beer_hunterPlayableDirector != null)
        {
            dialogue_beer_hunterPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (dialogue_beer_knightPlayableDirector != null)
        {
            dialogue_beer_knightPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (dialogue_battleBeer_hunterPlayableDirector != null)
        {
            dialogue_battleBeer_hunterPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (dialogue_battleBeer_knightPlayableDirector != null)
        {
            dialogue_battleBeer_knightPlayableDirector.stopped += OnPlayableDirectorStopped;
        }

        s_knight_battle_a_pd.stopped += OnPlayableDirectorStopped;
        s_hunter_battle_a_pd.stopped += OnPlayableDirectorStopped;
        s_knight_battle_b_pd.stopped += OnPlayableDirectorStopped;
        s_hunter_battle_b_pd.stopped += OnPlayableDirectorStopped;
        s_knight_1_beer_pd.stopped += OnPlayableDirectorStopped;
        s_hunter_1_beer_pd.stopped += OnPlayableDirectorStopped;
        s_knight_2_beer_pd.stopped += OnPlayableDirectorStopped;
        s_hunter_2_beer_pd.stopped += OnPlayableDirectorStopped;
        s_knight_3_beer_a_pd.stopped += OnPlayableDirectorStopped;
        s_hunter_3_beer_a_pd.stopped += OnPlayableDirectorStopped;
        s_knight_3_beer_b_pd.stopped += OnPlayableDirectorStopped;
        s_hunter_3_beer_b_pd.stopped += OnPlayableDirectorStopped;
        s_knight_4_beer_a_pd.stopped += OnPlayableDirectorStopped;
        s_hunter_4_beer_a_pd.stopped += OnPlayableDirectorStopped;
        s_knight_4_beer_b_pd.stopped += OnPlayableDirectorStopped;
        s_hunter_4_beer_b_pd.stopped += OnPlayableDirectorStopped;
        gift_opening_pd.stopped += OnPlayableDirectorStopped;
        targetText_2_PlayableDirector.stopped += OnPlayableDirectorStopped;

        // Initialize the dictionary and add the points
        pointsDictionary = new Dictionary<int, Vector3>();

        // Add knight and hunter start and end points
        pointsDictionary.Add(0, startpoint_knight.position); // 左から1番目のスタート点
        pointsDictionary.Add(1, startpoint_hunter.position); // 左から2番目のスタート点
        pointsDictionary.Add(10, endtpoint_knight.position);  // 左から1番目の終点
        pointsDictionary.Add(11, endpoint_hunter.position);   // 左から2番目の終点

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


    }

    // Update is called once per frame
    void Update()
    {

        switch (currentGameMode)
        {
            case GameMode.TextPlaying:
                pauseAni();
                if (Input.GetMouseButtonDown(0))  // Input.GetKeyDown(KeyCode.Return)
                {
                    if (has_targetText_2_PlayableDirector_played && !has_s_knight_a_pd_started && !has_s_knight_a_pd_played && !has_s_hunter_a_pd_started && !has_s_hunter_a_pd_played)
                    {
                        if (enterAlone != null)
                        {
                            enterAlone.SetActive(false);
                        }
                        s_knight_a.SetActive(true);
                        has_s_knight_a_pd_started = true;
                    }
                    else if (has_s_knight_a_pd_started && !has_s_knight_a_pd_played && !has_s_hunter_a_pd_started && !has_s_hunter_a_pd_played)
                    {
                        s_knight_a_pd.time = s_knight_a_pd.duration;
                        s_knight_a_pd.Evaluate();
                    }
                    else if (!has_s_knight_a_pd_started && has_s_knight_a_pd_played && !has_s_hunter_a_pd_started && !has_s_hunter_a_pd_played)
                    {
                        s_knight_a.SetActive(false);
                        s_hunter_a.SetActive(true);
                        has_s_hunter_a_pd_started = true;
                    }
                    else if (has_s_hunter_a_pd_started && !has_s_hunter_a_pd_played)
                    {
                        s_hunter_a_pd.time = s_hunter_a_pd.duration;
                        s_hunter_a_pd.Evaluate();
                    }
                    else if (!has_s_hunter_a_pd_started && has_s_hunter_a_pd_played && !game_start_played)
                    {
                        s_hunter_a.SetActive(false);
                        CreateHoverAreaJ1(tenGameObjects[0], tenGameObjects[3]);
                        CreateHoverAreaJ1(tenGameObjects[4], tenGameObjects[7]);
                        game_start.SetActive(true);
                    }

                    if (has_dialogue_failed_hunter_started && !has_dialogue_failed_hunter_played)
                    {
                        dialogue_failed_hunterPlayableDirector.time = dialogue_failed_hunterPlayableDirector.duration;
                        dialogue_failed_hunterPlayableDirector.Evaluate();
                    }

                    if (has_dialogue_failed_knight_started && !has_dialogue_failed_knight_played)
                    {
                        dialogue_failed_knightPlayableDirector.time = dialogue_failed_knightPlayableDirector.duration;
                        dialogue_failed_knightPlayableDirector.Evaluate();
                    }

                    if (has_dialogue_beer_hunter_started && !has_dialogue_beer_hunter_played)
                    {
                        dialogue_beer_hunterPlayableDirector.time = dialogue_beer_hunterPlayableDirector.duration;
                        dialogue_beer_hunterPlayableDirector.Evaluate();
                    }

                    if (has_dialogue_beer_knight_started && !has_dialogue_beer_knight_played)
                    {
                        dialogue_beer_knightPlayableDirector.time = dialogue_beer_knightPlayableDirector.duration;
                        dialogue_beer_knightPlayableDirector.Evaluate();
                    }

                    if(has_dialogue_battleBeer_hunter_started && !has_dialogue_battleBeer_hunter_played)
                    {
                        dialogue_battleBeer_hunterPlayableDirector.time = dialogue_battleBeer_hunterPlayableDirector.duration;
                        dialogue_battleBeer_hunterPlayableDirector.Evaluate();
                    }

                    if (has_dialogue_battleBeer_knight_started && !has_dialogue_battleBeer_knight_played)
                    {
                        dialogue_battleBeer_knightPlayableDirector.time = dialogue_battleBeer_knightPlayableDirector.duration;
                        dialogue_battleBeer_knightPlayableDirector.Evaluate();
                    }

                    if (has_dialogue_OE_hunter_started && !has_dialogue_OE_hunter_played)
                    {
                        dialogue_OE_hunterPlayableDirector.time = dialogue_OE_hunterPlayableDirector.duration;
                        dialogue_OE_hunterPlayableDirector.Evaluate();
                    }

                    if (has_dialogue_OE_knight_started && !has_dialogue_OE_knight_played)
                    {
                        dialogue_OE_knightPlayableDirector.time = dialogue_OE_knightPlayableDirector.duration;
                        dialogue_OE_knightPlayableDirector.Evaluate();
                    }

                    if (has_s_knight_battle_a_started && !has_s_knight_battle_a_played)
                    {
                        s_knight_battle_a_pd.time = s_knight_battle_a_pd.duration;
                        s_knight_battle_a_pd.Evaluate();
                    }

                    if (has_s_knight_battle_b_started && !has_s_knight_battle_b_played)
                    {
                        s_knight_battle_b_pd.time = s_knight_battle_b_pd.duration;
                        s_knight_battle_b_pd.Evaluate();
                    }

                    if (has_s_hunter_battle_a_started && !has_s_hunter_battle_a_played)
                    {
                        s_hunter_battle_a_pd.time = s_hunter_battle_a_pd.duration;
                        s_hunter_battle_a_pd.Evaluate();
                    }

                    if (has_s_hunter_battle_b_started && !has_s_hunter_battle_b_played)
                    {
                        s_hunter_battle_b_pd.time = s_hunter_battle_b_pd.duration;
                        s_hunter_battle_b_pd.Evaluate();
                    }

                    if (has_s_knight_1_beer_started && !has_s_knight_1_beer_played)
                    {
                        s_knight_1_beer_pd.time = s_knight_1_beer_pd.duration;
                        s_knight_1_beer_pd.Evaluate();
                    }

                    if (has_s_hunter_1_beer_started && !has_s_hunter_1_beer_played)
                    {
                        s_hunter_1_beer_pd.time = s_hunter_1_beer_pd.duration;
                        s_hunter_1_beer_pd.Evaluate();
                    }

                    if (has_s_knight_2_beer_started && !has_s_knight_2_beer_played)
                    {
                        s_knight_2_beer_pd.time = s_knight_2_beer_pd.duration;
                        s_knight_2_beer_pd.Evaluate();
                    }

                    if (has_s_hunter_2_beer_started && !has_s_hunter_2_beer_played)
                    {
                        s_hunter_2_beer_pd.time = s_hunter_2_beer_pd.duration;
                        s_hunter_2_beer_pd.Evaluate();
                    }

                    if (has_s_knight_3_beer_a_started && !has_s_knight_3_beer_a_played)
                    {
                        s_knight_3_beer_a_pd.time = s_knight_3_beer_a_pd.duration;
                        s_knight_3_beer_a_pd.Evaluate();
                    }

                    if (has_s_knight_3_beer_b_started && !has_s_knight_3_beer_b_played)
                    {
                        s_knight_3_beer_b_pd.time = s_knight_3_beer_b_pd.duration;
                        s_knight_3_beer_b_pd.Evaluate();
                    }

                    if (has_s_hunter_3_beer_a_started && !has_s_hunter_3_beer_a_played)
                    {
                        s_hunter_3_beer_a_pd.time = s_hunter_3_beer_a_pd.duration;
                        s_hunter_3_beer_a_pd.Evaluate();
                    }

                    if (has_s_hunter_3_beer_b_started && !has_s_hunter_3_beer_b_played)
                    {
                        s_hunter_3_beer_b_pd.time = s_hunter_3_beer_b_pd.duration;
                        s_hunter_3_beer_b_pd.Evaluate();
                    }

                    if (has_s_knight_4_beer_a_started && !has_s_knight_4_beer_a_played)
                    {
                        s_knight_4_beer_a_pd.time = s_knight_4_beer_a_pd.duration;
                        s_knight_4_beer_a_pd.Evaluate();
                    }

                    if (has_s_knight_4_beer_b_started && !has_s_knight_4_beer_b_played)
                    {
                        s_knight_4_beer_b_pd.time = s_knight_4_beer_b_pd.duration;
                        s_knight_4_beer_b_pd.Evaluate();
                    }

                    if (has_s_hunter_4_beer_a_started && !has_s_hunter_4_beer_a_played)
                    {
                        s_hunter_4_beer_a_pd.time = s_hunter_4_beer_a_pd.duration;
                        s_hunter_4_beer_a_pd.Evaluate();
                    }

                    if (has_s_hunter_4_beer_b_started && !has_s_hunter_4_beer_b_played)
                    {
                        s_hunter_4_beer_b_pd.time = s_hunter_4_beer_b_pd.duration;
                        s_hunter_4_beer_b_pd.Evaluate();
                    }

                    if (has_diaolgue_started && !has_diaolgue_p1_played && !has_diaolgue_p2_started && !has_diaolgue_played)
                    {
                        dialogue.time = FrameToTime(321);
                        dialogue.Evaluate();
                    }
                    else if(has_diaolgue_started && has_diaolgue_p1_played && !has_diaolgue_p2_started && !has_diaolgue_played)
                    {
                        dialogue.Play();
                        has_diaolgue_p2_started = true;
                    }
                    else if(has_diaolgue_started && has_diaolgue_p1_played && has_diaolgue_p2_started && !has_diaolgue_played)
                    {
                        dialogue.time = dialogue.duration;
                        dialogue.Evaluate();
                    }

                    if (has_dialogue_B2_started && !has_dialogue_B2_p1_played && !has_dialogue_B2_p2_started && !has_dialogue_B2_played)
                    {
                        dialogue_B2_PlayableDirector.time = FrameToTime(821);
                        dialogue_B2_PlayableDirector.Evaluate();
                    }
                    else if (has_dialogue_B2_started && has_dialogue_B2_p1_played && !has_dialogue_B2_p2_started && !has_dialogue_B2_played)
                    {
                        dialogue_B2_PlayableDirector.Play();
                        has_dialogue_B2_p2_started = true;
                    }
                    else if (has_dialogue_B2_started && has_dialogue_B2_p1_played && has_dialogue_B2_p2_started && !has_dialogue_B2_played)
                    {
                        dialogue_B2_PlayableDirector.time = dialogue_B2_PlayableDirector.duration;
                        dialogue_B2_PlayableDirector.Evaluate();
                    }

                    //if(has_diaolgue_0_started && !has_diaolgue_0_p1_played && !has_diaolgue_0_p2_started && !has_diaolgue_0_p2_played && !has_diaolgue_0_p3_started && !has_diaolgue_0_p3_played && !has_diaolgue_0_p4_started && !has_diaolgue_0_p4_played && !has_diaolgue_0_p5_started && !has_diaolgue_0_played)
                    //{
                    //    dialogue_0.time = FrameToTime(600);
                    //    dialogue_0.Evaluate();
                    //}
                    //else if(has_diaolgue_0_started && has_diaolgue_0_p1_played && !has_diaolgue_0_p2_started && !has_diaolgue_0_p2_played && !has_diaolgue_0_p3_started && !has_diaolgue_0_p3_played && !has_diaolgue_0_p4_started && !has_diaolgue_0_p4_played && !has_diaolgue_0_p5_started && !has_diaolgue_0_played)
                    //{
                    //    dialogue_0.Play();
                    //    has_diaolgue_0_p2_started = true;
                    //}
                    //else if (has_diaolgue_0_started && has_diaolgue_0_p1_played && has_diaolgue_0_p2_started && !has_diaolgue_0_p2_played && !has_diaolgue_0_p3_started && !has_diaolgue_0_p3_played && !has_diaolgue_0_p4_started && !has_diaolgue_0_p4_played && !has_diaolgue_0_p5_started && !has_diaolgue_0_played)
                    //{
                    //    dialogue_0.time = FrameToTime(1320);
                    //    dialogue_0.Evaluate();
                    //}
                    //else if (has_diaolgue_0_started && has_diaolgue_0_p1_played && has_diaolgue_0_p2_started && has_diaolgue_0_p2_played && !has_diaolgue_0_p3_started && !has_diaolgue_0_p3_played && !has_diaolgue_0_p4_started && !has_diaolgue_0_p4_played && !has_diaolgue_0_p5_started && !has_diaolgue_0_played)
                    //{
                    //    dialogue_0.Play();
                    //    has_diaolgue_0_p3_started = true;
                    //}
                    //else if (has_diaolgue_0_started && has_diaolgue_0_p1_played && has_diaolgue_0_p2_started && has_diaolgue_0_p2_played && has_diaolgue_0_p3_started && !has_diaolgue_0_p3_played && !has_diaolgue_0_p4_started && !has_diaolgue_0_p4_played && !has_diaolgue_0_p5_started && !has_diaolgue_0_played)
                    //{
                    //    dialogue_0.time = FrameToTime(1755);
                    //    dialogue_0.Evaluate();
                    //}
                    //else if (has_diaolgue_0_started && has_diaolgue_0_p1_played && has_diaolgue_0_p2_started && has_diaolgue_0_p2_played && has_diaolgue_0_p3_started && has_diaolgue_0_p3_played && !has_diaolgue_0_p4_started && !has_diaolgue_0_p4_played && !has_diaolgue_0_p5_started && !has_diaolgue_0_played)
                    //{
                    //    dialogue_0.Play();
                    //    has_diaolgue_0_p4_started = true;
                    //}
                    //else if (has_diaolgue_0_started && has_diaolgue_0_p1_played && has_diaolgue_0_p2_started && has_diaolgue_0_p2_played && has_diaolgue_0_p3_started && has_diaolgue_0_p3_played && has_diaolgue_0_p4_started && !has_diaolgue_0_p4_played && !has_diaolgue_0_p5_started && !has_diaolgue_0_played)
                    //{
                    //    dialogue_0.time = FrameToTime(2316);
                    //    dialogue_0.Evaluate();
                    //}
                    //else if (has_diaolgue_0_started && has_diaolgue_0_p1_played && has_diaolgue_0_p2_started && has_diaolgue_0_p2_played && has_diaolgue_0_p3_started && has_diaolgue_0_p3_played && has_diaolgue_0_p4_started && has_diaolgue_0_p4_played && !has_diaolgue_0_p5_started && !has_diaolgue_0_played)
                    //{
                    //    dialogue_0.Play();
                    //    has_diaolgue_0_p5_started = true;
                    //}
                    //else if (has_diaolgue_0_started && has_diaolgue_0_p1_played && has_diaolgue_0_p2_started && has_diaolgue_0_p2_played && has_diaolgue_0_p3_started && has_diaolgue_0_p3_played && has_diaolgue_0_p4_started && has_diaolgue_0_p4_played && has_diaolgue_0_p5_started && !has_diaolgue_0_played)
                    //{
                    //    dialogue_0.time = dialogue_0.duration;
                    //    dialogue_0.Evaluate();
                    //}

                }

                break;

            case GameMode.PlayerPlaying:

                // Enterキーが押されたかどうかをチェック
                if (Input.GetMouseButtonDown(0))  // Input.GetKeyDown(KeyCode.Return)
                {
                    charaMoveAndAnimationLogic();
                }


                break;

            case GameMode.WaitForSceneChange:
                // シーンを切り替える
                enterAlone.SetActive(true);
                if (Input.GetMouseButtonDown(0))  // Input.GetKeyDown(KeyCode.Return)
                {
                    SceneManager.LoadScene("M");
                }
                    

                break;

            case GameMode.choosing:

                break;
        }

    }

    public void pauseAni()
    {
        if (has_dialogue_B2_started)
        {
            Debug.Log($"dialogue_B2 time right now: {dialogue_B2_PlayableDirector.time}");

            if (dialogue_B2_PlayableDirector.time >= FrameToTime(821) && !has_dialogue_B2_p1_played)
            {
                Debug.Log("stop the timeline dialogue_B2!");
                dialogue_B2_PlayableDirector.Pause();
                has_dialogue_B2_p1_played = true;
            }
        }
        //else if (has_diaolgue_0_started)
        //{
        //    Debug.Log($"dialogue_0 time right now: {dialogue_0.time}");

        //    if (dialogue_0.time >= FrameToTime(600) && !has_diaolgue_0_p1_played)
        //    {
        //        Debug.Log("1 stop the timeline dialogue_0!");
        //        dialogue_0.Pause();
        //        has_diaolgue_0_p1_played = true;
        //    }
        //    else if(dialogue_0.time >= FrameToTime(1320) && !has_diaolgue_0_p2_played)
        //    {
        //        Debug.Log("2 stop the timeline dialogue_0!");
        //        dialogue_0.Pause();
        //        has_diaolgue_0_p2_played = true;
        //    }
        //    else if (dialogue_0.time >= FrameToTime(1755) && !has_diaolgue_0_p3_played)
        //    {
        //        Debug.Log("3 stop the timeline dialogue_0!");
        //        dialogue_0.Pause();
        //        has_diaolgue_0_p3_played = true;
        //    }
        //    else if (dialogue_0.time >= FrameToTime(2316) && !has_diaolgue_0_p4_played)
        //    {
        //        Debug.Log("4 stop the timeline dialogue_0!");
        //        dialogue_0.Pause();
        //        has_diaolgue_0_p4_played = true;
        //    }
        //}
        else if (has_diaolgue_started)
        {
            Debug.Log($"dialogue time right now: {dialogue.time}");

            if (dialogue.time >= FrameToTime(321) && !has_diaolgue_p1_played)
            {
                Debug.Log("stop the timeline dialogue!");
                dialogue.Pause();
                has_diaolgue_p1_played = true;
            }
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
            //Debug.Log("LoadScene:TitleScene");
            //SceneManager.LoadScene("TitleScene");
            Debug.Log("LoadScene:M");
            SceneManager.LoadScene("M");
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit(); // ゲームを閉じる
        }
    }
    public void reTry()
    {
        currentMovementIndex = 0;
        isAnyHorizontalLineCreated = false;
        isHorizontal_1_LineCreated = false;
        isHorizontal_2_LineCreated = false;
        DestroyAllHorizontalLinesInScene();
        canCreateLine_1 = true;
        canCreateLine_2 = true;
        canDeleteLine_1 = true;
        canDeleteLine_2 = true;
        knight.transform.position = character1_initial_pos.position;
        hunter.transform.position = character2_initial_pos.position;
        gift_is_dog = false;
        if (dog != null)
        {
            dog.SetActive(false);
        }
        if (gift_opening != null)
        {
            gift_opening.SetActive(false);
        }

        if (dialogue_failed_knight != null)
        {
            dialogue_failed_knight.SetActive(false);
        }
        if (dialogue_OE_hunter != null)
        {
            dialogue_OE_hunter.SetActive(false);
        }
        if (dialogue_beer_knight != null)
        {
            dialogue_beer_knight.SetActive(false);
        }
        if (dialogue_battleBeer_hunter != null)
        {
            dialogue_battleBeer_hunter.SetActive(false);
        }
        s_knight_a.SetActive(false);
        s_hunter_a.SetActive(false);
        game_start.SetActive(false);
        if (fukidashi_0 != null)
        {
            fukidashi_0.SetActive(false);
        }
        if (fukidashi != null)
        {
            fukidashi.SetActive(false);
        }
        if (dog != null)
        {
            dog.SetActive(false);
        }
        if (targetCompletedText != null)
        {
            targetCompletedText.SetActive(false);
        }
        if (dialogue_failed_hunter != null)
        {
            dialogue_failed_hunter.SetActive(false);
        }
        if (dialogue_OE_knight != null)
        {
            dialogue_OE_knight.SetActive(false);
        }
        if (dialogue_beer_hunter != null)
        {
            dialogue_beer_hunter.SetActive(false);
        }
        if (dialogue_battleBeer_knight != null)
        {
            dialogue_battleBeer_knight.SetActive(false);
        }
        dialogue_B2.SetActive(false);
        gift_opening.SetActive(false);
        s_knight_battle_a.SetActive(false);
        s_hunter_battle_a.SetActive(false);
        s_knight_battle_b.SetActive(false);
        s_hunter_battle_b.SetActive(false);
        s_knight_1_beer.SetActive(false);
        s_hunter_1_beer.SetActive(false);
        s_knight_2_beer.SetActive(false);
        s_hunter_2_beer.SetActive(false);
        s_knight_3_beer_a.SetActive(false);
        s_hunter_3_beer_a.SetActive(false);
        s_knight_3_beer_b.SetActive(false);
        s_hunter_3_beer_b.SetActive(false);
        s_knight_4_beer_a.SetActive(false);
        s_hunter_4_beer_a.SetActive(false);
        s_knight_4_beer_b.SetActive(false);
        s_hunter_4_beer_b.SetActive(false);
        targetCompletedText_2.SetActive(false);
        enterAlone.SetActive(false);
        has_dialogue_B2_started = false;
        has_dialogue_B2_played = false;
        has_s_knight_a_pd_played = false;
        has_s_knight_a_pd_started = false;
        has_s_hunter_a_pd_played = false;
        has_s_hunter_a_pd_started = false;
        has_dialogue_failed_hunter_started = false;
        has_dialogue_failed_hunter_played = false;
        has_dialogue_failed_knight_played = false;
        has_dialogue_failed_knight_started = false;
        has_dialogue_beer_hunter_started = false;
        has_dialogue_beer_hunter_played = false;
        has_dialogue_beer_knight_played = false;
        has_dialogue_beer_knight_started = false;
        has_dialogue_battleBeer_hunter_started = false;
        has_dialogue_battleBeer_hunter_played = false;
        has_dialogue_battleBeer_knight_played = false;
        has_dialogue_battleBeer_knight_started = false;
        has_dialogue_OE_hunter_started = false;
        has_dialogue_OE_hunter_played = false;
        has_dialogue_OE_knight_started = false;
        has_dialogue_OE_knight_played = false;
        has_s_knight_battle_a_started = false;
        has_s_knight_battle_a_played = false;
        has_s_hunter_battle_a_started = false;
        has_s_hunter_battle_a_played = false;
        has_s_knight_battle_b_started = false;
        has_s_knight_battle_b_played = false;
        has_s_hunter_battle_b_started = false;
        has_s_hunter_battle_b_played = false;
        has_s_knight_1_beer_started = false;
        has_s_knight_1_beer_played = false;
        has_s_hunter_1_beer_started = false;
        has_s_hunter_1_beer_played = false;
        has_s_knight_2_beer_started = false;
        has_s_knight_2_beer_played = false;
        has_s_hunter_2_beer_started = false;
        has_s_hunter_2_beer_played = false;
        has_s_knight_3_beer_a_started = false;
        has_s_knight_3_beer_a_played = false;
        has_s_hunter_3_beer_a_started = false;
        has_s_hunter_3_beer_a_played = false;
        has_s_knight_3_beer_b_started = false;
        has_s_knight_3_beer_b_played = false;
        has_s_hunter_3_beer_b_started = false;
        has_s_hunter_3_beer_b_played = false;
        has_s_knight_4_beer_a_started = false;
        has_s_knight_4_beer_a_played = false;
        has_s_knight_4_beer_b_started = false;
        has_s_knight_4_beer_b_played = false;
        has_s_hunter_4_beer_b_started = false;
        has_s_hunter_4_beer_b_played = false;
        has_s_hunter_4_beer_a_started = false;
        has_s_hunter_4_beer_a_played = false;
        has_diaolgue_0_started = false;
        has_diaolgue_0_played = false;
        has_diaolgue_started = false;
        has_diaolgue_played = false;
        game_start_played = false;
        has_diaolgue_0_p2_started = false;
        has_diaolgue_0_p1_played = false;
        has_diaolgue_0_p3_started = false;
        has_diaolgue_0_p2_played = false;
        has_diaolgue_0_p4_started = false;
        has_diaolgue_0_p3_played = false;
        has_diaolgue_0_p5_started = false;
        has_diaolgue_0_p4_played = false;
        has_diaolgue_p2_started = false;
        has_diaolgue_p1_played = false;

        currentGameMode = GameMode.PlayerPlaying;
    }
    //public void ChangeTextColor(TMP_Text tmp)
    //{
    //    if (cleared == false)
    //    {
    //        tmp.color = Color.gray; // もし`failed`は`true`であれば，テキストの色を灰色に変える
    //    }
    //}

    //  プレイヤーがEnterを押すと、キャラクターの移動＋プロットアイコンの生成＋ストーリーメッセージの生成を一つずつ表示される
    public void charaMoveAndAnimationLogic()
    {
        if (isHorizontal_1_LineCreated == true && isHorizontal_2_LineCreated == false) // ルート２　失敗：戦っただけで終わった
        {
            charaMoveRoute2();

        }
        else if (isHorizontal_1_LineCreated == true && isHorizontal_2_LineCreated == true) // ルート４　成功
        {
            charaMoveRoute4();
        }
        else if (isHorizontal_1_LineCreated == false && isHorizontal_2_LineCreated == true) // ルート３　失敗：酒場だけ
        {
            charaMoveRoute3();

        }
        else if (isHorizontal_1_LineCreated == false && isHorizontal_2_LineCreated == false) // ルート１　失敗：出会わなかった
        {
            charaMoveRoute1();
        }
    }

    /*
       　　　　    「騎士」0　　　　　   「猟師」1
                       ||                   ||
                       ○2    ○3    ○4    ○5
                       ||                   ||
                       ○6    ○7    ○8    ○9
                       ||                   ||
   　　　　　　   「結末」10　　　　     「結末」11
    */
    public void charaMoveRoute1()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 5 });
                enterAlone.SetActive(true);
                break;
            case 1:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });
                enterAlone.SetActive(false);
                canDeleteLine_1 = false;
                canCreateLine_1 = false;
                s_knight_battle_a.SetActive(true);
                has_s_knight_battle_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 2:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });
                s_knight_battle_a.SetActive(false);
                s_hunter_battle_a.SetActive(true);
                has_s_hunter_battle_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 3:
                StartMovement(new List<int> { 2, 6 }, new List<int> { 5, 9 });
                enterAlone.SetActive(true);
                s_hunter_battle_a.SetActive(false);
                break;

            case 4:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 9, 9 });
                enterAlone.SetActive(false);
                canDeleteLine_2 = false;
                s_knight_1_beer.SetActive(true);
                has_s_knight_1_beer_started = true;
                currentGameMode = GameMode.TextPlaying;
                canCreateLine_2 = false;
                break;

            case 5:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 9, 9 });
                s_knight_1_beer.SetActive(false);
                s_hunter_1_beer.SetActive(true);
                has_s_hunter_1_beer_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 6:
                StartMovement(new List<int> { 6, 10 }, new List<int> { 9, 9 });
                s_hunter_1_beer.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 9 });
                enterAlone.SetActive(false);
                dialogue_OE_knight.SetActive(true);
                has_dialogue_OE_knight_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 8:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 11 });
                dialogue_OE_knight.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 9:
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 10, 10 }, new List<int> { 11, 11 });
                dialogue_OE_hunter.SetActive(true);
                has_dialogue_OE_hunter_started = true;
                currentGameMode = GameMode.TextPlaying;

                hasMovementFinshed = true;
                break;

        }
    }

    public void charaMoveRoute2()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 5 });
                enterAlone.SetActive(true);
                break;

            case 1:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 }); // キャラクターを止まらせてセリフを表示する
                enterAlone.SetActive(false);
                canCreateLine_1 = false;
                canDeleteLine_1 = false;
                s_knight_battle_a.SetActive(true);
                has_s_knight_battle_a_started = true;
                currentGameMode = GameMode.TextPlaying;

                break;

            case 2:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });
                s_knight_battle_a.SetActive(false);
                s_hunter_battle_a.SetActive(true);
                has_s_hunter_battle_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 3:
                StartMovement(new List<int> { 2, 3 }, new List<int> { 5, 4 });
                s_hunter_battle_a.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 4:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 4 }); // 止まらせて吹き出しを表示する
                fukidashi_0.SetActive(true);
                has_diaolgue_0_started = true;
                enterAlone.SetActive(false);
                currentGameMode = GameMode.TextPlaying;
                
                break;

            case 5:
                fukidashi_0.SetActive(false);
                StartMovement(new List<int> { 3, 5 }, new List<int> { 4, 2 });
                enterAlone.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 2, 2 });
                enterAlone.SetActive(false);
                s_hunter_battle_b.SetActive(true);
                has_s_hunter_battle_b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 7:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 2, 2 });
                s_knight_battle_b.SetActive(true);
                s_hunter_battle_b.SetActive(false);
                has_s_knight_battle_b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 8:
                StartMovement(new List<int> { 5, 9 }, new List<int> { 2, 6 });
                s_knight_battle_b.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 6, 6 });
                enterAlone.SetActive(false);
                canDeleteLine_2 = false;
                canCreateLine_2 = false;
                s_hunter_2_beer.SetActive(true);
                has_s_hunter_2_beer_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 10:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 6, 6 });
                s_hunter_2_beer.SetActive(false);
                s_knight_2_beer.SetActive(true);
                has_s_knight_2_beer_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 11:
                s_knight_2_beer.SetActive(false);
                StartMovement(new List<int> { 9, 9 }, new List<int> { 6, 10 });
                enterAlone.SetActive(true);
                break;

            case 12:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 });
                enterAlone.SetActive(false);
                dialogue_failed_hunter.SetActive(true);
                has_dialogue_failed_hunter_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 13:
                dialogue_failed_hunter.SetActive(false);
                StartMovement(new List<int> { 9, 11 }, new List<int> { 10, 10 });
                enterAlone.SetActive(true);
                break;

            case 14:
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 11, 11 }, new List<int> { 10, 10 });
                gift_opening.SetActive(true);
                dialogue_failed_knight.SetActive(true);
                has_dialogue_failed_knight_started = true;
                currentGameMode = GameMode.TextPlaying;

                hasMovementFinshed = true;

                break;
        }
    }

    public void charaMoveRoute3()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 5 });
                enterAlone.SetActive(true);
                break;
            case 1:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });
                enterAlone.SetActive(false);
                canCreateLine_1 = false;
                canDeleteLine_1 = false;
                s_knight_battle_a.SetActive(true);
                has_s_knight_battle_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 2:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });
                s_knight_battle_a.SetActive(false);
                s_hunter_battle_a.SetActive(true);
                has_s_hunter_battle_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 3:
                StartMovement(new List<int> { 2, 6 }, new List<int> { 5, 9 });
                enterAlone.SetActive(true);
                s_hunter_battle_a.SetActive(false);

                break;

            case 4:
                enterAlone.SetActive(false);
                canDeleteLine_2 = false;
                canCreateLine_2 = false;
                StartMovement(new List<int> { 6, 6 }, new List<int> { 9, 9 });
                s_knight_3_beer_a.SetActive(true);
                has_s_knight_3_beer_a_started = true;
                currentGameMode = GameMode.TextPlaying;

                break;

            case 5:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 9, 9 });
                s_knight_3_beer_a.SetActive(false);
                s_hunter_3_beer_a.SetActive(true);
                has_s_hunter_3_beer_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 6:
                StartMovement(new List<int> { 6, 7 }, new List<int> { 9, 8 });
                enterAlone.SetActive(true);
                s_hunter_3_beer_a.SetActive(false);
                break;

            case 7:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 8, 8 });
                enterAlone.SetActive(false);
                fukidashi.SetActive(true);
                has_diaolgue_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 8:
                fukidashi.SetActive(false);
                StartMovement(new List<int> { 7, 9 }, new List<int> { 8, 6 });
                enterAlone.SetActive(true);
                break;

            case 9:
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 9, 9 }, new List<int> { 6, 6 });
                s_hunter_3_beer_b.SetActive(true);
                has_s_hunter_3_beer_b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 10:
                s_hunter_3_beer_b.SetActive(false);
                StartMovement(new List<int> { 9, 9 }, new List<int> { 6, 6 });
                s_knight_3_beer_b.SetActive(true);
                has_s_knight_3_beer_b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 11:
                s_knight_3_beer_b.SetActive(false);
                StartMovement(new List<int> { 9, 9 }, new List<int> { 6, 10 });
                enterAlone.SetActive(true);
                break;

            case 12:
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 });
                dialogue_beer_hunter.SetActive(true);
                has_dialogue_beer_hunter_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 13:
                StartMovement(new List<int> { 9, 11 }, new List<int> { 10, 10 });
                dialogue_beer_hunter.SetActive(false);
                enterAlone.SetActive(true);

                break;

            case 14:
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 11, 11 }, new List<int> { 10, 10 });
                dialogue_beer_knight.SetActive(true);
                has_dialogue_beer_knight_started = true;
                gift_opening.SetActive(true);
                currentGameMode = GameMode.TextPlaying;

                hasMovementFinshed = true;
                break;
        }
    }

    public void charaMoveRoute4()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 5 });
                enterAlone.SetActive(true);
                break;

            case 1:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 }); // キャラクターを止まらせてセリフを表示する
                enterAlone.SetActive(false);
                canCreateLine_1 = false;
                canDeleteLine_1 = false;
                s_knight_battle_a.SetActive(true);
                has_s_knight_battle_a_started = true;
                currentGameMode = GameMode.TextPlaying;

                break;

            case 2:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 5, 5 });
                s_knight_battle_a.SetActive(false);
                s_hunter_battle_a.SetActive(true);
                has_s_hunter_battle_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 3:
                StartMovement(new List<int> { 2, 3 }, new List<int> { 5, 4 });
                s_hunter_battle_a.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 4:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 4 }); // 止まらせて吹き出しを表示する
                fukidashi_0.SetActive(true);
                has_diaolgue_0_started = true;
                enterAlone.SetActive(false);
                currentGameMode = GameMode.TextPlaying;
                break;

            case 5:
                fukidashi_0.SetActive(false);
                StartMovement(new List<int> { 3, 5 }, new List<int> { 4, 2 });
                enterAlone.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 2, 2 });
                enterAlone.SetActive(false);
                s_hunter_battle_b.SetActive(true);
                has_s_hunter_battle_b_started = true;
                currentGameMode = GameMode.TextPlaying;

                break;

            case 7:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 2, 2 });
                s_knight_battle_b.SetActive(true);
                has_s_knight_battle_a_started = true;
                s_hunter_battle_b.SetActive(false);
                currentGameMode = GameMode.TextPlaying;
                break;

            case 8:
                StartMovement(new List<int> { 5, 9 }, new List<int> { 2, 6 });
                s_knight_battle_b.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 6, 6 });
                enterAlone.SetActive(false);
                canCreateLine_2 = false;
                canDeleteLine_2 = false;
                s_hunter_4_beer_a.SetActive(true);
                has_s_hunter_4_beer_a_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 10:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 6, 6 });
                s_hunter_4_beer_a.SetActive(false);
                s_knight_4_beer_a.SetActive(true);
                has_s_knight_4_beer_a_started = true;
                currentGameMode = GameMode.TextPlaying;

                break;

            case 11:
                s_knight_4_beer_a.SetActive(false);
                StartMovement(new List<int> { 9, 8 }, new List<int> { 6, 7 });
                enterAlone.SetActive(true);
                break;

            case 12:
                enterAlone.SetActive(false);
                StartMovement(new List<int> { 8, 8 }, new List<int> { 7, 7 });
                dialogue_B2.SetActive(true);
                has_dialogue_B2_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 13:
                dialogue_B2.SetActive(false);
                StartMovement(new List<int> { 8, 6 }, new List<int> { 7, 9 });
                enterAlone.SetActive(true);
                break;

            case 14:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 9, 9 });
                enterAlone.SetActive(false);
                s_knight_4_beer_b.SetActive(true);
                has_s_knight_4_beer_b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 15:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 9, 9 });
                s_knight_4_beer_b.SetActive(false);
                s_hunter_4_beer_b.SetActive(true);
                has_s_hunter_4_beer_b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 16:
                StartMovement(new List<int> { 6, 10 }, new List<int> { 9, 9 });
                s_hunter_4_beer_b.SetActive(false);
                enterAlone.SetActive(true);
                break;

            case 17:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 9 });
                enterAlone.SetActive(false);
                dialogue_battleBeer_knight.SetActive(true);
                has_dialogue_battleBeer_knight_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 18:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 11 });
                enterAlone.SetActive(true);
                dialogue_battleBeer_knight.SetActive(false);
                break;

            case 19:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 11, 11 });
                enterAlone.SetActive(false);
                dialogue_battleBeer_hunter.SetActive(true);
                has_dialogue_battleBeer_hunter_started = true;
                currentGameMode = GameMode.TextPlaying;
                gift_opening.SetActive(true);
                gift_is_dog = true;
                break;
        }
    }

    void StartMovement(List<int> knightPath, List<int> hunterPath)
    {
        // 進行中の移動を停止
        if (knightMovementCoroutine != null)
            StopCoroutine(knightMovementCoroutine);
        if (hunterMovementCoroutine != null)
            StopCoroutine(hunterMovementCoroutine);

        knightMovementCoroutine = StartCoroutine(MoveKnightCoroutine(knightPath));
        hunterMovementCoroutine = StartCoroutine(MoveHunterCoroutine(hunterPath));

        currentMovementIndex++;
    }

    IEnumerator MoveKnightCoroutine(List<int> path)
    {
        foreach (int point in path)
        {
            Vector3 targetPosition = pointsDictionary[point] + new Vector3(0, 0, -0.1f);
            while (knight.transform.position != targetPosition)
            {
                knight.transform.position = Vector3.MoveTowards(knight.transform.position, targetPosition, Time.deltaTime * speed);
                yield return null;
            }
        }
    }

    IEnumerator MoveHunterCoroutine(List<int> path)
    {
        foreach (int point in path)
        {
            Vector3 targetPosition = pointsDictionary[point] + new Vector3(0, 0, -0.1f);
            while (hunter.transform.position != targetPosition)
            {
                hunter.transform.position = Vector3.MoveTowards(hunter.transform.position, targetPosition, Time.deltaTime * speed);
                yield return null;
            }
        }
    }




    // 点のペアごとにホバーエリアを生成するメソッド
    void CreateHoverAreaJ1(GameObject pointA, GameObject pointB)
    {
        Debug.Log($"CreateHoverArea called with pointA: {pointA.name}, pointB: {pointB.name}");   // デバッグログを出力して、点Aと点Bの情報を表示する

        Vector3 midPoint = (pointA.transform.position + pointB.transform.position) / 2;   // 点Aと点Bの中間点を計算する
        Vector3 direction = (pointB.transform.position - pointA.transform.position).normalized;   // 点Aから点Bへの方向ベクトルを計算し、正規化する

        GameObject hoverArea = new GameObject("HoverArea");   // 新しい GameObject を作成し、その名前を "HoverArea" に設定する
        BoxCollider boxCollider = hoverArea.AddComponent<BoxCollider>();   // BoxCollider コンポーネントを追加し、ホバーエリアのサイズを設定する

        // 点Aと点Bの間の距離をホバーエリアの横幅に設定し、縦幅を hoverAreaWidth、厚みを 0.1f に設定する
        boxCollider.size = new Vector3(Vector3.Distance(pointA.transform.position, pointB.transform.position), hoverAreaWidth, 0.1f);

        // BoxCollider をトリガーとして設定する
        boxCollider.isTrigger = true;

        Debug.Log($"BoxCollider created with size: {boxCollider.size}");   // BoxCollider のサイズをデバッグログで表示する

        hoverArea.transform.position = midPoint;   // ホバーエリアの位置を中間点に設定する
        // 【new！】ここを必ず注意してください！こう書いてください！
        // LookRotationを使って向きを設定し、水平方向に回転させます。
        hoverArea.transform.LookAt(pointB.transform);
        hoverArea.transform.Rotate(0, 90, 0);  // Colliderの長辺が点Aと点Bに向くようにしてください。

        // J1hoverScript スクリプトをホバーエリアに追加する
        J1_hoverArea J1hoverScript = hoverArea.AddComponent<J1_hoverArea>();         // Need changed NOTICE！
        J1hoverScript.Initialize(pointA, pointB, horizontalLineMaterial, horizontalLineWidth, tooltipPrefab);   // J1hoverScript スクリプトを初期化する

        // J1hoverScript スクリプトが追加されたことをデバッグログで表示する
        Debug.Log($"J1hoverScript added to {hoverArea.name}");

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

        // J1_charaHoverAreaScript スクリプトをキャラクターに追加する
        J1_charaHoverArea J1_charaHoverAreaScript = character.AddComponent<J1_charaHoverArea>();

        // J1_charaHoverAreaScript スクリプトの情報番号を設定する
        J1_charaHoverAreaScript.charaInfoNum = charaInfoNum;

        // J1_charaHoverAreaScriptスクリプトを初期化する
        J1_charaHoverAreaScript.Initialize(character, charaInfoPrefab, charaInfoPosition);
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
    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == JyomakuTextPlayableDirector)
        {
            targetText.SetActive(true);
            targetTextPlayableDirector.Play();

            Debug.Log(" JyomakuText Timeline playback completed.");
        }
        else if (director == targetTextPlayableDirector)
        {
            //currentGameMode = GameMode.PlayerPlaying;
            has_targetTextPlayableDirector_played = true;
            targetText_2.SetActive(true);
            
            Debug.Log(" targetText Timeline playback completed.");
            //Debug.Log("GameMode.PlayerPlayingに切り替える");
        }
        else if (director == targetText_2_PlayableDirector)
        {
            has_targetText_2_PlayableDirector_played = true;
            enterAlone.SetActive(true);
            Debug.Log(" targetText_2 Timeline playback completed.");
        }
        else if (director == game_start_pd)
        {
            game_start.SetActive(false);
            enterAlone.SetActive(true);
            game_start_played = true;
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == targetCompletedTextPlayableDirector)
        {
            Debug.Log("targetCompletedTextPlayableDirector playback completed.");
        }
        else if (director == s_knight_a_pd)
        {
            has_s_knight_a_pd_played = true;
            has_s_knight_a_pd_started = false;
            Debug.Log("s_knight_a_pd playback completed.");
        }
        else if (director == s_hunter_a_pd)
        {
            has_s_hunter_a_pd_played = true;
            has_s_hunter_a_pd_started = false;
            Debug.Log("s_hunter_a_pd playback completed.");
        }
        else if (director == dialogue_B2_PlayableDirector)
        {
            has_dialogue_B2_started = false;
            has_dialogue_B2_played = true;
            Debug.Log(" dialogue_B2_PlayableDirector playback completed.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == dialogue)
        {
            has_diaolgue_played = true;
            has_diaolgue_started = false;
            Debug.Log("dialogue Timeline playback completed.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == dialogue_0)
        {
            has_diaolgue_0_played = true;
            has_diaolgue_0_started = false;
            Debug.Log("dialogue_0 Timeline playback completed.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == dialogue_failed_knightPlayableDirector)
        {
            has_dialogue_failed_knight_played = true;
            has_dialogue_failed_knight_started = false;
            targetCompletedText.SetActive(true);
            currentGameMode = GameMode.WaitForSceneChange;
        }
        else if (director == dialogue_beer_knightPlayableDirector)
        {
            has_dialogue_beer_knight_played = true;
            has_dialogue_beer_knight_started = false;
            currentGameMode = GameMode.WaitForSceneChange;
        }
        else if (director == dialogue_battleBeer_hunterPlayableDirector)
        {
            has_dialogue_battleBeer_hunter_played = true;
            has_dialogue_battleBeer_hunter_started = false;
            targetCompletedText_2.SetActive(true);
            currentGameMode = GameMode.WaitForSceneChange;
        }
        else if (director == dialogue_battleBeer_knightPlayableDirector)
        {
            has_dialogue_battleBeer_knight_played = true;
            has_dialogue_battleBeer_knight_started = false;
            targetCompletedText.SetActive(true);
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == dialogue_OE_knightPlayableDirector)
        {
            has_dialogue_OE_knight_played = true;
            has_dialogue_OE_knight_started = false;
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == dialogue_OE_hunterPlayableDirector)
        {
            has_dialogue_OE_hunter_played = true;
            has_dialogue_OE_hunter_started = false;
            currentGameMode = GameMode.WaitForSceneChange;
        }
        else if (director == dialogue_beer_hunterPlayableDirector)
        {
            has_dialogue_beer_hunter_played = true;
            has_dialogue_beer_hunter_started = false;
            targetCompletedText_2.SetActive(true);
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_knight_battle_a_pd)
        {
            has_s_knight_battle_a_played = true;
            has_s_knight_battle_a_started = false;
            Debug.Log("s_knight_battle_a has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_hunter_battle_a_pd)
        {
            has_s_hunter_battle_a_played = true;
            has_s_hunter_battle_a_started = false;
            Debug.Log("s_hunter_battle_a has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_knight_battle_b_pd)
        {
            has_s_knight_battle_b_played = true;
            has_s_knight_battle_b_started = false;
            Debug.Log("s_knight_battle_b has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_hunter_battle_b_pd)
        {
            has_s_hunter_battle_b_played = true;
            has_s_hunter_battle_b_started = false;
            Debug.Log("s_hunter_battle_b has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_knight_1_beer_pd)
        {
            has_s_knight_1_beer_played = true;
            has_s_knight_1_beer_started=false;
            Debug.Log("s_knight_1_beer has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_hunter_1_beer_pd)
        {
            has_s_hunter_1_beer_played = true;
            has_s_hunter_1_beer_started = false;
            Debug.Log("s_hunter_1_beer has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_knight_2_beer_pd)
        {
            has_s_knight_2_beer_played = true;
            has_s_knight_2_beer_started = false;
            Debug.Log("s_knight_2_beer has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_hunter_2_beer_pd)
        {
            has_s_hunter_2_beer_played = true;
            has_s_hunter_2_beer_started = false;
            Debug.Log("s_hunter_2_beer has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_knight_3_beer_a_pd)
        {
            has_s_knight_3_beer_a_played = true;
            has_s_knight_3_beer_a_started = false;
            Debug.Log("s_knight_3_beer_a has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_hunter_3_beer_a_pd)
        {
            has_s_hunter_3_beer_a_played = true;
            has_s_hunter_3_beer_a_started = false;
            Debug.Log("s_hunter_3_beer_a has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_knight_3_beer_b_pd)
        {
            has_s_knight_3_beer_b_played = true;
            has_s_knight_3_beer_b_started = false;
            Debug.Log("s_knight_3_beer_b has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_hunter_3_beer_b_pd)
        {
            has_s_hunter_3_beer_b_played = true;
            has_s_hunter_3_beer_b_started = false;
            Debug.Log("s_hunter_3_beer_b has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_knight_4_beer_a_pd)
        {
            has_s_knight_4_beer_a_played = true;
            has_s_knight_4_beer_a_started = false;
            Debug.Log("s_knight_4_beer_a has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_hunter_4_beer_a_pd)
        {
            has_s_hunter_4_beer_a_played = true;
            has_s_hunter_4_beer_a_started = false;
            Debug.Log("s_hunter_4_beer_a has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_knight_4_beer_b_pd)
        {
            has_s_knight_4_beer_b_played = true;
            has_s_knight_4_beer_b_started = false;
            Debug.Log("s_knight_4_beer_b has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == s_hunter_4_beer_b_pd)
        {
            has_s_hunter_4_beer_b_played = true;
            has_s_hunter_4_beer_b_started = false;
            Debug.Log("s_hunter_4_beer_b has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == gift_opening_pd)
        {
            if (gift_is_dog)
            {
                dog.SetActive(true);
                audioSourceJ1.PlayOneShot(barkClip);
            }
            Debug.Log("gift_opening_pd has played.");
        }
    }
    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        s_knight_a_pd.stopped -= OnPlayableDirectorStopped;
        s_hunter_a_pd.stopped -= OnPlayableDirectorStopped;
        if (JyomakuTextPlayableDirector != null)
        {
            JyomakuTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (targetTextPlayableDirector != null)
        {
            targetTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (targetCompletedTextPlayableDirector != null)
        {
            targetCompletedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }


        if (dialogue_B2_PlayableDirector != null)
        {
            dialogue_B2_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        }


        if (dialogue != null)
        {
            dialogue.stopped -= OnPlayableDirectorStopped;
        }

        if (dialogue_failed_hunterPlayableDirector != null)
        {
            dialogue_failed_hunterPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (dialogue_failed_knightPlayableDirector != null)
        {
            dialogue_failed_knightPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (dialogue_beer_hunterPlayableDirector != null)
        {
            dialogue_beer_hunterPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (dialogue_beer_knightPlayableDirector != null)
        {
            dialogue_beer_knightPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (dialogue_battleBeer_hunterPlayableDirector != null)
        {
            dialogue_battleBeer_hunterPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (dialogue_battleBeer_knightPlayableDirector != null)
        {
            dialogue_battleBeer_knightPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (dialogue_OE_hunterPlayableDirector != null)
        {
            dialogue_OE_hunterPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (dialogue_OE_knightPlayableDirector != null)
        {
            dialogue_OE_knightPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        s_knight_battle_a_pd.stopped -= OnPlayableDirectorStopped;
        s_hunter_battle_a_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_battle_b_pd.stopped -= OnPlayableDirectorStopped;
        s_hunter_battle_b_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_1_beer_pd.stopped -= OnPlayableDirectorStopped;
        s_hunter_1_beer_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_2_beer_pd.stopped -= OnPlayableDirectorStopped;
        s_hunter_2_beer_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_3_beer_a_pd.stopped -= OnPlayableDirectorStopped;
        s_hunter_3_beer_a_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_3_beer_b_pd.stopped -= OnPlayableDirectorStopped;
        s_hunter_3_beer_b_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_4_beer_a_pd.stopped -= OnPlayableDirectorStopped;
        s_hunter_4_beer_a_pd.stopped -= OnPlayableDirectorStopped;
        s_knight_4_beer_b_pd.stopped -= OnPlayableDirectorStopped;
        s_hunter_4_beer_b_pd.stopped -= OnPlayableDirectorStopped;
        gift_opening_pd.stopped -= OnPlayableDirectorStopped;
        game_start_pd.stopped -= OnPlayableDirectorStopped;
        targetCompletedText_2_PlayableDirector.stopped -= OnPlayableDirectorStopped;
    }


    void DestroyAllHorizontalLinesInScene()
    {
        // シーンの中にある全ての 「HorizontalLine」 GameObjectを探して以下の配列に集める
        GameObject[] horizontalLines = GameObject.FindGameObjectsWithTag("HorizontalLine");

        if (horizontalLines.Length == 0)
        {
            Debug.Log("No HorizontalLine objects found in the scene.");
            return;
        }

        // 全部削除する
        foreach (GameObject line in horizontalLines)
        {
            Destroy(line);
            Debug.Log($"Destroyed HorizontalLine: {line.name}");
        }
    }
}
