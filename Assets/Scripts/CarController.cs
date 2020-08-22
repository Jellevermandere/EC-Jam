using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*********** Based on : https://answers.unity.com/questions/686025/top-down-2d-car-physics-1.html ***************//

public class CarController : MonoBehaviour
{
    [SerializeField]
    public float acceleration, steering, maxSpeed;

    private float h,v,currentSpeed;
    private Vector2 speed;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get input
        h = -Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // Calculate speed from input and acceleration (transform.up is forward)
        speed = transform.up * (v * acceleration);
    }
    // update every physics update
    void FixedUpdate()
    {
        rb.AddForce(speed, ForceMode2D.Impulse);

        // Create car rotation
        float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
        if (direction >= 0.0f)
        {
            rb.rotation += h * steering * (rb.velocity.magnitude / maxSpeed);
        }
        else
        {
            rb.rotation -= h * steering * (rb.velocity.magnitude / maxSpeed);
        }

        /*
        rb.AddForce(speed, ForceMode2D.Impulse);

        // Create car rotation
        float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
        if (direction >= 0.0f)
        {
            rb.rotation += h * steering * (rb.velocity.magnitude / maxSpeed);
        }
        else
        {
            rb.rotation -= h * steering * (rb.velocity.magnitude / maxSpeed);
        }

        // Change velocity based on rotation
        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.left)) * 2.0f;
        Vector2 relativeForce = Vector2.right * driftForce;
        Debug.DrawLine(rb.position, rb.GetRelativePoint(relativeForce), Color.green);
        rb.AddForce(rb.GetRelativeVector(relativeForce));
        */
        // Force max speed limit
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        currentSpeed = rb.velocity.magnitude;
        
    }
}

