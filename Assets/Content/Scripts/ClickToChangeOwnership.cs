using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script need to work with PhotonView and change owener: takeover
[RequireComponent(typeof(PhotonView))]
public class ClickToChangeOwnership : Photon.MonoBehaviour
{
    public void ClickOwnership()
    {
        if(photonView.ownerId == PhotonNetwork.player.ID)
        {
            return;
        }
        this.photonView.RequestOwnership();
        Debug.Log("Ownership takeover...");
    }

}
