using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class J0_5_GameController : MonoBehaviour
{
    public PlayableDirector j0_5_starting;

    public bool canToNextStage = false;

    // Start is called before the first frame update
    void Start()
    {
        if(j0_5_starting != null)
        {
            j0_5_starting.stopped += OnPlayableDirectorStopped;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) && canToNextStage)
        {
            SceneManager.LoadScene("JyoMaku_1");
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if(director == j0_5_starting)
        {
            canToNextStage = true;

            Debug.Log("canToNextStage:" + canToNextStage);
        }
    }

    void OnDestroy()
    {
        // イベントのサブスクライブを解除して、メモリリークを防ぐ
        if (j0_5_starting != null)
        {
            j0_5_starting.stopped -= OnPlayableDirectorStopped;
        }
    }
}
