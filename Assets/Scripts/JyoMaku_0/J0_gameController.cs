using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;

public class J0_gameController : MonoBehaviour
{

    public GameObject Jyomaku_J0_Text; // ゲームオブジェクト Jyomaku_J0_Text
    public PlayableDirector Jyomaku_J0_TextPlayableDirector; // Jyomaku_J0_TextのPlayableDirector

    public GameObject target_J0_Text; // ゲームオブジェクト target_J0_Text
    public PlayableDirector target_J0_TextPlayableDirector; // target_J0_TextのPlayableDirector

    public GameObject targetCompletedText; // ゲームオブジェクト targetCompletedText
    public PlayableDirector targetCompletedTextPlayableDirector; // targetCompletedTextのPlayableDirector

    public GameObject failedText; // ゲームオブジェクト failedText
    public PlayableDirector failedTextPlayableDirector; // failedTextのPlayableDirector
    public bool failed = false; // このステージをクリアできなかったをfalseとマークする

    public GameObject kingWords; // ゲームオブジェクト kingWords
    public PlayableDirector kingWordsPlayableDirector; // kingWordsのPlayableDirector

    public GameObject failed_kingWords; // ゲームオブジェクト failed_kingWords
    public PlayableDirector failed_kingWordsPlayableDirector; // failed_kingWordsのPlayableDirector

    public bool kingWords_played = false; // 国王のセリフは再生したかをfalseとマークする

    public GameObject knightWords; // ゲームオブジェクト knightWords
    public PlayableDirector knightWordsPlayableDirector; // knightWordsのPlayableDirector

    public GameObject failed_knightWords; // ゲームオブジェクト knightWords
    public PlayableDirector failed_knightWordsPlayableDirector; // knightWordsのPlayableDirector

    public bool knightWords_played = false; // 騎士のセリフは再生したかをfalseとマークする

    public GameObject fukidashi_J0;  // ゲームオブジェクト 吹き出し＿J0
    public PlayableDirector dialogue_J0_PlayableDirector; //  会話セリフ＿J0 のPlayableDirector
    public GameObject dog; // 犬

    public Transform startpoint_character1; // 左から1番目のスタート点の位置
    public Transform startpoint_character2; // 左から2番目のスタート点の位置
    public GameObject[] tenGameObjects; // それぞれの点
    public Transform[] tenPositions; // それぞれの点の位置
    public Transform endtpoint_character1; //左から1番目の終点の位置
    public Transform endpoint_character2; // 左から2番目の終点の位置
    public Dictionary<int, Vector3> pointsDictionary;// 点とその位置情報を格納する辞書

    public bool isHorizontalLineCreated = false; //　横線は描かれたか？のブール値

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

    public GameObject menu; // menu_controller
    public Vector3 menuTargetPosition = new Vector3(5.5f, -4.2f, 0); // たどり着いて欲しい座標
    public bool menuIsOnItsPos = false; // メニューは目標座標に着いたかをfalseとマークする
    public float moveSpeed = 1.5f; // メニューの移動スピード
    public TMP_Text cannotENTER; // メニューの「Enter：進む」

    public GameObject newCharaAnounce; // 新しいキャラクターが登場したという通知
    public PlayableDirector newCharaAnPlayableDirector; // 新しいキャラクターが登場したという通知 PlayableDirector

    public AudioClip leftClickClip;
    public AudioClip rightClickClip;

    public AudioSource audioSourceJ0;

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
        

        CreateHoverAreaCharacter(character1, 1); // 騎士：１
        CreateHoverAreaCharacter(character2, 3); // 国王：３

