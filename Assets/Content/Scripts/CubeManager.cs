using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : Photon.MonoBehaviour
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
