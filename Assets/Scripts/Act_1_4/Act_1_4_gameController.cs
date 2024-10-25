using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class Act_1_4_gameController : MonoBehaviour
{
    public GameObject stronger_pirate;
    public GameObject stronger_king;

    public GameObject Act_1_4_Text; // ゲームオブジェクト Act_1_4_Text
    public PlayableDirector Act_1_4_TextPlayableDirector; // Act_1_4_TextのPlayableDirector

    public GameObject target1_Text; // ゲームオブジェクト 上のゲームターゲット
    public PlayableDirector target1_TextPlayableDirector; // 上のゲームターゲットのPlayableDirector

    public GameObject target2_Text; // ゲームオブジェクト 下のゲームターゲット
    public PlayableDirector target2_TextPlayableDirector; // 下のゲームターゲットのPlayableDirector

    public GameObject failedText; // ゲームオブジェクト failedText
    public PlayableDirector failedTextPlayableDirector; // failedTextのPlayableDirector

    public GameObject failed2Text; // ゲームオブジェクト failedText
    public PlayableDirector failed2TextPlayableDirector; // failedTextのPlayableDirector

    public GameObject clearedText; // ゲームオブジェクト clearedText
    public PlayableDirector clearedTextPlayableDirector; // failedTextのPlayableDirector

    public GameObject target1_CompletedText; // ゲームオブジェクト 上のtargetCompletedText
    public PlayableDirector target1_CompletedTextPlayableDirector; // 上のtargetCompletedTextのPlayableDirector

    public GameObject target2_CompletedText; // ゲームオブジェクト 下のtargetCompletedText
    public PlayableDirector target2_CompletedTextPlayableDirector; // 下のtargetCompletedTextのPlayableDirector

    public bool Done_target1 = false;
    public bool Done_target2 = false;

    public GameObject stronger_Text; // ゲームオブジェクト stronger_Text
    public PlayableDirector stronger_TextPlayableDirector; // stronger_TextのPlayableDirector

    public PlayableDirector stronger_ani; // stronger iconのアニメーション

    public GameObject explode_b;
    public PlayableDirector explode_b_ani; // explode_b iconのアニメーション

    public GameObject explode_c;
    public PlayableDirector explode_c_ani; // explode_c iconのアニメーション

    // 吹き出し
    public GameObject dialogue_A;
    public PlayableDirector dialogue_A_pd;

    public GameObject dialogue_B1;
    public PlayableDirector dialogue_B1_pd;

    public GameObject dialogue_B2;
    public PlayableDirector dialogue_B2_pd;

    public GameObject dialogue_C;
    public PlayableDirector dialogue_C_pd;

    public GameObject dialogue_D;
    public PlayableDirector dialogue_D_pd;

    public GameObject dialogue_E;
    public PlayableDirector dialogue_E_pd;

    public GameObject dialogue_F;
    public PlayableDirector dialogue_F_pd;

    public GameObject end_1;
    public PlayableDirector end_1_pd;
    public GameObject end_2;
    public PlayableDirector end_2_pd;
    public GameObject end_3;
    public PlayableDirector end_3_pd;
    public GameObject end_8;
    public PlayableDirector end_8_pd;
    public GameObject end_9;
    public PlayableDirector end_9_pd;
    public GameObject end_4;
    public PlayableDirector end_4_pd;
    public GameObject end_5;
    public PlayableDirector end_5_pd;
    public GameObject end_6;
    public PlayableDirector end_6_pd;
    public GameObject end_7;
    public PlayableDirector end_7_pd;

    // エンディング　セリフ

    public Transform startpoint_character1; // 左から1番目のスタート点の位置
    public Transform startpoint_character2; // 左から2番目のスタート点の位置
    public GameObject[] tenGameObjects; // それぞれの点
    public Transform[] tenPositions; // それぞれの点の位置
    public Transform endpoint_character1; //左から1番目の終点の位置
    public Transform endpoint_character2; // 左から2番目の終点の位置
    public Dictionary<int, Vector3> pointsDictionary;// 点とその位置情報を格納する辞書

    public bool isHorizontal_1_LineCreated = false; //　左上の横線は描かれたか？のブール値
    public bool isHorizontal_2_LineCreated = false; //　右上の横線は描かれたか？のブール値
    public bool isHorizontal_3_LineCreated = false; //　左中の横線は描かれたか？のブール値

    public bool cleared = false;

    public GameObject character1; // 左から1番目のキャラクター
    public GameObject character2; // 左から2番目のキャラクター

    private Coroutine character1MovementCoroutine; // 左から1番目のキャラクターの移動
    private Coroutine character2MovementCoroutine; // 左から2番目のキャラクターの移動

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

    public AudioSource audioSourceA1_4;

    public GameObject newCharaAnounce; // 新しいキャラクターが登場したという通知
    public PlayableDirector newCharaAnPlayableDirector; // 新しいキャラクターが登場したという通知 PlayableDirector

    private enum GameMode
    {
        TextPlaying, // ゲーム開始時のテキストが再生中
        PlayerPlaying, // プレイヤーが操作している状態
        WaitForSceneChange // 現シーンのゲーム内容が終了し、プレイヤーがEnterを押すのを待って次のシーンに切り替える
    }
    private GameMode currentGameMode = GameMode.TextPlaying; // 現シーン開始時にゲームモードをStartTextPlayingに設定

    private enum route
    {
        A, AC, ACD, B2, AE, B1F, 
        _0_line, B1
    }

    private route finalRoute = route._0_line; // 最初は０本

    // Start is called before the first frame update
    void Start()
    {
        CreateHoverAreaCharacter(character1, 5);
        CreateHoverAreaCharacter(character2, 3);

        stronger_king.SetActive(false);
        stronger_pirate.SetActive(false);
        target1_Text.SetActive(false);
        target2_Text.SetActive(false);
        target1_CompletedText.SetActive(false);
        target2_CompletedText.SetActive(false);
        failedText.SetActive(false);
        failed2Text.SetActive(false);
        clearedText.SetActive(false);

        stronger_Text.SetActive(false);
        explode_b.SetActive(false);
        explode_c.SetActive(false);

        menu.SetActive(false);
        newCharaAnounce.SetActive(false);

        dialogue_A.SetActive(false);
        dialogue_B1.SetActive(false);
        dialogue_B2.SetActive(false);
        dialogue_C.SetActive(false);
        dialogue_D.SetActive(false);
        dialogue_E.SetActive(false);
        dialogue_F.SetActive(false);

        end_1.SetActive(false);
        end_2.SetActive(false);
        end_3.SetActive(false);
        end_8.SetActive(false);
        end_9.SetActive(false);
        end_4.SetActive(false);
        end_5.SetActive(false);
        end_6.SetActive(false);
        end_7.SetActive(false);

        bookEvent();



        // Initialize the dictionary and add the points
        pointsDictionary = new Dictionary<int, Vector3>();

        // Add knight and hunter start and end points
        pointsDictionary.Add(0, startpoint_character1.position); // 左から1番目のスタート点
        pointsDictionary.Add(1, startpoint_character2.position); // 左から2番目のスタート点

        pointsDictionary.Add(8, endpoint_character1.position);  // 左から1番目の終点
        pointsDictionary.Add(9, endpoint_character2.position);   // 左から2番目の終点
 

        // Add the positions of the other points (4 gameobjects)
        for (int i = 0; i < tenPositions.Length; i++)
        {
            pointsDictionary.Add(i + 2, tenPositions[i].position); // 点の位置（2から7まで）
        }

        // すべての点の座標を表示する（デバッグ用）
        foreach (KeyValuePair<int, Vector3> point in pointsDictionary)
        {
            Debug.Log("Point " + point.Key + ": " + point.Value);
        }

        CreateHoverArea(tenGameObjects[0], tenGameObjects[1]);
        CreateHoverArea(tenGameObjects[2], tenGameObjects[3]);
        CreateHoverArea(tenGameObjects[4], tenGameObjects[5]);


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

    public void handleResult()
    {
        if (Done_target1 && Done_target2)
        {
            cleared = true;
            clearedText.SetActive(true);
        }
        else if ((Done_target1 && !Done_target2) || (!Done_target1 && !Done_target2))
        {
            failedText.SetActive(true);
        }
        else if (!Done_target1 && Done_target2)
        {
            failed2Text.SetActive(true);
        }
    }

    public void charaMoveAndAnimationLogic()
    {
        handleFinalRoute();
        Debug.Log("finalRoute:" + finalRoute);
        switch (finalRoute)
        {
            case route.A:
                show_A();
                break;

            case route.AC:
                show_AC();
                break;
                
            case route.ACD:
                show_ACD();
                break;

            case route.B2:
                show_B2();
                break;

            case route.AE:
                show_AE();
                break;

            case route.B1F:
                show_B1F();
                break;

            case route._0_line:
                show_0_line();
                break;

            case route.B1:
                show_B1();
                break;

              
        }
    }
    /*
  　　　　     「海賊」0　　　　 「国王」1　　　　
                  ||                ||          　　　
                  ○2    横線1      ○3 　　 
                  ||                ||   強くなる             
                  ○4    横線3      ○5 　　 
                  ||                ||      　　     
                  ○6    横線5      ○7 　　
                  ||                ||      　　      
　　　　　　 「ギロチン」8　　　　「寝る」9　　  　
       */
    public void show_A()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2, 3 }, new List<int> { 1, 3, 2 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 2, 2 });
                dialogue_A.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 3, 5 }, new List<int> { 2, 4 });
                break;

            case 3:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 4, 4 });
                stronger_ani.Play();
                break;

            case 4:
                StartMovement(new List<int> { 5, 7 }, new List<int> { 4, 6 });
                stronger_pirate.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 8 });
                break;

            case 6:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 8, 8 });
                end_2.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 7, 9 }, new List<int> { 8, 8 });
                Done_target2 = true;
                target2_CompletedText.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 8, 8 });
                end_5.SetActive(true);
                break;
        }
    }

    public void show_AC()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2, 3 }, new List<int> { 1, 3, 2 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 2, 2 });
                dialogue_A.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 3, 5 }, new List<int> { 2, 4 });
                break;

            case 3:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 4, 4 });
                stronger_ani.Play();
                break;

            case 4:
                StartMovement(new List<int> { 5, 4 }, new List<int> { 4, 5 });
                stronger_pirate.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 5, 5 });
                explode_b.SetActive(true);
                dialogue_C.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 4, 6 }, new List<int> { 5, 7 });
                break;

            case 7:
                StartMovement(new List<int> { 6, 8 }, new List<int> { 7, 7 });
                break;

            case 8:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 7, 7 });
                end_1.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 7, 9 });
                break;

            case 10:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 9, 9 });
                end_7.SetActive(true);
                break;
        }
    }
    public void show_ACD()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2, 3 }, new List<int> { 1, 3, 2 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 2, 2 });
                dialogue_A.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 3, 5 }, new List<int> { 2, 4 });
                break;

            case 3:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 4, 4 });
                stronger_ani.Play();
                break;

            case 4:
                StartMovement(new List<int> { 5, 4 }, new List<int> { 4, 5 });
                stronger_pirate.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 5, 5 });
                explode_b.SetActive(true);
                dialogue_C.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 4, 6, 7 }, new List<int> { 5, 7, 6 });
                break;

            case 7:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 6 });
                dialogue_D.SetActive(true);
                Done_target1 = true;
                target1_CompletedText.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 8 });
                break;

            case 9:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 8, 8 });
                end_8.SetActive(true);
                break;

            case 10:
                StartMovement(new List<int> { 7, 9 }, new List<int> { 8, 8 });
                target2_CompletedText.SetActive(true);
                Done_target2 = true;
                break;

            case 11:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 8, 8 });
                end_6.SetActive(true);
                break;
        }
    }

    public void show_B2()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });
                break;

            case 1:
                StartMovement(new List<int> { 2, 4 }, new List<int> { 3, 5 });
                break;

            case 2:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 5, 5 });
                stronger_ani.Play();
                break;

            case 3:
                StartMovement(new List<int> { 4, 6 }, new List<int> { 5, 7 });
                stronger_king.SetActive(true);
                break;

            case 4:
                StartMovement(new List<int> { 6, 7 }, new List<int> { 7, 6 });
                break;

            case 5:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 6 });
                dialogue_B2.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 8 });
                break;

            case 7:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 8, 8 });
                end_9.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 7, 9 }, new List<int> { 8, 8 });
                target2_CompletedText.SetActive(true);
                Done_target2 = true;
                break;

            case 9:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 8, 8 });
                end_5.SetActive(true);
                break;

        }
    }

    public void show_AE() // Actually: AF(dialogue A & F)
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2, 3 }, new List<int> { 1, 3, 2 });
                break;

            case 1:
                StartMovement(new List<int> { 3, 3 }, new List<int> { 2, 2 });
                dialogue_A.SetActive(true);
                break;

            case 2:
                StartMovement(new List<int> { 3, 5 }, new List<int> { 2, 4 });
                break;

            case 3:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 4, 4 });
                stronger_ani.Play();
                break;

            case 4:
                StartMovement(new List<int> { 5, 7, 6 }, new List<int> { 4, 6, 7 });
                stronger_pirate.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 });
                dialogue_F.SetActive(true);
                target1_CompletedText.SetActive(true);
                Done_target1 = true;
                break;

            case 6:
                StartMovement(new List<int> { 6, 8 }, new List<int> { 7, 7 });
                break;

            case 7:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 7, 7 });
                end_1.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 7, 9 });
                break;

            case 9:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 9, 9 });
                end_7.SetActive(true);
                break;
        }
    }

    public void show_B1F() // Actually: B1E(dialogue B1 & E)
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });
                break;

            case 1:
                StartMovement(new List<int> { 2, 4 }, new List<int> { 3, 5 });
                break;

            case 2:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 5, 5 });
                stronger_ani.Play();
                break;

            case 3:
                StartMovement(new List<int> { 4, 5 }, new List<int> { 5, 4 });
                stronger_king.SetActive(true);
                break;

            case 4:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 4, 4 });
                dialogue_B1.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 5, 7, 6 }, new List<int> { 4, 6, 7 });
                break;

            case 6:
                StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 });
                dialogue_E.SetActive(true);
                explode_c.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 6, 8 }, new List<int> { 7, 7 });
                break;

            case 8:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 7, 7 });
                end_1.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 7, 9 });
                break;

            case 10:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 9, 9 });
                end_7.SetActive(true);
                break;
        }
    }

    public void show_0_line()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });
                break;

            case 1:
                StartMovement(new List<int> { 2, 4 }, new List<int> { 3, 5 });
                break;

            case 2:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 5, 5 });
                stronger_ani.Play();
                break;

            case 3:
                StartMovement(new List<int> { 4, 6 }, new List<int> { 5, 7 });
                stronger_king.SetActive(true);
                break;

            case 4:
                StartMovement(new List<int> { 6, 8 }, new List<int> { 7, 7 });
                break;

            case 5:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 7, 7 });
                end_3.SetActive(true);
                break;

            case 6:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 7, 9 });
                break;

            case 7:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 9, 9 });
                end_4.SetActive(true);
                break;
        }
    }

    public void show_B1()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });
                break;

            case 1:
                StartMovement(new List<int> { 2, 4 }, new List<int> { 3, 5 });
                break;

            case 2:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 5, 5 });
                stronger_ani.Play();
                break;

            case 3:
                StartMovement(new List<int> { 4, 5 }, new List<int> { 5, 4 });
                stronger_king.SetActive(true);
                break;

            case 4:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 4, 4 });
                dialogue_B1.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 5, 7 }, new List<int> { 4, 6 });
                break;

            case 6:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 8 });
                break;

            case 7:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 8, 8 });
                end_9.SetActive(true);
                break;

            case 8:
                StartMovement(new List<int> { 7, 9 }, new List<int> { 8, 8 });
                target2_CompletedText.SetActive(true);
                Done_target2 = true;
                break;

            case 9:
                StartMovement(new List<int> { 9, 9 }, new List<int> { 8, 8 });
                end_5.SetActive(true);
                break;
        }
    }

    public void handleFinalRoute()
    {
        // 3つのbool変数を使用して条件判断を簡略化する
        bool a = isHorizontal_1_LineCreated;
        bool b = isHorizontal_2_LineCreated;
        bool c = isHorizontal_3_LineCreated;

        if (a && !b && !c)
        {
            finalRoute = route.A;
            return;
        }
        if (a && b && !c)
        {
            finalRoute = route.AC;
            return;
        }
        if (a && b && c)
        {
            finalRoute = route.ACD;
            return;
        }
        if (!a && !b && c)
        {
            finalRoute = route.B2;
            return;
        }
        if (a && !b && c)
        {
            finalRoute = route.AE;
            return;
        }
        if (!a && b && c)
        {
            finalRoute = route.B1F;
            return;
        }
        if (!a && !b && !c)
        {
            finalRoute = route._0_line;
            return;
        }
        if (!a && b && !c)
        {
            finalRoute = route.B1;
            return;
        }
    }

    public void bookEvent()
    {
        Act_1_4_TextPlayableDirector.stopped += OnPlayableDirectorStopped;
        target1_TextPlayableDirector.stopped += OnPlayableDirectorStopped;
        target2_TextPlayableDirector.stopped += OnPlayableDirectorStopped;
        failedTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        failed2TextPlayableDirector.stopped += OnPlayableDirectorStopped;
        clearedTextPlayableDirector.stopped += OnPlayableDirectorStopped;

        stronger_TextPlayableDirector.stopped += OnPlayableDirectorStopped;
        stronger_ani.stopped += OnPlayableDirectorStopped; 
        explode_b_ani.stopped += OnPlayableDirectorStopped; 
        explode_c_ani.stopped += OnPlayableDirectorStopped; 

        dialogue_A_pd.stopped += OnPlayableDirectorStopped;
        dialogue_B1_pd.stopped += OnPlayableDirectorStopped;
        dialogue_B2_pd.stopped += OnPlayableDirectorStopped;
        dialogue_C_pd.stopped += OnPlayableDirectorStopped;
        dialogue_D_pd.stopped += OnPlayableDirectorStopped;
        dialogue_E_pd.stopped += OnPlayableDirectorStopped;
        dialogue_F_pd.stopped += OnPlayableDirectorStopped;

        end_1_pd.stopped += OnPlayableDirectorStopped;
        end_2_pd.stopped += OnPlayableDirectorStopped;
        end_3_pd.stopped += OnPlayableDirectorStopped;
        end_8_pd.stopped += OnPlayableDirectorStopped;
        end_9_pd.stopped += OnPlayableDirectorStopped;
        end_4_pd.stopped += OnPlayableDirectorStopped;
        end_5_pd.stopped += OnPlayableDirectorStopped;
        end_6_pd.stopped += OnPlayableDirectorStopped;
        end_7_pd.stopped += OnPlayableDirectorStopped;

    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == Act_1_4_TextPlayableDirector)
        {
            target1_Text.SetActive(true);
            Debug.Log("Act_1_4_TextPlayableDirector has been played.");
        }
        else if (director == target1_TextPlayableDirector)
        {
            target2_Text.SetActive(true);
            Debug.Log("target1_TextPlayableDirector has been played.");
        }
        else if (director == target2_TextPlayableDirector)
        {
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("target2_TextPlayableDirector has been played.");
        }
        else if (director == stronger_ani)
        {
            stronger_Text.SetActive(true);
            Debug.Log("stronger_ani has been played.");
        }
        else if (director == end_4_pd)
        {
            handleResult();
            Debug.Log("end_4_pd has been played.");
        }
        else if (director == end_5_pd)
        {
            handleResult();

            Debug.Log("end_5_pd has been played.");
        }
        else if (director == end_6_pd)
        {
            handleResult();

            Debug.Log("end_6_pd has been played.");
        }
        else if (director == end_7_pd)
        {
            handleResult();

            Debug.Log("end_7_pd has been played.");
        }
        else if (director == failedTextPlayableDirector)
        {
            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("failedTextPlayableDirector has been played.");
        }
        else if (director == failed2TextPlayableDirector)
        {
            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("failed2TextPlayableDirector has been played.");
        }
        else if (director == clearedTextPlayableDirector)
        {
            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("clearedTextPlayableDirector has been played.");
        }
        else if (director == target1_CompletedTextPlayableDirector)
        {
            //Done_target1 = true;
            Debug.Log("target1_CompletedTextPlayableDirector has been played.");
        }
        else if (director == target2_CompletedTextPlayableDirector)
        {
            //Done_target2 = true;
            Debug.Log("target2_CompletedTextPlayableDirector has been played.");
        }
        else if (director == explode_b_ani)
        {
            
            Debug.Log("explode_b_ani has been played.");
        }
        else if((director == dialogue_D_pd) || (director == dialogue_F_pd))
        {
            newCharaAnounce.SetActive(true);
        }
    }


    void OnDestroy()
    {
        Act_1_4_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        target1_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        target2_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        failedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        failed2TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        clearedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        stronger_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        stronger_ani.stopped -= OnPlayableDirectorStopped;
        explode_b_ani.stopped -= OnPlayableDirectorStopped;
        explode_c_ani.stopped -= OnPlayableDirectorStopped;

        dialogue_A_pd.stopped -= OnPlayableDirectorStopped;
        dialogue_B1_pd.stopped -= OnPlayableDirectorStopped;
        dialogue_B2_pd.stopped -= OnPlayableDirectorStopped;
        dialogue_C_pd.stopped -= OnPlayableDirectorStopped;
        dialogue_D_pd.stopped -= OnPlayableDirectorStopped;
        dialogue_E_pd.stopped -= OnPlayableDirectorStopped;
        dialogue_F_pd.stopped -= OnPlayableDirectorStopped;

        end_1_pd.stopped -= OnPlayableDirectorStopped;
        end_2_pd.stopped -= OnPlayableDirectorStopped;
        end_3_pd.stopped -= OnPlayableDirectorStopped;
        end_8_pd.stopped -= OnPlayableDirectorStopped;
        end_9_pd.stopped -= OnPlayableDirectorStopped;
        end_4_pd.stopped -= OnPlayableDirectorStopped;
        end_5_pd.stopped -= OnPlayableDirectorStopped;
        end_6_pd.stopped -= OnPlayableDirectorStopped;
        end_7_pd.stopped -= OnPlayableDirectorStopped;
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

                    Debug.Log("LoadScene:Act_1_2.5");
                    SceneManager.LoadScene("Act_1_2.5");

                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("LoadScene:Act_1_4");
            SceneManager.LoadScene("Act_1_4");
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

    IEnumerator MoveMenuToTarget(Vector3 targetPosition, float m_speed)
    {
        while (menu.transform.position != targetPosition)
        {
            menu.transform.position = Vector3.MoveTowards(menu.transform.position, targetPosition, Time.deltaTime * m_speed);
            yield return null;
        }
        menuIsOnItsPos = true;

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

        // A1_4hoverScript スクリプトをホバーエリアに追加する
        Act_1_4_hoverArea A1_4_hoverScript = hoverArea.AddComponent<Act_1_4_hoverArea>();         // 【＊大切】Need changed NOTICE！
        A1_4_hoverScript.Initialize(pointA, pointB, horizontalLineMaterial, horizontalLineWidth, tooltipPrefab);   // A1_4_hoverScript スクリプトを初期化する

        // A1_4_hoverScript スクリプトが追加されたことをデバッグログで表示する
        Debug.Log($"A1_4_hoverScript added to {hoverArea.name}");

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

        // A1_4_charaHoverAreaScript スクリプトをキャラクターに追加する
        Act_1_4_charaHoverArea Act_1_4_charaHoverAreaScript = character.AddComponent<Act_1_4_charaHoverArea>();

        // A1_4_charaHoverAreaScript スクリプトの情報番号を設定する
        Act_1_4_charaHoverAreaScript.charaInfoNum = charaInfoNum;

        // A1_4_charaHoverAreaScriptスクリプトを初期化する
        Act_1_4_charaHoverAreaScript.Initialize(character, charaInfoPrefab, charaInfoPosition);
    }
}
