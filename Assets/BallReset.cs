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
    public float timeToMove = 3.0f;



    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (activatedLerp == true)
        {
            ResetBall();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.name == "Floor")
        {
            activatedLerp = true;
        }
    }

    void ResetBall()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        time += Time.deltaTime;
        float lerpTime = time / timeToMove;
        transform.position = Vector3.Lerp(transform.position, Destination.transform.position, lerpTime);

        if (lerpTime >= 1.0f)
        {
            activatedLerp = false;
            time = 0.0f;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

}
