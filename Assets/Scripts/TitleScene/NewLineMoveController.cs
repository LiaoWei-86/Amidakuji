using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class NewLineMoveController : MonoBehaviour
{
    public GameObject movingObject; // 移動させるGameObject
    public float moveSpeed = 1f; // 移動速度
    public delegate void EventEndHandler();
    public event EventEndHandler OnEventEnd; // 移動完了時のイベント

    private LineRenderer lineRenderer;
    private bool isMoving = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            StartCoroutine(MoveObject());
        }
    }

    private IEnumerator MoveObject()
    {
        isMoving = true;

        // マウスクリック位置からLineRenderer上のポイントを取得
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 最も近いポイントを探す
        float closestDistance = Mathf.Infinity;
        int closestIndex = -1;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float distance = Vector3.Distance(worldPosition, lineRenderer.GetPosition(i));
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        // 移動する位置を決定
        if (closestIndex != -1)
        {
            Vector3 targetPosition = lineRenderer.GetPosition(closestIndex);
            float journeyLength = Vector3.Distance(movingObject.transform.position, targetPosition);
            float startTime = Time.time;

            while (Vector3.Distance(movingObject.transform.position, targetPosition) > 0.01f)
            {
                float distCovered = (Time.time - startTime) * moveSpeed;
                float fractionOfJourney = distCovered / journeyLength;

                movingObject.transform.position = Vector3.Lerp(movingObject.transform.position, targetPosition, fractionOfJourney);
                yield return null;
            }

            // 最終的に正確に目的地に移動
            movingObject.transform.position = targetPosition;
        }

        isMoving = false;

        // イベントを発火
        OnEventEnd?.Invoke();
    }
}
