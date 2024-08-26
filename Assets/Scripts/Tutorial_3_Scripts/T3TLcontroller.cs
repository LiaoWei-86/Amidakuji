using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class T3TLcontroller : MonoBehaviour
{
    public GameObject start_intro_Message; // ゲームオブジェクト start_intro_Message（開始メッセージ）
    public GameObject second_intro_Message; // ゲームオブジェクト second_intro_Message（開始メッセージ）
    public GameObject lineCount1_Message; // ゲームオブジェクト lineCount1_Message（残りの線何本メッセージ）
    public GameObject lineCount0_Message; // ゲームオブジェクト lineCount0_Message（残りの線何本メッセージ）
    public GameObject storyMessage; // ゲームオブジェクト storyMessage（物語のメッセージ）
    public GameObject endMessage; // ゲームオブジェクト endMessage（エンディングメッセージ）
    public PlayableDirector start_intro_MessagePlayableDirector; // start_intro_MessageのPlayableDirector
    public PlayableDirector second_intro_MessagePlayableDirector; // second_intro_MessageのPlayableDirector
    public PlayableDirector lineCount1_MessagePlayableDirector; // lineCount1_MessageのPlayableDirector
    public PlayableDirector lineCount0_MessagePlayableDirector; // lineCount0_MessageのPlayableDirector
    public PlayableDirector storyMessagePlayableDirector; // storyMessageのPlayableDirector
    public PlayableDirector endMessagePlayableDirector; // endMessageのPlayableDirector

    public bool isHorizontalLineCreated = false;

    private bool isStoryPlaying = false;  // storyMessageが再生中かどうかを示すブール値、初期値はfalse
    private bool isEndPlaying = false;  // endMessageが再生中かどうかを示すブール値、初期値はfalse

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
        if (storyMessage != null )
        {
            storyMessage.SetActive(false);
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

        if (storyMessagePlayableDirector != null)
        {
            storyMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("storyMessagePlayableDirector is not assigned.");
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
        //Debug.Log("isHorizontalLineCreated: " +isHorizontalLineCreated);
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
                    //  このモードでは、プレイヤーがEnterを押すと、

                    if (hasSecondIntroPlayed == true && isStoryPlaying == false)
                    {

                        if(second_intro_Message != null)
                        {
                            second_intro_Message.SetActive(false);
                        }

                        if(storyMessage != null && storyMessagePlayableDirector != null)
                        {
                            storyMessage.SetActive(true);
                            storyMessagePlayableDirector.Play();
                            isStoryPlaying = true;

                        }

                        foreach (KeyValuePair<int, Vector3> kvp in DrawLineT3Script.pointsDictionary) 
                        {
                            Debug.Log($"PointsKey: {kvp.Key}, PointsTransformPositionVector3: {kvp.Value}");
                        }

                        StartMovement(new List<int> { 0,2,3,5 }, new List<int> { 1,3,2,4 });
                    }


                    break;

                case GameMode.WaitForSceneChange:
                    // シーンを切り替える

                    SceneManager.LoadScene("Tutorial_4_Scene");
                    break;
            }
        }
    }


    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == start_intro_MessagePlayableDirector)
        {
            lineCount1_Message.SetActive(true);
            lineCount1_MessagePlayableDirector.Play();
            
            Debug.Log("start_intro_Message Timeline playback completed.");
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
        else if (director == storyMessagePlayableDirector)
        {
            isStoryPlaying = false;  // 再生完了とマークする

            // storyMessageが再生完了したらすぐendMessageを再生する
            isEndPlaying = true;
            endMessage.SetActive(true);
            endMessagePlayableDirector.Play();

            Debug.Log("storyMessage Timeline playback completed.");
        }
        else if (director == endMessagePlayableDirector)
        {
            isEndPlaying = false;  // 再生完了とマークする
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
        StartCoroutine(AfterMovementCoroutine());
    }

    IEnumerator AfterMovementCoroutine()
    {
        yield return new WaitUntil(() => knightMovementCoroutine == null && hunterMovementCoroutine == null);

        // 次の状態に移行
        if (isHorizontalLineCreated)
        {
            currentGameMode = GameMode.WaitForSceneChange;
        }
    }

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

    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        if (start_intro_MessagePlayableDirector != null)
        {
            start_intro_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
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

        if (storyMessagePlayableDirector != null)
        {
            storyMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (endMessagePlayableDirector != null)
        {
            endMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }
}
