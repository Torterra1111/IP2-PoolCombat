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
       
        do
        {
            new WaitForSeconds(5.0f);
            transform.position = Vector3.Lerp(Waypoint, Waypoint2, 2.0f);
            transform.position = Vector3.Lerp(Waypoint2, Waypoint, 2.0f);

        } while (travel);


    }

    void Travel1()
    {
        transform.position = Vector3.Lerp(Waypoint, Waypoint2, 2.0f);

    }
    void Travel2()
    {
        transform.position = Vector3.Lerp(Waypoint2, Waypoint, 2.0f);

    }
}
