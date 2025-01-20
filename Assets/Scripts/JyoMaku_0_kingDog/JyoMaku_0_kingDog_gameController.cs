using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;
using TMPro;

public class JyoMaku_0_kingDog_gameController : MonoBehaviour
{
    private bool hasFinishedOnce = false;

    public GameObject line1;
    public GameObject line2;
    public Collider line_1_collider;
    public Collider line_2_collider;
    public bool is_horizontal_line_1_connected = false;
    public bool is_horizontal_line_2_connected = false;
    public bool can_line_1_changed = true;
    public bool can_line_2_changed = true;

    public GameObject character1;
    public GameObject character2;
    public Transform character1_initial_pos;
    public Transform character2_initial_pos;
    public Transform startpoint_character1; // 左から1番目のスタート点の位置
    public Transform startpoint_character2; // 左から2番目のスタート点の位置
    public GameObject[] tenGameObjects; // それぞれの点
    public Transform[] tenPositions; // それぞれの点の位置
    public Transform endtpoint_character1; //左から1番目の終点の位置
    public Transform endpoint_character2; // 左から2番目の終点の位置
    public Dictionary<int, Vector3> pointsDictionary;// 点とその位置情報を格納する辞書

    private Coroutine character1MovementCoroutine; // 左から1番目のキャラクターの移動
    private Coroutine character2MovementCoroutine; // 左から2番目のキャラクターの移動

    public bool hasMovementFinshed = false; //　キャラクターの移動は完成されたか？のブール値
    public int currentMovementIndex = 0; // プレイヤーがEnterを押す際にプロットアイコンを生成するために計算用のIndex

    public float speed = 3.0f;

    public TMP_FontAsset dotFont;  // ドットフォント
    public GameObject charaInfoPrefab;

    public GameObject questionMark1;
    public GameObject questionMark2;
    public GameObject questionMark3;
    public GameObject questionMark4;
    public GameObject questionMark5;

    public GameObject game_start;
    public PlayableDirector game_start_pd;
    public bool game_started = false;

    public GameObject nextIcon;

    public GameObject target_1;
    public PlayableDirector target_1_pd;

    public GameObject target_1_completed;
    public PlayableDirector target_1_completed_pd;

    public GameObject target_2;
    public PlayableDirector target_2_pd;

    public GameObject target_2_completed;
    public PlayableDirector target_2_completed_pd;

    public GameObject jyomaku_name;
    public PlayableDirector jyomaku_name_pd;

    public GameObject dog_with_chinaGirl;
    public GameObject dog_with_hunter;
    public GameObject dog_with_hunter_road;
    public GameObject dog_road;
    public PlayableDirector dog_run_pd;
    public PlayableDirector dog_road_pd;

    public GameObject a_xue_1;
    public PlayableDirector a_xue_1_pd;
    private bool a_xue_1_started = false;
    private bool a_xue_1_played = false;

    public GameObject a_xue_2;
    public PlayableDirector a_xue_2_pd;
    private bool a_xue_2_started = false;
    private bool a_xue_2_played = false;

    public GameObject h1_xue_dog;
    public PlayableDirector h1_xue_dog_pd;
    private bool h1_xue_dog_started = false;
    private bool h1_xue_dog_played = false;

    public GameObject b_xue_1_t1;
    public PlayableDirector b_xue_1_t1_pd;
    private bool b_xue_1_t1_started = false;
    private bool b_xue_1_t1_played = false;

    public GameObject b_xue_1_t2;
    public PlayableDirector b_xue_1_t2_pd;
    private bool b_xue_1_t2_started = false;
    private bool b_xue_1_t2_played = false;

    public GameObject b_xue_2a;
    public PlayableDirector b_xue_2a_pd;
    private bool b_xue_2a_started = false;
    private bool b_xue_2a_played = false;

    public GameObject b_xue_2b_t1;
    public PlayableDirector b_xue_2b_t1_pd;
    private bool b_xue_2b_t1_started = false;
    private bool b_xue_2b_t1_played = false;

    public GameObject b_xue_2b_t2;
    public PlayableDirector b_xue_2b_t2_pd;
    private bool b_xue_2b_t2_started = false;
    private bool b_xue_2b_t2_played = false;

    public GameObject h2_xue;
    public PlayableDirector h2_xue_pd;
    private bool h2_xue_started = false;
    private bool h2_xue_t1_played = false;
    private bool h2_xue_t2_played = false;
    private bool h2_xue_t3_played = false;
    private bool h2_xue_t2_started = false;
    private bool h2_xue_t3_started = false;
    private bool h2_xue_t4_started = false;
    private bool h2_xue_played = false;

    public GameObject c_xue_1_t1;
    public PlayableDirector c_xue_1_t1_pd;
    private bool c_xue_1_t1_started = false;
    private bool c_xue_1_t1_played = false;

    public GameObject c_xue_1_t2;
    public PlayableDirector c_xue_1_t2_pd;
    private bool c_xue_1_t2_started = false;
    private bool c_xue_1_t2_played = false;

    public GameObject c_xue_2_t1;
    public PlayableDirector c_xue_2_t1_pd;
    private bool c_xue_2_t1_started = false;
    private bool c_xue_2_t1_played = false;

    public GameObject c_xue_2_t2;
    public PlayableDirector c_xue_2_t2_pd;
    private bool c_xue_2_t2_started = false;
    private bool c_xue_2_t2_played = false;

    public GameObject c_xue_3a_t1;
    public PlayableDirector c_xue_3a_t1_pd;
    private bool c_xue_3a_t1_started = false;
    private bool c_xue_3a_t1_played = false;

    public GameObject c_xue_3a_t2;
    public PlayableDirector c_xue_3a_t2_pd;
    private bool c_xue_3a_t2_started = false;
    private bool c_xue_3a_t2_played = false;

    public GameObject c_xue_3a_t3;
    public PlayableDirector c_xue_3a_t3_pd;
    private bool c_xue_3a_t3_started = false;
    private bool c_xue_3a_t3_played = false;

    public GameObject c_xue_3b;
    public PlayableDirector c_xue_3b_pd;
    private bool c_xue_3b_started = false;
    private bool c_xue_3b_played = false;

    public GameObject c_xue_4a_t1;
    public PlayableDirector c_xue_4a_t1_pd;
    private bool c_xue_4a_t1_started = false;
    private bool c_xue_4a_t1_played = false;

    public GameObject c_xue_4a_t2;
    public PlayableDirector c_xue_4a_t2_pd;
    private bool c_xue_4a_t2_started = false;
    private bool c_xue_4a_t2_played = false;

    public GameObject c_xue_4b_t1;
    public PlayableDirector c_xue_4b_t1_pd;
    private bool c_xue_4b_t1_started = false;
    private bool c_xue_4b_t1_played = false;

    public GameObject c_xue_4b_t2;
    public PlayableDirector c_xue_4b_t2_pd;
    private bool c_xue_4b_t2_started = false;
    private bool c_xue_4b_t2_played = false;

    public GameObject d1_xue;
    public PlayableDirector d1_xue_pd;
    private bool d1_xue_started = false;
    private bool d1_xue_t1_played = false;
    private bool d1_xue_t2_started = false;
    private bool d1_xue_played = false;

    public GameObject d4_xue;
    public PlayableDirector d4_xue_pd;
    private bool d4_xue_started = false;
    private bool d4_xue_t1_played = false;
    private bool d4_xue_t2_played = false;
    private bool d4_xue_t3_played = false;
    private bool d4_xue_t4_played = false;
    private bool d4_xue_t5_played = false;
    private bool d4_xue_t2_started = false;
    private bool d4_xue_t3_started = false;
    private bool d4_xue_t4_started = false;
    private bool d4_xue_t5_started = false;
    private bool d4_xue_t6_started = false;
    private bool d4_xue_played = false;

    public GameObject d23_xue;
    public PlayableDirector d23_xue_pd;
    private bool d23_xue_started = false;
    private bool d23_xue_t1_played = false;
    private bool d23_xue_t2_started = false;
    private bool d23_xue_t2_played = false;
    private bool d23_xue_t3_started = false;
    private bool d23_xue_t3_played = false;
    private bool d23_xue_t4_started = false;
    private bool d23_xue_played = false;

    public GameObject a_hunter;
    public PlayableDirector a_hunter_pd;
    private bool a_hunter_started = false;
    private bool a_hunter_played = false;

    public GameObject b_hunter_1_t1;
    public PlayableDirector b_hunter_1_t1_pd;
    private bool b_hunter_1_t1_started = false;
    private bool b_hunter_1_t1_played = false;

    public GameObject b_hunter_1_t2;
    public PlayableDirector b_hunter_1_t2_pd;
    private bool b_hunter_1_t2_started = false;
    private bool b_hunter_1_t2_played = false;

    public GameObject b_hunter_2a_t1;
    public PlayableDirector b_hunter_2a_t1_pd;
    private bool b_hunter_2a_t1_started = false;
    private bool b_hunter_2a_t1_played = false;

    public GameObject b_hunter_2a_t2;
    public PlayableDirector b_hunter_2a_t2_pd;
    private bool b_hunter_2a_t2_started = false;
    private bool b_hunter_2a_t2_played = false;

    public GameObject b_hunter_2b;
    public PlayableDirector b_hunter_2b_pd;
    private bool b_hunter_2b_started = false;
    private bool b_hunter_2b_played = false;

    public GameObject h2_hunter_t1;
    public PlayableDirector h2_hunter_t1_pd;
    private bool h2_hunter_t1_started = false;
    private bool h2_hunter_t1_played = false;

    public GameObject h2_hunter_t2;
    public PlayableDirector h2_hunter_t2_pd;
    private bool h2_hunter_t2_started = false;
    private bool h2_hunter_t2_played = false;

    public GameObject c_hunter_1_t1;
    public PlayableDirector c_hunter_1_t1_pd;
    private bool c_hunter_1_t1_started = false;
    private bool c_hunter_1_t1_played = false;

    public GameObject c_hunter_1_t2;
    public PlayableDirector c_hunter_1_t2_pd;
    private bool c_hunter_1_t2_started = false;
    private bool c_hunter_1_t2_played = false;

    public GameObject c_hunter_2_t1;
    public PlayableDirector c_hunter_2_t1_pd;
    private bool c_hunter_2_t1_started = false;
    private bool c_hunter_2_t1_played = false;

    public GameObject c_hunter_2_t2;
    public PlayableDirector c_hunter_2_t2_pd;
    private bool c_hunter_2_t2_started = false;
    private bool c_hunter_2_t2_played = false;

    public GameObject c_hunter_2_t3;
    public PlayableDirector c_hunter_2_t3_pd;
    private bool c_hunter_2_t3_started = false;
    private bool c_hunter_2_t3_played = false;

    public GameObject c_hunter_2_t4;
    public PlayableDirector c_hunter_2_t4_pd;
    private bool c_hunter_2_t4_started = false;
    private bool c_hunter_2_t4_played = false;

    public GameObject c_hunter_3a_t1;
    public PlayableDirector c_hunter_3a_t1_pd;
    private bool c_hunter_3a_t1_started = false;
    private bool c_hunter_3a_t1_played = false;

    public GameObject c_hunter_3a_t2;
    public PlayableDirector c_hunter_3a_t2_pd;
    private bool c_hunter_3a_t2_started = false;
    private bool c_hunter_3a_t2_played = false;

    public GameObject c_hunter_3b_t1;
    public PlayableDirector c_hunter_3b_t1_pd;
    private bool c_hunter_3b_t1_started = false;
    private bool c_hunter_3b_t1_played = false;

    public GameObject c_hunter_3b_t2;
    public PlayableDirector c_hunter_3b_t2_pd;
    private bool c_hunter_3b_t2_started = false;
    private bool c_hunter_3b_t2_played = false;

    public GameObject c_hunter_4a_t1;
    public PlayableDirector c_hunter_4a_t1_pd;
    private bool c_hunter_4a_t1_started = false;
    private bool c_hunter_4a_t1_played = false;

    public GameObject c_hunter_4a_t2;
    public PlayableDirector c_hunter_4a_t2_pd;
    private bool c_hunter_4a_t2_started = false;
    private bool c_hunter_4a_t2_played = false;

    public GameObject c_hunter_4b;
    public PlayableDirector c_hunter_4b_pd;
    private bool c_hunter_4b_started = false;
    private bool c_hunter_4b_played = false;

    public GameObject d1_hunter;
    public PlayableDirector d1_hunter_pd;
    private bool d1_hunter_started = false;
    private bool d1_hunter_played = false;

    public GameObject d23_hunter;
    public PlayableDirector d23_hunter_pd;
    private bool d23_hunter_started = false;
    private bool d23_hunter_t1_played = false;
    private bool d23_hunter_t2_played = false;
    private bool d23_hunter_t3_played = false;
    private bool d23_hunter_t4_started = false;
    private bool d23_hunter_t2_started = false;
    private bool d23_hunter_t3_started = false;
    private bool d23_hunter_played = false;

    public GameObject d4_hunter;
    public PlayableDirector d4_hunter_pd;
    private bool d4_hunter_started = false;
    private bool d4_hunter_played = false;

    public GameObject D_A_24;
    public PlayableDirector D_A_24_pd;
    private bool D_A_24_started = false;
    private bool D_A_24_t1_played = false;
    private bool D_A_24_t2_started = false;
    private bool D_A_24_played = false;

    public GameObject D_B_3;
    public PlayableDirector D_B_3_pd;
    private bool D_B_3_started = false;
    private bool D_B_3_t1_played = false;
    private bool D_B_3_t2_played = false;
    private bool D_B_3_t3_played = false;
    private bool D_B_3_t4_played = false;
    private bool D_B_3_t5_played = false;
    private bool D_B_3_t6_played = false;
    private bool D_B_3_t2_started = false;
    private bool D_B_3_t3_started = false;
    private bool D_B_3_t4_started = false;
    private bool D_B_3_t5_started = false;
    private bool D_B_3_t6_started = false;
    private bool D_B_3_t7_started = false;
    private bool D_B_3_played = false;

