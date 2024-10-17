using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class T5TLcontroller : MonoBehaviour
{
    public GameObject mouse_intro_Message; // ゲームオブジェクト mouse_intro_Message（開始メッセージ1）
    public PlayableDirector mouse_intro_MessagePlayableDirector; // mouse_intro_MessageのPlayableDirector

    public GameObject chara_intro_Message; // ゲームオブジェクト chara_intro_Message（開始メッセージ2）
    public PlayableDirector chara_intro_MessagePlayableDirector; // chara_intro_MessageのPlayableDirector
    private bool chara_intro_MessagePlayed = false; //　chara_intro_Messageはすでに再生したかどうか？のブール値

    public GameObject simulating_Message; // ゲームオブジェクト simulating_Message
    public PlayableDirector simulating_MessagePlayableDirector; // simulating_MessageのPlayableDirector

    public GameObject result_Message; // ゲームオブジェクト result_Message
    public PlayableDirector result_MessagePlayableDirector; // result_MessageのPlayableDirector

    public GameObject result_0_Message; // ゲームオブジェクト result_Message
    public PlayableDirector result_0_MessagePlayableDirector; // result_MessageのPlayableDirector

    public GameObject line3Story_Message; // ゲームオブジェクト line3Story_Message
    public PlayableDirector line3Story_MessagePlayableDirector; // line3Story_MessageのPlayableDirector

    public GameObject line4Story_Message; // ゲームオブジェクト line4Story_Message
    public PlayableDirector line4Story_MessagePlayableDirector; // line4Story_MessageのPlayableDirector

    public GameObject newChara_Message; // ゲームオブジェクト newChara_Message
    public PlayableDirector newChara_MessagePlayableDirector; // newChara_MessageのPlayableDirector

    public GameObject[] plotIconPrefabs; // プロットアイコンのプレハブ
    public Transform[] plotIconPositions; // plotIconの位置
    private int currentMovementIndex = 0; // プレイヤーがEnterを押す際にプロットアイコンを生成するために計算用のIndex

    public GameObject endMessage; // ゲームオブジェクト endMessage（開始メッセージ2）
    public PlayableDirector endMessagePlayableDirector; // endMessageのPlayableDirector


    public bool isHorizontal_1_LineCreated = false; //　上から3本目の横線は描かれたか？のブール値
    public bool isHorizontal_2_LineCreated = false; //　上から4本目の横線は描かれたか？のブール値

    public bool hasMovementFinshed = false; //　キャラクターの移動は完成されたか？のブール値
    public int result_Index = 0;

    public bool charaKnightInfoChecked = false; //　騎士のキャラクター情報は確認されたか？のブール値
    public bool charaHunterInfoChecked = false; //　猟師のキャラクター情報は確認されたか？のブール値

    //public bool isKnightMoving = false; // 騎士は動いてるかどうかを示すブール値、初期値はfalse
    //public bool isHunterMoving = false; // 猟師は動いてるかどうかを示すブール値、初期値はfalse

    public GameObject knight;
    public GameObject hunter;

    private Coroutine knightMovementCoroutine;
    private Coroutine hunterMovementCoroutine;

    public float speed = 3.0f;


    public characterInfoHoverT5 characterInfoHoverT5Script;// characterInfoHoverT5スクリプトの参照
    public DrawLineT5 DrawLineT5Script;    // DrawLineT5スクリプトの参照

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
        if (characterInfoHoverT5Script == null)
        {
            characterInfoHoverT5Script = FindObjectOfType<characterInfoHoverT5>();
        }

        //  開始時にchara_intro_Messageを非表示にする
        if (chara_intro_Message != null)
        {
            chara_intro_Message.SetActive(false);
        }

        //  開始時にsimulating_Messageを非表示にする
        if (simulating_Message != null)
        {
            simulating_Message.SetActive(false);
        }

        //  開始時にresult_Messageを非表示にする
        if (result_Message != null)
        {
            result_Message.SetActive(false);
        }

        //  開始時にresult_0_Messageを非表示にする
        if (result_0_Message != null)
        {
            result_0_Message.SetActive(false);
        }

        if (line3Story_Message != null)
        {
            line3Story_Message.SetActive(false);
        }

        if (line4Story_Message != null)
        {
            line4Story_Message.SetActive(false);
        }

        if (newChara_Message != null)
        {
            newChara_Message.SetActive(false);
        }

        if (endMessage != null)
        {
            endMessage.SetActive(false);
        }


        // PlayableDirectorがnullでないことを確認し、再生完了イベントをサブスクライブ
        if (mouse_intro_MessagePlayableDirector != null)
        {
            mouse_intro_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("mouse_intro_MessagePlayableDirector is not assigned.");
        }

        if (chara_intro_MessagePlayableDirector != null)
        {
            chara_intro_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("chara_intro_MessagePlayableDirector is not assigned.");
        }

        if (result_MessagePlayableDirector != null)
        {
            result_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("result_MessagePlayableDirector is not assigned.");
        }

        if (result_0_MessagePlayableDirector != null)
        {
            result_0_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("result_0_MessagePlayableDirector is not assigned.");
        }

        if (line3Story_MessagePlayableDirector != null)
        {
            line3Story_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("line3Story_MessagePlayableDirector is not assigned.");
        }

        if (line4Story_MessagePlayableDirector != null)
        {
            line4Story_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("line4Story_MessagePlayableDirector is not assigned.");
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
        if ((charaKnightInfoChecked == true || charaHunterInfoChecked == true) && !chara_intro_MessagePlayed)
        {
            currentGameMode = GameMode.TextPlaying;
            mouse_intro_Message.SetActive(false);
            chara_intro_Message.SetActive(true);
            chara_intro_MessagePlayableDirector.Play();

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
                    if (hasMovementFinshed)
                    {
                        simulating_Message.SetActive(false);

                        if(result_Index == 0)
                        {
                            if (result_0_Message != null)
                            {
                                result_0_Message.SetActive(true);
                                result_0_MessagePlayableDirector.Play();
                            }
                        }
                        else if(result_Index == 1)
                        {
                            if (result_Message != null)
                            {
                                result_Message.SetActive(true);
                                result_MessagePlayableDirector.Play();
                            }
                        }

                    }
                    else if (!hasMovementFinshed)
                    {
                        if (isHorizontal_1_LineCreated == true && isHorizontal_2_LineCreated == false)
                        {
                            if (chara_intro_Message != null)
                            {
                                chara_intro_Message.SetActive(false);

                            }

                            if (simulating_Message != null)
                            {
                                simulating_Message.SetActive(true);
                                simulating_MessagePlayableDirector.Play();
                            }

                            PlayNextStep_();

                            switch (currentMovementIndex)
                            {
                                case 0:
                                    StartMovement(new List<int> { 0, 2, 3 }, new List<int> { 1, 3, 2 });
                                    break;
                                case 1:
                                    StartMovement(new List<int> { 3, 5, 4 }, new List<int> { 2, 4, 5 });
                                    break;
                                case 2:
                                    StartMovement(new List<int> { 4, 6, 7 }, new List<int> { 5, 7, 6 });
                                    break;
                                case 3:
                                    StartMovement(new List<int> { 7, 9, 11 }, new List<int> { 6, 8, 10 });
                                    hasMovementFinshed = true;
                                    break;
                            }

                        }
                        else if (isHorizontal_1_LineCreated == false && isHorizontal_2_LineCreated == true)
                        {
                            if (chara_intro_Message != null)
                            {
                                chara_intro_Message.SetActive(false);

                            }

                            if (simulating_Message != null)
                            {
                                simulating_Message.SetActive(true);
                                simulating_MessagePlayableDirector.Play();
                            }

                            PlayNextStep_();

                            switch (currentMovementIndex)
                            {
                                case 0:
                                    StartMovement(new List<int> { 0, 2, 3 }, new List<int> { 1, 3, 2 });
                                    break;
                                case 1:
                                    StartMovement(new List<int> { 3, 5, 4 }, new List<int> { 2, 4, 5 });
                                    break;
                                case 2:
                                    StartMovement(new List<int> { 4, 6, 8, 9 }, new List<int> { 5, 7, 9, 8 });
                                    break;
                                case 3:
                                    StartMovement(new List<int> { 9, 11 }, new List<int> { 8, 10 });
                                    hasMovementFinshed = true;
                                    break;
                            }
                        }
                        else if (isHorizontal_1_LineCreated == true && isHorizontal_2_LineCreated == true)
                        {

                            if (chara_intro_Message != null)
                            {
                                chara_intro_Message.SetActive(false);

                            }

                            if (simulating_Message != null)
                            {
                                simulating_Message.SetActive(true);
                                simulating_MessagePlayableDirector.Play();
                            }


                            foreach (KeyValuePair<int, Vector3> kvp in DrawLineT5Script.pointsDictionary)
                            {
                                Debug.Log($"PointsKey: {kvp.Key}, PointsTransformPositionVector3: {kvp.Value}");
                            }

                            PlayNextStep();

                            /*

                       　　　　    「騎士」0　　　　　「猟師」1
                                       ||                ||
                           points[0]   ○2    points[4]  ○3
                                       ||                ||
                           points[1]   ○4    points[5]  ○5
                                       ||                ||
                           points[2]   ○6    points[6]  ○7
                                       ||                ||
                           points[3]   ○8    points[7]  ○9
                                       ||                ||
                   　　　　　　    「結末」10　　　　　「結末」11
                            */

                            switch (currentMovementIndex)
                            {
                                case 0:
                                    StartMovement(new List<int> { 0, 2, 3 }, new List<int> { 1, 3, 2 });
                                    break;
                                case 1:
                                    StartMovement(new List<int> { 3, 5, 4 }, new List<int> { 2, 4, 5 });
                                    break;
                                case 2:
                                    StartMovement(new List<int> { 4, 6, 7 }, new List<int> { 5, 7, 6 });
                                    break;
                                case 3:
                                    StartMovement(new List<int> { 7, 9, 8 }, new List<int> { 6, 8, 9 });
                                    break;
                                case 4:
                                    StartMovement(new List<int> { 8, 10 }, new List<int> { 9, 11 });
                                    hasMovementFinshed = true;
                                    result_Index = result_Index + 1;
                                    break;

                            }
                        }
                    
                    }


                    break;

                case GameMode.WaitForSceneChange:
                    // シーンを切り替える

                    SceneManager.LoadScene("Tutorial_6_Scene");
                    break;
            }
        }
    }

    private void PlayNextStep()
    {
        Debug.Log("PlayNextStep called, currentMovementIndex: " + currentMovementIndex);

        if (currentMovementIndex < 4)
        {
            Debug.Log("Generating PlotIcon: " + currentMovementIndex);

            GeneratePlotIcon(currentMovementIndex);

          
        }
        else
        {
            Debug.Log("All story messages played.");
        }
    }

    void GeneratePlotIcon(int index)
    {
        if (index < 4)
        {
            // 特定の位置にplotIconを置く
            GameObject plotIcon = Instantiate(plotIconPrefabs[index], plotIconPositions[index].position, Quaternion.identity);
            plotIcon.name = "plotIcon" + index; // plotIconの名前をつける
            Debug.Log($"Generated {plotIcon.name} at position {plotIcon.transform.position}");
        }
        else
        {
            Debug.Log($"All plotIcons have been generated.");
        }

    }

    private void PlayNextStep_()
    {
        Debug.Log("PlayNextStep called, currentMovementIndex: " + currentMovementIndex);

        if (currentMovementIndex < 3)
        {
            Debug.Log("Generating PlotIcon: " + currentMovementIndex);

            GeneratePlotIcon_(currentMovementIndex);
         
        }
        else
        {
            Debug.Log("All story messages played.");
        }
    }

    void GeneratePlotIcon_(int index)
    {
        if (index < 3)
        {
            // 特定の位置にplotIconを置く
            GameObject plotIcon = Instantiate(plotIconPrefabs[index], plotIconPositions[index].position, Quaternion.identity);
            plotIcon.name = "plotIcon" + index; // plotIconの名前をつける
            Debug.Log($"Generated {plotIcon.name} at position {plotIcon.transform.position}");

        }
        else
        {
            Debug.Log($"All plotIcons have been generated.");
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
            Vector3 targetPosition = DrawLineT5Script.pointsDictionary[point];
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
            Vector3 targetPosition = DrawLineT5Script.pointsDictionary[point];
            while (hunter.transform.position != targetPosition)
            {
                hunter.transform.position = Vector3.MoveTowards(hunter.transform.position, targetPosition, Time.deltaTime * speed);
                yield return null;
            }
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == mouse_intro_MessagePlayableDirector)
        {
            currentGameMode = GameMode.PlayerPlaying;

            Debug.Log(" mouse_intro_Message Timeline playback completed.");
        }
        else if (director == chara_intro_MessagePlayableDirector)
        {
            currentGameMode = GameMode.PlayerPlaying;
            chara_intro_MessagePlayed = true; // 再生したとマークする

            Debug.Log("chara_intro_Message Timeline playback completed.");
        }
        else if (director == result_MessagePlayableDirector)
        {
            line4Story_Message.SetActive(true);
            line4Story_MessagePlayableDirector.Play();

            Debug.Log("result_Message Timeline playback completed.");
        }
        else if (director == result_0_MessagePlayableDirector)
        {
            line3Story_Message.SetActive(true);
            line3Story_MessagePlayableDirector.Play();

            Debug.Log("result_0_Message Timeline playback completed.");
        }
        else if (director == line3Story_MessagePlayableDirector)
        {
            currentGameMode = GameMode.PlayerPlaying;

            Debug.Log("line3Story_Message Timeline playback completed.");
        }
        else if (director == line4Story_MessagePlayableDirector)
        {
            newChara_Message.SetActive(true);
            newChara_MessagePlayableDirector.Play();

            Debug.Log("line4Story_Message Timeline playback completed.");
        }
        else if (director == newChara_MessagePlayableDirector)
        {
            currentGameMode = GameMode.PlayerPlaying;

            Debug.Log("newChara_Message Timeline playback completed.");
        }
    }
    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        if (mouse_intro_MessagePlayableDirector != null)
        {
            mouse_intro_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (chara_intro_MessagePlayableDirector != null)
        {
            chara_intro_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }



        //for (int i = 0; i < storyMessagePlayableDirectors.Length; i++)
        //{
        //    if (storyMessagePlayableDirectors[i] != null)
        //    {
        //        storyMessagePlayableDirectors[i].stopped -= OnPlayableDirectorStopped;
        //    }
        //}

        if (result_MessagePlayableDirector != null)
        {
            result_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (line3Story_MessagePlayableDirector != null)
        {
            line3Story_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (line4Story_MessagePlayableDirector != null)
        {
            line4Story_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (newChara_MessagePlayableDirector != null)
        {
            newChara_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (endMessagePlayableDirector != null)
        {
            endMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }

}
