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
    }

    private void FixedUpdate()
    {
        var rightTouch = itemsShow.GetState(SteamVR_Input_Sources.RightHand);
        var leftHoriz = itemsScroll.GetAxis(SteamVR_Input_Sources.RightHand).x;
        var rightHoriz = itemsScroll.GetAxis(SteamVR_Input_Sources.RightHand).x;
        var rightGrip = itemSpawn.GetStateDown(SteamVR_Input_Sources.RightHand);

        if (rightTouch)
        {
            ShowItem();
            if (leftHoriz < -sensitivity || rightHoriz < -sensitivity)
            {
                ScrollLeftItem();
                leftHoriz = 0.0f;
                rightHoriz = 0.0f;
            }
            else if (leftHoriz > sensitivity || rightHoriz > sensitivity)
            {
                ScrollRightItem();
                leftHoriz = 0.0f;
                rightHoriz = 0.0f;
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
