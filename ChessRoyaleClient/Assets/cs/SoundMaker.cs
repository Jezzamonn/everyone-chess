using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour {

    public AudioSource audioSourceIntro;
    public AudioSource audioSourceLoop;

    private bool startedLoop;

    private void Start()
    {
        audioSourceIntro.Play();
    }

    void FixedUpdate()
    {
        if (!audioSourceIntro.isPlaying && !startedLoop)
        {
            audioSourceLoop.Play();
            Debug.Log("Done playing");
            startedLoop = true;
        }
    }
}
