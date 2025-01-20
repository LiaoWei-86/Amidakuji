using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class T_N_Mdead_gm : MonoBehaviour
{
    public PlayableDirector end_ani;
    private bool end_ani_played = false;

    public Collider menu_collider1;
    public Collider menu_collider2;
    public Collider menu_collider3;

    // Start is called before the first frame update
    void Start()
    {
        end_ani.stopped += OnPlayableDirectorStopped;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider == menu_collider1 || hit.collider == menu_collider2 || hit.collider == menu_collider3)
            {
                Debug.Log("カーソルは menu_BoxCollider の中にある");
                return;
            }

            if (hit.collider == GetComponent<Collider>())
            {
                Debug.Log("Mouse is over BigCollider");
            }
        }
    }

    void OnMouseOver()
    {
        if (end_ani_played)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("JyoMaku_0_king_dog");
            }
        }
    }
    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == end_ani)
        {
            end_ani_played = true;
        }
    }

    void OnDestroy()
    {
        end_ani.stopped -= OnPlayableDirectorStopped;
    }
}
