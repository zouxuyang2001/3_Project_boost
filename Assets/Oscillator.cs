using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[DisallowMultipleComponent]// only allowed one component on the game object
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f);
    [SerializeField] float period = 2f;

    //todo remove from inspector later

    [Range(0,1)][SerializeField]float movementFactor; // 0 for not moved, 1 for fully moved
    Vector3 startingPos;
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //todo protect against period is zero
        float cycles = Time.time / period; // grows continuelly from 0

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);//goes from -1 to 1

        movementFactor = (rawSinWave/2f )+ 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;

        
    }
}
