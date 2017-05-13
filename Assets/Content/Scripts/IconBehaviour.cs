using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class IconBehaviour : Photon.MonoBehaviour, IPunObservable
{

    public GameObject targetIcon;
    private Vector3 startPosition;
    public Text textLog;

    bool isVisible;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowHideIcon();
        }

        //this.photonView.RPC("RPCShowHideIcon", PhotonTargets.AllBufferedViaServer);

        textLog.text = "isVisible: " + isVisible;
    }

    public void ShowHideIcon()
    {
        if (photonView.isMine)
        {
            
            isVisible = !isVisible;
            
        }
        
    }

    [PunRPC]
    public void RPCShowHideIcon()
    {
        if (isVisible)
        {
            targetIcon.SetActive(isVisible);
        }
        else
        {
            targetIcon.GetComponentInChildren<IconMovement>().disableSelf();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(isVisible);
            this.photonView.RPC("RPCShowHideIcon", PhotonTargets.AllBufferedViaServer);
        }
        else
        {
            this.isVisible = (bool)stream.ReceiveNext();
        }
    }
}
