using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HoverAreaT4 : MonoBehaviour
{
    private GameObject pointA;
    private GameObject pointB;
    private Material horizontalLineMaterial;
    private float horizontalLineWidth;
    private GameObject tooltipPrefab;
    private GameObject tooltipInstance;
    private GameObject currentLine;

    public T4TLcontroller T4TLcontrollerScript;
    public DrawLineT4 DrawLineT4Script;

    private Dictionary<int, int[]> horizontalLines = new Dictionary<int, int[]>();

    // Start is called before the first frame update
    void Start()
    {
        if (DrawLineT4Script == null)
        {
            DrawLineT4Script = FindObjectOfType<DrawLineT4>();
        }

        if (T4TLcontrollerScript == null)
        {
            T4TLcontrollerScript = FindObjectOfType<T4TLcontroller>();
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
                T4TLcontrollerScript.isHorizontalLineCreated = true;
                Debug.Log("T4TLcontrollerScript.isHorizontalLineCreated" + T4TLcontrollerScript.isHorizontalLineCreated);
            }
        }
        else if (Input.GetMouseButtonDown(1)) // ÓÒ¥¯¥ê¥Ã¥¯¤Çºá¾€¤òÏ÷³ý
        {
            if (currentLine != null)
            {
                Destroy(currentLine);
                currentLine = null;

                Debug.Log("Horizontal line destroyed");
                T4TLcontrollerScript.isHorizontalLineCreated = false;
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
            Gizmos.DrawWireCube(midPoint, new Vector3(Vector3.Distance(pointA.transform.position, pointB.transform.position), DrawLineT4Script.hoverAreaWidth, 0.1f));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
