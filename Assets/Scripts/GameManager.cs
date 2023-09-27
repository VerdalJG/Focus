using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Zona1;
    public GameObject Zona2;
    public GameObject Zona3;

    public ParticleSystem Zona1ParticleA;
    public ParticleSystem Zona1ParticleB;

    public ParticleSystem Zona2ParticleA;
    public ParticleSystem Zona2ParticleB;

    public ParticleSystem Zona3ParticleA;
    public ParticleSystem Zona3ParticleB;

    PlayerInteract playerInteractRef;

    public static int gameState = 1;

    private void Start()
    {
        playerInteractRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
    }

    private void Update()
    {
        Debug.Log(gameState);
        if (gameState == 1)
        {
            Zona1.SetActive(true);
            Zona2.SetActive(true);
            Zona3.SetActive(false);

            playerInteractRef.ambienceParticles = Zona1ParticleA;
            playerInteractRef.ambienceParticles2 = Zona1ParticleB;
        }
        else if (gameState == 2)
        {
            Zona1.SetActive(false);
            Zona2.SetActive(true);
            Zona3.SetActive(true);

            playerInteractRef.ambienceParticles = Zona2ParticleA;
            playerInteractRef.ambienceParticles2 = Zona2ParticleB;
        }

        else if (gameState >= 3)
        {
            Zona1.SetActive(false);
            Zona2.SetActive(false);
            Zona3.SetActive(true);

            playerInteractRef.ambienceParticles = Zona3ParticleA;
            playerInteractRef.ambienceParticles2 = Zona3ParticleB;
        }
    }
}
