using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float shakeDuration = 0f;
    public float ShakeDecrease = 0.1f;
    public float shakeAmount = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
    }


    void Shake()
    {
        //As long as there is time for shaking
        if (shakeDuration > 0)
        {
            GetComponent<Camera>().transform.position = new Vector3(0f, 0f,-10f) + Random.insideUnitSphere * shakeAmount; //Makes the Camera Shake
            shakeDuration -= Time.deltaTime * ShakeDecrease; //Decreases the time of shaking
        }
        else //If the shake duration is less than 0 (it has ended)
        {
            shakeDuration = 0f; //Makes sure it has ended)
            GetComponent<Camera>().transform.position = new Vector3(0f, 0f, -10f); //Reset camera
        }

    }
}
