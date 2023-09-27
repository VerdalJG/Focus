using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReceptor : MonoBehaviour
{
    public GameObject triggerBox;
    TriggerDetect triggerDetectRef;
    // Start is called before the first frame update
    void Start()
    {
        triggerDetectRef = triggerBox.GetComponent<TriggerDetect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerDetectRef.focusable)
        {
            gameObject.tag = "Focusable";
        }
        else
        {
            gameObject.tag = "Unfocusable";
        }
    }
}
