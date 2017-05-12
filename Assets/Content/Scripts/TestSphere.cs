using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSphere : Photon.MonoBehaviour, IPunObservable
{
    bool isContact = false;
    public bool isIndicator = false;

	// Use this for initialization
	void Start ()
    {
        isIndicator = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isContact)
        {
            this.photonView.RPC("PrintOut", PhotonTargets.All, null);
        }
    }

    public void OnClickerContact()
    {
        if (photonView.isMine)
        {
            isContact = true;
        }
    }
    [PunRPC]
    void PrintOut()
    {
        Debug.Log("isContact: " + isContact);
        isIndicator = !isIndicator;
        isContact = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(isContact);
            Debug.Log("Sending isContact as " + isContact);
        }
        else
        {
            this.isContact = (bool)stream.ReceiveNext();
        }
    }
}
