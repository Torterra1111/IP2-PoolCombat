using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHitEffect : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyObj", 0.2f);
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }
}
