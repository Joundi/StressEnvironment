using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PhoneState : MonoBehaviour {
    public enum ePhoneState { Inactive, Ringing}

    public List<Material> materials;

    SteamVR_Controller.Device device;
    SteamVR_TrackedObject controller;

    Vector2 touchpad;

    private MeshRenderer mr;
    private AudioSource sound;

    public bool Ringing = false; 
    public bool CancelledCall = false;

    public float deadzoneradius;

    // Use this for initialization
    void Start () {
        mr = GetComponent<MeshRenderer>();
        sound = GetComponent<AudioSource>();
        controller = GetComponentInParent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update () {

		if(Ringing != sound.isPlaying)
        {
            if (Ringing)
            {
                sound.Play();
                mr.material = materials[1];
            }
            else
            {
                mr.material = materials[0];
                PhoneCallEnded();
            }
        }

        if(Ringing && controller != null)
        {
            try
            {
                device = SteamVR_Controller.Input((int)controller.index);
                //If finger is on touchpad
                if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    //Read the touchpad values
                    touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

                    if (touchpad.x >= 0 - deadzoneradius)
                    {
                        CancelledCall = true;
                        Debug.Log("Appel accepter");
                    }

                    if (touchpad.x >= 0 + deadzoneradius)
                    {
                        CancelledCall = true;
                        Debug.Log("Appel rejeter");
                    }

                }
            }
            catch (Exception e) { }
        }

        if(CancelledCall)
        {
            sound.Stop();
            Ringing = false;
            CancelledCall = false;
        }
    }

    public void CancelCall()
    {
        CancelledCall = true;
    }

    public void MakeRing()
    {
        Ringing = true;
    }

    public delegate void VoidEvent();
    public event VoidEvent PhoneCallEnded;
}
