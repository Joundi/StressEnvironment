using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressEvent_PhoneCall : StressEvent_Immediate {

    public PhoneState phone;

	//------------------------------------------------------
	//  Stress Event Start
	//------------------------------------------------------
	override public void StartEvent()
    {
        base.StartEvent();
        phone.MakeRing();
        phone.PhoneCallEnded += CallEnded;
	}

    private void CallEnded()
    {
        Debug.Log("Le téléphone a fini de sonner");
        EndEvent();
    }


    //------------------------------------------------------
    //  Stress Event End
    //------------------------------------------------------
    override public void EndEvent()
	{
		base.EndEvent();
	}

	void Update() {


		// Test the player's input to rejet the call
		/*if (false){
			phone.CancelCall();
            EndEvent();
		}
        */
	}

}
