using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class T2TLcontroller : MonoBehaviour
{
    public GameObject startMessage; // ゲームオブジェクト startMessage（開始メッセージ）
    public GameObject storyMessage; // ゲームオブジェクト storyMessage（物語のメッセージ）
    public GameObject endMessage; // ゲームオブジェクト endMessage（エンディングメッセージ）
    public PlayableDirector startMessagePlayableDirector; // startMessageのPlayableDirector
    public PlayableDirector storyMessagePlayableDirector; // storyMessageのPlayableDirector
    public PlayableDirector endMessagePlayableDirector; // endMessageのPlayableDirector

    private bool isStartPlaying = true;  // startMessageが再生中かどうかを示すブール値、初期値はtrue
    private bool isStoryPlaying = false;  // storyMessageが再生中かどうかを示すブール値、初期値はfalse
    private bool isEndPlaying = false;  // endMessageが再生中かどうかを示すブール値、初期値はfalse

    public bool isCharacterMoving = false; // 騎士は動いてるかどうかを示すブール値、初期値はfalse

    // ゲームモードを設定し、ゲームが実行されるとこの3つのモードの間で切り替えが行われます
    private enum GameMode
    {
        StartTextPlaying, // ゲーム開始時のテキストが再生中
        PlayerPlaying, // プレイヤーが操作している状態(具体的に言うと、Enterを押したら物語テキストが再生する部分)
        WaitForSceneChange // 現シーンのゲーム内容が終了し、プレイヤーがEnterを押すのを待って次のシーンに切り替える
    }

    private GameMode currentGameMode = GameMode.StartTextPlaying; // 現シーン開始時にゲームモードをStartTextPlayingに設定


    // Start is called before the first frame update
    void Start()
    {
        //  開始時にstoryMessageとendMessageのGameObjectを非表示にする
        if (storyMessage != null && endMessage != null)
        {
            storyMessage.SetActive(false);
            endMessage.SetActive(false);
        }

        // PlayableDirectorがnullでないことを確認し、再生完了イベントをサブスクライブ
        if (startMessagePlayableDirector != null)
        {
            startMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("startMessagePlayableDirector is not assigned.");
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
        // Enterキーが押されたかどうかをチェック
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (currentGameMode)
            {
                case GameMode.StartTextPlaying:

                    break;

                case GameMode.PlayerPlaying:
                    //  このモードでは、プレイヤーがEnterを押すと、キャラクターが動き、storyとendMessagePlayableDirectorが再生される

                    isCharacterMoving = true; //騎士が動く

                    if (!isStoryPlaying)
                    {
                        if(storyMessage != null)
                        {
                            startMessage.SetActive(false);
                        }

                        // storyMessageのGameObjectをアクティブにする
                        if (storyMessage != null && endMessage != null)
                        {
                            storyMessage.SetActive(true);
                        }

                        if (storyMessagePlayableDirector != null && endMessagePlayableDirector != null)  
                        {
                            storyMessagePlayableDirector.Play();  // storyMessageのPlayableDirectorを再生する
                            isStoryPlaying = true; // 再生中とマークする
                            
                        }
                    }

                    break;

                case GameMode.WaitForSceneChange:
                    // シーンを切り替える
                    SceneManager.LoadScene("Tutorial_3_Scene");
                    break;
            }
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == startMessagePlayableDirector)
        {
            isStartPlaying = false;  // 再生完了とマークする
            currentGameMode = GameMode.PlayerPlaying;  // PlayerPlayingモードに変更する
            Debug.Log("startMessage Timeline playback completed.");
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

    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        if (startMessagePlayableDirector != null)
        {
            startMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
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
