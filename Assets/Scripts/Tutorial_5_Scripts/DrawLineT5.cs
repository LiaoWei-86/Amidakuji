using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DrawLineT5 : MonoBehaviour
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
    public GameObject charaInfoPrefab;

    public Material horizontalLineMaterial; // 横線のマテリアル
    public float horizontalLineWidth = 0.1f; // 横線の幅
    private Dictionary<(GameObject, GameObject), GameObject> horizontalLines = new Dictionary<(GameObject, GameObject), GameObject>(); // 横線の辞書
    public float hoverAreaWidth = 0.4f; // 横線のホバーエリアの縦幅

    private List<GameObject> lines = new List<GameObject>(); // 線オブジェクトのリスト
    private List<GameObject> points = new List<GameObject>(); // 点オブジェクトのリスト

    public Dictionary<int, Vector3> pointsDictionary;// 点とその位置情報を格納する辞書

    public float offset = -0.1f;  // キャラクターのオフセット


    public TMP_FontAsset dotFont;  // ドットフォント

    public T5TLcontroller T5TLcontrollerScript;
    public characterInfoHoverT5 characterInfoHoverT5Script;

    // Start is called before the first frame update
    void Start()
    {
        if (T5TLcontrollerScript != null)
        {
            T5TLcontrollerScript = FindObjectOfType<T5TLcontroller>();
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

            // 生成された全ての点の情報をログに出力
            foreach (var point in points)
            {
                Debug.Log($"Point: {point.name}, Position: {point.transform.position}");
            }

            // 生成された全ての点に番号付けてそれぞれの位置情報をKeyValuePair<int, Vector3>の形でディクショナリーに保存した
            // それを確認するために、保存された点の番号と位置情報をログに出力
            foreach (KeyValuePair<int, Vector3> kvp in pointsDictionary)
            {
                Debug.Log($"PointsKey: {kvp.Key}, PointsTransformPositionVector3: {kvp.Value}");
            }

            
        }

        // 横線を描画
        DrawHorizontalLine(2, 3);
        DrawHorizontalLine(4, 5);


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


            // 番号生成情報
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
                 * "if (points.Count > 7)"----->  これらのコードを実行する時、pointの数が既に 8 に達しているということ
                 
                 * 前の線上の点を加えた後で次の線の点を追加するため、

                    points[0]→line1_point0
                    points[1]→line1_point1
                    points[2]→line2_point0
                    points[3]→line2_point1
                    
                    　　　　「騎士」0　　　　　「猟師」1
                                ||                ||
                    points[0]   ○2    points[4]  ○3
                                ||                ||
                    points[1]   ○4    points[5]  ○5
                                ||                ||
                    points[2]   ○6    points[6]  ○7
                                ||                ||
                    points[3]   ○8    points[7]  ○9
                                ||                ||
                　　　　　　「結末」10　　　　　「結末」11
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
                CreateHoverAreaCharacter(characterObject,lineNumber);
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

    // 横線を描画するメソッド
    private void DrawHorizontalLine(int start, int end)
    {
        // 線のための新しい GameObject を作成
        GameObject lineObject = new GameObject($"HorizontalLine({start},{end})");
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
            Debug.Log($"【DrawHorizontalLine】Key[{start}]Value[{startPoint}]");
        }
        else
        {
            // Key not found
            Debug.Log($"【DrawHorizontalLine】Key[{start}]Value[startPoint] not found.");
        }
        if (pointsDictionary.TryGetValue(end, out endPoint))
        {
            //Key found
            Debug.Log($"【DrawHorizontalLine】Key[{end}]Value[{endPoint}]");
        }
        else
        {
            // Key not found
            Debug.Log($"【DrawHorizontalLine】Key[{end}]Value[endPoint] not found.");
        }

        //横線を描く
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    // 点のペアごとにホバーエリアを生成するメソッド
    void CreateHoverAreaT5(GameObject pointA, GameObject pointB)
    {
        Debug.Log($"CreateHoverArea called with pointA: {pointA.name}, pointB: {pointB.name}");   // デバッグログを出力して、点Aと点Bの情報を表示する

        Vector3 midPoint = (pointA.transform.position + pointB.transform.position) / 2;   // 点Aと点Bの中間点を計算する
        Vector3 direction = (pointB.transform.position - pointA.transform.position).normalized;   // 点Aから点Bへの方向ベクトルを計算し、正規化する

        GameObject hoverArea = new GameObject("HoverArea");   // 新しい GameObject を作成し、その名前を "HoverArea" に設定する
        BoxCollider boxCollider = hoverArea.AddComponent<BoxCollider>();   // BoxCollider コンポーネントを追加し、ホバーエリアのサイズを設定する

        // 点Aと点Bの間の距離をホバーエリアの横幅に設定し、縦幅を hoverAreaWidth、厚みを 0.1f に設定する
        boxCollider.size = new Vector3(Vector3.Distance(pointA.transform.position, pointB.transform.position), hoverAreaWidth, 0.1f);

        // BoxCollider をトリガーとして設定する
        boxCollider.isTrigger = true;

        Debug.Log($"BoxCollider created with size: {boxCollider.size}");   // BoxCollider のサイズをデバッグログで表示する

        hoverArea.transform.position = midPoint;   // ホバーエリアの位置を中間点に設定する
        hoverArea.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);   // ホバーエリアの回転を、点Aから点Bへの方向に基づいて設定する

        // HoverAreaT5 スクリプトをホバーエリアに追加する
        HoverAreaT5 hoverScriptT5 = hoverArea.AddComponent<HoverAreaT5>();         // Need changed NOTICE！
        hoverScriptT5.Initialize(pointA, pointB, horizontalLineMaterial, horizontalLineWidth, tooltipPrefab);   // HoverAreaT5 スクリプトを初期化する

        // HoverAreaT5 スクリプトが追加されたことをデバッグログで表示する
        Debug.Log($"HoverArea script added to {hoverArea.name}");

        // ホバーエリアが作成された位置とサイズをデバッグログで表示する
        Debug.Log($"Hover Area created at {midPoint} with size {boxCollider.size}");
    }

    // キャラクターのホバーエリアを生成するメソッド
    void CreateHoverAreaCharacter(GameObject character, int charaInfoNum)
    {
        // デバッグログを出力して、キャラクターの情報を表示する
        Debug.Log($"CreateHoverArea called with character: {character.name}");

        // キャラクターの位置からオフセットを加えた位置を計算する
        Vector3 charaInfoPosition = character.transform.position + new Vector3(-3,-1,0);

        // キャラクターに BoxCollider コンポーネントを追加し、ホバーエリアのサイズを設定する
        BoxCollider boxCollider = character.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(1f, 1f, 0.1f);
        boxCollider.isTrigger = true;

        // characterInfoHoverT5 スクリプトをキャラクターに追加する
        characterInfoHoverT5 characterInfoHoverT5Script = character.AddComponent<characterInfoHoverT5>();

        // characterInfoHoverT5 スクリプトの情報番号を設定する
        characterInfoHoverT5Script.charaInfoNum = charaInfoNum;

        // characterInfoHoverT5 スクリプトを初期化する
        characterInfoHoverT5Script.Initialize(character, charaInfoPrefab, charaInfoPosition);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
