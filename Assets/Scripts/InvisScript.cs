using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisScript : MonoBehaviour
{
    public void Activate()
    {
        SendMessageUpwards("Kill");
    }

    void Kill()
    {
        return;
    }
}
