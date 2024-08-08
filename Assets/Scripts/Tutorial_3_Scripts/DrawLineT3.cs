using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineT3 : MonoBehaviour
{
    public Material lineMaterial; // ���Υޥƥꥢ��
    public float lineWidth = 0.1f; // ���η�
    public Vector3 initialStartPoint = new Vector3(0, 0, 0); // ���ڤ����
    public Vector3 initialEndPoint = new Vector3(0, 5, 0); // ���ڤνK��
    public int numberOfLines = 2; // ������
    public int pointsPerLine = 1; // �����ε����

    public GameObject circlePrefab; // ��(��)�Υץ�ϥ�
    public GameObject[] characterPrefabs; // ����饯���`�Υץ�ϥ�
    public GameObject[] endingPrefabs; // �Yĩ��������Υץ�ϥ�
    public GameObject plotIconPrefab; // �ץ�åȥ�������Υץ�ϥ�

    public GameObject tooltipPrefab; // ��ʾ���Υץ�ϥ�
    public Material horizontalLineMaterial; // �ᾀ�Υޥƥꥢ��
    public float horizontalLineWidth = 0.1f; // �ᾀ�η�
    private Dictionary<(GameObject, GameObject), GameObject> horizontalLines = new Dictionary<(GameObject, GameObject), GameObject>(); // �ᾀ�δǕ�
    public float hoverAreaWidth = 0.4f; // �ᾀ�ΥۥЩ`���ꥢ�οk��

    //public GameObject T2TLcontrollerGameObject; // T2TLcontroller������ץȤ�isCharacterMoving�֩`�낎��ȡ�ä��뤿��
    //public T2TLcontroller T2TLcontrollerScript; // T2TLcontroller������ץȤβ��դ��{���뤿��

    private List<GameObject> lines = new List<GameObject>(); // �����֥������ȤΥꥹ��
    private List<GameObject> points = new List<GameObject>(); // �㥪�֥������ȤΥꥹ��


    // Start is called before the first frame update
    void Start()
    {
        //  ������ץȤβ��դ�ȡ��
        //T2TLcontrollerScript = T2TLcontrollerGameObject.GetComponent<T2TLcontroller>();

        // �}���ξ�������
        for (int i = 0; i < numberOfLines; i++)
        {
            Vector3 startPoint = initialStartPoint + new Vector3(i * 2, 0, 0); // ��˥��ե��å�
            Vector3 endPoint = initialEndPoint + new Vector3(i * 2, 0, 0);
            CreateLine(startPoint, endPoint, i);
        }
    }

    void CreateLine(Vector3 startPoint, Vector3 endPoint, int lineIndex)
    {
        int lineNumber = lineIndex + 1; // lineNumber is 1-based for display
        Debug.Log($"Creating Line: lineIndex = {lineIndex}, lineNumber = {lineNumber}");

        // ���Τ�����¤��� GameObject ������
        GameObject lineObject = new GameObject("Line" + lineNumber );
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        // ���Υޥƥꥢ��ȷ����O��
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // �������ȽK����O��
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);

        Debug.Log($"Line {lineNumber }: Start Point = {startPoint}, End Point = {endPoint}");

        // �ң��㣩��λ�ä�Ӌ�㤷������
        for (int i = 0; i < pointsPerLine; i++)
        {
            Vector3 circlePosition = startPoint + new Vector3(0, (endPoint.y - startPoint.y) / (pointsPerLine + 1) * (i + 1), 0);
            GameObject circleObject = Instantiate(circlePrefab, circlePosition, Quaternion.identity);
            circleObject.transform.parent = lineObject.transform;
            points.Add(circleObject);


            Debug.Log($"Line {lineNumber }: Created Circle at {circlePosition}");



            // Add hover area for each point pair
            if (points.Count > 1)
            {
                Debug.Log($"Creating Hover Area for point pair: {points[points.Count - 2].name}, {points[points.Count - 1].name}");
                CreateHoverArea(points[points.Count - 2], points[points.Count - 1]);

                Debug.Log("we are here!");
            }

        }

        

        // ����饯���`������
        if (lineIndex < characterPrefabs.Length)
        {
            Vector3 characterPosition = startPoint + new Vector3(0, 1, -0.1f);
            GameObject characterObject = Instantiate(characterPrefabs[lineIndex], characterPosition, Quaternion.identity);
            characterObject.transform.parent = lineObject.transform;

            Debug.Log($"Line {lineNumber }: Created Character at {characterPosition}");
        }
        else
        {
            Debug.LogWarning($"Line {lineNumber }: No character prefab available for line number {lineNumber}");
        }


        // ����饯���`�Υ��`���å�λ�ä�Ӌ��
        Vector3 targetPosition = endPoint + new Vector3(0, 0, -0.1f);

        // �Yĩ�������������
        if (lineIndex < endingPrefabs.Length)
        {
            Vector3 endingPosition = endPoint + new Vector3(0, -1, 0);
            GameObject endingObject = Instantiate(endingPrefabs[lineIndex], endingPosition, Quaternion.identity);
            endingObject.transform.parent = lineObject.transform;
            Debug.Log($"Line {lineNumber }: Created Ending at {endingPosition}");
        }
        else
        {
            Debug.LogWarning($"Line {lineNumber }: No ending prefab available for line number {lineNumber}");
        }

        // �ץ�åȥ������������
        Vector3 circleMiddlePosition = (startPoint + endPoint) / 2;
        Vector3 plotIconPosition = circleMiddlePosition + new Vector3(1, 0, 0);
        GameObject plotIconObject = Instantiate(plotIconPrefab, plotIconPosition, Quaternion.identity);
        plotIconObject.transform.parent = lineObject.transform;
        plotIconObject.SetActive(false);
        Debug.Log($"Line {lineNumber }: Created Plot Icon at {plotIconPosition}");

        lines.Add(lineObject);
    }

    void CreateHoverArea(GameObject pointA, GameObject pointB)
    {
        Debug.Log($"CreateHoverArea called with pointA: {pointA.name}, pointB: {pointB.name}");

        Vector3 midPoint = (pointA.transform.position + pointB.transform.position) / 2;
        Vector3 direction = (pointB.transform.position - pointA.transform.position).normalized;

        GameObject hoverArea = new GameObject("HoverArea");
        BoxCollider boxCollider = hoverArea.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(Vector3.Distance(pointA.transform.position, pointB.transform.position), hoverAreaWidth, 0.1f);
        boxCollider.isTrigger = true;

        Debug.Log($"BoxCollider created with size: {boxCollider.size}");

        hoverArea.transform.position = midPoint;
        hoverArea.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        HoverArea hoverScript = hoverArea.AddComponent<HoverArea>();
        hoverScript.Initialize(pointA, pointB, horizontalLineMaterial, horizontalLineWidth, tooltipPrefab);

        Debug.Log($"HoverArea script added to {hoverArea.name}");

        Debug.Log($"Hover Area created at {midPoint} with size {boxCollider.size}");
    }

    // Update is called once per frame
    void Update()
    {
        // �ǥХå��������
        Debug.Log($"Update called: Number of lines = {lines.Count}, Number of points = {points.Count}");
    }
}