    public GameObject D_B_4;
    public PlayableDirector D_B_4_pd;
    private bool D_B_4_started = false;
    private bool D_B_4_t1_played = false;
    private bool D_B_4_t2_played = false;
    private bool D_B_4_t3_played = false;
    private bool D_B_4_t4_played = false;
    private bool D_B_4_t5_played = false;
    private bool D_B_4_t6_played = false;
    private bool D_B_4_t2_started = false;
    private bool D_B_4_t3_started = false;
    private bool D_B_4_t4_started = false;
    private bool D_B_4_t5_started = false;
    private bool D_B_4_t6_started = false;
    private bool D_B_4_t7_started = false;
    private bool D_B_4_played = false;

    [SerializeField] private float frameRate = 60.0f;

    private bool toNextSceneOK = false;
    private bool toCutWari = false;
    public enum GameMode
    {
        TextPlaying, // テキストが再生中
        PlayerPlaying, // プレイヤーが操作している状態
        WaitForSceneChange, // 現シーンのゲーム内容が終了し、プレイヤーがEnterを押すのを待って次のシーンに切り替える
        Choosing
    }
    public GameMode currentGameMode = GameMode.TextPlaying; // 現シーン開始時にゲームモードをStartTextPlayingに設定

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the dictionary and add the points
        pointsDictionary = new Dictionary<int, Vector3>();

        // Add knight and hunter start and end points
        pointsDictionary.Add(0, startpoint_character1.position); // 左から1番目のスタート点
        pointsDictionary.Add(1, startpoint_character2.position); // 左から2番目のスタート点
        pointsDictionary.Add(14, endtpoint_character1.position);  // 左から1番目の終点
        pointsDictionary.Add(15, endpoint_character2.position);   // 左から2番目の終点

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

        hideText();
        bookEvent();

