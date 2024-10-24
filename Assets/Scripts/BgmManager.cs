using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;  //  BGMManager は１つだけ存在する
    public AudioSource audioSource;     // BGMを再生するための AudioSource
    public AudioClip bgmClip;           // この BGM　CLIP

    private void Awake()
    {
        //  BGMManager はシーンを切り替えても消されない
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGMを再生する
    public void PlayBGM(AudioClip newBGM)
    {
        if (audioSource.clip != newBGM)  //  BGMを変える必要がないかどうかを確認する
        {
            audioSource.clip = newBGM;
            audioSource.Play();
        }
    }

    // BGMを止める
    public void StopBGM()
    {
        audioSource.Stop();
    }
}
