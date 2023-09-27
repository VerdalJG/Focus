using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    public AudioClip[] loops;
    public AudioClip[] intros;
    public int gameLevel;

    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        source.clip = intros[GameManager.gameState - 1];
        source.loop = false;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            ChangeMusic();
        }
    }


    public void ChangeMusic()
    {
        source.clip = loops[GameManager.gameState - 1];
        source.loop = true;
        source.Play();
    }
}