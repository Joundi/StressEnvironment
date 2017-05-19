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
        lastTranslation = new Vector3();
    }

    void FixedUpdate () {
        // The security distance depends on the player speed
        securityDistance = speedPlayer * securityDistanceMultiplier;
        if (!disabled)
        {
            speedPlayer = playerRb.velocity.magnitude;
            if (currentTarget != null)
            {
                transform.LookAt(currentTarget.transform.position);
                Vector3 targetVector = Vector3.Normalize(currentTarget.transform.position - transform.position);
               
                // Player is not braking
                if (speedPlayer >= speedPlayerOld)
                {
                    // Brake to respect the minimum distance
                    if (securityDistance >= Vector3.Distance(transform.position, player.transform.position))
                    {
                        Debug.Log("brake to respect the security distance");
                        targetVector = lastTranslation * 0.993f;
                        lastTranslation = targetVector;
                        Debug.Log(targetVector);
                    }
                    else {
                        Debug.Log("Accelerate to reach the player");
                        targetVector = targetVector * Time.deltaTime * playerRb.velocity.magnitude * 1.03f;
                        targetVector = Vector3.ProjectOnPlane(targetVector, Vector3.up);
                        lastTranslation = targetVector;
                        Debug.Log(targetVector);
                    }
                    if (reactionTime >= 0) reactionTime = -1;

                } else { // Player is Braking
                    //Initiate the reaction Time
                    if (reactionTime == -1)
                    {
                        reactionTime = 1f;
                        targetVector = lastTranslation;
                    }
                    else
                    {
                        if (reactionTime == 0)
                        {
                            //Brake
                            targetVector = lastTranslation * brakingMultiplier;
                            lastTranslation = targetVector;
                            Debug.Log("Strong brake");
                            Debug.Log(targetVector);
                        }
                        else
                        {
                            Debug.Log("Is wainting to brake");
                            targetVector = lastTranslation;
                            reactionTime -= Time.fixedDeltaTime;
                            Debug.Log(targetVector);
                            if (reactionTime < 0) reactionTime = 0;
                        }
                    }
                }
                //lastTranslation = targetVector;
                Debug.Log("Vector applied: " + targetVector);
                transform.Translate(targetVector);
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

    //private void OnTriggerEnter(Collider other)
    //{
    //    // Destroy the target if contact
    //    if (other.gameObject == currentTarget)
    //    {
    //        targets.Remove(currentTarget);
    //        Destroy(currentTarget);
    //        currentTarget = targets.FirstOrDefault();
    //    }  
    //}
    // Destroy the car if it hits the player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            Debug.Log("Hit");
            disabled = true;
            destroyCountdown = 5f;
        }
    }
}
