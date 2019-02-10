using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

    // Empty GameObject for ball to reset to if it hits the floor 
    public GameObject Destination;

    // Boolean to check if ball has collided with floor
    public bool activatedLerp = false;

    // Time to be added with deltaTime
    float time = 0.0f;

    // Time it takes to move
    public float timeToMove = 2.0f;



    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (activatedLerp == true)
        {
            Debug.Log("Collided with floor: " + activatedLerp);
            ResetBall();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.name == "Floor")
        {
            Debug.Log("Activated Lerp!");
            activatedLerp = true;
        }
    }

    void ResetBall()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log("Resetting the ball ...");
        time += Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, Destination.transform.position, time / timeToMove);

        if (transform.position == Destination.transform.position)
        {
            Debug.Log("Disabling lerp");
            activatedLerp = false;
            time = 0.0f;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

}
