using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTriggers : MonoBehaviour
{ 
    public int triggerNumber;

    public GameObject pillar;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            pillar.GetComponent<CircleTrigger>().TriggerActivated(triggerNumber);
        }
    }
}
