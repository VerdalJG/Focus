using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDisable : MonoBehaviour
{
    GameObject playerInstance;
    public GameObject visorInstance;

    float distancePlayerToVisor;
    float distanceToPlayer;
    MeshRenderer objectMesh;

    private void Start()
    {
        playerInstance = GameObject.FindGameObjectWithTag("Player");
        objectMesh = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        distancePlayerToVisor = Mathf.Abs(Vector3.Distance(playerInstance.transform.position, visorInstance.transform.position));
        distanceToPlayer = Mathf.Abs(Vector3.Distance(gameObject.transform.position, playerInstance.transform.position));

        if (distancePlayerToVisor<distanceToPlayer)
        {
            objectMesh.enabled = true;
        }
        else
        {
            objectMesh.enabled = false;
        }
    }
}
