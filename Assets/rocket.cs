using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource thrusting;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        thrusting = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        { //can thrust while rotating
            rigidbody.AddRelativeForce(Vector3.up);

            if (!thrusting.isPlaying)
            {
                thrusting.Play();
            }
        }
        else
        {
            thrusting.Stop();
        }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward);
            }

            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward);
            }


        
    }

}