using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[DisallowMultipleComponent]// only allowed one component on the game object
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;

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
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
    }
}
