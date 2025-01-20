using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class saikaiStarting : MonoBehaviour
{
    public PlayableDirector starting;
    private bool hasStartingPlayed;

    // Start is called before the first frame update
    void Start()
    {
        hasStartingPlayed = false;
        starting.stopped += OnPlayableDirectorStopped;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStartingPlayed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("Act_1_saikai");
            }
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == starting)
        {
            hasStartingPlayed = true;
            Debug.Log($"hasStartingPlayed:{hasStartingPlayed}");

        }
    }

    private void OnDestroy()
    {
        starting.stopped -= OnPlayableDirectorStopped;
    }
}
