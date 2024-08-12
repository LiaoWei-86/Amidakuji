using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class HoverArea : MonoBehaviour
{
    private GameObject pointA;
    private GameObject pointB;
    private Material horizontalLineMaterial;
    private float horizontalLineWidth;
    private GameObject tooltipPrefab;
    private GameObject tooltipInstance;
    private GameObject currentLine;

    public T3TLcontroller T3TLcontrollerScript; // T3TLcontrollerスクリプトの参照を格納するため
    public DrawLineT3 DrawLineT3Script; // DrawLineT3スクリプトの参照を格納するため

    // 新しいフィールドを追加して、作成された横線の情報を記録します
    private Dictionary<int, int[]> horizontalLines = new Dictionary<int, int[]>();

    void Start()
    {
        //スクリプトの参照を取得

        if(DrawLineT3Script != null)
        {
            DrawLineT3Script = FindObjectOfType<DrawLineT3>();
        }

        if (T3TLcontrollerScript == null)
        {
            T3TLcontrollerScript = FindObjectOfType<T3TLcontroller>();
        }

    }
    

    public void Initialize(GameObject pointA, GameObject pointB, Material horizontalLineMaterial, float horizontalLineWidth, GameObject tooltipPrefab)
    {
        this.pointA = pointA;
        this.pointB = pointB;
        this.horizontalLineMaterial = horizontalLineMaterial;
        this.horizontalLineWidth = horizontalLineWidth;
        this.tooltipPrefab = tooltipPrefab;

        Debug.Log($"HoverArea initialized with points {pointA.name} and {pointB.name}, material {horizontalLineMaterial.name}, width {horizontalLineWidth}, tooltip prefab {tooltipPrefab.name}");
    }

    void OnMouseEnter()
    {
        if (tooltipPrefab != null)
        {
            tooltipInstance = Instantiate(tooltipPrefab, transform.position, Quaternion.identity);
            Debug.Log($"Tooltip instantiated at {transform.position}");
        }
    }

    void OnMouseExit()
    {
        if (tooltipInstance != null)
        {
            Destroy(tooltipInstance);
            Debug.Log("Tooltip destroyed");
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリックで横線を生成または削除
        {
            if (currentLine == null)
            {
                CreateHorizontalLine();
                Debug.Log("Horizontal line created");
                T3TLcontrollerScript.isHorizontalLineCreated = true;
            }
        }
        else if (Input.GetMouseButtonDown(1)) // 右クリックで横線を削除
        {
            if (currentLine != null)
            {
                Destroy(currentLine);
                currentLine = null;

                Debug.Log("Horizontal line destroyed");
                T3TLcontrollerScript.isHorizontalLineCreated = false;
            }
        }
    }

    void CreateHorizontalLine()
    {
        GameObject lineObject = new GameObject("HorizontalLine");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        lineRenderer.material = horizontalLineMaterial;
        lineRenderer.startWidth = horizontalLineWidth;
        lineRenderer.endWidth = horizontalLineWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, pointA.transform.position);
        lineRenderer.SetPosition(1, pointB.transform.position);

        currentLine = lineObject;

        Debug.Log($"Horizontal line created between {pointA.name} and {pointB.name}");

        
    }

    

    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);

            // Draw the hover area
            Vector3 midPoint = (pointA.transform.position + pointB.transform.position) / 2;
            Gizmos.DrawWireCube(midPoint, new Vector3(Vector3.Distance(pointA.transform.position, pointB.transform.position), DrawLineT3Script.hoverAreaWidth, 0.1f));
        }
    }

}
