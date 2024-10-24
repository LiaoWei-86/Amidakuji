using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act_1_2_hoverArea : MonoBehaviour
{
    private GameObject pointA; // 横線の始点となるポイントA
    private GameObject pointB; // 横線の終点となるポイントB
    private Material horizontalLineMaterial; // 横線のマテリアルを保持
    private float horizontalLineWidth; // 横線の幅を設定するための変数
    private GameObject tooltipPrefab; // ツールチップのプレハブを保持
    private GameObject tooltipInstance; // インスタンス化されたツールチップを保持
    private GameObject currentLine; // 現在の横線オブジェクトを保持

    public Act_1_2_gameController Act_1_2_gameControllerScript;

    private Dictionary<int, int[]> horizontalLines = new Dictionary<int, int[]>(); // 横線の情報を保存するディクショナリ

    // Start is called before the first frame update
    void Start()
    {
        if (Act_1_2_gameControllerScript == null)
        {
            Act_1_2_gameControllerScript = FindObjectOfType<Act_1_2_gameController>(); //  Act_1_2_gameControllerスクリプトをシーン内から探して参照を取得
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // HoverAreaクラスの初期化処理
    public void Initialize(GameObject pointA, GameObject pointB, Material horizontalLineMaterial, float horizontalLineWidth, GameObject tooltipPrefab)
    {
        // 引数で受け取った値をメンバ変数に代入
        this.pointA = pointA;
        this.pointB = pointB;
        this.horizontalLineMaterial = horizontalLineMaterial;
        this.horizontalLineWidth = horizontalLineWidth;
        this.tooltipPrefab = tooltipPrefab;

        // 初期化された情報をログ出力
        Debug.Log($"HoverArea initialized with points {pointA.name} and {pointB.name}, material {horizontalLineMaterial.name}, width {horizontalLineWidth}, tooltip prefab {tooltipPrefab.name}");
    }

    // マウスがオブジェクトに入った際の処理
    void OnMouseEnter()
    {
        if (tooltipPrefab != null)
        {
            // ツールチップを表示するためのインスタンスを生成
            tooltipInstance = Instantiate(tooltipPrefab, transform.position, Quaternion.identity);
            Debug.Log($"Tooltip instantiated at {transform.position}");
        }
    }

    // マウスがオブジェクトから離れた際の処理
    void OnMouseExit()
    {
        if (tooltipInstance != null)
        {
            // 表示されているツールチップを削除
            Destroy(tooltipInstance);
            Debug.Log("Tooltip destroyed");
        }
    }

    // マウスがオブジェクト上にある際の処理
    void OnMouseOver()
    {
        // 左クリックで横線を生成または削除
        if (Input.GetMouseButtonDown(0)) // 左クリックが押された時
        {
            if (currentLine == null)
            {
                //CreateHorizontalLine(); // 横線を生成する
                //Debug.Log("Horizontal line created");
                //Act_1_2_gameControllerScript.audioSourceA1_2.PlayOneShot(Act_1_2_gameControllerScript.leftClickClip);


                if (pointA.name == "circle1" && Act_1_2_gameControllerScript.isHorizontal_2_LineCreated == false)
                {
                    CreateHorizontalLine(); // 横線を生成する
                    Debug.Log("Horizontal line created");
                    Act_1_2_gameControllerScript.isHorizontal_1_LineCreated = true;
                    Act_1_2_gameControllerScript.audioSourceA1_2.PlayOneShot(Act_1_2_gameControllerScript.leftClickClip);

                    Debug.Log("Act_1_2_gameControllerScript.isHorizontal_1_LineCreated: " + Act_1_2_gameControllerScript.isHorizontal_1_LineCreated);
                }
                else if (pointA.name == "circle1" && Act_1_2_gameControllerScript.isHorizontal_2_LineCreated == true)
                {
                    Act_1_2_gameControllerScript.audioSourceA1_2.PlayOneShot(Act_1_2_gameControllerScript.missClip);
                    Debug.Log("[×]Act_1_2_gameControllerScript.isHorizontal_1_LineCreated: " + Act_1_2_gameControllerScript.isHorizontal_1_LineCreated);
                }

                else if (pointA.name == "circle2" && Act_1_2_gameControllerScript.isHorizontal_1_LineCreated == false)
                {
                    CreateHorizontalLine(); // 横線を生成する
                    Debug.Log("Horizontal line created");
                    Act_1_2_gameControllerScript.isHorizontal_2_LineCreated = true;
                    Act_1_2_gameControllerScript.audioSourceA1_2.PlayOneShot(Act_1_2_gameControllerScript.leftClickClip);
                    Debug.Log("Act_1_2_gameControllerScript.isHorizontal_2_LineCreated: " + Act_1_2_gameControllerScript.isHorizontal_2_LineCreated);
                }
                else if(pointA.name == "circle2" && Act_1_2_gameControllerScript.isHorizontal_1_LineCreated == true)
                {
                    Act_1_2_gameControllerScript.audioSourceA1_2.PlayOneShot(Act_1_2_gameControllerScript.missClip);
                    Debug.Log("[×]Act_1_2_gameControllerScript.isHorizontal_2_LineCreated: " + Act_1_2_gameControllerScript.isHorizontal_2_LineCreated);
                }

                else if(pointA.name == "circle4" && Act_1_2_gameControllerScript.isHorizontal_4_LineCreated == false)
                {
                    CreateHorizontalLine(); // 横線を生成する
                    Debug.Log("Horizontal line created");
                    Act_1_2_gameControllerScript.isHorizontal_3_LineCreated = true;
                    Act_1_2_gameControllerScript.audioSourceA1_2.PlayOneShot(Act_1_2_gameControllerScript.leftClickClip);
                    Debug.Log("Act_1_2_gameControllerScript.isHorizontal_3_LineCreated: " + Act_1_2_gameControllerScript.isHorizontal_3_LineCreated);
                }
                else if (pointA.name == "circle4" && Act_1_2_gameControllerScript.isHorizontal_4_LineCreated == true)
                {
                    Act_1_2_gameControllerScript.audioSourceA1_2.PlayOneShot(Act_1_2_gameControllerScript.missClip);
                    Debug.Log("[×]Act_1_2_gameControllerScript.isHorizontal_3_LineCreated: " + Act_1_2_gameControllerScript.isHorizontal_3_LineCreated);
                }

                else if(pointA.name == "circle5" && Act_1_2_gameControllerScript.isHorizontal_3_LineCreated == false)
                {
                    CreateHorizontalLine(); // 横線を生成する
                    Debug.Log("Horizontal line created");
                    Act_1_2_gameControllerScript.isHorizontal_4_LineCreated = true;
                    Act_1_2_gameControllerScript.audioSourceA1_2.PlayOneShot(Act_1_2_gameControllerScript.leftClickClip);
                    Debug.Log("Act_1_2_gameControllerScript.isHorizontal_4_LineCreated: " + Act_1_2_gameControllerScript.isHorizontal_4_LineCreated);
                }
                else if (pointA.name == "circle5" && Act_1_2_gameControllerScript.isHorizontal_3_LineCreated == true)
                {
                    Act_1_2_gameControllerScript.audioSourceA1_2.PlayOneShot(Act_1_2_gameControllerScript.missClip);
                    Debug.Log("[×]Act_1_2_gameControllerScript.isHorizontal_4_LineCreated: " + Act_1_2_gameControllerScript.isHorizontal_4_LineCreated);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1)) // 右クリックが押された時
        {
            if (currentLine != null)
            {
                // 既存の横線を削除する
                Destroy(currentLine);
                currentLine = null;
                Act_1_2_gameControllerScript.audioSourceA1_2.PlayOneShot(Act_1_2_gameControllerScript.rightClickClip);

                Debug.Log("Horizontal line destroyed");

                if (pointA.name == "circle1")
                {
                    Act_1_2_gameControllerScript.isHorizontal_1_LineCreated = false;
                    Debug.Log("Act_1_2_gameControllerScript.isHorizontal_1_LineCreated: " + Act_1_2_gameControllerScript.isHorizontal_1_LineCreated);
                }
                else if (pointA.name == "circle2")
                {
                    Act_1_2_gameControllerScript.isHorizontal_2_LineCreated = false;
                    Debug.Log("Act_1_2_gameControllerScript.isHorizontal_2_LineCreated: " + Act_1_2_gameControllerScript.isHorizontal_2_LineCreated);
                }
                else if (pointA.name == "circle4")
                {
                    Act_1_2_gameControllerScript.isHorizontal_3_LineCreated = false;
                    Debug.Log("Act_1_2_gameControllerScript.isHorizontal_3_LineCreated: " + Act_1_2_gameControllerScript.isHorizontal_3_LineCreated);
                }
                else if (pointA.name == "circle5")
                {
                    Act_1_2_gameControllerScript.isHorizontal_4_LineCreated = false;
                    Debug.Log("Act_1_2_gameControllerScript.isHorizontal_4_LineCreated: " + Act_1_2_gameControllerScript.isHorizontal_4_LineCreated);
                }
            }
        }
    }
    // 横線を生成する関数
    void CreateHorizontalLine()
    {
        // 新しい横線用のゲームオブジェクトを作成
        GameObject lineObject = new GameObject("HorizontalLine");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>(); // LineRendererコンポーネントを追加

        // LineRendererの設定を行う
        lineRenderer.material = horizontalLineMaterial; // 横線のマテリアルを設定
        lineRenderer.startWidth = horizontalLineWidth; // 横線の開始地点の幅を設定
        lineRenderer.endWidth = horizontalLineWidth; // 横線の終点の幅を設定
        lineRenderer.positionCount = 2; // 頂点の数を2に設定（始点と終点）
        lineRenderer.SetPosition(0, pointA.transform.position); // 始点の位置を設定
        lineRenderer.SetPosition(1, pointB.transform.position); // 終点の位置を設定

        currentLine = lineObject; // 作成した横線をcurrentLineに保存

        Debug.Log($"Horizontal line created between {pointA.name} and {pointB.name}");
    }

    // Gizmosでデバッグ描画を行う関数
    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.yellow; // 線の色を黄色に設定
            Gizmos.DrawLine(pointA.transform.position, pointB.transform.position); // 始点と終点を結ぶ線を描画

            // ホバーエリアを描画する
            Vector3 midPoint = (pointA.transform.position + pointB.transform.position) / 2; // 始点と終点の中間地点を計算
            Gizmos.DrawWireCube(midPoint, new Vector3(Vector3.Distance(pointA.transform.position, pointB.transform.position), Act_1_2_gameControllerScript.hoverAreaWidth, 0.1f)); // 中間地点に四角形を描画
        }
    }
}
