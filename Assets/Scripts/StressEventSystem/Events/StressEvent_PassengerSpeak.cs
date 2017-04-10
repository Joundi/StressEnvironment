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

    private void Update()
    {
        if (vm.Status(0) != 2 && speechStarted) // speech demarré et fini
        {

        }
        EndEvent();
    }

    override public void EndEvent()
    {
        base.EndEvent();
    }
}
