using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class TimelineController : MonoBehaviour
{
    public GameObject introduceMessage; // 歓迎メッセージのGameObject
    public PlayableDirector introduceM_pd;
    private bool introduceM_pd_start = true;
    private bool introduceM_pd_played = false;

    public GameObject firstActMessage; // 序幕メッセージのGameObject
    public PlayableDirector firstActPlayableDirector;  // 序幕メッセージのPlayableDirector
    private bool fstAM_pd_start = false;
    private bool fstAM_pd_played = false;

    private enum GameMode
    {
        TextPlaying,
        WaitForSceneChange
    }

    private GameMode currentGameMode = GameMode.TextPlaying;  // デフォルトゲームモードはTextPlaying


    void Start()
    {
        firstActMessage.SetActive(false);
        // PlayableDirectorがnullでないことを確認し、再生完了イベントをサブスクライブ
        introduceM_pd.stopped += OnPlayableDirectorStopped;
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
        // Enterキーが押されたかどうかをチェック修正→マウスの左クリック
        if (Input.GetMouseButtonDown(0))  // Input.GetKeyDown(KeyCode.Return)
        {
            switch (currentGameMode)
            {
                case GameMode.TextPlaying:

                    if (introduceM_pd_start && !introduceM_pd_played && !fstAM_pd_played)
                    {
                        introduceM_pd.time = introduceM_pd.duration;
                        introduceM_pd.Evaluate();
                        introduceM_pd_start = false;
                        introduceM_pd_played = true;
                    }
                    else if (!introduceM_pd_start && introduceM_pd_played && !fstAM_pd_start && !fstAM_pd_played)
                    {
                        introduceMessage.SetActive(false); // デフォルトの歓迎メッセージを非表示にする

                        firstActMessage.SetActive(true);  // 序幕メッセージを表示する
                        fstAM_pd_start = true;
                    }
                    else if (fstAM_pd_start )
                    {
                        Debug.Log("stop!");
                        firstActPlayableDirector.time = firstActPlayableDirector.duration;
                        firstActPlayableDirector.Evaluate();

                        fstAM_pd_start = false;
                        fstAM_pd_played = true;
                    }
                    else if(!fstAM_pd_start && fstAM_pd_played)
                    {
                        SceneManager.LoadScene("JyoMaku_before_0");
                    }

                    break;

                case GameMode.WaitForSceneChange:
                    // シーンを切り替える
                    SceneManager.LoadScene("JyoMaku_before_0");
                    break;
            }
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == introduceM_pd)
        {
            introduceM_pd_played = true;
            introduceM_pd_start = false;
            Debug.Log("introduceM_pd playback completed.");
        }
        if (director == firstActPlayableDirector)
        {
            fstAM_pd_played = true;
            fstAM_pd_start = false;
            currentGameMode = GameMode.WaitForSceneChange;
            Debug.Log("firstActPlayableDirector playback completed.");
        }
    }

    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        if (firstActPlayableDirector != null)
        {
            firstActPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        introduceM_pd.stopped -= OnPlayableDirectorStopped;
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
