using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class T3TLcontroller : MonoBehaviour
{
    public GameObject target_Message; // ゲームオブジェクト target_intro_Message（開始メッセージ）
    public GameObject start_intro_Message; // ゲームオブジェクト start_intro_Message（開始メッセージ）
    public GameObject second_intro_Message; // ゲームオブジェクト second_intro_Message（開始メッセージ）
    public GameObject lineCount1_Message; // ゲームオブジェクト lineCount1_Message（残りの線何本メッセージ）
    public GameObject lineCount0_Message; // ゲームオブジェクト lineCount0_Message（残りの線何本メッセージ）

    public List<GameObject> storyMessages; // ゲームオブジェクト storyMessage（物語のメッセージ）

    public GameObject endMessage; // ゲームオブジェクト endMessage（エンディングメッセージ）
    public PlayableDirector target_MessagePlayableDirector; // target_MessageのPlayableDirector
    public PlayableDirector start_intro_MessagePlayableDirector; // start_intro_MessageのPlayableDirector
    public PlayableDirector second_intro_MessagePlayableDirector; // second_intro_MessageのPlayableDirector
    public PlayableDirector lineCount1_MessagePlayableDirector; // lineCount1_MessageのPlayableDirector
    public PlayableDirector lineCount0_MessagePlayableDirector; // lineCount0_MessageのPlayableDirector

    public PlayableDirector[] storyMessagePlayableDirectors; // storyMessageのPlayableDirector

    public PlayableDirector endMessagePlayableDirector; // endMessageのPlayableDirector

    private int currentStoryIndex = 0; // どのPlayableDirectorが再生してるかを追跡するため

    public bool isHorizontalLineCreated = false;



    private bool isStoryPlaying = false;  // storyMessageが再生中かどうかを示すブール値、初期値はfalse
    private bool isEndPlaying = false;  // endMessageが再生中かどうかを示すブール値、初期値はfalse
    private bool hasEndPlayed = false;

    private bool hasSecondIntroPlayed = false;// second_intro_Messageが再生終了かどうかを示すブール値、初期値はfalse
    private bool haslineCount0_Played = false;// lineCount0_Messageが再生終了かどうかを示すブール値、初期値はfalse

    public bool isKnightMoving = false; // 騎士は動いてるかどうかを示すブール値、初期値はfalse
    public bool isHunterMoving = false; // 猟師は動いてるかどうかを示すブール値、初期値はfalse

    public GameObject knight;
    public GameObject hunter;

    private Coroutine knightMovementCoroutine;
    private Coroutine hunterMovementCoroutine;

    public float speed = 3.0f;

    public DrawLineT3 DrawLineT3Script; // DrawLineT3スクリプトの参照を格納するため

    // ゲームモードを設定し、ゲームが実行されるとこの3つのモードの間で切り替えが行われます
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
        if (DrawLineT3Script != null)
        {
            DrawLineT3Script = FindObjectOfType<DrawLineT3>();
        }

        //  開始時にsecond_intro_Messageを非表示にする
        if (second_intro_Message != null)
        {
            second_intro_Message.SetActive(false);
        }
        //  開始時にstoryMessageを非表示にする
        if (storyMessages != null)
        {
            // storyMessagesの非表示をループで行う
            foreach (var message in storyMessages)
            {
                message.SetActive(false);
            }
        }
        //  開始時にlineCount0_Messageを非表示にする
        if (lineCount1_Message != null)
        {
            lineCount1_Message.SetActive(false);
        }
        //  開始時にlineCount0_Messageを非表示にする
        if (lineCount0_Message != null)
        {
            lineCount0_Message.SetActive(false);
        }
        //  開始時にendMessageのGameObjectを非表示にする
        if (endMessage != null)
        {
            endMessage.SetActive(false);
        }
        //  開始時にtarget_MessageのGameObjectを非表示にする
        if (target_Message != null)
        {
            target_Message.SetActive(false);
        }


        // PlayableDirectorがnullでないことを確認し、再生完了イベントをサブスクライブ
        if (start_intro_MessagePlayableDirector != null)
        {
            start_intro_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("start_intro_MessagePlayableDirector is not assigned.");
        }

        if (second_intro_MessagePlayableDirector != null)
        {
            second_intro_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("start_intro_MessagePlayableDirector is not assigned.");
        }

        if (lineCount1_MessagePlayableDirector != null)
        {
            lineCount1_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("lineCount1_MessagePlayableDirector is not assigned.");
        }

        if (lineCount0_MessagePlayableDirector != null)
        {
            lineCount0_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("lineCount1_MessagePlayableDirector is not assigned.");
        }
        if (target_MessagePlayableDirector != null)
        {
            target_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }

        // PlayableDirectorがnullでないことを確認し、再生完了イベントをサブスクライブ
        for (int i = 0; i < storyMessagePlayableDirectors.Length; i++)
        {
            if (storyMessagePlayableDirectors[i] != null)
            {
                storyMessagePlayableDirectors[i].stopped += OnPlayableDirectorStopped;
            }
            else
            {
                Debug.LogWarning($"storyMessagePlayableDirector[{i}] is not assigned.");
            }
        }

        if (endMessagePlayableDirector != null)
        {
            endMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("endMessagePlayableDirector is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {

   
        if (isHorizontalLineCreated == true && !hasSecondIntroPlayed)
        {
            currentGameMode = GameMode.TextPlaying;
            start_intro_Message.SetActive(false);
            if (!haslineCount0_Played)
            {
                lineCount1_Message.SetActive(false);
                lineCount0_Message.SetActive(true);
                lineCount0_MessagePlayableDirector.Play();
            }

            second_intro_Message.SetActive(true);
            second_intro_MessagePlayableDirector.Play();

        }

        // Enterキーが押されたかどうかをチェック
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (currentGameMode)
            {
                case GameMode.TextPlaying:
                    
                    break;

                case GameMode.PlayerPlaying:
                    //  このモードでは、プレイヤーがEnterを押すと、キャラクターの移動＋プロットアイコンの生成＋ストーリーメッセージの生成を一つずつ表示される

                    if (hasSecondIntroPlayed == true && isStoryPlaying == false)
                    {

                        if (second_intro_Message != null)
                        {
                            second_intro_Message.SetActive(false);
                        }

                        PlayNextStory();
                        

                        //foreach (KeyValuePair<int, Vector3> kvp in DrawLineT3Script.pointsDictionary)
                        //{
                        //    Debug.Log($"PointsKey: {kvp.Key}, PointsTransformPositionVector3: {kvp.Value}");
                        //}

                        
                        switch (currentStoryIndex)
                        {
                            case 0:
                                StartMovement(new List<int> { 0, 2, 3 }, new List<int> { 1, 3, 2 });
                                break;
                            case 1:
                                StartMovement(new List<int> { 3, 3 }, new List<int> { 2, 4 });
                                break;
                            case 2:
                                StartMovement(new List<int> { 3, 5 }, new List<int> { 4, 4 });
                                break;
                            case 3:
                                StartMovement(new List<int> { 5, 5 }, new List<int> { 4, 4 });
                                break;
                            case 4:
                                Debug.Log("currentStoryIndex:4 but do not move");
                                break;
                            case 5:
                                Debug.Log("currentStoryIndex:5 but do not move");
                                break;
                        }
                    }
                    

                    break;

                case GameMode.WaitForSceneChange:
                    // シーンを切り替える
                    if (!isEndPlaying && hasEndPlayed == true )
                    {
                        SceneManager.LoadScene("Tutorial_4_Scene");
                    }
                    
                    break;
            }
        }
    }

    private void PlayNextStory()
    {
        Debug.Log("PlayNextStory called, currentStoryIndex: " + currentStoryIndex);

        if (currentStoryIndex < storyMessagePlayableDirectors.Length)
        {
            Debug.Log("Playing story message: " + currentStoryIndex);

            storyMessages[currentStoryIndex].SetActive(true);
            storyMessagePlayableDirectors[currentStoryIndex].Play();
            GeneratePlotIcon(currentStoryIndex);
            isStoryPlaying = true; 
        }
        else
        {
            Debug.Log("All story messages played.");
            // Add debug here to check if it's reaching the end too early
            Debug.Log("Switching to WaitForSceneChange mode");

            Debug.Log("All story messages played.");
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        Debug.Log("PlayableDirector stopped, currentStoryIndex: " + currentStoryIndex);

        if (director == start_intro_MessagePlayableDirector)
        {
            target_MessagePlayableDirector.Play();
            target_Message.SetActive(true);


            Debug.Log("start_intro_Message Timeline playback completed.");
        }
        else if (director == target_MessagePlayableDirector)
        {

            lineCount1_Message.SetActive(true);
            lineCount1_MessagePlayableDirector.Play();
        }
        else if (director == lineCount1_MessagePlayableDirector)
        {
            
            currentGameMode = GameMode.PlayerPlaying;

            Debug.Log("lineCount1_Message Timeline playback completed.");
        }
        
        else if (director == second_intro_MessagePlayableDirector)
        {
            hasSecondIntroPlayed = true;
            Debug.Log("hasSecondIntroPlayed" + hasSecondIntroPlayed);

            currentGameMode = GameMode.PlayerPlaying;

            Debug.Log("second_intro_Message Timeline playback completed.");
        }
        else if (director == lineCount0_MessagePlayableDirector)
        {
            haslineCount0_Played = true;
            Debug.Log("haslineCount0_Played" + haslineCount0_Played);

            Debug.Log("lineCount0_Message Timeline playback completed.");
        }
        // もしstoryMessagePlayableDirectorの中でどちらがstopしたら
        else if (System.Array.IndexOf(storyMessagePlayableDirectors, director) != -1)
        {
            Debug.Log("A story message PlayableDirector stopped, increasing currentStoryIndex.");

            currentStoryIndex++; // 再生するPlayableDirectorナンバーを更新する
            Debug.Log("currentStoryIndex"+currentStoryIndex);
            isStoryPlaying = false; // ストーリーメッセージは再生完了とマークする

            Debug.Log("New currentStoryIndex: " + currentStoryIndex);

            // 全部のストーリーメッセージは再生されたかを確認する
            if (currentStoryIndex >= storyMessagePlayableDirectors.Length)
            {
                
                Debug.Log("All story messages played. Showing end message.");

                isEndPlaying = true;
                endMessage.SetActive(true);
                endMessagePlayableDirector.Play();
            }
        }
        else if (director == endMessagePlayableDirector)
        {
            isEndPlaying = false;  // 再生完了とマークする
            hasEndPlayed = true;
            currentGameMode = GameMode.WaitForSceneChange;  //  シーン切り替え待ちモードに変更する
            Debug.Log("endMessage Timeline playback completed.");
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

        // 移動完了後に入力待ち状態に戻る
        //StartCoroutine(AfterMovementCoroutine());
    }
    /* 元々の意図は、キャラクターが終点に到達した後、waitForSceneChange状態に入り、プレイヤーがEnterキーを押すとシーンが切り替わるというものでした。
     * しかし、現在はstoryMessageを1本ずつ再生し、最後のメッセージが再生された時点でwaitForSceneChange状態に入るように変更したため、そのロジックが使えなくなり、
     * コードをコメントしました。*/

    //IEnumerator AfterMovementCoroutine()
    //{
    //    yield return new WaitUntil(() => knightMovementCoroutine == null && hunterMovementCoroutine == null);

    //    // 次の状態に移行
    //    if (isHorizontalLineCreated)
    //    {
    //        currentGameMode = GameMode.WaitForSceneChange;
    //    }
    //}

    IEnumerator MoveKnightCoroutine(List<int> path)
    {
        foreach (int point in path)
        {
            Vector3 targetPosition = DrawLineT3Script.pointsDictionary[point];
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
            Vector3 targetPosition = DrawLineT3Script.pointsDictionary[point];
            while (hunter.transform.position != targetPosition)
            {
                hunter.transform.position = Vector3.MoveTowards(hunter.transform.position, targetPosition, Time.deltaTime * speed);
                yield return null;
            }
        }
    }

    void GeneratePlotIcon(int index)
    {
        if (index < 4)
        {
            // 特定の位置にplotIconを置く
            GameObject plotIcon = Instantiate(DrawLineT3Script.plotIconPrefabs[index], DrawLineT3Script.plotIconPositions[index].position, Quaternion.identity);
            plotIcon.name = "plotIcon" + index; // plotIconの名前をつける
            Debug.Log($"Generated {plotIcon.name} at position {plotIcon.transform.position}");
        }
        
    }

    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        if (start_intro_MessagePlayableDirector != null)
        {
            start_intro_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
  
        if (target_MessagePlayableDirector != null)
        {
            target_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (second_intro_MessagePlayableDirector != null)
        {
            second_intro_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (lineCount1_MessagePlayableDirector != null)
        {
            lineCount1_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (lineCount0_MessagePlayableDirector != null)
        {
            lineCount0_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        for (int i = 0; i < storyMessagePlayableDirectors.Length; i++)
        {
            if (storyMessagePlayableDirectors[i] != null)
            {
                storyMessagePlayableDirectors[i].stopped -= OnPlayableDirectorStopped;
            }
        }

        if (endMessagePlayableDirector != null)
        {
            endMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }
}