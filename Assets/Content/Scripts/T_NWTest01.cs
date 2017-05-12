using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_NWTest01 : Photon.MonoBehaviour, IPunObservable
{
    public GameObject target;
    bool isActive;

    // Use this for initialization

    private void Awake()
    {
        target.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.ProcessInputs();

        if (this.target != null && isActive)
        {
            this.target.SetActive(!this.target.GetActive());
            isActive = false;
        }
	}

    private void ProcessInputs()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!this.isActive)
            {
                this.isActive = true;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.isActive);
        }
        else
        {
            this.isActive = (bool)stream.ReceiveNext();
        }
    }
}
