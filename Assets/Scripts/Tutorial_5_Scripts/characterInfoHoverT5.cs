using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class characterInfoHoverT5 : MonoBehaviour
{
    
    private GameObject character;  // キャラクタ�`オブジェクトの歌孚
    private GameObject charaInfoPrefab;  // キャラクタ�`秤�鵑魃輅召垢襯廛譽魯屬硫燐�
    private GameObject charaInfoInstance;    // プレハブのインスタンス
    private Vector3 charaInfoPosition;    // キャラクタ�`秤�鵑留輅称志�
    public int charaInfoNum;    // キャラクタ�`秤�鵑侶�催


    public Vector2 textSize = new Vector2(4, 6);    // テキストボックスのサイズを�O協
    public Vector3 textOffset = new Vector3(0.1f, -1.7f, -0.1f);    // テキストボックスの了崔のオフセットを�O協

    public TMP_FontAsset dotFont;    // TextMeshPro喘のフォントアセット�纂箸没�撹�gみのTMPフォント

    private Dictionary<int, string[]> characterInfoDict;    // キャラクタ�`秤�鵑魃４�

    public DrawLineT5 DrawLineT5Script;    // DrawLineT5スクリプトの歌孚

    // キャラクタ�`秤�鵑鰓O協する
    private static readonly Dictionary<int, string[]> defaultCharacterInfoDict = new Dictionary<int, string[]>
    {
        { 1, new string[] { "�T平", "＄ 俔羽な溌を��っています。", "＄ 忽藍の芦畠を便るよう凋じられています。", "＄ �|剴の���gを�X��しています。" } },
        { 2, new string[] { "�d��", "＄ �d溌が圀しいです。", "＄ 佩圭音苧になった弟を冥しています。", "＄ 房いがけず返に秘れた��右は詳まず、�Aめます。" } }
    };

    // Start is called before the first frame update
    void Start()
    {
        if (DrawLineT5Script == null)
        {
            DrawLineT5Script = FindObjectOfType<DrawLineT5>();
        }

        // キャラクタ�`秤�鵑鰓O協
        characterInfoDict = new Dictionary<int, string[]>(defaultCharacterInfoDict);

        // フォントの�O協
        dotFont = DrawLineT5Script.dotFont;
    }

    // キャラクタ�`秤�鷄輅召粒�豚晒メソッド
    public void Initialize(GameObject character, GameObject charaInfoPrefab, Vector3 charaInfoPosition)
    {
        // 局されたキャラクタ�`や燕幣了崔、プレハブを隠隔する
        this.character = character;
        this.charaInfoPosition = charaInfoPosition;
        this.charaInfoPrefab = charaInfoPrefab;

        // 兜豚晒のデバッグメッセ�`ジを燕幣
        Debug.Log($"HoverArea initialized with character: {character.name}");
    }

    // マウスがキャラクタ�`の貧に秘った�rの�I尖
    void OnMouseEnter()
    {
        // キャラクタ�`秤�鵑��O協されていない��栽はエラ�`メッセ�`ジを燕幣して�I尖を�K阻する
        if (character == null)
        {
            Debug.LogError("Character is not initialized.");
            return;
        }

        // キャラクタ�`秤�鵐妊�クショナリが兜豚晒されていない��栽もエラ�`メッセ�`ジを燕幣する
        if (characterInfoDict == null)
        {
            Debug.LogError("characterInfoDict is not initialized.");
            return;
        }

        // キャラクタ�`秤�鵑離廛譽魯屬�贋壓する��栽、インスタンス晒して燕幣する
        if (charaInfoPrefab != null)
        {
            // キャラクタ�`秤�鵑離ぅ鵐好織鵐垢鯢�撹する
            charaInfoInstance = Instantiate(charaInfoPrefab, charaInfoPosition, Quaternion.identity);

            // 燕幣了崔やその麿の奉來を�O協
            charaInfoInstance.transform.position = transform.position + new Vector3(0, -2.5f, 0); // 燕幣了崔を�{屁
            Debug.Log($"CharaInfoPrefab of {character} instantiated at {charaInfoPosition}");

            // キャラクタ�`兆を函誼してデバッグ燕幣
            string characterName = character.name;
            Debug.Log("Which character is the player placing the mouse on? --> " + character.name);

            // キャラクタ�`兆の恷瘁の猟忖を桑催として盾裂する
            if (int.TryParse(characterName[characterName.Length - 1].ToString(), out int charaInfoNum))
            {
                // ディクショナリからキャラクタ�`秤�鵑鯣ゝ�
                if (characterInfoDict.TryGetValue(charaInfoNum, out string[] info))
                {
                    // 函誼したキャラクタ�`秤�鵑鬟妊丱奪葦輅�
                    Debug.Log("Character info found: " + string.Join(", ", info));

                    // // テキストオブジェクトを伏撹し、秤�鵑魃輅�
                    GameObject textObject = new GameObject("CharacterInfoText");
                    textObject.transform.SetParent(charaInfoInstance.transform, false);
                    RectTransform rectTransform = textObject.AddComponent<RectTransform>();
                    rectTransform.sizeDelta = textSize; // テキストボックスのサイズを�O協
                    rectTransform.position = charaInfoInstance.transform.position + textOffset; // テキストボックスの了崔を�O協

                    TextMeshPro tmpText = textObject.AddComponent<TextMeshPro>();
                    tmpText.font = dotFont; // フォントを�O協
                    tmpText.fontSize = 3; // 猟忖サイズを�O協
                    tmpText.lineSpacing = 25.0f; // 佩�gを�O協
                    tmpText.color = new Color32(0x07, 0x8D, 0x21, 0xFF); // color #078D21
                    tmpText.text = string.Join("\n", info);　// テキスト坪否を�O協
                }
                else
                {
                    // キャラクタ�`桑催に��鬉垢詛��鵑��oい��栽のエラ�`
                    Debug.LogError($"No character info found for character number {charaInfoNum}");
                }
            }
            else
            {
                // キャラクタ�`兆から桑催を盾裂できなかった��栽のエラ�`
                Debug.LogError($"Failed to parse character number from name: {characterName}");
            }
        }
    }

    // マウスがキャラクタ�`から�xれたときの�I尖
    void OnMouseExit()
    {
        // キャラクタ�`秤�鵑離ぅ鵐好織鵐垢�贋壓する��栽は篤��する
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
