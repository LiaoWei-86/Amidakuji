using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineT5 : MonoBehaviour
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
    public GameObject[] plotIconPrefabs; // �ץ�åȥ�������Υץ�ϥ�
    public Transform[] plotIconPositions; // plotIcon��λ��

    public GameObject tooltipPrefab; // ��ʾ���Υץ�ϥ�
    public GameObject charaInfoPrefab;
    public Material horizontalLineMaterial; // �ᾀ�Υޥƥꥢ��
    public float horizontalLineWidth = 0.1f; // �ᾀ�η�
    private Dictionary<(GameObject, GameObject), GameObject> horizontalLines = new Dictionary<(GameObject, GameObject), GameObject>(); // �ᾀ�δǕ�
    public float hoverAreaWidth = 0.4f; // �ᾀ�ΥۥЩ`���ꥢ�οk��

    private List<GameObject> lines = new List<GameObject>(); // �����֥������ȤΥꥹ��
    private List<GameObject> points = new List<GameObject>(); // �㥪�֥������ȤΥꥹ��

    public Dictionary<int, Vector3> pointsDictionary;// ��Ȥ���λ�������{����Ǖ�

    public float offset = -0.1f;

    public T5TLcontroller T5TLcontrollerScript;

    // Start is called before the first frame update
    void Start()
    {
        if (T5TLcontrollerScript != null)
        {
            T5TLcontrollerScript = FindObjectOfType<T5TLcontroller>();
        }

        pointsDictionary = new Dictionary<int, Vector3>();

        // �}���ξ�������
        for (int i = 0; i < numberOfLines; i++)
        {
            Vector3 startPoint = initialStartPoint + new Vector3(i * 2, 0, 0); // ��˥��ե��å�
            Vector3 endPoint = initialEndPoint + new Vector3(i * 2, 0, 0);

            pointsDictionary.Add(i, startPoint); // ��δǕ��ˤ��줾��ξ��Υ����`�ȵ�򷬺Ÿ�����λ�������{����
            pointsDictionary.Add(i + numberOfLines * (pointsPerLine + 1), endPoint); // ��δǕ��ˤ��줾��ξ��Υ���ɵ�򷬺Ÿ�����λ�������{����


            CreateLine(startPoint, endPoint, i); // CreateLine�v����g�Ф���

            // ���ɤ��줿ȫ�Ƥε��������˳���
            foreach (var point in points)
            {
                Debug.Log($"Point: {point.name}, Position: {point.transform.position}");
            }

            foreach (KeyValuePair<int, Vector3> kvp in pointsDictionary)
            {
                Debug.Log($"PointsKey: {kvp.Key}, PointsTransformPositionVector3: {kvp.Value}");
            }

            
        }

        DrawHorizontalLine(2, 3);
        DrawHorizontalLine(4, 5);
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


            // �����������
            circleObject.name = $"Circle_Line{lineNumber}_Point{i}";
            Debug.Log($"Line {lineNumber}: Created Circle at {circlePosition} ");

            pointsDictionary.Add(lineIndex + (i + 1) * numberOfLines, circlePosition);



            // Add hover area for each point pair
            if (points.Count > 7)
            {
                Debug.Log($"Creating Hover Area for point pair: {points[points.Count - 2].name}, {points[points.Count - 1].name}");
                CreateHoverAreaT5(points[2], points[6]);
                CreateHoverAreaT5(points[3], points[7]);
                /*
                 * "if (points.Count > 7)"----->  �����Υ��`�ɤ�g�Ф���r��point�������Ȥ� 8 ���_���Ƥ���Ȥ�������
                 
                 * ǰ�ξ��Ϥε��Ӥ�����ǴΤξ��ε��׷�Ӥ��뤿�ᡢ

                    points[0]��line1_point0
                    points[1]��line1_point1
                    points[2]��line2_point0
                    points[3]��line2_point1
                    
                    �����������Tʿ��0�������������d����1
                                ||                ||
                    points[0]   ��2    points[4]  ��3
                                ||                ||
                    points[1]   ��4    points[5]  ��5
                                ||                ||
                    points[2]   ��6    points[6]  ��7
                                ||                ||
                    points[3]   ��8    points[7]  ��9
                                ||                ||
                ���������������Yĩ��10�������������Yĩ��11
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
                CreateHoverAreaCharacter(characterObject);
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


        lines.Add(lineObject);
    }

    private void DrawHorizontalLine(int start, int end)
    {
        // ���Τ�����¤��� GameObject ������
        GameObject lineObject = new GameObject($"HorizontalLine({start},{end})");
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
            Debug.Log($"��DrawHorizontalLine��Key[{start}]Value[{startPoint}]");
        }
        else
        {
            // Key not found
            Debug.Log($"��DrawHorizontalLine��Key[{start}]Value[startPoint] not found.");
        }
        if (pointsDictionary.TryGetValue(end, out endPoint))
        {
            //Key found
            Debug.Log($"��DrawHorizontalLine��Key[{end}]Value[{endPoint}]");
        }
        else
        {
            // Key not found
            Debug.Log($"��DrawHorizontalLine��Key[{end}]Value[endPoint] not found.");
        }

        //�ᾀ���褯
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    void CreateHoverAreaT5(GameObject pointA, GameObject pointB)
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

        HoverAreaT5 hoverScriptT5 = hoverArea.AddComponent<HoverAreaT5>();                    // Need changed NOTICE��
        hoverScriptT5.Initialize(pointA, pointB, horizontalLineMaterial, horizontalLineWidth, tooltipPrefab);

        Debug.Log($"HoverArea script added to {hoverArea.name}");

        Debug.Log($"Hover Area created at {midPoint} with size {boxCollider.size}");
    }

    void CreateHoverAreaCharacter(GameObject character)
    {
        Debug.Log($"CreateHoverArea called with character: {character.name}");


        BoxCollider boxCollider = character.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(1f, 1f, 0.1f);
        boxCollider.isTrigger = true;

        characterInfoHoverT5 characterInfoHoverT5Script = character.AddComponent<characterInfoHoverT5>();
        characterInfoHoverT5Script.Initialize(character, charaInfoPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
