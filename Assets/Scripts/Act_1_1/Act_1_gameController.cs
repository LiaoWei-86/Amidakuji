using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;
public class Act_1_gameController : MonoBehaviour
{
    public GameObject Act_1_Text; // ゲームオブジェクト Act_1_Text
    public PlayableDirector Act_1_TextPlayableDirector; // Act_1_TextのPlayableDirector

    public GameObject target_A1_Text; // ゲームオブジェクト target_A1_Text
    public PlayableDirector target_A1_TextPlayableDirector; // target_A1_TextのPlayableDirector

    public GameObject targetCompletedText; // ゲームオブジェクト targetCompletedText
    public PlayableDirector targetCompletedTextPlayableDirector; // targetCompletedTextのPlayableDirector

    public GameObject failedText; // ゲームオブジェクト failedText
    public PlayableDirector failedTextPlayableDirector; // failedTextのPlayableDirector

    public GameObject clearedText; // ゲームオブジェクト clearedText
    public PlayableDirector clearedTextPlayableDirector; // failedTextのPlayableDirector

    public bool cleared = false; // このステージをクリアできたか？をfalseとマークする（まだクリアできていないということ）

    public GameObject dialogue_1; // あみだくじの途中ででてくる吹き出し(1)
    public PlayableDirector dialogue_1_PlayableDirector; // あみだくじの途中ででてくる吹き出し(1)

    public GameObject dialogue_2; // あみだくじの途中ででてくる吹き出し(2)
    public PlayableDirector dialogue_2_PlayableDirector; // あみだくじの途中ででてくる吹き出し(2)

    public GameObject dialogue_3; // あみだくじの途中ででてくる吹き出し(3)
    public PlayableDirector dialogue_3_PlayableDirector; // あみだくじの途中ででてくる吹き出し(3)

    public List<GameObject> endings; // ending、end_dialogueなど
    public PlayableDirector[] endings_PlayableDirectors; // ending、end_dialogueなどのPlayableDirector

    public GameObject treature_beThrown; // treature_beThrownのゲームオブジェクト
    public PlayableDirector treature_beThrown_PlayableDirector; // treature_beThrownのPlayableDirector

    public GameObject treature_bePassed; // treature_bePassedのゲームオブジェクト
    public PlayableDirector treature_bePassed_PlayableDirector; // treature_bePassedのPlayableDirector

    public Transform startpoint_character1; // 左から1番目のスタート点の位置
    public Transform startpoint_character2; // 左から2番目のスタート点の位置
    public GameObject[] tenGameObjects; // それぞれの点
    public Transform[] tenPositions; // それぞれの点の位置
    public Transform endtpoint_character1; //左から1番目の終点の位置
    public Transform endpoint_character2; // 左から2番目の終点の位置
    public Dictionary<int, Vector3> pointsDictionary;// 点とその位置情報を格納する辞書

    public bool isHorizontal_1_LineCreated = false; //　1本目の横線は描かれたか？のブール値
    public bool isHorizontal_2_LineCreated = false; //　2本目の横線は描かれたか？のブール値

    public GameObject character1; // 左から1番目のキャラクター
    public GameObject character2; // 左から2番目のキャラクター

    public GameObject treature_line; // 線にある宝
    public bool _is_treature_got = false; // 宝は取られたか？をfalseとマークする 
    public GameObject treature_get; // 犬に取られた宝
    public GameObject treasure_to_hunter; // 猟師に渡された宝

    private Coroutine character1MovementCoroutine; // 左から1番目のキャラクターの移動
    private Coroutine character2MovementCoroutine; // 左から2番目のキャラクターの移動

    public bool hasMovementFinshed = false; //　キャラクターの移動は完成されたか？のブール値
    private int currentMovementIndex = 0; // プレイヤーがEnterを押す際にプロットアイコンを生成するために計算用のIndex

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

    public GameObject newCharaAnounce; // 新しいキャラクターが登場したという通知
    public PlayableDirector newCharaAnPlayableDirector; // 新しいキャラクターが登場したという通知 PlayableDirector

    public AudioClip leftClickClip;
    public AudioClip rightClickClip;
    public AudioClip itemGotClip;

    public AudioSource audioSourceA1;

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
        CreateHoverAreaCharacter(character1, 4); // 左＝character1＝犬＝4
        CreateHoverAreaCharacter(character2, 2); // 右＝character2＝猟師＝2

