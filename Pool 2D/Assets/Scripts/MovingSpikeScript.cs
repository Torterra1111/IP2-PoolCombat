using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpikeScript : MonoBehaviour
{
    Vector3 Waypoint;
    Vector3 Waypoint2;
    public GameObject WaypointT1;
    public GameObject WaypointT2;

    public float speed;
    private float startTime;

    //left public to see (when playing in editor) the distance between waypoints, it makes synchronizing different moving traps easier
    public float waypointsDistance;

    bool travel;
    bool travelBack;

    void Start()
    {
        Waypoint = WaypointT1.transform.position;
        Waypoint2 = WaypointT2.transform.position;

        travel = true;
        travelBack = false;

        speed = 2.0f;
        startTime = Time.time;
        waypointsDistance = Vector3.Distance(Waypoint, Waypoint2);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceCovered = (Time.time - startTime) * speed;
        float waypointsDistanceFraction = distanceCovered / waypointsDistance;

        if (travel)
        {
            transform.position = Vector3.Lerp(Waypoint, Waypoint2, waypointsDistanceFraction);
            if (waypointsDistanceFraction < 1 && waypointsDistanceFraction > 0.98f)
            {
                travel = false;
                travelBack = true;
                startTime = Time.time;
                waypointsDistanceFraction = 0;
            }
        }

        if(travelBack)
        {
            transform.position = Vector3.Lerp(Waypoint2, Waypoint, waypointsDistanceFraction);
            if (waypointsDistanceFraction < 1 && waypointsDistanceFraction > 0.98f)
            {
                travel = true;
                travelBack = false;
                startTime = Time.time;
                waypointsDistanceFraction = 0;
            }
        }
    }
}
