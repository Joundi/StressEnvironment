using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressEvent_PhoneCall : StressEvent_Immediate {

	private bool isRinging;
	public AudioSource audioSource;
	public Text alertText;

	//------------------------------------------------------
	//  Stress Event Start
	//------------------------------------------------------
	override public void StartEvent()
    {
        base.StartEvent();
		isRinging = true;
		alertText.text = "Votre téléphone sonne. Prenez-le et rejetez l'appel.";
	}


	//------------------------------------------------------
	//  Stress Event End
	//------------------------------------------------------
	override public void EndEvent()
	{
		base.EndEvent();
		alertText.text = "";
	}

	void Update() {
		// Make the phone ring
		if (isRinging && !audioSource.isPlaying){
			Debug.Log("play");
			audioSource.Play();

		}
		// Make the phone stop
		if (!isRinging && audioSource.isPlaying){
			Debug.Log("stop");
			audioSource.Stop();
		}


		// Test the player's input to rejet the call
		if (false){
			RejectCall();
		}
	}

	public void RejectCall(){
		isRinging = false;
		// Call the end of the event
	}
}
