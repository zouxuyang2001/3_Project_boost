using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audiosource;
    [SerializeField] float rcsRotation = 200f;
    [SerializeField] float mainThrust = 1500f;
    [SerializeField] AudioClip mainEngine,level,death;
    enum State { Alive,Dying,Transcending};
    State state = State.Alive;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
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
                StartFinishSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audiosource.Stop();
        audiosource.PlayOneShot(death);
        Invoke("LoadFirstLevel", 4f);
    }

    private void StartFinishSequence()
    {
        state = State.Transcending;
        audiosource.Stop();
        audiosource.PlayOneShot(level);
        Invoke("LoadNextScene", 2f);//parameterise time
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);// back to level 0.
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1); // allow to do more than 2 levels
    }

    void ProcessInput()    {
        //todo some where to stop the sound on death
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    void RespondToRotateInput()
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

    void RespondToThrustInput()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        { //can thrust while rotating
            ApplyThrust(thrustThisFrame);
        }
        else
        {
            audiosource.Stop();
        }
    }

    private void ApplyThrust(float thrustThisFrame)
    {
        rigidbody.AddRelativeForce(Vector3.up * thrustThisFrame);

        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(mainEngine);
        }
    }
}