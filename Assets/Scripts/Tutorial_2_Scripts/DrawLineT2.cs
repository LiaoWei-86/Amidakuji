using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineT2 : MonoBehaviour
{
    public Material lineMaterial; // 線のマテリアル
    public float lineWidth = 0.1f; // 線の幅
    public Vector3 startPoint = new Vector3(0, 0, 0); // 起点
    public Vector3 endPoint = new Vector3(0, 5, 0); // 終点

    public GameObject circlePrefab; // 円(点)のプレハブ

    public GameObject characterPrefab; //　キャラクターのプレハブ
    private Vector3 characterPosition; // キャラクターの位置
    private Vector3 targetPosition; // ターゲット位置
    private GameObject characterObject; //　キャラクター用に新しく作成された GameObject
    public float speed = 1.0f; // キャラクターの移動速度

    public GameObject endingPrefab; //　結末アイコンのプレハブ

    //public GameObject plotIconPrefab; //　プロットアイコンのプレハブ
    //private Vector3 plotIconPosition; // プロットアイコンの位置
    //private GameObject plotIconObject; //　プロットアイコン用に新しく作成された GameObject


    public GameObject T2TLcontrollerGameObject; // T2TLcontrollerスクリプトのisCharacterMovingブール値を取得するため
    public T2TLcontroller T2TLcontrollerScript; // T2TLcontrollerスクリプトの参照を格納するため

    void Start()
    {
        // T2TLcontrollerスクリプトの参照を取得
        T2TLcontrollerScript = T2TLcontrollerGameObject.GetComponent<T2TLcontroller>();

        // 線のための新しい GameObject を作成
        GameObject lineObject = new GameObject("Line");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        // 線のマテリアルと幅を設定
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // 線の起点と終点を設定
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);

        // 円の位置を計算（線の中点）
        Vector3 circlePosition = (startPoint + endPoint) / 2;

        // 円のための新しい GameObject を作成
        GameObject circleObject = Instantiate(circlePrefab, circlePosition, Quaternion.identity);

        // 円を線のオブジェクトの子オブジェクトとして設定
        circleObject.transform.parent = lineObject.transform;


        // キャラクター
        characterPosition = (startPoint + new Vector3(0,1,-0.1f)); //　キャラクターの位置を計算（startPointの上）
        characterObject = Instantiate(characterPrefab, characterPosition, Quaternion.identity); 
        characterObject.transform.parent = lineObject.transform; // キャラクターを線のオブジェクトの子オブジェクトとして設定
        // キャラクターの移動先位置を計算（endPoint）
        targetPosition = endPoint+(new Vector3(0,0,-0.1f)); // 画像が見れるためにｚが-0.1fにした


        // 結末アイコン
        Vector3 endingPosition = (endPoint + new Vector3(0, -1, 0)); // 結末アイコンの位置を計算（endPointの下）
        GameObject endingObject = Instantiate(endingPrefab, endingPosition, Quaternion.identity); // 結末アイコンのための新しい GameObject を作成
        endingObject.transform.parent = lineObject.transform; // 結末アイコンを線のオブジェクトの子オブジェクトとして設定

        //// プロットアイコン
        //plotIconPosition = ((circlePosition + endPoint) / 2 + new Vector3(1, 0, 0)); //　プロットアイコンの位置を計算（circleと終点の中間点の右）
        //plotIconObject = Instantiate(plotIconPrefab, plotIconPosition, Quaternion.identity); 
        //plotIconObject.transform.parent = lineObject.transform; 
        //plotIconObject.SetActive(false); // プロットアイコンを非表示にする

    }


    // Update is called once per frame
    void Update()
    {
        if (T2TLcontrollerScript != null && T2TLcontrollerScript.isCharacterMoving)
        {
            // キャラクターの移動アニメーションを実行
            Debug.Log("isCharacterMoving is true, start moving the character.");

            // 移動中だったら：
            if (T2TLcontrollerScript.isCharacterMoving)
            {
                // キャラクターの現在位置を表示
                Debug.Log("Current character position: " + characterObject.transform.position);


                // 移動方向を計算
                Vector3 direction = (targetPosition - transform.position).normalized;

                // 毎フレームの移動距離を計算
                float step = speed * Time.deltaTime;
                // キャラクターの位置を更新
                Vector3 position = characterObject.transform.position;
                Vector3 pos = Vector3.MoveTowards(position, targetPosition, step);
                characterObject.transform.position = pos;

                // ターゲット位置に到達したかどうかをチェック
                if (Vector3.Distance(characterObject.transform.position, targetPosition) < 0.001f)
                {
                    T2TLcontrollerScript.isCharacterMoving = false; // 移動を停止
                }

                //// キャラクターのy値がplotIconPositionのy値と等しいかどうかをチェック
                //if (Mathf.Abs(characterObject.transform.position.y - plotIconPosition.y) < 0.05f)
                //{
                //    Debug.Log("Character reached plot icon y position");
                //    plotIconObject.SetActive(true); // プロットアイコンを表示
                //}

                //// plotIconPositionのy値を表示
                //Debug.Log("Plot icon y position: " + plotIconPosition.y);
                // characterObjectのy値を表示
                Debug.Log("Character y position: " + characterObject.transform.position.y);

            }

        }
    }
}
