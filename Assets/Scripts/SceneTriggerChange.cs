using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTriggerChange : MonoBehaviour
{
    public GameObject doorless;
    public GameObject withdoor;
    public VoiceClipPlayer vcRef;

    private void Start()
    {
        doorless.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.gameState++;
            other.gameObject.GetComponent<MusicPlayer>().ChangeMusic();
            doorless.SetActive(true);
            withdoor.SetActive(false);

            if(name=="TriggerL2")
            {
                vcRef.VC1();
            }
            if (name == "TriggerL3")
            {
                vcRef.VC2();
            }
            Destroy(gameObject);
        }
    }
}
