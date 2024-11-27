using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [Header("LineRenderers")]
    public LineRenderer LineRenderer1, LineRenderer2, LineRenderer3, LineRenderer4, LineRenderer5;

    [Header("Bars")]
    public GameObject bar1, bar2, bar3, bar4, bar5;

    [Header("Player and Target Objects")]
    public GameObject Player;
    public Transform circle1, circle2, circle3, circle4, circle5, circle6, circle7, circle8, circle9, circle10;
    public Transform Text1, Text2, Text3, Text4, Text5, Text6;

    private bool isLine1Active = false;
    private bool isLine2Active = false;
    private bool isLine3Active = false;
    private bool isLine4Active = false;
    private bool isLine5Active = false;
    private bool isPlayerMoving = false;

    private void Start()
    {
        // 初期化: 非表示
        SetLineAndBarActive(LineRenderer1, bar1, false);
        SetLineAndBarActive(LineRenderer2, bar2, false);
        SetLineAndBarActive(LineRenderer3, bar3, false);
        SetLineAndBarActive(LineRenderer4, bar4, false);
        SetLineAndBarActive(LineRenderer5, bar5, false);
    }

    private void Update()
    {
        if (isPlayerMoving) return;

        // Enter キーで PlayerMove 実行
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isPlayerMoving = true;
            StartCoroutine(PlayerMove());
        }
    }

    private void SetLineAndBarActive(LineRenderer line, GameObject bar, bool active)
    {
        if (line != null) line.gameObject.SetActive(active);
        if (bar != null) bar.SetActive(active);
    }

    private IEnumerator PlayerMove()
    {
        if (!isLine1Active)
        {
            yield return MoveToPosition(Player.transform, Text1.position, 90);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("TutorialScene");
        }
        else if (!isLine2Active)
        {
            yield return MoveToPosition(Player.transform, circle1.position, 30);
            yield return MoveToPosition(Player.transform, circle2.position, 60);
            yield return MoveToPosition(Player.transform, Text2.position, 60);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("ContinueScene");
        }
        else if (!isLine3Active)
        {
            yield return MoveToPosition(Player.transform, circle1.position, 30);
            yield return MoveToPosition(Player.transform, circle2.position, 60);
            yield return MoveToPosition(Player.transform, circle3.position, 15);
            yield return MoveToPosition(Player.transform, circle4.position, 60);
            yield return MoveToPosition(Player.transform, Text3.position, 45);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("NewjourneyScene");
        }
        else if (!isLine4Active)
        {
            yield return MoveToPosition(Player.transform, circle1.position, 30);
            yield return MoveToPosition(Player.transform, circle2.position, 60);
            yield return MoveToPosition(Player.transform, circle3.position, 15);
            yield return MoveToPosition(Player.transform, circle4.position, 60);
            yield return MoveToPosition(Player.transform, circle5.position, 15);
            yield return MoveToPosition(Player.transform, circle6.position, 60);
            yield return MoveToPosition(Player.transform, Text4.position, 30);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("CharactorScene");
        }
        else if (!isLine5Active)
        {
            yield return MoveToPosition(Player.transform, circle1.position, 30);
            yield return MoveToPosition(Player.transform, circle2.position, 60);
            yield return MoveToPosition(Player.transform, circle3.position, 15);
            yield return MoveToPosition(Player.transform, circle4.position, 60);
            yield return MoveToPosition(Player.transform, circle5.position, 15);
            yield return MoveToPosition(Player.transform, circle6.position, 60);
            yield return MoveToPosition(Player.transform, circle7.position, 15);
            yield return MoveToPosition(Player.transform, circle8.position, 60);
            yield return MoveToPosition(Player.transform, Text5.position, 15);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("journeyScene");
        }
        else
        {
            yield return MoveToPosition(Player.transform, circle1.position, 30);
            yield return MoveToPosition(Player.transform, circle2.position, 60);
            yield return MoveToPosition(Player.transform, circle3.position, 15);
            yield return MoveToPosition(Player.transform, circle4.position, 60);
            yield return MoveToPosition(Player.transform, circle5.position, 15);
            yield return MoveToPosition(Player.transform, circle6.position, 60);
            yield return MoveToPosition(Player.transform, circle7.position, 15);
            yield return MoveToPosition(Player.transform, circle8.position, 60);
            yield return MoveToPosition(Player.transform, circle9.position, 15);
            yield return MoveToPosition(Player.transform, circle10.position, 60);
            yield return MoveToPosition(Player.transform, Text6.position, 15);
            yield return new WaitForSeconds(1f);
            Application.Quit();
        }
    }

    private IEnumerator MoveToPosition(Transform obj, Vector3 targetPosition, int frames)
    {
        Vector3 startPosition = obj.position;
        for (int i = 0; i < frames; i++)
        {
            obj.position = Vector3.Lerp(startPosition, targetPosition, (float)i / frames);
            yield return null;
        }
        obj.position = targetPosition;
    }

    public void OnMouseEnterLine(GameObject bar)
    {
        bar.SetActive(true);
    }

    public void OnMouseExitLine(GameObject bar)
    {
        bar.SetActive(false);
    }

    public void OnMouseClickLine(LineRenderer line, ref bool isActive)
    {
        line.gameObject.SetActive(true);
        isActive = true;
    }
}


