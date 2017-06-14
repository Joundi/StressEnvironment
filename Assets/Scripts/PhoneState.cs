using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneState : MonoBehaviour {
    public enum ePhoneState { Inactive, Ringing}

    public List<Material> materials;

    private MeshRenderer mr;
    private AudioSource sound;

    public bool Ringing = false; 
    public bool CancelledCall = false;

    // Use this for initialization
    void Start () {
        mr = GetComponent<MeshRenderer>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {

		if(Ringing != sound.isPlaying)
        {
            if (Ringing)
            {
                sound.Play();
               // mr.material = materials[1];
            }
            else
            {
                // mr.material = materials[0];
                PhoneCallEnded();
            }
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
