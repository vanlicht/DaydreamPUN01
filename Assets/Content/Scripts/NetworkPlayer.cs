using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour, IPunObservable
{
    #region Public Properties
    public GameObject otherPlayerController;
    public GameObject playerController;
    public GameObject otherPlayerHead;
    public GameObject playerCamera;
    #endregion

    #region Private Properties
    Vector3 correctPlayerPos;
    Quaternion correctPlayerRot = Quaternion.identity;
    #endregion

    #region MonoBehaviours
    // Update is called once per frame
    void Update ()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5f);
            otherPlayerHead.transform.rotation = Quaternion.Lerp(otherPlayerHead.transform.rotation, this.correctPlayerRot, Time.deltaTime * 5f);
        }
	}
    #endregion

    #region PUN Methods
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(playerCamera.transform.rotation);
            this.photonView.RPC("UpdateOtherPlayerController", PhotonTargets.Others, playerController.transform.localPosition, playerController.transform.localRotation);
        }
        else
        {
            this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }
    #endregion

    #region RPC Methods
    [PunRPC]
    public void UpdateOtherPlayerController(Vector3 pos, Quaternion rot)
    {
        otherPlayerController.transform.localRotation = rot;
        otherPlayerController.transform.localPosition = Vector3.Lerp(otherPlayerController.transform.localPosition, pos, Time.deltaTime * 5f);
    }

    #endregion
}
