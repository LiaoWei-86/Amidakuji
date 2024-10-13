using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class NewLineMoveController : MonoBehaviour
{
    public GameObject movingObject; // �ړ�������GameObject
    public float moveSpeed = 1f; // �ړ����x
    public delegate void EventEndHandler();
    public event EventEndHandler OnEventEnd; // �ړ��������̃C�x���g

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

        // �}�E�X�N���b�N�ʒu����LineRenderer��̃|�C���g���擾
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // �ł��߂��|�C���g��T��
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

        // �ړ�����ʒu������
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

            // �ŏI�I�ɐ��m�ɖړI�n�Ɉړ�
            movingObject.transform.position = targetPosition;
        }

        isMoving = false;

        // �C�x���g�𔭉�
        OnEventEnd?.Invoke();
    }
}
