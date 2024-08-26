using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineT4 : MonoBehaviour
{
    public Material lineMaterial; // ���Υޥƥꥢ��
    public float lineWidth = 0.1f; // ���η�
    public Vector3 initialStartPoint = new Vector3(0, 0, 0); // ���ڤ����
    public Vector3 initialEndPoint = new Vector3(0, 5, 0); // ���ڤνK��
    public int numberOfLines = 2; // ������
    public int pointsPerLine = 2; // �����ε����

    public GameObject circlePrefab; // ��(��)�Υץ�ϥ�
    public GameObject[] characterPrefabs; // ����饯���`�Υץ�ϥ�
    public GameObject[] endingPrefabs; // �Yĩ��������Υץ�ϥ�
    public GameObject[] plotIconPrefabs; // �ץ��åȥ�������Υץ�ϥ�
    public Transform[] plotIconPositions; // plotIcon��λ��

    private bool plotIcon0Generated = false;
    private bool plotIcon3Generated = false;
    private bool plotIcon1_2Generated = false;

    public GameObject tooltipPrefab; // ��ʾ���Υץ�ϥ�
    public Material horizontalLineMaterial; // �ᾀ�Υޥƥꥢ��
    public float horizontalLineWidth = 0.1f; // �ᾀ�η�
    private Dictionary<(GameObject, GameObject), GameObject> horizontalLines = new Dictionary<(GameObject, GameObject), GameObject>(); // �ᾀ�δǕ�
    public float hoverAreaWidth = 0.4f; // �ᾀ�ΥۥЩ`���ꥢ�οk��

    //public GameObject T2TLcontrollerGameObject; // T2TLcontroller������ץȤ�isCharacterMoving�֩`�낎��ȡ�ä��뤿��
    //public T2TLcontroller T2TLcontrollerScript; // T2TLcontroller������ץȤβ��դ��{���뤿��

    private List<GameObject> lines = new List<GameObject>(); // �����֥������ȤΥꥹ��
    private List<GameObject> points = new List<GameObject>(); // �㥪�֥������ȤΥꥹ��

    public Dictionary<int, Vector3> pointsDictionary;// ��Ȥ���λ�������{����Ǖ�

    public float offset = -0.1f;

    public T4TLcontroller T4TLcontrollerScript;

    // Start is called before the first frame update
    void Start()
    {
        if (T4TLcontrollerScript != null)
        {
            T4TLcontrollerScript = FindObjectOfType<T4TLcontroller>();
        }

        pointsDictionary = new Dictionary<int, Vector3>();
        //  ������ץȤβ��դ�ȡ��
        //T2TLcontrollerScript = T2TLcontrollerGameObject.GetComponent<T2TLcontroller>();

        // �}���ξ�������
        for (int i = 0; i < numberOfLines; i++)
        {
            Vector3 startPoint = initialStartPoint + new Vector3(i * 2, 0, 0); // ��˥��ե��å�
            Vector3 endPoint = initialEndPoint + new Vector3(i * 2, 0, 0);

            pointsDictionary.Add(i, startPoint); // ��δǕ��ˤ��줾��ξ��Υ����`�ȵ�򷬺Ÿ�����λ�������{����
            pointsDictionary.Add(i + numberOfLines * (pointsPerLine + 1), endPoint); // ��δǕ��ˤ��줾��ξ��Υ���ɵ�򷬺Ÿ�����λ�������{����


            CreateLine(startPoint, endPoint, i); // CreateLine�v����g�Ф���

        }

        // ���ɤ��줿ȫ�Ƥε����������˳���
        foreach (var point in points)
        {
            Debug.Log($"Point: {point.name}, Position: {point.transform.position}");
        }

        foreach (KeyValuePair<int, Vector3> kvp in pointsDictionary)
        {
            Debug.Log($"PointsKey: {kvp.Key}, PointsTransformPositionVector3: {kvp.Value}");
        }

        DrawHorizontalLine(2, 3);
    }

    void CreateLine(Vector3 startPoint, Vector3 endPoint, int lineIndex)
    {
        int lineNumber = lineIndex + 1; // lineNumber is 1-based for display
        Debug.Log($"Creating Line: lineIndex = {lineIndex}, lineNumber = {lineNumber}");

        // ���Τ�����¤��� GameObject ������
        GameObject lineObject = new GameObject("Line" + lineNumber);
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


            // ��������׷��
            circleObject.name = $"Circle_Line{lineNumber}_Point{i}";
            Debug.Log($"Line {lineNumber}: Created Circle at {circlePosition} with ID ({lineNumber}, {i})");

            pointsDictionary.Add(lineIndex + (i + 1) * numberOfLines, circlePosition);

            

            // Add hover area for each point pair
            if (points.Count > 3)
            {
                Debug.Log($"Creating Hover Area for point pair: {points[points.Count - 2].name}, {points[points.Count - 1].name}");
                CreateHoverAreaT4(points[points.Count - 3], points[points.Count - 1]);
                /*
                 * "if (points.Count > 3)"----->  �����Υ��`�ɤ�g�Ф���r��point�������Ȥ�4���_���Ƥ���Ȥ�������
                 * points[points.Count - 3]��points.[1]
                 * points[points.Count - 1]��points.[3]
                 
                 * ǰ�ξ��Ϥε��Ӥ�����ǴΤξ��ε��׷�Ӥ��뤿�ᡢ

                    points.[0]��line1_point0
                    points.[1]��line1_point1
                    points.[2]��line2_point0
                    points.[3]��line2_point1
                    
                    �����������Tʿ��0�������������d����1
                                ||                ||
                    points.[0]  ��2   points.[2]  ��3
                                ||                ||
                    points.[1]  ��4   points.[3]  ��5
                                ||                ||
                ���������������Yĩ��6�������������Yĩ��7
                */

            }

        }



        // ����饯���`������
        if (lineIndex < characterPrefabs.Length)
        {
            Vector3 characterPosition = startPoint + new Vector3(0, 1, -0.1f);
            GameObject characterObject = Instantiate(characterPrefabs[lineIndex], characterPosition, Quaternion.identity);

            string gameObjectName = $"character{lineNumber}";
            GameObject parentGameObject = GameObject.Find(gameObjectName);
            if (parentGameObject != null)
            {
                // �⤷Ҋ�Ĥ����顢characterObject��Transform��Ҋ�Ĥ���줿GameObject��Transform���ӹ����֥������Ȥ��O��
                characterObject.transform.parent = parentGameObject.transform;
                characterObject.transform.position = parentGameObject.transform.position + new Vector3(0, offset, 0);
            }
            else
            {
                // �⤷Ҋ�Ĥ����ʤ��ä��顢����`��ǥХå�����
                Debug.LogError($"GameObject '{gameObjectName}' not found!");
            }
            characterObject.name = $"Character{lineNumber}";

            Debug.Log($"Line {lineNumber }: Created {characterObject.name} at {characterPosition}");
        }
        else
        {
            Debug.LogWarning($"Line {lineNumber }: No character prefab available for line number {lineNumber}");
        }



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

        //// �ץ��åȥ������������
        //Vector3 circleMiddlePosition = (startPoint + endPoint) / 2;
        //Vector3 plotIconPosition = circleMiddlePosition + new Vector3(1, 0, 0);
        //GameObject plotIconObject = Instantiate(plotIconPrefab, plotIconPosition, Quaternion.identity);
        //plotIconObject.transform.parent = lineObject.transform;
        //plotIconObject.SetActive(false);
        //Debug.Log($"Line {lineNumber }: Created Plot Icon at {plotIconPosition}");

        lines.Add(lineObject);
    }

    private void DrawHorizontalLine(int start,int end)
    {
        // ���Τ�����¤��� GameObject ������
        GameObject lineObject = new GameObject("HorizontalLine" + 1);
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        // ���Υޥƥꥢ��ȷ����O��
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = horizontalLineWidth;
        lineRenderer.endWidth = horizontalLineWidth;

        Vector3 startPoint = new Vector3();
        Vector3 endPoint = new Vector3();

        if (pointsDictionary.TryGetValue(start, out startPoint))
        {
            //Key found
            Debug.Log($"Key[{start}]Value[{startPoint}]");
        }
        else
        {
            // Key not found
            Debug.Log($"Key[{start}]Value[startPoint] not found.");
        }
        if (pointsDictionary.TryGetValue(end, out endPoint))
        {
            //Key found
            Debug.Log($"Key[{end}]Value[{endPoint}]");
        }
        else
        {
            // Key not found
            Debug.Log($"Key[{end}]Value[endPoint] not found.");
        }

        //�ᾀ���褯
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    void CreateHoverAreaT4(GameObject pointA, GameObject pointB)
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

        HoverAreaT4 hoverScriptT4 = hoverArea.AddComponent<HoverAreaT4>();                    // Need changed NOTICE��
        hoverScriptT4.Initialize(pointA, pointB, horizontalLineMaterial, horizontalLineWidth, tooltipPrefab);

        Debug.Log($"HoverArea script added to {hoverArea.name}");

        Debug.Log($"Hover Area created at {midPoint} with size {boxCollider.size}");
    }

    // Update is called once per frame
    void Update()
    {
        // �ǥХå����������
        //Debug.Log($"Update called: Number of lines = {lines.Count}, Number of points = {points.Count}");

        // watch knight's position,if it is OK, created plotIcon
        if (!plotIcon0Generated && Mathf.Abs(T4TLcontrollerScript.knight.transform.position.x - pointsDictionary[3].x) < 0.05f)
        {
            GeneratePlotIcon(0); // ����plotIcon0
            plotIcon0Generated = true; // ��ֹ�ظ�����
        }
        // watch hunter's position,if it is OK, created plotIcon
        if (!plotIcon3Generated && Mathf.Abs(T4TLcontrollerScript.hunter.transform.position.y - pointsDictionary[5].y) < 0.05f)
        {
            GeneratePlotIcon(3); // ����plotIcon0
            plotIcon3Generated = true; // ��ֹ�ظ�����
        }

        if (!plotIcon1_2Generated && Mathf.Abs(T4TLcontrollerScript.knight.transform.position.y - (pointsDictionary[2].y + pointsDictionary[4].y) / 2) < 0.05f)
        {
            for (int i = 1; i <= 2; i++)
            {
                GeneratePlotIcon(i); // ����plotIcon1-3
            }
            plotIcon1_2Generated = true; // ��ֹ�ظ�����
        }
    }

    void GeneratePlotIcon(int index)
    {
        if (index < plotIconPrefabs.Length && index < plotIconPositions.Length)
        {
            // ��ָ��λ������plotIcon
            GameObject plotIcon = Instantiate(plotIconPrefabs[index], plotIconPositions[index].position, Quaternion.identity);
            plotIcon.name = "plotIcon" + index; // ΪplotIcon����
            Debug.Log($"Generated {plotIcon.name} at position {plotIcon.transform.position}");
        }
        else
        {
            Debug.LogWarning("Invalid plotIcon index or position");
        }
    }
}