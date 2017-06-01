﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreeEvent_FollowingCar : StressEvent_Immediate {

	public Rigidbody carRigidbody;
	//public GameObject target;
	public GameObject followingCarPrefab;
	public float intervalTarget, spawnDistanceMultiplier;

	//private float currentDistance;
	private bool isRunning;
	private GameObject followingCar;
	private FollowingCar followingCarScript;

	// Use this for initialization
	void Start () {
		base.Start();
		if (spawnDistanceMultiplier == 0f){
			spawnDistanceMultiplier = 10f;
		}
		isRunning = false;
	}


	//------------------------------------------------------
    //  Stress Event Start
    //------------------------------------------------------
    override public void StartEvent()
    {
        base.StartEvent();
		isRunning = true;
		//currentDistance = 0f;
		// Instantiate the car just behind the player
		followingCar = Instantiate(followingCarPrefab, carRigidbody.transform.position - carRigidbody.transform.forward * spawnDistanceMultiplier, Quaternion.identity);
        followingCarScript = followingCar.GetComponent<FollowingCar>();

	}
	//------------------------------------------------------
	//  Stress Event End
	//------------------------------------------------------
	override public void EndEvent()
    {
        base.EndEvent();
		isRunning = false;
		Destroy(followingCar);
    }

	//void FixedUpdate(){
	//	if (isRunning){
	//		//Distance since last frame depending on the speed of the car
	//		currentDistance += carRigidbody.velocity.magnitude * Time.fixedDeltaTime;
	//		if (currentDistance > intervalTarget){
	//			followingCarScript.targets.Add(Instantiate(target, carRigidbody.transform.position + new Vector3(0,0.1f,0), Quaternion.identity));
	//			currentDistance = 0f;
	//		}
	//	}
	//}

}