using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HoverArea : MonoBehaviour
{
    private GameObject pointA; // �ᾀ��ʼ��Ȥʤ�ݥ����A
    private GameObject pointB; // �ᾀ�νK��Ȥʤ�ݥ����B
    private Material horizontalLineMaterial; // �ᾀ�Υޥƥꥢ��򱣳�
    private float horizontalLineWidth; // �ᾀ�η����O�����뤿��Ή���
    private GameObject tooltipPrefab; // �ĩ`����åפΥץ�ϥ֤򱣳�
    private GameObject tooltipInstance; // ���󥹥��󥹻����줿�ĩ`����åפ򱣳�
    private GameObject currentLine; // �F�ڤκᾀ���֥������Ȥ򱣳�

    public T3TLcontroller T3TLcontrollerScript; // T3TLcontroller������ץȤβ��դ��{���뤿��
    public DrawLineT3 DrawLineT3Script; // DrawLineT3������ץȤβ��դ��{���뤿��

    // �¤����ե��`��ɤ�׷�Ӥ��ơ����ɤ��줿�ᾀ������ӛ�h���ޤ�
    private Dictionary<int, int[]> horizontalLines = new Dictionary<int, int[]>(); // �ᾀ�����򱣴椹��ǥ�������ʥ�

    void Start()
    {
        //������ץȤβ��դ�ȡ��
        if (DrawLineT3Script != null)
        {
            DrawLineT3Script = FindObjectOfType<DrawLineT3>(); // DrawLineT3������ץȤ򥷩`���ڤ���̽���Ʋ��դ�ȡ��
        }

        if (T3TLcontrollerScript == null)
        {
            T3TLcontrollerScript = FindObjectOfType<T3TLcontroller>(); // T3TLcontroller������ץȤ򥷩`���ڤ���̽���Ʋ��դ�ȡ��
        }

    }

    // HoverArea���饹�γ��ڻ��I��
    public void Initialize(GameObject pointA, GameObject pointB, Material horizontalLineMaterial, float horizontalLineWidth, GameObject tooltipPrefab)
    {
        // �������ܤ�ȡ�ä�������Љ����˴���
        this.pointA = pointA;
        this.pointB = pointB;
        this.horizontalLineMaterial = horizontalLineMaterial;
        this.horizontalLineWidth = horizontalLineWidth;
        this.tooltipPrefab = tooltipPrefab;

        // ���ڻ����줿���������
        Debug.Log($"HoverArea initialized with points {pointA.name} and {pointB.name}, material {horizontalLineMaterial.name}, width {horizontalLineWidth}, tooltip prefab {tooltipPrefab.name}");
    }

    // �ޥ��������֥������Ȥ���ä��H�΄I��
    void OnMouseEnter()
    {
        if (tooltipPrefab != null)
        {
            // �ĩ`����åפ��ʾ���뤿��Υ��󥹥��󥹤�����
            tooltipInstance = Instantiate(tooltipPrefab, transform.position, Quaternion.identity);
            Debug.Log($"Tooltip instantiated at {transform.position}");
        }
    }

    // �ޥ��������֥������Ȥ����x�줿�H�΄I��
    void OnMouseExit()
    {
        if (tooltipInstance != null)
        {
            // ��ʾ����Ƥ���ĩ`����åפ�����
            Destroy(tooltipInstance);
            Debug.Log("Tooltip destroyed");
        }
    }

    // �ޥ��������֥��������Ϥˤ����H�΄I��
    void OnMouseOver()
    {
        // �󥯥�å��Ǻᾀ�����ɤޤ�������
        if (Input.GetMouseButtonDown(0)) // �󥯥�å���Ѻ���줿�r
        {
            if (currentLine == null)
            {
                CreateHorizontalLine(); // �ᾀ�����ɤ���
                Debug.Log("Horizontal line created");
                T3TLcontrollerScript.isHorizontalLineCreated = true; // �ᾀ�����ɤ��줿���Ȥ�ե饰�ǹ���
            }
        }
        else if (Input.GetMouseButtonDown(1)) // �ҥ���å���Ѻ���줿�r
        {
            if (currentLine != null)
            {
                // �ȴ�κᾀ����������
                Destroy(currentLine);
                currentLine = null;

                Debug.Log("Horizontal line destroyed");
                T3TLcontrollerScript.isHorizontalLineCreated = false; // �ᾀ���������줿���Ȥ�ե饰�ǹ���
            }
        }
    }

    // �ᾀ�����ɤ����v��
    void CreateHorizontalLine()
    {
        // �¤����ᾀ�äΥ��`�४�֥������Ȥ�����
        GameObject lineObject = new GameObject("HorizontalLine");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>(); // LineRenderer����ݩ`�ͥ�Ȥ�׷��

        // LineRenderer���O�����Ф�
        lineRenderer.material = horizontalLineMaterial; // �ᾀ�Υޥƥꥢ����O��
        lineRenderer.startWidth = horizontalLineWidth; // �ᾀ���_ʼ�ص�η����O��
        lineRenderer.endWidth = horizontalLineWidth; // �ᾀ�νK��η����O��
        lineRenderer.positionCount = 2; // 픵������2���O����ʼ��ȽK�㣩
        lineRenderer.SetPosition(0, pointA.transform.position); // ʼ���λ�ä��O��
        lineRenderer.SetPosition(1, pointB.transform.position); // �K���λ�ä��O��

        currentLine = lineObject; // ���ɤ����ᾀ��currentLine�˱���

        Debug.Log($"Horizontal line created between {pointA.name} and {pointB.name}");
    }

    // Gizmos�ǥǥХå��軭���Ф��v��
    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.yellow; // ����ɫ���ɫ���O��
            Gizmos.DrawLine(pointA.transform.position, pointB.transform.position); // ʼ��ȽK���Y�־����軭

            // �ۥЩ`���ꥢ���軭����
            Vector3 midPoint = (pointA.transform.position + pointB.transform.position) / 2; // ʼ��ȽK������g�ص��Ӌ��
            Gizmos.DrawWireCube(midPoint, new Vector3(Vector3.Distance(pointA.transform.position, pointB.transform.position), DrawLineT3Script.hoverAreaWidth, 0.1f)); // ���g�ص���Ľ��Τ��軭
        }
    }

}
