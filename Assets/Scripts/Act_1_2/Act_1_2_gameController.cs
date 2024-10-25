using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class Act_1_2_gameController : MonoBehaviour
{
    public GameObject Act_1_2_Text; // ゲームオブジェクト Act_1_2_Text
    public PlayableDirector Act_1_2_TextPlayableDirector; // Act_1_2_TextのPlayableDirector

    public GameObject targetM_A1_2_Text; // ゲームオブジェクト 上のゲームターゲット
    public PlayableDirector targetM_A1_2_TextPlayableDirector; // 上のゲームターゲットのPlayableDirector

    public GameObject targetN_A1_2_Text; // ゲームオブジェクト 下のゲームターゲット
    public PlayableDirector targetN_A1_2_TextPlayableDirector; // 下のゲームターゲットのPlayableDirector

    //public GameObject targetS_a_Text; // ゲームオブジェクト エクストラのゲームターゲット　？？？？？
    //public PlayableDirector targetS_a_TextPlayableDirector; // エクストラのゲームターゲットのPlayableDirector

    //public GameObject targetS_b_Text; // ゲームオブジェクト エクストラのゲームターゲット　ちゃんと漢字とかある
    //public PlayableDirector targetS_b_TextPlayableDirector; // エクストラのゲームターゲットのPlayableDirector　　　

    public GameObject target_M_CompletedText; // ゲームオブジェクト 上のtargetCompletedText
    public PlayableDirector target_M_CompletedTextPlayableDirector; // 上のtargetCompletedTextのPlayableDirector

    public GameObject target_N_CompletedText; // ゲームオブジェクト 下のtargetCompletedText
    public PlayableDirector target_N_CompletedTextPlayableDirector; // 下のtargetCompletedTextのPlayableDirector

    //public GameObject target_S_CompletedText; // ゲームオブジェクト エクストラのtargetCompletedText
    //public PlayableDirector target_S_CompletedTextPlayableDirector; // エクストラのtargetCompletedTextのPlayableDirector

    public GameObject failedText; // ゲームオブジェクト failedText
    public PlayableDirector failedTextPlayableDirector; // failedTextのPlayableDirector

    public GameObject clearedText; // ゲームオブジェクト clearedText
    public PlayableDirector clearedTextPlayableDirector; // failedTextのPlayableDirector

    public bool cleared = false; // このステージをクリアできたか？をfalseとマークする（まだクリアできていないということ）

    //public bool _is_extraTargetCompleted = false; // エクストラのターゲットは達成した？のブール値をfalseとマークする

    public Transform startpoint_character1; // 左から1番目のスタート点の位置
    public Transform startpoint_character2; // 左から2番目のスタート点の位置
    public Transform startpoint_character3; // 左から3番目のスタート点の位置

    public GameObject[] tenGameObjects; // それぞれの点
    public Transform[] tenPositions; // それぞれの点の位置
    public Transform endpoint_character1; //左から1番目の終点の位置
    public Transform endpoint_character2; // 左から2番目の終点の位置
    public Transform endpoint_character3; // 左から2番目の終点の位置
    public Dictionary<int, Vector3> pointsDictionary;// 点とその位置情報を格納する辞書

    public bool isHorizontal_1_LineCreated = false; //　左上の横線は描かれたか？のブール値
    public bool isHorizontal_2_LineCreated = false; //　右上の横線は描かれたか？のブール値
    public bool isHorizontal_3_LineCreated = false; //　左下の横線は描かれたか？のブール値
    public bool isHorizontal_4_LineCreated = false; //　右下の横線は描かれたか？のブール値

    public GameObject character1; // 左から1番目のキャラクター
    public GameObject character2; // 左から2番目のキャラクター
    public GameObject character3; // 左から3番目のキャラクター

    public GameObject ball_waitToBeGot; // 線にあるボール
    public bool _is_ball_got = false; // ボールは取られたか？をfalseとマークする 
    public GameObject ball_withDog; // 犬に取られたボール
    public GameObject ball_beThrown_gameOb; // ボールが捨てられたアニメーション
    public PlayableDirector ball_beThrown; // ボールが捨てられたアニメーション

    private Coroutine character1MovementCoroutine; // 左から1番目のキャラクターの移動
    private Coroutine character2MovementCoroutine; // 左から2番目のキャラクターの移動
    private Coroutine character3MovementCoroutine; // 左から3番目のキャラクターの移動

    public bool hasMovementFinshed = false; //　キャラクターの移動は完成されたか？のブール値
    private int currentMovementIndex = 0; // プレイヤーがEnterを押す際に計算用のIndex

    public float speed = 3.0f;

    public GameObject tooltipPrefab; // 提示枠のプレハブ
    public Material horizontalLineMaterial; // 横線のマテリアル
    public float horizontalLineWidth = 0.05f; // 横線の幅
    public float hoverAreaWidth = 0.6f; // 横線のホバーエリアの縦幅
    public TMP_FontAsset dotFont;  // ドットフォント
    public GameObject charaInfoPrefab;

    public GameObject menu; // menu_controller
    public Vector3 menuTargetPosition = new Vector3(5.5f, -4.2f, 0); // たどり着いて欲しい座標
    public bool menuIsOnItsPos = false; // メニューは目標座標に着いたかをfalseとマークする
    public float moveSpeed = 1.5f; // メニューの移動スピード
    public TMP_Text cannotENTER; // メニューの「Enter：進む」

    public GameObject BoynewCharaAnounce; // 少年が登場したという通知
    public PlayableDirector BoynewCharaAnPlayableDirector; // 少年が登場したという通知 PlayableDirector

    public GameObject GirlnewCharaAnounce; // 少女が登場したという通知
    public PlayableDirector GirlnewCharaAnPlayableDirector; // 少女が登場したという通知 PlayableDirector

    public AudioClip leftClickClip;
    public AudioClip rightClickClip;
    public AudioClip missClip;
    public AudioClip itemGotClip;

    public AudioSource audioSourceA1_2;

    // 演出用 GameObject や PlayableDirectorなど
    public GameObject dialogue_A; // 海賊：「俺の宝を取ったのはお前か！あれ？この犬はうちの娘がよく遊んでいる犬じゃないか」
    public PlayableDirector dialogue_A_PlayableDirector; // 犬と少女はボール遊んでる PlayableDirector

    public GameObject dialogue_B; // 海賊：「今日はしっかりお仕置きしてやる！この泥棒犬め！」犬：（クゥーン）
    public PlayableDirector dialogue_B_PlayableDirector; // 左下の横線だけ繋いだ場合、犬は海賊に殴られた PlayableDirector

    public GameObject dialogue_C; // 海賊：「本当はお前を始末したいところだが、娘はお前と仲が良いし、今回は見逃してやる」犬：「ワン！」
    public PlayableDirector dialogue_C_PlayableDirector; // 左上と左下の横線だけ繋いだ場合、海賊は犬を見逃した PlayableDirector

    public GameObject dialogue_D_1; // 国王：（金銀を身に着け、威風堂々としている）海賊：（さすが王様だな。あいつの財宝を一つでも奪えば、俺の船は十年は航海できるだろう）
    public PlayableDirector dialogue_D_1_PlayableDirector; // 右上　国王と海賊のすれ違い PlayableDirector

    public GameObject dialogue_D_2; // 国王：（金銀を身に着け、威風堂々としている） 海賊：（もっと早くこの金持ちの王様の財宝を狙うべきだったな...）
    public PlayableDirector dialogue_D_2_PlayableDirector; // 右下の横線　国王と海賊のすれ違い PlayableDirector

    public GameObject dialogue_E; // 国王：（群衆に自分の財宝を誇示している）海賊：（全然隠さないんだな…次の獲物はお前だ）
    public PlayableDirector dialogue_E_PlayableDirector; // 右下 また国王とすれ違った海賊は国王を狙った　 PlayableDirector

    public GameObject dialogue_F; // 少年：「城に迷い込んだのか？僕が国王から助けてやろう」国王：「この薄汚い犬め！お前！早くこの犬を連れて行け！」
    public PlayableDirector dialogue_F_PlayableDirector; // 右下　犬と少年はボール遊んでる PlayableDirector

    public GameObject dialogue_G; // 国王：「またこの犬か！さっさと追い払え！」犬：（クゥーン）
    public PlayableDirector dialogue_G_PlayableDirector; // 左下　国王と犬のすれ違い PlayableDirector

    public GameObject ending_dog_sleep; // 犬が寝る結末アイコンに着いた
    public PlayableDirector ending_dog_sleep_PlayableDirector; // 犬が寝る結末アイコンに着いた PlayableDirector

    public GameObject ending_pirate_sleep; // 海賊が寝る結末アイコンに着いた
    public PlayableDirector ending_pirate_sleep_PlayableDirector; // 海賊が寝る結末アイコンに着いた PlayableDirector

    public GameObject ending_king_sleep; // 国王が寝る結末アイコンに着いた
    public PlayableDirector ending_king_sleep_PlayableDirector; // 国王が寝る結末アイコンに着いた PlayableDirector

    public GameObject ending_dog_fall; // 犬が滑る結末アイコンに着いた
    public PlayableDirector ending_dog_fall_PlayableDirector; // 犬が滑る結末アイコンに着いた PlayableDirector

    public GameObject ending_pirate_fall; // 海賊が滑る結末アイコンに着いた
    public PlayableDirector ending_pirate_fall_PlayableDirector; // 海賊が滑る結末アイコンに着いた PlayableDirector

    public GameObject ending_king_fall; // 国王が滑る結末アイコンに着いた
    public PlayableDirector ending_king_fall_PlayableDirector; // 国王が滑る結末アイコンに着いた PlayableDirector

    public GameObject ending_dog_food; // 犬が食べ物結末アイコンに着いた
    public PlayableDirector ending_dog_food_PlayableDirector; // 犬が食べ物結末アイコンに着いた PlayableDirector

    public GameObject ending_pirate_food; // 海賊が食べ物結末アイコンに着いた
    public PlayableDirector ending_pirate_food_PlayableDirector; // 海賊が食べ物結末アイコンに着いた PlayableDirector

    public GameObject ending_king_food; // 国王が食べ物結末アイコンに着いた
    public PlayableDirector ending_king_food_PlayableDirector; // 国王が食べ物結末アイコンに着いた PlayableDirector

    private enum GameMode
    {
        TextPlaying, // ゲーム開始時のテキストが再生中
        PlayerPlaying, // プレイヤーが操作している状態
        WaitForSceneChange // 現シーンのゲーム内容が終了し、プレイヤーがEnterを押すのを待って次のシーンに切り替える
    }
    private GameMode currentGameMode = GameMode.TextPlaying; // 現シーン開始時にゲームモードをStartTextPlayingに設定

    private enum route
    {
        _0_line_connected, // どの横線でも接続されてない
        _1_line_left_up, // 左上の横線だけ接続された　isHorizontal_1_LineCreatedだけ==true
        _1_line_left_down, // 左下の横線だけ接続された　isHorizontal_3_LineCreatedだけ==true
        _1_line_right_up, // 右上の横線だけ接続された　isHorizontal_2_LineCreatedだけ==true
        _1_line_right_down, // 右下の横線だけ接続された　isHorizontal_4_LineCreatedだけ==true
        _2_line_left_two, // 左の二本だけ接続された　isHorizontal_1_LineCreated==true && isHorizontal_3_LineCreated==true
        _2_line_right_two, // 右の二本だけ接続された  isHorizontal_2_LineCreated==true && isHorizontal_4_LineCreated==true
        _2_line_tilt_left, // 左上と右下の横線だけ接続された　isHorizontal_1_LineCreated==true && isHorizontal_4_LineCreated==true
        _2_line_tilt_right // 右上と左下の横線だけ接続された　isHorizontal_2_LineCreated==true && isHorizontal_3_LineCreated==true
    }

    private route finalRoute = route._0_line_connected; // 最初は０本

    // Start is called before the first frame update
    void Start()
    {
        bookEvent();

        CreateHoverAreaCharacter(character1, 4); // 左1＝character1＝犬＝4
        CreateHoverAreaCharacter(character2, 5); // 左2＝character2＝海賊＝5
        CreateHoverAreaCharacter(character3, 3); // 左3＝character3＝国王＝3

        CreateHoverArea(tenGameObjects[0], tenGameObjects[1]);
        CreateHoverArea(tenGameObjects[1], tenGameObjects[2]);
        CreateHoverArea(tenGameObjects[3], tenGameObjects[4]);
        CreateHoverArea(tenGameObjects[4], tenGameObjects[5]);


        if (BoynewCharaAnounce != null)
        {
            BoynewCharaAnounce.SetActive(false);
        }
        if (GirlnewCharaAnounce != null)
        {
            GirlnewCharaAnounce.SetActive(false);
        }
        if (menu != null)
        {
            menu.SetActive(false);
        }

        targetM_A1_2_Text.SetActive(false);
        targetN_A1_2_Text.SetActive(false);
        target_M_CompletedText.SetActive(false);
        target_N_CompletedText.SetActive(false);

        failedText.SetActive(false);
        clearedText.SetActive(false);

        dialogue_A.SetActive(false);
        dialogue_B.SetActive(false);
        dialogue_C.SetActive(false);
        dialogue_D_1.SetActive(false);
        dialogue_D_2.SetActive(false);
        dialogue_E.SetActive(false);
        dialogue_F.SetActive(false);
        dialogue_G.SetActive(false);

        ball_withDog.SetActive(false);
        ball_beThrown_gameOb.SetActive(false);

        ending_dog_fall.SetActive(false);
        ending_pirate_fall.SetActive(false);
        ending_king_fall.SetActive(false);
        ending_dog_sleep.SetActive(false);
        ending_king_sleep.SetActive(false);
        ending_pirate_sleep.SetActive(false);
        ending_dog_food.SetActive(false);
        ending_king_food.SetActive(false);
        ending_pirate_food.SetActive(false);

        // Initialize the dictionary and add the points
        pointsDictionary = new Dictionary<int, Vector3>();

        // Add knight and hunter start and end points
        pointsDictionary.Add(0, startpoint_character1.position); // 左から1番目のスタート点
        pointsDictionary.Add(1, startpoint_character2.position); // 左から2番目のスタート点
        pointsDictionary.Add(2, startpoint_character3.position); // 左から3番目のスタート点
        pointsDictionary.Add(9, endpoint_character1.position);  // 左から1番目の終点
        pointsDictionary.Add(10, endpoint_character2.position);   // 左から2番目の終点
        pointsDictionary.Add(11, endpoint_character3.position);  // 左から3番目の終点

        // Add the positions of the other points (4 gameobjects)
        for (int i = 0; i < tenPositions.Length; i++)
        {
            pointsDictionary.Add(i + 3, tenPositions[i].position); // 点の位置（3から8まで）
        }

        // すべての点の座標を表示する（デバッグ用）
        foreach (KeyValuePair<int, Vector3> point in pointsDictionary)
        {
            Debug.Log("Point " + point.Key + ": " + point.Value);
        }

    }

    // クリアできなかった場合、メニューの「Enter」の色を灰色にする
    public void ChangeTextColor(TMP_Text tmp)
    {
        if (cleared == false)
        {
            tmp.color = Color.gray; // もしcleared == falseであれば，テキストの色を灰色に変える

        }
    }


    // Update is called once per frame
    void Update()
    {
        switch (currentGameMode)
        {
            case GameMode.TextPlaying:

                break;

            case GameMode.PlayerPlaying:

                // Enterキーが押されたかどうかをチェック
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    charaMoveAndAnimationLogic();
                }

                break;

            case GameMode.WaitForSceneChange:

                // シーンを切り替える
                ChangeTextColor(cannotENTER);

                menu.SetActive(true);
                StartCoroutine(MoveMenuToTarget(menuTargetPosition, moveSpeed));
                waitForSceneChange_Menu();

                break;
        }
    }

    public void charaMoveAndAnimationLogic()
    {
        handleFinalRoute();
        //  キャラクターの移動＋プロットアイコンの生成＋ストーリーメッセージの生成を一つずつ表示される

        /*
   　　　　     「犬」0　　　　　「海賊」1　　　　　「国王」2
                   ||                ||          　　　||
                   ○3    横線1      ○4 　　 横線2    ○5
                   ||                ||                ||
                   ○6    横線3      ○7 　　 横線4    ○8
                   ||                ||      　　      ||
　　　　　　   「寝る」9　　　　　「滑る」10　　　　「食べ物」11
        */
        switch (finalRoute)
        {
            case route._0_line_connected:

                switch (currentMovementIndex)
                {
                    case 0:
                        StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                        break;

                    case 1:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 4 }, new List<int> { 5, 5 });
                        ball_waitToBeGot.SetActive(false);
                        ball_withDog.SetActive(true);
                        audioSourceA1_2.PlayOneShot(itemGotClip);
                        break;

                    case 2:
                        StartMovement(new List<int> { 3, 6 }, new List<int> { 4, 7 }, new List<int> { 5, 8 });
                        break;

                    case 3:
                        StartMovement(new List<int> { 6, 9 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });
                        break;

                    case 4:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });
                        ending_dog_sleep.SetActive(true);
                        ball_withDog.SetActive(false);

                        break;

                    case 5:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 7, 10 }, new List<int> { 8, 8 });
                        ending_dog_sleep.SetActive(false);
                        break;

                    case 6:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 }, new List<int> { 8, 8 });

                        ending_pirate_fall.SetActive(true);

                        break;

                    case 7:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 }, new List<int> { 8, 11 });
                        ending_pirate_fall.SetActive(false);
                        break;

                    case 8:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                        ending_king_food.SetActive(true);
                        break;
                }

                break;

            case route._1_line_left_up:

                switch (currentMovementIndex)
                {
                    case 0:
                        StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                        break;

                    case 1:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 4 }, new List<int> { 5, 5 });
                        ball_waitToBeGot.SetActive(false);
                        ball_withDog.SetActive(true);
                        audioSourceA1_2.PlayOneShot(itemGotClip);
                        break;

                    case 2:
                        StartMovement(new List<int> { 3, 4 }, new List<int> { 4, 3 }, new List<int> { 5, 5 });

                        break;

                    case 3:
                        StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 3 }, new List<int> { 5, 5 });
                        dialogue_A.SetActive(true);
                        ball_withDog.SetActive(false);
                        GirlnewCharaAnounce.SetActive(true);
                        break;

                    case 4:
                        StartMovement(new List<int> { 4, 7 }, new List<int> { 3, 6 }, new List<int> { 5, 8 });
                        //dialogue_A.SetActive(false);
                        ball_withDog.SetActive(true);
                        break;

                    case 5:
                        StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 9 }, new List<int> { 8, 8 });
                        break;

                    case 6:
                        StartMovement(new List<int> { 7, 7 }, new List<int> { 9, 9 }, new List<int> { 8, 8 });
                        ending_pirate_sleep.SetActive(true);

                        break;

                    case 7:
                        StartMovement(new List<int> { 7, 10 }, new List<int> { 9, 9 }, new List<int> { 8, 8 });
                        ending_pirate_sleep.SetActive(false);
                        break;

                    case 8:
                        StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 9 }, new List<int> { 8, 8 });
                        ending_dog_fall.SetActive(true);
                        break;

                    case 9:
                        StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 9 }, new List<int> { 8, 11 });
                        ending_dog_fall.SetActive(false);
                        ball_withDog.SetActive(false);
                        break;

                    case 10:
                        StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 9 }, new List<int> { 11, 11 });
                        ending_king_food.SetActive(true);
                        break;
                }

                break;

            case route._1_line_left_down:

                switch (currentMovementIndex)
                {
                    case 0:
                        StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                        break;

                    case 1:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 4 }, new List<int> { 5, 5 });
                        ball_waitToBeGot.SetActive(false);
                        ball_withDog.SetActive(true);
                        audioSourceA1_2.PlayOneShot(itemGotClip);
                        break;

                    case 2:
                        StartMovement(new List<int> { 3, 6 }, new List<int> { 4, 7 }, new List<int> { 5, 8 });
                        break;

                    case 3:
                        StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });
                        ball_withDog.SetActive(false);
                        ball_beThrown_gameOb.SetActive(true);

                        break;

                    case 4:
                        StartMovement(new List<int> { 6, 7 }, new List<int> { 7, 6 }, new List<int> { 8, 8 });
                        
                        break;

                    case 5:
                        StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 6 }, new List<int> { 8, 8 });
                        dialogue_B.SetActive(true);
                        break;

                    case 6:
                        StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 9 }, new List<int> { 8, 8 });
                        break;

                    case 7:
                        StartMovement(new List<int> { 7, 7 }, new List<int> { 9, 9 }, new List<int> { 8, 8 });
                        ending_pirate_sleep.SetActive(true);
                        break;

                    case 8:
                        StartMovement(new List<int> { 7, 10 }, new List<int> { 9, 9 }, new List<int> { 8, 8 });
                        ending_pirate_sleep.SetActive(false);
                        break;

                    case 9:
                        StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 9 }, new List<int> { 8, 8 });
                        ending_dog_fall.SetActive(true);
                        break;

                    case 10:
                        StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 9 }, new List<int> { 8, 11 });
                        ending_dog_fall.SetActive(false);
                        break;

                    case 11:
                        StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 9 }, new List<int> { 11, 11 });
                        ending_king_food.SetActive(true);
                        break;
                }

                break;

            case route._1_line_right_up:

                switch (currentMovementIndex)
                {
                    case 0:
                        StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                        break;

                    case 1:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 4 }, new List<int> { 5, 5 });
                        ball_waitToBeGot.SetActive(false);
                        ball_withDog.SetActive(true);
                        audioSourceA1_2.PlayOneShot(itemGotClip);
                        break;

                    case 2:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 5 }, new List<int> { 5, 4 });
                        break;

                    case 3:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 5, 5 }, new List<int> { 4, 4 });
                        dialogue_D_1.SetActive(true);

                        break;

                    case 4:
                        StartMovement(new List<int> { 3, 6 }, new List<int> { 5, 8 }, new List<int> { 4, 7 });
                        break;

                    case 5:
                        StartMovement(new List<int> { 6, 9 }, new List<int> { 8, 8 }, new List<int> { 7, 7 });
                        break;

                    case 6:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 8, 8 }, new List<int> { 7, 7 });
                        ball_withDog.SetActive(false);
                        ending_dog_sleep.SetActive(true);
                        break;

                    case 7:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 8, 8 }, new List<int> { 7, 10 });
                        ending_dog_sleep.SetActive(false);
                        break;

                    case 8:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 8, 8 }, new List<int> { 10, 10 });
                        ending_king_fall.SetActive(true);
                        break;

                    case 9:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 8, 11 }, new List<int> { 10, 10 });
                        ending_king_fall.SetActive(false);
                        break;

                    case 10:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                        ending_pirate_food.SetActive(true);
                        break;

                }

                break;

            case route._1_line_right_down:

                switch (currentMovementIndex)
                {
                    case 0:
                        StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                        break;

                    case 1:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 4 }, new List<int> { 5, 5 });
                        ball_waitToBeGot.SetActive(false);
                        ball_withDog.SetActive(true);
                        audioSourceA1_2.PlayOneShot(itemGotClip);
                        break;

                    case 2:
                        StartMovement(new List<int> { 3, 6 }, new List<int> { 4, 7, 8 }, new List<int> { 5, 8, 7 });
                        break;

                    case 3:
                        StartMovement(new List<int> { 6, 6 }, new List<int> { 8, 8 }, new List<int> { 7, 7 });
                        dialogue_D_2.SetActive(true);
                        break;

                    case 4:
                        StartMovement(new List<int> { 6, 9 }, new List<int> { 8, 8 }, new List<int> { 7, 7 });
                        break;

                    case 5:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 8, 8 }, new List<int> { 7, 7 });
                        ball_withDog.SetActive(false);
                        ending_dog_sleep.SetActive(true);
                        break;

                    case 6:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 8, 8 }, new List<int> { 7, 10 });
                        ending_dog_sleep.SetActive(false);
                        break;

                    case 7:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 8, 8 }, new List<int> { 10, 10 });
                        ending_king_fall.SetActive(true);
                        break;

                    case 8:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 8, 11 }, new List<int> { 10, 10 });
                        ending_king_fall.SetActive(false);
                        break;

                    case 9:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                        ending_pirate_food.SetActive(true);
                        break;

                }

                break;

            case route._2_line_left_two:

                switch (currentMovementIndex)
                {
                    case 0:
                        StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                        break;

                    case 1:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 4 }, new List<int> { 5, 5 });
                        ball_waitToBeGot.SetActive(false);
                        ball_withDog.SetActive(true);
                        audioSourceA1_2.PlayOneShot(itemGotClip);
                        break;

                    case 2:
                        StartMovement(new List<int> { 3, 4 }, new List<int> { 4, 3 }, new List<int> { 5, 5 });

                        break;

                    case 3:
                        StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 3 }, new List<int> { 5, 5 });
                        dialogue_A.SetActive(true);
                        ball_withDog.SetActive(false);
                        GirlnewCharaAnounce.SetActive(true);
                        break;

                    case 4:
                        StartMovement(new List<int> { 4, 7 }, new List<int> { 3, 6 }, new List<int> { 5, 8 });
                        ball_withDog.SetActive(true);
                        break;

                    case 5:
                        StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 6 }, new List<int> { 8, 8 });
                        
                        ball_beThrown_gameOb.SetActive(true);
                        ball_withDog.SetActive(false);
                        break;

                    case 6:
                        StartMovement(new List<int> { 7, 6 }, new List<int> { 6, 7 }, new List<int> { 8, 8 });

                        break;

                    case 7:
                        StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });

                        dialogue_C.SetActive(true);

                        break;

                    case 8:
                        StartMovement(new List<int> { 6, 9 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });
                        
                        break;

                    case 9:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });
                        ending_dog_sleep.SetActive(true);
                        break;

                    case 10:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 7, 10 }, new List<int> { 8, 8 });
                        ending_dog_sleep.SetActive(false);
                        break;

                    case 11:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 }, new List<int> { 8, 8 });
                        ending_pirate_fall.SetActive(true);
                        break;

                    case 12:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 }, new List<int> { 8, 11 });
                        ending_pirate_fall.SetActive(false);
                        break;

                    case 13:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                        ending_king_food.SetActive(true);
                        break;
                }

                break;

            case route._2_line_right_two:

                switch (currentMovementIndex)
                {
                    case 0:
                        StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                        break;

                    case 1:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 4 }, new List<int> { 5, 5 });
                        ball_waitToBeGot.SetActive(false);
                        ball_withDog.SetActive(true);
                        audioSourceA1_2.PlayOneShot(itemGotClip);
                        break;

                    case 2:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 5 }, new List<int> { 5, 4 });
                        break;

                    case 3:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 5, 5 }, new List<int> { 4, 4 });
                        dialogue_D_1.SetActive(true);
                        break;

                    case 4:
                        StartMovement(new List<int> { 3, 6 }, new List<int> { 5, 8, 7 }, new List<int> { 4, 7, 8 });
                        break;


                    case 5:
                        StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });
                        dialogue_E.SetActive(true);
                        break;

                    case 6:
                        StartMovement(new List<int> { 6, 9 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });
                        break;

                    case 7:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });
                        ball_withDog.SetActive(false);
                        ending_dog_sleep.SetActive(true);
                        break;

                    case 8:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 7, 10 }, new List<int> { 8, 8 });
                        ending_dog_sleep.SetActive(false);
                        break;

                    case 9:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 }, new List<int> { 8, 8 });

                        ending_pirate_fall.SetActive(true);

                        break;

                    case 10:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 }, new List<int> { 8, 11 });
                        ending_pirate_fall.SetActive(false);
                        break;

                    case 11:
                        StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                        ending_king_food.SetActive(true);
                        break;
                }

                break;

            case route._2_line_tilt_left:

                switch (currentMovementIndex)
                {
                    case 0:
                        StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                        break;

                    case 1:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 4 }, new List<int> { 5, 5 });
                        ball_waitToBeGot.SetActive(false);
                        ball_withDog.SetActive(true);
                        audioSourceA1_2.PlayOneShot(itemGotClip);
                        break;

                    case 2:
                        StartMovement(new List<int> { 3, 4 }, new List<int> { 4, 3 }, new List<int> { 5, 5 });

                        break;

                    case 3:
                        StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 3 }, new List<int> { 5, 5 });
                        dialogue_A.SetActive(true);
                        ball_withDog.SetActive(false);
                        GirlnewCharaAnounce.SetActive(true);
                        break;

                    case 4:
                        StartMovement(new List<int> { 4, 7, 8 }, new List<int> { 3, 6 }, new List<int> { 5, 8, 7 });

                        ball_withDog.SetActive(true);
                        break;

                    case 5:
                        StartMovement(new List<int> { 8, 8 }, new List<int> { 6, 6 }, new List<int> { 7, 7 });
                        ball_withDog.SetActive(false);
                        dialogue_F.SetActive(true);
                        BoynewCharaAnounce.SetActive(true);
                        break;

                    case 6:
                        StartMovement(new List<int> { 8, 8 }, new List<int> { 6, 9 }, new List<int> { 7, 7 });
                        
                        break;

                    case 7:
                        StartMovement(new List<int> { 8, 8 }, new List<int> { 9, 9 }, new List<int> { 7, 7 });
                        ending_pirate_sleep.SetActive(true);
                        break;

                    case 8:
                        StartMovement(new List<int> { 8, 8 }, new List<int> { 9, 9 }, new List<int> { 7, 10 });
                        ending_pirate_sleep.SetActive(false);
                        break;

                    case 9:
                        StartMovement(new List<int> { 8, 8 }, new List<int> { 9, 9 }, new List<int> { 10, 10 });
                        ending_king_fall.SetActive(true);

                        break;

                    case 10:
                        StartMovement(new List<int> { 8, 11 }, new List<int> { 9, 9 }, new List<int> { 10, 10 });
                        ending_king_fall.SetActive(false);

                        break;

                    case 11:
                        StartMovement(new List<int> { 11, 11 }, new List<int> { 9, 9 }, new List<int> { 10, 10 });
                        ending_dog_food.SetActive(true);
                        cleared = true;
                        break;
                }

                break;

            case route._2_line_tilt_right:

                switch (currentMovementIndex)
                {
                    case 0:
                        StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                        break;

                    case 1:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 4 }, new List<int> { 5, 5 });
                        ball_waitToBeGot.SetActive(false);
                        ball_withDog.SetActive(true);
                        audioSourceA1_2.PlayOneShot(itemGotClip);
                        break;

                    case 2:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 5 }, new List<int> { 5, 4 });
                        break;

                    case 3:
                        StartMovement(new List<int> { 3, 3 }, new List<int> { 5, 5 }, new List<int> { 4, 4 });
                        dialogue_D_1.SetActive(true);

                        break;

                    case 4:
                        StartMovement(new List<int> { 3, 6 }, new List<int> { 5, 8 }, new List<int> { 4, 7 });
                        break;

                    case 5:
                        StartMovement(new List<int> { 6, 6 }, new List<int> { 8, 8 }, new List<int> { 7, 7 });
                        ball_withDog.SetActive(false);
                        ball_beThrown_gameOb.SetActive(true);
                        break;

                    case 6:
                        StartMovement(new List<int> { 6, 7 }, new List<int> { 8, 8 }, new List<int> { 7, 6 });

                        break;

                    case 7:
                        StartMovement(new List<int> { 7, 7 }, new List<int> { 8, 8 }, new List<int> { 6, 6 });
                        dialogue_G.SetActive(true);
                        break;

                    case 8:
                        StartMovement(new List<int> { 7, 7 }, new List<int> { 8, 8 }, new List<int> { 6, 9 });
                        break;

                    case 9:
                        StartMovement(new List<int> { 7, 7 }, new List<int> { 8, 8 }, new List<int> { 9, 9 });
                        ending_king_sleep.SetActive(true);
                        break;

                    case 10:
                        StartMovement(new List<int> { 7, 10 }, new List<int> { 8, 8 }, new List<int> { 9, 9 });
                        ending_king_sleep.SetActive(false);
                        break;

                    case 11:
                        StartMovement(new List<int> { 10, 10 }, new List<int> { 8, 8 }, new List<int> { 9, 9 });
                        ending_dog_fall.SetActive(true);
                        break;

                    case 12:
                        StartMovement(new List<int> { 10, 10 }, new List<int> { 8, 11 }, new List<int> { 9, 9 });
                        ending_dog_fall.SetActive(false);
                        break;

                    case 13:
                        StartMovement(new List<int> { 10, 10 }, new List<int> { 11, 11 }, new List<int> { 9, 9 });
                        ending_pirate_food.SetActive(true);
                        break;
                }

                break;



        }

    }

    // void start()で イベントのサブスクライブ　をここで統一やる
    public void bookEvent()
    {
        Act_1_2_TextPlayableDirector.stopped += OnPlayableDirectorStopped;
        targetM_A1_2_TextPlayableDirector.stopped += OnPlayableDirectorStopped;
        targetN_A1_2_TextPlayableDirector.stopped += OnPlayableDirectorStopped;
        target_M_CompletedTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        target_N_CompletedTextPlayableDirector.stopped += OnPlayableDirectorStopped;

        failedTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        clearedTextPlayableDirector.stopped += OnPlayableDirectorStopped;

        dialogue_A_PlayableDirector.stopped += OnPlayableDirectorStopped;
        dialogue_B_PlayableDirector.stopped += OnPlayableDirectorStopped;
        dialogue_C_PlayableDirector.stopped += OnPlayableDirectorStopped;
        dialogue_D_1_PlayableDirector.stopped += OnPlayableDirectorStopped;
        dialogue_D_2_PlayableDirector.stopped += OnPlayableDirectorStopped;
        dialogue_E_PlayableDirector.stopped += OnPlayableDirectorStopped;
        dialogue_F_PlayableDirector.stopped += OnPlayableDirectorStopped;
        dialogue_G_PlayableDirector.stopped += OnPlayableDirectorStopped;

        ball_beThrown.stopped += OnPlayableDirectorStopped;

        ending_dog_fall_PlayableDirector.stopped += OnPlayableDirectorStopped;
        ending_pirate_fall_PlayableDirector.stopped += OnPlayableDirectorStopped;
        ending_king_fall_PlayableDirector.stopped += OnPlayableDirectorStopped;
        ending_dog_sleep_PlayableDirector.stopped += OnPlayableDirectorStopped;
        ending_king_sleep_PlayableDirector.stopped += OnPlayableDirectorStopped;
        ending_pirate_sleep_PlayableDirector.stopped += OnPlayableDirectorStopped;
        ending_dog_food_PlayableDirector.stopped += OnPlayableDirectorStopped;
        ending_king_food_PlayableDirector.stopped += OnPlayableDirectorStopped;
        ending_pirate_food_PlayableDirector.stopped += OnPlayableDirectorStopped;
    }

    // PlayableDirector再生終わったら何が起こるのサブスクライク
    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if(director == Act_1_2_TextPlayableDirector)
        {
            targetM_A1_2_Text.SetActive(true);
            Debug.Log("Act_1_2_TextPlayableDirector has been played.");
        }
        else if (director == targetM_A1_2_TextPlayableDirector)
        {
            targetN_A1_2_Text.SetActive(true);
            Debug.Log("targetM_A1_2_TextPlayableDirector has been played.");
        }
        else if (director == targetN_A1_2_TextPlayableDirector)
        {
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("targetN_A1_2_TextPlayableDirector has been played.");
            Debug.Log("GameMode.PlayerPlayingに切り替える");
        }
        else if (director == ending_king_food_PlayableDirector)
        {
            failedText.SetActive(true);
            Debug.Log("ending_king_food_PlayableDirector has been played.");

            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("WaitForSceneChangeに切り替える");
        }
        else if (director == dialogue_A_PlayableDirector)
        {
            target_M_CompletedText.SetActive(true);

            Debug.Log("dialogue_A_PlayableDirector has been played.");
        }
        else if (director == ball_beThrown)
        {
            
            Debug.Log("ball_beThrown has been played.");
        }
        else if (director == ending_pirate_food_PlayableDirector)
        {
            failedText.SetActive(true);
            Debug.Log("ending_pirate_food_PlayableDirector has been played.");

            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("WaitForSceneChangeに切り替える");
        }
        else if (director == GirlnewCharaAnPlayableDirector)
        {
            
            Debug.Log("ending_pirate_food_PlayableDirector has been played.");
        }
        else if (director == dialogue_F_PlayableDirector)
        {
            target_N_CompletedText.SetActive(true);
            Debug.Log("dialogue_F_PlayableDirector has been played.");
        }
        else if (director == ending_dog_food_PlayableDirector)
        {
            clearedText.SetActive(true);
            Debug.Log("ending_dog_food_PlayableDirector has been played.");

            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("WaitForSceneChangeに切り替える");
        }
    }

    private void handleFinalRoute()
    {
        if (isHorizontal_1_LineCreated == false && isHorizontal_2_LineCreated == false && isHorizontal_3_LineCreated == false && isHorizontal_4_LineCreated == false)
        {
            finalRoute = route._0_line_connected;
            Debug.Log("finalRoute:" + finalRoute);
        }
        else if (isHorizontal_1_LineCreated == true && isHorizontal_2_LineCreated == false && isHorizontal_3_LineCreated == false && isHorizontal_4_LineCreated == false)
        {
            finalRoute = route._1_line_left_up;
            Debug.Log("finalRoute:" + finalRoute);
        }
        else if(isHorizontal_1_LineCreated == false && isHorizontal_2_LineCreated == true && isHorizontal_3_LineCreated == false && isHorizontal_4_LineCreated == false)
        {
            finalRoute = route._1_line_right_up;
            Debug.Log("finalRoute:" + finalRoute);
        }
        else if (isHorizontal_1_LineCreated == false && isHorizontal_2_LineCreated == false && isHorizontal_3_LineCreated == true && isHorizontal_4_LineCreated == false)
        {
            finalRoute = route._1_line_left_down;
            Debug.Log("finalRoute:" + finalRoute);
        }
        else if (isHorizontal_1_LineCreated == false && isHorizontal_2_LineCreated == false && isHorizontal_3_LineCreated == false && isHorizontal_4_LineCreated == true)
        {
            finalRoute = route._1_line_right_down;
            Debug.Log("finalRoute:" + finalRoute);
        }
        else if (isHorizontal_1_LineCreated == true && isHorizontal_2_LineCreated == false && isHorizontal_3_LineCreated == true && isHorizontal_4_LineCreated == false)
        {
            finalRoute = route._2_line_left_two;
            Debug.Log("finalRoute:" + finalRoute);
        }
        else if (isHorizontal_1_LineCreated == true && isHorizontal_2_LineCreated == false && isHorizontal_3_LineCreated == false && isHorizontal_4_LineCreated == true)
        {
            finalRoute = route._2_line_tilt_left;
            Debug.Log("finalRoute:" + finalRoute);
        }
        else if (isHorizontal_1_LineCreated == false && isHorizontal_2_LineCreated == true && isHorizontal_3_LineCreated == true && isHorizontal_4_LineCreated == false)
        {
            finalRoute = route._2_line_tilt_right;
            Debug.Log("finalRoute:" + finalRoute);
        }
        else if (isHorizontal_1_LineCreated == false && isHorizontal_2_LineCreated == true && isHorizontal_3_LineCreated == false && isHorizontal_4_LineCreated == true)
        {
            finalRoute = route._2_line_right_two;
            Debug.Log("finalRoute:" + finalRoute);
        }
    }
    
    void StartMovement(List<int> character_1_Path, List<int> character_2_Path, List<int> character_3_Path)
    {
        // 進行中の移動を停止

        if (character1MovementCoroutine != null)
            StopCoroutine(character1MovementCoroutine);
        if (character2MovementCoroutine != null)
            StopCoroutine(character2MovementCoroutine);
        if (character3MovementCoroutine != null)
            StopCoroutine(character3MovementCoroutine);

        character1MovementCoroutine = StartCoroutine(MoveCharacter1Coroutine(character_1_Path));
        character2MovementCoroutine = StartCoroutine(MoveCharacter2Coroutine(character_2_Path));
        character3MovementCoroutine = StartCoroutine(MoveCharacter3Coroutine(character_3_Path));

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

    IEnumerator MoveCharacter3Coroutine(List<int> path)
    {
        foreach (int point in path)
        {
            Vector3 targetPosition = pointsDictionary[point] + new Vector3(0, 0, -0.1f);
            while (character3.transform.position != targetPosition)
            {
                character3.transform.position = Vector3.MoveTowards(character3.transform.position, targetPosition, Time.deltaTime * speed);
                yield return null;
            }
        }
    }

    IEnumerator MoveMenuToTarget(Vector3 targetPosition, float m_speed)
    {
        while (menu.transform.position != targetPosition)
        {
            menu.transform.position = Vector3.MoveTowards(menu.transform.position, targetPosition, Time.deltaTime * m_speed);
            yield return null;
        }
        menuIsOnItsPos = true;

    }

    //  waitForSceneChangeに切り替えたら、どのシーンに移動するかをプレイヤーのインプットを待つ
    public void waitForSceneChange_Menu()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (menuIsOnItsPos == true)
            {
                if (cleared)
                {
                    /*
                     こちらは元々、
                    正常な順序で Act_1_1 → Act_1_1.5 → Act_1_2 → Act_1_2.5 → Act_1_3 → Act_1_3.5 → Act_1_4 と進む予定でした。
                    
                    しかし、stage3とstage4の順番を入れ替えるべきだと感じたため、シーン名を変更せずにコードを調整しました。
                    
                    その結果、現在は Act_1_1 → Act_1_1.5 → Act_1_2 → Act_1_3.5 → Act_1_4 → Act_1_2.5 → Act_1_3 という順序になっています。
                     */

                    Debug.Log("LoadScene:Act_1_3.5");
                    SceneManager.LoadScene("Act_1_3.5");

                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("LoadScene:Act_1_2");
            SceneManager.LoadScene("Act_1_2");
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("LoadScene:TitleScene");
            SceneManager.LoadScene("TitleScene");
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit(); // ゲームを閉じる
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

        // A1_2_charaHoverAreaScript スクリプトをキャラクターに追加する
        Act_1_2_charaHoverArea Act_1_2_charaHoverAreaScript = character.AddComponent<Act_1_2_charaHoverArea>();

        // A1_2_charaHoverAreaScript スクリプトの情報番号を設定する
        Act_1_2_charaHoverAreaScript.charaInfoNum = charaInfoNum;

        // A1_2_charaHoverAreaScriptスクリプトを初期化する
        Act_1_2_charaHoverAreaScript.Initialize(character, charaInfoPrefab, charaInfoPosition);
    }

    void CreateHoverArea(GameObject pointA, GameObject pointB)
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
        hoverArea.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);   // ホバーエリアの回転を、点Aから点Bへの方向に基づいて設定する

        // A1_hoverScript スクリプトをホバーエリアに追加する
        Act_1_2_hoverArea A1_2_hoverScript = hoverArea.AddComponent<Act_1_2_hoverArea>();         // 【＊大切】Need changed NOTICE！
        A1_2_hoverScript.Initialize(pointA, pointB, horizontalLineMaterial, horizontalLineWidth, tooltipPrefab);   // A1_2_hoverScript スクリプトを初期化する

        // A1_2_hoverScript スクリプトが追加されたことをデバッグログで表示する
        Debug.Log($"A1_2_hoverScript added to {hoverArea.name}");

        // ホバーエリアが作成された位置とサイズをデバッグログで表示する
        Debug.Log($"Hover Area created at {midPoint} with size {boxCollider.size}");
    }

    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        Act_1_2_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        targetM_A1_2_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        targetN_A1_2_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        target_M_CompletedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        target_N_CompletedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;

        failedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        clearedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;

        dialogue_A_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        dialogue_B_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        dialogue_C_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        dialogue_D_1_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        dialogue_D_2_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        dialogue_E_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        dialogue_F_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        dialogue_G_PlayableDirector.stopped -= OnPlayableDirectorStopped;

        ball_beThrown.stopped -= OnPlayableDirectorStopped;

        ending_dog_fall_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        ending_pirate_fall_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        ending_king_fall_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        ending_dog_sleep_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        ending_king_sleep_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        ending_pirate_sleep_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        ending_dog_food_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        ending_king_food_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        ending_pirate_food_PlayableDirector.stopped -= OnPlayableDirectorStopped;

    }
}
