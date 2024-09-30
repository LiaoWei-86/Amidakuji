using System.Collections+ADs-
using System.Collections.Generic+ADs-
using UnityEngine+ADs-
using UnityEngine.Playables+ADs-
using UnityEngine.SceneManagement+ADs-

public class gameController2 : MonoBehaviour
+AHs-
    public GameObject firstMessage+ADs- // +MLIw/DDgMKow1jC4MKcwrzDI- firstMessage+/wiVi1nLMOEwwzC7MPwwuA-1+/wk-
    public PlayableDirector firstMessagePlayableDirector+ADs- // firstMessage+MG4-PlayableDirector
    private bool hasFirstMessagePlayed +AD0- false+ADs- //  firstMessage+MG9RjXUfW4xOhjBVMIwwXzBL/x8wbjDWMPww61AkADswfjBgUY11H1uMToYwVzBmMGowRA-

    public GameObject secondMessage+ADs- // +MLIw/DDgMKow1jC4MKcwrzDI- secondMessage+/wiVi1nLMOEwwzC7MPwwuA-1+/wk-
    public PlayableDirector secondMessagePlayableDirector+ADs- // secondMessage+MG4-PlayableDirector
    private bool hasSecondMessagePlayed +AD0- false+ADs- //  secondMessage+MG9RjXUfW4xOhjBVMIwwXzBL/x8wbjDWMPww61AkADswfjBgUY11H1uMToYwVzBmMGowRA-

    public GameObject thirdMessage+ADs- // +MLIw/DDgMKow1jC4MKcwrzDI- thirdMessage+/wiVi1nLMOEwwzC7MPwwuA-1+/wk-
    public PlayableDirector thirdMessagePlayableDirector+ADs- // thirdMessage+MG4-PlayableDirector
    private bool hasThirdMessagePlayed +AD0- false+ADs- //  thirdMessage+MG9RjXUfW4xOhjBVMIwwXzBL/x8wbjDWMPww61AkADswfjBgUY11H1uMToYwVzBmMGowRA-

    public GameObject charaMessage+ADs- // +MLIw/DDgMKow1jC4MKcwrzDI- charaMessage+/wiVi1nLMOEwwzC7MPwwuA-1+/wk-
    public PlayableDirector charaMessagePlayableDirector+ADs- // charaMessage+MG4-PlayableDirector
    private bool hasCharaMessagePlayed +AD0- false+ADs- //  charaMessage+MG9RjXUfW4xOhjBVMIwwXzBL/x8wbjDWMPww61AkADswfjBgUY11H1uMToYwVzBmMGowRA-

    public GameObject endMessage+ADs- // +MLIw/DDgMKow1jC4MKcwrzDI- endMessage+/wiVi1nLMOEwwzC7MPwwuA-1+/wk-
    public PlayableDirector endMessagePlayableDirector+ADs- // endMessage+MG4-PlayableDirector
    private bool hasEndMessagePlayed +AD0- false+ADs- //  endMessage+MG9RjXUfW4xOhjBVMIwwXzBL/x8wbjDWMPww61AkADswfjBgUY11H1uMToYwVzBmMGowRA-

    public GameObject lineMessage+ADs- // +MLIw/DDgMKow1jC4MKcwrzDI- lineMessage+/wiVi1nLMOEwwzC7MPwwuA-1+/wk-
    public PlayableDirector lineMessagePlayableDirector+ADs- // lineMessage+MG4-PlayableDirector
    private bool hasLineMessagePlayed +AD0- false+ADs- //  lineMessage+MG9RjXUfW4xOhjBVMIwwXzBL/x8wbjDWMPww61AkADswfjBgUY11H1uMToYwVzBmMGowRA-

    public GameObject pointMessage+ADs- // +MLIw/DDgMKow1jC4MKcwrzDI- pointMessage+/wiVi1nLMOEwwzC7MPwwuA-1+/wk-
    public PlayableDirector pointMessagePlayableDirector+ADs- // pointMessage+MG4-PlayableDirector
    private bool hasPointMessagePlayed +AD0- false+ADs- //  pointMessage+MG9RjXUfW4xOhjBVMIwwXzBL/x8wbjDWMPww61AkADswfjBgUY11H1uMToYwVzBmMGowRA-

    public GameObject yoko+AF8-lineMessage+ADs- // +MLIw/DDgMKow1jC4MKcwrzDI- yoko+AF8-lineMessage+/wiVi1nLMOEwwzC7MPwwuA-1+/wk-
    public PlayableDirector yoko+AF8-lineMessagePlayableDirector+ADs- // yoko+AF8-lineMessage+MG4-PlayableDirector
    private bool hasYoko+AF8-LineMessagePlayed +AD0- false+ADs- //  yoko+AF8-lineMessage+MG9RjXUfW4xOhjBVMIwwXzBL/x8wbjDWMPww61AkADswfjBgUY11H1uMToYwVzBmMGowRA-

    public GameObject knight+ADs-
    public GameObject hunter+ADs-
    public GameObject knightPrefab+ADs-
    public GameObject hunterPrefab+ADs-

    public Vector3 offset+AD0- new Vector3(0,-2,0)+ADs-
    public GameObject end1Prefab+ADs-
    public GameObject end2Prefab+ADs-

    // Start is called before the first frame update
    void Start()
    +AHs-
        //  +lYtZy2ZCMGs-secondMessage+MJKXXohoeTowazBZMIs-
        if (secondMessage +ACEAPQ- null)
        +AHs-
            secondMessage.SetActive(false)+ADs-
        +AH0-
        //  +lYtZy2ZCMGs-thirdMessage+MJKXXohoeTowazBZMIs-
        if (thirdMessage +ACEAPQ- null)
        +AHs-
            thirdMessage.SetActive(false)+ADs-
        +AH0-
        if (charaMessage +ACEAPQ- null)
        +AHs-
            charaMessage.SetActive(false)+ADs-
        +AH0-
        if (lineMessage +ACEAPQ- null)
        +AHs-
            lineMessage.SetActive(false)+ADs-
        +AH0-
        if (pointMessage +ACEAPQ- null)
        +AHs-
            pointMessage.SetActive(false)+ADs-
        +AH0-
        if (yoko+AF8-lineMessage +ACEAPQ- null)
        +AHs-
            yoko+AF8-lineMessage.SetActive(false)+ADs-
        +AH0-
        if (endMessage +ACEAPQ- null)
        +AHs-
            endMessage.SetActive(false)+ADs-
        +AH0-

        // PlayableDirector+MEw-null+MGcwajBEMFMwaDCSeLqKjTBXMAFRjXUfW4xOhjCkMNkw8zDIMJIwtTDWMLkwrzDpMKQw1g-
        if (firstMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            firstMessagePlayableDirector.stopped +-+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-
        if (secondMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            secondMessagePlayableDirector.stopped +-+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-
        if (thirdMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            thirdMessagePlayableDirector.stopped +-+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-
        if (charaMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            charaMessagePlayableDirector.stopped +-+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-
        if (pointMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            pointMessagePlayableDirector.stopped +-+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-
        if (lineMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            lineMessagePlayableDirector.stopped +-+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-
        if (yoko+AF8-lineMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            yoko+AF8-lineMessagePlayableDirector.stopped +-+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-
        if (endMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            endMessagePlayableDirector.stopped +-+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-
    +AH0-

    // Update is called once per frame
    void Update()
    +AHs-
        Debug.Log(+ACI-hasLineMessagePlayed:+ACIAKw-hasLineMessagePlayed)+ADs-

        if (Input.GetKeyDown(KeyCode.Return))
        +AHs-
            HandleEnterPress()+ADs-
        +AH0-
    +AH0-

    void HandleEnterPress()
    +AHs-
        if (hasFirstMessagePlayed +AD0APQ- true +ACYAJg- +ACE-hasSecondMessagePlayed)
        +AHs-
            firstMessage.SetActive(false)+ADs-
            secondMessage.SetActive(true)+ADs-
            secondMessagePlayableDirector.Play()+ADs-
        +AH0-
        else if(hasSecondMessagePlayed +AD0APQ- true +ACYAJg- +ACE-hasThirdMessagePlayed)
        +AHs-
            secondMessage.SetActive(false)+ADs-
            thirdMessage.SetActive(true)+ADs-
            thirdMessagePlayableDirector.Play()+ADs-
        +AH0-
        else if (hasThirdMessagePlayed +AD0APQ- true +ACYAJg- +ACE-hasCharaMessagePlayed)
        +AHs-
            thirdMessage.SetActive(false)+ADs-
            knight +AD0- Instantiate(knightPrefab, knight.transform.position, Quaternion.identity)+ADs-
            hunter +AD0- Instantiate(hunterPrefab, hunter.transform.position, Quaternion.identity)+ADs-
            charaMessage.SetActive(true)+ADs-
            charaMessagePlayableDirector.Play()+ADs-
        +AH0-
        else if (hasCharaMessagePlayed +AD0APQ- true +ACYAJg- +ACE-hasEndMessagePlayed)
        +AHs-
            GameObject end1 +AD0- Instantiate(end1Prefab, (knight.transform.position +- offset), Quaternion.identity)+ADs-
            GameObject end2 +AD0- Instantiate(end2Prefab, (hunter.transform.position +- offset), Quaternion.identity)+ADs-
            endMessage.SetActive(true)+ADs-
            endMessagePlayableDirector.Play()+ADs-
        +AH0-
        else if (hasEndMessagePlayed +AD0APQ- true +ACYAJg- +ACE-hasLineMessagePlayed)
        +AHs-
            lineMessage.SetActive(true)+ADs-
            lineMessagePlayableDirector.Play()+ADs-
        +AH0-
        else if(hasLineMessagePlayed +AD0APQ- true +ACYAJg- +ACE-hasPointMessagePlayed)
        +AHs-
            pointMessage.SetActive(true)+ADs-
            pointMessagePlayableDirector.Play()+ADs-
        +AH0-
        else if (hasPointMessagePlayed +AD0APQ- true +ACYAJg- +ACE-hasYoko+AF8-LineMessagePlayed)
        +AHs-
            yoko+AF8-lineMessage.SetActive(true)+ADs-
            yoko+AF8-lineMessagePlayableDirector.Play()+ADs-
        +AH0-
        else if (hasYoko+AF8-LineMessagePlayed +AD0APQ- true )
        +AHs-
            SceneManager.LoadScene(+ACI-Tutorial+AF8-1+AF8-Scene+ACI-)+ADs-
        +AH0-
    +AH0-


    void OnPlayableDirectorStopped(PlayableDirector director)
    +AHs-
        Debug.Log(+ACI-PlayableDirector Stopped: +ACI- +- director.name)+ADs-

        if (director +AD0APQ- firstMessagePlayableDirector)
        +AHs-
            hasFirstMessagePlayed +AD0- true+ADs-

        +AH0-
        else if (director +AD0APQ- secondMessagePlayableDirector)
        +AHs-
            hasSecondMessagePlayed +AD0- true+ADs-


        +AH0-
        else if (director +AD0APQ- thirdMessagePlayableDirector)
        +AHs-
            hasThirdMessagePlayed +AD0- true+ADs-

        +AH0-
        else if (director +AD0APQ- charaMessagePlayableDirector)
        +AHs-
            hasCharaMessagePlayed +AD0- true+ADs-
        +AH0-
        else if (director +AD0APQ- lineMessagePlayableDirector)
        +AHs-
            hasLineMessagePlayed +AD0- true+ADs-
        +AH0-
        else if (director +AD0APQ- pointMessagePlayableDirector)
        +AHs-
            hasPointMessagePlayed +AD0- true+ADs-
        +AH0-
        else if (director +AD0APQ- yoko+AF8-lineMessagePlayableDirector)
        +AHs-
            hasYoko+AF8-LineMessagePlayed +AD0- true+ADs-
        +AH0-
        else if (director +AD0APQ- endMessagePlayableDirector)
        +AHs-
            hasEndMessagePlayed +AD0- true+ADs-
        +AH0-
    +AH0-

    void OnDestroy()
    +AHs-
        // +MKQw2TDzMMgwbjC1MNYwuTCvMOkwpDDWMJKJ45ZkMFcwZjABMOEw4jDqMOow/DCvMJKWMjBQ-
        if (firstMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            firstMessagePlayableDirector.stopped -+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-

        if (secondMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            secondMessagePlayableDirector.stopped -+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-

        if (thirdMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            thirdMessagePlayableDirector.stopped -+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-

        if (charaMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            charaMessagePlayableDirector.stopped -+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-
        if (lineMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            lineMessagePlayableDirector.stopped -+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-
        if (pointMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            pointMessagePlayableDirector.stopped -+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-
        if (yoko+AF8-lineMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            yoko+AF8-lineMessagePlayableDirector.stopped -+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-
        if (endMessagePlayableDirector +ACEAPQ- null)
        +AHs-
            endMessagePlayableDirector.stopped -+AD0- OnPlayableDirectorStopped+ADs-
        +AH0-
    +AH0-

+AH0-
