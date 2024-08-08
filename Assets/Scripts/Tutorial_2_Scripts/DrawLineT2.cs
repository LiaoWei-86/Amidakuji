using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineT2 : MonoBehaviour
{
    public Material lineMaterial; // ���Υޥƥꥢ��
    public float lineWidth = 0.1f; // ���η�
    public Vector3 startPoint = new Vector3(0, 0, 0); // ���
    public Vector3 endPoint = new Vector3(0, 5, 0); // �K��

    public GameObject circlePrefab; // ��(��)�Υץ�ϥ�

    public GameObject characterPrefab; //������饯���`�Υץ�ϥ�
    private Vector3 characterPosition; // ����饯���`��λ��
    private Vector3 targetPosition; // ���`���å�λ��
    private GameObject characterObject; //������饯���`�ä��¤������ɤ��줿 GameObject
    public float speed = 1.0f; // ����饯���`���Ƅ��ٶ�

    public GameObject endingPrefab; //���Yĩ��������Υץ�ϥ�

    public GameObject plotIconPrefab; //���ץ�åȥ�������Υץ�ϥ�
    private Vector3 plotIconPosition; // �ץ�åȥ��������λ��
    private GameObject plotIconObject; //���ץ�åȥ��������ä��¤������ɤ��줿 GameObject


    public GameObject T2TLcontrollerGameObject; // T2TLcontroller������ץȤ�isCharacterMoving�֩`�낎��ȡ�ä��뤿��
    public T2TLcontroller T2TLcontrollerScript; // T2TLcontroller������ץȤβ��դ��{���뤿��

    void Start()
    {
        // T2TLcontroller������ץȤβ��դ�ȡ��
        T2TLcontrollerScript = T2TLcontrollerGameObject.GetComponent<T2TLcontroller>();

        // ���Τ�����¤��� GameObject ������
        GameObject lineObject = new GameObject("Line");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

        // ���Υޥƥꥢ��ȷ����O��
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // �������ȽK����O��
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);

        // �Ҥ�λ�ä�Ӌ�㣨�����е㣩
        Vector3 circlePosition = (startPoint + endPoint) / 2;

        // �ҤΤ�����¤��� GameObject ������
        GameObject circleObject = Instantiate(circlePrefab, circlePosition, Quaternion.identity);

        // �Ҥ򾀤Υ��֥������Ȥ��ӥ��֥������ȤȤ����O��
        circleObject.transform.parent = lineObject.transform;


        // ����饯���`
        characterPosition = (startPoint + new Vector3(0,1,-0.1f)); //������饯���`��λ�ä�Ӌ�㣨startPoint���ϣ�
        characterObject = Instantiate(characterPrefab, characterPosition, Quaternion.identity); 
        characterObject.transform.parent = lineObject.transform; // ����饯���`�򾀤Υ��֥������Ȥ��ӥ��֥������ȤȤ����O��
        // ����饯���`���Ƅ���λ�ä�Ӌ�㣨endPoint��
        targetPosition = endPoint+(new Vector3(0,0,-0.1f)); // ����Ҋ��뤿��ˣ���-0.1f�ˤ���


        // �Yĩ��������
        Vector3 endingPosition = (endPoint + new Vector3(0, -1, 0)); // �Yĩ���������λ�ä�Ӌ�㣨endPoint���£�
        GameObject endingObject = Instantiate(endingPrefab, endingPosition, Quaternion.identity); // �Yĩ��������Τ�����¤��� GameObject ������
        endingObject.transform.parent = lineObject.transform; // �Yĩ��������򾀤Υ��֥������Ȥ��ӥ��֥������ȤȤ����O��

        // �ץ�åȥ�������
        plotIconPosition = ((circlePosition + endPoint) / 2 + new Vector3(1, 0, 0)); //���ץ�åȥ��������λ�ä�Ӌ�㣨circle�ȽK������g����ң�
        plotIconObject = Instantiate(plotIconPrefab, plotIconPosition, Quaternion.identity); 
        plotIconObject.transform.parent = lineObject.transform; 
        plotIconObject.SetActive(false); // �ץ�åȥ��������Ǳ�ʾ�ˤ���

    }


    // Update is called once per frame
    void Update()
    {
        if (T2TLcontrollerScript != null && T2TLcontrollerScript.isCharacterMoving)
        {
            // ����饯���`���Ƅӥ��˥�`������g��
            Debug.Log("isCharacterMoving is true, start moving the character.");

            // �Ƅ��Ф��ä��飺
            if (T2TLcontrollerScript.isCharacterMoving)
            {
                // ����饯���`�άF��λ�ä��ʾ
                Debug.Log("Current character position: " + characterObject.transform.position);


                // �Ƅӷ����Ӌ��
                Vector3 direction = (targetPosition - transform.position).normalized;

                // ���ե�`����ƄӾ��x��Ӌ��
                float step = speed * Time.deltaTime;
                // ����饯���`��λ�ä����
                Vector3 position = characterObject.transform.position;
                Vector3 pos = Vector3.MoveTowards(position, targetPosition, step);
                characterObject.transform.position = pos;

                // ���`���å�λ�ä˵��_�������ɤ���������å�
                if (Vector3.Distance(characterObject.transform.position, targetPosition) < 0.001f)
                {
                    T2TLcontrollerScript.isCharacterMoving = false; // �ƄӤ�ֹͣ
                }

                // ����饯���`��y����plotIconPosition��y���ȵȤ������ɤ���������å�
                if (Mathf.Abs(characterObject.transform.position.y - plotIconPosition.y) < 0.05f)
                {
                    Debug.Log("Character reached plot icon y position");
                    plotIconObject.SetActive(true); // �ץ�åȥ���������ʾ
                }

                // plotIconPosition��y�����ʾ
                Debug.Log("Plot icon y position: " + plotIconPosition.y);
                // characterObject��y�����ʾ
                Debug.Log("Character y position: " + characterObject.transform.position.y);

            }

        }
    }
}
