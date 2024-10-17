using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineController : MonoBehaviour
{
    public GameObject introduceMessage; // 歓迎メッセージのGameObject
    public GameObject firstActMessage; // 序幕メッセージのGameObject
    public PlayableDirector firstActPlayableDirector;  // 序幕メッセージのPlayableDirector

    private enum GameMode
    {
        TextPlaying,
        WaitForSceneChange
    }

    private GameMode currentGameMode = GameMode.TextPlaying;  // デフォルトゲームモードはTextPlaying
    private bool isFirstActPlaying = false;  // 序幕メッセージが再生中かどうかを示すブール値、初期値はfalse

    void Start()
    {
        // PlayableDirectorがnullでないことを確認し、再生完了イベントをサブスクライブ
        if (firstActPlayableDirector != null)
        {
            firstActPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("PlayableDirector is not assigned.");
        }
    }

    void Update()
    {
        // Enterキーが押されたかどうかをチェック
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (currentGameMode)
            {
                case GameMode.TextPlaying:
                    if (!isFirstActPlaying) // 序幕メッセージが再生中でない場合
                    {
                        if (introduceMessage != null) // 歓迎メッセージがnullでない場合
                        {
                            introduceMessage.SetActive(false); // デフォルトの歓迎メッセージを非表示にする
                        }

                        if (firstActMessage != null)  // 序幕メッセージがnullでない場合
                        {
                            firstActMessage.SetActive(true);  // 序幕メッセージを表示する
                        }

                        // 播放 Timeline
                        if (firstActPlayableDirector != null)  // 序幕メッセージのPlayableDirectorがnullでない場合
                        {
                            firstActPlayableDirector.Play();  // 序幕メッセージのPlayableDirectorを再生する
                            isFirstActPlaying = true;  // 再生中とマークする
                        }
                        else
                        {
                            Debug.LogWarning("PlayableDirector is not assigned.");
                        }
                    }
                    break;

                case GameMode.WaitForSceneChange:
                    // シーンを切り替える
                    SceneManager.LoadScene("JyoMaku_0");
                    break;
            }
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == firstActPlayableDirector)
        {
            isFirstActPlaying = false;  // 再生完了とマークする
            currentGameMode = GameMode.WaitForSceneChange;  // シーン切り替え待ちモードに変更する
            Debug.Log("Timeline playback completed.");
        }
    }

    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        if (firstActPlayableDirector != null)
        {
            firstActPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }

    /*
    【OnPlayableDirectorStopped メソッドの実現原理】
    OnPlayableDirectorStopped メソッドは、PlayableDirector の再生が停止したときに呼び出されるイベントハンドラーです。実現原理は次の通りです：

    イベントサブスクライブ:

    Start メソッドで、PlayableDirector が停止したときに OnPlayableDirectorStopped メソッドが呼び出されるように、firstActPlayableDirector.stopped イベントにサブスクライブしています。
    firstActPlayableDirector.stopped += OnPlayableDirectorStopped;

    イベント発生時の処理:

    OnPlayableDirectorStopped メソッドは、PlayableDirector の再生が停止すると自動的に呼び出されます。
    メソッド内部では、PlayableDirector が firstActPlayableDirector と一致するかどうかを確認します。
    一致する場合、isFirstActPlaying を false に設定し、再生が完了したことを示します。
    次に、currentGameMode を WaitForSceneChange に変更し、シーン切り替え待ちモードにします。
    最後に、デバッグログを出力して、再生が完了したことを通知します。

    イベントサブスクライブの解除:

    メモリリークを防ぐために、OnDestroy メソッドでイベントの購読を解除します。
    firstActPlayableDirector.stopped -= OnPlayableDirectorStopped;
     */
}
