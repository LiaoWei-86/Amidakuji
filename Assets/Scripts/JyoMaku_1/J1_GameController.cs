using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class J1_GameController : MonoBehaviour
{
    public GameObject JyomakuText; // ゲームオブジェクト JyomakuText
    public PlayableDirector JyomakuTextPlayableDirector; // JyomakuTextのPlayableDirector

    public GameObject targetText; // ゲームオブジェクト targetText
    public PlayableDirector targetTextPlayableDirector; // targetTextのPlayableDirector

    public GameObject targetCompletedText; // ゲームオブジェクト targetCompletedText
    public PlayableDirector targetCompletedTextPlayableDirector; // targetCompletedTextのPlayableDirector

    public GameObject didnotfriendText; // ゲームオブジェクト didnotfriendText
    public PlayableDirector didnotfriendTextPlayableDirector; // didnotfriendTextのPlayableDirector

    public GameObject didnotMetText; // ゲームオブジェクト didnotMetText
    public PlayableDirector didnotMetTextPlayableDirector; // didnotMetTextのPlayableDirector

    public GameObject befriend; //プロットアイコンbefriend
    public PlayableDirector befriendPlayableDirector; // プロットアイコンbefriendのPlayableDirector

    public GameObject becameFriendText; // ゲームオブジェクト becameFriendText
    public PlayableDirector becameFriendTextPlayableDirector; // becameFriendTextのPlayableDirector

    public PlayableDirector swordPlayableDirector; //  swordのPlayableDirector

    // 結末アイコンにたどり着いた時のセリフ

    // 失敗の場合
    public GameObject dialogue_failed_hunter; // ゲームオブジェクト dialogue_failed_hunter
    public PlayableDirector dialogue_failed_hunterPlayableDirector; // dialogue_failed_hunterのPlayableDirector

    public GameObject dialogue_failed_knight; // ゲームオブジェクト dialogue_failed_knight
    public PlayableDirector dialogue_failed_knightPlayableDirector; // dialogue_failed_knightのPlayableDirector

    // 酒場だけに行った場合
    public GameObject dialogue_beer_hunter; // ゲームオブジェクト dialogue_beer_hunter
    public PlayableDirector dialogue_beer_hunterPlayableDirector; // dialogue_beer_hunterのPlayableDirector

    public GameObject dialogue_beer_knight; // ゲームオブジェクト dialogue_beer_knight
    public PlayableDirector dialogue_beer_knightPlayableDirector; // dialogue_beer_knightのPlayableDirector

    // 戦って酒場だけに行った場合
    public GameObject dialogue_battleBeer_hunter; // ゲームオブジェクト dialogue_battleBeer_hunter
    public PlayableDirector dialogue_battleBeer_hunterPlayableDirector; // dialogue_battleBeer_hunterのPlayableDirector

    public GameObject dialogue_battleBeer_knight; // ゲームオブジェクト dialogue_battleBeer_knight
    public PlayableDirector dialogue_battleBeer_knightPlayableDirector; // dialogue_battleBeer_knightのPlayableDirector

    // 線を引かず、そのまま結末アイコンにたどり着いた場合
    public GameObject dialogue_OE_hunter; // ゲームオブジェクト dialogue_OE_hunter
    public PlayableDirector dialogue_OE_hunterPlayableDirector; // dialogue_OE_hunterのPlayableDirector

    public GameObject dialogue_OE_knight; // ゲームオブジェクト dialogue_OE_knight
    public PlayableDirector dialogue_OE_knightPlayableDirector; // dialogue_OE_knightのPlayableDirector

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

    public GameObject fukidashi; // 吹き出しのゲームオブジェクト
    public PlayableDirector dialogue; // セリフのPlayableDirector
    public GameObject dog; // 犬

    public GameObject knight;
    public GameObject hunter;

    private Coroutine knightMovementCoroutine;
    private Coroutine hunterMovementCoroutine;

    public bool hasMovementFinshed = false; //　キャラクターの移動は完成されたか？のブール値
    public bool theyAreFriend = false; //　騎士と猟師は友人になったか？のブール値

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

    // 遊び終わったらメニューが飛んでくる
    public GameObject menu; // menu_controller
    public Vector3 menuTargetPosition = new Vector3(5.5f, -4.2f, 0); // たどり着いて欲しい座標
    public bool menuIsOnItsPos = false; // メニューは目標座標に着いたかをfalseとマークする
    public float moveSpeed = 1.5f; // メニューの移動スピード
    public TMP_Text cannotENTER; // メニューの「Enter：進む」

    public AudioClip leftClickClip;
    public AudioClip rightClickClip;

    public AudioSource audioSourceJ1;

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
        CreateHoverAreaCharacter(knight, 1);
        CreateHoverAreaCharacter(hunter, 2);

        //  開始時にを非表示にする
        if (targetText != null)
        {
            targetText.SetActive(false);
        }
        if (didnotfriendText != null)
        {
            didnotfriendText.SetActive(false);
        }
        if (didnotMetText != null)
        {
            didnotMetText.SetActive(false);
        }
        if (becameFriendText != null)
        {
            becameFriendText.SetActive(false);
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
        if( menu != null)
        {
            menu.SetActive(false);
        }


        // PlayableDirectorがnullでないことを確認し、再生完了イベントをサブスクライブ
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
        if (didnotMetTextPlayableDirector != null)
        {
            didnotMetTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (didnotfriendTextPlayableDirector != null)
        {
            didnotfriendTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("didnotfriendTextPlayableDirector is not assigned.");
        }
        if (befriendPlayableDirector != null)
        {
            befriendPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("befriendPlayableDirector is not assigned.");
        }
        if (becameFriendTextPlayableDirector != null)
        {
            becameFriendTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning(" becameFriendTextPlayableDirector is not assigned.");
        }
        if (dialogue != null)
        {
            dialogue.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("dialogue is not assigned.");
        }

        if (swordPlayableDirector != null)
        {
            swordPlayableDirector.stopped += OnPlayableDirectorStopped;
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

        // Initialize the dictionary and add the points
        pointsDictionary = new Dictionary<int, Vector3>();

        // Add knight and hunter start and end points
        pointsDictionary.Add(0, startpoint_knight.position); // 左から1番目のスタート点
        pointsDictionary.Add(1, startpoint_hunter.position); // 左から2番目のスタート点
        pointsDictionary.Add(6, endtpoint_knight.position);  // 左から1番目の終点
        pointsDictionary.Add(7, endpoint_hunter.position);   // 左から2番目の終点

        // Add the positions of the other points (4 gameobjects)
        for (int i = 0; i < tenPositions.Length; i++)
        {
            pointsDictionary.Add(i + 2, tenPositions[i].position); // 点の位置（2から5まで）
        }

        // すべての点の座標を表示する（デバッグ用）
        foreach (KeyValuePair<int, Vector3> point in pointsDictionary)
        {
            Debug.Log("Point " + point.Key + ": " + point.Value);
        }

        CreateHoverAreaJ1(tenGameObjects[0],tenGameObjects[1]);
        CreateHoverAreaJ1(tenGameObjects[2], tenGameObjects[3]);
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

    public void waitForSceneChange_Menu()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (menuIsOnItsPos == true)
            {
                if (theyAreFriend)
                {

                    Debug.Log("LoadScene:JyoMaku_1.5_YiChi");
                    SceneManager.LoadScene("JyoMaku_1.5_YiChi");

                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("LoadScene:JyoMaku_1");
            SceneManager.LoadScene("JyoMaku_1");
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

    public void ChangeTextColor(TMP_Text tmp)
    {
        if (theyAreFriend == false)
        {
            tmp.color = Color.gray; // もし`failed`は`true`であれば，テキストの色を灰色に変える
        }
    }

    public void charaMoveAndAnimationLogic()
    {
        //  このモードでは、プレイヤーがEnterを押すと、キャラクターの移動＋プロットアイコンの生成＋ストーリーメッセージの生成を一つずつ表示される


        /*
           　　　　    「騎士」0　　　　　「猟師」1
                           ||                ||
                 circle1   ○2     circle2   ○3
                           ||                ||
                 circle3   ○4     circle4   ○5
                           ||                ||
       　　　　　　   「結末」6　　　　　「結末」7
        */

        if (isHorizontal_1_LineCreated == true && isHorizontal_2_LineCreated == false) // 失敗：戦っただけで終わった
        {
            switch (currentMovementIndex)
            {
                case 0:
                    StartMovement(new List<int> { 0, 2, 3 }, new List<int> { 1, 3, 2 });
                    break;
                case 1:
                    StartMovement(new List<int> { 3, 3 }, new List<int> { 2, 2 }); // キャラクターを止まらせてアニメーションを再生する
                    swordPlayableDirector.Play();
                    break;
                case 2:
                    StartMovement(new List<int> { 3, 5 }, new List<int> { 2, 4 });
                    break;
                case 3:
                    StartMovement(new List<int> { 5, 5 }, new List<int> { 4, 6 });

                    break;
                case 4:
                    StartMovement(new List<int> { 5, 5 }, new List<int> { 6, 6 }); // 猟師が着いたから、止まらせて吹き出しを表示する
                    dialogue_failed_hunter.SetActive(true);

                    break;

                case 5:
                    StartMovement(new List<int> { 5, 7 }, new List<int> { 6, 6 });

                    break;

                case 6:
                    StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 6 }); // 騎士も着いたから、止まらせて吹き出しを表示する
                    dialogue_failed_knight.SetActive(true);

                    hasMovementFinshed = true;

                    break;
            }

        }
        else if (isHorizontal_1_LineCreated == true && isHorizontal_2_LineCreated == true) // 成功：戦って酒場に行った
        {
            switch (currentMovementIndex)
            {
                case 0:
                    StartMovement(new List<int> { 0, 2, 3 }, new List<int> { 1, 3, 2 });
                    break;

                case 1:
                    StartMovement(new List<int> { 3, 3 }, new List<int> { 2, 2 });
                    swordPlayableDirector.Play();
                    break;

                case 2:
                    StartMovement(new List<int> { 3, 5, 4 }, new List<int> { 2, 4, 5 });

                    break;
                case 3:
                    StartMovement(new List<int> { 4, 4 }, new List<int> { 5, 5 });
                    befriendPlayableDirector.Play();

                    break;

                case 4:
                    StartMovement(new List<int> { 4, 6 }, new List<int> { 5, 5 });
                    break;

                case 5:
                    StartMovement(new List<int> { 6, 6 }, new List<int> { 5, 5 });
                    dialogue_battleBeer_knight.SetActive(true);

                    break;

                case 6:

                    StartMovement(new List<int> { 6, 6 }, new List<int> { 5, 7 });

                    break;

                case 7:
                    StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 });
                    dialogue_battleBeer_hunter.SetActive(true);

                    hasMovementFinshed = true;
                    break;
            }
        }
        else if (isHorizontal_1_LineCreated == false && isHorizontal_2_LineCreated == true) // 成功：酒場だけに行った
        {
            switch (currentMovementIndex)
            {
                case 0:
                    StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });
                    break;
                case 1:
                    StartMovement(new List<int> { 2, 4, 5 }, new List<int> { 3, 5, 4 });
                    break;
                case 2:
                    StartMovement(new List<int> { 5, 5 }, new List<int> { 4, 4 });
                    befriendPlayableDirector.Play();
                    break;
                case 3:
                    StartMovement(new List<int> { 5, 5 }, new List<int> { 4, 6 });

                    break;

                case 4:
                    StartMovement(new List<int> { 5, 5 }, new List<int> { 6, 6 });
                    dialogue_beer_hunter.SetActive(true);

                    break;

                case 5:
                    StartMovement(new List<int> { 5, 7 }, new List<int> { 6, 6 });

                    break;

                case 6:
                    StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 6 });
                    dialogue_beer_knight.SetActive(true);

                    hasMovementFinshed = true;
                    break;
            }

        }
        else if (isHorizontal_1_LineCreated == false && isHorizontal_2_LineCreated == false) // 失敗：そのまま吐いた
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
                    StartMovement(new List<int> { 4, 6 }, new List<int> { 5, 5 });

                    break;

                case 3:
                    StartMovement(new List<int> { 6, 6 }, new List<int> { 5, 5 });
                    dialogue_OE_knight.SetActive(true);

                    break;

                case 4:
                    StartMovement(new List<int> { 6, 6 }, new List<int> { 5, 7 });

                    break;

                case 5:
                    StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 });
                    dialogue_OE_hunter.SetActive(true);

                    hasMovementFinshed = true;

                    break;

            }
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

    IEnumerator MoveMenuToTarget(Vector3 targetPosition, float m_speed)
    {
        while (menu.transform.position != targetPosition)
        {
            menu.transform.position = Vector3.MoveTowards(menu.transform.position, targetPosition, Time.deltaTime * m_speed);
            yield return null;
        }
        menuIsOnItsPos = true;
   
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
        hoverArea.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);   // ホバーエリアの回転を、点Aから点Bへの方向に基づいて設定する

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

    private void ShowNewCharacter()
    {
        fukidashi.SetActive(true);
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
            currentGameMode = GameMode.PlayerPlaying;

            Debug.Log(" targetText Timeline playback completed.");
            Debug.Log("GameMode.PlayerPlayingに切り替える");
        }
        else if(director == targetCompletedTextPlayableDirector)
        {
            theyAreFriend = true;
            Debug.Log("they are friend:" + theyAreFriend);

            currentGameMode = GameMode.WaitForSceneChange;
        }
        else if (director == didnotfriendTextPlayableDirector)
        {
            currentGameMode = GameMode.WaitForSceneChange;

            Debug.Log(" didnotfriendText Timeline playback completed.");
            Debug.Log("GameMode.WaitForSceneChangeに切り替える");
        }
        else if (director == didnotMetTextPlayableDirector)
        {
            currentGameMode = GameMode.WaitForSceneChange;

            Debug.Log(" didnotMetText Timeline playback completed.");
            Debug.Log("GameMode.WaitForSceneChangeに切り替える");
        }
        else if (director == befriendPlayableDirector)
        {
            becameFriendText.SetActive(true);
            becameFriendTextPlayableDirector.Play(); 
            Debug.Log(" befriend Timeline playback completed.");
        }
        else if (director == becameFriendTextPlayableDirector)
        {
            ShowNewCharacter();

            Debug.Log("becameFriendText Timeline playback completed.");
        }
        else if (director == dialogue)
        {
            dog.SetActive(true);

            Debug.Log("dialogue Timeline playback completed.");
        }
        else if (director == swordPlayableDirector)
        {
            fukidashi_0.SetActive(true);

            Debug.Log("swordPlayableDirector Timeline playback completed.");
        }
        else if(director == dialogue_failed_knightPlayableDirector)
        {
            didnotfriendText.SetActive(true);
            didnotfriendTextPlayableDirector.Play();
        }
        else if (director == dialogue_beer_knightPlayableDirector)
        {
            targetCompletedText.SetActive(true);
        }
        else if (director == dialogue_battleBeer_hunterPlayableDirector)
        {
            targetCompletedText.SetActive(true);
        }
        else if (director == dialogue_OE_hunterPlayableDirector)
        {
            didnotMetText.SetActive(true);
            didnotMetTextPlayableDirector.Play();
        }
    }
    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
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

        if (didnotfriendTextPlayableDirector != null)
        {
            didnotfriendTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (didnotMetTextPlayableDirector != null)
        {
            didnotMetTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (befriendPlayableDirector != null)
        {
            befriendPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (becameFriendTextPlayableDirector != null)
        {
            becameFriendTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (dialogue != null)
        {
            dialogue.stopped -= OnPlayableDirectorStopped;
        }

        if (swordPlayableDirector != null)
        {
            swordPlayableDirector.stopped -= OnPlayableDirectorStopped;
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
    }
}
