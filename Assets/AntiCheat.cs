using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiCheat : MonoBehaviour
{
	public GameObject Ball; 

    // Use this for initialization
    void Start()
    {
		Ball = GameObject.FindGameObjectWithTag("Ball");
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "AntiCheatZone")
        {
            Debug.Log("You are on the " + col.gameObject.name + "!!!") ;
			Ball.GetComponent<Ball>().collideWithAssets = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "AntiCheatZone")
        {
            Debug.Log("You left the " + col.gameObject.name + "!!!");
			Ball.GetComponent<Ball>().collideWithAssets = false;
        }
    }
}
