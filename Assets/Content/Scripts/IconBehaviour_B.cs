using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class IconBehaviour_B : Photon.PunBehaviour
{

    public GameObject targetIcon;
    private Vector3 startPosition;
    public Text textLog;

    bool isVisibleIcon;

    private void Start()
    {
        Debug.Log("isVisible 0: " + isVisibleIcon);
    }

    void Update ()
    {

        textLog.text = "isVisibleIcon: " + isVisibleIcon;
    }

    public void T_OnClick()
    {
        if (photonView.isMine)
        {
            isVisibleIcon = !isVisibleIcon;
        }
        //this.photonView.RPC("RPCShowHideIcon", PhotonTargets.AllBuffered, isVisibleIcon);
    }

    public void ShowHideIcon(bool value)
    {
        targetIcon.SetActive(isVisibleIcon);
    }

    //[PunRPC]
    //public void RPCShowHideIcon_Start(bool value)
    //{
    //    Debug.Log("isVisible 3: " + isVisibleIcon);
    //    targetIcon.SetActive(isVisibleIcon);
    //}

    [PunRPC]
    public void RPCShowHideIcon(bool value)
    {
        
        Debug.Log("isVisible 2: " + isVisibleIcon);
        //isVisibleIcon = !isVisibleIcon;
        Debug.Log("isVisible 3: " + isVisibleIcon);
        targetIcon.SetActive(isVisibleIcon);
        Debug.Log("isVisible 4: " + isVisibleIcon);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(isVisibleIcon);
            Debug.Log("isVisible 4: " + isVisibleIcon);
            this.photonView.RPC("RPCShowHideIcon", PhotonTargets.AllBuffered, isVisibleIcon);
        }
        else
        {
            isVisibleIcon = (bool)stream.ReceiveNext();
        }
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        //
    }
}
