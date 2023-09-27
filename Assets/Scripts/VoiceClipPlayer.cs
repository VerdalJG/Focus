using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceClipPlayer : MonoBehaviour
{
    public AudioClip[] voiceClips;

    public AudioSource source;

    bool catchVC3 = false;
    bool catchVC4 = false;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    public void VC1()
    {
        source.clip = voiceClips[1];
        source.Play();
    }

    public void VC2()
    {
        source.clip = voiceClips[2];
        source.Play();
    }

    public void VC4()
    {
        source.clip = voiceClips[4];
        source.Play();
    }

    private void Update()
    {
        if (GameManager.gameState == 3 && catchVC3 == false)
        {
            catchVC3 = true;
            source.clip = voiceClips[3];
            source.Play();
        }
    }
}
