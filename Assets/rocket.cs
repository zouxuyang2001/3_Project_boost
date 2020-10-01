using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource thrusting;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 1500f;
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
        Thrust();
        Rotate();

    }

    void Rotate() //player can rotate rocket
    {
        rigidbody.freezeRotation = true; // take manual control of rotation
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidbody.freezeRotation = false; // resume physics control of rotation 
    }

    void Thrust() //player can make rocket move
    {
        if (Input.GetKey(KeyCode.Space))
        { //can thrust while rotating
            float thrustThisFrame = mainThrust * Time.deltaTime;
            rigidbody.AddRelativeForce(Vector3.up*thrustThisFrame);

            if (!thrusting.isPlaying)
            {
                thrusting.Play();
            }
        }
        else
        {
            thrusting.Stop();
        }
    }
}