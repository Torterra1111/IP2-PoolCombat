using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{   //Trajectory path that animates from origin of the ball to the predicted end point
    private LineRenderer lineRenderer;
    private float counter;
    private float dist;

    public Transform origin;
    public Transform destination;

    public float lineDrawSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>(); //using the LineRenderer component to draw the actual line
        lineRenderer.SetPosition(0, origin.position); //grabs the start position of the ball
        lineRenderer.SetWidth(.45f, .45f); //thiccness of the line, can be modified to be skinnier at the end

        dist = Vector3.Distance(origin.position, destination.position); //gives back the length of the position to animate/draw the line

    }

    // Update is called once per frame
    void Update()
    {
        if (counter < dist)
        {   //increment counter every frame
            counter += .1f / lineDrawSpeed;

            //lerp is linear interpolation between 2 values (dist and counter in this case) based on time
            float x = Mathf.Lerp(0, dist, counter);

            // takes in the start and end points
            Vector3 pointA = origin.position;
            Vector3 pointB = destination.position;

            //get unit vector in the desired direction, multiply it by the desired length and add the starting point. "x" is unknown since it changes over time to animate the line.
            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

            lineRenderer.SetPosition(1, pointAlongLine);
        }

    }
}
