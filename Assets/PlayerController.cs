using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    /* 
        The following three variables are actions that come with the new Steam VR plugin. 
        In the order of flow for spawning the objects, a user shows the items, scrolls 
        through and picks one, and then finally spawn the item of their choice. 
     */
    public SteamVR_Action_Boolean itemsShow;
    public SteamVR_Action_Vector2 itemsScroll;
    public SteamVR_Action_Boolean itemSpawn;


    // int to select objects
    public int currentObject = 0;

    // Object List
    public List<GameObject> objectList;

    // Object Prefab list to instantiate objects
    public List<GameObject> objectPrefabList;
    public float sensitivity = 0.9f;

    // bool to stop scrolling quickly
    bool scroll = false;

    /*
        The following two boolean variables are for the Ball.cs script. If one of them is instantiated, 
        then the ball will add their respective ExitPoint as part of their reference. This will make 
        looking for the exitPoints less expensive as opposed to searching for it in every frame.  
     */
    public bool instantiatedTeleportOne = false;
    public bool instantiatedTeleportTwo = false;

    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            objectList.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowItem()
    {
        objectList[currentObject].SetActive(true);
    }

    public void DeactivateItem()
    {
        objectList[currentObject].SetActive(false);
    }

    public void ScrollLeftItem()
    {
        Debug.Log("Moving menu pieces left");
        objectList[currentObject].SetActive(false);
        currentObject--;

        if (currentObject < 0)
        {
            currentObject = objectList.Count - 1;
        }
        objectList[currentObject].SetActive(true);
    }

    public void ScrollRightItem()
    {
        Debug.Log("Moving menu pieces right");
        objectList[currentObject].SetActive(false);
        currentObject++;

        if (currentObject > objectList.Count - 1)
        {
            currentObject = 0;
        }
        objectList[currentObject].SetActive(true);
    }

    public void SpawnItem()
    {
        Instantiate(objectPrefabList[currentObject],
             objectList[currentObject].transform.position,
             objectList[currentObject].transform.rotation
             );
        if (objectPrefabList[currentObject].tag == "Teleport_Target_One")
        {
            instantiatedTeleportOne = true;
        }

        if (objectPrefabList[currentObject].tag == "Teleport_Target_Two")
        {
            instantiatedTeleportTwo = true;
        }

    }

    private void FixedUpdate()
    {
        var rightTouch = itemsShow.GetState(SteamVR_Input_Sources.RightHand);
        var horizJoystick = itemsScroll.GetAxis(SteamVR_Input_Sources.RightHand).x;
        var rightGrip = itemSpawn.GetStateDown(SteamVR_Input_Sources.RightHand);

        if (rightTouch)
        {
            ShowItem();
            if (horizJoystick < -sensitivity && !scroll)
            {
                Debug.Log(horizJoystick);
                ScrollLeftItem();
                horizJoystick = 0.0f;
                scroll = true;
            }
            else if (horizJoystick > sensitivity && !scroll)
            {
                Debug.Log(horizJoystick);
                ScrollRightItem();
                horizJoystick = 0.0f;
                scroll = true;
            }

            else if (horizJoystick >= 0.0f && horizJoystick <= 0.0f)
            {
                scroll = false;
            }

            if (rightGrip)
            {
                SpawnItem();
            }
        }
        else if (!rightTouch)
        {
            DeactivateItem();
        }
    }
}
