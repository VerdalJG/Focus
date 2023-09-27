using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public GameObject mainCamera;

    float distance = 200f;

    LayerMask visorObj; // Layer 9
    LayerMask invisObj; // Layer 10
    LayerMask visibleObj; // Layer 12
    LayerMask ground; // Layer 11
    LayerMask walls; // Layer 13

    float timer;
    float timerMax = 3;

    public bool isFocusing = false;
    float OGVolume;
    float OGBlend;
    float OGPitch;

    public ParticleSystem ambienceParticles;
    public ParticleSystem ambienceParticles2;

    public AudioSource musicSource;

    public RectTransform reticle;
    public GameObject black;
    float retTimer;
    float scaleNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        visorObj = 1 << 9;
        invisObj = 1 << 10;
        visibleObj = 1 << 12;
        ground = 1 << 11;
        walls = 1 << 13;

        OGVolume = musicSource.volume;
        OGBlend = musicSource.spatialBlend;
        OGPitch = musicSource.pitch;
    }

    void Update()
    {
        if (GameManager.gameState != 4)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, distance, visibleObj))
            {
                // Reticle effect here
                black.SetActive(true);
                retTimer += Time.deltaTime;
                scaleNum = Mathf.SmoothStep(1, 3, retTimer);
                reticle.localScale = new Vector3(scaleNum, scaleNum, scaleNum);

                if (Input.GetButton("Interact"))
                {
                    hit.collider.gameObject.BroadcastMessage("Activate");
                }
            }

            else if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, distance, invisObj))
            {
                if (hit.collider.gameObject.tag == "Unfocusable")
                {
                    black.SetActive(false);
                    retTimer = 0;
                    scaleNum = 1;
                    reticle.localScale = new Vector3(scaleNum, scaleNum, scaleNum);
                    isFocusing = false;
                    return;
                }

                if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, distance, visorObj) || hit.collider.gameObject.tag == "Focusable")
                {
                    // Reticle effect here
                    black.SetActive(true);
                    retTimer += Time.deltaTime;
                    scaleNum = Mathf.SmoothStep(1, 3, retTimer);
                    reticle.localScale = new Vector3(scaleNum, scaleNum, scaleNum);

                    if (Input.GetButton("Interact"))
                    {
                        isFocusing = true;
                    }
                }
            }
            else
            {
                black.SetActive(false);
                retTimer = 0;
                scaleNum = 1;
                reticle.localScale = new Vector3(scaleNum, scaleNum, scaleNum);
                isFocusing = false;
            }
            if (Input.GetButtonUp("Interact"))
            {
                isFocusing = false;
            }

            if (isFocusing)
            {
                timer += Time.deltaTime;

                var main = ambienceParticles.main;
                var emission = ambienceParticles.emission;
                main.simulationSpeed = Mathf.SmoothStep(0.1f, 0, timer / timerMax);
                emission.rateOverTime = Mathf.SmoothStep(150, 300, timer / timerMax);

                var main2 = ambienceParticles2.main;
                var emission2 = ambienceParticles2.emission;
                main2.simulationSpeed = Mathf.SmoothStep(0.1f, 0, timer / timerMax);
                emission2.rateOverTime = Mathf.SmoothStep(150, 300, timer / timerMax);

                musicSource.volume = Mathf.SmoothStep(OGVolume, 0.5F, timer / timerMax);
                musicSource.spatialBlend = Mathf.SmoothStep(OGBlend, 1, timer / timerMax);
                musicSource.pitch = Mathf.SmoothStep(OGPitch, OGPitch - 0.1f, timer / timerMax);


                if (timer >= timerMax)
                {
                    hit.collider.gameObject.SendMessage("Activate");
                }
            }
            else
            {
                musicSource.volume = Mathf.SmoothStep(musicSource.volume, OGVolume, 0.1F);
                musicSource.spatialBlend = Mathf.SmoothStep(musicSource.spatialBlend, OGBlend, 0.05F);
                musicSource.pitch = Mathf.SmoothStep(musicSource.pitch, OGPitch, 0.1F);

                var main = ambienceParticles.main;
                var emission = ambienceParticles.emission;

                emission.rateOverTime = 150;
                main.simulationSpeed = 0.1f;

                var main2 = ambienceParticles2.main;
                var emission2 = ambienceParticles2.emission;

                emission2.rateOverTime = 100;
                main2.simulationSpeed = 0.1f;
                timer = 0;
            }
        }

        else if (GameManager.gameState == 4)
        {
            RaycastHit terrain;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out terrain, distance, walls))
            {
                if (terrain.collider.gameObject.tag == ("Walls"))
                {
                    // Reticle effect here
                    black.SetActive(true);
                    retTimer += Time.deltaTime;
                    scaleNum = Mathf.SmoothStep(1, 3, retTimer);
                    reticle.localScale = new Vector3(scaleNum, scaleNum, scaleNum);

                    if (Input.GetButton("Interact"))
                    {
                        terrain.collider.gameObject.GetComponent<WallColorChange>().focusing = true;
                    }

                }
                else
                {
                    black.SetActive(false);
                    retTimer = 0;
                    scaleNum = 1;
                    reticle.localScale = new Vector3(scaleNum, scaleNum, scaleNum);
                    isFocusing = false;
                }
            }
            else if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out terrain, distance, ground))
            {
                if (terrain.collider.gameObject.tag == ("Walls"))
                {
                    // Reticle effect here
                    black.SetActive(true);
                    retTimer += Time.deltaTime;
                    scaleNum = Mathf.SmoothStep(1, 3, retTimer);
                    reticle.localScale = new Vector3(scaleNum, scaleNum, scaleNum);

                    if (Input.GetButton("Interact"))
                    {
                        terrain.collider.gameObject.GetComponent<WallColorChange>().focusing = true;
                    }
                }
                else
                {
                    black.SetActive(false);
                    retTimer = 0;
                    scaleNum = 1;
                    reticle.localScale = new Vector3(scaleNum, scaleNum, scaleNum);
                    isFocusing = false;
                }
            }
            else
            {
                black.SetActive(false);
                retTimer = 0;
                scaleNum = 1;
                reticle.localScale = new Vector3(scaleNum, scaleNum, scaleNum);
                isFocusing = false;
                terrain.collider.gameObject.GetComponent<WallColorChange>().focusing = false;

            }
            if (terrain.collider.gameObject.tag == ("Walls"))
            {
                if (Input.GetButtonUp("Interact"))
                {
                    terrain.collider.gameObject.GetComponent<WallColorChange>().focusing = false;
                }
            }
            

        }
      
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward, Color.cyan);
    }
}
