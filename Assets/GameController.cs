using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve;
using Valve.VR;

public class GameController : MonoBehaviour
{

    public SteamVR_LoadLevel loadLevel;

    public GameObject startButton;

    public GameObject switchLevelSound;

    public bool goalHit = false;
    public bool starsHit = false;


    // This boolean only activates in the main menu
    bool menuStart = false;

    bool menuSelect = false;

    // This boolean only activates in the final level
    public bool lastLevel = false;

    bool soundPlay = false;



    // Use this for initialization
    void Start()
    {
        if (loadLevel.levelName == "")
        {
            lastLevel = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (goalHit && starsHit && !lastLevel)
        {
            Debug.Log("You won this level! Proceeding to the next level...");
            Destroy(GameObject.FindGameObjectWithTag("Ball"));
            if (!soundPlay)
            {
                switchLevelSound.GetComponent<AudioSource>().Play();
                soundPlay = true;
            }

            loadLevel.Trigger();
        }

        else if (menuStart)
        {
            startButton.GetComponent<AudioSource>().Play();
            loadLevel.Trigger();
            menuStart = false;
        }

        else if (goalHit && starsHit && lastLevel)
        {
            Destroy(GameObject.FindGameObjectWithTag("Ball"));
            Debug.Log("You won the game!");
        }

    }

    public void ActivateGame()
    {
        menuStart = true;
    }
}
