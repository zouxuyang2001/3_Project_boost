using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource thrusting;
    [SerializeField] float rcsRotation = 200f;
    [SerializeField] float mainThrust = 1500f;
    [SerializeField] AudioClip mainEngine;

    enum State { Alive,Dying,Transcending};
    State state = State.Alive;
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

    void OnCollisionEnter(Collision collision)
    {
        //ignore collision if not alive
        if(state != State.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do something
                print("OK");
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextScene", 2f);//parameterise time
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
                break;
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);// back to level 0.
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1); // allow to do more than 2 levels
    }

    void ProcessInput()
    {
        //todo some where to stop the sound on death
        if (state == State.Alive)
        {
            Thrusting();
            Rotate();
        }
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