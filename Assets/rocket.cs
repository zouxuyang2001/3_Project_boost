using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource thrusting;
    [SerializeField] float rcsRotation = 200f;
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
        Thrusting();
        Rotate();

    }

    void Rotate()
    {
        float rotateThisFrame = rcsRotation * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
           
            transform.Rotate(Vector3.forward*rotateThisFrame);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotateThisFrame);
        }
    }

    void Thrusting()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        { //can thrust while rotating
            
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