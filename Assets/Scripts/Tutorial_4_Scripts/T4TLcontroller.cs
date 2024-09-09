using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class T4TLcontroller : MonoBehaviour
{
    public GameObject start_intro_Message; // ���`�४�֥������� start_intro_Message���_ʼ��å��`����
    public GameObject second_intro_Message; // ���`�४�֥������� second_intro_Message���_ʼ��å��`����
    public GameObject lineCount1_Message; // ���`�४�֥������� lineCount1_Message���Ф�ξ��α���å��`����
    public GameObject lineCount0_Message; // ���`�४�֥������� lineCount0_Message���Ф�ξ��α���å��`����

    public List<GameObject> storyMessages; // ���`�४�֥������� storyMessage�����Z�Υ�å��`����

    public GameObject endMessage; // ���`�४�֥������� endMessage������ǥ��󥰥�å��`����
    public PlayableDirector start_intro_MessagePlayableDirector; // start_intro_Message��PlayableDirector
    public PlayableDirector second_intro_MessagePlayableDirector; // second_intro_Message��PlayableDirector
    public PlayableDirector lineCount1_MessagePlayableDirector; // lineCount1_Message��PlayableDirector
    public PlayableDirector lineCount0_MessagePlayableDirector; // lineCount0_Message��PlayableDirector

    public PlayableDirector[] storyMessagePlayableDirectors; // storyMessage��PlayableDirector

    private int currentStoryIndex = 0; // �ɤ�PlayableDirector���������Ƥ뤫��׷�E���뤿��

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

    private Coroutine knightMovementCoroutine;
    private Coroutine hunterMovementCoroutine;

    public DrawLineT4 DrawLineT4Script;

    public float speed = 3.0f;

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
        if (DrawLineT4Script != null)
        {
            DrawLineT4Script = FindObjectOfType<DrawLineT4>();
        }

        //  �_ʼ�r��second_intro_Message��Ǳ�ʾ�ˤ���
        if (second_intro_Message != null)
        {
            second_intro_Message.SetActive(false);
        }
        //  �_ʼ�r��storyMessage��Ǳ�ʾ�ˤ���
        if (storyMessages != null)
        {
            // storyMessages�ηǱ�ʾ���`�פ��Ф�
            foreach (var message in storyMessages)
            {
                message.SetActive(false);
            }
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

        // PlayableDirector��null�Ǥʤ����Ȥ�_�J�����������˥��٥�Ȥ򥵥֥����饤��
        for (int i = 0; i < storyMessagePlayableDirectors.Length; i++)
        {
            if (storyMessagePlayableDirectors[i] != null)
            {
                storyMessagePlayableDirectors[i].stopped += OnPlayableDirectorStopped;
            }
            else
            {
                Debug.LogWarning($"storyMessagePlayableDirector[{i}] is not assigned.");
            }
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
        //Debug.Log("isHorizontalLineCreated"+ isHorizontalLineCreated);
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
                    //  ���Υ�`�ɤǤϡ��ץ쥤��`��Enter��Ѻ���ȡ�����饯���`���Ƅӣ��ץ�åȥ�����������ɣ����ȩ`��`��å��`�������ɤ�һ�Ĥ��ı�ʾ�����

                    if (hasSecondIntroPlayed == true && isStoryPlaying == false)
                    {

                        if (second_intro_Message != null)
                        {
                            second_intro_Message.SetActive(false);
                        }

                        PlayNextStory();

                        foreach (KeyValuePair<int, Vector3> kvp in DrawLineT4Script.pointsDictionary)
                        {
                            Debug.Log($"PointsKey: {kvp.Key}, PointsTransformPositionVector3: {kvp.Value}");
                        }

                        //StartMovement(new List<int> { 0, 2, 3, 5, 4, 6 }, new List<int> { 1, 3, 2, 4, 5, 7 });

                        switch (currentStoryIndex)
                        {
                            case 0:
                                StartMovement(new List<int> { 0, 2, 3 }, new List<int> { 1, 3, 2 });
                                break;
                            case 1:
                                StartMovement(new List<int> { 3, 3 }, new List<int> { 2, 4 });
                                break;
                            case 2:
                                StartMovement(new List<int> { 3, 5 }, new List<int> { 4, 4 });
                                break;
                            case 3:
                                StartMovement(new List<int> { 5, 4 }, new List<int> { 4, 5 });
                                break;
                            case 4:
                                StartMovement(new List<int> { 4, 6 }, new List<int> { 5, 5 });
                                break;
                            case 5:
                                StartMovement(new List<int> { 6, 6 }, new List<int> { 5, 7 });
                                break;
                        }
                    }


                    break;

                case GameMode.WaitForSceneChange:
                    // ���`����Ф��椨��

                    SceneManager.LoadScene("Tutorial_5_Scene");
                    break;
            }
        }
    }

    private void PlayNextStory()
    {
        Debug.Log("PlayNextStory called, currentStoryIndex: " + currentStoryIndex);

        if (currentStoryIndex < storyMessagePlayableDirectors.Length)
        {
            Debug.Log("Playing story message: " + currentStoryIndex);

            storyMessages[currentStoryIndex].SetActive(true);
            storyMessagePlayableDirectors[currentStoryIndex].Play();
            GeneratePlotIcon(currentStoryIndex);
            isStoryPlaying = true;
        }
        else
        {
            Debug.Log("All story messages played.");
            // Add debug here to check if it's reaching the end too early
            Debug.Log("Switching to WaitForSceneChange mode");

            Debug.Log("All story messages played.");
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
        else if (System.Array.IndexOf(storyMessagePlayableDirectors, director) != -1)
        {
            Debug.Log("A story message PlayableDirector stopped, increasing currentStoryIndex.");

            currentStoryIndex++; // ��������PlayableDirector�ʥ�Щ`����¤���
            Debug.Log("currentStoryIndex" + currentStoryIndex);
            isStoryPlaying = false; // ���ȩ`��`��å��`�����������ˤȥީ`������

            Debug.Log("New currentStoryIndex: " + currentStoryIndex);

            // ȫ���Υ��ȩ`��`��å��`�����������줿����_�J����
            if (currentStoryIndex >= storyMessagePlayableDirectors.Length)
            {

                Debug.Log("All story messages played. Showing end message.");

                isEndPlaying = true;
                endMessage.SetActive(true);
                endMessagePlayableDirector.Play();
            }
        }
        else if (director == endMessagePlayableDirector)
        {
            isEndPlaying = false;  // �������ˤȥީ`������
            currentGameMode = GameMode.WaitForSceneChange;  //  ���`���Ф��椨������`�ɤˉ������
            Debug.Log("endMessage Timeline playback completed.");
        }
    }



    void StartMovement(List<int> knightPath, List<int> hunterPath)
    {
        // �M���Ф��ƄӤ�ֹͣ
        if (knightMovementCoroutine != null)
            StopCoroutine(knightMovementCoroutine);
        if (hunterMovementCoroutine != null)
            StopCoroutine(hunterMovementCoroutine);

        knightMovementCoroutine = StartCoroutine(MoveKnightCoroutine(knightPath));
        hunterMovementCoroutine = StartCoroutine(MoveHunterCoroutine(hunterPath));

    }

    IEnumerator MoveKnightCoroutine(List<int> path)
    {
        foreach (int point in path)
        {
            Vector3 targetPosition = DrawLineT4Script.pointsDictionary[point];
            while (knight.transform.position != targetPosition)
            {
                knight.transform.position = Vector3.MoveTowards(knight.transform.position, targetPosition, Time.deltaTime * speed);
                yield return null;
            }
        }
    }

    IEnumerator MoveHunterCoroutine(List<int> path)
    {
        foreach (int point in path)
        {
            Vector3 targetPosition = DrawLineT4Script.pointsDictionary[point];
            while (hunter.transform.position != targetPosition)
            {
                hunter.transform.position = Vector3.MoveTowards(hunter.transform.position, targetPosition, Time.deltaTime * speed);
                yield return null;
            }
        }
    }

    void GeneratePlotIcon(int index)
    {
        if (index < 4)
        {
            // �ض���λ�ä�plotIcon���ä�
            GameObject plotIcon = Instantiate(DrawLineT4Script.plotIconPrefabs[index], DrawLineT4Script.plotIconPositions[index].position, Quaternion.identity);
            plotIcon.name = "plotIcon" + index; // plotIcon����ǰ��Ĥ���
            Debug.Log($"Generated {plotIcon.name} at position {plotIcon.transform.position}");
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

        for (int i = 0; i < storyMessagePlayableDirectors.Length; i++)
        {
            if (storyMessagePlayableDirectors[i] != null)
            {
                storyMessagePlayableDirectors[i].stopped -= OnPlayableDirectorStopped;
            }
        }

        if (endMessagePlayableDirector != null)
        {
            endMessagePlayableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }
}
