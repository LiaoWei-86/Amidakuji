using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class JyoMakuToAct1 : MonoBehaviour
{
    public PlayableDirector j2_starting;

    public bool canToNextStage = false;

    // Start is called before the first frame update
    void Start()
    {
        if (j2_starting != null)
        {
            j2_starting.stopped += OnPlayableDirectorStopped;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canToNextStage)
        {
            SceneManager.LoadScene("Act_1");
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == j2_starting)
        {
            canToNextStage = true;

            Debug.Log("canToNextStage:" + canToNextStage);
        }
    }

    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        if (j2_starting != null)
        {
            j2_starting.stopped -= OnPlayableDirectorStopped;
        }
    }
}
