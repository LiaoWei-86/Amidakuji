using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Act_1_15gmController : MonoBehaviour
{
    public PlayableDirector Act_1_15starting;

    private bool canToNextStage = false;

    public GameObject character1;
    public GameObject character2;
    public GameObject character3;
    public GameObject charaInfo;

    // Start is called before the first frame update
    void Start()
    {
        CreateHoverAreaCharacter(character1);
        CreateHoverAreaCharacter(character2);
        CreateHoverAreaCharacter(character3);

        if (Act_1_15starting != null)
        {
            Act_1_15starting.stopped += OnPlayableDirectorStopped;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canToNextStage)
        {
            SceneManager.LoadScene("Act_1_2");
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == Act_1_15starting)
        {

            canToNextStage = true;

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
        A_1_15_hoverChara A_1_15_hoverCharaScript = character.AddComponent<A_1_15_hoverChara>();

        // A1_25_charaHoverAreaScriptスクリプトを初期化する
        A_1_15_hoverCharaScript.Initialize(character, charaInfo, charaInfoPosition);
    }
    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        if (Act_1_15starting != null)
        {
            Act_1_15starting.stopped -= OnPlayableDirectorStopped;
        }
    }
}
