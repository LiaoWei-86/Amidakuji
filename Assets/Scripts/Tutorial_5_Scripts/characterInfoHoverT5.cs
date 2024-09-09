using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class characterInfoHoverT5 : MonoBehaviour
{
    private GameObject character;

    private GameObject charaInfoPrefab;
    private GameObject charaInfoInstance;
    private Vector3 charaInfoPosition;
    public int charaInfoNum;


    public TMP_FontAsset dotFont; // 自定义的TMP字体
    private Dictionary<int, string[]> characterInfoDict; // 存储角色信息

    public void SetCharacterInfo(Dictionary<int, string[]> charaInfoDict)
    {
        this.characterInfoDict = charaInfoDict;
    }

    public DrawLineT5 DrawLineT5Script;

    // Start is called before the first frame update
    void Start()
    {
        if (DrawLineT5Script == null)
        {
            DrawLineT5Script = FindObjectOfType<DrawLineT5>();
        }

        Dictionary<int, string[]> characterInfoDict = new Dictionary<int, string[]>
        {
            { 1, new string[] { "騎士","○ 凶暴な犬を飼っています。", "○ 国王の安全を守るよう命じられています。", "○ 東洋の剣術を軽視してます。" } },
            { 2, new string[] { "猟師", "○ 猟犬が欲しいです。", "○ 行方不明になった娘を探しています。", "○ 思いがけず手に入れた財宝は拒まず、貯めます。" } }
        };

        SetCharacterInfo(characterInfoDict);

        dotFont = DrawLineT5Script.dotFont;
    }



    public void Initialize(GameObject character, GameObject charaInfoPrefab, Vector3 charaInfoPosition)
    {
        this.character = character;
        this.charaInfoPosition = charaInfoPosition;
        this.charaInfoPrefab = charaInfoPrefab;

        Debug.Log($"HoverArea initialized with");
    }

    void OnMouseEnter()
    {
        if (characterInfoDict == null)
        {
            Debug.LogError("characterInfoDict is not initialized.");
            return;
        }

        if (character == null)
        {
            Debug.LogError("Character is not initialized.");
            return;
        }

        if (charaInfoPrefab != null)
        {
            charaInfoInstance = Instantiate(charaInfoPrefab, charaInfoPosition, Quaternion.identity);

            // 设置显示位置和其他属性
            charaInfoInstance.transform.position = transform.position + new Vector3(0, -2.5f, 0); // 调整显示位置
            Debug.Log($"CharaInfoPrefab of {character} instantiated at {charaInfoPosition}");

            string characterName = character.name;
            Debug.Log("Which character does player place mouse on? "+character.name);
            int charaInfoNum = int.Parse(characterName[characterName.Length - 1].ToString());
            if (characterInfoDict.TryGetValue(charaInfoNum, out string[] info))
            {
                Debug.Log("Character info found: " + string.Join(", ", info));

            }
            else
            {
                Debug.LogError($"No character info found for character number {charaInfoNum}");
            }


            foreach (var infoer in info)
            {
                Debug.Log(infoer);
            }
            // 生成文本框
            GameObject textObject = new GameObject("CharacterInfoText");
            textObject.transform.SetParent(charaInfoInstance.transform, false);
            Debug.Log($"charaInfoInstance.transform.position:{charaInfoInstance.transform.position}");

            RectTransform rectTransform = textObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(4, 6); // 设置文本框大小
            rectTransform.position = charaInfoInstance.transform.position+ new Vector3(0.1f, -1.7f, -0.1f); // 设置文本框位置

            Debug.Log($"rectTransform.position :{rectTransform.position }");

            TextMeshPro tmpText = textObject.AddComponent<TextMeshPro>();
            tmpText.font = dotFont; // 设置字体
            tmpText.fontSize = 3; // 字号
            tmpText.lineSpacing = 25.0f; // 行间距
            tmpText.color = new Color32(0x07, 0x8D, 0x21, 0xFF); //color #078D21
            tmpText.text = $"{info[0]}\n{info[1]}\n{info[2]}\n{info[3]}"; // 设置文本内容
        }
    }


    void OnMouseExit()
    {
        if (charaInfoInstance != null)
        {
            Destroy(charaInfoInstance);
            Debug.Log("charaInfoInstance destroyed");
        }
        foreach (Transform child in charaInfoPrefab.transform)
        {
            Destroy(child.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
