using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class JyoMakuToAct1 : MonoBehaviour
{
    public PlayableDirector j2_starting;

    public bool canToNextStage = false;

    public GameObject character1;
    public GameObject character2;
    public GameObject charaInfo;

    // Start is called before the first frame update
    void Start()
    {
        CreateHoverAreaCharacter(character1);
        CreateHoverAreaCharacter(character2);

        if (j2_starting != null)
        {
            j2_starting.stopped += OnPlayableDirectorStopped;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canToNextStage)
        {
            SceneManager.LoadScene("Act_1_1");
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == j2_starting)
        {
            canToNextStage = true;

            Debug.Log("canToNextStage:" + canToNextStage);
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
        J_A1_1_hoverArea _A1_1_HoverArea = character.AddComponent<J_A1_1_hoverArea>();

        // A1_25_charaHoverAreaScriptスクリプトを初期化する
        _A1_1_HoverArea.Initialize(character, charaInfo, charaInfoPosition);
    }

    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        if (j2_starting != null)
        {
            j2_starting.stopped -= OnPlayableDirectorStopped;
        }
    }
}
