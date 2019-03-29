using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRotation : MonoBehaviour
{
    public float rotationAmount;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, rotationAmount);
    }
}
