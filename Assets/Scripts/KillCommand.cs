using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCommand : MonoBehaviour
{
    public GameObject door;
    public GameObject doorlessRoom;
    public GameObject doorRoom;
    public GameObject pillarL3;

    public ParticleSystem particleFocus;

    public void Kill()
    {
        if (GameManager.gameState == 1)
        {
            particleFocus.Play(true);
            doorRoom.SetActive(true);
            doorlessRoom.SetActive(false);
            //door.SetActive(true);

            Destroy(gameObject);
        }
        if (GameManager.gameState == 2)
        {
            pillarL3.SetActive(true);
            particleFocus.Play(true);
            doorRoom.SetActive(true);
            doorlessRoom.SetActive(false);
            //door.SetActive(true);

            Destroy(gameObject);
        }
        
       
    }
}
