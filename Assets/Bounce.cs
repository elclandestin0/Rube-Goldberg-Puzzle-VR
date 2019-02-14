using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    // Amount of force applied to ball when bounce is activated
    public float bounceForce = 5.0f;

    // Bool to activate bounce
    public bool bounce = false;

    // Ball to add force to when bouncing
    public GameObject ball;



    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (bounce)
        {
			BallBounce();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Ball")
        {
            bounce = true;
        }
    }

    void BallBounce()
    {
        ball.GetComponent<Rigidbody>().AddForce(0, bounceForce, 0, ForceMode.Impulse);
        bounce = false;
    }
}
