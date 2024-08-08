using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class T2TLcontroller : MonoBehaviour
{
    public GameObject startMessage; // ���`�४�֥������� startMessage���_ʼ��å��`����
    public GameObject storyMessage; // ���`�४�֥������� storyMessage�����Z�Υ�å��`����
    public GameObject endMessage; // ���`�४�֥������� endMessage������ǥ��󥰥�å��`����
    public PlayableDirector startMessagePlayableDirector; // startMessage��PlayableDirector
    public PlayableDirector storyMessagePlayableDirector; // storyMessage��PlayableDirector
    public PlayableDirector endMessagePlayableDirector; // endMessage��PlayableDirector

    private bool isStartPlaying = true;  // startMessage�������Ф��ɤ�����ʾ���֩`�낎�����ڂ���true
    private bool isStoryPlaying = false;  // storyMessage�������Ф��ɤ�����ʾ���֩`�낎�����ڂ���false
    private bool isEndPlaying = false;  // endMessage�������Ф��ɤ�����ʾ���֩`�낎�����ڂ���false

    public bool isCharacterMoving = false; // �Tʿ�τӤ��Ƥ뤫�ɤ�����ʾ���֩`�낎�����ڂ���false

    // ���`���`�ɤ��O���������`�ब�g�Ф����Ȥ���3�ĤΥ�`�ɤ��g���Ф��椨���Ф��ޤ�
    private enum GameMode
    {
        StartTextPlaying, // ���`���_ʼ�r�Υƥ����Ȥ�������
        PlayerPlaying, // �ץ쥤��`���������Ƥ���״�B(����Ĥ��Ԥ��ȡ�Enter��Ѻ���������Z�ƥ����Ȥ��������벿��)
        WaitForSceneChange // �F���`��Υ��`�����ݤ��K�ˤ����ץ쥤��`��Enter��Ѻ���Τ���äƴΤΥ��`����Ф��椨��
    }

    private GameMode currentGameMode = GameMode.StartTextPlaying; // �F���`���_ʼ�r�˥��`���`�ɤ�StartTextPlaying���O��


    // Start is called before the first frame update
    void Start()
    {
        //  �_ʼ�r��storyMessage��endMessage��GameObject��Ǳ�ʾ�ˤ���
        if (storyMessage != null && endMessage != null)
        {
            storyMessage.SetActive(false);
            endMessage.SetActive(false);
        }

        // PlayableDirector��null�Ǥʤ����Ȥ�_�J�����������˥��٥�Ȥ򥵥֥����饤��
        if (startMessagePlayableDirector != null)
        {
            startMessagePlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("startMessagePlayableDirector is not assigned.");
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
        // Enter���`��Ѻ���줿���ɤ���������å�
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (currentGameMode)
            {
                case GameMode.StartTextPlaying:

                    break;

                case GameMode.PlayerPlaying:
                    //  ���Υ�`�ɤǤϡ��ץ쥤��`��Enter��Ѻ���ȡ�����饯���`���Ӥ���story��endMessagePlayableDirector�����������

                    isCharacterMoving = true; //�Tʿ���Ӥ�

                    if (!isStoryPlaying)
                    {
                        if(storyMessage != null)
                        {
                            startMessage.SetActive(false);
                        }

                        // storyMessage��GameObject�򥢥��ƥ��֤ˤ���
                        if (storyMessage != null && endMessage != null)
                        {
                            storyMessage.SetActive(true);
                        }

                        if (storyMessagePlayableDirector != null && endMessagePlayableDirector != null)  
                        {
                            storyMessagePlayableDirector.Play();  // storyMessage��PlayableDirector����������
                            isStoryPlaying = true; // �����Фȥީ`������
                            
                        }
                    }

                    break;

                case GameMode.WaitForSceneChange:
                    // ���`����Ф��椨��
                    SceneManager.LoadScene("Tutorial_3_Scene");
                    break;
            }
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == startMessagePlayableDirector)
        {
            isStartPlaying = false;  // �������ˤȥީ`������
            currentGameMode = GameMode.PlayerPlaying;  // PlayerPlaying��`�ɤˉ������
            Debug.Log("startMessage Timeline playback completed.");
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
        if (startMessagePlayableDirector != null)
        {
            startMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
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
