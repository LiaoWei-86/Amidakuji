using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_1_15_hoverChara : MonoBehaviour
{
    private GameObject character;  // キャラクターオブジェクトの参照
    private GameObject charaInfo;  // キャラクター情報を表示するプレハブの参照
    private GameObject charaInfoInstance;    // プレハブのインスタンス
    private Vector3 charaInfoPosition;    // キャラクター情報の表示位置

    public Act_1_15gmController act_1_15GmControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        if (act_1_15GmControllerScript == null)
        {
            act_1_15GmControllerScript = FindObjectOfType<Act_1_15gmController>();
        }
    }

    public void Initialize(GameObject character, GameObject charaInfo, Vector3 charaInfoPosition)
    {
        // 渡されたキャラクターや表示位置、プレハブを保持する
        this.character = character;
        this.charaInfoPosition = charaInfoPosition;
        this.charaInfo = charaInfo;

        // 初期化のデバッグメッセージを表示
        Debug.Log($"HoverArea initialized with character: {character.name}");
    }

    void OnMouseEnter()
    {
        // キャラクター情報が設定されていない場合はエラーメッセージを表示して処理を終了する
        if (character == null)
        {
            Debug.LogError("Character is not initialized.");
            return;
        }

        // キャラクター情報のプレハブが存在する場合、インスタンス化して表示する
        if (charaInfo != null)
        {
            // キャラクター情報のインスタンスを生成する
            charaInfoInstance = Instantiate(charaInfo, charaInfoPosition, Quaternion.identity);

            // 表示位置やその他の属性を設定
            charaInfoInstance.transform.position = transform.position + new Vector3(0, -2.5f, 0); // 表示位置を調整
            Debug.Log($"CharaInfoPrefab of {character.name} instantiated at {charaInfoPosition}");
        }
        else
        {
            // キャラクター名に対応する情報が無い場合のエラー
            Debug.LogError($"No character info found .");
        }
    }

    // マウスがキャラクターから離れたときの処理
    void OnMouseExit()
    {
        // キャラクター情報のインスタンスが存在する場合は破棄する
        if (charaInfoInstance != null)
        {
            Destroy(charaInfoInstance);
            Debug.Log("charaInfoInstance destroyed");

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
