using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class characterInfoHoverT5 : MonoBehaviour
{
    
    private GameObject character;  // キャラクターオブジェクトの参照
    private GameObject charaInfoPrefab;  // キャラクター情報を表示するプレハブの参照
    private GameObject charaInfoInstance;    // プレハブのインスタンス
    private Vector3 charaInfoPosition;    // キャラクター情報の表示位置
    public int charaInfoNum;    // キャラクター情報の番号


    public Vector2 textSize = new Vector2(4, 6);    // テキストボックスのサイズを設定
    public Vector3 textOffset = new Vector3(0.1f, -1.7f, -0.1f);    // テキストボックスの位置のオフセットを設定

    public TMP_FontAsset dotFont;    // TextMeshPro用のフォントアセット；既に作成済みのTMPフォント

    private Dictionary<int, string[]> characterInfoDict;    // キャラクター情報を保存

    public DrawLineT5 DrawLineT5Script;    // DrawLineT5スクリプトの参照

    // キャラクター情報を設定する
    private static readonly Dictionary<int, string[]> defaultCharacterInfoDict = new Dictionary<int, string[]>
    {
        { 1, new string[] { "騎士", "○ 凶暴な犬を飼っています。", "○ 国王の安全を守るよう命じられています。", "○ 東洋の剣術を軽視しています。" } },
        { 2, new string[] { "猟師", "○ 猟犬が欲しいです。", "○ 行方不明になった娘を探しています。", "○ 思いがけず手に入れた財宝は拒まず、貯めます。" } }
    };

    // Start is called before the first frame update
    void Start()
    {
        if (DrawLineT5Script == null)
        {
            DrawLineT5Script = FindObjectOfType<DrawLineT5>();
        }

        // キャラクター情報を設定
        characterInfoDict = new Dictionary<int, string[]>(defaultCharacterInfoDict);

        // フォントの設定
        dotFont = DrawLineT5Script.dotFont;
    }

    // キャラクター情報表示の初期化メソッド
    public void Initialize(GameObject character, GameObject charaInfoPrefab, Vector3 charaInfoPosition)
    {
        // 渡されたキャラクターや表示位置、プレハブを保持する
        this.character = character;
        this.charaInfoPosition = charaInfoPosition;
        this.charaInfoPrefab = charaInfoPrefab;

        // 初期化のデバッグメッセージを表示
        Debug.Log($"HoverArea initialized with character: {character.name}");
    }

    // マウスがキャラクターの上に入った時の処理
    void OnMouseEnter()
    {
        // キャラクター情報が設定されていない場合はエラーメッセージを表示して処理を終了する
        if (character == null)
        {
            Debug.LogError("Character is not initialized.");
            return;
        }

        // キャラクター情報ディクショナリが初期化されていない場合もエラーメッセージを表示する
        if (characterInfoDict == null)
        {
            Debug.LogError("characterInfoDict is not initialized.");
            return;
        }

        // キャラクター情報のプレハブが存在する場合、インスタンス化して表示する
        if (charaInfoPrefab != null)
        {
            // キャラクター情報のインスタンスを生成する
            charaInfoInstance = Instantiate(charaInfoPrefab, charaInfoPosition, Quaternion.identity);

            // 表示位置やその他の属性を設定
            charaInfoInstance.transform.position = transform.position + new Vector3(0, -2.5f, 0); // 表示位置を調整
            Debug.Log($"CharaInfoPrefab of {character} instantiated at {charaInfoPosition}");

            // キャラクター名を取得してデバッグ表示
            string characterName = character.name;
            Debug.Log("Which character is the player placing the mouse on? --> " + character.name);

            // キャラクター名の最後の文字を番号として解析する
            if (int.TryParse(characterName[characterName.Length - 1].ToString(), out int charaInfoNum))
            {
                // ディクショナリからキャラクター情報を取得
                if (characterInfoDict.TryGetValue(charaInfoNum, out string[] info))
                {
                    // 取得したキャラクター情報をデバッグ表示
                    Debug.Log("Character info found: " + string.Join(", ", info));

                    // // テキストオブジェクトを生成し、情報を表示
                    GameObject textObject = new GameObject("CharacterInfoText");
                    textObject.transform.SetParent(charaInfoInstance.transform, false);
                    RectTransform rectTransform = textObject.AddComponent<RectTransform>();
                    rectTransform.sizeDelta = textSize; // テキストボックスのサイズを設定
                    rectTransform.position = charaInfoInstance.transform.position + textOffset; // テキストボックスの位置を設定

                    TextMeshPro tmpText = textObject.AddComponent<TextMeshPro>();
                    tmpText.font = dotFont; // フォントを設定
                    tmpText.fontSize = 3; // 文字サイズを設定
                    tmpText.lineSpacing = 25.0f; // 行間を設定
                    tmpText.color = new Color32(0x07, 0x8D, 0x21, 0xFF); // color #078D21
                    tmpText.text = string.Join("\n", info);　// テキスト内容を設定
                }
                else
                {
                    // キャラクター番号に対応する情報が無い場合のエラー
                    Debug.LogError($"No character info found for character number {charaInfoNum}");
                }
            }
            else
            {
                // キャラクター名から番号を解析できなかった場合のエラー
                Debug.LogError($"Failed to parse character number from name: {characterName}");
            }
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
