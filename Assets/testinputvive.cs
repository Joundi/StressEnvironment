using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class testinputvive : MonoBehaviour {

	SteamVR_Controller.Device device;
	public SteamVR_TrackedObject controller;

	Vector2 touchpad;
	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update ()
	{
		device = SteamVR_Controller.Input ((int)controller.index);
		//If finger is on touchpad
		if (device.GetTouch (SteamVR_Controller.ButtonMask.Touchpad)) {
			//Read the touchpad values
			touchpad = device.GetAxis (EVRButtonId.k_EButton_SteamVR_Touchpad);


			Debug.Log (touchpad);
		}
	}
}