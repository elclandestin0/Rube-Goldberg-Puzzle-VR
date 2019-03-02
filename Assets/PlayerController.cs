using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    /* 
        The following three variables are actions that come with the new Steam VR plugin. 
        In the order of flow for spawning the objects, a user shows the items, scrolls 
        through and picks one, and then finally spawn the item of their choice. The 
        last variable is for disabling the UI.
     */
    public SteamVR_Action_Boolean itemsShow;
    public SteamVR_Action_Vector2 itemsScroll;
    public SteamVR_Action_Boolean itemSpawn;
    public SteamVR_Action_Boolean disableUI;


    // int to select objects
    public int currentObject = 0;

    public GameObject AntiCheatUI;

    // Game Objects that play sound from different actions
    public GameObject SpawnObjectSound;
    public GameObject CycleObjectSound;
    public GameObject ShowObjectSound;
    // Object List
    public List<GameObject> objectList;

    // Object Prefab list to instantiate objects
    public List<GameObject> objectPrefabList;
    public float sensitivity = 0.9f;

    // bool to stop scrolling quickly
    bool scroll = false;

    bool showSound = false;

    /*
        The following  boolean variables are for the Ball.cs script. If one of them is instantiated, 
        then the ball will add their respective ExitPoint as part of their reference. This will make 
        looking for the exitPoints less expensive as opposed to searching for it in every frame.  
     */
    public bool instantiatedTeleportOne = false;
    public bool instantiatedTeleportTwo = false;

    // Same logic from above applies to the following boolean variable, but for teleportation
    public bool instantiatedTrampoline = false;


    /* Counter variables to determine how many of each Goldberg Object can be instantiated. 
    Differs in every scene. The other variables of type Text are meant to store the int 
    variables we initialized prior. */
    public int teleportCounterOne;
    public int teleportCounterTwo;
    public int woodPlankCounter;
    public int metalPlankCounter;
    public int trampolineCounter;

    public Text teleportCountOne;
    public Text teleportCountTwo;
    public Text woodPlankCount;
    public Text metalPlankCount;
    public Text trampolineCount;


    // Use this for initialization
    void Start()
    {
        SpawnObjectSound = GameObject.Find("SpawnObjectSound");
        ShowObjectSound = GameObject.Find("ShowObjectSound");
        CycleObjectSound = GameObject.Find("CycleObjectSound");
        foreach (Transform child in transform)
        {
            objectList.Add(child.gameObject);
        }
        trampolineCount.text = "Limit: " + trampolineCounter;
        woodPlankCount.text = "Limit: " + woodPlankCounter;
        metalPlankCount.text = "Limit: " + metalPlankCounter;
        teleportCountOne.text = "Limit: " + teleportCounterOne;
        teleportCountTwo.text = "Limit: " + teleportCounterTwo;
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
        if (objectPrefabList[currentObject].tag == "Teleport_Target_One" && teleportCounterOne != 0)
        {
            InstantiateObject();
            instantiatedTeleportOne = true;
            teleportCounterOne--;
            teleportCountOne.text = "Limit: " + teleportCounterOne;
        }

        if (objectPrefabList[currentObject].tag == "Teleport_Target_Two" && teleportCounterTwo != 0)
        {
            InstantiateObject();
            instantiatedTeleportTwo = true;
            teleportCounterTwo--;
            teleportCountTwo.text = "Limit: " + teleportCounterTwo;
        }

        if (objectPrefabList[currentObject].tag == "Trampoline" && trampolineCounter != 0)
        {
            InstantiateObject();
            instantiatedTrampoline = true;
            trampolineCounter--;
            trampolineCount.text = "Limit: " + trampolineCounter;
        }
        if (objectPrefabList[currentObject].tag == "MetalPlank" && metalPlankCounter != 0)
        {
            InstantiateObject();
            metalPlankCounter--;
            metalPlankCount.text = "Limit: " + metalPlankCounter;
        }
        if (objectPrefabList[currentObject].tag == "WoodPlank" && woodPlankCounter != 0)
        {
            InstantiateObject();
            woodPlankCounter--;
            woodPlankCount.text = "Limit: " + woodPlankCounter;
        }
    }

    public void DeactivateUI()
    {
        AntiCheatUI.SetActive(false);
    }

    public void InstantiateObject()
    {
        Instantiate(objectPrefabList[currentObject],
     objectList[currentObject].transform.position,
     objectList[currentObject].transform.rotation
     );
    }

    private void FixedUpdate()
    {
        var rightTouch = itemsShow.GetState(SteamVR_Input_Sources.RightHand);
        var rightHorizJoystick = itemsScroll.GetAxis(SteamVR_Input_Sources.RightHand).x;
        var rightGripOne = itemSpawn.GetStateDown(SteamVR_Input_Sources.RightHand);
        var leftTrigger = disableUI.GetStateDown(SteamVR_Input_Sources.LeftHand);
        var rightTrigger = disableUI.GetStateDown(SteamVR_Input_Sources.RightHand);

        if (rightTouch)
        {
            ShowItem();
            if (!showSound)
            {
                ShowObjectSound.GetComponent<AudioSource>().Play();
                showSound = true;
            }
            if (rightHorizJoystick < -sensitivity && !scroll)
            {
                ScrollLeftItem();
                ShowObjectSound.GetComponent<AudioSource>().Play();
                rightHorizJoystick = 0.0f;
                scroll = true;
            }
            else if (rightHorizJoystick > sensitivity && !scroll)
            {
                ScrollRightItem();
                ShowObjectSound.GetComponent<AudioSource>().Play();
                rightHorizJoystick = 0.0f;
                scroll = true;
            }

            else if (rightHorizJoystick >= 0.0f && rightHorizJoystick <= 0.0f)
            {
                scroll = false;
            }

            if (rightGripOne)
            {
                SpawnItem();
                SpawnObjectSound.GetComponent<AudioSource>().Play();
            }
        }
        else if (!rightTouch)
        {
            DeactivateItem();
            showSound = false;
        }
        if (leftTrigger || rightTrigger)
        {
            DeactivateUI();
        }
    }
}
