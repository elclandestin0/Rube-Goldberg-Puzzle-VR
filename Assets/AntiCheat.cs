using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiCheat : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "AntiCheatZone")
        {
            Debug.Log("You are on the " + col.gameObject.name + "!!!");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "AntiCheatZone")
        {
            Debug.Log("You left the " + col.gameObject.name + "!!!");
        }
    }
}
