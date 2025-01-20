using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class J0_5_GameController : MonoBehaviour
{
    public PlayableDirector j0_5_starting;
    private bool start = true;

    public bool canToNextStage = false;

    public GameObject character1;
    public GameObject character2;
    public GameObject charaInfo;
    // Start is called before the first frame update
    void Start()
    {
        if(j0_5_starting != null)
        {
            j0_5_starting.stopped += OnPlayableDirectorStopped;
        }

        CreateHoverAreaCharacter(character1);
        CreateHoverAreaCharacter(character2);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(start && !canToNextStage)
            {
                j0_5_starting.time = j0_5_starting.duration;
                j0_5_starting.Evaluate();
                start = false;
                canToNextStage = true;
            }
            else if(!start && canToNextStage)
            {
                SceneManager.LoadScene("JyoMaku_1");
            }
        }

    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if(director == j0_5_starting)
        {
            canToNextStage = true;
            start = false;
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
        J0_5_hoverChara J0_5_hoverAreaScript = character.AddComponent<J0_5_hoverChara>();

        // A1_25_charaHoverAreaScriptスクリプトを初期化する
        J0_5_hoverAreaScript.Initialize(character, charaInfo, charaInfoPosition);
    }

    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        if (j0_5_starting != null)
        {
            j0_5_starting.stopped -= OnPlayableDirectorStopped;
        }
    }
}
