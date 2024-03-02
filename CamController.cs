using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CamController : MonoBehaviour
{

    public float speed = 20.0f;
    public float turnSpeed = 10.0f;
    public float horizontalInput;
    public float forwardInput;
    bool isAuthenticated;
    // Start is called before the first frame update
    void Start()
    {
        //enabled = false;
        //EnableCameraControl();
        
        Debug.Log("Hello World");
    }

    public void EnableCameraControl()
    {
        Debug.Log("In the EnableCameraControl");
        isAuthenticated = true;
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAuthenticated)
            return; // If not authenticated, do nothing

        Debug.Log("In the CamController");

        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //transform.Translate(0, 0, 1);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
    }

    
}
