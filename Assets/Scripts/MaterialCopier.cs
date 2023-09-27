using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCopier : MonoBehaviour
{
    public string linkedToName;

    public GameObject linkedTo;

    public Material linkMat;

    public Renderer rend;

    private void Awake()
    {
        linkedTo = GameObject.Find(linkedToName);
        rend = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        rend.material = linkedTo.GetComponent<MeshRenderer>().material;
    }
}
