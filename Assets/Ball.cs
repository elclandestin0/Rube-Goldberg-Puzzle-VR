using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{


    // List of star collectibles
    public List<GameObject> collectibles;

    // Exit points for teleport blocks
    public GameObject ExitPointOne;
    public GameObject ExitPointTwo;

    public GameObject Trampoline;

    // GameObject reference to the GameController, in order to use the script's functions
    public GameObject GameController;

    // GameObject referencve to the PlayerController
    public GameObject PlayerController;

    // Force of ball when teleporting
    public float teleportForce;

    public float bounceForce;

    // count of the number of stars the ball collided with
    static public int starsCollected;

    // Empty GameObject for ball to reset to if it hits the floor 
    public GameObject Destination;

    // Boolean that activates if ball has collided with floor
    public bool activatedLerp = false;

    // Boolean that activates if ball has bounced
    public bool bounce = false;

    // Boolean that activates when player is in the anti-cheat zone
    public bool collideWithAssets = false;

    // Time to be added with deltaTime
    float time = 0.0f;

    // Time it takes to move
    public float timeToMove = 3.0f;
    // Use this for initialization
    void Start()
    {
        collectibles = new List<GameObject>();
        foreach (GameObject collectible in GameObject.FindGameObjectsWithTag("Star"))
        {
            collectibles.Add(collectible);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bounce)
        {
            Bounce();
        }

        if (activatedLerp)
        {
            ResetBall();
        }

        if (PlayerController.GetComponent<PlayerController>().instantiatedTeleportOne)
        {
            Debug.Log("First teleporter instantiated!");
            ExitPointOne = GameObject.FindGameObjectWithTag("ExitPointOne");
            PlayerController.GetComponent<PlayerController>().instantiatedTeleportOne = false;
        }

        if (PlayerController.GetComponent<PlayerController>().instantiatedTeleportTwo)
        {
            Debug.Log("Second teleporter instantiated!");
            ExitPointTwo = GameObject.FindGameObjectWithTag("ExitPointTwo");
            PlayerController.GetComponent<PlayerController>().instantiatedTeleportTwo = false;
        }

        if (PlayerController.GetComponent<PlayerController>().instantiatedTrampoline)
        {
            Debug.Log("Trampoline instantiated!");
            Trampoline = GameObject.FindGameObjectWithTag("Trampoline");
            PlayerController.GetComponent<PlayerController>().instantiatedTrampoline = false;
        }

        if (starsCollected == collectibles.Count)
        {
            GameController.GetComponent<GameController>().starsHit = true;
            starsCollected = 0;
        }
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Floor")
        {
            activatedLerp = true;
        }

        if (col.gameObject.tag == "Trampoline" && collideWithAssets)
        {
            bounce = true;
        }

        if (col.gameObject.tag == "Teleport_Target_One" && collideWithAssets)
        {
            Teleport(ExitPointTwo);
        }

        if (col.gameObject.tag == "Teleport_Target_Two" && collideWithAssets)
        {
            Teleport(ExitPointOne);
        }

        if (col.gameObject.tag == "Goal" && collideWithAssets)
        {
            GameController.GetComponent<GameController>().goalHit = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Star" && collideWithAssets)
        {
            col.gameObject.SetActive(false);
            starsCollected++;
        }
    }

    void ResetBall()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        time += Time.deltaTime;
        float lerpTime = time / timeToMove;
        transform.position = Vector3.Lerp(transform.position, Destination.transform.position, lerpTime);

        // reactivate all collectibles from goal
        ReactivateStars();

        // reset starsCollected to zero
        starsCollected = 0;

        GameController.GetComponent<GameController>().starsHit = false;
        GameController.GetComponent<GameController>().goalHit = false;

        if (lerpTime >= 1.0f)
        {
            activatedLerp = false;
            time = 0.0f;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    public void ReactivateStars()
    {
        foreach (GameObject collectible in collectibles)
        {
            collectible.SetActive(true);
        }
    }

    public void Teleport(GameObject ExitPoint)
    {
        transform.position = ExitPoint.transform.position;
        float ballVelocity = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        float finalVelocity = ballVelocity * teleportForce;
        gameObject.GetComponent<Rigidbody>().AddForce(ExitPoint.transform.forward * finalVelocity, ForceMode.Impulse);
    }

    void Bounce()
    {
        // Make trampoline bounce from vector3.forward
        float ballVelocity = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        float finalVelocity = ballVelocity * bounceForce;
        gameObject.GetComponent<Rigidbody>().AddForce(Trampoline.transform.forward * finalVelocity, ForceMode.Impulse);
        bounce = false;
    }
}
