using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CircleTrigger : MonoBehaviour
{
    public bool levelStarted = false;
    public bool levelFinished = false;

    public int playerProgress;
    public int playerCycles;

    public GameObject[] pathTriggers;

    public Camera cameraRef;
    PostProcessVolume effectRef;
    PostProcessOutline outl;
    Bloom bl;

    public Color[] outlineColors;

    private void Start()
    {
        pathTriggers = GameObject.FindGameObjectsWithTag("PathTriggers");
        effectRef = cameraRef.GetComponent<PostProcessVolume>();
        effectRef.profile.TryGetSettings<PostProcessOutline>(out outl);
        effectRef.profile.TryGetSettings<Bloom>(out bl);
    }

    public void TriggerActivated(int triggerNumber)
    {
        if (!levelStarted && triggerNumber == 0)
        {
            levelStarted = true;
            // Quote

            return;
        }

        if (levelStarted)
        {
            if (triggerNumber - 1 == playerProgress)
            {
                playerProgress++;
            }
            else
            {
                playerProgress = 0;
            }

            if (playerProgress == 3)
            {
                playerProgress = 0;
                playerCycles++;
                OutlineChange(playerCycles);
            }

            if (playerCycles == 3 && !levelFinished)
            {
                // Quote

                //Deactivate triggers
                foreach (GameObject t in pathTriggers)
                {
                    t.SetActive(false);
                }

                // Activate walls/ground/ceiling for interaction
                GameManager.gameState = 4; // Endgame
            }
        }
       
    }

    void OutlineChange(int playerCycle)
    {
        if (playerCycle == 1)
        {
            outl.color.value = outlineColors[1];
            bl.intensity.value = 2;
        }
        if (playerCycle == 2)
        {
            outl.color.value = outlineColors[2];
            bl.intensity.value = 2;
        }
        if (playerCycle == 3)
        {
            outl.color.value = outlineColors[3];
            bl.intensity.value = 2;
        }

        Debug.Log(playerCycle);
        Debug.Log(outl.color.value);
        //outl.color.value = outlineColors[playerCycle];
        //bl.intensity.value = 2;
    }
}
