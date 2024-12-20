﻿using TMPro;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class TextPlayableBehaviour : PlayableBehaviour
{
    public GameObject charaObject;
    private string text;

    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
        this.text = this.charaObject.GetComponent<TextMeshPro>().text;
        this.charaObject.GetComponent<TextMeshPro>().text = "";
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {
        //this.charaObject.GetComponent<TextMeshPro>().text = this.text;

        if (charaObject != null)
        {
            var textMeshPro = charaObject.GetComponent<TextMeshPro>();
            if (textMeshPro != null)
            {
                textMeshPro.text = this.text;
            }
        }
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {

    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {

    }

    // Called each frame while the state is set to Play
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        // PlayableTrackのClip上でシークバーが移動するたびに呼ばれ続ける（PrepareFrameの後）
        if (charaObject == null || this.text == null) { return; }
        var percent = (float)playable.GetTime() / (float)playable.GetDuration();

        this.charaObject.GetComponent<TextMeshPro>().text =
            this.text.Substring(0, (int)Mathf.Round(this.text.Length * percent));
    }
}