        //  開始時にを非表示にする
        if (target_A1_Text != null)
        {
            target_A1_Text.SetActive(false);
        }
        if (targetCompletedText != null)
        {
            targetCompletedText.SetActive(false);
        }
        if (failedText != null)
        {
            failedText.SetActive(false);
        }
        if (clearedText != null)
        {
            clearedText.SetActive(false);
        }
        if (newCharaAnounce != null)
        {
            newCharaAnounce.SetActive(false);
        }
        if (menu != null)
        {
            menu.SetActive(false);
        }
        if(treature_get != null)
        {
            treature_get.SetActive(false);
        }
        if (dialogue_1 != null)
        {
            dialogue_1.SetActive(false);
        }
        if (dialogue_2 != null)
        {
            dialogue_2.SetActive(false);
        }
        if (dialogue_3 != null)
        {
            dialogue_3.SetActive(false);
        }

        if (endings!= null)
        {
            // endingsの非表示をループで行う
            foreach (var ending in endings)
            {
                ending.SetActive(false);
            }
        }
        if (treature_beThrown != null)
        {
            treature_beThrown.SetActive(false);
        }
        if (treature_bePassed != null)
        {
            treature_bePassed.SetActive(false);
        }
        if (treasure_to_hunter != null)
        {
            treasure_to_hunter.SetActive(false);
        }

