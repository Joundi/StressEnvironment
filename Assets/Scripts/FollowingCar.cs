using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FollowingCar : MonoBehaviour {

	//The goal of this script
	//Is to get the nearest target
	//Get to it
	//Destroy it
	//Go to next Target

	private GameObject currentTarget;
	private Rigidbody rb;

	public List<GameObject> targets;
	public float speed;

	void Start () {
		targets = new List<GameObject>();
		rb = GetComponent<Rigidbody>();
		if (speed == 0f){
			speed = 10f;
		}
	}
	void Update () {
		if (currentTarget != null){
			// Rotate to look at the target
			transform.LookAt(currentTarget.transform.position);
			// Translate in the direction of the target
			transform.Translate(currentTarget.transform.position * Time.deltaTime * speed);
			if ( Vector3.Distance(transform.position, currentTarget.transform.position) < Mathf.Epsilon * 10000000000f){
				targets.Remove(currentTarget);
				Destroy(currentTarget);
				currentTarget = targets.FirstOrDefault();
			}
		} else {
			currentTarget = targets.FirstOrDefault();
		}
	}
}
