﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class Act_1_3_gameController : MonoBehaviour
{

    public GameObject Act_1_3_Text; // ゲームオブジェクト Act_1_3_Text
    public PlayableDirector Act_1_3_TextPlayableDirector; // Act_1_3_TextのPlayableDirector

    public GameObject target_Upper; // ゲームオブジェクト 上のゲームターゲット
    public PlayableDirector target_UpperPlayableDirector; // 上のゲームターゲットのPlayableDirector

    public GameObject target_Middle; // ゲームオブジェクト 中のゲームターゲット
    public PlayableDirector target_MiddlePlayableDirector; // 中のゲームターゲットのPlayableDirector

    public GameObject target_Down; // ゲームオブジェクト 下のゲームターゲット
    public PlayableDirector target_DownPlayableDirector; // 下のゲームターゲットのPlayableDirector

    public GameObject failedText; // ゲームオブジェクト failedText
    public PlayableDirector failedTextPlayableDirector; // failedTextのPlayableDirector

    public GameObject clearedText; // ゲームオブジェクト clearedText
    public PlayableDirector clearedTextPlayableDirector; // failedTextのPlayableDirector

    public GameObject target_Upper_Completed; // ゲームオブジェクト 上のゲームターゲット
    public PlayableDirector target_Upper_CompletedPlayableDirector; // 上のゲームターゲットのPlayableDirector

    public GameObject target_Middle_Completed; // ゲームオブジェクト 中のゲームターゲット
    public PlayableDirector target_Middle_CompletedPlayableDirector; // 中のゲームターゲットのPlayableDirector

    public GameObject target_Down_Completed; // ゲームオブジェクト 下のゲームターゲット
    public PlayableDirector target_DownCompletedPlayableDirector; // 下のゲームターゲットのPlayableDirector

    public GameObject Dialogue_A;
    public PlayableDirector Dialogue_A_pd; // ゲームターゲットDialogue_AのPlayableDirector

    public GameObject Dialogue_B;
    public PlayableDirector Dialogue_B_pd; // ゲームターゲットDialogue_BのPlayableDirector

    public GameObject Dialogue_C;
    public PlayableDirector Dialogue_C_pd; // ゲームターゲットDialogue_CのPlayableDirector

    public GameObject Dialogue_D;
    public PlayableDirector Dialogue_D_pd; // ゲームターゲットDialogue_DのPlayableDirector

    public GameObject Dialogue_E;
    public PlayableDirector Dialogue_E_pd; // ゲームターゲットDialogue_EのPlayableDirector

    public GameObject Dialogue_F;
    public PlayableDirector Dialogue_F_pd; // ゲームターゲットDialogue_FのPlayableDirector

    public GameObject Dialogue_G;
    public PlayableDirector Dialogue_G_pd; // ゲームターゲットDialogue_GのPlayableDirector

    public GameObject Dialogue_H;
    public PlayableDirector Dialogue_H_pd; // ゲームターゲットDialogue_HのPlayableDirector

    public GameObject Dialogue_I; // ゲームターゲットDialogue_I
    public PlayableDirector Dialogue_I_pd; // ゲームターゲットDialogue_IのPlayableDirector

    public GameObject Dialogue_J; // ゲームターゲットDialogue_ J
    public PlayableDirector Dialogue_J_pd; // ゲームターゲットDialogue_JのPlayableDirector

    public GameObject Dialogue_K;
    public PlayableDirector Dialogue_K_pd; // ゲームターゲットDialogue_KのPlayableDirector

    public GameObject Dialogue_L;
    public PlayableDirector Dialogue_L_pd; // ゲームターゲットDialogue_LのPlayableDirector

    public GameObject Dialogue_M;
    public PlayableDirector Dialogue_M_pd; // ゲームターゲットDialogue_LのPlayableDirector

    public GameObject end_boy_beer; // 上と同じ
    public PlayableDirector end_boy_beer_pd;

    public GameObject end_girl_beer; 
    public PlayableDirector end_girl_beer_pd;

    public GameObject end_hunter_beer;
    public PlayableDirector end_hunter_beer_pd;

    public GameObject end_boy_ship; 
    public PlayableDirector end_boy_ship_pd;

    public GameObject end_girl_ship;
    public PlayableDirector end_girl_ship_pd;

    public GameObject end_hunter_ship;
    public PlayableDirector end_hunter_ship_pd;

    public GameObject end_boy_castle;
    public PlayableDirector end_boy_castle_pd;

    public GameObject end_girl_castle;
    public PlayableDirector end_girl_castle_pd;

    public GameObject end_hunter_castle;
    public PlayableDirector end_hunter_castle_pd;

    public PlayableDirector dog_treasure_run_pd;

    public PlayableDirector sword_pd;

    public bool _is_targret1_completed = false;
    public bool _is_targret2_completed = false;
    public bool _is_targret3_completed = false;

    public bool cleared = false; // このステージをクリアできたか？をfalseとマークする（まだクリアできていないということ）

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
    public bool isHorizontal_3_LineCreated = false; //　左中の横線は描かれたか？のブール値
    public bool isHorizontal_4_LineCreated = false; //　右中の横線は描かれたか？のブール値
    public bool isHorizontal_5_LineCreated = false; //　左下の横線は描かれたか？のブール値
    public bool isHorizontal_6_LineCreated = false; //　右下の横線は描かれたか？のブール値

    public GameObject character1; // 左から1番目のキャラクター
    public GameObject character2; // 左から2番目のキャラクター
    public GameObject character3; // 左から3番目のキャラクター

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
    public Vector3 menuTargetPosition = new Vector3(5.5f, -4.2f, -0.9f); // たどり着いて欲しい座標
    public bool menuIsOnItsPos = false; // メニューは目標座標に着いたかをfalseとマークする
    public float moveSpeed = 1.5f; // メニューの移動スピード
    public TMP_Text cannotENTER; // メニューの「Enter：進む」

    public AudioClip leftClickClip;
    public AudioClip rightClickClip;
    public AudioClip missClip;
    public AudioClip itemGotClip;

    public AudioSource audioSourceA1_3;

    private bool girlVShunter = false;
    private bool boyVShunter = false;

    private bool treature_boy_girl = false;
    private bool treature_boy_hunter = false;

    private enum route
    {
        _A_a, _A_c, _A_e, _A_b, _A_d , _A_f,
        _B_a_c, _B_a_e, _B_a_d, _B_a_f, _B_c_e, _B_b_c, _B_c_f, _B_d_f, _B_b_e, _B_d_e, _B_b_d, _B_b_f,
        _C_a_c_f, _C_a_c_e, _C_a_d_f, _C_a_d_e, _C_b_c_e , _C_b_c_f, _C_b_d_e , _C_b_d_f,
        _0_line
    }

    private route finalRoute = route._0_line; // 最初は０本

    private enum GameMode
    {
        TextPlaying, // ゲーム開始時のテキストが再生中
        PlayerPlaying, // プレイヤーが操作している状態
        WaitForSceneChange // 現シーンのゲーム内容が終了し、プレイヤーがEnterを押すのを待って次のシーンに切り替える
    }
    private GameMode currentGameMode = GameMode.TextPlaying; // 現シーン開始時にゲームモードをStartTextPlayingに設定

    // Start is called before the first frame update
    void Start()
    {
        target_Upper.SetActive(false);
        target_Middle.SetActive(false);
        target_Down.SetActive(false);
        target_Upper_Completed.SetActive(false);
        target_Middle_Completed.SetActive(false);
        target_Down_Completed.SetActive(false);
        menu.SetActive(false);
        failedText.SetActive(false);
        clearedText.SetActive(false);
        Dialogue_A.SetActive(false);
        Dialogue_B.SetActive(false); 
        Dialogue_C.SetActive(false); 
        Dialogue_D.SetActive(false); 
        Dialogue_E.SetActive(false);
        Dialogue_F.SetActive(false); 
        Dialogue_G.SetActive(false);
        Dialogue_H.SetActive(false);
        Dialogue_I.SetActive(false);
        Dialogue_J.SetActive(false);
        Dialogue_K.SetActive(false);
        Dialogue_L.SetActive(false);
        Dialogue_M.SetActive(false);
        end_boy_beer.SetActive(false);
        end_girl_beer.SetActive(false);
        end_hunter_beer.SetActive(false);
        end_boy_ship.SetActive(false);
        end_girl_ship.SetActive(false);
        end_hunter_ship.SetActive(false);
        end_boy_castle.SetActive(false);
        end_girl_castle.SetActive(false);
        end_hunter_castle.SetActive(false);

        bookEvent();

        CreateHoverAreaCharacter(character1, 6);
        CreateHoverAreaCharacter(character2, 7);
        CreateHoverAreaCharacter(character3, 2);

        // Initialize the dictionary and add the points
        pointsDictionary = new Dictionary<int, Vector3>();

        // Add knight and hunter start and end points
        pointsDictionary.Add(0, startpoint_character1.position); // 左から1番目のスタート点
        pointsDictionary.Add(1, startpoint_character2.position); // 左から2番目のスタート点
        pointsDictionary.Add(2, startpoint_character3.position); // 左から3番目のスタート点
        pointsDictionary.Add(12, endpoint_character1.position);  // 左から1番目の終点
        pointsDictionary.Add(13, endpoint_character2.position);   // 左から2番目の終点
        pointsDictionary.Add(14, endpoint_character3.position);  // 左から3番目の終点

        // Add the positions of the other points (4 gameobjects)
        for (int i = 0; i < tenPositions.Length; i++)
        {
            pointsDictionary.Add(i + 3, tenPositions[i].position); // 点の位置（3から11まで）
        }

        // すべての点の座標を表示する（デバッグ用）
        foreach (KeyValuePair<int, Vector3> point in pointsDictionary)
        {
            Debug.Log("Point " + point.Key + ": " + point.Value);
        }

        CreateHoverArea(tenGameObjects[0], tenGameObjects[1]);
        CreateHoverArea(tenGameObjects[1], tenGameObjects[2]);
        CreateHoverArea(tenGameObjects[3], tenGameObjects[4]);
        CreateHoverArea(tenGameObjects[4], tenGameObjects[5]);
        CreateHoverArea(tenGameObjects[6], tenGameObjects[7]);
        CreateHoverArea(tenGameObjects[7], tenGameObjects[8]);
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
        Debug.Log("finalRoute:" + finalRoute);
        //  キャラクターの移動＋プロットアイコンの生成＋ストーリーメッセージの生成を一つずつ表示される

        /*
   　　　　     「少年」0　　　　「少女」1　　　　　「猟師」2
                   ||                ||          　　　||
                   ○3    横線1      ○4 　　 横線2    ○5
                   ||                ||                ||
                   ○6    横線3      ○7 　　 横線4    ○8
                   ||                ||      　　      ||
                   ○9    横線5      ○10 　　横線6    ○11
                   ||                ||      　　      ||
　　　　　　   「酒場」12　　　　「海賊船」13　　  　「城」14

        横線1: a    横線2: b    横線3: c    横線4: d    横線5: e    横線6: f
        A：1本だけ　B：2本だけ　C：3本

        */


        switch (finalRoute)
        {
            case route._0_line:
                show_0_line();
                break;

            case route._A_a:
                show_A_a();
                break;

            case route._A_b:
                show_A_b();
                break;

            case route._A_c:
                show_A_c();
                break;

            case route._A_d:
                show_A_d();
                break;

            case route._A_e:
                show_A_e();
                break;

            case route._A_f:
                show_A_f();
                break;

            case route._B_a_c:
                show_B_a_c();
                break;

            case route._B_a_e:
                show_B_a_e();
                break;

            case route._B_a_d:
                show_B_a_d();
                break;

            case route._B_a_f:
                show_B_a_f();
                break;

            case route._B_b_c:
                show_B_b_c();
                break;

            case route._B_b_d:
                show_B_b_d();
                break;

            case route._B_b_e:
                show_B_b_e();
                break;

            case route._B_b_f:
                show_B_b_f();
                break;

            case route._B_c_e:
                show_B_c_e();
                break;

            case route._B_c_f:
                show_B_c_f();
                break;

            case route._B_d_e:
                show_B_d_e();
                break;

            case route._B_d_f:
                show_B_d_f();
                break;

            case route._C_a_c_e:
                show_C_a_c_e();
                break;

            case route._C_a_c_f:
                show_C_a_c_f();
                break;

            case route._C_a_d_e:
                show_C_a_d_e();
                break;

            case route._C_a_d_f:
                show_C_a_d_f();
                break;

            case route._C_b_c_e:
                show_C_b_c_e();
                break;

            case route._C_b_c_f:
                show_C_b_c_f();
                break;

            case route._C_b_d_e:
                show_C_b_d_e();
                break;

            case route._C_b_d_f:
                show_C_b_d_f();
                break;
        }
    }
    public void handleResult()
    {
        if (_is_targret1_completed && _is_targret2_completed && _is_targret3_completed)
        {
            cleared = true;
            clearedText.SetActive(true);
        }
        else
        {
            failedText.SetActive(true);
        }
    }
    public void handleFinalRoute()
    {
        // 6つのbool変数を使用して条件判断を簡略化する
        bool a = isHorizontal_1_LineCreated;
        bool b = isHorizontal_2_LineCreated;
        bool c = isHorizontal_3_LineCreated;
        bool d = isHorizontal_4_LineCreated;
        bool e = isHorizontal_5_LineCreated;
        bool f = isHorizontal_6_LineCreated;

        // すべての線が接続されていない場合
        if (!a && !b && !c && !d && !e && !f)
        {
            finalRoute = route._0_line;
            return;
        }

        // 1本の線だけが接続されている場合
        if (a && !b && !c && !d && !e && !f)
        {
            finalRoute = route._A_a;
            return;
        }
        if (!a && !b && c && !d && !e && !f)
        {
            finalRoute = route._A_c;
            return;
        }
        if (!a && !b && !c && !d && e && !f)
        {
            finalRoute = route._A_e;
            return;
        }
        if (!a && b && !c && !d && !e && !f)
        {
            finalRoute = route._A_b;
            return;
        }
        if (!a && !b && !c && d && !e && !f)
        {
            finalRoute = route._A_d;
            return;
        }
        if (!a && !b && !c && !d && !e && f)
        {
            finalRoute = route._A_f;
            return;
        }

        // 2本の線が接続されている場合
        if (a && !b && c && !d && !e && !f)
        {
            finalRoute = route._B_a_c;
            return;
        }
        if (a && !b && !c && !d && e && !f)
        {
            finalRoute = route._B_a_e;
            return;
        }
        if (a && !b && !c && d && !e && !f)
        {
            finalRoute = route._B_a_d;
            return;
        }
        if (a && !b && !c && !d && !e && f)
        {
            finalRoute = route._B_a_f;
            return;
        }
        if (!a && b && c && !d && !e && !f)
        {
            finalRoute = route._B_b_c;
            return;
        }
        if (!a && b && !c && d && !e && !f)
        {
            finalRoute = route._B_b_d;
            return;
        }
        if (!a && b && !c && !d && e && !f)
        {
            finalRoute = route._B_b_e;
            return;
        }
        if (!a && b && !c && !d && !e && f)
        {
            finalRoute = route._B_b_f;
            return;
        }
        if (!a && !b && c && !d && e && !f)
        {
            finalRoute = route._B_c_e;
            return;
        }
        if (!a && !b && c && !d && !e && f)
        {
            finalRoute = route._B_c_f;
            return;
        }
        if (!a && !b && !c && d && e && !f)
        {
            finalRoute = route._B_d_e;
            return;
        }
        if (!a && !b && !c && d && !e && f)
        {
            finalRoute = route._B_d_f;
            return;
        }


        // 3本の線が接続されている場合
        if (a && !b && c && !d && !e && f)
        {
            finalRoute = route._C_a_c_f;
            return;
        }
        if (a && !b && c && !d && e && !f)
        {
            finalRoute = route._C_a_c_e;
            return;
        }
        if (a && !b && !c && d && !e && f)
        {
            finalRoute = route._C_a_d_f;
            return;
        }
        if (a && !b && !c && d && e && !f)
        {
            finalRoute = route._C_a_d_e;
            return;
        }
        if (!a && b && c && !d && e && !f)
        {
            finalRoute = route._C_b_c_e;
            return;
        }
        if (!a && b && c && !d && !e && f)
        {
            finalRoute = route._C_b_c_f;
            return;
        }
        if (!a && b && !c && d && e && !f)
        {
            finalRoute = route._C_b_d_e;
            return;
        }
        if (!a && b && !c && d && !e && f)
        {
            finalRoute = route._C_b_d_f;
            return;
        }
    }

    /*
　　　　     「少年」0　　　　「少女」1　　　　　「猟師」2
               ||                ||          　　　||
               ○3    横線1      ○4 　　 横線2    ○5
               ||                ||                ||
               ○6    横線3      ○7 　　 横線4    ○8
               ||                ||      　　      ||
               ○9    横線5      ○10 　　横線6    ○11
               ||                ||      　　      ||
　　　　　　   「酒場」12　　　　「海賊船」13　　  　「城」14
    */

    public void show_0_line()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 6 }, new List<int> { 4, 7 }, new List<int> { 5, 8 });
                break;

            case 2:
                StartMovement(new List<int> { 6, 9 }, new List<int> { 7, 10 }, new List<int> { 8, 11 });
                break;

            case 3:
                StartMovement(new List<int> { 9, 12 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                break;

            case 4:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                end_boy_beer.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 10, 13 }, new List<int> { 11, 11 });
                end_boy_beer.SetActive(false);
                break;

            case 6:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 11, 11 });
                end_girl_ship.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 11, 14 });
                end_girl_ship.SetActive(false);
                break;

            case 8:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 14, 14 });
                end_hunter_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_A_a()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3, 4 }, new List<int> { 1, 4, 3 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 3 }, new List<int> { 5, 5 });
                Dialogue_A.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 4, 7 }, new List<int> { 3, 6 }, new List<int> { 5, 8 });
                break;

            case 3:
                StartMovement(new List<int> { 7, 10 }, new List<int> { 6, 9 }, new List<int> { 8, 11 });
                break;

            case 4:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 12 }, new List<int> { 11, 11 });
                break;

            case 5:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_girl_beer.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 10, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_girl_beer.SetActive(false);
                break;

            case 7:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_boy_ship.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 14 });
                end_boy_ship.SetActive(false);
                break;

            case 9:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 14, 14 });
                end_hunter_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_A_b()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4, 5 }, new List<int> { 2, 5, 4 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 5, 5 }, new List<int> { 4, 4 });
                Dialogue_B.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 3, 6 }, new List<int> { 5, 8 }, new List<int> { 4, 7 });
                break;

            case 3:
                StartMovement(new List<int> { 6, 9 }, new List<int> { 8, 11 }, new List<int> { 7, 10 });
                break;

            case 4:
                StartMovement(new List<int> { 9, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                break;

            case 5:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                end_boy_beer.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 13 });
                end_boy_beer.SetActive(false);
                break;

            case 7:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 14 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(false);
                break;

            case 9:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 14, 14 }, new List<int> { 13, 13 });
                end_girl_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_A_c()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 6, 7 }, new List<int> { 4, 7, 6 }, new List<int> { 5, 8 });
                break;

            case 2:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 6 }, new List<int> { 8, 8 });
                Dialogue_D.SetActive(true);
                break;

            case 3:
                StartMovement(new List<int> { 7, 10 }, new List<int> { 6, 9 }, new List<int> { 8, 11 });
                break;

            case 4:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 12 }, new List<int> { 11, 11 });
                break;

            case 5:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_girl_beer.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 10, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_girl_beer.SetActive(false);
                break;

            case 7:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_boy_ship.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 14 });
                end_boy_ship.SetActive(false);
                break;

            case 9:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 14, 14 });
                end_hunter_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_A_d()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 6 }, new List<int> { 4, 7, 8 }, new List<int> { 5, 8, 7 });
                break;

            case 2:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 8, 8 }, new List<int> { 7, 7 });
                sword_pd.Play();
                girlVShunter = true;
                break;

            case 3:
                StartMovement(new List<int> { 6, 9 }, new List<int> { 8, 11 }, new List<int> { 7, 10 });
                break;

            case 4:
                StartMovement(new List<int> { 9, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                break;

            case 5:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                end_boy_beer.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 13 });
                end_boy_beer.SetActive(false);

                break;

            case 7:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 14 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(false);
                break;

            case 9:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 14, 14 }, new List<int> { 13, 13 });
                end_girl_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_A_e()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 6 }, new List<int> { 4, 7 }, new List<int> { 5, 8 });
                break;

            case 2:
                StartMovement(new List<int> { 6, 9, 10 }, new List<int> { 7, 10, 9 }, new List<int> { 8, 11 });
                break;

            case 3:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 9 }, new List<int> { 11, 11 });
                dog_treasure_run_pd.Play();
                treature_boy_girl = true;
                break;

            case 4:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 12 }, new List<int> { 11, 11 });
                
                break;

            case 5:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_girl_beer.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 10, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_girl_beer.SetActive(false);
                break;

            case 7:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_boy_ship.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 14 });
                end_boy_ship.SetActive(false);
                break;

            case 9:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 14, 14 });
                end_hunter_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_A_f()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 6 }, new List<int> { 4, 7 }, new List<int> { 5, 8 });
                break;

            case 2:
                StartMovement(new List<int> { 6, 9 }, new List<int> { 7, 10, 11 }, new List<int> { 8, 11, 10 });
                break;

            case 3:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                Dialogue_K.SetActive(true);
                break;

            case 4:
                StartMovement(new List<int> { 9, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });

                break;

            case 5:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                end_boy_beer.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 13 });
                end_boy_beer.SetActive(false);
                break;

            case 7:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 14 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(false);
                break;

            case 9:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 14, 14 }, new List<int> { 13, 13 });
                end_girl_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_B_a_c()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3, 4 }, new List<int> { 1, 4, 3 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 3 }, new List<int> { 5, 5 });
                Dialogue_A.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 4, 7, 6 }, new List<int> { 3, 6, 7 }, new List<int> { 5, 8 });
                break;

            case 3:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });
                Dialogue_M.SetActive(true);
                break;

            case 4:
                StartMovement(new List<int> { 6, 9 }, new List<int> { 7, 10 }, new List<int> { 8, 11 });
                break;

            case 5:
                StartMovement(new List<int> { 9, 12 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                break;

            case 6:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                end_boy_beer.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 10, 13 }, new List<int> { 11, 11 });
                end_boy_beer.SetActive(false);
                break;

            case 8:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 11, 11 });
                end_girl_ship.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 11, 14});
                end_girl_ship.SetActive(false);
                break;

            case 10:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 14, 14 });
                end_hunter_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_B_a_d()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3, 4 }, new List<int> { 1, 4, 3 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 3 }, new List<int> { 5, 5 });
                Dialogue_A.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 4, 7, 8 }, new List<int> { 3, 6 }, new List<int> { 5, 8, 7 });
                break;

            case 3:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 6, 6 }, new List<int> { 7, 7 });
                sword_pd.Play();
                boyVShunter = true;
                break;

            case 4:
                StartMovement(new List<int> { 8, 11 }, new List<int> { 6, 9 }, new List<int> { 7, 10 });
                break;

            case 5:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 9, 12 }, new List<int> { 10, 10 });
                break;

            case 6:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 12, 12 }, new List<int> { 10, 10 });
                end_girl_beer.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 12, 12 }, new List<int> { 10, 13 });
                end_girl_beer.SetActive(false);
                break;

            case 8:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 12, 12 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 11, 14 }, new List<int> { 12, 12 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(false);
                break;

            case 10:
                StartMovement(new List<int> { 14, 14 }, new List<int> { 12, 12 }, new List<int> { 13, 13 });
                end_boy_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_B_a_e()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3, 4 }, new List<int> { 1, 4, 3 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 3 }, new List<int> { 5, 5 });
                Dialogue_A.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 4, 7 }, new List<int> { 3, 6 }, new List<int> { 5, 8 });
                break;

            case 3:
                StartMovement(new List<int> { 7, 10, 9 }, new List<int> { 6, 9, 10 }, new List<int> { 8, 11 });
                break;

            case 4:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                dog_treasure_run_pd.Play();
                treature_boy_girl = true;
                break;

            case 5:
                StartMovement(new List<int> { 9, 12 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                break;

            case 6:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                end_boy_beer.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 10, 13 }, new List<int> { 11, 11 });
                end_boy_beer.SetActive(false);
                break;

            case 8:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 11, 11 });
                end_girl_ship.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 11, 14 });
                end_girl_ship.SetActive(false);
                break;

            case 10:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 14, 14 });
                end_hunter_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_B_a_f()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3, 4 }, new List<int> { 1, 4, 3 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 3 }, new List<int> { 5, 5 });
                Dialogue_A.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 4, 7 }, new List<int> { 3, 6 }, new List<int> { 5, 8 });
                break;

            case 3:
                StartMovement(new List<int> { 7, 10, 11 }, new List<int> { 6, 9 }, new List<int> { 8, 11, 10 });
                break;

            case 4:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 9, 9 }, new List<int> { 10, 10 });
                Dialogue_L.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 9, 12 }, new List<int> { 10, 10 });
                break;

            case 6:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 12, 12 }, new List<int> { 10, 10 });
                end_girl_beer.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 12, 12 }, new List<int> { 10, 13 });
                end_girl_beer.SetActive(false);
                break;

            case 8:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 12, 12 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 11, 14 }, new List<int> { 12, 12 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(false);
                break;

            case 10:
                StartMovement(new List<int> { 14, 14 }, new List<int> { 12, 12 }, new List<int> { 13, 13 });
                end_boy_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_B_c_e()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 6, 7 }, new List<int> { 4, 7, 6 }, new List<int> { 5, 8 });
                break;

            case 2:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 6 }, new List<int> { 8, 8 });
                Dialogue_D.SetActive(true);
                break;

            case 3:
                StartMovement(new List<int> { 7, 10, 9 }, new List<int> { 6, 9, 10 }, new List<int> { 8, 11 });

                break;

            case 4:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                dog_treasure_run_pd.Play();
                treature_boy_girl = true;
                break;

            case 5:
                StartMovement(new List<int> { 9, 12 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                break;

            case 6:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                end_boy_beer.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 10, 13 }, new List<int> { 11, 11 });
                end_boy_beer.SetActive(false);
                break;

            case 8:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 11, 11 });
                end_girl_ship.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 11, 14 });
                end_girl_ship.SetActive(false);
                break;

            case 10:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 14, 14 });
                end_hunter_castle.SetActive(true);
                handleResult();
                break;

        }
    }

    public void show_B_b_c() 
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4, 5 }, new List<int> { 2, 5, 4 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 5, 5 }, new List<int> { 4, 4 });
                Dialogue_B.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 3, 6, 7 }, new List<int> { 5, 8 }, new List<int> { 4, 7, 6 });
                break;

            case 3:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 8, 8 }, new List<int> { 6, 6 });
                Dialogue_C.SetActive(true);
                break;

            case 4:
                StartMovement(new List<int> { 7, 10 }, new List<int> { 8, 11 }, new List<int> { 6, 9 });
                break;

            case 5:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 11, 11 }, new List<int> { 9, 12 });
                break;

            case 6:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 11, 11 }, new List<int> { 12, 12 });
                end_hunter_beer.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 10, 13 }, new List<int> { 11, 11 }, new List<int> { 12, 12 });
                end_hunter_beer.SetActive(false);
                break;

            case 8:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 11, 11 }, new List<int> { 12, 12 });
                end_boy_ship.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 11, 14 }, new List<int> { 12, 12 });
                end_boy_ship.SetActive(false);
                break;

            case 10:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 14, 14 }, new List<int> { 12, 12 });
                end_girl_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_B_c_f()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 6, 7 }, new List<int> { 4, 7, 6 }, new List<int> { 5, 8 });
                break;

            case 2:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 6 }, new List<int> { 8, 8 });
                Dialogue_D.SetActive(true);
                break;

            case 3:
                StartMovement(new List<int> { 7, 10, 11 }, new List<int> { 6, 9 }, new List<int> { 8, 11, 10 });
                break;

            case 4:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 9, 9 }, new List<int> { 10, 10 });
                Dialogue_L.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 9, 12 }, new List<int> { 10, 10 });
                break;

            case 6:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 12, 12 }, new List<int> { 10, 10 });
                end_girl_beer.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 12, 12 }, new List<int> { 10, 13 });
                end_girl_beer.SetActive(false);
                break;

            case 8:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 12, 12 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 11, 14 }, new List<int> { 12, 12 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(false);
                break;

            case 10:
                StartMovement(new List<int> { 14, 14 }, new List<int> { 12, 12 }, new List<int> { 13, 13 });
                end_boy_castle.SetActive(true);
                handleResult();
                break;

        }
    }

    public void show_B_d_f() 
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 6 }, new List<int> { 4, 7, 8 }, new List<int> { 5, 8, 7 });
                break;

            case 2:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 8, 8 }, new List<int> { 7, 7 });
                sword_pd.Play();
                girlVShunter = true;
                break;

            case 3:
                StartMovement(new List<int> { 6, 9 }, new List<int> { 8, 11, 10 }, new List<int> { 7, 10, 11 });
                break;

            case 4:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                Dialogue_K.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 9, 12 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                break;

            case 6:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                end_boy_beer.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 10, 13 }, new List<int> { 11, 11 });
                end_boy_beer.SetActive(false);
                break;

            case 8:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 11, 11 });
                end_girl_ship.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 11, 14 });
                end_girl_ship.SetActive(false);
                break;

            case 10:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 14, 14 });
                end_hunter_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_B_b_e()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4, 5 }, new List<int> { 2, 5, 4 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 5, 5 }, new List<int> { 4, 4 });
                Dialogue_B.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 3, 6 }, new List<int> { 5, 8 }, new List<int> { 4, 7 });
                break;

            case 3:
                StartMovement(new List<int> { 6, 9, 10 }, new List<int> { 8, 11 }, new List<int> { 7, 10, 9 });
                break;

            case 4:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 11, 11 }, new List<int> { 9, 9 });
                treature_boy_hunter = true;
                dog_treasure_run_pd.Play();
                break;

            case 5:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 11, 11 }, new List<int> { 9, 12 });
                break;

            case 6:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 11, 11 }, new List<int> { 12, 12 });
                end_hunter_beer.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 10, 13 }, new List<int> { 11, 11 }, new List<int> { 12, 12 });
                end_hunter_beer.SetActive(false);
                break;

            case 8:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 11, 11 }, new List<int> { 12, 12 });
                end_boy_ship.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 11, 14 }, new List<int> { 12, 12 });
                end_boy_ship.SetActive(false);
                break;

            case 10:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 14, 14 }, new List<int> { 12, 12 });
                end_girl_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_B_d_e()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 6 }, new List<int> { 4, 7, 8 }, new List<int> { 5, 8, 7 });
                break;

            case 2:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 8, 8 }, new List<int> { 7, 7 });
                sword_pd.Play();
                girlVShunter = true;
                break;

            case 3:
                StartMovement(new List<int> { 6, 9, 10 }, new List<int> { 8, 11 }, new List<int> { 7, 10, 9 });

                break;

            case 4:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 11, 11 }, new List<int> { 9, 9 });
                dog_treasure_run_pd.Play();
                treature_boy_hunter = true;
                break;

            case 5:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 11, 11 }, new List<int> { 9, 12 });

                break;

            case 6:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 11, 11 }, new List<int> { 12, 12 });
                end_hunter_beer.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 10, 13 }, new List<int> { 11, 11 }, new List<int> { 12, 12 });
                end_hunter_beer.SetActive(false);
                break;

            case 8:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 11, 11 }, new List<int> { 12, 12 });
                end_boy_ship.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 11, 14 }, new List<int> { 12, 12 });
                end_boy_ship.SetActive(false);
                break;

            case 10:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 14, 14 }, new List<int> { 12, 12 });
                end_girl_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_B_b_d()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4, 5 }, new List<int> { 2, 5, 4 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 5, 5 }, new List<int> { 4, 4 });
                Dialogue_B.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 3, 6 }, new List<int> { 5, 8, 7 }, new List<int> { 4, 7, 8 });
                break;

            case 3:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });
                sword_pd.Play();
                girlVShunter = true;
                break;

            case 4:
                StartMovement(new List<int> { 6, 9 }, new List<int> { 7, 10 }, new List<int> { 8, 11 });
                break;

            case 5:
                StartMovement(new List<int> { 9, 12 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                break;

            case 6:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                end_boy_beer.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 10, 13 }, new List<int> { 11, 11 });
                end_boy_beer.SetActive(false);
                break;

            case 8:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 11, 11 });
                end_girl_ship.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 11, 14 });
                end_girl_ship.SetActive(false);
                break;

            case 10:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 14, 14 });
                end_hunter_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_B_b_f()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4, 5 }, new List<int> { 2, 5, 4 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 5, 5 }, new List<int> { 4, 4 });
                Dialogue_B.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 3, 6 }, new List<int> { 5, 8 }, new List<int> { 4, 7 });
                break;

            case 3:
                StartMovement(new List<int> { 6, 9 }, new List<int> { 8, 11, 10 }, new List<int> { 7, 10, 11 });
                break;

            case 4:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                Dialogue_K.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 9, 12 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                break;

            case 6:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 10, 10 }, new List<int> { 11, 11 });
                end_boy_beer.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 10, 13 }, new List<int> { 11, 11 });
                end_boy_beer.SetActive(false);
                break;

            case 8:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 11, 11 });
                end_girl_ship.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 11, 14 });
                end_girl_ship.SetActive(false);
                break;

            case 10:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 13, 13 }, new List<int> { 14, 14 });
                end_hunter_castle.SetActive(true);
                handleResult();
                break;
        }
    }

    public void show_C_a_c_f()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3, 4 }, new List<int> { 1, 4, 3 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 3 }, new List<int> { 5, 5 });
                Dialogue_A.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 4, 7, 6 }, new List<int> { 3, 6, 7 }, new List<int> { 5, 8 });
                break;

            case 3:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });
                Dialogue_M.SetActive(true);
                break;

            case 4:
                StartMovement(new List<int> { 6, 9 }, new List<int> { 7, 10, 11 }, new List<int> { 8, 11, 10 });
                break;

            case 5:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                Dialogue_K.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 9, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                break;

            case 7:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                end_boy_beer.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 13 });
                end_boy_beer.SetActive(false);
                break;

            case 9:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(true);
                break;

            case 10:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 14 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(false);
                break;

            case 11:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 14, 14 }, new List<int> { 13, 13 });
                end_girl_castle.SetActive(true);
                handleResult();
                break;
        }
    }
    public void show_C_a_c_e()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3, 4 }, new List<int> { 1, 4, 3 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 3 }, new List<int> { 5, 5 });
                Dialogue_A.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 4, 7, 6 }, new List<int> { 3, 6, 7 }, new List<int> { 5, 8 });
                break;

            case 3:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });
                Dialogue_M.SetActive(true);
                break;

            case 4:
                StartMovement(new List<int> { 6, 9, 10 }, new List<int> { 7, 10, 9 }, new List<int> { 8, 11 });

                break;

            case 5:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 9 }, new List<int> { 11, 11 });
                dog_treasure_run_pd.Play();
                treature_boy_girl = true;
                break;

            case 6:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 12 }, new List<int> { 11, 11 });
    
                break;

            case 7:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_girl_beer.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 10, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_girl_beer.SetActive(false);
                break;

            case 9:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_boy_ship.SetActive(true);
                break;

            case 10:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 14 });
                end_boy_ship.SetActive(false);
                break;

            case 11:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 14, 14 });
                end_hunter_castle.SetActive(true);
                handleResult();
                break;
        }
    }
    public void show_C_a_d_f()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3, 4 }, new List<int> { 1, 4, 3 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 3 }, new List<int> { 5, 5 });
                Dialogue_A.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 4, 7, 8 }, new List<int> { 3, 6 }, new List<int> { 5, 8, 7 });
                break;

            case 3:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 6, 6 }, new List<int> { 7, 7 });
                sword_pd.Play();
                boyVShunter = true;
                break;

            case 4:
                StartMovement(new List<int> { 8, 11, 10 }, new List<int> { 6, 9 }, new List<int> { 7, 10, 11 });
                
                break;

            case 5:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 9 }, new List<int> { 11, 11 });
                Dialogue_L.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 12 }, new List<int> { 11, 11 });
                
                break;

            case 7:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_girl_beer.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 10, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_girl_beer.SetActive(false);
                break;

            case 9:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_boy_ship.SetActive(true);
                break;

            case 10:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 14 });
                end_boy_ship.SetActive(false);
                break;

            case 11:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 14, 14 });
                end_hunter_castle.SetActive(true);
                handleResult();
                break;
        }
    }
    public void show_C_a_d_e()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3, 4 }, new List<int> { 1, 4, 3 }, new List<int> { 2, 5 });
                break;

            case 1:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 3 }, new List<int> { 5, 5 });
                Dialogue_A.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 4, 7, 8 }, new List<int> { 3, 6 }, new List<int> { 5, 8, 7 });
                break;

            case 3:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 6, 6 }, new List<int> { 7, 7 });
                sword_pd.Play();
                boyVShunter = true;
                break;

            case 4:
                StartMovement(new List<int> { 8, 11 }, new List<int> { 6, 9, 10 }, new List<int> { 7, 10, 9 });
                break;

            case 5:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 10, 10 }, new List<int> { 9, 9 });
                Dialogue_H.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 10, 10 }, new List<int> { 9, 12 });
                break;

            case 7:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 10, 10 }, new List<int> { 12, 12 });
                end_hunter_beer.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 10, 13 }, new List<int> { 12, 12 });
                end_hunter_beer.SetActive(false);
                break;

            case 9:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 13, 13 }, new List<int> { 12, 12 });
                end_girl_ship.SetActive(true);
                break;

            case 10:
                StartMovement(new List<int> { 11, 14 }, new List<int> { 13, 13 }, new List<int> { 12, 12 });
                end_girl_ship.SetActive(false);
                break;

            case 11:
                StartMovement(new List<int> { 14, 14 }, new List<int> { 13, 13 }, new List<int> { 12, 12 });
                end_boy_castle.SetActive(true);
                handleResult();
                break;
        }
    }
    public void show_C_b_c_e()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4, 5 }, new List<int> { 2, 5, 4 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 5, 5 }, new List<int> { 4, 4 });
                Dialogue_B.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 3, 6, 7 }, new List<int> { 5, 8 }, new List<int> { 4, 7, 6 });
                break;

            case 3:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 8, 8 }, new List<int> { 6, 6 });
                Dialogue_C.SetActive(true);
                break;

            case 4:
                StartMovement(new List<int> { 7, 10, 9 }, new List<int> { 8, 11 }, new List<int> { 6, 9, 10 });
                break;

            case 5:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                treature_boy_hunter = true;
                dog_treasure_run_pd.Play();
                break;

            case 6:
                StartMovement(new List<int> { 9, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                end_boy_beer.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 13 });
                end_boy_beer.SetActive(false);
                break;

            case 8:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 14 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(false);
                break;

            case 10:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 14, 14 }, new List<int> { 13, 13 });
                end_girl_castle.SetActive(true);
                handleResult();
                break;
        }
    } 
    public void show_C_b_c_f()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4, 5 }, new List<int> { 2, 5, 4 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 5, 5 }, new List<int> { 4, 4 });
                Dialogue_B.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 3, 6, 7 }, new List<int> { 5, 8 }, new List<int> { 4, 7, 6 });
                break;

            case 3:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 8, 8 }, new List<int> { 6, 6 });
                Dialogue_C.SetActive(true);
                break;

            case 4:
                StartMovement(new List<int> { 7, 10, 11 }, new List<int> { 8, 11, 10 }, new List<int> { 6, 9 });
                break;

            case 5:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 10, 10 }, new List<int> { 9, 9 });
                Dialogue_J.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 10, 10 }, new List<int> { 9, 12 });
                
                break;

            case 7:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 10, 10 }, new List<int> { 12, 12 });
                end_hunter_beer.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 10, 13 }, new List<int> { 12, 12 });
                end_hunter_beer.SetActive(false);
                break;

            case 9:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 13, 13 }, new List<int> { 12, 12 });
                end_girl_ship.SetActive(true);
                break;

            case 10:
                StartMovement(new List<int> { 11, 14 }, new List<int> { 13, 13 }, new List<int> { 12, 12 });
                end_girl_ship.SetActive(false);
                break;

            case 11:
                StartMovement(new List<int> { 14, 14 }, new List<int> { 13, 13 }, new List<int> { 12, 12 });
                end_boy_castle.SetActive(true);
                handleResult();
                break;
        }
    }
    public void show_C_b_d_e()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4, 5 }, new List<int> { 2, 5, 4 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 5, 5 }, new List<int> { 4, 4 });
                Dialogue_B.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 3, 6 }, new List<int> { 5, 8, 7 }, new List<int> { 4, 7, 8 });
                break;

            case 3:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });
                sword_pd.Play();
                girlVShunter = true;
                break;

            case 4:
                StartMovement(new List<int> { 6, 9, 10 }, new List<int> { 7, 10, 9 }, new List<int> { 8, 11 });
                break;

            case 5:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 9 }, new List<int> { 11, 11 });
                dog_treasure_run_pd.Play();
                treature_boy_girl = true;
                break;

            case 6:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 9, 12 }, new List<int> { 11, 11 });
                break;

            case 7:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_girl_beer.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 10, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_girl_beer.SetActive(false);
                break;

            case 9:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 11 });
                end_boy_ship.SetActive(true);
                break;

            case 10:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 11, 14 });
                end_boy_ship.SetActive(false);
                break;

            case 11:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 12, 12 }, new List<int> { 14, 14 });
                end_hunter_castle.SetActive(true);
                handleResult();
                break;
        }
    } 
    public void show_C_b_d_f()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 3 }, new List<int> { 1, 4, 5 }, new List<int> { 2, 5, 4 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 5, 5 }, new List<int> { 4, 4 });
                Dialogue_B.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 3, 6 }, new List<int> { 5, 8, 7 }, new List<int> { 4, 7, 8 });
                break;

            case 3:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 }, new List<int> { 8, 8 });
                sword_pd.Play();
                girlVShunter = true;
                break;

            case 4:
                StartMovement(new List<int> { 6, 9 }, new List<int> { 7, 10, 11 }, new List<int> { 8, 11, 10 });
                break;

            case 5:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                Dialogue_K.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 9, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });

                break;

            case 7:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 10 });
                end_boy_beer.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 10, 13 });
                end_boy_beer.SetActive(false);
                break;

            case 9:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(true);
                break;

            case 10:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 14 }, new List<int> { 13, 13 });
                end_hunter_ship.SetActive(false);
                break;

            case 11:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 14, 14 }, new List<int> { 13, 13 });
                end_girl_castle.SetActive(true);
                handleResult();
                break;
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

    // void start()で イベントのサブスクライブ　をここで統一やる
    public void bookEvent()
    {
        Act_1_3_TextPlayableDirector.stopped += OnPlayableDirectorStopped;
        target_UpperPlayableDirector.stopped += OnPlayableDirectorStopped;
        target_MiddlePlayableDirector.stopped += OnPlayableDirectorStopped;
        target_DownPlayableDirector.stopped += OnPlayableDirectorStopped;
        failedTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        clearedTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        Dialogue_A_pd.stopped += OnPlayableDirectorStopped;
        Dialogue_B_pd.stopped += OnPlayableDirectorStopped;
        Dialogue_C_pd.stopped += OnPlayableDirectorStopped;
        Dialogue_D_pd.stopped += OnPlayableDirectorStopped;
        Dialogue_E_pd.stopped += OnPlayableDirectorStopped;
        Dialogue_F_pd.stopped += OnPlayableDirectorStopped;
        Dialogue_G_pd.stopped += OnPlayableDirectorStopped;
        Dialogue_H_pd.stopped += OnPlayableDirectorStopped;
        Dialogue_I_pd.stopped += OnPlayableDirectorStopped;
        Dialogue_J_pd.stopped += OnPlayableDirectorStopped;
        Dialogue_K_pd.stopped += OnPlayableDirectorStopped;
        Dialogue_L_pd.stopped += OnPlayableDirectorStopped;
        Dialogue_M_pd.stopped += OnPlayableDirectorStopped;
        end_boy_beer_pd.stopped += OnPlayableDirectorStopped;
        end_girl_beer_pd.stopped += OnPlayableDirectorStopped;
        end_hunter_beer_pd.stopped += OnPlayableDirectorStopped;
        end_boy_ship_pd.stopped += OnPlayableDirectorStopped;
        end_girl_ship_pd.stopped += OnPlayableDirectorStopped;
        end_hunter_ship_pd.stopped += OnPlayableDirectorStopped;
        end_boy_castle_pd.stopped += OnPlayableDirectorStopped;
        end_girl_castle_pd.stopped += OnPlayableDirectorStopped;
        end_hunter_castle_pd.stopped += OnPlayableDirectorStopped;
        dog_treasure_run_pd.stopped += OnPlayableDirectorStopped;
        sword_pd.stopped += OnPlayableDirectorStopped;

    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == Act_1_3_TextPlayableDirector)
        {
            target_Upper.SetActive(true);
            Debug.Log("Act_1_3_TextPlayableDirector has been played.");
        }
        else if (director == target_UpperPlayableDirector)
        {
            target_Middle.SetActive(true);
            Debug.Log("target_UpperPlayableDirector has been played.");
        }
        else if (director == target_MiddlePlayableDirector)
        {
            target_Down.SetActive(true);
            Debug.Log("target_MiddlePlayableDirector has been played.");
        }
        else if (director == target_DownPlayableDirector)
        {
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("target_DownPlayableDirector has been played.");
        }
        else if (director == failedTextPlayableDirector)
        {
            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("failedTextPlayableDirector has been played.");
        }
        else if (director == clearedTextPlayableDirector)
        {
            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("clearedTextPlayableDirector has been played.");
        }
        else if (director == Dialogue_A_pd)
        {
            _is_targret1_completed = true;
            target_Upper_Completed.SetActive(true);
            Debug.Log("Dialogue_A_pd has been played.");
        }
        else if (director == sword_pd)
        {
            if (girlVShunter)
            {
                Dialogue_E.SetActive(true);
            }
            else if (boyVShunter)
            {
                Dialogue_F.SetActive(true);
            }

            Debug.Log("sword_pd has been played.");
        }
        else if (director == dog_treasure_run_pd)
        {
            if (treature_boy_girl)
            {
                Dialogue_G.SetActive(true);
            }
            else if (treature_boy_hunter)
            {
                Dialogue_I.SetActive(true);
            }
            
            Debug.Log("dog_treasure_run_pd has been played.");
        }
        else if (director == Dialogue_F_pd)
        {
            _is_targret3_completed = true;
            target_Down_Completed.SetActive(true);
            Debug.Log("Dialogue_F_pd has been played.");
        }
        else if (director == Dialogue_H_pd)
        {
            _is_targret2_completed = true;
            target_Middle_Completed.SetActive(true);
            Debug.Log("Dialogue_H_pd has been played.");
        }
    }

    void OnDestroy()
    {
        Act_1_3_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        target_UpperPlayableDirector.stopped -= OnPlayableDirectorStopped;
        target_MiddlePlayableDirector.stopped -= OnPlayableDirectorStopped;
        target_DownPlayableDirector.stopped -= OnPlayableDirectorStopped;
        failedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        clearedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        Dialogue_A_pd.stopped -= OnPlayableDirectorStopped;
        Dialogue_B_pd.stopped -= OnPlayableDirectorStopped;
        Dialogue_C_pd.stopped -= OnPlayableDirectorStopped;
        Dialogue_D_pd.stopped -= OnPlayableDirectorStopped;
        Dialogue_E_pd.stopped -= OnPlayableDirectorStopped;
        Dialogue_F_pd.stopped -= OnPlayableDirectorStopped;
        Dialogue_G_pd.stopped -= OnPlayableDirectorStopped;
        Dialogue_H_pd.stopped -= OnPlayableDirectorStopped;
        Dialogue_I_pd.stopped -= OnPlayableDirectorStopped;
        Dialogue_J_pd.stopped -= OnPlayableDirectorStopped;
        Dialogue_K_pd.stopped -= OnPlayableDirectorStopped;
        Dialogue_L_pd.stopped -= OnPlayableDirectorStopped;
        Dialogue_M_pd.stopped -= OnPlayableDirectorStopped;
        end_boy_beer_pd.stopped -= OnPlayableDirectorStopped;
        end_girl_beer_pd.stopped -= OnPlayableDirectorStopped;
        end_hunter_beer_pd.stopped -= OnPlayableDirectorStopped;
        end_boy_ship_pd.stopped -= OnPlayableDirectorStopped;
        end_girl_ship_pd.stopped -= OnPlayableDirectorStopped;
        end_hunter_ship_pd.stopped -= OnPlayableDirectorStopped;
        end_boy_castle_pd.stopped -= OnPlayableDirectorStopped;
        end_girl_castle_pd.stopped -= OnPlayableDirectorStopped;
        end_hunter_castle_pd.stopped -= OnPlayableDirectorStopped;
        dog_treasure_run_pd.stopped -= OnPlayableDirectorStopped;
        sword_pd.stopped -= OnPlayableDirectorStopped;
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

                    Debug.Log("LoadScene:temporary");
                    SceneManager.LoadScene("temporary");

                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("LoadScene:Act_1_3");
            SceneManager.LoadScene("Act_1_3");
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

        // A1_3_charaHoverAreaScript スクリプトをキャラクターに追加する
        Act_1_3_charaHoverArea Act_1_3_charaHoverAreaScript = character.AddComponent<Act_1_3_charaHoverArea>();

        // A1_3_charaHoverAreaScript スクリプトの情報番号を設定する
        Act_1_3_charaHoverAreaScript.charaInfoNum = charaInfoNum;

        // A1_3_charaHoverAreaScriptスクリプトを初期化する
        Act_1_3_charaHoverAreaScript.Initialize(character, charaInfoPrefab, charaInfoPosition);
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
        Act_1_3_hoverArea A1_3_hoverScript = hoverArea.AddComponent<Act_1_3_hoverArea>();         // 【＊大切】Need changed NOTICE！
        A1_3_hoverScript.Initialize(pointA, pointB, horizontalLineMaterial, horizontalLineWidth, tooltipPrefab);   // A1_3_hoverScript スクリプトを初期化する

        // A1_3_hoverScript スクリプトが追加されたことをデバッグログで表示する
        Debug.Log($"A1_3_hoverScript added to {hoverArea.name}");

        // ホバーエリアが作成された位置とサイズをデバッグログで表示する
        Debug.Log($"Hover Area created at {midPoint} with size {boxCollider.size}");
    }
}