        // PlayableDirectorがnullでないことを確認し、再生完了イベントをサブスクライブ
        if (newCharaAnPlayableDirector != null)
        {
            newCharaAnPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (Act_1_TextPlayableDirector != null)
        {
            Act_1_TextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (target_A1_TextPlayableDirector != null)
        {
            target_A1_TextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (failedTextPlayableDirector != null)
        {
            failedTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (clearedTextPlayableDirector != null)
        {
            clearedTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (targetCompletedTextPlayableDirector != null)
        {
            targetCompletedTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (dialogue_1_PlayableDirector != null)
        {
            dialogue_1_PlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (dialogue_2_PlayableDirector != null)
        {
            dialogue_2_PlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (dialogue_3_PlayableDirector != null)
        {
            dialogue_3_PlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (treature_beThrown_PlayableDirector != null)
        {
            treature_beThrown_PlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (treature_bePassed_PlayableDirector != null)
        {
            treature_bePassed_PlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        // PlayableDirectorがnullでないことを確認し、再生完了イベントをサブスクライブ
        for (int i = 0; i < endings_PlayableDirectors.Length; i++)
        {
            if (endings_PlayableDirectors[i] != null)
            {
                endings_PlayableDirectors[i].stopped += OnPlayableDirectorStopped;
                Debug.Log($"endings_PlayableDirectors[{i}] += OnPlayableDirectorStopped.");
            }
            else
            {
                Debug.LogWarning($"endings_PlayableDirectors[{i}] is not assigned.");
            }
        }


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
            pointsDictionary.Add(i + 2, tenPositions[i].position); // 点の位置（2から5まで）
        }

        // すべての点の座標を表示する（デバッグ用）
        foreach (KeyValuePair<int, Vector3> point in pointsDictionary)
        {
            Debug.Log("Point " + point.Key + ": " + point.Value);
        }

        CreateHoverAreaA1(tenGameObjects[0], tenGameObjects[1]);
        CreateHoverAreaA1(tenGameObjects[2], tenGameObjects[3]);
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

        //  このモードでは、プレイヤーがEnterを押すと、キャラクターの移動＋プロットアイコンの生成＋ストーリーメッセージの生成を一つずつ表示される
        /*
         * dialogue_1　上だけ繋いだ、犬は宝を捨てた
         * dialogue_2　下だけ繋いだ、海賊登場
         * dialogue_3　横線2本繋いだ場合、2本目の吹き出し
         endings[0]=endings_PlayableDirectors[0] 猟師は食べ物の結末アイコンに着いた
         endings[1]=endings_PlayableDirectors[1] 猟師は1本目の横線で海賊船に着いた
         endings[2]=endings_PlayableDirectors[2] 成功。猟師は海賊船に着いた。ペンダントが現れた。
         endings[3]=endings_PlayableDirectors[3] 成功。犬は2本目の横線を通して食べ物に着いた
         endings[4]=endings_PlayableDirectors[4] 犬は宝を捨てて、食べ物に着いた。
         endings[5]=endings_PlayableDirectors[5] 犬は宝をもって海賊船に着いた。宝を返した
         endings[6]=endings_PlayableDirectors[6] 犬は宝を捨てて、海賊船に着いた。海賊に𠮟られた
        */

        /*
           　　　　     「犬」0　　　　　「猟師」1
                           ||                ||
                 circle1   ○2     circle2   ○3
                           ||                ||
                 circle3   ○4     circle4   ○5
                           ||                ||
       　　　　　　   「結末」6　　　　　「結末」7
        */

        // 失敗。上の横線だけ接続された。犬は宝を捨てたので、猟師に渡せなかった
        if (isHorizontal_1_LineCreated == true && isHorizontal_2_LineCreated == false)
        {
            switch (currentMovementIndex)
            {
                case 0:
                    StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });

                    break;

                case 1:
                    StartMovement(new List<int> { 2, 2 }, new List<int> { 3, 3 });
                    treature_line.SetActive(false);
                    treature_get.SetActive(true);
                    audioSourceA1.PlayOneShot(itemGotClip);
                    break;

                case 2:
                    StartMovement(new List<int> { 2, 2 }, new List<int> { 3, 3 });
                    treature_get.SetActive(false);
                    treature_beThrown.SetActive(true);
                    treature_beThrown_PlayableDirector.Play();

                    break;

                case 3:
                    StartMovement(new List<int> { 2, 3 }, new List<int> { 3, 2 });

                    break;

                case 4:
                    StartMovement(new List<int> { 3, 3 }, new List<int> { 2, 2 });
                    dialogue_1.SetActive(true);
                    dialogue_1_PlayableDirector.Play();
                    break;

                case 5:
                    StartMovement(new List<int> { 3, 5 }, new List<int> { 2, 4 });
                    break;

                case 6:
                    StartMovement(new List<int> { 5, 5 }, new List<int> { 4, 6 });

                    break;

                case 7:
                    StartMovement(new List<int> { 5, 5 }, new List<int> { 6, 6 });
                    endings[1].SetActive(true);
                    endings_PlayableDirectors[1].Play();
                    break;

                case 8:
                    StartMovement(new List<int> { 5, 7 }, new List<int> { 6, 6 });
                    break;

                case 9:
                    StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 6 });
                    endings[4].SetActive(true);
                    endings_PlayableDirectors[4].Play();

                    break;
            }
        }
        // 失敗。2本とも接続された。犬は宝を捨てたので、猟師に渡せなかった
        else if (isHorizontal_1_LineCreated == true && isHorizontal_2_LineCreated == true)
        {
            switch (currentMovementIndex)
            {
                case 0:
                    StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });

                    break;

                case 1:
                    StartMovement(new List<int> { 2, 2 }, new List<int> { 3, 3 });
                    treature_line.SetActive(false);
                    treature_get.SetActive(true);
                    audioSourceA1.PlayOneShot(itemGotClip);
                    break;

                case 2:
                    StartMovement(new List<int> { 2, 2 }, new List<int> { 3, 3 });
                    treature_get.SetActive(false);
                    treature_beThrown.SetActive(true);
                    treature_beThrown_PlayableDirector.Play();

                    break;

                case 3:
                    StartMovement(new List<int> { 2, 3 }, new List<int> { 3, 2 });

                    break;
                case 4:
                    StartMovement(new List<int> { 3, 3 }, new List<int> { 2, 2 });
                    dialogue_1.SetActive(true);
                    dialogue_1_PlayableDirector.Play();
                    break;

                case 5:
                    StartMovement(new List<int> { 3, 5, 4 }, new List<int> { 2, 4, 5 });

                    break;

                case 6:
                    StartMovement(new List<int> { 4, 4 }, new List<int> { 5, 5 });
                    dialogue_3.SetActive(true);
                    dialogue_3_PlayableDirector.Play();
                    break;

                case 7:
                    StartMovement(new List<int> { 4, 6 }, new List<int> { 5, 5 });
                    break;

                case 8:
                    StartMovement(new List<int> { 6, 6 }, new List<int> { 5, 5 });
                    endings[6].SetActive(true);
                    endings_PlayableDirectors[6].Play();
                    break;

                case 9:
                    StartMovement(new List<int> { 6, 6 }, new List<int> { 5, 7 });
                    break;

                case 10:
                    StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 });
                    endings[0].SetActive(true);
                    endings_PlayableDirectors[0].Play();
                    break;
            }
        }
        // 成功。下の横線だけ接続されたので、宝を捨てないで猟師に渡した
        else if (isHorizontal_1_LineCreated == false && isHorizontal_2_LineCreated == true)
        {
            switch (currentMovementIndex)
            {
                case 0:
                    StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });

                    break;

                case 1:
                    StartMovement(new List<int> { 2, 2 }, new List<int> { 3, 3 });
                    treature_line.SetActive(false);
                    treature_get.SetActive(true);
                    audioSourceA1.PlayOneShot(itemGotClip);
                    break;

                case 2:
                    StartMovement(new List<int> { 2, 4, 5 }, new List<int> { 3, 5, 4 });
                    break;

                case 3:
                    StartMovement(new List<int> { 5, 5 }, new List<int> { 4, 4 });
                    treature_bePassed.SetActive(true);
                    treature_get.SetActive(false);

                    break;

                case 4:
                    StartMovement(new List<int> { 5, 5 }, new List<int> { 4, 6 });
                    treature_bePassed.SetActive(false);
                    treasure_to_hunter.SetActive(true);
                    break;

                case 5:
                    StartMovement(new List<int> { 5, 5 }, new List<int> { 6, 6 });
                    treasure_to_hunter.SetActive(false);
                    endings[2].SetActive(true);
                    endings_PlayableDirectors[2].Play();
                    break;

                case 6:
                    StartMovement(new List<int> { 5, 7 }, new List<int> { 6, 6 });
                    break;

                case 7:
                    StartMovement(new List<int> { 7, 7 }, new List<int> { 6, 6 });
                    endings[3].SetActive(true);
                    endings_PlayableDirectors[3].Play();
                    break;
            }
        }
        // 失敗。2本とも接続されてない、結末アイコンに直通
        else if (isHorizontal_1_LineCreated == false && isHorizontal_2_LineCreated == false)
        {
            switch (currentMovementIndex)
            {
                case 0:
                    StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });

