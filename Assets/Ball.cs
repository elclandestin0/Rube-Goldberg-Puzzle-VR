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

    // Gameobject reference to the goal, in order to use the script's functions
    public GameObject Goal;

    // Force of ball when teleporting
    public float force = 5.0f;

    // count of the number of stars the ball collided with
    static public int starsCollected;

    // Empty GameObject for ball to reset to if it hits the floor 
    public GameObject Destination;

    // Boolean to check if ball has collided with floor
    public bool activatedLerp = false;

    // Boolean to find exitPoints
    public bool findExitPoints = false;


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
        if (activatedLerp == true)
        {
            ResetBall();
        }
        ExitPointOne = GameObject.FindGameObjectWithTag("ExitPointOne");
        ExitPointTwo = GameObject.FindGameObjectWithTag("ExitPointTwo");

        if (starsCollected == collectibles.Count)
        {
            Goal.GetComponent<Goal>().starsHit = true;
            starsCollected = 0;
        }
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.name == "Floor")
        {
            activatedLerp = true;
        }

        if (col.gameObject.name == "Teleport_Target_One")
        {
            transform.position = ExitPointTwo.transform.position;
            float ballVelocity = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            float finalVelocity = ballVelocity * force;
            gameObject.GetComponent<Rigidbody>().AddForce(ExitPointTwo.transform.forward * finalVelocity, ForceMode.Impulse);
        }

        if (col.gameObject.name == "Teleport_Target_Two")
        {
            transform.position = ExitPointOne.transform.position;
            float ballVelocity = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            float finalVelocity = ballVelocity * force;
            gameObject.GetComponent<Rigidbody>().AddForce(ExitPointOne.transform.forward * finalVelocity, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Star")
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

        Goal.GetComponent<Goal>().starsHit = false;
        Goal.GetComponent<Goal>().goalHit = false;


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
}
