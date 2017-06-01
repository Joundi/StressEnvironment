using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FollowingCar : MonoBehaviour {

    private GameObject player;
	private GameObject currentTarget;
	private Rigidbody rb;
    private Rigidbody playerRb;

	//public List<GameObject> targets;
	public float speed;
    private float speedPlayer;
    private float speedPlayerOld;
    private float destroyCountdown;
    private float reactionTime;
    private float securityDistance;
    public Vector3 lastTranslation;
    private float distance;
    private bool disabled;

    public float brakingMultiplier;
    public float securityDistanceMultiplier;


    void Start () {
        player = GameObject.Find("CarPlayer");
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody>();
        }
		//targets = new List<GameObject>();
		rb = GetComponent<Rigidbody>();
		if (speed == 0f){
			speed = 10f;
		}
        speedPlayerOld = 0f;
        disabled = false;
        currentTarget = player;
        distance = -404f;
    }

    void FixedUpdate () {
        // The security distance depends on the player speed.
        // +5 because of the size of both cars, when in collision the Distance between them is 5
        securityDistance = 5 + (speedPlayer * securityDistanceMultiplier);
        if (!disabled)
        {
            speedPlayer = playerRb.velocity.magnitude;
            speedPlayerOld = speedPlayer;
            if (currentTarget != null)
            {
                transform.LookAt(currentTarget.transform.position);

                if (distance != -404f){
                    // Player is not braking
                    if (speedPlayer >= speedPlayerOld)
                    {
                        // Brake to respect the minimum distance
                        if (securityDistance >= Vector3.Distance(transform.position, player.transform.position))
                        {
                            distance *= 0.993f;
                        }
                        // Accelerate to reach the player
                        else {
                            distance *= 1.006f;
                        }
                        if (reactionTime >= 0) reactionTime = -1;

                    } else { // Player is Braking
                        //Initiate the reaction Time
                        if (reactionTime == -1)
                        {
                            reactionTime = 1f;
                        }
                        else
                        {
                            if (reactionTime == 0)
                            {
                                distance *= brakingMultiplier;
                            }
                            else
                            {
                                reactionTime -= Time.fixedDeltaTime;
                                if (reactionTime < 0) reactionTime = 0;
                            }
                        }
                    }
                } else {
                    distance = playerRb.velocity.magnitude * Time.fixedDeltaTime;
                }
                transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, distance);
            }
        } else
        {
            if (destroyCountdown <= 0)
            {
                Destroy(this.gameObject);
            } else
            {
                destroyCountdown -= Time.fixedDeltaTime;
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            disabled = true;
            destroyCountdown = 5f;
        }
    }
}
