using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleSceneManager : MonoBehaviour
{
    public GameObject titleTextPrefab;    // タイトルテキストのPrefab
    public GameObject tutorialTextPrefab; // チュートリアルテキストのPrefab
    public GameObject barTutorial, barContinue1, barContinue2, barNewJourney1, barNewJourney2, barCharaRecords1, barCharaRecords2, barJourneyRecords1, barJourneyRecords2, barExit;
    public GameObject player;

    public LineRenderer lineTutorial, lineContinue1, lineContinue2, lineNewJourney1, lineNewJourney2, lineCharaRecords1, lineCharaRecords2, lineJourneyRecords1, lineJourneyRecords2, lineExit;

    private GameObject titleTextInstance;
    private GameObject tutorialTextInstance;
    private Coroutine typingCoroutine;
    private bool isTitleDisplayed = false;
    private bool isTutorialDisplayed = false;
    private bool playerMoving = false;

    void Start()
    {
        // タイトルとチュートリアルテキストのPrefabをインスタンス化
        titleTextInstance = Instantiate(titleTextPrefab, transform);
        tutorialTextInstance = Instantiate(tutorialTextPrefab, transform);

        StartTitleAnimation();
    }

    void Update()
    {
        HandleEnterKey();
        HandleMouseHover();
    }

    private void StartTitleAnimation()
    {
        TextMeshProUGUI titleTMP = titleTextInstance.GetComponent<TextMeshProUGUI>();
        typingCoroutine = StartCoroutine(TypeText(titleTMP, "Welcome to the Game!"));  // メッセージ例
    }

    private IEnumerator TypeText(TextMeshProUGUI textMesh, string message)
    {
        textMesh.text = "";
        foreach (char letter in message.ToCharArray())
        {
            textMesh.text += letter;
            yield return new WaitForSeconds(0.1f);  // アニメーションの速さを調整
        }
        isTitleDisplayed = true;
        StartTutorialAnimation();
    }

    private void StartTutorialAnimation()
    {
        TextMeshProUGUI tutorialTMP = tutorialTextInstance.GetComponent<TextMeshProUGUI>();
        typingCoroutine = StartCoroutine(TypeText(tutorialTMP, "Press Enter to Start"));  // チュートリアルメッセージ例
    }

    private void HandleEnterKey()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                TextMeshProUGUI titleTMP = titleTextInstance.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI tutorialTMP = tutorialTextInstance.GetComponent<TextMeshProUGUI>();
                titleTMP.text = "Welcome to the Game!"; // フルテキスト
                tutorialTMP.text = "Press Enter to Start";
                isTitleDisplayed = true;
                isTutorialDisplayed = true;
            }
            else if (isTitleDisplayed && isTutorialDisplayed && !playerMoving)
            {
                CheckLineSelection();
            }
        }
    }

    private void HandleMouseHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject hoveredObject = hit.collider.gameObject;

            barTutorial.SetActive(hoveredObject == lineTutorial.gameObject);
            barContinue1.SetActive(hoveredObject == lineContinue1.gameObject);
            barContinue2.SetActive(hoveredObject == lineContinue2.gameObject);
            barNewJourney1.SetActive(hoveredObject == lineNewJourney1.gameObject);
            barNewJourney2.SetActive(hoveredObject == lineNewJourney2.gameObject);
            barCharaRecords1.SetActive(hoveredObject == lineCharaRecords1.gameObject);
            barCharaRecords2.SetActive(hoveredObject == lineCharaRecords2.gameObject);
            barJourneyRecords1.SetActive(hoveredObject == lineJourneyRecords1.gameObject);
            barJourneyRecords2.SetActive(hoveredObject == lineJourneyRecords2.gameObject);
            barExit.SetActive(hoveredObject == lineExit.gameObject);
        }
        else
        {
            DisableAllBars();
        }
    }

    private void DisableAllBars()
    {
        barTutorial.SetActive(false);
        barContinue1.SetActive(false);
        barContinue2.SetActive(false);
        barNewJourney1.SetActive(false);
        barNewJourney2.SetActive(false);
        barCharaRecords1.SetActive(false);
        barCharaRecords2.SetActive(false);
        barJourneyRecords1.SetActive(false);
        barJourneyRecords2.SetActive(false);
        barExit.SetActive(false);
    }

    private void CheckLineSelection()
    {
        // マウス位置による選択とシーン切り替え処理
        if (barTutorial.activeSelf)
        {
            StartCoroutine(MovePlayerAlongLine(lineTutorial, "Scene_tutorial"));
        }
        else if (barContinue1.activeSelf)
        {
            StartCoroutine(MovePlayerAlongLine(lineContinue1, null));
        }
        else if (barContinue2.activeSelf)
        {
            StartCoroutine(MovePlayerAlongLine(lineContinue2, "Scene_continue"));
        }
        else if (barNewJourney1.activeSelf)
        {
            StartCoroutine(MovePlayerAlongLine(lineNewJourney1, null));
        }
        else if (barNewJourney2.activeSelf)
        {
            StartCoroutine(MovePlayerAlongLine(lineNewJourney2, "Scene_newjourney"));
        }
        else if (barCharaRecords1.activeSelf)
        {
            StartCoroutine(MovePlayerAlongLine(lineCharaRecords1, null));
        }
        else if (barCharaRecords2.activeSelf)
        {
            StartCoroutine(MovePlayerAlongLine(lineCharaRecords2, "Scene_chararecords"));
        }
        else if (barJourneyRecords1.activeSelf)
        {
            StartCoroutine(MovePlayerAlongLine(lineJourneyRecords1, null));
        }
        else if (barJourneyRecords2.activeSelf)
        {
            StartCoroutine(MovePlayerAlongLine(lineJourneyRecords2, "Scene_journeyrecords"));
        }
        else if (barExit.activeSelf)
        {
            StartCoroutine(MovePlayerAlongLine(lineExit, "Scene_exit"));
        }
    }

    private IEnumerator MovePlayerAlongLine(LineRenderer line, string sceneToLoad)
    {
        playerMoving = true;
        int pointCount = line.positionCount;
        Vector3[] positions = new Vector3[pointCount];
        line.GetPositions(positions);

        foreach (Vector3 position in positions)
        {
            player.transform.position = position;
            yield return new WaitForSeconds(0.05f);  // 移動速度調整
        }

        playerMoving = false;
        if (sceneToLoad != null)
        {
            yield return new WaitForSeconds(2.0f);  // n秒待機
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
