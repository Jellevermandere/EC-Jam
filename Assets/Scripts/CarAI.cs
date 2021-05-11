using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    [SerializeField]
    private float acceleration, steering, maxSpeed, minDistanceToPoint; 

    private RoadController roadController;

    public Vector3 nextGoal;
    private Vector2 speed;
    [SerializeField]
    private int nrOfPoint;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        roadController = GameObject.FindGameObjectWithTag("RoadController").GetComponent<RoadController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = nextGoal - transform.position;

        if(Vector3.Distance(transform.position, nextGoal)< minDistanceToPoint)
        {
            nrOfPoint++;
            //Debug.Log("NextPoint - " + gameObject.name);
            GoToNextPoint();
            
        }
    }

    void GoToNextPoint()
    {
        if (nrOfPoint < roadController.nrOfDeletetPoints)
        {
            transform.position = roadController.roadPoints[roadController.nrOfDeletetPoints];
            nrOfPoint = roadController.nrOfDeletetPoints + 1;
            nextGoal = roadController.roadPoints[nrOfPoint];
        }
        else if (nrOfPoint < roadController.roadPoints.Count)
        {
            nextGoal = roadController.roadPoints[nrOfPoint];
        }
        else
        {
            transform.position = roadController.roadPoints[roadController.nrOfDeletetPoints];
            nrOfPoint = roadController.nrOfDeletetPoints + 1;
            nextGoal = roadController.roadPoints[nrOfPoint];
        }

        
    }

    private void FixedUpdate()
    {
        //rb.AddForce(speed, ForceMode2D.Impulse);

        // Create car rotation
        //float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
        //rb.rotation +=   h * steering;
        //rb.AddTorque(-Vector2.SignedAngle(rb.velocity, rb.GetRelativeVector(Vector2.up))*steering);


        rb.AddTorque(-Vector2.SignedAngle(nextGoal - transform.position, rb.GetRelativeVector(Vector2.up)) * steering);//, ForceMode2D.Impulse);
        rb.AddForce(transform.up * acceleration, ForceMode2D.Impulse);
        //rb.AddForce((transform.up + Vector3.Normalize(nextGoal - transform.position) * 0.1f) * acceleration, ForceMode2D.Impulse);

        // Force max speed limit
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
