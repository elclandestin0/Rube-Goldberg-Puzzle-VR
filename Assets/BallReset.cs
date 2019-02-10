using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Ball has collided with " + col.gameObject.name);
        
        if (col.gameObject.name == "Floor")
        {
            Debug.Log("Reset the god damn ball dawg");
        }
    }

}
