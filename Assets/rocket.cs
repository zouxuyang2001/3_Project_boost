using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audiosource;
    [SerializeField] float rcsRotation = 200f;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine,level,death;
    [SerializeField] ParticleSystem mainEngineParticles, levelParticles, deathParticles;
    enum State { Alive,Dying,Transcending};
    State state = State.Alive;
   public bool collisionAreEnable = true;
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
        if(state != State.Alive||!collisionAreEnable)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
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
        deathParticles.Play();
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    private void StartFinishSequence()
    {
        state = State.Transcending;
        audiosource.Stop();
        audiosource.PlayOneShot(level);
        levelParticles.Play();
        Invoke("LoadNextScene",levelLoadDelay);//parameterise time
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
            RespondToThrustInput();
            RespondToRotateInput();
        }
        RespondToDebugKey();
    }

    void RespondToDebugKey()
    {
       if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
       else if(Input.GetKeyDown(KeyCode.C)){
            collisionAreEnable = !collisionAreEnable;
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
        
        if (Input.GetKey(KeyCode.Space))
        { //can thrust while rotating
            ApplyThrust();
            
        }
        else
        {
            audiosource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * Time.deltaTime*mainThrust);

        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(mainEngine);
        }

        mainEngineParticles.Play();
    }

    
}  