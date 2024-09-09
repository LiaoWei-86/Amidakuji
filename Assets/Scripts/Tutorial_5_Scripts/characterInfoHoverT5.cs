using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class characterInfoHoverT5 : MonoBehaviour
{
    
    private GameObject character;  // ����饯���`���֥������Ȥβ���
    private GameObject charaInfoPrefab;  // ����饯���`�����ʾ����ץ�ϥ֤β���
    private GameObject charaInfoInstance;    // �ץ�ϥ֤Υ��󥹥���
    private Vector3 charaInfoPosition;    // ����饯���`���α�ʾλ��
    public int charaInfoNum;    // ����饯���`���η���


    public Vector2 textSize = new Vector2(4, 6);    // �ƥ����ȥܥå����Υ��������O��
    public Vector3 textOffset = new Vector3(0.1f, -1.7f, -0.1f);    // �ƥ����ȥܥå�����λ�äΥ��ե��åȤ��O��

    public TMP_FontAsset dotFont;    // TextMeshPro�äΥե���ȥ����åȣ��Ȥ����ɜg�ߤ�TMP�ե����

    private Dictionary<int, string[]> characterInfoDict;    // ����饯���`���򱣴�

    public DrawLineT5 DrawLineT5Script;    // DrawLineT5������ץȤβ���

    // ����饯���`�����O������
    private static readonly Dictionary<int, string[]> defaultCharacterInfoDict = new Dictionary<int, string[]>
    {
        { 1, new string[] { "�Tʿ", "�� �ױ���Ȯ��äƤ��ޤ���", "�� �����ΰ�ȫ���ؤ�褦�������Ƥ��ޤ���", "�� �|��΄��g���Xҕ���Ƥ��ޤ���" } },
        { 2, new string[] { "�d��", "�� �dȮ���������Ǥ���", "�� �з������ˤʤä����̽���Ƥ��ޤ���", "�� ˼���������֤���줿ؔ���Ͼܤޤ����A��ޤ���" } }
    };

    // Start is called before the first frame update
    void Start()
    {
        if (DrawLineT5Script == null)
        {
            DrawLineT5Script = FindObjectOfType<DrawLineT5>();
        }

        // ����饯���`�����O��
        characterInfoDict = new Dictionary<int, string[]>(defaultCharacterInfoDict);

        // �ե���Ȥ��O��
        dotFont = DrawLineT5Script.dotFont;
    }

    // ����饯���`����ʾ�γ��ڻ��᥽�å�
    public void Initialize(GameObject character, GameObject charaInfoPrefab, Vector3 charaInfoPosition)
    {
        // �ɤ��줿����饯���`���ʾλ�á��ץ�ϥ֤򱣳֤���
        this.character = character;
        this.charaInfoPosition = charaInfoPosition;
        this.charaInfoPrefab = charaInfoPrefab;

        // ���ڻ��ΥǥХå���å��`�����ʾ
        Debug.Log($"HoverArea initialized with character: {character.name}");
    }

    // �ޥ���������饯���`���Ϥ���ä��r�΄I��
    void OnMouseEnter()
    {
        // ����饯���`����O������Ƥ��ʤ����Ϥϥ���`��å��`�����ʾ���ƄI���K�ˤ���
        if (character == null)
        {
            Debug.LogError("Character is not initialized.");
            return;
        }

        // ����饯���`���ǥ�������ʥ꤬���ڻ�����Ƥ��ʤ����Ϥ⥨��`��å��`�����ʾ����
        if (characterInfoDict == null)
        {
            Debug.LogError("characterInfoDict is not initialized.");
            return;
        }

        // ����饯���`���Υץ�ϥ֤����ڤ�����ϡ����󥹥��󥹻����Ʊ�ʾ����
        if (charaInfoPrefab != null)
        {
            // ����饯���`���Υ��󥹥��󥹤����ɤ���
            charaInfoInstance = Instantiate(charaInfoPrefab, charaInfoPosition, Quaternion.identity);

            // ��ʾλ�ä䤽���������Ԥ��O��
            charaInfoInstance.transform.position = transform.position + new Vector3(0, -2.5f, 0); // ��ʾλ�ä��{��
            Debug.Log($"CharaInfoPrefab of {character} instantiated at {charaInfoPosition}");

            // ����饯���`����ȡ�ä��ƥǥХå���ʾ
            string characterName = character.name;
            Debug.Log("Which character is the player placing the mouse on? --> " + character.name);

            // ����饯���`������������֤򷬺ŤȤ��ƽ�������
            if (int.TryParse(characterName[characterName.Length - 1].ToString(), out int charaInfoNum))
            {
                // �ǥ�������ʥ꤫�饭��饯���`����ȡ��
                if (characterInfoDict.TryGetValue(charaInfoNum, out string[] info))
                {
                    // ȡ�ä�������饯���`����ǥХå���ʾ
                    Debug.Log("Character info found: " + string.Join(", ", info));

                    // // �ƥ����ȥ��֥������Ȥ����ɤ��������ʾ
                    GameObject textObject = new GameObject("CharacterInfoText");
                    textObject.transform.SetParent(charaInfoInstance.transform, false);
                    RectTransform rectTransform = textObject.AddComponent<RectTransform>();
                    rectTransform.sizeDelta = textSize; // �ƥ����ȥܥå����Υ��������O��
                    rectTransform.position = charaInfoInstance.transform.position + textOffset; // �ƥ����ȥܥå�����λ�ä��O��

                    TextMeshPro tmpText = textObject.AddComponent<TextMeshPro>();
                    tmpText.font = dotFont; // �ե���Ȥ��O��
                    tmpText.fontSize = 3; // ���֥��������O��
                    tmpText.lineSpacing = 25.0f; // ���g���O��
                    tmpText.color = new Color32(0x07, 0x8D, 0x21, 0xFF); // color #078D21
                    tmpText.text = string.Join("\n", info);��// �ƥ��������ݤ��O��
                }
                else
                {
                    // ����饯���`���Ťˌ��ꤹ����󤬟o�����ϤΥ���`
                    Debug.LogError($"No character info found for character number {charaInfoNum}");
                }
            }
            else
            {
                // ����饯���`�����鷬�Ť�����Ǥ��ʤ��ä����ϤΥ���`
                Debug.LogError($"Failed to parse character number from name: {characterName}");
            }
        }
    }

    // �ޥ���������饯���`�����x�줿�Ȥ��΄I��
    void OnMouseExit()
    {
        // ����饯���`���Υ��󥹥��󥹤����ڤ�����Ϥ��Ɨ�����
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
