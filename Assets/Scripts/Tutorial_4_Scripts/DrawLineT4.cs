using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineT4 : MonoBehaviour
{
    public Material lineMaterial; // 線のマテリアル
    public float lineWidth = 0.1f; // 線の幅
    public Vector3 initialStartPoint = new Vector3(0, 0, 0); // 初期の起点
    public Vector3 initialEndPoint = new Vector3(0, 5, 0); // 初期の終点
    public int numberOfLines = 2; // 線の数
    public int pointsPerLine = 2; // 各線の点の数

    public GameObject circlePrefab; // 円(点)のプレハブ
    public GameObject[] characterPrefabs; // キャラクターのプレハブ
    public GameObject[] endingPrefabs; // 結末アイコンのプレハブ
    public GameObject[] plotIconPrefabs; // プロットアイコンのプレハブ
    public Transform[] plotIconPositions; // plotIconの位置


    public GameObject tooltipPrefab; // 提示枠のプレハブ
    public Material horizontalLineMaterial; // 横線のマテリアル
    public float horizontalLineWidth = 0.1f; // 横線の幅
    private Dictionary<(GameObject, GameObject), GameObject> horizontalLines = new Dictionary<(GameObject, GameObject), GameObject>(); // 横線の辞書
    public float hoverAreaWidth = 0.4f; // 横線のホバーエリアの縦幅


    private List<GameObject> lines = new List<GameObject>(); // 線オブジェクトのリスト
    private List<GameObject> points = new List<GameObject>(); // 点オブジェクトのリスト

    public Dictionary<int, Vector3> pointsDictionary;// 点とその位置情報を格納する辞書

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

        // 複数の線を生成
        for (int i = 0; i < numberOfLines; i++)
        {
            Vector3 startPoint = initialStartPoint + new Vector3(i * 2, 0, 0); // 横にオフセット
            Vector3 endPoint = initialEndPoint + new Vector3(i * 2, 0, 0);

            pointsDictionary.Add(i, startPoint); // 点の辞書にそれぞれの線のスタート点を番号付けて位置情報を格納する
            pointsDictionary.Add(i + numberOfLines * (pointsPerLine + 1), endPoint); // 点の辞書にそれぞれの線のエンド点を番号付けて位置情報を格納する


            CreateLine(startPoint, endPoint, i); // CreateLine関数を実行する

        }

        // 生成された全ての点の情報をログに出力
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

        // 線のための新しい GameObject を作成
        GameObject lineObject = new GameObject("Line" + lineNumber);
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        // 線のマテリアルと幅を設定
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // 線の起点と終点を設定
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);

        Debug.Log($"Line {lineNumber }: Start Point = {startPoint}, End Point = {endPoint}");

        

        // 円（点）の位置を計算し、生成
        for (int i = 0; i < pointsPerLine; i++)
        {
            Vector3 circlePosition = startPoint + new Vector3(0, (endPoint.y - startPoint.y) / (pointsPerLine + 1) * (i + 1), 0);
            GameObject circleObject = Instantiate(circlePrefab, circlePosition, Quaternion.identity);
            circleObject.transform.parent = lineObject.transform;
            points.Add(circleObject);


            // 番号情報を追加
            circleObject.name = $"Circle_Line{lineNumber}_Point{i}";
            Debug.Log($"Line {lineNumber}: Created Circle at {circlePosition} with ID ({lineNumber}, {i})");

            pointsDictionary.Add(lineIndex + (i + 1) * numberOfLines, circlePosition);

            

            // Add hover area for each point pair
            if (points.Count > 3)
            {
                Debug.Log($"Creating Hover Area for point pair: {points[points.Count - 2].name}, {points[points.Count - 1].name}");
                CreateHoverAreaT4(points[points.Count - 3], points[points.Count - 1]);
                /*
                 * "if (points.Count > 3)"----->  これらのコードを実行する時、pointの数が既に4に達しているということ
                 * points[points.Count - 3]＝points[1]
                 * points[points.Count - 1]＝points[3]
                 
                 * 前の線上の点を加えた後で次の線の点を追加するため、

                    points[0]→line1_point0
                    points[1]→line1_point1
                    points[2]→line2_point0
                    points[3]→line2_point1
                    
                    　　　　「騎士」0　　　　　「猟師」1
                                ||                ||
                     points[0]  ○2    points[2]  ○3
                                ||                ||
                     points[1]  ○4    points[3]  ○5
                                ||                ||
                　　　　　　「結末」6　　　　　「結末」7
                */

            }

        }



        // キャラクターを生成
        if (lineIndex < characterPrefabs.Length)
        {
            Vector3 characterPosition = startPoint + new Vector3(0, 1, -0.1f);
            GameObject characterObject = Instantiate(characterPrefabs[lineIndex], characterPosition, Quaternion.identity);

            string gameObjectName = $"character{lineNumber}";
            GameObject parentGameObject = GameObject.Find(gameObjectName);
            if (parentGameObject != null)
            {
                // もし見つけたら、characterObjectのTransformを見つかられたGameObjectのTransformの子供オブジェクトに設置
                characterObject.transform.parent = parentGameObject.transform;
                characterObject.transform.position = parentGameObject.transform.position + new Vector3(0, offset, 0);
            }
            else
            {
                // もし見つけられなかったら、エラーをデバッグする
                Debug.LogError($"GameObject '{gameObjectName}' not found!");
            }
            characterObject.name = $"Character{lineNumber}";

            Debug.Log($"Line {lineNumber }: Created {characterObject.name} at {characterPosition}");
        }
        else
        {
            Debug.LogWarning($"Line {lineNumber }: No character prefab available for line number {lineNumber}");
        }



        // 結末アイコンを生成
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

    private void DrawHorizontalLine(int start,int end)
    {
        // 線のための新しい GameObject を作成
        GameObject lineObject = new GameObject("HorizontalLine" + 1);
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        // 線のマテリアルと幅を設定
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

        //横線を描く
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

        HoverAreaT4 hoverScriptT4 = hoverArea.AddComponent<HoverAreaT4>();                    // Need changed NOTICE！
        hoverScriptT4.Initialize(pointA, pointB, horizontalLineMaterial, horizontalLineWidth, tooltipPrefab);

        Debug.Log($"HoverArea script added to {hoverArea.name}");

        Debug.Log($"Hover Area created at {midPoint} with size {boxCollider.size}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
