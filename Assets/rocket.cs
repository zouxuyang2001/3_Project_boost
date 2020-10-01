using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()    {
        ProcessInput();
    }

   void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)){ //can thrust while rotating
           rigidbody.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.A)) { 
            print("rotating left");
        }
        
        else if (Input.GetKey(KeyCode.D))
        {
            print("rotating right");
        }


    }
}
