using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiCheat : MonoBehaviour
{
    public GameObject Ball;

    public GameObject AntiCheatUI;

    bool activateOnce = false;

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
            Debug.Log("You are on the " + col.gameObject.name + "!!!");
            Ball.GetComponent<Ball>().collideWithAssets = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "AntiCheatZone")
        {
            // var trans = 0.5f;
            // var ballColor = Ball.GetComponent<Material>().
			// ballColor.a = trans;
            Ball.GetComponent<Ball>().collideWithAssets = false;
            if (!activateOnce)
            {
                ActivateUI();
            }
        }
    }

    void ActivateUI()
    {
        Debug.Log("Activated UI!");
        AntiCheatUI.SetActive(true);
        activateOnce = true;
    }
}
