using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressEvent_PhoneCall : StressEvent_InContainer
{

//    public VoiceManager vm;



    protected override void Start()
    {
        base.Start();

    }

    //------------------------------------------------------
    //  implement the behavior of the event
    //------------------------------------------------------
    public override void StartContainerEvent()
    {
        base.StartContainerEvent();

    }

    //------------------------------------------------------
    //  Reset event values, prepare it for the next call
    //------------------------------------------------------
    public override void EndContainerEvent()
    {
        base.EndContainerEvent();

    }
}
