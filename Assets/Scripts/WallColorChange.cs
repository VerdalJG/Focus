using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WallColorChange : MonoBehaviour
{
    public Material matBlack;
    public Material matWhite;

    public Renderer rend;
    public bool focusing;

    public bool focusable = true;

    public float focusTimer;
    public float focusMax = 3;

    public static float puzzleProgress;
    bool stop = false;

    public Camera cameraRef;
    PostProcessVolume effectRef;
    PostProcessOutline outl;
    Bloom bl;

    public GameObject finalVC;
    GameObject Player;
    float timer;
    float timerMax = 10;
    public GameObject fade;
    public Color invis;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rend = GetComponent<MeshRenderer>();
        rend.material = matBlack;
        effectRef = cameraRef.GetComponent<PostProcessVolume>();
        effectRef.profile.TryGetSettings<PostProcessOutline>(out outl);
        effectRef.profile.TryGetSettings<Bloom>(out bl);
    }

    private void Update()
    {
        Mathf.Clamp(focusTimer, 0, focusMax);
        if (focusing)
        {
            ColorChange();
        }
        else
        {
            FailFocus();
        }
        if (!focusable) // Cuando se ha terminado el focus
        {
            rend.material = matWhite;
            outl.color.value = Color.black;
            bl.intensity.value = 2;
        }

        if(puzzleProgress >=3 && stop==false)
        {
           stop = true;
           finalVC.GetComponent<VoiceClipPlayer>().VC4();
           Player.GetComponent<MusicPlayer>().ChangeMusic();
        }
        if(stop==true)
        {
            timer = timerMax - Time.deltaTime;
            fade.GetComponent<Renderer>().material.color = Color.Lerp(invis, Color.white, 10);
        }
    }


    public void ColorChange()
    {
        if (focusable)
        {
            if (focusTimer < focusMax)
            {
                focusTimer += Time.deltaTime;
                rend.material.Lerp(matBlack, matWhite, focusTimer / focusMax);
            }
            else
            {
                focusable = false;
                puzzleProgress++;
            }
        }
        
    }

    public void FailFocus()
    {
        if (focusable)
        {
            if (focusTimer > 0)
            {
                focusTimer -= Time.deltaTime;
                rend.material.Lerp(matBlack, matWhite, focusTimer / focusMax);
            }
        }
        
    }
}
