using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HoverAreaT5 : MonoBehaviour
{
    private GameObject pointA;
    private GameObject pointB;
    private Material horizontalLineMaterial;
    private float horizontalLineWidth;
    private GameObject tooltipPrefab;
    private GameObject tooltipInstance;
    private GameObject currentLine;

    public T5TLcontroller T5TLcontrollerScript;
    public DrawLineT5 DrawLineT5Script;

    private Dictionary<int, int[]> horizontalLines = new Dictionary<int, int[]>();

    // Start is called before the first frame update
    void Start()
    {
        if (DrawLineT5Script == null)
        {
            DrawLineT5Script = FindObjectOfType<DrawLineT5>();
        }

        if (T5TLcontrollerScript == null)
        {
            T5TLcontrollerScript = FindObjectOfType<T5TLcontroller>();
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
        if (Input.GetMouseButtonDown(0)) // ×ó¥¯¥ê¥Ã¥¯¤Çºá¾€¤òÉú³É¤Þ¤¿¤ÏÏ÷³ý
        {
            if (currentLine == null)
            {
                CreateHorizontalLine();
                Debug.Log("Horizontal line created");
                //T5TLcontrollerScript.isHorizontalLineCreated = true;
                //Debug.Log("T4TLcontrollerScript.isHorizontalLineCreated" + T5TLcontrollerScript.isHorizontalLineCreated);
            }
        }
        else if (Input.GetMouseButtonDown(1)) // ÓÒ¥¯¥ê¥Ã¥¯¤Çºá¾€¤òÏ÷³ý
        {
            if (currentLine != null)
            {
                Destroy(currentLine);
                currentLine = null;

                Debug.Log("Horizontal line destroyed");
                //T5TLcontrollerScript.isHorizontalLineCreated = false;
            }
        }
    }

    void CreateHorizontalLine()
    {
        GameObject lineObject = new GameObject($"HorizontalLine({pointA.name},{pointB.name})");
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
            Gizmos.DrawWireCube(midPoint, new Vector3(Vector3.Distance(pointA.transform.position, pointB.transform.position), DrawLineT5Script.hoverAreaWidth, 0.1f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
