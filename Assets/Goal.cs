using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve;
using Valve.VR;
public class Goal : MonoBehaviour
{

    public SteamVR_LoadLevel loadLevel;


    public bool goalHit = false;
    public bool starsHit = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (goalHit && starsHit)
        {
            Debug.Log("You won this level! Proceeding to the next level...");
            Destroy(GameObject.FindGameObjectWithTag("Ball"));
			loadLevel.Trigger();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ball")
        {
            goalHit = true;
        }
    }
}
