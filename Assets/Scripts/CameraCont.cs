using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCont : MonoBehaviour
{
    float x;
    float y;
    float z;
    public Transform playerBody;

    public float sensitivity = 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        x = sensitivity * Input.GetAxis("Mouse X");
        y = sensitivity * -Input.GetAxis("Mouse Y");
        gameObject.transform.Rotate(y, x, 0);
        z = transform.eulerAngles.z;
        playerBody.transform.Rotate(0, 0, -z);
    }
}
