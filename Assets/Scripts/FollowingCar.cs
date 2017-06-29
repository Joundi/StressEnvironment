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
	private float brakeDuration;
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
		reactionTime = 1f;
    }

    void FixedUpdate () {
        // The security distance depends on the player speed.
        // +5 because of the size of both cars, when in collision the Distance between them is 5
        securityDistance = 5 + (speedPlayer * securityDistanceMultiplier);
        if (!disabled)	
        {
            speedPlayer = playerRb.velocity.magnitude;
            if (currentTarget != null)
            {
                transform.LookAt(currentTarget.transform.position);

                if (distance != -404f){
                    // Player is not braking
					if (speedPlayer + 0.16f >= speedPlayerOld && reactionTime == 1f)
                    {
						//Debug.Log("speedPlayer: " + speedPlayer + " speedPlayerOld :" + speedPlayerOld);
                        // Brake to respect the minimum distance
                        if (securityDistance >= Vector3.Distance(transform.position, player.transform.position))
                        {
							Debug.Log ("Brake to respect the security distance");
							distance *= 0.993f;
                        }
                        // Accelerate to reach the player
                        else {
							Debug.Log ("Accelerate to reach the player");
                            distance *= 1.010f;
                        }
                    } else { // Player is Braking
                        //Initiate the reaction Time
						Debug.Log("try to brake :");
						Debug.Log("speedPlayer: " + speedPlayer + " speedPlayerOld :" + speedPlayerOld + " reactionTime :" + reactionTime );

                        if (reactionTime == 1f)
                        {
							reactionTime -= Time.fixedDeltaTime;
							brakeDuration = 0f;
							distance = playerRb.velocity.magnitude * Time.fixedDeltaTime;
                        }
                        else
                        {
							if (reactionTime == 0 && brakeDuration > 0f)
                            {
								//Debug.Log ("Braking");
								distance *= brakingMultiplier;
								brakeDuration -= Time.fixedDeltaTime;
                            }
                            else
                            {
								if (reactionTime > 0f) {
									distance = playerRb.velocity.magnitude * Time.fixedDeltaTime;
									reactionTime -= Time.fixedDeltaTime;
									if (speedPlayer + 0.16f >= speedPlayerOld) {
										brakeDuration += Time.fixedDeltaTime;
									}
									if (reactionTime < 0)
										reactionTime = 0;
								} else {
									reactionTime = 1f;
								}	
                            }
                        }
                    }
                } else {
                    distance = playerRb.velocity.magnitude * Time.fixedDeltaTime;
                }
                transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, distance);
            }
			speedPlayerOld = speedPlayer;
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
