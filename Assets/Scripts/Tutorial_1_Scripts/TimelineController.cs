using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineController : MonoBehaviour
{
    public GameObject introduceMessage; // �Zӭ��å��`����GameObject
    public GameObject firstActMessage; // ��Ļ��å��`����GameObject
    public PlayableDirector firstActPlayableDirector;  // ��Ļ��å��`����PlayableDirector

    private enum GameMode
    {
        TextPlaying,
        WaitForSceneChange
    }

    private GameMode currentGameMode = GameMode.TextPlaying;  // �ǥե���ȥ��`���`�ɤ�TextPlaying
    private bool isFirstActPlaying = false;  // ��Ļ��å��`���������Ф��ɤ�����ʾ���֩`�낎�����ڂ���false

    void Start()
    {
        // PlayableDirector��null�Ǥʤ����Ȥ�_�J�����������˥��٥�Ȥ򥵥֥����饤��
        if (firstActPlayableDirector != null)
        {
            firstActPlayableDirector.stopped += OnPlayableDirectorStopped;
        }
        else
        {
            Debug.LogWarning("PlayableDirector is not assigned.");
        }
    }

    void Update()
    {
        // Enter���`��Ѻ���줿���ɤ���������å�
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (currentGameMode)
            {
                case GameMode.TextPlaying:
                    if (!isFirstActPlaying) // ��Ļ��å��`���������ФǤʤ�����
                    {
                        if (introduceMessage != null) // �Zӭ��å��`����null�Ǥʤ�����
                        {
                            introduceMessage.SetActive(false); // �ǥե���ȤΚZӭ��å��`����Ǳ�ʾ�ˤ���
                        }

                        if (firstActMessage != null)  // ��Ļ��å��`����null�Ǥʤ�����
                        {
                            firstActMessage.SetActive(true);  // ��Ļ��å��`�����ʾ����
                        }

                        // ���� Timeline
                        if (firstActPlayableDirector != null)  // ��Ļ��å��`����PlayableDirector��null�Ǥʤ�����
                        {
                            firstActPlayableDirector.Play();  // ��Ļ��å��`����PlayableDirector����������
                            isFirstActPlaying = true;  // �����Фȥީ`������
                        }
                        else
                        {
                            Debug.LogWarning("PlayableDirector is not assigned.");
                        }
                    }
                    break;

                case GameMode.WaitForSceneChange:
                    // ���`����Ф��椨��
                    SceneManager.LoadScene("Tutorial_2_Scene");
                    break;
            }
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == firstActPlayableDirector)
        {
            isFirstActPlaying = false;  // �������ˤȥީ`������
            currentGameMode = GameMode.WaitForSceneChange;  // ���`���Ф��椨������`�ɤˉ������
            Debug.Log("Timeline playback completed.");
        }
    }

    void OnDestroy()
    {
        // ���٥�ȤΥ��֥����饤�֤������ơ������`�������
        if (firstActPlayableDirector != null)
        {
            firstActPlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }

    /*
    ��OnPlayableDirectorStopped �᥽�åɤΌg�Fԭ��
    OnPlayableDirectorStopped �᥽�åɤϡ�PlayableDirector ��������ֹͣ�����Ȥ��˺��ӳ�����륤�٥�ȥϥ�ɥ�`�Ǥ����g�Fԭ��ϴΤ�ͨ��Ǥ���

    ���٥�ȥ��֥����饤��:

    Start �᥽�åɤǡ�PlayableDirector ��ֹͣ�����Ȥ��� OnPlayableDirectorStopped �᥽�åɤ����ӳ������褦�ˡ�firstActPlayableDirector.stopped ���٥�Ȥ˥��֥����饤�֤��Ƥ��ޤ���
    firstActPlayableDirector.stopped += OnPlayableDirectorStopped;

    ���٥�Ȱk���r�΄I��:

    OnPlayableDirectorStopped �᥽�åɤϡ�PlayableDirector ��������ֹͣ������ԄӵĤ˺��ӳ�����ޤ���
    �᥽�å��ڲ��Ǥϡ�PlayableDirector �� firstActPlayableDirector ��һ�¤��뤫�ɤ�����_�J���ޤ���
    һ�¤�����ϡ�isFirstActPlaying �� false ���O���������������ˤ������Ȥ�ʾ���ޤ���
    �Τˡ�currentGameMode �� WaitForSceneChange �ˉ���������`���Ф��椨������`�ɤˤ��ޤ���
    ����ˡ��ǥХå�����������ơ����������ˤ������Ȥ�֪ͨ���ޤ���

    ���٥�ȥ��֥����饤�֤ν��:

    �����`�����������ˡ�OnDestroy �᥽�åɤǥ��٥�Ȥ�ُ�i�������ޤ���
    firstActPlayableDirector.stopped -= OnPlayableDirectorStopped;
     */
}
