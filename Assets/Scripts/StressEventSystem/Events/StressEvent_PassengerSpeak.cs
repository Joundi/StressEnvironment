using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressEvent_PassengerSpeak : StressEvent_Immediate
{

    public string speech;
    private VoiceManager vm;
    private bool speechStarted = false;

    protected override void Start()
    {
        base.Start();
        vm = GameObject.Find("VoiceManager").GetComponent<VoiceManager>();
        if (vm.VoiceInit == 0)
            vm.Start();
    }


    //------------------------------------------------------
    //  Stress Event Start
    //------------------------------------------------------
    override public void StartEvent()
    {
        base.StartEvent();
        vm.Say(speech);
        speechStarted = true;
    }
    // Need a fix here to prevent from logging every frame
    private void Update()
    {
        if (vm.Status(0) != 2 && speechStarted) // speech demarré et fini
        {

        }
        // EndEvent should be called exclusively when the event ends
        //EndEvent();
    }

    override public void EndEvent()
    {
        base.EndEvent();
    }
}