                    break;

                case 1:
                    StartMovement(new List<int> { 2, 2 }, new List<int> { 3, 3 });
                    treature_line.SetActive(false);
                    treature_get.SetActive(true);
                    audioSourceA1.PlayOneShot(itemGotClip);
                    break;

                case 2:
                    StartMovement(new List<int> { 2, 4 }, new List<int> { 3, 5 });
                    break;

                case 3:
                    StartMovement(new List<int> { 4, 6 }, new List<int> { 5, 5 });
                    break;

                case 4:
                    StartMovement(new List<int> { 6, 6 }, new List<int> { 5, 5 });
                    treature_get.SetActive(false);
                    endings[5].SetActive(true);
                    endings_PlayableDirectors[5].Play();
                    break;

                case 5:
                    StartMovement(new List<int> { 6, 6 }, new List<int> { 5, 7 });
                    break;

                case 6:
                    StartMovement(new List<int> { 6, 6 }, new List<int> { 7, 7 });
                    endings[0].SetActive(true);
                    endings_PlayableDirectors[0].Play();
                    break;

            }

        }
    }

    public void ChangeTextColor(TMP_Text tmp)
    {
        if (cleared == false)
        {
            tmp.color = Color.gray; // もしcleared == falseであれば，テキストの色を灰色に変える
        }
    }

    public void waitForSceneChange_Menu()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (menuIsOnItsPos == true)
            {
                if (cleared)
                {

                    Debug.Log("LoadScene:Act_1_1.5");
                    SceneManager.LoadScene("Act_1_1.5");

                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("LoadScene:Act_1_1");
            SceneManager.LoadScene("Act_1_1");
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

    // 点のペアごとにホバーエリアを生成するメソッド
    void CreateHoverAreaA1(GameObject pointA, GameObject pointB)
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
        Act_1_hoverArea A1_hoverScript = hoverArea.AddComponent<Act_1_hoverArea>();         // 【＊大切】Need changed NOTICE！
        A1_hoverScript.Initialize(pointA, pointB, horizontalLineMaterial, horizontalLineWidth, tooltipPrefab);   // A1_hoverScript スクリプトを初期化する

        // A1_hoverScript スクリプトが追加されたことをデバッグログで表示する
        Debug.Log($"A1_hoverScript added to {hoverArea.name}");

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

        // A1_charaHoverAreaScript スクリプトをキャラクターに追加する
        Act_1_charaHoverArea A1_charaHoverAreaScript = character.AddComponent<Act_1_charaHoverArea>();

        // A1_charaHoverAreaScript スクリプトの情報番号を設定する
        A1_charaHoverAreaScript.charaInfoNum = charaInfoNum;

        // A1_charaHoverAreaScriptスクリプトを初期化する
        A1_charaHoverAreaScript.Initialize(character, charaInfoPrefab, charaInfoPosition);
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

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == Act_1_TextPlayableDirector)
        {
            target_A1_Text.SetActive(true);
            target_A1_TextPlayableDirector.Play();

            Debug.Log(" Act_1_TextPlayableDirector Timeline playback completed.");
        }
        else if (director == target_A1_TextPlayableDirector)
        {
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("target_A1_TextPlayableDirector Timeline playback completed.");
            Debug.Log("GameMode.PlayerPlayingに切り替える");
        }
        else if (director == failedTextPlayableDirector)
        {
            Debug.Log("クリアできた？:" + cleared);

            currentGameMode = GameMode.WaitForSceneChange;

            Debug.Log(" failedTextPlayableDirector Timeline playback completed.");
        }
        else if (director == clearedTextPlayableDirector)
        {
            targetCompletedText.SetActive(true);
            targetCompletedTextPlayableDirector.Play();

            Debug.Log("clearedTextPlayableDirector Timeline playback completed.");
        }
        else if (director == endings_PlayableDirectors[3])
        {
            cleared = true;
            Debug.Log("クリアできた？:" + cleared);

            currentGameMode = GameMode.WaitForSceneChange;

            Debug.Log("endings_PlayableDirectors[3] Timeline playback completed.");
        }
        else if((director == endings_PlayableDirectors[4]) || (director == endings_PlayableDirectors[0]))
        {
            failedText.SetActive(true);
            failedTextPlayableDirector.Play();
        }
        else if(director == dialogue_2_PlayableDirector)
        {
            newCharaAnounce.SetActive(true);
            newCharaAnPlayableDirector.Play();
            
            Debug.Log("dialogue_2_PlayableDirector Timeline playback completed.");
        }
        else if (director == treature_bePassed_PlayableDirector)
        {
            dialogue_2.SetActive(true);
            dialogue_2_PlayableDirector.Play();
        }
        else if(director == newCharaAnPlayableDirector)
        {
            clearedText.SetActive(true);
            clearedTextPlayableDirector.Play();
        }
    }
    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        if (newCharaAnPlayableDirector != null)
        {
            newCharaAnPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (Act_1_TextPlayableDirector != null)
        {
            Act_1_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (target_A1_TextPlayableDirector != null)
        {
            target_A1_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (failedTextPlayableDirector != null)
        {
            failedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (clearedTextPlayableDirector != null)
        {
            clearedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (targetCompletedTextPlayableDirector != null)
        {
            targetCompletedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning($"targetCompletedTextPlayableDirector is not assigned.");
        }

        if (dialogue_1_PlayableDirector != null)
        {
            dialogue_1_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (dialogue_2_PlayableDirector != null)
        {
            dialogue_2_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (dialogue_3_PlayableDirector != null)
        {
            dialogue_3_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        Debug.Log("endings_PlayableDirectors.Length:" + endings_PlayableDirectors.Length);

        for (int i = 0; i < endings_PlayableDirectors.Length; i++)
        {

            if (endings_PlayableDirectors[i] != null)
            {
                Debug.Log($"*****endings_PlayableDirectors[{i}] is assigned.");
                endings_PlayableDirectors[i].stopped -= OnPlayableDirectorStopped;

            }
            else
            {
                Debug.LogWarning($"endings_PlayableDirectors[{i}] is not assigned.");
            }
        }

        if (treature_beThrown_PlayableDirector != null)
        {
            treature_beThrown_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (treature_bePassed_PlayableDirector != null)
        {
            treature_bePassed_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

    }

}