        CreateHoverAreaCharacter(character1, 5);
        CreateHoverAreaCharacter(character2, 1);
    }
    private void pauseAni()
    {
        if (d1_xue_started)
        {
            Debug.Log($"d1_xue time right now: {d1_xue_pd.time}");

            if (d1_xue_pd.time >= FrameToTime(910) && !d1_xue_t1_played) 
            {
                Debug.Log("stop the timeline d1_xue!");
                d1_xue_pd.Pause(); 
                d1_xue_t1_played = true;
            }
        }
        else if (d23_xue_started)
        {
            Debug.Log($"d23_xue time right now: {d23_xue_pd.time}");
            if (d23_xue_pd.time >= FrameToTime(1557) && !d23_xue_t1_played)
            {
                Debug.Log("stop the timeline d23_xue [first time]!");
                d23_xue_pd.Pause();
                d23_xue_t1_played = true;
            }
            else if (d23_xue_pd.time >= FrameToTime(1710) && !d23_xue_t2_played)
            {
                Debug.Log("stop the timeline d23_xue [second time]!");
                d23_xue_pd.Pause();
                d23_xue_t2_played = true;
            }
            else if (d23_xue_pd.time >= FrameToTime(2058) && !d23_xue_t3_played)
            {
                Debug.Log("stop the timeline d23_xue [third time]!");
                d23_xue_pd.Pause();
                d23_xue_t3_played = true;
            }
        }
        else if (d23_hunter_started)
        {
            Debug.Log($"d23_hunter time right now: {d23_hunter_pd.time}");
            if (d23_hunter_pd.time >= FrameToTime(854) && !d23_hunter_t1_played)
            {
                Debug.Log("stop the timeline d23_hunter [first time]!");
                d23_hunter_pd.Pause();
                d23_hunter_t1_played = true;
            }
            else if (d23_hunter_pd.time >= FrameToTime(1674) && !d23_hunter_t2_played)
            {
                Debug.Log("stop the timeline d23_hunter [second time]!");
                d23_hunter_pd.Pause();
                d23_hunter_t2_played = true;
            }
            else if (d23_hunter_pd.time >= FrameToTime(2529) && !d23_hunter_t3_played)
            {
                Debug.Log("stop the timeline d23_hunter [third time]!");
                d23_hunter_pd.Pause();
                d23_hunter_t3_played = true;
            }
        }
        else if (D_A_24_started)
        {
            Debug.Log($"D_A_24 time right now: {D_A_24_pd.time}");
            if (D_A_24_pd.time >= FrameToTime(824) && !D_A_24_t1_played)
            {
                Debug.Log("stop the timeline D_A_24_pd (dialogue_A_2_4)!");
                D_A_24_pd.Pause();
                D_A_24_t1_played = true;
            }
        }
        else if (h2_xue_started)
        {
            Debug.Log($"h2_xue time right now: {h2_xue_pd.time}");
            if (h2_xue_pd.time >= FrameToTime(210) && !h2_xue_t1_played)
            {
                Debug.Log("stop the timeline h2_xue_pd [first time](林雪が掲示板に着いた時の演出)!");
                h2_xue_pd.Pause();
                h2_xue_t1_played = true;
            }
            else if (h2_xue_pd.time >= FrameToTime(321) && !h2_xue_t2_played)
            {
                Debug.Log("stop the timeline h2_xue_pd [second time](林雪が掲示板に着いた時の演出)!");
                h2_xue_pd.Pause();
                h2_xue_t2_played = true;
            }
            else if (h2_xue_pd.time >= FrameToTime(431) && !h2_xue_t3_played)
            {
                Debug.Log("stop the timeline h2_xue_pd [third time](林雪が掲示板に着いた時の演出)!");
                h2_xue_pd.Pause();
                h2_xue_t3_played = true;
            }
        }
        else if (D_B_3_started)
        {
            Debug.Log($"Dialogue_B_3 time right now: {D_B_3_pd.time}");
            if (D_B_3_pd.time >= FrameToTime(1099) && !D_B_3_t1_played)
            {
                Debug.Log("stop the timeline dialogue_B_3 [first time](ルート3×酒場の演出)!");
                D_B_3_pd.Pause();
                D_B_3_t1_played = true;
            }
            else if (D_B_3_pd.time >= FrameToTime(1786) && !D_B_3_t2_played)
            {
                Debug.Log("stop the timeline dialogue_B_3 [second time](ルート3×酒場の演出)!");
                D_B_3_pd.Pause();
                D_B_3_t2_played = true;
            }
            else if (D_B_3_pd.time >= FrameToTime(2471) && !D_B_3_t3_played)
            {
                Debug.Log("stop the timeline dialogue_B_3 [third time](ルート3×酒場の演出)!");
                D_B_3_pd.Pause();
                D_B_3_t3_played = true;
            }
            else if (D_B_3_pd.time >= FrameToTime(3255) && !D_B_3_t4_played)
            {
                Debug.Log("stop the timeline dialogue_B_3 [forth time](ルート3×酒場の演出)!");
                D_B_3_pd.Pause();
                D_B_3_t4_played = true;
            }
            else if (D_B_3_pd.time >= FrameToTime(3961) && !D_B_3_t5_played)
            {
                Debug.Log("stop the timeline dialogue_B_3 [fifth time](ルート3×酒場の演出)!");
                D_B_3_pd.Pause();
                D_B_3_t5_played = true;
            }
            else if (D_B_3_pd.time >= FrameToTime(4768) && !D_B_3_t6_played)
            {
                Debug.Log("stop the timeline dialogue_B_3 [sixth time](ルート3×酒場の演出)!");
                D_B_3_pd.Pause();
                D_B_3_t6_played = true;
            }
        }
        else if (D_B_4_started)
        {
            Debug.Log($"Dialogue_B_4 time right now: {D_B_4_pd.time}");
            // 881  1555  2300  2897  3922  4613 
            if (D_B_4_pd.time >= FrameToTime(881) && !D_B_4_t1_played)
            {
                Debug.Log("stop the timeline dialogue_B_4 [first time](ルート4×酒場の演出)!");
                D_B_4_pd.Pause();
                D_B_4_t1_played = true;
            }
            else if (D_B_4_pd.time >= FrameToTime(1555) && !D_B_4_t2_played)
            {
                Debug.Log("stop the timeline dialogue_B_4 [second time](ルート4×酒場の演出)!");
                D_B_4_pd.Pause();
                D_B_4_t2_played = true;
            }
            else if (D_B_4_pd.time >= FrameToTime(2300) && !D_B_4_t3_played)
            {
                Debug.Log("stop the timeline dialogue_B_4 [third time](ルート4×酒場の演出)!");
                D_B_4_pd.Pause();
                D_B_4_t3_played = true;
            }
            else if (D_B_4_pd.time >= FrameToTime(2897) && !D_B_4_t4_played)
            {
                Debug.Log("stop the timeline dialogue_B_4 [forth time](ルート4×酒場の演出)!");
                D_B_4_pd.Pause();
                D_B_4_t4_played = true;
            }
            else if (D_B_4_pd.time >= FrameToTime(3922) && !D_B_4_t5_played)
            {
                Debug.Log("stop the timeline dialogue_B_4 [fifth time](ルート4×酒場の演出)!");
                D_B_4_pd.Pause();
                D_B_4_t5_played = true;
            }
            else if (D_B_4_pd.time >= FrameToTime(4613) && !D_B_4_t6_played)
            {
                Debug.Log("stop the timeline dialogue_B_4 [sixth time](ルート4×酒場の演出)!");
                D_B_4_pd.Pause();
                D_B_4_t6_played = true;
            }
        }
        else if (d4_xue_started)
        {
            if (d4_xue_pd.state == PlayState.Playing)
            {
                Debug.Log($"d4_xue time right now: {d4_xue_pd.time}");
            }
            else
            {
                Debug.Log($"Timeline state is not Playing: {d4_xue_pd.state}");
            }
            //1193  1785  2451  2979  
            if (d4_xue_pd.time >= FrameToTime(1193) && !d4_xue_t1_played)
            {
                Debug.Log("stop d4_xue for 1st");
                d4_xue_pd.Pause();
                d4_xue_t1_played = true;
            }
            else if (d4_xue_pd.time >= FrameToTime(1785) && !d4_xue_t2_played)
            {
                Debug.Log("stop d4_xue for 2nd");
                d4_xue_pd.Pause();
                d4_xue_t2_played = true;
            }
            else if (d4_xue_pd.time >= FrameToTime(2451) && !d4_xue_t3_played)
            {
                Debug.Log("stop d4_xue for 3rd");
                d4_xue_pd.Pause();
                d4_xue_t3_played = true;
            }
            else if (d4_xue_pd.time >= FrameToTime(2979) && !d4_xue_t4_played)
            {
                Debug.Log("stop d4_xue for 4th");
                d4_xue_pd.Pause();
                d4_xue_t4_played = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (currentGameMode)
        {
            case GameMode.TextPlaying:
                //Debug.Log("now is TextPlaying!");
                readText();
                pauseAni();
                break;

            case GameMode.PlayerPlaying:
                //Debug.Log("now is PlayerPlaying!");
                if (game_start.activeSelf)
                {
                    game_start.SetActive(false);
                }
                if (a_hunter.activeSelf)
                {
                    a_hunter.SetActive(false);
                }
                charaMoveAndAnimationLogic();
                break;

            case GameMode.WaitForSceneChange:
                //Debug.Log("now is WaitForSceneChange!");
                waitForSceneChange();
                break;

            case GameMode.Choosing:
                //Debug.Log("now is Choosing!");

                break;
        }
    }


    public void readText()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("readText!");
            //a_xue + a_hunter
            if (a_xue_1_started && !a_xue_1_played)
            {
                // a_xue_1の最後に行く
                a_xue_1_pd.time = a_xue_1_pd.duration;
                a_xue_1_pd.Evaluate();
            }
            else if (!a_xue_1_started && a_xue_1_played && !a_xue_2_started && !a_xue_2_played )
            {
                //  a_xue_1を非表示にする　a_xue_2の再生
                a_xue_1.SetActive(false);
                a_xue_2.SetActive(true);
                a_xue_2_started = true;
            }
            else if (a_xue_2_started && !a_xue_2_played)
            {
                // a_xue_2 の最後に行く
                a_xue_2_pd.time = a_xue_2_pd.duration;
                a_xue_2_pd.Evaluate();
            }
            else if (!a_xue_2_started && a_xue_2_played && !a_hunter_started && !a_hunter_played )
            {
                //  a_xue_2を非表示にする　dog_run_pdの再生
                a_xue_2.SetActive(false);
                dog_run_pd.Play();
            }
            else if (a_hunter_started && !a_hunter_played)
            {
                // a_hunter の最後に行く
                a_hunter_pd.time = a_hunter_pd.duration;
                a_hunter_pd.Evaluate();
            }
            else if (!a_hunter_started && a_hunter_played && !h1_xue_dog_started && !h1_xue_dog_played)
            {
                game_start.SetActive(true);
                a_hunter.SetActive(false);
            }

            //h1_xue_dog 
            if(h1_xue_dog_started && !h1_xue_dog_played)
            {
                h1_xue_dog_pd.time = h1_xue_dog_pd.duration;
                h1_xue_dog_pd.Evaluate();
            }

            //b_xue_1 + b_hunter_1
            play_b_1();

            //h2_hunter
            if (h2_hunter_t1_started && !h2_hunter_t1_played &&!h2_hunter_t2_started && !h2_hunter_t2_played)
            {
                h2_hunter_t1_pd.time = h2_hunter_t1_pd.duration;
                h2_hunter_t1_pd.Evaluate();
            }
            else if(!h2_hunter_t1_started && h2_hunter_t1_played && !h2_hunter_t2_started && !h2_hunter_t2_played)
            {
                h2_hunter_t1.SetActive(false);
                h2_hunter_t2.SetActive(true);
                h2_hunter_t2_started = true;
            }
            else if(h2_hunter_t2_started && !h2_hunter_t2_played)
            {
                h2_hunter_t2_pd.time = h2_hunter_t2_pd.duration;
                h2_hunter_t2_pd.Evaluate();
            }

            //c_xue_1 + c_hunter_1
            play_c_1();
            

            //d1_xue
            if (d1_xue_started && !d1_xue_t1_played && !d1_xue_t2_started && !d1_xue_played)
            {
                d1_xue_pd.time = FrameToTime(910);
                d1_xue_pd.Evaluate();
            }
            else if (d1_xue_started && d1_xue_t1_played && !d1_xue_t2_started && !d1_xue_played)
            {
                d1_xue_pd.Play();
                d1_xue_t2_started = true;
            }
            else if (d1_xue_started && d1_xue_t1_played && d1_xue_t2_started && !d1_xue_played)
            {
                d1_xue_pd.time = d1_xue_pd.duration;
                d1_xue_pd.Evaluate();
            }

            //d1_hunter
            if (d1_hunter_started && !d1_hunter_played)
            {
                d1_hunter_pd.time = d1_hunter_pd.duration;
                d1_hunter_pd.Evaluate();
            }

            //b_xue_2a + b_hunter_2a
            play_b_2a();

            // b_hunter_2b + b_xue_2b
            play_b_2b();
            
            //h2_xue
            play_h2_xue();

            //d23_xue
            play_d23_xue();

            //d23_hunter
            play_d23_hunter();

            //c_hunter_2 + c_xue_2
            play_c_2();

            //c_xue_3a + c_hunter_3a
            play_c_3a();

            //D_A_2_4
            if (D_A_24_started && !D_A_24_t1_played && !D_A_24_t2_started && !D_A_24_played)
            {
                D_A_24_pd.time = FrameToTime(824);
                D_A_24_pd.Evaluate();
            }
            else if (D_A_24_started && D_A_24_t1_played && !D_A_24_t2_started && !D_A_24_played)
            {
                D_A_24_pd.Play();
                D_A_24_t2_started = true;
            }
            else if (D_A_24_started && D_A_24_t1_played && D_A_24_t2_started && !D_A_24_played)
            {
                D_A_24_pd.time = D_A_24_pd.duration;
                D_A_24_pd.Evaluate();
            }

            //D_B_3
            play_D_B_3();

            //D_B_4     881  1555  2300  2897  3765  4462 
            play_D_B_4();


            //c_hunter_3b + c_xue_3b
            play_c_3b();

            //c_hunter_4a + c_xue_4a
            play_c_4a();

            //c_xue_4b + c_hunter_4b
            play_c_4b_con();

            //d4_xue
            play_d4_xue();

            //d4_hunter
            play_d4_hunter();

        }
    }

    //b_xue_2a + b_hunter_2a
    public void play_b_2a()
    {
        if (b_xue_2a_started && !b_xue_2a_played && !b_hunter_2a_t1_started && !b_hunter_2a_t1_played && !b_hunter_2a_t2_started && !b_hunter_2a_t2_played)
        {
            b_xue_2a_pd.time = b_xue_2a_pd.duration;
            b_xue_2a_pd.Evaluate();
        }
        else if (!b_xue_2a_started && b_xue_2a_played && !b_hunter_2a_t1_started && !b_hunter_2a_t1_played && !b_hunter_2a_t2_started && !b_hunter_2a_t2_played)
        {
            b_xue_2a.SetActive(false);
            Debug.Log("b_xue_2a:" + b_xue_2a.activeSelf);
            b_hunter_2a_t1.SetActive(true);
            b_hunter_2a_t1_started = true;
        }
        else if (!b_xue_2a_started && b_xue_2a_played && b_hunter_2a_t1_started && !b_hunter_2a_t1_played && !b_hunter_2a_t2_started && !b_hunter_2a_t2_played)
        {
            b_hunter_2a_t1_pd.time = b_hunter_2a_t1_pd.duration;
            b_hunter_2a_t1_pd.Evaluate();
        }
        else if (!b_xue_2a_started && b_xue_2a_played && !b_hunter_2a_t1_started && b_hunter_2a_t1_played && !b_hunter_2a_t2_started && !b_hunter_2a_t2_played)
        {
            b_hunter_2a_t1.SetActive(false);
            b_hunter_2a_t2.SetActive(true);
            b_hunter_2a_t2_started = true;
        }
        else if (!b_xue_2a_started && b_xue_2a_played && !b_hunter_2a_t1_started && b_hunter_2a_t1_played && b_hunter_2a_t2_started && !b_hunter_2a_t2_played)
        {
            b_hunter_2a_t2_pd.time = b_hunter_2a_t2_pd.duration;
            b_hunter_2a_t2_pd.Evaluate();
        }
    }

    // b_hunter_2b + b_xue_2b
    public void play_b_2b()
    {
        if (b_hunter_2b_started && !b_hunter_2b_played && !b_xue_2b_t1_started && !b_xue_2b_t1_played && !b_xue_2b_t2_started && !b_xue_2b_t2_played)
        {
            b_hunter_2b_pd.time = b_hunter_2b_pd.duration;
            b_hunter_2b_pd.Evaluate();
        }
        else if (!b_hunter_2b_started && b_hunter_2b_played && !b_xue_2b_t1_started && !b_xue_2b_t1_played && !b_xue_2b_t2_started && !b_xue_2b_t2_played)
        {
            b_hunter_2b.SetActive(false);
            b_xue_2b_t1.SetActive(true);
            b_xue_2b_t1_started = true;
        }
        else if (!b_hunter_2b_started && b_hunter_2b_played && b_xue_2b_t1_started && !b_xue_2b_t1_played && !b_xue_2b_t2_started && !b_xue_2b_t2_played)
        {
            b_xue_2b_t1_pd.time = b_xue_2b_t1_pd.duration;
            b_xue_2b_t1_pd.Evaluate();
        }
        else if (!b_hunter_2b_started && b_hunter_2b_played && !b_xue_2b_t1_started && b_xue_2b_t1_played && !b_xue_2b_t2_started && !b_xue_2b_t2_played)
        {
            b_xue_2b_t1.SetActive(false);
            b_xue_2b_t2.SetActive(true);
            b_xue_2b_t2_started = true;
        }
        else if (!b_hunter_2b_started && b_hunter_2b_played && !b_xue_2b_t1_started && b_xue_2b_t1_played && b_xue_2b_t2_started && !b_xue_2b_t2_played)
        {
            b_xue_2b_t2_pd.time = b_xue_2b_t2_pd.duration;
            b_xue_2b_t2_pd.Evaluate();
        }
    }

    //b_xue_1 + b_hunter_1
    public void play_b_1()
    {

        if (b_xue_1_t1_started && !b_xue_1_t1_played && !b_xue_1_t2_started && !b_xue_1_t2_played)
        {
            b_xue_1_t1_pd.time = b_xue_1_t1_pd.duration;
            b_xue_1_t1_pd.Evaluate();
        }
        else if (!b_xue_1_t1_started && b_xue_1_t1_played && !b_xue_1_t2_started && !b_xue_1_t2_played)
        {
            b_xue_1_t1.SetActive(false);
            b_xue_1_t2.SetActive(true);
            b_xue_1_t2_started = true;
        }
        else if (b_xue_1_t2_started && !b_xue_1_t2_played && !b_hunter_1_t1_started && !b_hunter_1_t1_played)
        {
            b_xue_1_t2_pd.time = b_xue_1_t2_pd.duration;
            b_xue_1_t2_pd.Evaluate();
        }
        else if (!b_xue_1_t2_started && b_xue_1_t2_played && !b_hunter_1_t1_started && !b_hunter_1_t1_played)
        {
            b_xue_1_t2.SetActive(false);
            b_hunter_1_t1.SetActive(true);
            b_hunter_1_t1_started = true;
        }
        else if (b_hunter_1_t1_started && !b_hunter_1_t1_played && !b_hunter_1_t2_started && !b_hunter_1_t2_played)
        {
            b_hunter_1_t1_pd.time = b_hunter_1_t1_pd.duration;
            b_hunter_1_t1_pd.Evaluate();
        }
        else if (!b_hunter_1_t1_started && b_hunter_1_t1_played && !b_hunter_1_t2_started && !b_hunter_1_t2_played)
        {
            b_hunter_1_t1.SetActive(false);
            b_hunter_1_t2.SetActive(true);
            b_hunter_1_t2_started = true;
        }
        else if (b_hunter_1_t2_started && !b_hunter_1_t2_played && !h2_hunter_t1_started && !h2_hunter_t1_played)
        {
            b_hunter_1_t2_pd.time = b_hunter_1_t2_pd.duration;
            b_hunter_1_t2_pd.Evaluate();
        }
    }

    //c_xue_1 + c_hunter_1
    public void play_c_1()
    {
        if (c_xue_1_t1_started && !c_xue_1_t1_played && !c_xue_1_t2_started && !c_xue_1_t2_played)
        {
            c_xue_1_t1_pd.time = c_xue_1_t1_pd.duration;
            c_xue_1_t1_pd.Evaluate();
        }
        else if (!c_xue_1_t1_started && c_xue_1_t1_played && !c_xue_1_t2_started && !c_xue_1_t2_played)
        {
            c_xue_1_t1.SetActive(false);
            c_xue_1_t2.SetActive(true);
            c_xue_1_t2_started = true;
        }
        else if (c_xue_1_t2_started && !c_xue_1_t2_played && !c_hunter_1_t1_started && !c_hunter_1_t1_played)
        {
            c_xue_1_t2_pd.time = c_xue_1_t2_pd.duration;
            c_xue_1_t2_pd.Evaluate();
        }
        else if (!c_xue_1_t2_started && c_xue_1_t2_played && !c_hunter_1_t1_started && !c_hunter_1_t1_played)
        {
            c_xue_1_t2.SetActive(false);
            c_hunter_1_t1.SetActive(true);
            c_hunter_1_t1_started = true;
        }
        else if (c_hunter_1_t1_started && !c_hunter_1_t1_played && !c_hunter_1_t2_started && !c_hunter_1_t2_played)
        {
            c_hunter_1_t1_pd.time = c_hunter_1_t1_pd.duration;
            c_hunter_1_t1_pd.Evaluate();
        }
        else if (!c_hunter_1_t1_started && c_hunter_1_t1_played && !c_hunter_1_t2_started && !c_hunter_1_t2_played)
        {
            c_hunter_1_t1.SetActive(false);
            c_hunter_1_t2.SetActive(true);
            c_hunter_1_t2_started = true;
        }
        else if (c_hunter_1_t2_started && !c_hunter_1_t2_played && !d1_xue_started && !d1_xue_t1_played && !d1_xue_t2_started && !d1_xue_played)
        {
            c_hunter_1_t2_pd.time = c_hunter_1_t2_pd.duration;
            c_hunter_1_t2_pd.Evaluate();
        }
    }

    //c_hunter_2 + c_xue_2
    public void play_c_2()
    {
        if (c_hunter_2_t1_started && !c_hunter_2_t1_played && !c_hunter_2_t2_started && !c_hunter_2_t2_played)
        {
            c_hunter_2_t1_pd.time = c_hunter_2_t1_pd.duration;
            c_hunter_2_t1_pd.Evaluate();
        }
        else if (!c_hunter_2_t1_started && c_hunter_2_t1_played && !c_hunter_2_t2_started && !c_hunter_2_t2_played)
        {
            c_hunter_2_t1.SetActive(false);
            c_hunter_2_t2.SetActive(true);
            c_hunter_2_t2_started = true;
        }
        else if (c_hunter_2_t2_started && !c_hunter_2_t2_played && !c_hunter_2_t3_started && !c_hunter_2_t3_played)
        {
            c_hunter_2_t2_pd.time = c_hunter_2_t2_pd.duration;
            c_hunter_2_t2_pd.Evaluate();
        }
        else if (!c_hunter_2_t2_started && c_hunter_2_t2_played && !c_hunter_2_t3_started && !c_hunter_2_t3_played)
        {
            c_hunter_2_t2.SetActive(false);
            c_hunter_2_t3.SetActive(true);
            c_hunter_2_t3_started = true;
        }
        else if (!c_hunter_2_t2_started && c_hunter_2_t2_played && c_hunter_2_t3_started && !c_hunter_2_t3_played)
        {
            c_hunter_2_t3_pd.time = c_hunter_2_t3_pd.duration;
            c_hunter_2_t3_pd.Evaluate();
        }
        else if (!c_hunter_2_t3_started && c_hunter_2_t3_played && !c_hunter_2_t4_started && !c_hunter_2_t4_played)
        {
            c_hunter_2_t3.SetActive(false);
            c_hunter_2_t4.SetActive(true);
            c_hunter_2_t4_started = true;
        }
        else if (!c_hunter_2_t3_started && c_hunter_2_t3_played && c_hunter_2_t4_started && !c_hunter_2_t4_played)
        {
            c_hunter_2_t4_pd.time = c_hunter_2_t4_pd.duration;
            c_hunter_2_t4_pd.Evaluate();
        }
        else if (!c_hunter_2_t4_started && c_hunter_2_t4_played && !c_xue_2_t1_started && !c_xue_2_t1_played)
        {
            c_hunter_2_t4.SetActive(false);
            c_xue_2_t1.SetActive(true);
            c_xue_2_t1_started = true;
        }
        else if (c_xue_2_t1_started && !c_xue_2_t1_played && !c_xue_2_t2_started && !c_xue_2_t2_played)
        {
            c_xue_2_t1_pd.time = c_xue_2_t1_pd.duration;
            c_xue_2_t1_pd.Evaluate();
        }
        else if (!c_xue_2_t1_started && c_xue_2_t1_played && !c_xue_2_t2_started && !c_xue_2_t2_played)
        {
            c_xue_2_t1.SetActive(false);
            c_xue_2_t2.SetActive(true);
            c_xue_2_t2_started = true;
        }
        else if (!c_xue_2_t1_started && c_xue_2_t1_played && c_xue_2_t2_started && !c_xue_2_t2_played)
        {
            c_xue_2_t2_pd.time = c_xue_2_t2_pd.duration;
            c_xue_2_t2_pd.Evaluate();
        }
    }

    //c_xue_3a + c_hunter_3a
    public void play_c_3a()
    {
        if (c_xue_3a_t1_started && !c_xue_3a_t1_played && !c_xue_3a_t2_started && !c_xue_3a_t2_played)
        {
            c_xue_3a_t1_pd.time = c_xue_3a_t1_pd.duration;
            c_xue_3a_t1_pd.Evaluate();
        }
        else if (!c_xue_3a_t1_started && c_xue_3a_t1_played && !c_xue_3a_t2_started && !c_xue_3a_t2_played)
        {
            c_xue_3a_t1.SetActive(false);
            c_xue_3a_t2.SetActive(true);
            c_xue_3a_t2_started = true;
        }
        else if (c_xue_3a_t2_started && !c_xue_3a_t2_played && !c_hunter_3a_t1_started && !c_hunter_3a_t1_played)
        {
            c_xue_3a_t2_pd.time = c_xue_3a_t2_pd.duration;
            c_xue_3a_t2_pd.Evaluate();
        }
        else if (!c_xue_3a_t2_started && c_xue_3a_t2_played && !c_hunter_3a_t1_started && !c_hunter_3a_t1_played)
        {
            c_xue_3a_t2.SetActive(false);
            c_hunter_3a_t1.SetActive(true);
            c_hunter_3a_t1_started = true;
        }
        else if (c_hunter_3a_t1_started && !c_hunter_3a_t1_played && !c_hunter_3a_t2_started && !c_hunter_3a_t2_played)
        {
            c_hunter_3a_t1_pd.time = c_hunter_3a_t1_pd.duration;
            c_hunter_3a_t1_pd.Evaluate();
        }
        else if (!c_hunter_3a_t1_started && c_hunter_3a_t1_played && !c_hunter_3a_t2_started && !c_hunter_3a_t2_played)
        {
            c_hunter_3a_t1.SetActive(false);
            c_hunter_3a_t2.SetActive(true);
            c_hunter_3a_t2_started = true;
        }
        else if (c_hunter_3a_t2_started && !c_hunter_3a_t2_played)
        {
            c_hunter_3a_t2_pd.time = c_hunter_3a_t2_pd.duration;
            c_hunter_3a_t2_pd.Evaluate();
        }
    }

    //c_hunter_3b + c_xue_3b
    public void play_c_3b()
    {
        if (c_hunter_3b_t1_started && !c_hunter_3b_t1_played && !c_hunter_3b_t2_started && !c_hunter_3b_t2_played)
        {
            c_hunter_3b_t1_pd.time = c_hunter_3b_t1_pd.duration;
            c_hunter_3b_t1_pd.Evaluate();
        }
        else if (!c_hunter_3b_t1_started && c_hunter_3b_t1_played && !c_hunter_3b_t2_started && !c_hunter_3b_t2_played)
        {
            c_hunter_3b_t1.SetActive(false);
            c_hunter_3b_t2.SetActive(true);
            c_hunter_3b_t2_started = true;
        }
        else if (c_hunter_3b_t2_started && !c_hunter_3b_t2_played && !c_xue_3b_started && !c_xue_3b_played)
        {
            c_hunter_3b_t2_pd.time = c_hunter_3b_t2_pd.duration;
            c_hunter_3b_t2_pd.Evaluate();
        }
        else if (!c_hunter_3b_t2_started && c_hunter_3b_t2_played && !c_xue_3b_started && !c_xue_3b_played)
        {
            c_hunter_3b_t2.SetActive(false);
            c_xue_3b.SetActive(true);
            c_xue_3b_started = true;
        }
        else if (c_xue_3b_started && !c_xue_3b_played)
        {
            c_xue_3b_pd.time = c_xue_3b_pd.duration;
            c_xue_3b_pd.Evaluate();
        }
    }

    //h2_xue
    public void play_h2_xue()
    {
        if (h2_xue_started && !h2_xue_t1_played && !h2_xue_t2_started && !h2_xue_t2_played && !h2_xue_t3_started && !h2_xue_t3_played && !h2_xue_t4_started && !h2_xue_played)
        {
            h2_xue_pd.time = FrameToTime(210);
            h2_xue_pd.Evaluate();
        }
        else if (h2_xue_started && h2_xue_t1_played && !h2_xue_t2_started && !h2_xue_t2_played && !h2_xue_t3_started && !h2_xue_t3_played && !h2_xue_t4_started && !h2_xue_played)
        {
            h2_xue_pd.Play();
            h2_xue_t2_started = true;
        }
        else if (h2_xue_started && h2_xue_t1_played && h2_xue_t2_started && !h2_xue_t2_played && !h2_xue_t3_started && !h2_xue_t3_played && !h2_xue_t4_started && !h2_xue_played)
        {
            h2_xue_pd.time = FrameToTime(321);
            h2_xue_pd.Evaluate();
        }
        else if (h2_xue_started && h2_xue_t1_played && h2_xue_t2_started && h2_xue_t2_played && !h2_xue_t3_started && !h2_xue_t3_played && !h2_xue_t4_started && !h2_xue_played)
        {
            h2_xue_pd.Play();
            h2_xue_t3_started = true;
        }
        else if (h2_xue_started && h2_xue_t1_played && h2_xue_t2_started && h2_xue_t2_played && h2_xue_t3_started && !h2_xue_t3_played && !h2_xue_t4_started && !h2_xue_played)
        {
            h2_xue_pd.time = FrameToTime(431);
            h2_xue_pd.Evaluate();
        }
        else if (h2_xue_started && h2_xue_t1_played && h2_xue_t2_started && h2_xue_t2_played && h2_xue_t3_started && h2_xue_t3_played && !h2_xue_t4_started && !h2_xue_played)
        {
            h2_xue_pd.Play();
            h2_xue_t4_started = true;
        }
        else if (h2_xue_started && h2_xue_t1_played && h2_xue_t2_started && h2_xue_t2_played && h2_xue_t3_started && h2_xue_t3_played && h2_xue_t4_started && !h2_xue_played)
        {
            h2_xue_pd.time = h2_xue_pd.duration;
            h2_xue_pd.Evaluate();
        }
    }

    //c_hunter_4a + c_xue_4a
    public void play_c_4a()
    {
        if (c_hunter_4a_t1_started && !c_hunter_4a_t1_played && !c_hunter_4a_t2_started && !c_hunter_4a_t2_played)
        {
            c_hunter_4a_t1_pd.time = c_hunter_4a_t1_pd.duration;
            c_hunter_4a_t1_pd.Evaluate();
        }
        else if (!c_hunter_4a_t1_started && c_hunter_4a_t1_played && !c_hunter_4a_t2_started && !c_hunter_4a_t2_played)
        {
            c_hunter_4a_t1.SetActive(false);
            c_hunter_4a_t2.SetActive(true);
            c_hunter_4a_t2_started = true;
        }
        else if (c_hunter_4a_t2_started && !c_hunter_4a_t2_played && !c_xue_4a_t1_started && !c_xue_4a_t1_played)
        {
            c_hunter_4a_t2_pd.time = c_hunter_4a_t2_pd.duration;
            c_hunter_4a_t2_pd.Evaluate();
        }
        else if (!c_hunter_4a_t2_started && c_hunter_4a_t2_played && !c_xue_4a_t1_started && !c_xue_4a_t1_played)
        {
            c_hunter_4a_t2.SetActive(false);
            c_xue_4a_t1.SetActive(true);
            c_xue_4a_t1_started = true;
        }
        else if (c_xue_4a_t1_started && !c_xue_4a_t1_played && !c_xue_4a_t2_started && !c_xue_4a_t2_played)
        {
            c_xue_4a_t1_pd.time = c_xue_4a_t1_pd.duration;
            c_xue_4a_t1_pd.Evaluate();
        }
        else if (!c_xue_4a_t1_started && c_xue_4a_t1_played && !c_xue_4a_t2_started && !c_xue_4a_t2_played)
        {
            c_xue_4a_t1.SetActive(false);
            c_xue_4a_t2.SetActive(true);
            c_xue_4a_t2_started = true;
        }
        else if (c_xue_4a_t2_started && !c_xue_4a_t2_played)
        {
            c_xue_4a_t2_pd.time = c_xue_4a_t2_pd.duration;
            c_xue_4a_t2_pd.Evaluate();
        }
    }

    //c_xue_4b + c_hunter_4b
    public void play_c_4b_con()
    {
        if (c_xue_4b_t1_started && !c_xue_4b_t1_played && !c_xue_4b_t2_started && !c_xue_4b_t2_played)
        {
            c_xue_4b_t1_pd.time = c_xue_4b_t1_pd.duration;
            c_xue_4b_t1_pd.Evaluate();
        }
        else if (!c_xue_4b_t1_started && c_xue_4b_t1_played && !c_xue_4b_t2_started && !c_xue_4b_t2_played)
        {
            c_xue_4b_t1.SetActive(false);
            c_xue_4b_t2.SetActive(true);
            c_xue_4b_t2_started = true;
        }
        else if (c_xue_4b_t2_started && !c_xue_4b_t2_played && !c_hunter_4b_started && !c_hunter_4b_played)
        {
            c_xue_4b_t2_pd.time = c_xue_4b_t2_pd.duration;
            c_xue_4b_t2_pd.Evaluate();
        }
        else if (!c_xue_4b_t2_started && c_xue_4b_t2_played && !c_hunter_4b_started && !c_hunter_4b_played)
        {
            c_xue_4b_t2.SetActive(false);
            c_hunter_4b.SetActive(true);
            c_hunter_4b_started = true;
        }
        else if (c_hunter_4b_started && !c_hunter_4b_played)
        {
            c_hunter_4b_pd.time = c_hunter_4b_pd.duration;
            c_hunter_4b_pd.Evaluate();
        }
    }

    //d4_xue 
    //1193  1785  2451  2979  3520
    public void play_d4_xue()
    {
        if (d4_xue_started && !d4_xue_played && !d4_xue_t1_played && !d4_xue_t2_started && !d4_xue_t2_played)
        {
            d4_xue_pd.time = FrameToTime(1193);
            d4_xue_pd.Evaluate();
        }
        else if (d4_xue_started && !d4_xue_played && d4_xue_t1_played && !d4_xue_t2_started && !d4_xue_t2_played)
        {
            d4_xue_pd.Play();
            d4_xue_t2_started = true;
        }
        else if (d4_xue_started && !d4_xue_played && d4_xue_t2_started && !d4_xue_t2_played && !d4_xue_t3_started && !d4_xue_t3_played)
        {
            d4_xue_pd.time = FrameToTime(1785);
            d4_xue_pd.Evaluate();
        }
        else if (d4_xue_started && !d4_xue_played && d4_xue_t2_started && d4_xue_t2_played && !d4_xue_t3_started && !d4_xue_t3_played)
        {
            d4_xue_pd.Play();
            d4_xue_t3_started = true;
        }
        else if (d4_xue_started && !d4_xue_played && d4_xue_t3_started && !d4_xue_t3_played && !d4_xue_t4_started && !d4_xue_t4_played)
        {
            d4_xue_pd.time = FrameToTime(2451);
            d4_xue_pd.Evaluate();
        }
        else if (d4_xue_started && !d4_xue_played && d4_xue_t3_started && d4_xue_t3_played && !d4_xue_t4_started && !d4_xue_t4_played)
        {
            d4_xue_pd.Play();
            d4_xue_t4_started = true;
        }
        else if (d4_xue_started && !d4_xue_played && d4_xue_t4_started && !d4_xue_t4_played && !d4_xue_t5_started && !d4_xue_t5_played)
        {
            d4_xue_pd.time = FrameToTime(2979);
            d4_xue_pd.Evaluate();
        }
        else if (d4_xue_started && !d4_xue_played && d4_xue_t4_started && d4_xue_t4_played && !d4_xue_t5_started && !d4_xue_t5_played)
        {
            d4_xue_pd.Play();
            d4_xue_t5_started = true;
        }
        else if (d4_xue_started && !d4_xue_played && d4_xue_t5_started && !d4_xue_t5_played && !d4_xue_t6_started)
        {
            d4_xue_pd.time = d4_xue_pd.duration;
            d4_xue_pd.Evaluate();
        }
    }

    //d4_hunter
    public void play_d4_hunter()
    {
        if (d4_hunter_started && !d4_hunter_played)
        {
            d4_hunter_pd.time = d4_hunter_pd.duration;
            d4_hunter_pd.Evaluate();
        }
    }
    public void play_d23_xue()
    {
        if (d23_xue_started && !d23_xue_t1_played && !d23_xue_t2_started && !d23_xue_t2_played && !d23_xue_t3_started && !d23_xue_t3_played && !d23_xue_t4_started && !d23_xue_played)
        {
            d23_xue_pd.time = FrameToTime(1557);
            d23_xue_pd.Evaluate();
        }
        else if (d23_xue_started && d23_xue_t1_played && !d23_xue_t2_started && !d23_xue_t2_played && !d23_xue_t3_started && !d23_xue_t3_played && !d23_xue_t4_started && !d23_xue_played)
        {
            Debug.Log("should be played!");
            d23_xue_pd.Play();
            d23_xue_t2_started = true;
        }
        else if (d23_xue_started && d23_xue_t1_played && d23_xue_t2_started && !d23_xue_t2_played && !d23_xue_t3_started && !d23_xue_t3_played && !d23_xue_t4_started && !d23_xue_played)
        {
            d23_xue_pd.time = FrameToTime(1710);
            d23_xue_pd.Evaluate();
        }
        else if (d23_xue_started && d23_xue_t1_played && d23_xue_t2_started && d23_xue_t2_played && !d23_xue_t3_started && !d23_xue_t3_played && !d23_xue_t4_started && !d23_xue_played)
        {
            d23_xue_pd.Play();
            d23_xue_t3_started = true;
        }
        else if (d23_xue_started && d23_xue_t1_played && d23_xue_t2_started && d23_xue_t2_played && d23_xue_t3_started && !d23_xue_t3_played && !d23_xue_t4_started && !d23_xue_played)
        {
            d23_xue_pd.time = FrameToTime(2058);
            d23_xue_pd.Evaluate();
        }
        else if (d23_xue_started && d23_xue_t1_played && d23_xue_t2_started && d23_xue_t2_played && d23_xue_t3_started && d23_xue_t3_played && !d23_xue_t4_started && !d23_xue_played)
        {
            d23_xue_pd.Play();
            d23_xue_t4_started = true;
        }
        else if (d23_xue_started && d23_xue_t1_played && d23_xue_t2_started && d23_xue_t2_played && d23_xue_t3_started && d23_xue_t3_played && d23_xue_t4_started && !d23_xue_played)
        {
            d23_xue_pd.time = d23_xue_pd.duration;
            d23_xue_pd.Evaluate();
        }
    }
    public void play_d23_hunter()
    {
        if (d23_hunter_started && !d23_hunter_t1_played && !d23_hunter_t2_started && !d23_hunter_t2_played && !d23_hunter_t3_started && !d23_hunter_t3_played && !d23_hunter_t4_started && !d23_hunter_played)
        {
            d23_hunter_pd.time = FrameToTime(854);
            d23_hunter_pd.Evaluate();
        }
        else if (d23_hunter_started && d23_hunter_t1_played && !d23_hunter_t2_started && !d23_hunter_t2_played && !d23_hunter_t3_started && !d23_hunter_t3_played && !d23_hunter_t4_started && !d23_hunter_played)
        {
            d23_hunter_pd.Play();
            d23_hunter_t2_started = true;
        }
        else if (d23_hunter_started && d23_hunter_t1_played && d23_hunter_t2_started && !d23_hunter_t2_played && !d23_hunter_t3_started && !d23_hunter_t3_played && !d23_hunter_t4_started && !d23_hunter_played)
        {
            d23_hunter_pd.time = FrameToTime(1674);
            d23_hunter_pd.Evaluate();
        }
        else if (d23_hunter_started && d23_hunter_t1_played && d23_hunter_t2_started && d23_hunter_t2_played && !d23_hunter_t3_started && !d23_hunter_t3_played && !d23_hunter_t4_started && !d23_hunter_played)
        {
            d23_hunter_pd.Play();
            d23_hunter_t3_started = true;
        }
        else if (d23_hunter_started && d23_hunter_t1_played && d23_hunter_t2_started && d23_hunter_t2_played && d23_hunter_t3_started && !d23_hunter_t3_played && !d23_hunter_t4_started && !d23_hunter_played)
        {
            d23_hunter_pd.time = FrameToTime(2529);
            d23_hunter_pd.Evaluate();
        }
        else if (d23_hunter_started && d23_hunter_t1_played && d23_hunter_t2_started && d23_hunter_t2_played && d23_hunter_t3_started && d23_hunter_t3_played && !d23_hunter_t4_started && !d23_hunter_played)
        {
            d23_hunter_pd.Play();
            d23_hunter_t4_started = true;
        }
        else if (d23_hunter_started && d23_hunter_t1_played && d23_hunter_t2_started && d23_hunter_t2_played && d23_hunter_t3_started && d23_hunter_t3_played && d23_hunter_t4_started && !d23_hunter_played)
        {
            d23_hunter_pd.time = d23_hunter_pd.duration;
            d23_hunter_pd.Evaluate();
        }
    }
    public void play_D_B_3()
    {
        if (D_B_3_started && !D_B_3_t1_played && !D_B_3_t2_started && !D_B_3_t2_played && !D_B_3_played)
        {
            D_B_3_pd.time = FrameToTime(1099);
            D_B_3_pd.Evaluate();
        }
        else if (D_B_3_started && D_B_3_t1_played && !D_B_3_t2_started && !D_B_3_t2_played && !D_B_3_played)
        {
            D_B_3_pd.Play();
            D_B_3_t2_started = true;
        }
        else if (D_B_3_started && D_B_3_t2_started && !D_B_3_t2_played && !D_B_3_t3_started && !D_B_3_t3_played && !D_B_3_played)
        {
            D_B_3_pd.time = FrameToTime(1786);
            D_B_3_pd.Evaluate();
        }
        else if (D_B_3_started && D_B_3_t2_started && D_B_3_t2_played && !D_B_3_t3_started && !D_B_3_t3_played && !D_B_3_played)
        {
            D_B_3_pd.Play();
            D_B_3_t3_started = true;
        }
        else if (D_B_3_started && D_B_3_t3_started && !D_B_3_t3_played && !D_B_3_t4_started && !D_B_3_t4_played && !D_B_3_played)
        {
            D_B_3_pd.time = FrameToTime(2471);
            D_B_3_pd.Evaluate();
        }
        else if (D_B_3_started && D_B_3_t3_started && D_B_3_t3_played && !D_B_3_t4_started && !D_B_3_t4_played && !D_B_3_played)
        {
            D_B_3_pd.Play();
            D_B_3_t4_started = true;
        }
        else if (D_B_3_started && D_B_3_t4_started && !D_B_3_t4_played && !D_B_3_t5_started && !D_B_3_t5_played && !D_B_3_played)
        {
            D_B_3_pd.time = FrameToTime(3255);
            D_B_3_pd.Evaluate();
        }
        else if (D_B_3_started && D_B_3_t4_started && D_B_3_t4_played && !D_B_3_t5_started && !D_B_3_t5_played && !D_B_3_played)
        {
            D_B_3_pd.Play();
            D_B_3_t5_started = true;
        }
        else if (D_B_3_started && D_B_3_t5_started && !D_B_3_t5_played && !D_B_3_t6_started && !D_B_3_t6_played && !D_B_3_t7_started && !D_B_3_played)
        {
            D_B_3_pd.time = FrameToTime(3961);
            D_B_3_pd.Evaluate();
        }
        else if (D_B_3_started && D_B_3_t5_started && D_B_3_t5_played && !D_B_3_t6_started && !D_B_3_t6_played && !D_B_3_t7_started && !D_B_3_played)
        {
            D_B_3_pd.Play();
            D_B_3_t6_started = true;
        }
        else if (D_B_3_started && D_B_3_t6_started && !D_B_3_t6_played && !D_B_3_t7_started && !D_B_3_played)
        {
            D_B_3_pd.time = FrameToTime(4768);
            D_B_3_pd.Evaluate();
        }
        else if (D_B_3_started && D_B_3_t6_started && D_B_3_t6_played && !D_B_3_t7_started && !D_B_3_played)
        {
            D_B_3_pd.Play();
            D_B_3_t7_started = true;
        }
        else if (D_B_3_started && D_B_3_t7_started && !D_B_3_played)
        {
            D_B_3_pd.time = D_B_3_pd.duration;
            D_B_3_pd.Evaluate();
        }
    }
    public void play_D_B_4()
    {
        //D_B_4     881  1555  2300  2897  3922  4613 

        if (D_B_4_started && !D_B_4_t1_played && !D_B_4_t2_started && !D_B_4_t2_played && !D_B_4_played)
        {
            D_B_4_pd.time = FrameToTime(881);
            D_B_4_pd.Evaluate();
        }
        else if (D_B_4_started && D_B_4_t1_played && !D_B_4_t2_started && !D_B_4_t2_played && !D_B_4_played)
        {
            D_B_4_pd.Play();
            D_B_4_t2_started = true;
        }
        else if (D_B_4_started && D_B_4_t2_started && !D_B_4_t2_played && !D_B_4_t3_started && !D_B_4_t3_played && !D_B_4_played)
        {
            D_B_4_pd.time = FrameToTime(1555);
            D_B_4_pd.Evaluate();
        }
        else if (D_B_4_started && D_B_4_t2_started && D_B_4_t2_played && !D_B_4_t3_started && !D_B_4_t3_played && !D_B_4_played)
        {
            D_B_4_pd.Play();
            D_B_4_t3_started = true;
        }
        else if (D_B_4_started && D_B_4_t3_started && !D_B_4_t3_played && !D_B_4_t4_started && !D_B_4_t4_played && !D_B_4_played)
        {
            Debug.Log("1");
            D_B_4_pd.time = FrameToTime(2300);
            D_B_4_pd.Evaluate();
        }
        else if (D_B_4_started && D_B_4_t3_started && D_B_4_t3_played && !D_B_4_t4_started && !D_B_4_t4_played && !D_B_4_played)
        {
            Debug.Log("2");
            D_B_4_pd.Play();
            D_B_4_t4_started = true;
        }
        else if (D_B_4_started && D_B_4_t4_started && !D_B_4_t4_played && !D_B_4_t5_started && !D_B_4_t5_played && !D_B_4_played)
        {
            D_B_4_pd.time = FrameToTime(2897);
            D_B_4_pd.Evaluate();
        }
        else if (D_B_4_started && D_B_4_t4_started && D_B_4_t4_played && !D_B_4_t5_started && !D_B_4_t5_played && !D_B_4_played)
        {
            D_B_4_pd.Play();
            D_B_4_t5_started = true;
        }
        else if (D_B_4_started && D_B_4_t5_started && !D_B_4_t5_played && !D_B_4_t6_started && !D_B_4_t6_played && !D_B_4_t7_started && !D_B_4_played)
        {
            D_B_4_pd.time = FrameToTime(3922);
            D_B_4_pd.Evaluate();
        }
        else if (D_B_4_started && D_B_4_t5_started && D_B_4_t5_played && !D_B_4_t6_started && !D_B_4_t6_played && !D_B_4_t7_started && !D_B_4_played)
        {
            D_B_4_pd.Play();
            D_B_4_t6_started = true;
        }
        else if (D_B_4_started && D_B_4_t6_started && !D_B_4_t6_played && !D_B_4_t7_started && !D_B_4_played)
        {
            D_B_4_pd.time = FrameToTime(4613);
            D_B_4_pd.Evaluate();
        }
        else if (D_B_4_started && D_B_4_t6_started && D_B_4_t6_played && !D_B_4_t7_started && !D_B_4_played)
        {
            D_B_4_pd.Play();
            D_B_4_t7_started = true;
        }
        else if (D_B_4_started && D_B_4_t7_started && !D_B_4_played)
        {
            D_B_4_pd.time = D_B_4_pd.duration;
            D_B_4_pd.Evaluate();
        }

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


    public void charaMoveAndAnimationLogic()
    {
        //Debug.Log("charaMoveAndAnimationLogic!");
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("characters go!");
            if (is_horizontal_line_1_connected == true && is_horizontal_line_2_connected == false) // ルート２　
            {
                charaMoveRoute2();
            }
            else if (is_horizontal_line_1_connected == true && is_horizontal_line_2_connected == true) // ルート４　
            {
                charaMoveRoute4();
            }
            else if (is_horizontal_line_1_connected == false && is_horizontal_line_2_connected == true) // ルート３　
            {
                charaMoveRoute3();
            }
            else if (is_horizontal_line_1_connected == false && is_horizontal_line_2_connected == false) // ルート１　
            {
                charaMoveRoute1();
            }
        }
    }
    /*
        0        1
        |        |
        2        3
        |        |
        4--5--6--7
        |        |
        8        9
        |        |
        10-11-12-13
        |        |
        14       15
     */

    // currentMovementIndexは【 0 】から始まる!! 【 1 】から書いたらキャラクターは動かない！！
    private void charaMoveRoute1()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });
                break;

            case 1:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 3, 3 });
                nextIcon.SetActive(false);
                h1_xue_dog.SetActive(true);
                h1_xue_dog_started = true;
                currentGameMode = GameMode.TextPlaying;
                dog_road_pd.Play();
                break;

            case 2:
                StartMovement(new List<int> { 2, 4 }, new List<int> { 3, 7 });
                h1_xue_dog.SetActive(false);
                nextIcon.SetActive(true); 
                break;

            case 3:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 7, 7 });
                nextIcon.SetActive(false);
                b_xue_1_t1.SetActive(true);
                b_xue_1_t1_started = true;
                can_line_1_changed = false;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 4:
                StartMovement(new List<int> { 4, 8 }, new List<int> { 7, 9 });
                b_hunter_1_t2.SetActive(false);
                nextIcon.SetActive(true);
                questionMark5.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 9, 9 });
                questionMark5.SetActive(false);
                nextIcon.SetActive(false);
                h2_hunter_t1.SetActive(true);
                h2_hunter_t1_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 6:
                StartMovement(new List<int> { 8, 10 }, new List<int> { 9, 13 });
                h2_hunter_t2.SetActive(false);
                nextIcon.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 13, 13 });
                nextIcon.SetActive(false);
                c_xue_1_t1.SetActive(true);
                c_xue_1_t1_started = true;
                can_line_2_changed = false;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 8:
                StartMovement(new List<int> { 10, 14 }, new List<int> { 13, 13 });
                nextIcon.SetActive(true);
                questionMark3.SetActive(true);
                c_hunter_1_t2.SetActive(false);
                break;

            case 9:
                StartMovement(new List<int> { 14, 14 }, new List<int> { 13, 13 });
                nextIcon.SetActive(false);
                questionMark3.SetActive(false);
                d1_xue.SetActive(true);
                d1_xue_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 10:
                StartMovement(new List<int> { 14, 14 }, new List<int> { 13, 15 });
                d1_xue.SetActive(false);
                nextIcon.SetActive(true);
                questionMark4.SetActive(false);
                break;

            case 11:
                StartMovement(new List<int> { 14, 14 }, new List<int> { 15, 15 });
                nextIcon.SetActive(false);
                questionMark4.SetActive(true);
                d1_hunter.SetActive(true);
                d1_hunter_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;
        }
    }

    private void charaMoveRoute2()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });
                break;

            case 1:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 3, 3 });
                nextIcon.SetActive(false);
                h1_xue_dog.SetActive(true);
                h1_xue_dog_started = true;
                currentGameMode = GameMode.TextPlaying;
                dog_road_pd.Play();
                break;

            case 2:
                StartMovement(new List<int> { 2, 4 }, new List<int> { 3, 7 });
                h1_xue_dog.SetActive(false);
                nextIcon.SetActive(true);
                break;

            case 3:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 7, 7 });
                nextIcon.SetActive(false);
                b_xue_2a.SetActive(true);
                b_xue_2a_started = true;
                can_line_1_changed = false;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 4:
                StartMovement(new List<int> { 4, 5 }, new List<int> { 7, 6 });
                b_hunter_2a_t2.SetActive(false);
                questionMark1.SetActive(true);
                nextIcon.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 6, 6 });
                questionMark1.SetActive(false);
                nextIcon.SetActive(false);
                D_A_24.SetActive(true);
                D_A_24_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 6:
                dog_with_chinaGirl.SetActive(true);
                D_A_24.SetActive(false);
                StartMovement(new List<int> { 5, 6, 7 }, new List<int> { 6, 5, 4 });
                nextIcon.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 4, 4 });
                nextIcon.SetActive(false);
                b_hunter_2b.SetActive(true);
                b_hunter_2b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 8:
                b_xue_2b_t2.SetActive(false);
                StartMovement(new List<int> { 7, 9 }, new List<int> { 4, 8 });
                questionMark5.SetActive(true);
                nextIcon.SetActive(true);

                break;

            case 9:
                questionMark5.SetActive(false);
                nextIcon.SetActive(false);
                StartMovement(new List<int> { 9, 9 }, new List<int> { 8, 8 });
                h2_xue.SetActive(true);
                h2_xue_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 10:
                h2_xue.SetActive(false);
                StartMovement(new List<int> { 9, 13 }, new List<int> { 8, 10 });
                nextIcon.SetActive(true);
                break;

            case 11:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 10, 10 });
                nextIcon.SetActive(false);
                c_hunter_2_t1.SetActive(true);
                c_hunter_2_t1_started = true;
                can_line_2_changed = false;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 12:
                c_xue_2_t2.SetActive(false);
                StartMovement(new List<int> { 13, 13 }, new List<int> { 10, 14 });
                questionMark3.SetActive(true);
                nextIcon.SetActive(true);
                break;

            case 13:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 14, 14 });
                questionMark3.SetActive(false);
                nextIcon.SetActive(false);
                d23_hunter.SetActive(true);
                d23_hunter_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 14:
                StartMovement(new List<int> { 13, 15 }, new List<int> { 14, 14 });
                d23_hunter.SetActive(false);
                questionMark3.SetActive(true);
                nextIcon.SetActive(true);
                break;

            case 15:
                StartMovement(new List<int> { 15, 15 }, new List<int> { 14, 14 });
                questionMark3.SetActive(false);
                nextIcon.SetActive(false);
                d23_xue.SetActive(true);
                d23_xue_started = true;
                currentGameMode = GameMode.TextPlaying;
                toCutWari = true;
                break;
        }
    }

    private void charaMoveRoute3()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });
                break;

            case 1:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 3, 3 });
                nextIcon.SetActive(false);
                h1_xue_dog.SetActive(true);
                h1_xue_dog_started = true;
                currentGameMode = GameMode.TextPlaying;
                dog_road_pd.Play();
                break;

            case 2:
                StartMovement(new List<int> { 2, 4 }, new List<int> { 3, 7 });
                h1_xue_dog.SetActive(false);
                nextIcon.SetActive(true);
                break;

            case 3:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 7, 7 });
                nextIcon.SetActive(false);
                b_xue_1_t1.SetActive(true);
                b_xue_1_t1_started = true;
                can_line_1_changed = false;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 4:
                StartMovement(new List<int> { 4, 8 }, new List<int> { 7, 9 });
                b_hunter_1_t2.SetActive(false);
                nextIcon.SetActive(true);
                questionMark5.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 8, 8 }, new List<int> { 9, 9 });
                questionMark5.SetActive(false);
                nextIcon.SetActive(false);
                h2_hunter_t1.SetActive(true);
                h2_hunter_t1_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 6:
                StartMovement(new List<int> { 8, 10 }, new List<int> { 9, 13 });
                dog_with_chinaGirl.SetActive(true);
                h2_hunter_t2.SetActive(false);
                nextIcon.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 13, 13 });
                nextIcon.SetActive(false);
                can_line_2_changed = false;
                c_xue_3a_t1.SetActive(true);
                c_xue_3a_t1_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 8:
                c_hunter_3a_t2.SetActive(false);
                StartMovement(new List<int> { 10, 11 }, new List<int> { 13, 12 });
                questionMark2.SetActive(true);
                nextIcon.SetActive(true);
                break;

            case 9:
                StartMovement(new List<int> { 11, 11 }, new List<int> { 12, 12 });
                questionMark2.SetActive(false);
                nextIcon.SetActive(false);
                D_B_3.SetActive(true);
                D_B_3_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 10:
                D_B_3.SetActive(false);
                dog_with_hunter_road.SetActive(true);
                dog_with_chinaGirl.SetActive(false);
                StartMovement(new List<int> { 11, 12, 13 }, new List<int> { 12, 11, 10 });
                nextIcon.SetActive(true);
                break;

            case 11:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 10, 10 });
                nextIcon.SetActive(false);
                c_hunter_3b_t1.SetActive(true);
                c_hunter_3b_t1_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 12:
                c_xue_3b.SetActive(false);
                StartMovement(new List<int> { 13, 13 }, new List<int> { 10, 14 });
                nextIcon.SetActive(true);
                questionMark3.SetActive(true);
                break;

            case 13:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 14, 14 });
                nextIcon.SetActive(false);
                questionMark3.SetActive(false);
                d23_hunter.SetActive(true);
                d23_hunter_started = true;
                dog_with_hunter.SetActive(true);
                currentGameMode = GameMode.TextPlaying;
                break;

            case 14:
                d23_hunter.SetActive(false);
                StartMovement(new List<int> { 13, 15 }, new List<int> { 14, 14 });
                nextIcon.SetActive(true);
                questionMark4.SetActive(true);
                break;

            case 15:
                StartMovement(new List<int> { 15, 15 }, new List<int> { 14, 14 });
                nextIcon.SetActive(false);
                questionMark4.SetActive(false);
                d23_xue.SetActive(true);
                d23_xue_started = true;
                currentGameMode = GameMode.TextPlaying;
                toNextSceneOK = true;
                break;
        }
    }
        

    private void charaMoveRoute4()
    {
        switch (currentMovementIndex)
        {
            case 0:
                StartMovement(new List<int> { 0, 2 }, new List<int> { 1, 3 });
                break;

            case 1:
                StartMovement(new List<int> { 2, 2 }, new List<int> { 3, 3 });
                nextIcon.SetActive(false);
                h1_xue_dog.SetActive(true);
                h1_xue_dog_started = true;
                currentGameMode = GameMode.TextPlaying;
                dog_road_pd.Play();
                break;

            case 2:
                StartMovement(new List<int> { 2, 4 }, new List<int> { 3, 7 });
                h1_xue_dog.SetActive(false);
                nextIcon.SetActive(true);
                break;

            case 3:
                StartMovement(new List<int> { 4, 4 }, new List<int> { 7, 7 });
                nextIcon.SetActive(false);
                b_xue_2a.SetActive(true);
                b_xue_2a_started = true;
                can_line_1_changed = false;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 4:
                StartMovement(new List<int> { 4, 5 }, new List<int> { 7, 6 });
                b_hunter_2a_t2.SetActive(false);
                questionMark1.SetActive(true);
                nextIcon.SetActive(true);
                break;

            case 5:
                StartMovement(new List<int> { 5, 5 }, new List<int> { 6, 6 });
                questionMark1.SetActive(false);
                nextIcon.SetActive(false);
                D_A_24.SetActive(true);
                D_A_24_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 6:
                dog_with_chinaGirl.SetActive(true);
                D_A_24.SetActive(false);
                StartMovement(new List<int> { 5, 6, 7 }, new List<int> { 6, 5, 4 });
                nextIcon.SetActive(true);
                break;

            case 7:
                StartMovement(new List<int> { 7, 7 }, new List<int> { 4, 4 });
                nextIcon.SetActive(false);
                b_hunter_2b.SetActive(true);
                b_hunter_2b_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 8:
                b_xue_2b_t2.SetActive(false);
                StartMovement(new List<int> { 7, 9 }, new List<int> { 4, 8 });
                questionMark5.SetActive(true);
                nextIcon.SetActive(true);

                break;

            case 9:
                questionMark5.SetActive(false);
                nextIcon.SetActive(false);
                StartMovement(new List<int> { 9, 9 }, new List<int> { 8, 8 });
                h2_xue.SetActive(true);
                h2_xue_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 10:
                h2_xue.SetActive(false);
                StartMovement(new List<int> { 9, 13 }, new List<int> { 8, 10 });
                nextIcon.SetActive(true);
                break;

            case 11:
                StartMovement(new List<int> { 13, 13 }, new List<int> { 10, 10 });
                nextIcon.SetActive(false);
                c_hunter_4a_t1.SetActive(true);
                c_hunter_4a_t1_started = true;
                can_line_2_changed = false;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 12:
                c_xue_4a_t2.SetActive(false);
                StartMovement(new List<int> { 13, 12 }, new List<int> { 10, 11 });
                questionMark2.SetActive(true);
                nextIcon.SetActive(true);
                break;

            case 13:
                StartMovement(new List<int> { 12, 12 }, new List<int> { 11, 11 });
                questionMark2.SetActive(false);
                nextIcon.SetActive(false);
                D_B_4.SetActive(true);
                D_B_4_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 14:
                D_B_4.SetActive(false);
                StartMovement(new List<int> { 12, 11, 10 }, new List<int> { 11, 12, 13 });
                nextIcon.SetActive(true);
                dog_with_chinaGirl.SetActive(false);
                dog_with_hunter_road.SetActive(true);
                break;

            case 15:
                StartMovement(new List<int> { 10, 10 }, new List<int> { 13, 13 });
                nextIcon.SetActive(false);
                c_xue_4b_t1.SetActive(true);
                c_xue_4b_t1_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 16:
                c_hunter_4b.SetActive(false);
                StartMovement(new List<int> { 10, 14 }, new List<int> { 13, 13 });
                questionMark3.SetActive(true);
                nextIcon.SetActive(true);

                break;

            case 17:
                StartMovement(new List<int> { 14, 14 }, new List<int> { 13, 13 });
                questionMark3.SetActive(false);
                nextIcon.SetActive(false);
                d4_xue.SetActive(true);
                d4_xue_pd.Play();
                d4_xue_started = true;
                currentGameMode = GameMode.TextPlaying;
                break;

            case 18:
                d4_xue.SetActive(false);
                StartMovement(new List<int> { 14, 14 }, new List<int> { 13, 15 });
                questionMark4.SetActive(true);
                nextIcon.SetActive(true);
                break;

            case 19:
                questionMark4.SetActive(false);
                nextIcon.SetActive(false);
                StartMovement(new List<int> { 14, 14 }, new List<int> { 15, 15 });
                d4_hunter.SetActive(true);
                d4_hunter_started = true;
                toNextSceneOK = true;
                currentGameMode = GameMode.TextPlaying;
                break;
        }
    }

    public void hideText()
    {
        if (!hasFinishedOnce)
        {
            game_start.SetActive(false);
            dog_run_pd.Stop();
            dog_road_pd.Stop();
            target_1.SetActive(false);
            target_1_completed.SetActive(false);
            target_2.SetActive(false);
            target_2_completed.SetActive(false);
            dog_road.SetActive(false);
        }
        else
        {
            dog_with_chinaGirl.SetActive(false);
        }
        dog_with_hunter_road.SetActive(false);
        dog_with_hunter.SetActive(false);
        a_xue_1.SetActive(false);
        a_xue_2.SetActive(false);
        h1_xue_dog.SetActive(false);
        b_xue_1_t1.SetActive(false);
        b_xue_1_t2.SetActive(false);
        b_xue_2a.SetActive(false);
        b_xue_2b_t1.SetActive(false);
        b_xue_2b_t2.SetActive(false);
        h2_xue.SetActive(false);
        c_xue_1_t1.SetActive(false);
        c_xue_1_t2.SetActive(false);
        c_xue_2_t1.SetActive(false);
        c_xue_2_t2.SetActive(false);
        c_xue_3a_t1.SetActive(false);
        c_xue_3a_t2.SetActive(false);
        c_xue_3a_t3.SetActive(false);
        c_xue_3b.SetActive(false);
        c_xue_4a_t1.SetActive(false);
        c_xue_4a_t2.SetActive(false);
        c_xue_4b_t1.SetActive(false);
        c_xue_4b_t2.SetActive(false);
        d1_xue.SetActive(false);
        d4_xue.SetActive(false);
        d23_xue.SetActive(false);
        a_hunter.SetActive(false);
        b_hunter_1_t1.SetActive(false);
        b_hunter_1_t2.SetActive(false);
        b_hunter_2a_t1.SetActive(false);
        b_hunter_2a_t2.SetActive(false);
        b_hunter_2b.SetActive(false);
        h2_hunter_t1.SetActive(false);
        h2_hunter_t2.SetActive(false);
        c_hunter_1_t1.SetActive(false);
        c_hunter_1_t2.SetActive(false);
        c_hunter_2_t1.SetActive(false);
        c_hunter_2_t2.SetActive(false);
        c_hunter_2_t3.SetActive(false);
        c_hunter_2_t4.SetActive(false);
        c_hunter_3a_t1.SetActive(false);
        c_hunter_3a_t2.SetActive(false);
        c_hunter_3b_t1.SetActive(false);
        c_hunter_3b_t2.SetActive(false);
        c_hunter_4a_t1.SetActive(false);
        c_hunter_4a_t2.SetActive(false);
        c_hunter_4b.SetActive(false);
        d1_hunter.SetActive(false);
        d23_hunter.SetActive(false);
        d4_hunter.SetActive(false);
        D_A_24.SetActive(false);
        D_B_3.SetActive(false);
        D_B_4.SetActive(false);
        nextIcon.SetActive(false);
        questionMark1.SetActive(false);
        questionMark2.SetActive(false);
        questionMark3.SetActive(false);
        questionMark4.SetActive(false);
        questionMark5.SetActive(false);
    }

    public void bookEvent()
    {
        game_start_pd.stopped += OnPlayableDirectorStopped;
        target_1_pd.stopped += OnPlayableDirectorStopped;
        target_1_completed_pd.stopped += OnPlayableDirectorStopped;
        target_2_pd.stopped += OnPlayableDirectorStopped;
        target_2_completed_pd.stopped += OnPlayableDirectorStopped;
        jyomaku_name_pd.stopped += OnPlayableDirectorStopped;
        dog_run_pd.stopped += OnPlayableDirectorStopped;
        dog_road_pd.stopped += OnPlayableDirectorStopped;
        a_xue_1_pd.stopped += OnPlayableDirectorStopped;
        a_xue_2_pd.stopped += OnPlayableDirectorStopped;
        h1_xue_dog_pd.stopped += OnPlayableDirectorStopped;
        b_xue_1_t1_pd.stopped += OnPlayableDirectorStopped;
        b_xue_1_t2_pd.stopped += OnPlayableDirectorStopped;
        b_xue_2a_pd.stopped += OnPlayableDirectorStopped;
        b_xue_2b_t1_pd.stopped += OnPlayableDirectorStopped;
        b_xue_2b_t2_pd.stopped += OnPlayableDirectorStopped;
        h2_xue_pd.stopped += OnPlayableDirectorStopped;
        c_xue_1_t1_pd.stopped += OnPlayableDirectorStopped;
        c_xue_1_t2_pd.stopped += OnPlayableDirectorStopped;
        c_xue_2_t1_pd.stopped += OnPlayableDirectorStopped;
        c_xue_2_t2_pd.stopped += OnPlayableDirectorStopped;
        c_xue_3a_t1_pd.stopped += OnPlayableDirectorStopped;
        c_xue_3a_t2_pd.stopped += OnPlayableDirectorStopped;
        c_xue_3a_t3_pd.stopped += OnPlayableDirectorStopped;
        c_xue_3b_pd.stopped += OnPlayableDirectorStopped;
        c_xue_4a_t1_pd.stopped += OnPlayableDirectorStopped;
        c_xue_4a_t2_pd.stopped += OnPlayableDirectorStopped;
        c_xue_4b_t1_pd.stopped += OnPlayableDirectorStopped;
        c_xue_4b_t2_pd.stopped += OnPlayableDirectorStopped;
        d1_xue_pd.stopped += OnPlayableDirectorStopped;
        d4_xue_pd.stopped += OnPlayableDirectorStopped;
        d23_xue_pd.stopped += OnPlayableDirectorStopped;
        a_hunter_pd.stopped += OnPlayableDirectorStopped;
        b_hunter_1_t1_pd.stopped += OnPlayableDirectorStopped;
        b_hunter_1_t2_pd.stopped += OnPlayableDirectorStopped;
        b_hunter_2a_t1_pd.stopped += OnPlayableDirectorStopped;
        b_hunter_2a_t2_pd.stopped += OnPlayableDirectorStopped;
        b_hunter_2b_pd.stopped += OnPlayableDirectorStopped;
        h2_hunter_t1_pd.stopped += OnPlayableDirectorStopped;
        h2_hunter_t2_pd.stopped += OnPlayableDirectorStopped;
        c_hunter_1_t1_pd.stopped += OnPlayableDirectorStopped;
        c_hunter_1_t2_pd.stopped += OnPlayableDirectorStopped;
        c_hunter_2_t1_pd.stopped += OnPlayableDirectorStopped;
        c_hunter_2_t2_pd.stopped += OnPlayableDirectorStopped;
        c_hunter_2_t3_pd.stopped += OnPlayableDirectorStopped;
        c_hunter_2_t4_pd.stopped += OnPlayableDirectorStopped;
        c_hunter_3a_t1_pd.stopped += OnPlayableDirectorStopped;
        c_hunter_3a_t2_pd.stopped += OnPlayableDirectorStopped;
        c_hunter_3b_t1_pd.stopped += OnPlayableDirectorStopped;
        c_hunter_3b_t2_pd.stopped += OnPlayableDirectorStopped;
        c_hunter_4a_t1_pd.stopped += OnPlayableDirectorStopped;
        c_hunter_4a_t2_pd.stopped += OnPlayableDirectorStopped;
        c_hunter_4b_pd.stopped += OnPlayableDirectorStopped;
        d1_hunter_pd.stopped += OnPlayableDirectorStopped;
        d23_hunter_pd.stopped += OnPlayableDirectorStopped;
        d4_hunter_pd.stopped += OnPlayableDirectorStopped;
        D_A_24_pd.stopped += OnPlayableDirectorStopped;
        D_B_3_pd.stopped += OnPlayableDirectorStopped;
        D_B_4_pd.stopped += OnPlayableDirectorStopped;
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

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == jyomaku_name_pd)
        {
            target_1.SetActive(true);
            Debug.Log("jyomaku_name_pd has played.");
        }
        else if (director == target_1_pd)
        {
            target_2.SetActive(true);
            Debug.Log("target_1_pd has played.");
        }
        else if (director == target_2_pd)
        {
            a_xue_1.SetActive(true);
            a_xue_1_started = true;
            Debug.Log("target_2_pd has played.");
        }
        else if(director == game_start_pd)
        {
            currentGameMode = GameMode.PlayerPlaying;
            game_start.SetActive(false);
            game_started = true;
            nextIcon.SetActive(true);
            Debug.Log($"game_started:{game_started}");
            Debug.Log("game_start_pd has played.");
        }
        else if (director == a_xue_1_pd)
        {
            a_xue_1_played = true;
            a_xue_1_started = false;
            Debug.Log("a_xue_1 has played.");
        }
        else if (director == a_xue_2_pd)
        {
            a_xue_2_played = true;
            a_xue_2_started = false;
            Debug.Log("a_xue_2 has played.");
        }
        else if (director == dog_run_pd)
        {
            a_hunter.SetActive(true);
            a_hunter_started = true;
            dog_road.SetActive(true);
            Debug.Log("dog_run_pd has played.");
        }
        else if (director == a_hunter_pd)
        {
            dog_with_chinaGirl.SetActive(false);
            a_hunter_played = true;
            a_hunter_started = false;
            Debug.Log("a_hunter has played.");

        }
        else if (director == h1_xue_dog_pd)
        {
            h1_xue_dog_started = false;
            h1_xue_dog_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("h1_xue_dog_pd has played.");
        }
        else if (director == dog_road_pd)
        {
            dog_road.SetActive(false);
            //dog_with_chinaGirl.SetActive(true);
            Debug.Log("dog_road_pd has played.");
        }
        else if (director == b_xue_1_t1_pd)
        {
            b_xue_1_t1_started = false;
            b_xue_1_t1_played = true;
            Debug.Log("b_xue_1_t1 has played.");
        }
        else if (director == b_xue_1_t2_pd)
        {
            b_xue_1_t2_started = false;
            b_xue_1_t2_played = true;
            Debug.Log("b_xue_1_t2 has played.");
        }
        else if (director == b_hunter_1_t1_pd)
        {
            b_hunter_1_t1_started = false;
            b_hunter_1_t1_played = true;
            Debug.Log("b_hunter_1_t1_pd has played.");
        }
        else if (director == b_hunter_1_t2_pd)
        {
            b_hunter_1_t2_started = false;
            b_hunter_1_t2_played = true; 
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("b_hunter_1_t2_pd has played.");
        }
        else if(director== h2_hunter_t1_pd)
        {
            h2_hunter_t1_started = false;
            h2_hunter_t1_played = true;
            Debug.Log("h2_hunter_t1_pd has played.");
        }
        else if (director == h2_hunter_t2_pd)
        {
            h2_hunter_t2_started = false;
            h2_hunter_t2_played = true; 
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("h2_hunter_t2_pd has played.");
        }
        else if (director == c_xue_1_t1_pd)
        {
            c_xue_1_t1_started = false;
            c_xue_1_t1_played = true;
            Debug.Log("c_xue_1_t1_pd has played.");
        }
        else if (director == c_xue_1_t2_pd)
        {
            c_xue_1_t2_started = false;
            c_xue_1_t2_played = true;
            Debug.Log("c_xue_1_t2_pd has played.");
        }
        else if (director == c_hunter_1_t1_pd)
        {
            c_hunter_1_t1_started = false;
            c_hunter_1_t1_played = true;
            Debug.Log("c_hunter_1_t1_pd has played.");
        }
        else if (director == c_hunter_1_t2_pd)
        {
            c_hunter_1_t2_started = false;
            c_hunter_1_t2_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("c_hunter_1_t2_pd has played.");
        }
        else if (director == d1_xue_pd)
        {
            d1_xue_played = true;
            d1_xue_started = false;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("d1_xue_pd has played.");
        }
        else if (director == d1_hunter_pd)
        {
            d1_hunter_played = true;
            d1_hunter_started = false;
            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("d1_hunter_pd has played.");
        }
        else if (director == d23_hunter_pd)
        {
            d23_hunter_played = true;
            d23_hunter_started = false;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("d23_hunter_pd has played.");
        }
        else if (director == d23_xue_pd)
        {
            d23_xue_played = true;
            d23_xue_started = false;
            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("d23_xue_pd has played.");
        }
        else if (director == b_xue_2a_pd)
        {
            b_xue_2a_played = true;
            b_xue_2a_started = false;
            Debug.Log("b_xue_2a has played.");
        }
        else if (director == b_hunter_2a_t1_pd)
        {
            b_hunter_2a_t1_played = true;
            b_hunter_2a_t1_started = false;
            Debug.Log("b_hunter_2a_t1 has played.");
        }
        else if (director == b_hunter_2a_t2_pd)
        {
            b_hunter_2a_t2_played = true;
            b_hunter_2a_t2_started = false;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("b_hunter_2a_t2 has played.");
        }
        else if (director == b_xue_2b_t1_pd)
        {
            b_xue_2b_t1_played = true;
            b_xue_2b_t1_started = false;
            Debug.Log("b_xue_2b_t1 has played.");
        }
        else if (director == b_xue_2b_t2_pd)
        {
            b_xue_2b_t2_played = true;
            b_xue_2b_t2_started = false;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("b_xue_2b_t2 has played.");
        }
        else if (director == b_hunter_2b_pd)
        {
            b_hunter_2b_played = true;
            b_hunter_2b_started = false;
            Debug.Log("b_hunter_2b has played.");
        }
        else if (director == D_A_24_pd)
        {
            D_A_24_played = true;
            D_A_24_started = false;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("D_A_24 has played.");
        }
        else if (director == h2_xue_pd)
        {
            h2_xue_started = false;
            h2_xue_played = true;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("h2_xue has played.");
        }
        else if (director == c_hunter_2_t1_pd)
        {
            c_hunter_2_t1_played = true;
            c_hunter_2_t1_started = false;
            Debug.Log("c_hunter_2_t1 has played.");
        }
        else if (director == c_hunter_2_t2_pd)
        {
            c_hunter_2_t2_played = true;
            c_hunter_2_t2_started = false;
            Debug.Log("c_hunter_2_t2 has played.");
        }
        else if (director == c_hunter_2_t3_pd)
        {
            c_hunter_2_t3_played = true;
            c_hunter_2_t3_started = false;
            Debug.Log("c_hunter_2_t3 has played.");
        }
        else if (director == c_hunter_2_t4_pd)
        {
            c_hunter_2_t4_played = true;
            c_hunter_2_t4_started = false;
            Debug.Log("c_hunter_2_t4 has played.");
        }
        else if (director == c_xue_2_t1_pd)
        {
            c_xue_2_t1_played = true;
            c_xue_2_t1_started = false;
            Debug.Log("c_xue_2_t1 has played.");
        }
        else if (director == c_xue_2_t2_pd)
        {
            c_xue_2_t2_played = true;
            c_xue_2_t2_started = false;
            Debug.Log("c_xue_2_t2 has played.");
            currentGameMode = GameMode.PlayerPlaying;
        }
        else if (director == c_xue_3a_t1_pd)
        {
            c_xue_3a_t1_started = false;
            c_xue_3a_t1_played = true;
            Debug.Log("c_xue_3a_t1 has played.");
        }
        else if (director == c_xue_3a_t2_pd)
        {
            c_xue_3a_t2_played = true;
            c_xue_3a_t2_started = false;
            Debug.Log("c_xue_3a_t2 has played.");
        }
        else if (director == c_xue_3a_t3_pd)
        {
            c_xue_3a_t3_played = true;
            c_xue_3a_t3_started = false;
            Debug.Log("c_xue_3a_t3 has played.");
        }
        else if (director == c_xue_3b_pd)
        {
            c_xue_3b_played = true;
            c_xue_3b_started = false;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("After c_xue_3b, back to PlayerPlaying.");
            Debug.Log("c_xue_3b has played.");
        }
        else if (director == c_xue_4a_t1_pd)
        {
            c_xue_4a_t1_played = true;
            c_xue_4a_t1_started = false;
            Debug.Log("c_xue_4a_t1 has played.");
        }
        else if (director == c_xue_4a_t2_pd)
        {
            c_xue_4a_t2_played = true;
            c_xue_4a_t2_started = false;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("After c_xue_4a_t2, back to PlayerPlaying.");
            Debug.Log("c_xue_4a_t2 has played.");
        }
        else if (director == c_xue_4b_t1_pd)
        {
            c_xue_4b_t1_played = true;
            c_xue_4b_t1_started = false;
            Debug.Log("c_xue_4b_t1 has played.");
        }
        else if (director == c_xue_4b_t2_pd)
        {
            c_xue_4b_t2_played = true;
            c_xue_4b_t2_started = false;
            Debug.Log("c_xue_4b_t2 has played.");
        }
        else if (director == c_hunter_3a_t1_pd)
        {
            c_hunter_3a_t1_started = false;
            c_hunter_3a_t1_played = true;
            Debug.Log("c_hunter_3a_t1 has played.");
        }
        else if (director == c_hunter_3a_t2_pd)
        {
            c_hunter_3a_t2_started = false;
            c_hunter_3a_t2_played = true;
            Debug.Log("c_hunter_3a_t2 has played.");
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("After c_hunter_3a_t2, back to PlayerPlaying.");
        }
        else if (director == c_hunter_3b_t1_pd)
        {
            c_hunter_3b_t1_started = false;
            c_hunter_3b_t1_played = true;
            Debug.Log("c_hunter_3b_t1 has played.");
        }
        else if (director == c_hunter_3b_t2_pd)
        {
            c_hunter_3b_t2_started = false;
            c_hunter_3b_t2_played = true;
            Debug.Log("c_hunter_3b_t2 has played.");
        }
        else if (director == c_hunter_4a_t1_pd)
        {
            c_hunter_4a_t1_played = true;
            c_hunter_4a_t1_started = false;
            Debug.Log("c_hunter_4a_t1 has played.");
        }
        else if (director == c_hunter_4a_t2_pd)
        {
            c_hunter_4a_t2_played = true;
            c_hunter_4a_t2_started = false;
            Debug.Log("c_hunter_4a_t2 has played.");
        }
        else if (director == c_hunter_4b_pd)
        {
            c_hunter_4b_played = true;
            c_hunter_4b_started = false;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("After c_hunter_4b, back to PlayerPlaying.");
            Debug.Log("c_hunter_4b has played.");
        }
        else if (director == d4_xue_pd)
        {
            d4_xue_played = true;
            d4_xue_started = false;
            Debug.Log("d4_xue has played.");
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("After d4_xue, back to PlayerPlaying.");
        }
        else if (director == d4_hunter_pd)
        {
            d4_hunter_played = true;
            d4_hunter_started = false;
            Debug.Log("d4_hunter has played.");
            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("After c_hunter_4b, WaitForSceneChange.");
        }
        else if (director == D_B_3_pd)
        {
            D_B_3_played = true;
            D_B_3_started = false;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("D_B_3(dialogue_B_3) has played. Back to Playerplaying.");
        }
        else if (director == D_B_4_pd)
        {
            D_B_4_played = true;
            D_B_4_started = false;
            currentGameMode = GameMode.PlayerPlaying;
            Debug.Log("D_B_4(dialogue_B_4) has played. Back to Playerplaying.");
        }
    }

    void OnDestroy()
    {
        game_start_pd.stopped -= OnPlayableDirectorStopped;
        target_1_pd.stopped -= OnPlayableDirectorStopped;
        target_1_completed_pd.stopped -= OnPlayableDirectorStopped;
        target_2_pd.stopped -= OnPlayableDirectorStopped;
        target_2_completed_pd.stopped -= OnPlayableDirectorStopped;
        jyomaku_name_pd.stopped -= OnPlayableDirectorStopped;
        dog_run_pd.stopped -= OnPlayableDirectorStopped;
        dog_road_pd.stopped -= OnPlayableDirectorStopped;
        a_xue_1_pd.stopped -= OnPlayableDirectorStopped;
        a_xue_2_pd.stopped -= OnPlayableDirectorStopped;
        h1_xue_dog_pd.stopped -= OnPlayableDirectorStopped;
        b_xue_1_t1_pd.stopped -= OnPlayableDirectorStopped;
        b_xue_1_t2_pd.stopped -= OnPlayableDirectorStopped;
        b_xue_2a_pd.stopped -= OnPlayableDirectorStopped;
        b_xue_2b_t1_pd.stopped -= OnPlayableDirectorStopped;
        b_xue_2b_t2_pd.stopped -= OnPlayableDirectorStopped;
        h2_xue_pd.stopped -= OnPlayableDirectorStopped;
        c_xue_1_t1_pd.stopped -= OnPlayableDirectorStopped;
        c_xue_1_t2_pd.stopped -= OnPlayableDirectorStopped;
        c_xue_2_t1_pd.stopped -= OnPlayableDirectorStopped;
        c_xue_2_t2_pd.stopped -= OnPlayableDirectorStopped;
        c_xue_3a_t1_pd.stopped -= OnPlayableDirectorStopped;
        c_xue_3a_t2_pd.stopped -= OnPlayableDirectorStopped;
        c_xue_3a_t3_pd.stopped -= OnPlayableDirectorStopped;
        c_xue_3b_pd.stopped -= OnPlayableDirectorStopped;
        c_xue_4a_t1_pd.stopped -= OnPlayableDirectorStopped;
        c_xue_4a_t2_pd.stopped -= OnPlayableDirectorStopped;
        c_xue_4b_t1_pd.stopped -= OnPlayableDirectorStopped;
        c_xue_4b_t2_pd.stopped -= OnPlayableDirectorStopped;
        d1_xue_pd.stopped -= OnPlayableDirectorStopped;
        d4_xue_pd.stopped -= OnPlayableDirectorStopped;
        d23_xue_pd.stopped -= OnPlayableDirectorStopped;
        a_hunter_pd.stopped -= OnPlayableDirectorStopped;
        b_hunter_1_t1_pd.stopped -= OnPlayableDirectorStopped;
        b_hunter_1_t2_pd.stopped -= OnPlayableDirectorStopped;
        b_hunter_2a_t1_pd.stopped -= OnPlayableDirectorStopped;
        b_hunter_2a_t2_pd.stopped -= OnPlayableDirectorStopped;
        b_hunter_2b_pd.stopped -= OnPlayableDirectorStopped;
        h2_hunter_t1_pd.stopped -= OnPlayableDirectorStopped;
        h2_hunter_t2_pd.stopped -= OnPlayableDirectorStopped;
        c_hunter_1_t1_pd.stopped -= OnPlayableDirectorStopped;
        c_hunter_1_t2_pd.stopped -= OnPlayableDirectorStopped;
        c_hunter_2_t1_pd.stopped -= OnPlayableDirectorStopped;
        c_hunter_2_t2_pd.stopped -= OnPlayableDirectorStopped;
        c_hunter_2_t3_pd.stopped -= OnPlayableDirectorStopped;
        c_hunter_2_t4_pd.stopped -= OnPlayableDirectorStopped;
        c_hunter_3a_t1_pd.stopped -= OnPlayableDirectorStopped;
        c_hunter_3a_t2_pd.stopped -= OnPlayableDirectorStopped;
        c_hunter_3b_t1_pd.stopped -= OnPlayableDirectorStopped;
        c_hunter_3b_t2_pd.stopped -= OnPlayableDirectorStopped;
        c_hunter_4a_t1_pd.stopped -= OnPlayableDirectorStopped;
        c_hunter_4a_t2_pd.stopped -= OnPlayableDirectorStopped;
        c_hunter_4b_pd.stopped -= OnPlayableDirectorStopped;
        d1_hunter_pd.stopped -= OnPlayableDirectorStopped;
        d23_hunter_pd.stopped -= OnPlayableDirectorStopped;
        d4_hunter_pd.stopped -= OnPlayableDirectorStopped;
        D_A_24_pd.stopped -= OnPlayableDirectorStopped;
        D_B_3_pd.stopped -= OnPlayableDirectorStopped;
        D_B_4_pd.stopped -= OnPlayableDirectorStopped;

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

        // j0_Kd_M_CharaHover スクリプトをキャラクターに追加する
        J0_kd_m_charaHover j0_Kd_M_CharaHover = character.AddComponent<J0_kd_m_charaHover>();

        // j0_Kd_M_CharaHover スクリプトの情報番号を設定する
        j0_Kd_M_CharaHover.charaInfoNum = charaInfoNum;

        //j0_Kd_M_CharaHoverスクリプトを初期化する
        j0_Kd_M_CharaHover.Initialize(character, charaInfoPrefab, charaInfoPosition);
    }

    public void waitForSceneChange()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (d1_hunter.activeSelf)
            {
                d1_hunter.SetActive(false);
                Debug.Log("d1_hunter再生終わった。シーンの切り替え→");
            }
            else if (d23_xue.activeSelf)
            {
                d23_xue.SetActive(false);
                Debug.Log("d23_xue再生終わった。シーンの切り替え→");
            }
            else if (d4_hunter.activeSelf)
            {
                d4_hunter.SetActive(false);
                Debug.Log("d4_hunter再生終わった。シーンの切り替え→");
            }
            if (toNextSceneOK)
            {
                Debug.Log("load scene:JyoMaku_1.5_YiChi");
                SceneManager.LoadScene("JyoMaku_1.5_YiChi");
            }
            else if (!toNextSceneOK && !toCutWari)
            {
                Debug.Log("load scene:ending1");
                SceneManager.LoadScene("ending1");
            }
            else if (toCutWari)
            {

            }
        }
    }


    public void reTry()
    {
        Debug.Log("リトライ！");
        hideText();
        currentMovementIndex = 0;
        can_line_1_changed = true;
        can_line_2_changed = true;
        is_horizontal_line_1_connected = false;
        is_horizontal_line_2_connected = false;
        toNextSceneOK = false;
        if (line1.activeSelf)
        {
            line1.SetActive(false);
        }
        if (line2.activeSelf)
        {
            line2.SetActive(false);
        }
        character1.transform.position = character1_initial_pos.position;
        character2.transform.position = character2_initial_pos.position;
        a_xue_1_started = false;
        a_xue_1_played = false;
        a_xue_2_started = false;
        a_xue_2_played = false;
        h1_xue_dog_started = false;
        h1_xue_dog_played = false;
        b_xue_1_t1_started = false;
        b_xue_1_t1_played = false;
        b_xue_1_t2_started = false;
        b_xue_1_t2_played = false;
        b_xue_2a_started = false;
        b_xue_2a_played = false;
        b_xue_2b_t1_started = false;
        b_xue_2b_t1_played = false;
        b_xue_2b_t2_started = false;
        b_xue_2b_t2_played = false;
        h2_xue_started = false;
        h2_xue_t1_played = false;
        h2_xue_t2_played = false;
        h2_xue_t3_played = false;
        h2_xue_t2_started = false;
        h2_xue_t3_started = false;
        h2_xue_t4_started = false;
        h2_xue_played = false;
        c_xue_1_t1_started = false;
        c_xue_1_t1_played = false;
        c_xue_1_t2_started = false;
        c_xue_1_t2_played = false;
        c_xue_2_t1_started = false;
        c_xue_2_t1_played = false;
        c_xue_2_t2_started = false;
        c_xue_2_t2_played = false;
        c_xue_3a_t1_started = false;
        c_xue_3a_t1_played = false;
        c_xue_3a_t2_started = false;
        c_xue_3a_t2_played = false;
        c_xue_3a_t3_started = false;
        c_xue_3a_t3_played = false;
        c_xue_3b_started = false;
        c_xue_3b_played = false;
        c_xue_4a_t1_started = false;
        c_xue_4a_t1_played = false;
        c_xue_4a_t2_started = false;
        c_xue_4a_t2_played = false;
        c_xue_4b_t1_started = false;
        c_xue_4b_t1_played = false;
        c_xue_4b_t2_started = false;
        c_xue_4b_t2_played = false;
        d1_xue_started = false;
        d1_xue_t1_played = false;
        d1_xue_t2_started = false;
        d1_xue_played = false;
        d4_xue_started = false;
        d4_xue_t1_played = false;
        d4_xue_t2_played = false;
        d4_xue_t3_played = false;
        d4_xue_t4_played = false;
        d4_xue_t5_played = false;
        d4_xue_t2_started = false;
        d4_xue_t3_started = false;
        d4_xue_t4_started = false;
        d4_xue_t5_started = false;
        d4_xue_t6_started = false;
        d4_xue_played = false;
        d23_xue_started = false;
        d23_xue_t1_played = false;
        d23_xue_t2_played = false;
        d23_xue_t3_played = false;
        d23_xue_played = false;
        d23_xue_t2_started = false;
        d23_xue_t3_started = false;
        d23_xue_t4_started = false;
        a_hunter_started = false;
        a_hunter_played = false;
        b_hunter_1_t1_started = false;
        b_hunter_1_t1_played = false;
        b_hunter_1_t2_started = false;
        b_hunter_1_t2_played = false;
        b_hunter_2a_t1_started = false;
        b_hunter_2a_t1_played = false;
        b_hunter_2a_t2_started = false;
        b_hunter_2a_t2_played = false;
        b_hunter_2b_started = false;
        b_hunter_2b_played = false;
        h2_hunter_t1_started = false;
        h2_hunter_t1_played = false;
        h2_hunter_t2_started = false;
        h2_hunter_t2_played = false;
        c_hunter_1_t1_started = false;
        c_hunter_1_t1_played = false;
        c_hunter_1_t2_started = false;
        c_hunter_1_t2_played = false;
        c_hunter_2_t1_started = false;
        c_hunter_2_t1_played = false;
        c_hunter_2_t2_started = false;
        c_hunter_2_t2_played = false;
        c_hunter_2_t3_started = false;
        c_hunter_2_t3_played = false;
        c_hunter_2_t4_started = false;
        c_hunter_2_t4_played = false;
        c_hunter_3a_t1_started = false;
        c_hunter_3a_t1_played = false;
        c_hunter_3a_t2_started = false;
        c_hunter_3a_t2_played = false;
        c_hunter_3b_t1_started = false;
        c_hunter_3b_t1_played = false;
        c_hunter_3b_t2_started = false;
        c_hunter_3b_t2_played = false;
        c_hunter_4a_t1_started = false;
        c_hunter_4a_t1_played = false;
        c_hunter_4a_t2_started = false;
        c_hunter_4a_t2_played = false;
        c_hunter_4b_started = false;
        c_hunter_4b_played = false;
        d1_hunter_started = false;
        d1_hunter_played = false;
        d23_hunter_started = false;
        d23_hunter_t1_played = false;
        d23_hunter_t2_played = false;
        d23_hunter_t3_played = false;
        d23_hunter_played = false;
        d23_hunter_t4_started = false;
        d23_hunter_t2_started = false;
        d23_hunter_t3_started = false;
        d4_hunter_started = false;
        d4_hunter_played = false;
        D_A_24_started = false;
        D_A_24_t1_played = false;
        D_A_24_t2_started = false;
        D_A_24_played = false;
        D_B_3_started = false;
        D_B_3_t1_played = false;
        D_B_3_t2_played = false;
        D_B_3_t3_played = false;
        D_B_3_t4_played = false;
        D_B_3_t5_played = false;
        D_B_3_t6_played = false;
        D_B_3_t2_started = false;
        D_B_3_t3_started = false;
        D_B_3_t4_started = false;
        D_B_3_t5_started = false;
        D_B_3_t6_started = false;
        D_B_3_t7_started = false;
        D_B_3_played = false;
        D_B_4_started = false;
        D_B_4_t1_played = false;
        D_B_4_t2_played = false;
        D_B_4_t3_played = false;
        D_B_4_t4_played = false;
        D_B_4_t5_played = false;
        D_B_4_t6_played = false;
        D_B_4_t2_started = false;
        D_B_4_t3_started = false;
        D_B_4_t4_started = false;
        D_B_4_t5_started = false;
        D_B_4_t6_started = false;
        D_B_4_played = false;
        toCutWari = false;
        currentGameMode = GameMode.PlayerPlaying;
    }
    public void toMainScene()
    {
        //Debug.Log("LoadScene:TitleScene");
        //SceneManager.LoadScene("TitleScene");
        Debug.Log("LoadScene:M");
        SceneManager.LoadScene("M");
    }

    public void quitGame()
    {
        Application.Quit(); // ゲームを閉じる
    }

}
