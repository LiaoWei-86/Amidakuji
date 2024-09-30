using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public GameObject firstMessage; // ���`�४�֥������� firstMessage���_ʼ��å��`��1��
    public PlayableDirector firstMessagePlayableDirector; // firstMessage��PlayableDirector
    private bool hasFirstMessagePlayed = false; //  firstMessage���������ˤ��줿�����Υ֩`�낎;�ޤ��������ˤ��Ƥʤ�

    public GameObject secondMessage; // ���`�४�֥������� secondMessage���_ʼ��å��`��1��
    public PlayableDirector secondMessagePlayableDirector; // secondMessage��PlayableDirector
    private bool hasSecondMessagePlayed = false; //  secondMessage���������ˤ��줿�����Υ֩`�낎;�ޤ��������ˤ��Ƥʤ�

    public GameObject thirdMessage; // ���`�४�֥������� thirdMessage���_ʼ��å��`��1��
    public PlayableDirector thirdMessagePlayableDirector; // thirdMessage��PlayableDirector
    private bool hasThirdMessagePlayed = false; //  thirdMessage���������ˤ��줿�����Υ֩`�낎;�ޤ��������ˤ��Ƥʤ�

    public GameObject charaMessage; // ���`�४�֥������� charaMessage���_ʼ��å��`��1��
    public PlayableDirector charaMessagePlayableDirector; // charaMessage��PlayableDirector
    private bool hasCharaMessagePlayed = false; //  charaMessage���������ˤ��줿�����Υ֩`�낎;�ޤ��������ˤ��Ƥʤ�

    public GameObject endMessage; // ���`�४�֥������� endMessage���_ʼ��å��`��1��
    public PlayableDirector endMessagePlayableDirector; // endMessage��PlayableDirector
    private bool hasEndMessagePlayed = false; //  endMessage���������ˤ��줿�����Υ֩`�낎;�ޤ��������ˤ��Ƥʤ�

    public GameObject lineMessage; // ���`�४�֥������� lineMessage���_ʼ��å��`��1��
    public PlayableDirector lineMessagePlayableDirector; // lineMessage��PlayableDirector
    private bool hasLineMessagePlayed = false; //  lineMessage���������ˤ��줿�����Υ֩`�낎;�ޤ��������ˤ��Ƥʤ�

    public GameObject pointMessage; // ���`�४�֥������� pointMessage���_ʼ��å��`��1��
    public PlayableDirector pointMessagePlayableDirector; // pointMessage��PlayableDirector
    private bool hasPointMessagePlayed = false; //  pointMessage���������ˤ��줿�����Υ֩`�낎;�ޤ��������ˤ��Ƥʤ�

    public GameObject yoko_lineMessage; // ���`�४�֥������� yoko_lineMessage���_ʼ��å��`��1��
    public PlayableDirector yoko_lineMessagePlayableDirector; // yoko_lineMessage��PlayableDirector
    private bool hasYoko_LineMessagePlayed = false; //  yoko_lineMessage���������ˤ��줿�����Υ֩`�낎;�ޤ��������ˤ��Ƥʤ�

    public GameObject knight;
    public GameObject hunter;
    public GameObject knightPrefab;
    public GameObject hunterPrefab;

    public Vector3 offset= new Vector3(0,-2,0);
    public GameObject end1Prefab;
    public GameObject end2Prefab;

    // Start is called before the first frame update
    void Start()
    {
        //  �_ʼ�r��secondMessage��Ǳ�ʾ�ˤ���
        if (secondMessage != null)
        {
            secondMessage.SetActive(false);
        }
        //  �_ʼ�r��thirdMessage��Ǳ�ʾ�ˤ���
        if (thirdMessage != null)
        {
            thirdMessage.SetActive(false);
        }
        if (charaMessage != null)
        {
            charaMessage.SetActive(false);
        }
        if (lineMessage != null)
        {
            lineMessage.SetActive(false);
        }
        if (pointMessage != null)
        {
            pointMessage.SetActive(false);
        }
        if (yoko_lineMessage != null)
        {
            yoko_lineMessage.SetActive(false);
        }
        if (endMessage != null)
        {
            endMessage.SetActive(false);
        }

        // PlayableDirector��null�Ǥʤ����Ȥ�_�J�����������˥��٥�Ȥ򥵥֥����饤��
        if (firstMessagePlayableDirector != null)
        {
            firstMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (secondMessagePlayableDirector != null)
        {
            secondMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (thirdMessagePlayableDirector != null)
        {
            thirdMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (charaMessagePlayableDirector != null)
        {
            charaMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (pointMessagePlayableDirector != null)
        {
            pointMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (lineMessagePlayableDirector != null)
        {
            lineMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (yoko_lineMessagePlayableDirector != null)
        {
            yoko_lineMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        if (endMessagePlayableDirector != null)
        {
            endMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("hasLineMessagePlayed:"+hasLineMessagePlayed);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            HandleEnterPress();
        }
    }

    void HandleEnterPress()
    {
        if (hasFirstMessagePlayed == true && !hasSecondMessagePlayed)
        {
            firstMessage.SetActive(false);
            secondMessage.SetActive(true);
            secondMessagePlayableDirector.Play();
        }
        else if(hasSecondMessagePlayed == true && !hasThirdMessagePlayed)
        {
            secondMessage.SetActive(false);
            thirdMessage.SetActive(true);
            thirdMessagePlayableDirector.Play();
        }
        else if (hasThirdMessagePlayed == true && !hasCharaMessagePlayed)
        {
            thirdMessage.SetActive(false);
            knight = Instantiate(knightPrefab, knight.transform.position, Quaternion.identity);
            hunter = Instantiate(hunterPrefab, hunter.transform.position, Quaternion.identity);
            charaMessage.SetActive(true);
            charaMessagePlayableDirector.Play();
        }
        else if (hasCharaMessagePlayed == true && !hasEndMessagePlayed)
        {
            GameObject end1 = Instantiate(end1Prefab, (knight.transform.position + offset), Quaternion.identity);
            GameObject end2 = Instantiate(end2Prefab, (hunter.transform.position + offset), Quaternion.identity);
            endMessage.SetActive(true);
            endMessagePlayableDirector.Play();
        }
        else if (hasEndMessagePlayed == true && !hasLineMessagePlayed)
        {
            lineMessage.SetActive(true);
            lineMessagePlayableDirector.Play();
        }
        else if(hasLineMessagePlayed == true && !hasPointMessagePlayed)
        {
            pointMessage.SetActive(true);
            pointMessagePlayableDirector.Play();
        }
        else if (hasPointMessagePlayed == true && !hasYoko_LineMessagePlayed)
        {
            yoko_lineMessage.SetActive(true);
            yoko_lineMessagePlayableDirector.Play();
        }
        else if (hasYoko_LineMessagePlayed == true )
        {
            SceneManager.LoadScene("Tutorial_1_Scene");
        }
    }


    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        Debug.Log("PlayableDirector Stopped: " + director.name);

        if (director == firstMessagePlayableDirector)
        {
            hasFirstMessagePlayed = true;

        }
        else if (director == secondMessagePlayableDirector)
        {
            hasSecondMessagePlayed = true;


        }
        else if (director == thirdMessagePlayableDirector)
        {
            hasThirdMessagePlayed = true;

        }
        else if (director == charaMessagePlayableDirector)
        {
            hasCharaMessagePlayed = true;
        }
        else if (director == lineMessagePlayableDirector)
        {
            hasLineMessagePlayed = true;
        }
        else if (director == pointMessagePlayableDirector)
        {
            hasPointMessagePlayed = true;
        }
        else if (director == yoko_lineMessagePlayableDirector)
        {
            hasYoko_LineMessagePlayed = true;
        }
        else if (director == endMessagePlayableDirector)
        {
            hasEndMessagePlayed = true;
        }
    }

    void OnDestroy()
    {
        // ���٥�ȤΥ��֥����饤�֤������ơ������`�������
        if (firstMessagePlayableDirector != null)
        {
            firstMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (secondMessagePlayableDirector != null)
        {
            secondMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (thirdMessagePlayableDirector != null)
        {
            thirdMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (charaMessagePlayableDirector != null)
        {
            charaMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (lineMessagePlayableDirector != null)
        {
            lineMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (pointMessagePlayableDirector != null)
        {
            pointMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (yoko_lineMessagePlayableDirector != null)
        {
            yoko_lineMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
        if (endMessagePlayableDirector != null)
        {
            endMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }

}
