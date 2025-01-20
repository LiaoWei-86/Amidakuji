using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class bf_J0gmController : MonoBehaviour
{

    public PlayableDirector starting;
    private bool start = true;
    private bool played = false;

    public GameObject character1;
    public GameObject character2;
    public GameObject charaInfo;

    private enum GameMode
    {
        TextPlaying,
        WaitForSceneChange
    }
    private GameMode currentGameMode = GameMode.TextPlaying;  // デフォルトゲームモードはTextPlaying

    // Start is called before the first frame update
    void Start()
    {
        CreateHoverAreaCharacter(character1);
        CreateHoverAreaCharacter(character2);

        if (starting != null)
        {
            starting.stopped += OnPlayableDirectorStopped;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentGameMode)
        {
            case GameMode.TextPlaying:
                if (Input.GetMouseButtonDown(0))
                {
                    if (start && !played)
                    {
                        starting.time = starting.duration;
                        starting.Evaluate();
                        start = false;
                        played = true;
                        currentGameMode = GameMode.WaitForSceneChange;
                    }
                }

                break;

            case GameMode.WaitForSceneChange:

                // Enterキーが押されたかどうかをチェック
                if (Input.GetMouseButtonDown(0))  // Input.GetKeyDown(KeyCode.Return)
                {
                    SceneManager.LoadScene("JyoMaku_0");
                }

                break;
        }

    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == starting)
        {
            played = true;
            start = false;
            currentGameMode = GameMode.WaitForSceneChange;  // シーン切り替え待ちモードに変更する
            Debug.Log("starting Timeline playback completed.");
        }
    }

    void CreateHoverAreaCharacter(GameObject character)
    {
        // デバッグログを出力して、キャラクターの情報を表示する
        Debug.Log($"CreateHoverArea called with character: {character.name}");

        // キャラクターの位置からオフセットを加えた位置を計算する
        Vector3 charaInfoPosition = character.transform.position + new Vector3(-3, -1, 0);

        // キャラクターに BoxCollider コンポーネントを追加し、ホバーエリアのサイズを設定する
        BoxCollider boxCollider = character.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(1f, 1f, 0.1f);
        boxCollider.isTrigger = true;


        // A1_25_charaHoverAreaScript スクリプトをキャラクターに追加する
        bf_J0_hoverChara bf_hoverAreaScript = character.AddComponent<bf_J0_hoverChara>();

        // A1_25_charaHoverAreaScriptスクリプトを初期化する
        bf_hoverAreaScript.Initialize(character, charaInfo, charaInfoPosition);
    }
    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        if (starting != null)
        {
            starting.stopped -= OnPlayableDirectorStopped;
        }
    }
}
