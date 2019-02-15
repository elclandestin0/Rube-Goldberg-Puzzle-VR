using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{

    public SteamVR_Action_Vector2 itemsScroll;
    public SteamVR_Action_Boolean itemSpawn;

    public float swipeSum;
    public float touchLast;
    public float touchCurrent;
    public float distance;
    public bool hasSwipedLeft;
    public bool hasSwipedRight;
    public float sensitivity = 0.2f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ScrollLeftItem()
    {

        Debug.Log("Moving menu pieces left");

    }

    public void ScrollRightItem()
    {

        Debug.Log("Moving menu pieces right");
    }

    public void SpawnItem()
    {
        Debug.Log("Spawning Item");

    }

    private void FixedUpdate()
    {

        var leftHoriz = itemsScroll.GetAxis(SteamVR_Input_Sources.LeftHand).x;
        var rightHoriz = itemsScroll.GetAxis(SteamVR_Input_Sources.RightHand).x;
        var rightGrip = itemSpawn.GetStateDown(SteamVR_Input_Sources.RightHand);


        // Move cube left
        if (leftHoriz < -sensitivity || rightHoriz < -sensitivity)
        {
            ScrollLeftItem();
        }

        if (leftHoriz > sensitivity || rightHoriz > sensitivity)
        {
			ScrollRightItem();
        }

        if (rightGrip)
        {
            SpawnItem();
        }

    }
}
