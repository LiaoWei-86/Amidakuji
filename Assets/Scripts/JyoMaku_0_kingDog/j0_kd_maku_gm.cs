using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine;

public class j0_kd_maku_gm : MonoBehaviour
{
    public PlayableDirector ani;
    private bool is_ani_played = false;

    // Start is called before the first frame update
    void Start()
    {
        ani.stopped += OnPlayableDirectorStopped;
    }

    private void OnMouseEnter()
    {
        Debug.Log("mouse enter!");
    }

    private void OnMouseOver()
    {
        if (is_ani_played)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("load scene JyoMaku_0_KD_Main");
                SceneManager.LoadScene("JyoMaku_0_KD_Main");
            }
        }
    }

    private void OnMouseExit()
    {
        Debug.Log("mouse out!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == ani)
        {
            is_ani_played = true;
        }
    }

    private void OnDestroy()
    {
        ani.stopped -= OnPlayableDirectorStopped;
    }
}
