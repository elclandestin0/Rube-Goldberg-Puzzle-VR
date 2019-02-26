using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve;
using Valve.VR;

public class GameController : MonoBehaviour
{

    public SteamVR_LoadLevel loadLevel;
    public GameObject startButton;
    GameObject switchLevelSound;
    GameObject allUIs;
    public GameObject welcome;

    public bool goalHit = false;
    public bool starsHit = false;


    // This boolean only activates in the main menu
    bool menuStart = false;

    bool dismissUI = false;

    // This boolean only activates in the final level
    public bool lastLevel = false;

    bool soundPlay = false;



    // Use this for initialization
    void Start()
    {
        if (loadLevel.levelName == "MainMenu")
        {
            lastLevel = true;
        }

        switchLevelSound = GameObject.Find("SwitchLevelSound");
        allUIs = GameObject.Find("AllUIs");
        welcome = GameObject.Find("Welcome");
        welcome.SetActive(false);
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
            if (!soundPlay)
            {
                switchLevelSound.GetComponent<AudioSource>().Play();
                soundPlay = true;
            }
            welcome.SetActive(true);
        }

        if (dismissUI)
        {
            allUIs.SetActive(false);
        }
    }

    public void ActivateGame()
    {
        menuStart = true;
    }

    public void DisableUIs()
    {
        dismissUI = true;
    }
    public void RestartGame()
    {
        startButton.GetComponent<AudioSource>().Play();
        loadLevel.Trigger();
    }
}