        //  開始時にを非表示にする
        if (target_J0_Text != null)
        {
            target_J0_Text.SetActive(false);
        }
        if (targetCompletedText != null)
        {
            targetCompletedText.SetActive(false);
        }
        if (fukidashi_J0 != null)
        {
            fukidashi_J0.SetActive(false);
        }
        if (dog != null)
        {
            dog.SetActive(false);
        }
        if (failedText != null)
        {
            failedText.SetActive(false);
        }
        if (kingWords != null)
        {
            kingWords.SetActive(false);
        }
        if (knightWords != null)
        {
            knightWords.SetActive(false);
        }
        if (failed_kingWords != null)
        {
            failed_kingWords.SetActive(false);
        }
        if (failed_knightWords != null)
        {
            failed_knightWords.SetActive(false);
        }
        if (menu != null)
        {
            menu.SetActive(false);
        }
        if (newCharaAnounce != null)
        {
            newCharaAnounce.SetActive(false);
        }

        // PlayableDirectorがnullでないことを確認し、再生完了イベントをサブスクライブ

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
        if (dialogue_J0_PlayableDirector != null)
        {
            dialogue_J0_PlayableDirector.stopped += OnPlayableDirectorStopped;
        }

        if (failedTextPlayableDirector != null)
        {
            failedTextPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (kingWordsPlayableDirector != null)
        {
            kingWordsPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (knightWordsPlayableDirector != null)
        {
            knightWordsPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (failed_kingWordsPlayableDirector != null)
        {
            failed_kingWordsPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (failed_knightWordsPlayableDirector != null)
        {
            failed_knightWordsPlayableDirector.stopped += OnPlayableDirectorStopped;
        }

        // Initialize the dictionary and add the points
        pointsDictionary = new Dictionary<int, Vector3>();

        // Add knight and hunter start and end points
        pointsDictionary.Add(0, startpoint_character1.position); // 左から1番目のスタート点
        pointsDictionary.Add(1, startpoint_character2.position); // 左から2番目のスタート点
        pointsDictionary.Add(4, endtpoint_character1.position);  // 左から1番目の終点
        pointsDictionary.Add(5, endpoint_character2.position);   // 左から2番目の終点

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

        CreateHoverAreaJ0(tenGameObjects[0], tenGameObjects[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (currentGameMode)
        {
            case GameMode.TextPlaying:

                break;

            case GameMode.PlayerPlaying:

                
                if (Input.GetKeyDown(KeyCode.Return))
                {

                    //  このモードでは、プレイヤーがEnterを押すと、キャラクターの移動＋プロットアイコンの生成＋ストーリーメッセージの生成を一つずつ表示される


                    /*
                   　　　　        「騎士」0　　　　　「国王」1
                                       ||                ||
                             circle1   ○2     circle2   ○3
                                       ||                ||
               　　　　　　       「結末」4　　　　　「結末」5
                    */

                    if (isHorizontalLineCreated == true)
                    {
                        switch (currentMovementIndex)
                        {
                            case 0:
                                StartMovement(new List<int> { 0, 2, 3 }, new List<int> { 1, 3, 2 });
                                break;
                            case 1:
                                StartMovement(new List<int> { 3, 3 }, new List<int> { 2, 2 });
                                fukidashi_J0.SetActive(true);
                                dog.SetActive(true);

                                break;
                            case 2:
                                StartMovement(new List<int> { 3, 3 }, new List<int> { 2, 4 });
                                break;

                            case 3:
                                StartMovement(new List<int> { 3, 3 }, new List<int> { 4, 4 });
                                kingWords.SetActive(true);

                                break;

                            case 4:
                                StartMovement(new List<int> { 3, 5 }, new List<int> { 4, 4 });
                                break;

                            case 5:
                                StartMovement(new List<int> { 5, 5 }, new List<int> { 4, 4 });
                                knightWords.SetActive(true);

                                hasMovementFinshed = true;

                                break;
                        }

                    }
                    else if (isHorizontalLineCreated == false)
                    {
                        switch (currentMovementIndex)
                        {
                            case 0:
                                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });
                                break;
                            case 1:
                                StartMovement(new List<int> { 2, 4 }, new List<int> { 3, 3 });
                                break;

                            case 2:
                                StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 3 });
                                failed_knightWords.SetActive(true);

                                break;

                            case 3:
                                StartMovement(new List<int> { 4, 4 }, new List<int> { 3, 5 });
                                break;

                            case 4:
                                StartMovement(new List<int> { 4, 4 }, new List<int> { 5, 5 });
                                failed_kingWords.SetActive(true);

                                hasMovementFinshed = true;
                                break;
                        }

                    }
                }


                    break;

            case GameMode.WaitForSceneChange:

                ChangeTextColor(cannotENTER);

                menu.SetActive(true);
                StartCoroutine(MoveMenuToTarget(menuTargetPosition, moveSpeed));

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (menuIsOnItsPos == true)
                    {
                        if (!failed)
                        {
                            if (kingWords_played && knightWords_played)
                            {
                                Debug.Log("LoadScene:JyoMaku_0.5");
                                SceneManager.LoadScene("JyoMaku_0.5");

                            }
                        }
                    }
                }else if (Input.GetKeyDown(KeyCode.R))
                {
                    Debug.Log("LoadScene:JyoMaku_0");
                    SceneManager.LoadScene("JyoMaku_0");
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

                    break;
        }
  
        
    }

    public void ChangeTextColor(TMP_Text tmp)
    {
        if (failed)
        {
            tmp.color = Color.gray; // もし`failed`は`true`であれば，テキストの色を灰色に変える
        }
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
        boxCollider.size = new Vector3(Vector3.Distance(pointA.transform.position, pointB.transform.position), hoverAreaWidth, 0.1f);

        // BoxCollider をトリガーとして設定する
        boxCollider.isTrigger = true;

        Debug.Log($"BoxCollider created with size: {boxCollider.size}");   // BoxCollider のサイズをデバッグログで表示する

        hoverArea.transform.position = midPoint;   // ホバーエリアの位置を中間点に設定する
        hoverArea.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);   // ホバーエリアの回転を、点Aから点Bへの方向に基づいて設定する

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

        // J1_charaHoverAreaScript スクリプトをキャラクターに追加する
        J0_charaHoverArea J0_charaHoverAreaScript = character.AddComponent<J0_charaHoverArea>();

        // J1_charaHoverAreaScript スクリプトの情報番号を設定する
        J0_charaHoverAreaScript.charaInfoNum = charaInfoNum;

        // J1_charaHoverAreaScriptスクリプトを初期化する
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
        if (director == Jyomaku_J0_TextPlayableDirector) 
        {
            target_J0_Text.SetActive(true);
            target_J0_TextPlayableDirector.Play();

            Debug.Log(" Jyomaku_J0_Text Timeline playback completed.");
        }
        else if (director == target_J0_TextPlayableDirector)
        {

            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log(" target_J0_Text Timeline playback completed.");
        }
        else if(director == dialogue_J0_PlayableDirector)
        {
            newCharaAnounce.SetActive(true);
            newCharaAnPlayableDirector.Play();
        }
        else if (director == failedTextPlayableDirector)
        {
            failed = true;
            currentGameMode = GameMode.WaitForSceneChange;
        }
        else if (director == kingWordsPlayableDirector)
        {
            kingWords_played = true;

            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == knightWordsPlayableDirector)
        {
            knightWords_played = true;
            targetCompletedText.SetActive(true);

            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == failed_kingWordsPlayableDirector)
        {
            kingWords_played = true;
            failedText.SetActive(true);
            currentGameMode = GameMode.PlayerPlaying;
            
        }
        else if (director == failed_knightWordsPlayableDirector)
        {
            knightWords_played = true;

            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == targetCompletedTextPlayableDirector)
        {
            

            currentGameMode = GameMode.WaitForSceneChange;
        }
    }

    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ


        if (Jyomaku_J0_TextPlayableDirector != null)
        {
            Jyomaku_J0_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (target_J0_TextPlayableDirector != null)
        {
            target_J0_TextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (targetCompletedTextPlayableDirector != null)
        {
            targetCompletedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (failedTextPlayableDirector != null)
        {
            failedTextPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (kingWordsPlayableDirector != null)
        {
            kingWordsPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (knightWordsPlayableDirector != null)
        {
            knightWordsPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (dialogue_J0_PlayableDirector != null)
        {
            dialogue_J0_PlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }
}
