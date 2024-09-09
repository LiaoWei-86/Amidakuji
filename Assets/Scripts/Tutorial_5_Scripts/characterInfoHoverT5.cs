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


    public TMP_FontAsset dotFont; // �Զ����TMP����
    private Dictionary<int, string[]> characterInfoDict; // �洢��ɫ��Ϣ

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
            { 1, new string[] { "�Tʿ","�� �ױ���Ȯ��äƤ��ޤ���", "�� �����ΰ�ȫ���ؤ�褦�������Ƥ��ޤ���", "�� �|��΄��g���Xҕ���Ƥޤ���" } },
            { 2, new string[] { "�d��", "�� �dȮ���������Ǥ���", "�� �з������ˤʤä����̽���Ƥ��ޤ���", "�� ˼���������֤���줿ؔ���Ͼܤޤ����A��ޤ���" } }
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

            // ������ʾλ�ú���������
            charaInfoInstance.transform.position = transform.position + new Vector3(0, -2.5f, 0); // ������ʾλ��
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
            // �����ı���
            GameObject textObject = new GameObject("CharacterInfoText");
            textObject.transform.SetParent(charaInfoInstance.transform, false);
            Debug.Log($"charaInfoInstance.transform.position:{charaInfoInstance.transform.position}");

            RectTransform rectTransform = textObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(4, 6); // �����ı����С
            rectTransform.position = charaInfoInstance.transform.position+ new Vector3(0.1f, -1.7f, -0.1f); // �����ı���λ��

            Debug.Log($"rectTransform.position :{rectTransform.position }");

            TextMeshPro tmpText = textObject.AddComponent<TextMeshPro>();
            tmpText.font = dotFont; // ��������
            tmpText.fontSize = 3; // �ֺ�
            tmpText.lineSpacing = 25.0f; // �м��
            tmpText.color = new Color32(0x07, 0x8D, 0x21, 0xFF); //color #078D21
            tmpText.text = $"{info[0]}\n{info[1]}\n{info[2]}\n{info[3]}"; // �����ı�����
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
