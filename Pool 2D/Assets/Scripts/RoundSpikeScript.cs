using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSpikeScript : MonoBehaviour
{
    Vector3 Waypoint;
    Vector3 Waypoint2;
    public GameObject WaypointT1;
    public GameObject WaypointT2;

    public bool travel = true;
    // Start is called before the first frame update
    void Start()
    {
        Waypoint = WaypointT1.transform.position;
        Waypoint2 = WaypointT2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Travel());
       
           
    }


    IEnumerator Travel()
    {
        
        transform.position = Vector3.Lerp(Waypoint, Waypoint2, 10.0f);
        yield return new WaitForSeconds(1.0f);
        transform.position = Vector3.Lerp(Waypoint2, Waypoint, 10.0f);
        new WaitForSeconds(1.0f);
    }
    void Travel2()
    {
        transform.position = Vector3.Lerp(Waypoint2, Waypoint, 2.0f);
        new WaitForSeconds(5.0f);
    }
}
