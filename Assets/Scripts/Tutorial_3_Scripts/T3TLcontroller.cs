using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class T3TLcontroller : MonoBehaviour
{
    public GameObject start_intro_Message; // ���`�४�֥������� start_intro_Message���_ʼ��å��`����
    public GameObject second_intro_Message; // ���`�४�֥������� second_intro_Message���_ʼ��å��`����
    public GameObject lineCount1_Message; // ���`�४�֥������� lineCount1_Message���Ф�ξ��α���å��`����
    public GameObject lineCount0_Message; // ���`�४�֥������� lineCount0_Message���Ф�ξ��α���å��`����
    public GameObject storyMessage; // ���`�४�֥������� storyMessage�����Z�Υ�å��`����
    public GameObject endMessage; // ���`�४�֥������� endMessage������ǥ��󥰥�å��`����
    public PlayableDirector start_intro_MessagePlayableDirector; // start_intro_Message��PlayableDirector
    public PlayableDirector second_intro_MessagePlayableDirector; // second_intro_Message��PlayableDirector
    public PlayableDirector lineCount1_MessagePlayableDirector; // lineCount1_Message��PlayableDirector
    public PlayableDirector lineCount0_MessagePlayableDirector; // lineCount0_Message��PlayableDirector
    public PlayableDirector storyMessagePlayableDirector; // storyMessage��PlayableDirector
    public PlayableDirector endMessagePlayableDirector; // endMessage��PlayableDirector

    public bool isHorizontalLineCreated = false;

    private bool isStoryPlaying = false;  // storyMessage�������Ф��ɤ�����ʾ���֩`�낎�����ڂ���false
    private bool isEndPlaying = false;  // endMessage�������Ф��ɤ�����ʾ���֩`�낎�����ڂ���false

    private bool hasSecondIntroPlayed = false;// second_intro_Message�������K�ˤ��ɤ�����ʾ���֩`�낎�����ڂ���false
    private bool haslineCount0_Played = false;// lineCount0_Message�������K�ˤ��ɤ�����ʾ���֩`�낎�����ڂ���false

    public bool isKnightMoving = false; // �Tʿ�τӤ��Ƥ뤫�ɤ�����ʾ���֩`�낎�����ڂ���false
    public bool isHunterMoving = false; // �d���τӤ��Ƥ뤫�ɤ�����ʾ���֩`�낎�����ڂ���false

    public GameObject knight;
    public GameObject hunter;

    public DrawLineT3 DrawLineT3Script; // DrawLineT3������ץȤβ��դ��{���뤿��

    // ���`���`�ɤ��O���������`�ब�g�Ф����Ȥ���3�ĤΥ�`�ɤ��g���Ф��椨���Ф��ޤ�
    private enum GameMode
    {
        TextPlaying, // ���`���_ʼ�r�Υƥ����Ȥ�������
        PlayerPlaying, // �ץ쥤��`���������Ƥ���״�B
        WaitForSceneChange // �F���`��Υ��`�����ݤ��K�ˤ����ץ쥤��`��Enter��Ѻ���Τ���äƴΤΥ��`����Ф��椨��
    }

    private GameMode currentGameMode = GameMode.TextPlaying; // �F���`���_ʼ�r�˥��`���`�ɤ�StartTextPlaying���O��

    // Start is called before the first frame update
    void Start()
    {
        if (DrawLineT3Script != null)
        {
            DrawLineT3Script = FindObjectOfType<DrawLineT3>();
        }

        //  �_ʼ�r��second_intro_Message��Ǳ�ʾ�ˤ���
        if (second_intro_Message != null)
        {
            second_intro_Message.SetActive(false);
        }
        //  �_ʼ�r��storyMessage��Ǳ�ʾ�ˤ���
        if (storyMessage != null )
        {
            storyMessage.SetActive(false);
        }
        //  �_ʼ�r��lineCount0_Message��Ǳ�ʾ�ˤ���
        if (lineCount1_Message != null)
        {
            lineCount1_Message.SetActive(false);
        }
        //  �_ʼ�r��lineCount0_Message��Ǳ�ʾ�ˤ���
        if (lineCount0_Message != null)
        {
            lineCount0_Message.SetActive(false);
        }
        //  �_ʼ�r��endMessage��GameObject��Ǳ�ʾ�ˤ���
        if (endMessage != null)
        {
            endMessage.SetActive(false);
        }


        // PlayableDirector��null�Ǥʤ����Ȥ�_�J�����������˥��٥�Ȥ򥵥֥����饤��
        if (start_intro_MessagePlayableDirector != null)
        {
            start_intro_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("start_intro_MessagePlayableDirector is not assigned.");
        }

        if (second_intro_MessagePlayableDirector != null)
        {
            second_intro_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("start_intro_MessagePlayableDirector is not assigned.");
        }

        if (lineCount1_MessagePlayableDirector != null)
        {
            lineCount1_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("lineCount1_MessagePlayableDirector is not assigned.");
        }

        if (lineCount0_MessagePlayableDirector != null)
        {
            lineCount0_MessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("lineCount1_MessagePlayableDirector is not assigned.");
        }

        if (storyMessagePlayableDirector != null)
        {
            storyMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("storyMessagePlayableDirector is not assigned.");
        }

        if (endMessagePlayableDirector != null)
        {
            endMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("endMessagePlayableDirector is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("isHorizontalLineCreated: " +isHorizontalLineCreated);
        if (isHorizontalLineCreated == true && !hasSecondIntroPlayed)
        {
            currentGameMode = GameMode.TextPlaying;
            start_intro_Message.SetActive(false);
            if (!haslineCount0_Played)
            {
                lineCount1_Message.SetActive(false);
                lineCount0_Message.SetActive(true);
                lineCount0_MessagePlayableDirector.Play();
            }

            second_intro_Message.SetActive(true);
            second_intro_MessagePlayableDirector.Play();

        }

        // Enter���`��Ѻ���줿���ɤ���������å�
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (currentGameMode)
            {
                case GameMode.TextPlaying:

                    break;

                case GameMode.PlayerPlaying:
                    //  ���Υ�`�ɤǤϡ��ץ쥤��`��Enter��Ѻ���ȡ�

                    if (hasSecondIntroPlayed == true && isStoryPlaying == false)
                    {

                        if(second_intro_Message != null)
                        {
                            second_intro_Message.SetActive(false);
                        }

                        if(storyMessage != null && storyMessagePlayableDirector != null)
                        {
                            storyMessage.SetActive(true);
                            second_intro_MessagePlayableDirector.Play();
                            isStoryPlaying = true;

                        }

                        foreach (KeyValuePair<int, Vector3> kvp in DrawLineT3Script.pointsDictionary) //2024.8.17begin here!!!
                        {
                            Debug.Log($"PointsKey: {kvp.Key}, PointsTransformPositionVector3: {kvp.Value}");
                        }
                    }


                    break;

                case GameMode.WaitForSceneChange:
                    // ���`����Ф��椨��

                    SceneManager.LoadScene("Tutorial_4_Scene");
                    break;
            }
        }
    }


    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == start_intro_MessagePlayableDirector)
        {
            lineCount1_Message.SetActive(true);
            lineCount1_MessagePlayableDirector.Play();
            
            Debug.Log("start_intro_Message Timeline playback completed.");
        }
        else if (director == lineCount1_MessagePlayableDirector)
        {
            currentGameMode = GameMode.PlayerPlaying;
            

            Debug.Log("lineCount1_Message Timeline playback completed.");
        }
        else if (director == second_intro_MessagePlayableDirector)
        {
            hasSecondIntroPlayed = true;
            Debug.Log("hasSecondIntroPlayed" + hasSecondIntroPlayed);

            currentGameMode = GameMode.PlayerPlaying;

            Debug.Log("second_intro_Message Timeline playback completed.");
        }
        else if (director == lineCount0_MessagePlayableDirector)
        {
            haslineCount0_Played = true;
            Debug.Log("haslineCount0_Played" + haslineCount0_Played);

            Debug.Log("lineCount0_Message Timeline playback completed.");
        }
        else if (director == storyMessagePlayableDirector)
        {
            isStoryPlaying = false;  // �������ˤȥީ`������

            // storyMessage���������ˤ����餹��endMessage����������
            isEndPlaying = true;
            endMessage.SetActive(true);
            endMessagePlayableDirector.Play();

            Debug.Log("storyMessage Timeline playback completed.");
        }
        else if (director == endMessagePlayableDirector)
        {
            isEndPlaying = false;  // �������ˤȥީ`������
            currentGameMode = GameMode.WaitForSceneChange;  //  ���`���Ф��椨������`�ɤˉ������
            Debug.Log("endMessage Timeline playback completed.");
        }
    }

    void OnDestroy()
    {
        // ���٥�ȤΥ��֥����饤�֤������ơ������`�������
        if (start_intro_MessagePlayableDirector != null)
        {
            start_intro_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (second_intro_MessagePlayableDirector != null)
        {
            second_intro_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (lineCount1_MessagePlayableDirector != null)
        {
            lineCount1_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (lineCount0_MessagePlayableDirector != null)
        {
            lineCount0_MessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (storyMessagePlayableDirector != null)
        {
            storyMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }

        if (endMessagePlayableDirector != null)
        {
            endMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }
}
