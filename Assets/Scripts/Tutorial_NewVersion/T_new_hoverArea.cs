using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_new_hoverArea : MonoBehaviour
{
    private GameObject pointA; // 横線の始点となるポイントA
    private GameObject pointB; // 横線の終点となるポイントB
    private Material horizontalLineMaterial; // 横線のマテリアルを保持
    private float horizontalLineWidth; // 横線の幅を設定するための変数
    private GameObject tooltipPrefab; // ツールチップのプレハブを保持
    private GameObject tooltipInstance; // インスタンス化されたツールチップを保持
    public GameObject currentLine; // 現在の横線オブジェクトを保持

    public T_new_gameController T_new_GameController_script; // T_new_gameControllerスクリプトの参照を格納するため

    // Start is called before the first frame update
    void Start()
    {
        if (T_new_GameController_script == null)
        {
            T_new_GameController_script = FindObjectOfType<T_new_gameController>(); // J0_gameControllerスクリプトをシーン内から探して参照を取得
        }
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
        // 左クリックで横線を生成
        if (Input.GetMouseButtonDown(0)) // 左クリックが押された時
        {
            
            if (currentLine == null)
            {
                if (!T_new_GameController_script.cannot_createLine)
                {
                    CreateHorizontalLine(); // 横線を生成する
                    Debug.Log("Horizontal line created");
                    T_new_GameController_script.audioSourceTn.PlayOneShot(T_new_GameController_script.leftClickClip);


                    if (pointA.name == "circle1")
                    {
                        T_new_GameController_script._isHoriz_line_Created = true;
                        Debug.Log("T_new_GameController_script.isHorizontal_1_LineCreated: " + T_new_GameController_script._isHoriz_line_Created);

                        if (!T_new_GameController_script.has_cleared_once && !T_new_GameController_script.has_text_horizontalLineCreated_pd_Played)
                        {
                            T_new_GameController_script.text_horizontalLineCreated.SetActive(true);
                            T_new_GameController_script.currentGameMode = T_new_gameController.GameMode.TextPlaying;
                        }
                       
                        if (T_new_GameController_script.text_mustEncounter.activeSelf)
                        {
                            T_new_GameController_script.text_mustEncounter.SetActive(false);
                        }
                        if (T_new_GameController_script.text_mustEncounter_1.activeSelf)
                        {
                            T_new_GameController_script.text_mustEncounter_1.SetActive(false);
                        }
                        if (T_new_GameController_script.pleaseClick.activeSelf)
                        {
                            T_new_GameController_script.pleaseClick.SetActive(false);
                        }
                        if (T_new_GameController_script.failedRoute)
                        {
                            if (T_new_GameController_script.currentMovementIndex > 0)
                            {
                                T_new_GameController_script.currentMovementIndex--;
                            }
                            
                            T_new_GameController_script.failedRoute = false;
                            T_new_GameController_script.clearedRoute = true;
                            Debug.Log($"failedRoute:{T_new_GameController_script.failedRoute};clearedRoute:{T_new_GameController_script.clearedRoute}");
                            Debug.Log($"currentMovementIndex after clicked:{T_new_GameController_script.currentMovementIndex}");
                        }

                        if (T_new_GameController_script.text_noLineCreated.activeSelf)
                        {
                            T_new_GameController_script.text_noLineCreated.SetActive(false);
                        }

                        
                    }
                }
                else if (T_new_GameController_script.cannot_createLine)
                {
                    T_new_GameController_script.audioSourceTn.PlayOneShot(T_new_GameController_script.missClip);
                }
            }    

        }
        else if (Input.GetMouseButtonDown(1)) // 右クリックが押された時
        {
            if (currentLine != null)
            {
                // 既存の横線を削除する
                if (!T_new_GameController_script.cannnot_cancell)
                {
                    
                    Destroy(currentLine);
                    currentLine = null;
                    T_new_GameController_script.audioSourceTn.PlayOneShot(T_new_GameController_script.rightClickClip);

                    Debug.Log("Horizontal line destroyed");

                    if (pointA.name == "circle1")
                    {
                        T_new_GameController_script._isHoriz_line_Created = false; // 横線が削除されたことをフラグで管理

                        if (T_new_GameController_script.text_horizontalLineCreated.activeSelf)
                        {
                            T_new_GameController_script.text_horizontalLineCreated.SetActive(false);
                        }
                        if (T_new_GameController_script.clearedRoute)
                        {
                            T_new_GameController_script.currentMovementIndex++;
                            T_new_GameController_script.failedRoute = true;
                            T_new_GameController_script.clearedRoute = false;
                            Debug.Log($"failedRoute:{T_new_GameController_script.failedRoute};clearedRoute:{T_new_GameController_script.clearedRoute}");
                            Debug.Log($"currentMovementIndex after clicked:{T_new_GameController_script.currentMovementIndex}");
                        }

                       
                    }
                    T_new_GameController_script.currentGameMode = T_new_gameController.GameMode.PlayerPlaying;
                }
                else if (T_new_GameController_script.cannnot_cancell)
                {
                    T_new_GameController_script.audioSourceTn.PlayOneShot(T_new_GameController_script.missClip);
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
            Gizmos.DrawWireCube(midPoint, new Vector3(Vector3.Distance(pointA.transform.position, pointB.transform.position), T_new_GameController_script.hoverAreaWidth, 0.1f)); // 中間地点に四角形を描画
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
