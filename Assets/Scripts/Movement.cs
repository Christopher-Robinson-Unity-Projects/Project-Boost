using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigBody;
    [SerializeField] float vThrust = 1f;
    [SerializeField] float rotationThrust = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rigBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("Pressed SPACE - Thrusting");
            rigBody.AddRelativeForce(Vector3.up * Time.deltaTime * vThrust);
        }

        
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Pressed A - Rotate Left");
            ApplyRotation(rotationThrust);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Pressed D - Rotate Right");
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThrust)
    {
        rigBody.freezeRotation = true; // freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThrust);
        rigBody.freezeRotation = false; // unfreeze rotation so physics system takes over
    }
}
